using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Specialized;

namespace RiskApps3.Utilities
{
    class RefreshTables
    {
        public static void updateTables()
        {
            updateTables(false);
        }

        // if force is true, update tables no matter what the dates are for the last update or the dates of tables on the server
        public static void updateTables(Boolean bForce)
        {
            NameValueCollection values = Configurator.GetConfig("globals");
            string APIServiceActive = values["APIServiceActive"] ?? "False";

            if (APIServiceActive != "True")
            {
                //don't overwrite any tables and simply return
                return;
            }

            if (!bForce)
            {
                //get the local script date of the most recent script run
                string localDBDate = (string)BCDB2.Instance.ExecuteScalarQuery(@"
                    SELECT TOP (1) scriptDate
	                FROM tblScriptsRun
	                ORDER BY dateRun DESC, scriptDate DESC");

                if (!String.IsNullOrEmpty(localDBDate))
                {
                    //test to see if the local DB needs freshening
                    //by comparing the result of the similar script run on the Server
                    string APIGetScriptsRunDateURL = values["APIGetScriptsRunDateURL"] ?? "None";
                    if (APIGetScriptsRunDateURL == "None") return;

                    //var jsonScriptsRunDate = new WebClient().DownloadString(APIGetScriptsRunDateURL);
                    //new way: get response w/in 5 seconds or exception out
                    try
                    {
                        var jsonScriptsRunDate = new TimedWebClient(5000).DownloadString(APIGetScriptsRunDateURL);

                        var jObjScriptsRunDate = (JObject)JsonConvert.DeserializeObject(jsonScriptsRunDate);
                        string scriptsRunDate = jObjScriptsRunDate.SelectToken("scriptsRunDate").ToObject<string>();
                        //since both dates are char strings in YYYY-MM-DD format, which go from highest to least significant digits,
                        //we can use direct string comparison without bothering to convert to dates
                        if (String.Compare(localDBDate, scriptsRunDate) >= 0)  //local DB is already more recently updated or same as the Service's date
                        {
                            return;
                        }
                    }
                    catch (WebException wex)
                    {
                        Logger.Instance.WriteToLog("[RefreshTables] could not connect to URL " + APIGetScriptsRunDateURL + ":\n\t" + wex.Message);
                        return;
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog("[RefreshTables] could not process response from " + APIGetScriptsRunDateURL + ":\n\t" + ex.Message);
                        return;
                    }
                }
            }
            
            string APIGetGenTablesURL = values["APIGetGenTablesURL"] ?? "None";
            if (APIGetGenTablesURL == "None") return;
            var json = ""; //old way didn't handle firewall issue properly: new WebClient().DownloadString(APIGetGenTablesURL);
            //new way: get response w/in 10 seconds or exception out
            try
            {
                json = new TimedWebClient(10000).DownloadString(APIGetGenTablesURL);
            }
            catch (WebException wex)
            {
                Logger.Instance.WriteToLog("[RefreshTables] could not connect to URL " + APIGetGenTablesURL + ":\n\t" + wex.Message);
                return;
            }

            var jArr = (JArray)JsonConvert.DeserializeObject(json);

            var tableList = jArr.ToObject<object[]>().ToList();

            foreach (JObject jObj in tableList)
            {
                string tableName = jObj.SelectToken("tableName").ToObject<string>();

                var colNamesList = jObj.SelectToken("colNames").ToObject<string[]>().ToList();
                string colNames = String.Join(",", colNamesList.ToArray());

                var dataList = jObj.SelectToken("data").ToObject<object[][]>().ToList();
                var dataRows = dataList.ToArray<object[]>();

                DataTable dt = new DataTable(tableName);
                foreach (var c in colNamesList)
                {
                    DataColumn col = new DataColumn();
                    dt.Columns.Add(col);
                }

                // add all rows to DataRowCollection. 
                foreach (var r in dataRows)
                {
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = r;
                    dt.Rows.Add(dr);
                }

                //remove existing records in table
                BCDB2.Instance.ExecuteNonQuery("TRUNCATE TABLE " + tableName);

                //insert new data drom Service into table
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(BCDB2.Instance.getConnectionString()))
                {
                    bulkCopy.DestinationTableName = tableName;
                    try
                    {
                        // Write from the source to the destination.
                        bulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog("[RefreshTables] could not do SQL bulk copy of table " + tableName + ":\n\t" + ex.Message);
                    }
                }
                Logger.Instance.WriteToLog("RefreshTables successfully updated table " + tableName + " from the Server.");
            }
        }

    }

}

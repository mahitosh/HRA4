﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using log4net;
using System.Web.Mvc;
using System.Web;
using System.Drawing;
using RiskApps3.Utilities;
namespace HRA4.Utilities
{
    public class Helpers
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Helpers));
        public static void CreateInstitutionDb(string conn, string dbscript)
        {            
            Logger.DebugFormat("CreateInstitutionDb: Start");
            string[] result = ParseSqlStatementBatch(dbscript);

            try
            {
                SqlConnection myConn = new SqlConnection(conn);

                foreach (string commandText in result)
                {
                   
                    try
                    {
                        if (myConn == null)
                            myConn = new SqlConnection(conn);
                        if (myConn.State == ConnectionState.Closed)
                        {
                            myConn.Open();
                        }
                        if (!string.IsNullOrWhiteSpace(commandText))
                        {
                            var cmd = new SqlCommand(commandText, myConn);
                            // Logger.DebugFormat("Sql statement:{0}", commandText);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    }

                myConn.Close();
                myConn.Dispose();
                Logger.DebugFormat("CreateInstitutionDb: End");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static string ShowPedigreeImage(RiskApps3.Model.PatientRecord.Patient proband)
        {
            int Width = 625;
            int Height = 625;
            PedigreeGenerator pg = new PedigreeGenerator(Width, Height, proband);
            Bitmap bmp;
            if (proband != null)
            {
                bmp = pg.GeneratePedigreeImage(proband);
            }
            else
            {
                bmp = pg.GeneratePedigreeImage();
            }
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            return base64Data;
        }

        private static string[] ParseSqlStatementBatch(string sqlStatementBatch)
        {
            // split the sql into seperate batches by dividing on the GO statement
            Regex sqlStatementBatchSplitter = new Regex(@"^\s*GO\s*\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return sqlStatementBatchSplitter.Split(sqlStatementBatch);
        }

        public static string GenerateDbName(string instName)
        {
            string tmp = instName.Split(' ')[0];// takes first string from name
            string tmp1 = Guid.NewGuid().ToString().Split('-')[0];// takes first string from GUID
            string tenantDbName = string.Format("{0}_{1}_{2}", "HRA", tmp.ToUpper(), tmp1);
            return tenantDbName;
        }
       
        public static string GetInstitutionConfiguration(string configTemplate, string tenantDbName)
        {
            string instConnectionString = ConfigurationSettings.InstitutionDbConnection;
            instConnectionString = instConnectionString.Replace("[DBNAME]", tenantDbName);

            string instConfiguration = configTemplate;
            instConfiguration = instConfiguration.Replace("[DBCONNECTION]", instConnectionString);
            instConfiguration = instConfiguration.Replace("[PWD]", ConfigurationSettings.InstitutionPassword);
            return instConfiguration;
        }





        /*
        protected virtual string RenderPartialView(string partialViewName, object model)
        {
            if ( ControllerContext == null)
                return string.Empty;

            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrEmpty(partialViewName))
                throw new ArgumentNullException("partialViewName");

            ModelState.Clear();//Remove possible model binding error.

            ViewData.Model = model;//Set the model to the partial view

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, partialViewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        */

    }
}

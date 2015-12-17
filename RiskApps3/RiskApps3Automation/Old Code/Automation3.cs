using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model.Clinic;

namespace RiskApps3Automation
{
    public class AutomationStatus
    {
        //public string status = "";
        //public string html = "";
        //public string printer = "";
    }

    public class Automation3
    {
        //private System.ComponentModel.BackgroundWorker worker = null;
        //private bool loggedNoAppointmentsMarked = false;

        public static String AUTOMATION_SQL =
        "SELECT tblRiskData.apptID, patientName, unitnum, DOB, CAST(apptdate + ' ' + appttime as DATETIME) as apptdatetime, riskdatacompleted FROM tblRiskData INNER JOIN tblAppointments ON tblRiskData.apptID = tblAppointments.apptid  WHERE (tblRiskData.printed=0) AND (tblAppointments.riskdatacompleted Is Not Null)";

        public Automation3()
        {           
        }
        public Automation3(System.ComponentModel.BackgroundWorker bw)
        {
            //worker = bw;
        }

        //System.Threading.Mutex appMutex;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // !!!!!!!!  This code has been replaced and is not used.  see Automation3Form code !!!!!!!
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void automate()
        {
            //try
            //{
            //    String CustomAutomationString = Configurator.getNodeValue("DatabaseInfo", "AUTOMATION_SQL");

            //    if (string.IsNullOrEmpty(CustomAutomationString) == false)
            //    {
            //        AUTOMATION_SQL = CustomAutomationString;
            //    }
            //    List<long> apptIDs = new List<long>();
            //    try
            //    {

            //        int count = 0;
            //        using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(AUTOMATION_SQL))
            //        {
            //            long apptID = 0;

            //            while (reader.Read())
            //            {
            //                count++;
            //                apptID = Int32.Parse(reader.GetValue(0).ToString());
            //                apptIDs.Add(apptID);
            //            }
            //        }
            //        if (count > 0)
            //        {
            //            updateStatus("Starting an automation cycle.",null,null);

            //            outputToLog("=========================================");
            //            outputToLog("Starting an automation cycle.  Number of patients to process: " + count);
            //            outputToLog("=========================================");

            //            loggedNoAppointmentsMarked = false;
            //        }
            //        else
            //        {
            //            updateStatus("No appointments are marked for automation.",null,null);

            //            if (loggedNoAppointmentsMarked == false)
            //            {
            //                outputToLog("-------------------------------------------------");
            //                outputToLog("No appointments are marked for automation.");
            //                outputToLog("-------------------------------------------------");
            //                loggedNoAppointmentsMarked = true;
            //            }
            //            return;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        logAutomation3Exception("Error getting appointment IDs: ", e);
            //    }
            //    for (int i = 0; i < apptIDs.Count; i++)
            //    {
            //        long apptid = apptIDs[i];

            //        updateStatus("Running automation for appointment " + apptid.ToString() + ".",null,null);

            //        try
            //        {
            //            if (worker != null && worker.CancellationPending)
            //            {
            //                return;
            //            }
            //            string unitnum = "";
            //            ParameterCollection pc = new ParameterCollection("apptID", apptid);
            //            using (SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_GetUnitnum", pc))   //ExecuteReader("SELECT unitnum FROM tblAppointments WHERE apptID = " + apptid))
            //            {
            //                if (reader != null && reader.Read())
            //                {
            //                    unitnum = reader.GetValue(0).ToString();
            //                }
            //            }
            //            if (string.IsNullOrEmpty(unitnum))
            //            {
            //                continue; // ????
            //            }
            //            SessionManager.Instance.SetActivePatientNoCallback(unitnum, (int)apptid);

            //            //--------------------------------------------------------
            //            // Run risk calcs 
            //            //--------------------------------------------------------

            //            // TBD ???
            //            // Limit RiskAutomation3 mutex to risk calcs,
            //            // not the entire automate().
            //            // Probably a few other short spans.
            //            // 
            //            updateStatus("Running risk calculations for appointment " + apptid.ToString() + ".", null, null);

            //            try
            //            {
            //                // No need to get / set the config file here????
            //                //
            //                // TBD:
            //                // Should a class in the chain that calls RiskApps3Automation.Automation3.automate() 
            //                // get / set the config file??? Should the SessionManager???
            //                //
            //                SessionManager.Instance.GetActivePatient().RunModels();
            //            }
            //            catch (Exception ee)
            //            {
            //                logAutomation3Exception("Unable to run risk calcs for apptID=" + apptid.ToString() + ".", ee);
            //                return;
            //            }

            //            //--------------------------------------------------------
            //            // print documents (autoPrint=true)
            //            //--------------------------------------------------------
            //            updateStatus("Printing documents for appointment " + apptid.ToString() + ".",null,null);

            //            try
            //            {
            //                printAutomationDocuments(apptid, unitnum);
            //            }
            //            catch (Exception e)
            //            {
            //                logAutomation3Exception(
            //                    "Automation - unable to connect to print automation docs for appt id=" +
            //                    apptIDs[i].ToString() + ".", e);
            //            }

            //            //--------------------------------------------------------
            //            // save documents (autoSave=true)
            //            //--------------------------------------------------------
            //            updateStatus("Saving documents for appointment " + apptid.ToString() + ".",null,null);

            //            try
            //            {
            //                saveAutomationDocuments(unitnum, apptid);
            //            }
            //            catch (Exception e)
            //            {
            //                logAutomation3Exception("Automation - unable to connect to save automation docs for appt id="
            //                    + apptid.ToString(), e);
            //            }

            //            // TBD - run automation stored procedures - currently a noop.
            //            //
            //            //updateStatus("Running automation stored procedures for appointment" + apptid.toString() + ".");
            //            runAutomationStoredProcedures();

            //            //clear automationPrint docs from printQ                
            //            updateStatus("Clearing print queue of automation documents for appointment " + apptid.ToString() + ".", null, null);
            //            clearPrintQueue();

            //            // Now that we have printed (or not), set the 'printed' flag so it doesn't get automated again.
            //            //
            //            try
            //            {
            //                String sqlStr = "UPDATE tblRiskData SET printed = 1 WHERE apptid = " + apptid.ToString() + ";";
            //                BCDB2.Instance.ExecuteNonQuery(sqlStr);
            //            }
            //            catch (Exception e)
            //            {
            //                logAutomation3Exception("Automation - unable to update appt id=" + apptid.ToString() +
            //                                            " printed=1.\n", e);
            //            }

            //        }
            //        catch (Exception ev)
            //        {
            //            logAutomation3Exception("Automate() apptID = " + apptid.ToString() + " error: ", ev);
            //        }
            //        updateStatus("Completed automation for appointment " + apptid.ToString() + ".", null, null);
            //        outputToLog("Completed automation for appointment " + apptid.ToString() + ".");
            //    }
            //    outputToLog("=========================================");
            //    outputToLog("Completed an automation cycle.");
            //    outputToLog("=========================================");
            //}
            //catch (Exception ex)
            //{
            //    logAutomation3Exception("FAILED TO COMPLETE AN AUTOMATION CYLE.", ex);
            //}
        }
        //public void outputToLog(string str)
        //{
        //    Logger.Instance.WriteToLog(str); 
        //}

        //private void saveAutomationDocuments(string unitnum, long apptID)
        //{
        //    String sqlStr =
        //        "SELECT documentName, documentFileName,documentTemplateID,conditionSQL, saveLocation FROM lkpDocumentTemplates WHERE autoSave<>0;";

        //    List<String> documentNames = new List<String>();
        //    List<String> documentFileNames = new List<String>();
        //    List<int> documentTemplateIDs = new List<int>();
        //    List<String> conditionSQLs = new List<String>();
        //    List<String> saveLocations = new List<String>();

        //    using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
        //    {
        //        while (reader.Read())
        //        {
        //            documentNames.Add(reader.GetValue(0).ToString());
        //            documentFileNames.Add(reader.GetValue(1).ToString());
        //            documentTemplateIDs.Add(Int32.Parse(reader.GetValue(2).ToString()));
        //            conditionSQLs.Add(reader.GetValue(3).ToString());
        //            saveLocations.Add(reader.GetValue(4).ToString());
        //        }
        //    }

        //    // override saveLocation IF the appointment provider has their own save location in lkpProviders!  jdg 9/12/12
        //    sqlStr = "select lkpProviders.documentStoragePath from tblappointments INNER JOIN lkpProviders ON tblappointments.apptphysname=lkpproviders.displayName WHERE apptID=" + apptID.ToString();
        //   string docStoragePath = "";

        //    using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
        //    {
        //        while (reader.Read())
        //        {
        //            docStoragePath = reader.GetValue(0).ToString();
        //        }
        //    }

        //    if (!String.IsNullOrEmpty(docStoragePath))
        //    {
        //        for (int i = 0; i < saveLocations.Count; i++)
        //        {
        //            saveLocations[i] = docStoragePath;
        //        }
        //    }
        //    // end override saveLocation

        //    // Conditionally save the documents.
        //    //
        //    for (int i = 0; i < documentNames.Count; i++)
        //    {
        //        bool saveDoc = evaluateSQLCondition(conditionSQLs[i]);
        //        if (saveDoc)
        //        {
        //            HtmlDocument hd = new HtmlDocument(documentTemplateIDs[i], unitnum, (int)apptID);
        //            hd.Save();
        //        }
        //    }
        //}
 
        //private void printAutomationDocuments(long apptid, string unitnum)
        //{
        //    String sqlStr =
        //        "SELECT documentName, documentFileName,documentTemplateID,conditionSQL FROM lkpDocumentTemplates WHERE autoPrint<> 0 AND htmlPath IS NOT NULL AND htmlPath <> '';";

        //    DbDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr);

        //    List<String> documentNames = new List<String>();
        //    List<String> documentFileNames = new List<String>();
        //    List<int> documentTemplateIDs = new List<int>();
        //    List<String> conditionSQLs = new List<String>();

        //    while (reader.Read())
        //    {
        //        documentNames.Add(reader.GetValue(0).ToString());
        //        documentFileNames.Add(reader.GetValue(1).ToString());
        //        documentTemplateIDs.Add(Int32.Parse(reader.GetValue(2).ToString()));
        //        conditionSQLs.Add(reader.GetValue(3).ToString());
        //    }
        //    reader.Close();

        //    for (int i = 0; i < documentNames.Count; i++)
        //    {
        //        DocumentTemplate dt = new DocumentTemplate(); 
        //        dt.documentTemplateID = documentTemplateIDs[i];

        //        bool printDoc = evaluateSQLCondition(getConditionSQL(documentTemplateIDs[i]));
        //        if (printDoc)
        //        {
        //            String printer = getPrinter(documentTemplateIDs[i]);
        //            HtmlDocument hdoc = new HtmlDocument(documentTemplateIDs[i], unitnum, (int)apptid);

        //            if (printer.ToUpper() != "NO_PRINT")
        //            {
        //                updateStatus(null, hdoc.template.htmlText, printer);
        //                // If no printer is specified, print to default printer.
        //                // 
        //                //if (string.IsNullOrEmpty(printer))
        //                //{
                            
        //                //    hdoc.print(""); 
        //                //}
        //                //else 
        //                //{
        //                //    // If the specified printer is invalid, HtmlDocument.Print() will use the default printer.
        //                //    //
        //                //    hdoc.print(printer); 
        //                //}

        //                //add a record to tblDocuments
        //                //
        //                sqlStr =
        //                    "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(" +
        //                    apptid.ToString() + "," + documentTemplateIDs[i] + "," + "'" + DateTime.Now +
        //                    "','AUTOMATION');";

        //                BCDB2.Instance.ExecuteNonQuery(sqlStr);
        //            }
        //        }
        //    }
        //}

        //private void runAutomationStoredProcedures()
        //{
        //    String sqlStr =
        //        "SELECT storedProcedure FROM lkpStoredProcedure WHERE routine LIKE '%automation%' ORDER BY _ord;";

        //    // TBD !!!!  Transform the RiskAppsCore.Automation code to RiskApps3 code.
        //    //
        //    //if (DBUtils.hasResult(sqlStr) == false)
        //    //{
        //    //    return;
        //    //}

        //    //updateStatus("Running stored procedures for appointment " + Globals.getApptID());
        //    //DbDataReader reader = DBUtils.ExecuteReader(sqlStr);

        //    //List<String> storedProcedures = new List<String>();
        //    //while (reader.Read())
        //    //{
        //    //    storedProcedures.Add(reader.GetValue(0).ToString());
        //    //}
        //    //reader.Close();

        //    //for (int i = 0; i < storedProcedures.Count; i++)
        //    //{
        //    //    String storedProcedure = DBUtils.replaceApptIDTags(storedProcedures[i]);
        //    //    Console.WriteLine("storedProcedure=" + storedProcedure);
        //    //    DBUtils.ExecuteNonQuery(storedProcedure);
        //    //}
        //}
        //private void clearPrintQueue()
        //{
        //    try
        //    {
        //        int apptID = SessionManager.Instance.GetActivePatient().apptid;
        //        string unitnum = SessionManager.Instance.GetActivePatient().unitnum;

        //        String sqlStr = "SELECT tblPrintQueue.documentTemplateID,tblPrintQueue.ID,documentName,documentFileName ";
        //        sqlStr = sqlStr +
        //                 " FROM tblPrintQueue INNER JOIN lkpDocumentTemplates ON tblPrintQueue.documentTemplateID=lkpDocumentTemplates.documentTemplateID ";
        //        sqlStr = sqlStr + " WHERE (tblPrintQueue.printed IS NULL) AND COALESCE(automationPrint,0)=1 ";
        //        sqlStr = sqlStr + " AND apptID=" + apptID;

        //        List<int> documentTemplateIDs = new List<int>();
        //        List<long> IDs = new List<long>();
        //        List<String> documentNames = new List<String>();
        //        List<String> documentFileNames = new List<String>();

        //        using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
        //        {
        //            while (reader.Read())
        //            {
        //                int documentTemplateID = -1;
        //                long ID = -1;
        //                String documentName = "";
        //                String documentFileName = "";

        //                if (!reader.IsDBNull(0))
        //                    documentTemplateID = Int32.Parse(reader.GetValue(0).ToString());

        //                if (!reader.IsDBNull(1))
        //                    ID = Int64.Parse(reader.GetValue(1).ToString());

        //                if (!reader.IsDBNull(2))
        //                    documentName = reader.GetValue(2).ToString();

        //                if (!reader.IsDBNull(3))
        //                    documentFileName = reader.GetValue(3).ToString();

        //                if (documentTemplateID > 0)
        //                {
        //                    documentTemplateIDs.Add(documentTemplateID);
        //                    IDs.Add(ID);
        //                    documentNames.Add(documentName);
        //                    documentFileNames.Add(documentFileName);
        //                }
        //            }
        //        }
        //        List<String> printerList = new List<String>();
        //        foreach (String strPrinter in PrinterSettings.InstalledPrinters)
        //        {
        //            printerList.Add(strPrinter);
        //        }
        //        Logger.Instance.WriteToLog("******** PRINTER LIST ********");
        //        for (int i = 0; i < printerList.Count; i++)
        //        {
        //            Logger.Instance.WriteToLog(printerList[i]);
        //        }
        //        Logger.Instance.WriteToLog("***************************");
        //        for (int i = 0; i < documentTemplateIDs.Count; i++)
        //        {
        //            int documentTemplateID = documentTemplateIDs[i];
                    
        //            String printer = getPrinter(documentTemplateIDs[i]);
        //            HtmlDocument hdoc = new HtmlDocument(documentTemplateIDs[i], unitnum, (int)apptID);

        //            if (printer.ToUpper() != "NO_PRINT")
        //            {
        //                updateStatus(null, hdoc.template.htmlText, printer);
        //                // If no printer is specified, print to default printer.
        //                //
        //                //if (printer == "") //  || printerList.Contains(printer) == false)
        //                //{
        //                //    hdoc.print("");
        //                //}
        //                //else 
        //                //{
        //                //    // If the specified printer is invalid, HtmlDocument.Print() will use the default printer.
        //                //    //
        //                //    hdoc.print(printer); 
        //                //}
        //            }

        //            sqlStr =
        //                "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(" +
        //               apptID + "," + documentTemplateID + "," + "'" + DateTime.Now +
        //                "','" + "BatchPrint" + "');";
        //            BCDB2.Instance.ExecuteNonQuery(sqlStr);

        //            sqlStr = "UPDATE tblPrintQueue SET printed='" + DateTime.Now + "' WHERE ID = " + IDs[i] + ";";
        //            BCDB2.Instance.ExecuteNonQuery(sqlStr);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logAutomation3Exception("clearPrintQueue", e); // ????
        //    }
        //}

        //private String getPrinter(int documentTemplateID)
        //{
        //    String sqlStr = "SELECT printer, condition FROM lkpDocumentTemplatePrinter WHERE documentTemplateID=" +
        //                    documentTemplateID + ";";

        //    List<String> printers = new List<String>();
        //    List<String> conditions = new List<String>();

        //    try
        //    {
        //        using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
        //        {
        //            while (reader.Read())
        //            {
        //                printers.Add(reader.GetValue(0).ToString());
        //                conditions.Add(reader.GetValue(1).ToString());
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logAutomation3Exception("Error getting printer for documentTemplateID " + documentTemplateID.ToString() + ": ", e);
        //        return "";
        //    }
        //    for (int i = 0; i < printers.Count; i++)
        //    {
        //        bool printToThisPrinter = evaluateSQLCondition(conditions[i]);
        //        if (printToThisPrinter)
        //        {
        //            return printers[i];
        //        }
        //    }
        //    return "";
        //}
        //private void updateStatus(string message, string html, string printer)
        //{
        //    if (worker != null)
        //    {
        //        AutomationStatus asu = new AutomationStatus();
        //        asu.status = message;
        //        asu.html = html;
        //        asu.printer = printer;
        //        worker.ReportProgress(0, message);
        //    }
        //    else
        //        Logger.Instance.WriteToLog("RiskApps3Automation status: " + message);
        //}        
        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //public string getConditionSQL(int documentTemplateID)
        //{
        //    String conditionSQL = "";  // ???
        //    try
        //    {
        //        String sqlStr =
        //            "SELECT conditionSQL FROM lkpDocumentTemplates WHERE documentTemplateID=" +
        //            documentTemplateID +
        //            ";";
        //        using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
        //        {
        //            if (reader.Read())
        //            {
        //                conditionSQL = reader.GetValue(0).ToString();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        logAutomation3Exception("", e); // ???
        //    }
        //    return conditionSQL;            
        //}

        //public bool evaluateSQLCondition(String conditionSQL)
        //{
        //    if (conditionSQL.ToUpper() == "FALSE")
        //    {
        //        return false;
        //    }

        //    if (conditionSQL.ToUpper() == "TRUE" || conditionSQL.Length == 0)
        //    {
        //        return true;
        //    }

        //    conditionSQL = replaceApptIDTags(conditionSQL);

        //    bool hasResult = false;
        //    bool hasBooleanResult = false;
        //    bool booleanResult = true;

        //    try
        //    {
        //        using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(conditionSQL))
        //        {

        //            if (reader.Read())
        //            {
        //                hasResult = true;
        //                String returnValue = reader.GetValue(0).ToString();
        //                if (returnValue.ToUpper() == "FALSE" || returnValue.ToUpper() == "NO")
        //                {
        //                    hasBooleanResult = true;
        //                    booleanResult = false;
        //                }

        //                if (returnValue.ToUpper() == "TRUE" || returnValue.ToUpper() == "YES")
        //                {
        //                    hasBooleanResult = true;
        //                    booleanResult = true;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //         logAutomation3Exception("Could not execute SQL query: '" + conditionSQL + "'", e);
        //    }
        //    if (hasBooleanResult)
        //    {
        //        return booleanResult;
        //    }
        //    return hasResult;
        //}

        //public String replaceApptIDTags(String sqlStr)
        //{
        //    sqlStr = Regex.Replace(sqlStr, "#[Aa][Pp][Pp][Tt][Ii][Dd]#",
        //                           "" + SessionManager.Instance.GetActivePatient().apptid.ToString());

        //    sqlStr = Regex.Replace(sqlStr, "#[Uu][Nn][Ii][Tt][Nn][Uu][Mm]#",
        //                           "" + SessionManager.Instance.GetActivePatient().unitnum);

        //    sqlStr = Regex.Replace(sqlStr, "#[Uu][Ss][Ee][Rr][Ll][Oo][Gg][Ii][Nn]#",
        //                           "" + "Automation");  // ???

        //    return sqlStr;
        //}
        ~Automation3()
        {
            //if (appMutex != null)
            //    appMutex.Close();
        }
        // TBD: Create a static utilities class with a logException() method. Put one in RiskApps3.Utilities.Logger???
        //
        //private void logAutomation3Exception(String message, Exception e)
        //{
        //    // get call stack
        //    StackTrace stackTrace = new StackTrace();

        //    // get calling method name
        //    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
        //    Logger.Instance.WriteToLog("[RiskAppsAutomation3] from [" + callingRoutine + "] " + message + "'\n\t" + e);
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}

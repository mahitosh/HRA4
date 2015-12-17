using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using System.Drawing.Printing;
using RiskApps3.Model.Clinic.Letters;

namespace RiskApps3Automation
{
    public partial class Automation : Form
    {
        
        private AutomationHeartbeat.Service1 status;
        private string defaultPrinter = "";
        private string idString = "Automation";
        private int automation_interval;

        /// <summary>
        /// The default constructor initializes and kicks of worker thread
        /// </summary>
        public Automation()
        {
            InitializeComponent();

            try
            {
                //status = new RiskApps3Automation.org.partners.dipr.hra.Service1();
                string url = Configurator.getAutomationHeartbeatServiceURL();
                if (url.Length == 0)
                {
                    status = null;
                }
                else
                {
                    status = new AutomationHeartbeat.Service1(url);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }

            System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
            defaultPrinter = settings.PrinterName;

            GetDbName();

            string sAutoInt = Configurator.getNodeValue("Globals", "AutomationInterval");
            int.TryParse(sAutoInt, out automation_interval);
            if (!(automation_interval > 0))
                automation_interval = 1000;

            timer1.Interval = 60000;
            
            button1.Text = "Cancel";
            label1.Text = "Running";
            backgroundWorker1.RunWorkerAsync();
        }

        /// <summary>
        /// form closing cancels thread and exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automation_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //if we are not waiting for a cancel, keep rolling
            while (backgroundWorker1.CancellationPending == false)
            {
                //grab the app mutex and run automation cycle
                System.Threading.Mutex appMutex = new System.Threading.Mutex(false, "RiskAppsAutomation");
                
                //don't wait for ever
                if (appMutex.WaitOne(automation_interval, false))
                {

                    //run automation
                    RunAutomationCycle(); 

                   // give back the mutex
                    appMutex.ReleaseMutex();                 
                }
                
                //this is a tight loop, so don't be a hog
                Thread.Sleep(automation_interval);
            }

        }

        /// <summary>
        /// This is the core automation routine that checks for appointments and processes each.
        /// </summary>
        private void RunAutomationCycle()
        {

            List<int> apptIDs = new List<int>();

            /***********************   get appointments to automate   ********************/
            SqlDataReader reader;
            string CustomAutomationString = Configurator.getNodeValue("DatabaseInfo", "AUTOMATION_SQL");
            if (string.IsNullOrEmpty(CustomAutomationString) == false)
            {
                reader = BCDB2.Instance.ExecuteReader(CustomAutomationString);
            }
            else
            {
                reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_Automation", null);
            }
            try
            {
                while (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                        apptIDs.Add(reader.GetInt32(0));
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("Error getting appointment IDs: " + e.ToString());
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            backgroundWorker1.ReportProgress(0, apptIDs.Count.ToString() + " appointments are marked for automation - " + DateTime.Now.ToString());

            
            /***********************    automation cycle   ********************/
            foreach (int apptid in apptIDs)
            {
                if (backgroundWorker1.CancellationPending) 
                    break;

                /***********************  Get Unitnum & set active patient  ********************/
                string unitnum = "";
                ParameterCollection pc = new ParameterCollection("apptID", apptid);
                using (reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_GetUnitnum", pc))
                {
                    if (reader != null && reader.Read())
                    {
                        unitnum = reader.GetValue(0).ToString();
                    }
                }

                SessionManager.Instance.SetActivePatientNoCallback(unitnum, (int)apptid);

                /***********************  Risk Calculations   ********************/
                try
                {
                    backgroundWorker1.ReportProgress(1,
                                                    "Running risk calculations for appointment: " + apptid.ToString() +
                                                    ".");
                    SessionManager.Instance.GetActivePatient().RecalculateRisk();
                }
                catch (Exception e)
                {
                    backgroundWorker1.ReportProgress(1,
                                                    "Risk calculations FAILED for appointment: " + apptid.ToString() +
                                                    ". " + e.ToString());
                }

                /***********************  Update BigQueue   ********************/
                try
                {
                    backgroundWorker1.ReportProgress(1, "Updating BigQueue for appointment: " + apptid.ToString() + ".");
                    pc = new ParameterCollection("unitnum", unitnum);
                    BCDB2.Instance.RunSPWithParams("sp_3_populateBigQueue", pc);
                }
                catch (Exception e)
                {
                    backgroundWorker1.ReportProgress(1,
                                                    "Updating BigQueue FAILED for appointment: " + apptid.ToString() +
                                                    ". " + e.ToString());
                }

                /***********************  Process Queue Documents    ********************/
                backgroundWorker1.ReportProgress(1,
                                                "Processing Queue Documents for appointment: " + apptid.ToString() + ".");
                pc = new ParameterCollection("apptID", apptid);
                BCDB2.Instance.RunSPWithParams("sp_processQueueDocuments", pc);

                /***********************  Run Automation Stored Procedures    ********************/
                backgroundWorker1.ReportProgress(1,
                                              "Run Automation Stored Procedures  : " + apptid.ToString() + ".");

                RiskAppCore.ApptUtils.runAutomationStoredProcedures(apptid);

                /***********************  export HL7 files    ********************/
                backgroundWorker1.ReportProgress(1,
                                              "Export HL7 File: " + apptid.ToString() + ".");
                RiskModels.RiskService.exportHL7File(apptid);

                /***********************  Process Batch Print Queue Documents    ********************/
                backgroundWorker1.ReportProgress(1,
                                              "Processing batch print queue documents for appointment: " + apptid.ToString() + "."); // Make batch + printing a single progress report ???
                RiskAppCore.ApptUtils.saveAutomationDocumentsToPrintQueue(apptid);

                /***********************  Print Documents   ********************/
                string print_report = "";
                try
                {
                    backgroundWorker1.ReportProgress(1, "Printing documents for appointment: " + apptid.ToString() + ".");  // Make batch + printing a single progress report ???
                    ParameterCollection printDocArgs = new ParameterCollection();
                    printDocArgs.Add("apptid", apptid);
                    reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_AutomationHtmlDocsToPrint", printDocArgs);

                    while (reader.Read())
                    {
                        int templateID = -1;
                        string printer = "";
                        string documentType = "";
                        string saveLocation = "";

                        if (reader.IsDBNull(0) == false)
                        {
                            templateID = reader.GetInt32(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            printer = reader.GetString(1);
                        }
                        if (reader.IsDBNull(2) == false)
                        {
                            documentType = reader.GetString(2);
                        }
                        if (reader.IsDBNull(3) == false)
                        {
                            saveLocation = reader.GetString(3);
                        }

                        ///////////////////////////////////////////////////
                        // documentType defaults to HTML in V3 Automation //
                        ///////////////////////////////////////////////////
                        if (string.IsNullOrEmpty(documentType))
                        {
                            documentType = "HTML";
                        }
                        RiskApps3.Model.Clinic.Letters.PrintUtils printUtils = new RiskApps3.Model.Clinic.Letters.PrintUtils(printer);

                        if (printer.ToLower().StartsWith("save as"))
                        {
                            if (string.IsNullOrEmpty(saveLocation) == false)
                            {
                                if (documentType.ToUpper() == "HTML")
                                {
                                    if (string.Compare(printer, "save as pdf", true) == 0)
                                    {
                                        if (printUtils.savePdfDoc(apptid, unitnum, templateID, saveLocation) == true)
                                        {
                                            print_report += "Saved document template " + templateID.ToString() + " in " + saveLocation + "<br/>";
                                        }

                                    }
                                    else
                                    {
                                        if (printUtils.saveHtmlDoc(apptid, unitnum, templateID, saveLocation) == true)
                                        {
                                            print_report += "Saved document template " + templateID.ToString() + " in " + saveLocation + "<br/>";
                                        }
                                    }
                                }
                                else if (documentType.ToUpper() == "WORD")
                                {
                                    if (printUtils.saveWordDoc(apptid, unitnum, templateID, saveLocation) == true)
                                    {
                                        print_report += "Saved document template " + templateID.ToString() + " in " + saveLocation + "<br/>";
                                    }
                                }
                            }
                        }
                        else if (printer == "BATCH")
                        {
                            pc = new ParameterCollection();
                            pc.Add("apptid", apptid);
                            pc.Add("dateTime", DateTime.Now);
                            pc.Add("templateID", templateID);
                            pc.Add("unitnum", unitnum);
                            pc.Add("documentType", documentType);

                            string sqlStr =
                                "INSERT INTO tblPrintQueue (apptID, created, documentTemplateID, unitnum, automationPrint, automationSave, documentType) VALUES (@apptid, @dateTime, @templateID, @unitnum, 0, 0, @documentType)";
                            int rowsAffected = BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);

                            print_report += "Sent document template " + templateID.ToString() + " to the batch print queue." + "<br/>";
                            backgroundWorker1.ReportProgress(2, "Appt " + apptid.ToString() + ":<br/>" + print_report);
                        }
                        else if (printer == "")
                        {
                        }
                        else if (string.IsNullOrEmpty(printer) == false)
                        {
                            if (documentType.ToUpper() == "WORD")
                            {
                                if (printUtils.printWordDoc(apptid, unitnum, templateID))
                                {
                                    print_report += "Sent document template " + templateID.ToString() + " to printer: " + printUtils.getPrinter() + "<br/>";
                                    pc = new ParameterCollection();
                                    pc.Add("apptid", apptid);
                                    pc.Add("templateID", templateID);
                                    pc.Add("dateTime", DateTime.Now);

                                    string sqlStr = "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(@apptid, @templateID, @dateTime, 'AUTOMATION');";
                                    BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);
                                }
                                else
                                {
                                    print_report += "Failed sending document template " + templateID.ToString() + " to printer: " + printUtils.getPrinter() + "<br/>";
                                }

                                backgroundWorker1.ReportProgress(2, "Appt " + apptid.ToString() + ":<br/>" + print_report);
                            }
                            else if (documentType.ToUpper() == "HTML")
                            {
                                HraHtmlDocument hdoc;

                                if ((hdoc = printUtils.printHtmlDoc(apptid, unitnum, templateID)) != null)
                                {
                                    print_report += "Sent document template " + templateID.ToString() + " to printer: " + hdoc.targetPrinter + "<br/>";

                                    pc = new ParameterCollection();
                                    pc.Add("apptid", hdoc.apptid);
                                    pc.Add("templateID", hdoc.template.documentTemplateID);
                                    pc.Add("dateTime", DateTime.Now);

                                    string sqlStr = "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(@apptid, @templateID, @dateTime, 'AUTOMATION');";
                                    int rowsAffected = BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);

                                    backgroundWorker1.ReportProgress(2, hdoc.template.htmlText + Environment.NewLine + "Appt " + apptid.ToString() + ":<br/>" + print_report);
                                }
                                else
                                {
                                    print_report += "Failed to send document template " + templateID.ToString() + " to printer: " + printUtils.getPrinter() + "<br/>";
                                    backgroundWorker1.ReportProgress(2, "Appt " + apptid.ToString() + ":<br/>" + print_report);
                                }
                            }
                            else if (documentType != "HTML" && documentType != "WORD")
                            {
                                print_report += "Could not process document: templateID " + templateID.ToString() + ", printer: " + printer + ", UNKOWN DOCUMENT TYPE: \"" + documentType + "\"<br/>";
                                backgroundWorker1.ReportProgress(2, "Appt " + apptid.ToString() + ":<br/>" + print_report);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    backgroundWorker1.ReportProgress(1,
                                                    "Printing FAILED for appointment: " + apptid.ToString() + ". " +
                                                    e.ToString());
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }

                /***********************  Mark as printed to complete automation   ********************/
                try
                {
                    pc = new ParameterCollection("apptid", apptid);
                    String sqlStr = "UPDATE tblRiskData SET printed = 1 WHERE apptid = @apptid";
                    int rowsAffected = BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);
                }
                catch (Exception e)
                {
                    backgroundWorker1.ReportProgress(1,
                                                    ("Automation - unable to update appt id=" + apptid.ToString() +
                                                     " printed=1.\n" + e.ToString()));
                }

                if (status != null)
                {
                    try
                    {
                        status.LogStatus(idString,
                                         "Automation",
                                         "Ok",
                                         ("Appt " + apptid + " " + print_report).Trim());
                    }
                    catch (System.Web.Services.Protocols.SoapException e)
                    {
                        //do nothing
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(ex.ToString());
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void GetDbName()
        {
            try
            {
                string sqlStr = "SELECT DB_NAME() AS DataBaseName";
                using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
                {
                    if (reader.Read())
                    {
                        if (reader.IsDBNull(0)==false)
                            idString += (" " + reader.GetValue(0).ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("Could not get database name. " + e.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                label1.Text = (string)e.UserState;
            }
            else if (e.ProgressPercentage == 1)
            {
                label1.Text = (string)e.UserState;
                Logger.Instance.WriteToLog(label1.Text);
            }
            else if (e.ProgressPercentage == 2)
            {
                string html = (string)e.UserState;
                if (html != null)
                {
                    if (panel1.Controls.Count > 0)
                    {
                        if (panel1.Controls[0] is WebBrowser)
                        {
                            WebBrowser wb = (WebBrowser)panel1.Controls[0];
                            wb.DocumentText = html;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Text = "Run";
            label1.Text = "Ready";
            button1.Enabled = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            else
            {
                button1.Text = "Cancel";
                label1.Text = "Running";
                backgroundWorker1.RunWorkerAsync();
                button1.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (status != null)
            {
                try
                {
                    status.LogHeartbeat(idString);
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteToLog(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0)
            {
                WebBrowser wb = new WebBrowser();
                wb.Dock = DockStyle.Fill;
                panel1.Controls.Add(wb);
                
                button2.Enabled = false;
            }
            
        }


    }
}

/************************   BWD to remove later  ********************/

//try
//{
//    SessionManager.Instance.SetActivePatient(unitnum, apptid);

//    HraHtmlDocument hdoc = new HraHtmlDocument(templateID, unitnum, apptid);
//    if (hdoc == null)
//    {
//        Logger.Instance.WriteToLog("RiskApps3Automation.HtmlDocument(" + templateID.ToString() + ", " + apptid.ToString() + ", \"" + unitnum + "\") returned null");
//    }
//    hdoc.apptid = apptid;

//    System.IO.FileInfo fInfo = hdoc.template.CalculateFileName(SessionManager.Instance.GetActivePatient().name,
//                            SessionManager.Instance.GetActivePatient().apptdatetime.ToShortDateString().Replace("/", "-"),
//                            SessionManager.Instance.GetActivePatient().apptid,
//                            SessionManager.Instance.GetActivePatient().unitnum,
//                            "pdf", saveLocation);

//    string PdfFileName = fInfo.FullName;

//    EvoPdf.HtmlToPdfConverter htmlToPdfConverter = new EvoPdf.HtmlToPdfConverter();
//    htmlToPdfConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
//    htmlToPdfConverter.HtmlViewerWidth = 800;
//    htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;
//    htmlToPdfConverter.PdfDocumentOptions.AvoidTextBreak = true;

//    htmlToPdfConverter.ConvertHtmlToFile(hdoc.template.htmlText, "", PdfFileName);

//}
//catch (Exception e)
//{
//    Logger.Instance.WriteToLog("saveHtmlDoc(" + apptid.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ", \"" + saveLocation + "\") " + e.ToString());
//}






/************************   BWD to remove later  ********************/

//public RiskApps3Automation.HtmlDocument printHtmlDoc(int apptID, int documentTemplateID, string unitnum, string printer)
//{
//    SessionManager.Instance.SetActivePatient(unitnum, apptID);

//    RiskApps3Automation.HtmlDocument hdoc = new RiskApps3Automation.HtmlDocument(documentTemplateID, unitnum, apptID);
//    if (string.IsNullOrEmpty(printer) || printer == "*")
//    {
//        System.Drawing.Printing.PrinterSettings settings = new System.Drawing.Printing.PrinterSettings();
//        String defaultPrinter = settings.PrinterName;
//        hdoc.targetPrinter = defaultPrinter;
//    }
//    else
//    {
//        hdoc.targetPrinter = printer;
//    }
//    hdoc.apptid = apptID;
//    hdoc.Print();

//    return hdoc;
//}








        /*
        public static void SetDefaultPrinter(string printerDevice)
        {
            int ret = 0;
            string path = "win32_printer.DeviceId='" + printerDevice + "'";

            using (System.Management.ManagementObject printer = new System.Management.ManagementObject(path))
            {
                System.Management.ManagementBaseObject outParams = printer.InvokeMethod("SetDefaultPrinter", null, null);

                ret = (int)(uint)outParams.Properties["ReturnValue"].Value;
            }
            return;
        }
         */
                        //PrinterSettings current = new PrinterSettings();
                        //if (printer != current.PrinterName)
                        //{
                        //    SetDefaultPrinter(printer);
                        //}

                        //if (printer != defaultPrinter)
                        //{
                        //    SetDefaultPrinter(defaultPrinter);
                        //}
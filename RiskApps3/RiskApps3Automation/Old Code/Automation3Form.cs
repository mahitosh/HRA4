//#define Automation3FormDebug

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using RiskAppCore;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using System.Management;
using Microsoft.Win32;
using System.Drawing.Printing;
using System.Threading;
using RiskModels;
using Configurator = RiskApps3.Utilities.Configurator;
using Logger = RiskApps3.Utilities.Logger;
using ParameterCollection = RiskApps3.Utilities.ParameterCollection;

namespace RiskApps3Automation
{
    public partial class Automation3Form : Form
    {
        private AutomationHeartbeat.Service1 status; // = new AutomationHeartbeat.Service1();

        private string defaultPrinter = "";
        private bool htmlInProgress = false;

        public Automation3Form(bool show)
        {
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

            InitializeComponent();
            initFormText();
            string AutomationInterval = Configurator.getNodeValue("Globals", "AutomationInterval");
            int aint = 10000;
            if (int.TryParse(AutomationInterval, out aint))
            {
                timer1.Interval = aint;
            }
            else
            {
                timer1.Interval = 10000;
            }
            timer1.Enabled = true;
            

            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocLoadComplete);

            PrinterSettings settings = new PrinterSettings();
            defaultPrinter = settings.PrinterName;
        }

        private void DocLoadComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            webBrowser1.Print();
            SetDefaultPrinter(defaultPrinter);
            htmlInProgress = false;
        }

        private void Automation3Form_Load(object sender, EventArgs e)
        {
            automation3List1.refreshGrid();
        }

        private void initFormText()
        {
            try
            {
                string sqlStr = " SELECT DB_NAME() AS DataBaseName";
                string dbName = "";
                using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr))
                {
                    if (reader.Read())
                    {
                        dbName = reader.GetValue(0).ToString();
                    }
                }
                this.Text += " - " + dbName;
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("Could not get database name. " + e.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (status != null)
            {
                try
                {
                    status.LogHeartbeat(this.Text);
                }
                catch (Exception ex)
                {
                    //     Logger.Instance.WriteToLog(ex.ToString());
                }
            }

            if (ProcessingThread.IsBusy == false)
            {
                automation3List1.refreshGrid();
                ProcessingThread.RunWorkerAsync();
            }
        }


        private void ProcessingThread_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Mutex appMutex = new System.Threading.Mutex(false, "RiskAppsAutomation");
            try
            {
                if (appMutex.WaitOne(500, false))
                {
                    automate();
                }
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog("[RiskApps3Automation.Automation3Form].ProcessingThread_DoWork: " +
                                           ee.ToString());
            }
            appMutex.ReleaseMutex();
        }

        public void automate()
        {
            if (status != null)
            {
                try
                {
                    //status.LogHeartbeat(this.Text);
                }
                catch (Exception ex)
                {
                    //  Logger.Instance.WriteToLog(ex.ToString());
                }
            }

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

            if (apptIDs.Count == 0)
            {
                ProcessingThread.ReportProgress(0,
                                                "No appointments are marked for automation - " + DateTime.Now.ToString());
                return;
            }


            /***********************    automation cycle   ********************/
            foreach (int apptid in apptIDs)
            {
                if (ProcessingThread.CancellationPending)
                {
                    return;
                }

                ProcessingThread.ReportProgress(0, "Running automation for appt: " + apptid.ToString() + ".");

                /***********************  Get Unitnum & set active patient  ********************/
                string unitnum = "";
                ParameterCollection pc = new ParameterCollection("apptID", apptid);
                using (reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_GetUnitnum", pc))
                    //ExecuteReader("SELECT unitnum FROM tblAppointments WHERE apptID = " + apptid))
                {
                    if (reader != null && reader.Read())
                    {
                        unitnum = reader.GetValue(0).ToString();
                    }
                }

                //StatusReport sr = new StatusReport();
                //sr.apptid = apptid;
                //sr.description = "Started Automation cycle";
                //ProcessingThread.ReportProgress(300, sr);

                //StatusReport sr = (StatusReport)e.UserState;
                //if (status != null)
                //{
                try
                {
                    //temp debug
                    //status.LogStatus(this.Text,
                    //         "Automation",
                    //         "Ok",
                    //         "tarted Automation cycle " + apptid.ToString());
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteToLog(ex.ToString());
                }
                //}
                SessionManager.Instance.SetActivePatientNoCallback(unitnum, (int) apptid);

                /***********************  Risk Calculations   ********************/
                try
                {
                    ProcessingThread.ReportProgress(1,
                                                    "Running risk calculations for appointment: " + apptid.ToString() +
                                                    ".");
                    SessionManager.Instance.GetActivePatient().RecalculateRisk();
                }
                catch (Exception e)
                {
                    ProcessingThread.ReportProgress(1,
                                                    "Risk calculations FAILED for appointment: " + apptid.ToString() +
                                                    ". " + e.ToString());
                }

                /***********************  Update BigQueue   ********************/
                try
                {
                    ProcessingThread.ReportProgress(1, "Updating BigQueue for appointment: " + apptid.ToString() + ".");
                    pc = new ParameterCollection("unitnum", unitnum);
                    BCDB2.Instance.RunSPWithParams("sp_3_populateBigQueue", pc);
                }
                catch (Exception e)
                {
                    ProcessingThread.ReportProgress(1,
                                                    "Updating BigQueue FAILED for appointment: " + apptid.ToString() +
                                                    ". " + e.ToString());
                }

                /***********************  Process Queue Documents    ********************/
                ProcessingThread.ReportProgress(1,
                                                "Processing Queue Documents for appointment: " + apptid.ToString() + ".");
                pc = new ParameterCollection("apptID", apptid);
                BCDB2.Instance.RunSPWithParams("sp_processQueueDocuments", pc);



                /***********************  Process Automation Save Documents    ********************/
                ProcessingThread.ReportProgress(1,
                                              "Processing Auto Save Documents: " + apptid.ToString() + ".");
                ApptUtils.saveAutomationDocumentsToPrintQueue(apptid);


                /***********************  Run Automation Stored Procedures    ********************/
                ProcessingThread.ReportProgress(1,
                                              "Run Automation Stored Procedures  : " + apptid.ToString() + ".");
           
                ApptUtils.runAutomationStoredProcedures(apptid);

                /***********************  export HL7 files    ********************/
                ProcessingThread.ReportProgress(1,
                                              "Export HL7 File: " + apptid.ToString() + ".");
                RiskService.exportHL7File(apptid);

                /***********************  Print Documents   ********************/
                try
                {
                    ProcessingThread.ReportProgress(1, "Printing documents for appointment: " + apptid.ToString() + ".");
                    ParameterCollection printDocArgs = new ParameterCollection();
                    printDocArgs.Add("apptid", apptid);
                    reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_AutomationHtmlDocsToPrint", printDocArgs);

                    int templateID = -1;
                    string printer = "";

                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0) == false)
                        {
                            templateID = reader.GetInt32(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            printer = reader.GetString(1);
                        }

                        if (templateID > 0)
                        {
                            if (string.IsNullOrEmpty(printer) == false)
                            {
                                if (printer.ToUpper() != "NO_PRINT")
                                {
                                    ProcessingThread.ReportProgress(1,
                                                                    "Printing templateID " + templateID.ToString() +
                                                                    " for apptID " + apptid.ToString() + ".");
                                    HtmlDocument hdoc = new HtmlDocument(templateID, unitnum, apptid);
                                    hdoc.targetPrinter = printer;
                                    hdoc.apptid = apptid;
                                    htmlInProgress = true;


                                    //try
                                    //{
                                    //    ////temp debug
                                    //    //status.LogStatus(this.Text,
                                    //    //                 "Automation",
                                    //    //                 "Ok",
                                    //    //                 "Appt " + hdoc.apptid.ToString() + " Sent doc " + hdoc.template.documentTemplateID.ToString() + " to " + hdoc.targetPrinter);
                                    //}
                                    //catch (Exception ex) { Logger.Instance.WriteToLog(ex.ToString()); }

                                    if (status != null)
                                    {
                                        try
                                        {
                                            status.LogStatus(this.Text,
                                                             "Automation",
                                                             "Ok",
                                                             "Appt " + hdoc.apptid.ToString() + " Sent doc " +
                                                             hdoc.template.documentTemplateID.ToString() + " to " +
                                                             hdoc.targetPrinter);
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


                                    ProcessingThread.ReportProgress(100, hdoc);
                                    while (htmlInProgress)
                                    {
                                        Thread.Sleep(100);
                                    }

                                    string sqlStr =
                                        "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(" +
                                        hdoc.apptid.ToString() + "," + hdoc.template.documentTemplateID + "," + "'" +
                                        DateTime.Now +
                                        "','AUTOMATION');";

                                    BCDB2.Instance.ExecuteNonQuery(sqlStr);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    ProcessingThread.ReportProgress(1,
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
                    String sqlStr = "UPDATE tblRiskData SET printed = 1 WHERE apptid = " + apptid.ToString() + ";";
                    BCDB2.Instance.ExecuteNonQuery(sqlStr);
                }
                catch (Exception e)
                {
                    ProcessingThread.ReportProgress(1,
                                                    ("Automation - unable to update appt id=" + apptid.ToString() +
                                                     " printed=1.\n" + e.ToString()));
                }

                ProcessingThread.ReportProgress(200, "");
            }
        }


        private void ProcessingThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                statusLabel.Text = (string) e.UserState;
            }
            else if (e.ProgressPercentage == 1)
            {
                statusLabel.Text = (string) e.UserState;
                Logger.Instance.WriteToLog(statusLabel.Text);
            }
            else if (e.ProgressPercentage == 100)
            {
                HtmlDocument hdoc = (HtmlDocument) e.UserState;
                if (hdoc.targetPrinter != "*")
                    SetDefaultPrinter(hdoc.targetPrinter);

                webBrowser1.DocumentText = hdoc.template.htmlText;
            }
            else if (e.ProgressPercentage == 200)
            {
                automation3List1.refreshGrid();
            }
            //else if (e.ProgressPercentage == 400)
            //{


            //}
            //else if (e.ProgressPercentage == 300)
            //{

            //}            
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (ProcessingThread.IsBusy)
                ProcessingThread.CancelAsync();

            Close();
        }

        public static void SetDefaultPrinter(string printerDevice)
        {
            int ret = 0;
            string path = "win32_printer.DeviceId='" + printerDevice + "'";

            using (ManagementObject printer = new ManagementObject(path))
            {
                ManagementBaseObject outParams = printer.InvokeMethod("SetDefaultPrinter", null, null);

                ret = (int) (uint) outParams.Properties["ReturnValue"].Value;
            }
            return;

            //using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer"))
            //{
            //    using (ManagementObjectCollection objectCollection = objectSearcher.Get())
            //    {
            //        foreach (ManagementObject mo in objectCollection)
            //        {
            //            if (string.Compare(mo["Name"].ToString(), defaultPrinter, true) == 0)
            //            {
            //                mo.InvokeMethod("SetDefaultPrinter", null, null);
            //                return;
            //            }
            //        }
            //    }
            //}
        }
    }

    public class StatusReport
    {
        public int apptid = -1;
        public string status = "OK";
        public string description = "";
    }
}

//private void logAutomation3Exception(String message, Exception e)
//{
//    // get call stack
//    StackTrace stackTrace = new StackTrace();

//    // get calling method name
//    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
//    Logger.Instance.WriteToLog("[RiskAppsAutomation3] from [" + callingRoutine + "] " + message + "'\n\t" + e);
//}


//string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
//string old_footer = "";
//string old_header = "";
//RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true);
//if (key != null)
//{
//    old_footer = (string)key.GetValue("footer");
//    old_header = (string)key.GetValue("header");
//}

//key.SetValue("header", "Hello World!");

//if (string.IsNullOrEmpty(old_footer) == false)
//    key.SetValue("footer", old_footer);

//if (string.IsNullOrEmpty(old_header) == false)
//    key.SetValue("header", old_header);


// Dispose the WebBrowser now that the task is complete. 
//((WebBrowser)sender).Dispose();


//automation3.automate();

//HtmlDocument hd;
//static Automation3 automation3; 
// bool showForm;
//if (automation3 == null)
//    automation3 = new Automation3(ProcessingThread);
//showForm = show;


//#if Automation3FormDebug
//            Logger.Instance.WriteToLog("TICK");
//#endif
//            if (ProcessingThread.IsBusy == false)
//            {
//#if Automation3FormDebug
//                Logger.Instance.WriteToLog("ProcessingThread.IsBusy == false");
//#endif
//                ProcessingThread.RunWorkerAsync();
//                while (ProcessingThread.IsBusy)
//                {
//                    // Keep UI messages moving, so the form remains responsive during the asynchronous operation.
//                    // 
//                    Application.DoEvents();
//                }

//            }
//#if Automation3FormDebug
//            else
//            {
//                Logger.Instance.WriteToLog("ProcessingThread.IsBusy == true");
//            }
//            Logger.Instance.WriteToLog("TOCK");
//#endif
//        }

//private void Automation3Form_Resize(object sender, EventArgs e)
//{
//}
//private void Automation3Form_Shown(object sender, EventArgs e)
//{
//    //if (showForm)
//    //{
//    //    Show();
//    //    WindowState = FormWindowState.Normal;
//    //}
//    //else
//    //{
//    //    this.Hide();
//    //}
//}

//            AutomationStatus asu = (AutomationStatus)e.UserState;
//            if (asu != null)
//            {

//                if (string.IsNullOrEmpty(asu.status) == false)
//                    statusLabel.Text = asu.status;

//                if (string.IsNullOrEmpty(asu.html) == false)
//                {
//                    webBrowser1.DocumentText = asu.html;

//                    if (string.IsNullOrEmpty(asu.html) == false)
//                    {

//                    }
//                }

//            }


//#if Automation3FormDebug
//            Logger.Instance.WriteToLog(message);
//#endif
//            automation3List1.refreshGrid();
//        }
//        ~Automation3Form()
//        {
//            if (ProcessingThread != null)
//            {
//                // If held, RiskAppsAutomation mutex will be released by system,
//                // when the ProcessingThread exits.
//                //
//                if (ProcessingThread.IsBusy)
//                {
//                    ProcessingThread.CancelAsync();
//                }
//            }
//        }   


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//public int SetAsDefaultPrinter(string printerDevice)
//{
//    int ret = 0;
//    string path = "win32_printer.DeviceId='" + printerDevice + "'";

//    using (ManagementObject printer = new ManagementObject(path))
//    {
//        ManagementBaseObject outParams = printer.InvokeMethod("SetDefaultPrinter", null, null);

//        ret = (int)(uint)outParams.Properties["ReturnValue"].Value;
//    }
//    return ret;
//}
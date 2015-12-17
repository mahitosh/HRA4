using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApp.Forms;
using RiskAppCore;
using RiskApps3.Model;
using RiskApps3.Controllers;
using RiskApps3.View;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.PMH;
using RiskApps3.View.PatientRecord.FamilyHistory;
using RiskApps3.View.PatientRecord.Medications;
using RiskApps3.View.PatientRecord.Pedigree;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using RiskApps3.View.RiskClinic;
using RiskApps3.Model.Clinic;
using RiskApps3.View.Admin;
using System.Diagnostics;
using RiskApp;
using RiskAppUtils;
using RiskApps3.Utilities;
using System.Threading;
using RiskApps3.View.Common;
using RiskApps3.View.BreastImaging;
using RiskApps3.View.Reporting;

namespace RiskApps3
{
    public partial class MainForm : Form
    {
        /**************************************************************************************************/
        public delegate void PushViewCallbackType(HraView view, WeifenLuo.WinFormsUI.Docking.DockState dockstate);
        public PushViewCallbackType PushViewDelegate;

        public bool failedLogin = false;

        RiskClinicDashboard rcd;
        BreastImagingDashboard bid;
        MyScheduleView mpv;
        public bool AggregatorIsAvailable;

        Stack<HraView> viewStack = new Stack<HraView>();

        /**************************************************************************************************/
        // the default constructor for the main form
        public MainForm()
        {
            InitializeComponent();
            
        }

        /**************************************************************************************************/
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            //if (e.CloseReason == CloseReason.WindowsShutDown) return;

            //// Confirm user wants to close
            //switch (MessageBox.Show(this, "Are you sure you want to close RiskApps?", "Closing RiskApps", MessageBoxButtons.YesNo))
            //{
            //    case DialogResult.No:
            //        e.Cancel = true;
            //        break;
            //    default:
            //        break;
            //}
        }

        /**************************************************************************************************/
        public void PushViewOnMainWindow(HraView view, WeifenLuo.WinFormsUI.Docking.DockState dockstate)
        {
            PushView(view);
        }

        /**************************************************************************************************/
        public void PushView(HraView view)
        {
            string window_name = "";

            if (viewStack.Count > 0)
            {
                viewStack.Peek().DockState = DockState.Hidden;
                window_name = " to " + viewStack.Peek().Text;
            }
            viewStack.Push(view);
            view.Show(theDockPanel);

            if (viewStack.Count==1)
            {
                utilitiesToolStripMenuItem.Visible = true;
                //ControlBox = true;
                backButton.Visible = false;
            }
            else
            {
                utilitiesToolStripMenuItem.Visible = false;
                //ControlBox = false;
                backButton.Visible = true;
            }

            backButton.Enabled = true;
            backButton.Text = "Back" + window_name;
        }
        /**************************************************************************************************/
        public void PopView()
        {
            string window_name = "";

            if (viewStack.Count > 0)
            {
                viewStack.Peek().Close();
                viewStack.Pop();
            }
            if (viewStack.Count > 0)
            {
                viewStack.Peek().PoppedToFront();
                viewStack.Peek().Show(theDockPanel);

                HraView temp = viewStack.Pop();
                if (viewStack.Count > 0)
                {
                    window_name = " to " + viewStack.Peek().Text;
                }
                backButton.Text = "Back" + window_name;
                viewStack.Push(temp);


            }
            if (viewStack.Count == 1)
            {
                utilitiesToolStripMenuItem.Visible = true;
                backButton.Enabled = false;
                //ControlBox = true;
                backButton.Visible = false;
            }
            else
            {
                utilitiesToolStripMenuItem.Visible = false;
                //ControlBox = false;
                backButton.Visible = true;
            }
        }

        /**************************************************************************************************/
        private bool IsCriticalMetaDataLoaded()
        {
            if (SessionManager.Instance.MetaData.Diseases.IsLoaded &&
                SessionManager.Instance.MetaData.GeneticTests.IsLoaded &&
                SessionManager.Instance.MetaData.BrOvCdsRecs.HraState == HraObject.States.Ready /*&&
                SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.hra_state == HraObject.States.Ready &&
                SessionManager.Instance.MetaData.CurrentUserDefaultPedigreePrefs.hra_state == HraObject.States.Ready*/
                )
            {
                return true;
            }
            else
                return false;
        }
        /**************************************************************************************************/
        //
        private void MainForm_Load(object sender, EventArgs e)
        {
            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

            MainFormLoading fml = new MainFormLoading();

            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel1.Visible = false;

            if (fml.ShowDialog() == DialogResult.Cancel)
            {
               failedLogin = true;
               this.Close();
            }
            else
            {
                PushViewDelegate = PushViewOnMainWindow;

                string configFile = SessionManager.SelectDockConfig("MainFormDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
                if (File.Exists(configFile))
                {
                    try
                    {
                        theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                    }
                    catch (NullDockingConfigException)
                    {
                        InitDefaultConfig();
                    }                    
                }
                else
                {
                    InitDefaultConfig();
                }

                startRiskServiceAvailabilityChecking();

                if (bid != null)
                {
                    bid.SetRoleAccess(fml.roleName);
                }

                switch (fml.roleName)
                {
                    // Administrative Staff cannot access PMR data (i.e., the dashboard and reports); can access other Utilities menu items.
                    case "Administrative Staff":
                        if (rcd != null)
                        {
                            rcd.Enabled = false;
                            rcd.Visible = false;
                        }



                        standardAndRiskClinicReportsToolStripMenuItem.Enabled = false;
                        clinicReportToolStripMenuItem.Enabled = false;
                        auditReportsToolStripMenuItem.Enabled = false;
                        editPedigreeSymbolsToolStripMenuItem.Enabled = false;

                        toolStripSeparator4.Visible = false;
                        toolStripSeparator5.Visible = false;
                        standardAndRiskClinicReportsToolStripMenuItem.Visible = false;
                        clinicReportToolStripMenuItem.Visible = false;
                        auditReportsToolStripMenuItem.Visible = false;
                        editPedigreeSymbolsToolStripMenuItem.Visible = false;
                        usersToolStripMenuItem.Visible = false;
                        editSurveyListToolStripMenuItem.Visible = false;
                        editQueueParameterList.Visible = false;
                        break;
                    case "Clinician":
                        editPedigreeSymbolsToolStripMenuItem.Visible = false;
                        editProvidersToolStripMenuItem.Visible = false;
                        usersToolStripMenuItem.Visible = false;
                        editSurveyListToolStripMenuItem.Visible = false;
                        editQueueParameterList.Visible = false;
                        break;
                    case "Technologist":
                        editPedigreeSymbolsToolStripMenuItem.Visible = false;
                        editProvidersToolStripMenuItem.Visible = false;
                        usersToolStripMenuItem.Visible = false;
                        editSurveyListToolStripMenuItem.Visible = false;
                        editQueueParameterList.Visible = false;
                        adminToolStripMenuItem.Visible = false;
                        toolStripSeparator2.Visible = false;
                        break;

                    case "Administrator":                        
                        break;

                    default:
                        editPedigreeSymbolsToolStripMenuItem.Enabled = false;
                        editPedigreeSymbolsToolStripMenuItem.Visible = false;
                        editSurveyListToolStripMenuItem.Visible = false;
                        editQueueParameterList.Visible = false;
                        break;
                }                
            }

            Thread.CurrentThread.Name = "MainGUI";
        }


        /**************************************************************************************************/
        private void InitDefaultConfig()
        {
            rcd = new RiskClinicDashboard();
            rcd.Show(theDockPanel);
            rcd.PushViewStack = PushViewOnMainWindow;
            rcd.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
        }
        
        /************************/
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(RiskClinicDashboard).ToString())
            {
                rcd = new RiskClinicDashboard();
                rcd.PushViewStack = PushViewOnMainWindow;
                viewStack.Push(rcd);
                return rcd;
            }
            if (persistString == typeof(BreastImagingDashboard).ToString())
            {
                bid = new BreastImagingDashboard();
                bid.PushViewStack = PushViewOnMainWindow;
                viewStack.Push(bid);
                return bid;
            }
            if (persistString == typeof(MyScheduleView).ToString())
            {
                mpv = new MyScheduleView();
                mpv.PushViewStack = PushViewOnMainWindow;
                viewStack.Push(mpv);
                return mpv;
            }

            if (persistString == "RiskApps3.View.RiskClinic.MyPatientsView")
            {
                RiskApps3.Model.Clinic.Dashboard.myPatientsQueue p_MyPatientsQueue = new Model.Clinic.Dashboard.myPatientsQueue();
                HighRiskFollowupView hrfv = new HighRiskFollowupView(p_MyPatientsQueue);
                hrfv.PushViewStack = PushViewOnMainWindow;
                viewStack.Push(hrfv);
                return hrfv;
            }

            return null;
        }
        /**************************************************************************************************/

        private void riskServicebackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (riskServicebackgroundWorker.CancellationPending)
                return;

            // Start the time-consuming operation of checking connectivity to the Aggregator and Risk Service
            AggregatorIsAvailable = testRequestToAggregator();
        }

        private void riskServicebackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripStatusLabel1.Text = (AggregatorIsAvailable ? "Connected" : "Not Connected");
            this.Refresh();
            statusStrip1.Refresh();
            RiskApps3.Utilities.Configurator.aggregatorActive = AggregatorIsAvailable;
        }

        private void startRiskServiceAvailabilityChecking()
        {
            toolStripStatusLabel2.Visible = true;

            toolStripStatusLabel1.Text = "Not Connected";
            toolStripStatusLabel1.Visible = true;

            AggregatorIsAvailable = false;
            if (RiskApps3.Utilities.Configurator.useAggregatorService())
            {
                //start Risk Service Availability Testing Thread(s)
                riskServicebackgroundWorker.RunWorkerAsync();
            }
            else
            {
                toolStripStatusLabel1.Text = "Unavailable";
                this.Refresh();
                statusStrip1.Refresh();
            }
        }

        /**************************************************************************************************/
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HraAboutBox hab = new HraAboutBox();
            hab.ShowDialog();
        }

        /**************************************************************************************************/
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (viewStack.Count > 0)
            {
                if (viewStack.Peek() is RiskClinicMainForm ||
                    viewStack.Peek() is EditSurveyForm || 
                    viewStack.Peek() is PedigreeForm ||
                    viewStack.Peek() is HighRiskFollowupView ||
                    viewStack.Peek() is MyScheduleView ||
                    viewStack.Peek() is PendingGeneticTestsView ||
                    viewStack.Peek() is PendingToDoView)
                {
                    CloseAppValidationForm cav = new CloseAppValidationForm();
                    cav.lastView = backButton.Text;
                    DialogResult dr = cav.ShowDialog();
                    if (dr == System.Windows.Forms.DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (dr == System.Windows.Forms.DialogResult.No)
                    {
                        PopView();
                        e.Cancel = true;
                        return;
                    }
                }
            }
            if (failedLogin == false)
            {
                string configFile = SessionManager.SelectDockConfig("MainFormDockPanel.config");
                
                if (SessionManager.Instance.SaveLayoutOnClose)
                    theDockPanel.SaveAsXml(configFile); 
            }

            if (mpv != null)
                SessionManager.Instance.RemoveHraView(mpv);

            for (int i = 0; i < viewStack.Count; i++)
            {
                viewStack.Pop().Close();
            }

                SessionManager.Instance.Shutdown();
        }

        /**************************************************************************************************/
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RiskClinicDashboard rcd = new RiskClinicDashboard();
            rcd.Show(theDockPanel);
            //rcd.AddViewToParent = AddView;
            rcd.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
        }

        private void myPatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pendingTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void atRiskToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pendingTestsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void patientsFollowedToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bRCAPositiveFamiliesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RiskAppUtils.frmAddEditAppointment addAppt = new RiskAppUtils.frmAddEditAppointment(RiskAppUtils.frmAddEditAppointment.ADD);
            addAppt.ShowDialog();
            SessionManager.Instance.ClearActivePatient();

            if (rcd != null)
                rcd.RefreshMyPatients();

            if (mpv != null)
                mpv.RefreshPatientList();


            if (bid != null)
                bid.RefreshPatientList();
        }

        private void createTestPatientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Cursor = Cursors.WaitCursor;
            //RiskAppCore.ApptUtils.createTestPatients();
            //if (rcd != null)
            //    rcd.RefreshMyPatients();

            //if (mpv != null)
            //    mpv.RefreshPatientList();

            //if (bid != null)
            //    bid.RefreshPatientList();

            //Cursor = Cursors.Default;

            RiskApps3.View.Admin.CreateTestPatientsForm createTPForm = new RiskApps3.View.Admin.CreateTestPatientsForm();

            createTPForm.ShowDialog();

            if (rcd != null)
                rcd.RefreshMyPatients();

            if (mpv != null)
                mpv.RefreshPatientList();


            if (bid != null)
                bid.RefreshPatientList();


        }


        private void deleteTestPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //BCDB2.Instance.ExecuteNonQuery("EXEC sp_deleteTestPatients");
            View.Appointments.DeleteTestAppointmentsPopup dtaPopup = new RiskApps3.View.Appointments.DeleteTestAppointmentsPopup();
            dtaPopup.ShowDialog();
            
            if (rcd != null)
                rcd.RefreshMyPatients();

            if (mpv != null)
                mpv.RefreshPatientList();


            if (bid != null)
                bid.RefreshPatientList();

            Cursor = Cursors.Default;
        }

        private void attributionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminMainForm adminMainForm = new AdminMainForm();
            adminMainForm.WindowState = FormWindowState.Maximized;
            adminMainForm.ShowDialog();
        }

        private void standardAndRiskClinicReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Reporting.exe";
            process.Start();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newMDIChild = new RiskAppUtils.frmAddEditUsers();
            //newMDIChild.MdiParent = this;
            newMDIChild.Show();
        }

        private void editProvidersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newMDIChild = new frmApptProviders();
            newMDIChild.ShowDialog();
        }

        private void clinicReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //RiskClinicReport rcp = new RiskClinicReport();
            //rcp.ShowDialog();

            Reporting r = new Reporting();
            r.ShowDialog();

        }

        private void checkVariationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeMyPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

            using (var form = new frmChangePassword(SessionManager.Instance.ActiveUser.userLogin, true, false))
            {
                form.ShowDialog(this);
            }
        }

        private void automationQueueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new AutomationQueueForm()) 
            {
                form.ShowDialog(this);
            }
        }

        private void cloudWebWorklistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new RiskApps3.View.Appointments.WebAppointments())
            {
                form.ShowDialog(this);
            }
            if (rcd != null)
                rcd.RefreshMyPatients();

            if (mpv != null)
                mpv.RefreshPatientList();


            if (bid != null)
                bid.RefreshPatientList();

            //using (var form = new CloudWebQueue())
            //{
            //    form.ShowDialog(this);
            //}
        }

        private void editPedigreeSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedigreeSymbolEditor pse = new PedigreeSymbolEditor();
            pse.ShowDialog();
        }

        private void auditReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AuditReportsForm auditReports = new AuditReportsForm();
                auditReports.ShowDialog();
            }
            catch (Exception eee)
            {
                Utilities.Logger.Instance.WriteToLog("auditReports error" + eee);
            }
        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.hughesriskapps.com/contactus.php");
            }
            catch { }
        }

        private void editSurveyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveSurveyEditor ase = new ActiveSurveyEditor();
            ase.ShowDialog();
        }

        private void editQueueParameterList_Click(object sender, EventArgs e)
        {
            QueueParameterEditor qpe = new QueueParameterEditor();
            qpe.ShowDialog();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopView();
        }

        /**************************************************************************************************/
        private bool testRequestToAggregator()
        {
            //call the aggregator service; this is a blocking call, up to 60 seconds
            //next line is for testing locally only
            //AggregatorServiceReferenceDev3.Service1Client client = new AggregatorServiceReferenceDev3.Service1Client();
            HraRiskAggregator.Service1Client client = new HraRiskAggregator.Service1Client("WSHttpBinding_IService1");
            client.InnerChannel.OperationTimeout = new TimeSpan(0, 1, 0);  //set the timeout to 1 minute

            try
            {
                //call the aggregator using the licenseID in the config file
                string licID = RiskApps3.Utilities.Configurator.getAggregatorLicenseID();

                //block here 'till get a reply or timeout
                return client.HeartBeat(licID);
            }
            catch (Exception e)  //exceptioned out on Aggregator web service call; likely due to timeout
            {
                return false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}

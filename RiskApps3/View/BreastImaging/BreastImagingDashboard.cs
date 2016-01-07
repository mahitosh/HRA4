using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using BrightIdeasSoftware;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.Clinic.Dashboard;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Utilities;
using RiskApps3.View.Appointments;
using RiskApps3.View.Common;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.Reporting;
using RiskApps3.View.Risk;
using RiskApps3.View.RiskClinic;
using RiskAppUtils;
using WeifenLuo.WinFormsUI.Docking;
using User = RiskAppCore.User;
using RiskApps3.View.PatientRecord.FamilyHistory;

namespace RiskApps3.View.BreastImaging
{
    public partial class BreastImagingDashboard : HraView
    {
        private AppointmentList appts = null;
        public int DashboardClinicId = -1;

        DateTime DashboardStart;

        DateTime DashboardEnd;
        RiskElementControl.Mode DashboardMode = RiskElementControl.Mode.Prevelance;

        private AtRisk p_AtRisk;
        private HighRiskBrcaQueue p_HighRiskBrcaQueue;
        private HighRiskLifetimeBreastQueue p_HighRiskLifetimeBreastQueue;
        private HighRiskColonQueue p_HighRiskColonQueue;

        private bool trigger = false;

        public delegate void FollowupCallbackType(string queue);
        public FollowupCallbackType FollowupDelegate;

        public BreastImagingDashboard()
        {

            DashboardStart = new DateTime(DateTime.Now.Year, 1, 1);
            DashboardEnd = new DateTime(DateTime.Now.Year, 12, 31);

            InitializeComponent();

            FollowupDelegate = Followup;

            olvColumnCompleted.Renderer = new NullDateRenderer();

            p_AtRisk = new AtRisk();
            p_HighRiskBrcaQueue = new HighRiskBrcaQueue();
            p_HighRiskLifetimeBreastQueue = new HighRiskLifetimeBreastQueue();
            p_HighRiskColonQueue = new HighRiskColonQueue();



            label8.BackColor = Color.Transparent;
            label9.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label10.BackColor = Color.Transparent;

        }

        public void Followup(string type)
        {
            HighRiskFollowupView hrfv = null;


            FollowupQueue queue = new FollowupQueue();
            queue.QueueName = "My Patients";
            queue.QueueText = "Follow Patients";
            queue.type = type;
            queue.clinicId = DashboardClinicId;
            queue.startTime = DashboardStart;
            queue.endTime = DashboardEnd;
            queue.mode = DashboardMode;

            hrfv = new HighRiskFollowupView(queue);

            if (hrfv != null)
            {
                hrfv.PushViewStack = PushViewStack;
                PushViewStack(hrfv, DockState.Document);
            }
        }
        private void BreastImagingDashboard_Load(object sender, EventArgs e)
        {
            //comboBox1.Text = "This Year";

            trigger = false;
            object defaultClinic = null;
            foreach (Clinic c in SessionManager.Instance.ActiveUser.userClinicList)
            {
                comboBox2.Items.Add(c);
                if (c.clinicID == DashboardClinicId)
                {
                    defaultClinic = c;
                }
            }
            if (defaultClinic != null)
            {
                comboBox2.SelectedItem = defaultClinic;
            }
            else
            {
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                    DashboardClinicId = ((Clinic)comboBox2.SelectedItem).clinicID;
                }
            }

            Clinic all = new Clinic();
            all.clinicID = -1;
            all.clinicName = "All Clinics";
            comboBox2.Items.Add(all);

            //LoadDashboardElements();

            SessionManager.Instance.ClearActivePatient();

            SessionManager.Instance.NewActivePatient +=
                new SessionManager.NewActivePatientEventHandler(NewActivePatient);

            dateTimePicker1.Value = DateTime.Now;


            CompositeAllFilter currentFilter = null;
            OlderApptFilter oaf = new OlderApptFilter();
            List<IModelFilter> listOfFilters = new List<IModelFilter>();
            listOfFilters.Add(oaf); //add the filter
            currentFilter = new CompositeAllFilter(listOfFilters);
            fastDataListView1.ModelFilter = currentFilter;


            trigger = true;
            GetNewAppointmentList();

            p_AtRisk.clinicId = DashboardClinicId;
            p_HighRiskBrcaQueue.clinicId = DashboardClinicId;
            p_HighRiskLifetimeBreastQueue.clinicId = DashboardClinicId;
            p_HighRiskColonQueue.clinicId = DashboardClinicId;

            p_AtRisk.AddHandlersWithLoad(null, atRiskLoaded, null);

        }
        /**************************************************************************************************/
        private void atRiskLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            label8.Text = p_AtRisk.NumAtRiskBreast.ToString("#,###,###");
            label9.Text = p_AtRisk.NumAtRiskColon.ToString("#,###,###");
            label6.Text = p_AtRisk.NumMaxLifetimeBreastGE20.ToString("#,###,###");
            label10.Text = p_AtRisk.PrintQueueCount.ToString("#,###,###");
        }

        private void GetNewAppointmentList()
        {

            fastDataListView1.Visible = false;

            if (appts != null)
                appts.ReleaseListeners(this);

            appts = new AppointmentList();
            if (checkBox1.Checked)
                appts.Date = dateTimePicker1.Value.ToShortDateString();
            else
                appts.Date = null;

            if (comboBox2.SelectedItem != null)
            {
                appts.clinicId = ((Clinic)comboBox2.SelectedItem).clinicID;
            }
            //appts.groupName = SessionManager.Instance.UserGroup;
            appts.NameOrMrn = textBoxFilterData.Text;
            appts.AddHandlersWithLoad(AppointmentListChanged,
                                      AppointmentListLoaded,
                                      AppointmentChanged);
        }

        /**************************************************************************************************/

        private void AppointmentListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Appointment theAppt = (Appointment)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }

        private void AppointmentListLoaded(HraListLoadedEventArgs e)
        {
            if (fastDataListView1.ModelFilter != null)
            {
                CompositeAllFilter currentFilter = (CompositeAllFilter)fastDataListView1.ModelFilter;
                currentFilter.Filters.Clear();
            }
            lock (appts)
            {

                Appointment a = ((Appointment)(fastDataListView1.SelectedObject));
                bool found = false;
                fastDataListView1.ClearObjects();
                fastDataListView1.SetObjects(appts);

                if (a != null)
                {
                    foreach (object o in fastDataListView1.Objects)
                    {
                        if (((Appointment)o).apptID == ((Appointment)a).apptID)
                        {
                            fastDataListView1.SelectedObject = o;
                            found = true;
                            break;
                        }
                    }
                }
                if (found == false)
                {
                    SessionManager.Instance.ClearActivePatient();
                }
                fastDataListView1.Visible = true;

                //label1.Text = appts.Count.ToString() + " Total";
                label1.Text = fastDataListView1.Items.Count.ToString() + " Results";
            }
        }

        private void AppointmentChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        //private void LoadDashboardElements()
        //{
        //    tableLayoutPanel2.Controls.Add(CreateElement("High Lifetime Breast Cancer Risk", "BreastCancer", DashboardStart, DashboardEnd, DashboardClinicId, Color.Red), 1, tableLayoutPanel2.RowCount - 1);
        //    tableLayoutPanel2.RowCount = tableLayoutPanel2.RowCount + 1;
        //    tableLayoutPanel2.Controls.Add(CreateElement("Intermediate Lifetime Breast Cancer Risk", "ModerateBreastCancer", DashboardStart, DashboardEnd, DashboardClinicId, Color.Orange), 1, tableLayoutPanel2.RowCount - 1);
        //    tableLayoutPanel2.RowCount = tableLayoutPanel2.RowCount + 1;
        //    tableLayoutPanel2.Controls.Add(CreateElement("High BRCA1/2 Mutation Risk", "BRCA", DashboardStart, DashboardEnd, DashboardClinicId, Color.Red), 1, tableLayoutPanel2.RowCount - 1);
        //    tableLayoutPanel2.RowCount = tableLayoutPanel2.RowCount + 1;
        //    tableLayoutPanel2.Controls.Add(CreateElement("High Lynch Syndrome Risk", "Lynch", DashboardStart, DashboardEnd, DashboardClinicId, Color.Red), 1, tableLayoutPanel2.RowCount - 1);
        //}

        private Control CreateElement(string title, string type, DateTime DashboardStart, DateTime DashboardEnd, int clinc_id, Color color)
        {
            RiskElementControl c = new RiskElementControl();
            c.SetColor(color);
            c.SetTitle(title);
            c.data.type = type;
            c.data.startTime = DashboardStart;
            c.data.endTime = DashboardEnd;
            c.data.clinicId = clinc_id;
            c.current_mode = DashboardMode;
            c.Register(Followup);
            c.Dock = DockStyle.Top;
            return c; 
        }

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            this.riskClinic.Enabled = true;
            this.documents.Enabled = true;
            this.riskCalcs.Enabled = true;
            editSurvey.Enabled = true;
            button6.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = checkBox1.Checked;

            if (trigger)
                GetNewAppointmentList();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (trigger)
                GetNewAppointmentList();
        }

        private void buttonApplyTextSearch_Click(object sender, EventArgs e)
        {
            if (trigger)
                GetNewAppointmentList();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (trigger)
            {
                label1.Text = "...";
                label6.Text = "...";
                label8.Text = "...";
                label9.Text = "...";
                label10.Text = "...";

                GetNewAppointmentList();
                if (comboBox2.SelectedItem != null)
                {
                    DashboardClinicId = ((Clinic)comboBox2.SelectedItem).clinicID;
                }
                //tableLayoutPanel2.Controls.Clear();
                //LoadDashboardElements();

                p_AtRisk.clinicId = DashboardClinicId;
                p_HighRiskBrcaQueue.clinicId = DashboardClinicId;
                p_HighRiskLifetimeBreastQueue.clinicId = DashboardClinicId;
                p_HighRiskColonQueue.clinicId = DashboardClinicId;
                p_AtRisk.LoadObject();
            }
        }

        private void fastDataListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(fastDataListView1.PointToScreen(e.Location));
            }
        }

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                SetStateApptSelected();
            }
        }

        private void SetStateApptSelected()
        {
            Appointment appt = (Appointment)(fastDataListView1.SelectedObject);

            if (string.IsNullOrEmpty(appt.unitnum) == false)
            {
                SessionManager.Instance.SetActivePatient(appt.unitnum, -1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment)(fastDataListView1.SelectedObject));
                string strMessage = "Are you sure you want to delete this appointment:\n\n";
                strMessage = strMessage + "Unit Number:\t" + selectedAppt.unitnum +
                             "\n";
                strMessage = strMessage + "Patient Name:\t" + selectedAppt.patientname +
                             "\n";
                strMessage = strMessage + "Date of Birth:\t" + selectedAppt.dob +
                             "\n\n";
                strMessage = strMessage + "Deletions cannot be undone!";
                if (MessageBox.Show(strMessage, "riskApps™", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("apptID", selectedAppt.apptID);
                    pc.Add("keepApptRecord", 0);
                    BCDB2.Instance.RunSPWithParams("sp_deleteWebAppointment", pc);
                }

                GetNewAppointmentList();
            }
            else
            {
                MessageBox.Show("No appointment selected.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addApptButton_Click(object sender, EventArgs e)
        {
            int clinicId = ((Clinic)comboBox2.SelectedItem).clinicID;
            User.setClinicID(clinicId);

            NewAppointmentWizard wizard = new NewAppointmentWizard(clinicId);
            wizard.ShowDialog(this);
            
            SessionManager.Instance.ClearActivePatient();
            GetNewAppointmentList();
        }

        private void editApptButton_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment)(fastDataListView1.SelectedObject));
                User.setClinicID(((Clinic)comboBox2.SelectedItem).clinicID);
                frmAddEditAppointment addAppt =
                    new frmAddEditAppointment(frmAddEditAppointment.EDIT, selectedAppt.apptID);
                addAppt.ShowDialog();
                SessionManager.Instance.ClearActivePatient();
                GetNewAppointmentList();
            }
            else
            {
                MessageBox.Show("No appointment selected.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyApptButton_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment)(fastDataListView1.SelectedObject));
                User.setClinicID(((Clinic)comboBox2.SelectedItem).clinicID);
                frmAddEditAppointment addAppt =
                    new frmAddEditAppointment(frmAddEditAppointment.COPY, selectedAppt.apptID);
                addAppt.ShowDialog();
                SessionManager.Instance.ClearActivePatient();
                GetNewAppointmentList();
            }
            else
            {
                MessageBox.Show("No appointment selected.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void riskClinic_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;

            if (appt.GoldenAppt != appt.apptID)
            {
                if (appt.apptdatetime <= appt.GoldenApptTime)
                {
                    //MessageBox.Show("You have selected an older appointment.  The most recent record for this patient is from " + appt.GoldenApptTime,");
                    LegacyApptForm laf = new LegacyApptForm();
                    laf.selected = appt.apptdatetime;
                    laf.golden = appt.GoldenApptTime;
                    laf.ShowDialog();
                    if (laf.result == "Continue")
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.GoldenAppt);
                        apptid = appt.GoldenAppt;
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                }
            }

            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid);
            msapf.ShowDialog();

            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);
            PushViewStack(rcmf, DockState.Document);

        }

        private void documents_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;

            NewHtmlDocumentForm nhdf = new NewHtmlDocumentForm();
            nhdf.routine = "screening";
            nhdf.ShowDialog();
            
            //PatientCommunicationView pcv = new PatientCommunicationView();
            //PushViewStack(pcv, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            //pcv.ShowDialog();
        }

        private void riskCalcs_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;

            RiskScoresView rsv = new RiskScoresView();
            //PushViewStack(rsv, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            rsv.ShowDialog();
        }

        public override void PoppedToFront()
        {
            GetNewAppointmentList();

            p_AtRisk.LoadObject();

            //tableLayoutPanel2.Controls.Clear();
            //LoadDashboardElements();
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment)(fastDataListView1.SelectedObject));
                User.setClinicID(((Clinic)comboBox2.SelectedItem).clinicID);
                frmAddEditAppointment addAppt =
                    new frmAddEditAppointment(frmAddEditAppointment.EDIT, selectedAppt.apptID);
                addAppt.ShowDialog();
                SessionManager.Instance.ClearActivePatient();
                GetNewAppointmentList();
            }
            else
            {
                MessageBox.Show("No appointment selected.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /***************************************************************/
        private bool testSGDoc(string fileName) // Silicus: Created a copy of this method in AppointmentService class.
        {
            //test if is Surgeon General XML file
            XmlDocument suspectSGDoc = new XmlDocument();
            suspectSGDoc.Load(fileName);
            XPathNavigator sgNav = suspectSGDoc.CreateNavigator();
            string str = sgNav.Evaluate("string(FamilyHistory/methodCode[1]/@displayName)").ToString();

            return (!String.IsNullOrEmpty(str) && str.StartsWith("Surgeon General")); //presumed Surgeon General XML file
        }

        /***************************************************************/
        private enum ImportExportMode { ExportHL7, ImportHL7, ExportXML, ImportXML };
        private class ImportExportArgs
        {
            public ImportExportMode mode;
            public string fileName;
            public Patient patient;
            public int apptID;
            public bool deIdentify;
            public bool is_SG_XML;
            public string existingUnitnum;
        }

        private void saveAsXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void saveAsXMLToolStripMenuItem_Click(object sender, EventArgs e, bool deIdentify)
        {
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;
            if (appt.GoldenAppt != appt.apptID)
            {
                if (appt.apptdatetime <= appt.GoldenApptTime)
                {
                    //MessageBox.Show("You have selected an older appointment.  The most recent record for this patient is from " + appt.GoldenApptTime,");
                    LegacyApptForm laf = new LegacyApptForm();
                    laf.selected = appt.apptdatetime;
                    laf.golden = appt.GoldenApptTime;
                    laf.ShowDialog();
                    if (laf.result == "Continue")
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.GoldenAppt);
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                }
            }

            Patient patient = SessionManager.Instance.GetActivePatient();

            string fileName = "";
            string initDir = Configurator.GetDocumentStorage();
            if (string.IsNullOrEmpty(initDir))
                exportSaveFileDialog.InitialDirectory = Configurator.getNodeValue("Globals", "DocumentStorage"); //"c:\\Program Files\\riskappsv2\\documents"; 
            else
                exportSaveFileDialog.InitialDirectory = initDir;

            exportSaveFileDialog.Filter = "Patient Serialization File (*.xml)|*.xml";
            exportSaveFileDialog.FilterIndex = 2;
            exportSaveFileDialog.RestoreDirectory = true;
            exportSaveFileDialog.OverwritePrompt = true;
            exportSaveFileDialog.FileName =
                SessionManager.Instance.GetActivePatient().name + " " +
                SessionManager.Instance.GetActivePatient().unitnum +
                " Serialization " +
                DateTime.Now.ToString("yyyy-MM-dd-HHmm");

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = exportSaveFileDialog.FileName;
                    if (fileName.Length != 0)
                    {
                        //xmlExport(fileName, patient);

                        ImportExportArgs args = new ImportExportArgs();
                        args.mode = ImportExportMode.ExportXML;
                        args.fileName = fileName;
                        args.patient = patient;
                        args.apptID = SessionManager.Instance.GetActivePatient().apptid;
                        args.deIdentify = deIdentify;

                        disableButtons();
                        importExportBackgroundWorker.RunWorkerAsync(args);
                    }
                }
                catch (Exception eee)
                {
                    // get call stack
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                    Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\t" + eee);
                    return;
                }
            }

        }
        private void disableButtons()
        {
            contextMenuStrip1.Enabled = false;
            button1.Enabled = false;
            addApptButton.Enabled = false;
            editApptButton.Enabled = false;
        }
        private void loadFromXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "";
            //importOpenFileDialog.InitialDirectory = "c:\\";
            importOpenFileDialog.Filter = "Patient Data XML Import File (*.xml)|*.xml";
            importOpenFileDialog.FilterIndex = 2;
            importOpenFileDialog.RestoreDirectory = true;
            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = importOpenFileDialog.FileName;
                    if (fileName.Length != 0)
                    {
                        //importXmlApptID = xmlImport(fileName);

                        ImportExportArgs args = new ImportExportArgs();
                        args.is_SG_XML = testSGDoc(fileName);
                        args.mode = (args.is_SG_XML) ? ImportExportMode.ImportHL7 : ImportExportMode.ImportXML;
                        args.fileName = fileName;
                        args.patient = (args.is_SG_XML) ? SessionManager.Instance.GetActivePatient() : null; 
                        args.apptID = SessionManager.Instance.GetActivePatient().apptid;
                        args.existingUnitnum = SessionManager.Instance.GetActivePatient().unitnum;

                        disableButtons();
                        importExportBackgroundWorker.RunWorkerAsync(args);
                    }
                }
                catch (Exception eeee)
                {
                    // get call stack
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                    Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\t" + eeee);
                    return;
                }
            }
        }

        private void saveAsHL7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void saveAsHL7ToolStripMenuItemProcess(object sender, EventArgs e, bool deIdentify)
        {
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;
            if (appt.GoldenAppt != appt.apptID)
            {
                if (appt.apptdatetime <= appt.GoldenApptTime)
                {
                    //MessageBox.Show("You have selected an older appointment.  The most recent record for this patient is from " + appt.GoldenApptTime,");
                    LegacyApptForm laf = new LegacyApptForm();
                    laf.selected = appt.apptdatetime;
                    laf.golden = appt.GoldenApptTime;
                    laf.ShowDialog();
                    if (laf.result == "Continue")
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.GoldenAppt);
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                }
            }




            string fileName = "";
            //exportSaveFileDialog.InitialDirectory = "c:\\Program Files\\riskappsv2\\documents"; 
            exportSaveFileDialog.Filter = "HL7 File (*.xml)|*.xml";
            exportSaveFileDialog.FilterIndex = 2;
            exportSaveFileDialog.RestoreDirectory = true;
            exportSaveFileDialog.OverwritePrompt = true;
            exportSaveFileDialog.FileName =
                SessionManager.Instance.GetActivePatient().name + " " +
                SessionManager.Instance.GetActivePatient().unitnum +
                " HL7 " +
                DateTime.Now.ToString("yyyy-MM-dd-HHmm");

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = exportSaveFileDialog.FileName;
                    if (fileName.Length != 0)
                    {
                        //exportHL7(fileName);

                        ImportExportArgs args = new ImportExportArgs();
                        args.mode = ImportExportMode.ExportHL7;
                        args.fileName = fileName;
                        //args.patient = null;  //old way
                        args.patient = SessionManager.Instance.GetActivePatient();
                        args.apptID = SessionManager.Instance.GetActivePatient().apptid;
                        args.deIdentify = deIdentify;

                        disableButtons();
                        importExportBackgroundWorker.RunWorkerAsync(args);
                    }
                }
                catch (Exception ee)
                {
                    // get call stack
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                    Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\t" + ee);
                    return;
                }
            }
        }

        private void loadFromHL7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "";
            //importOpenFileDialog.InitialDirectory = "c:\\";
            importOpenFileDialog.Filter = "HL7 XML Import File (*.xml)|*.xml";
            importOpenFileDialog.FilterIndex = 2;
            importOpenFileDialog.RestoreDirectory = true;
            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fileName = importOpenFileDialog.FileName;
                    if (fileName.Length != 0)
                    {
                        //HL7.DataImporter.ImportHL7(fileName);

                        ImportExportArgs args = new ImportExportArgs();
                        args.mode = ImportExportMode.ImportHL7;
                        args.is_SG_XML = testSGDoc(fileName);
                        args.fileName = fileName;
                        args.patient = SessionManager.Instance.GetActivePatient();
                        args.apptID = SessionManager.Instance.GetActivePatient().apptid;

                        disableButtons();
                        importExportBackgroundWorker.RunWorkerAsync(args);  
                    }
                }
                catch (Exception eeeee)
                {
                    // get call stack
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                    Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\t" + eeeee);
                    return;
                }
            }
        }

        ImportExportArgs args; 
        private void importExportBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            args = (ImportExportArgs)e.Argument;

            try
            {
                if (args.mode == ImportExportMode.ExportHL7)
                {

                    //PRB 2014.08.20 New and Improved way:
                    //Serialize the FH
                    args.patient.LoadFullObject(); //needed to ensure patient object is complete, including ObGynHx, etc.
                    FamilyHistory theFH = args.patient.owningFHx;
                    string fhAsString = TransformUtils.DataContractSerializeObject<FamilyHistory>(theFH);

                    //transform it
                    XmlDocument inDOM = new XmlDocument();
                    inDOM.LoadXml(fhAsString);
                    string toolsPath = Configurator.getNodeValue("Globals", "ToolsPath");  // @"C:\Program Files\riskappsv2\tools\";

                    XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hra_to_ccd_remove_namespaces.xsl");
                    XmlDocument hl7FHData = TransformUtils.performTransformWithParam(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "deIdentify", (args.deIdentify) ? "1" : "0");

                    //following actually removes all indentation and extra whitespace; prefer to save the file with indentations, so leave this commented
                    //hl7FHData.PreserveWhitespace = true;
                    hl7FHData.Save(args.fileName);
                }
                else if (args.mode == ImportExportMode.ImportHL7)
                {
                    if (File.Exists(args.fileName))
                    {
                        Appointment.DeleteApptData(args.apptID, true);

                        //string local_dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                        string local_dir = Configurator.getNodeValue("Globals", "ToolsPath");

                        string riskMeanings = File.ReadAllText(Path.Combine(local_dir, "riskMeanings.xml"));
                        string HL7Relationships = File.ReadAllText(Path.Combine(local_dir, "HL7Relationships.xml"));

                        string hl7 = File.ReadAllText(args.fileName);
                        //if this is a Surgeon General XML file, transform it to HL7 first
                        if (args.is_SG_XML)
                        {
                            //transform it
                            XmlDocument inDOM = new XmlDocument();
                            inDOM.LoadXml(hl7);
                            string toolsPath = Configurator.getNodeValue("Globals", "ToolsPath");  // @"C:\Program Files\riskappsv2\tools\";
                            XmlDocument result_SG_XmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"sg_to_hl7.xsl");
                            hl7 = result_SG_XmlDoc.InnerXml;
                        }

                        Patient p = Patient.processHL7Import(args.apptID, hl7, riskMeanings, HL7Relationships);
                        if (string.IsNullOrEmpty(p.name))
                        {
                            if (string.IsNullOrEmpty(args.patient.name) == false)
                            {
                                p.name = args.patient.name;
                            }
                        }
                        p.PersistFullObject(new HraModelChangedEventArgs(null));
                    }
                }
                else if (args.mode == ImportExportMode.ExportXML)
                {
                    xmlExport(args.fileName, args.patient, args.deIdentify);
                }
                else if (args.mode == ImportExportMode.ImportXML)
                {
                    args.apptID = xmlImport(args.fileName, args.apptID, args.existingUnitnum); //pass in the unitnum for the existing apptID
                }
                else
                {
                    // get call stack
                    StackTrace stackTrace = new StackTrace();

                    // get calling method name
                    String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                    Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\tUnknown mode: [" +
                        args.mode.ToString() + "]");
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteToLog(ex.ToString());
                //// get call stack
                //StackTrace stackTrace = new StackTrace();

                //// get calling method name
                //String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                //Utilities.Logger.Instance.WriteToLog("[MyPatientsView] from [" + callingRoutine + "]\n\tmode=" +
                //    args.mode.ToString() + "\n\tfileName=" + args.fileName + "\n\tunitnum=" + 
                //    args.patient == null ? "" : args.patient.unitnum);
            }
            importExportBackgroundWorker.ReportProgress(100);
        }

        private int xmlImport(string fileName, int apptid, string existingUnitnum)
        {
            DataContractSerializer ds = new DataContractSerializer(typeof(FamilyHistory));
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            FamilyHistory fhx;
            try
            {
                fhx = (FamilyHistory)ds.ReadObject(fs);
            }
            catch (Exception e)  //catch exception where cdsBreastOvary data is older version
            {
                fs.Flush();
                fs.Position = 0;
                XDocument doc;
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    doc = XDocument.Load(reader);
                }

                doc.XPathSelectElement("//*[local-name() = 'cdsBreastOvary']").Remove();

                var xmlDocument = new XmlDocument();
                using (var xmlReader = doc.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }

                MemoryStream ms = new MemoryStream();
                xmlDocument.Save(ms);
                ms.Flush();
                ms.Position = 0;
                fhx = (FamilyHistory)ds.ReadObject(ms);
            }

            foreach (Person p in fhx)
            {
                if (p is Patient)
                {
                    fhx.proband = (Patient)p;
                }
            }
            fhx.proband.apptid = apptid;
            Appointment.DeleteApptData(apptid, true);
            if (fhx.proband.unitnum == null)  //no unitnum happens when importing from de-identified XML
            {
                fhx.proband.unitnum = existingUnitnum;  //just continue to use the existing unitnum for the appt we're overwriting
            }
            Appointment.UpdateAppointmentUnitnum(apptid, fhx.proband.unitnum);
            fhx.proband.PersistFullObject(new HraModelChangedEventArgs(null));

            fs.Close();

            return fhx.proband.apptid;


        }

        private void importExportBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }
        private void xmlExport(string fileName, Patient patient, bool deIdentify)
        {
            patient.LoadFullObject();
            FamilyHistory theFH = patient.owningFHx;

            if (!deIdentify)
            {
                //legacy code chunk; written before caring about de-identifying
                DataContractSerializer ds = new DataContractSerializer(typeof(FamilyHistory));
                FileStream stm = new FileStream(fileName, FileMode.Create);
                ds.WriteObject(stm, theFH);
                stm.Flush();
                stm.Position = 0;
                stm.Close();
                return;
            }

            // De-Identify the XML data by using a transform, then save the file
            string fhAsString = TransformUtils.DataContractSerializeObject<FamilyHistory>(theFH);

            //transform it
            XmlDocument inDOM = new XmlDocument();
            inDOM.LoadXml(fhAsString);
            string toolsPath = Configurator.AssemblyDirectory; //since project is built with xsl file as linked project member, xsl s/b in the executing folder, whereever that is

            XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hraDeIdentifySerialized.xsl");

            //following actually removes all indentation and extra whitespace; prefer to save the file with indentations, so leave this commented
            //hl7FHData.PreserveWhitespace = true;
            resultXmlDoc.Save(fileName);
        }

        private void importExportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            GetNewAppointmentList();
            fastDataListView1.Refresh();

            Thread.Sleep(100);
            Application.DoEvents();

            if (args.apptID > 0)
            {
                foreach (object o in fastDataListView1.Objects)
                {
                    if (((Appointment)o).apptID == args.apptID)
                    {
                        fastDataListView1.SelectedObject = o;
                        break;
                    }
                }
            }

            contextMenuStrip1.Enabled = true;
            button1.Enabled = true;
            addApptButton.Enabled = true;
            editApptButton.Enabled = true;
            progressBar1.Visible = false;
            progressBar1.Enabled = false;
        }

        private void markCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }

            Appointment.MarkComplete(appt.apptID);
            MessageBox.Show("Appointment Marked Complete.");
            if (trigger)
                GetNewAppointmentList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }
            Appointment.MarkIncomplete(appt.apptID);

            MessageBox.Show("Appointment Marked Incomplete.");
            if (trigger)
                GetNewAppointmentList();
        }

        private void markForAutomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }
            Appointment.MarkComplete(appt.apptID);
            RunRiskModelsDialog rsmd = new RunRiskModelsDialog(true);
            rsmd.ShowDialog();
            //Appointment.MarkForAutomation(appt.apptID);
            MessageBox.Show("Risk data recalculated and automation documents run","Automation Complete",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }






        //private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    tableLayoutPanel2.Controls.Clear();
        //    switch (comboBox1.SelectedItem.ToString())
        //    {
        //        case "Today":
        //            DashboardStart = DateTime.Now.Date;
        //            DashboardEnd = DateTime.Now.Date;
        //            break;
        //        case "This Week":
        //            DashboardStart = UIUtils.weekstart(DateTime.Now.Date);
        //            DashboardEnd = UIUtils.weekend(DateTime.Now.Date);
        //            break;
        //        case "This Month":
        //            DashboardStart = new DateTime(DateTime.Now.Year, 1, 1);
        //            DashboardEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        //            break;
        //        case "This Year":
        //            DashboardStart = new DateTime(DateTime.Now.Year, 1, 1);
        //            DashboardEnd = new DateTime(DateTime.Now.Year, 12, 31);
        //            break;
        //        case "Last Year":
        //            DashboardStart = new DateTime(DateTime.Now.Year-1, 1, 1);
        //            DashboardEnd = new DateTime(DateTime.Now.Year-1, 12, 31);
        //            break;
        //        default:
        //            DateTime.TryParse("1/1/1900", out DashboardStart);
        //            DateTime.TryParse("1/1/3000", out DashboardEnd);
        //            break;
        //    }
            
        //    LoadDashboardElements();
        //}

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            //tableLayoutPanel2.Controls.Clear();
            //LoadDashboardElements();
        }

        //private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    tableLayoutPanel2.Controls.Clear();
        //    switch (comboBox3.SelectedItem.ToString())
        //    {
        //        case "New Cases (Incidence)":
        //            DashboardMode = RiskApps3.View.Reporting.RiskElementControl.Mode.Incidence;
        //            break;
        //        case "All Cases (Prevelance)":
        //            DashboardMode = RiskApps3.View.Reporting.RiskElementControl.Mode.Prevelance;
        //            break;
        //    }

        //    LoadDashboardElements();
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            using (var form = new PrintQueueForm())
            {
                form.ShowDialog(this);
            }
        }

        private void todayButton_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void LifetimeBreastQueue_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskLifetimeBreastQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, DockState.Document);
        }

        private void BrcaQueue_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskBrcaQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, DockState.Document);
        }

        private void LynchQueue_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskColonQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, DockState.Document);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            LifetimeBreastQueue_Click(sender, e);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            BrcaQueue_Click(sender, e);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            LynchQueue_Click(sender, e);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;

            if (appt.GoldenAppt != appt.apptID)
            {
                if (appt.apptdatetime < appt.GoldenApptTime)
                {
                    //MessageBox.Show("You have selected an older appointment.  The most recent record for this patient is from " + appt.GoldenApptTime,");
                    LegacyApptForm laf = new LegacyApptForm();
                    laf.selected = appt.apptdatetime;
                    laf.golden = appt.GoldenApptTime;
                    laf.ShowDialog();
                    if (laf.result == "Continue")
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.GoldenAppt);
                        apptid = appt.GoldenAppt;
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                }
            }
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid);
            msapf.ShowDialog();


            PedigreeForm pf = new PedigreeForm();
            PushViewStack(pf, DockState.Document);
        }



        internal void SetRoleAccess(string p)
        {
            switch (p)
            {
                case "Administrative Staff":
                    tableLayoutPanel1.Visible = false;
                    tableLayoutPanel1.Enabled = false;

                    groupBox1.Visible = false;
                    groupBox1.Enabled = false;

                    //groupBox2.Visible = false;
                    //groupBox2.Enabled = false;

                    groupBox3.Visible = false;
                    groupBox3.Enabled = false;

                    break;
                case "Clinician":
                    break;
                case "Technologist":
                    break;
                case "Administrator":
                    break;
                default:
                    break;
            }
        }

        internal void RefreshPatientList()
        {
            GetNewAppointmentList();
        }

        private void deIdentifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsHL7ToolStripMenuItemProcess(sender, e, true);
        }

        private void identifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsHL7ToolStripMenuItemProcess(sender, e, false);
        }

        private void deIdentifiedXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsXMLToolStripMenuItem_Click(sender, e, true);
        }

        private void identifiedXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsXMLToolStripMenuItem_Click(sender, e, false);
        }

        private void textBoxFilterData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (trigger)
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    GetNewAppointmentList();
                }
            }
        }

        private void editSurvey_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }
            Appointment appt = ((Appointment)(fastDataListView1.SelectedObject));
            int apptid = appt.apptID;

            if (appt.GoldenAppt != appt.apptID)
            {
                if (appt.apptdatetime <= appt.GoldenApptTime)
                {
                    //MessageBox.Show("You have selected an older appointment.  The most recent record for this patient is from " + appt.GoldenApptTime,");
                    LegacyApptForm laf = new LegacyApptForm();
                    laf.selected = appt.apptdatetime;
                    laf.golden = appt.GoldenApptTime;
                    laf.ShowDialog();
                    if (laf.result == "Continue")
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.GoldenAppt);
                        apptid = appt.GoldenAppt;
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.unitnum, apptid);
                }
            }

            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid);
            msapf.ShowDialog();

            EditSurveyForm esf = new EditSurveyForm();
            esf.InitialView = typeof(ExpressFamilyHistoryView);
            PushViewStack(esf, DockState.Document);

        }

        private void addToDoNotCallListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string assignedBy = "";
            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();
                }
            }
            Patient p = SessionManager.Instance.GetActivePatient();     // TODO:  Check this!!
            Task t = new Task(p, "Task", null, assignedBy, DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            p.Tasks.AddToList(t, args);

            PtFollowup newFollowup = new PtFollowup(t);
            newFollowup.Disposition = "Omit From List";
            newFollowup.TypeOfFollowup = "Phone Call";
            newFollowup.Comment = "Do Not Call";
            newFollowup.Who = assignedBy;
            newFollowup.Date = DateTime.Now;

            args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            t.FollowUps.AddToList(newFollowup, args);

        }
    }
}

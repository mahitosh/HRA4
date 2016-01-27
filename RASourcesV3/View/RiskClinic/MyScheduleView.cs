using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using BrightIdeasSoftware;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Utilities;
using RiskApps3.View.Appointments;
using RiskApps3.View.Common;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.Risk;
using WeifenLuo.WinFormsUI.Docking;

namespace RiskApps3.View.RiskClinic
{
    public partial class MyScheduleView : HraView
    {
        /**************************************************************************************************/
        //   Members

        private AppointmentList appts = null;

        private PedigreeImageView pf;

        private RiskScoresView rsv;

        private PatientCommunicationView pcv;

        private bool trigger = false;

        public int defaultClinicId = 1;


        /**************************************************************************************************/

        public MyScheduleView(int initialFilterSelectionIndex, string initialNameOrMRN, bool filterToday)
        {
            
            InitializeComponent();
            if (string.IsNullOrEmpty(initialNameOrMRN) == false)
            {
                textBoxFilterData.Text = initialNameOrMRN;
            }
            checkBox1.Checked = filterToday;

            olvColumnCompleted.Renderer = new NullDateRenderer();
            _addEditApptController = new AddEditCopyApptController(this);
        }

        public MyScheduleView()
        {
            InitializeComponent();
            olvColumnCompleted.Renderer = new NullDateRenderer();
            _addEditApptController = new AddEditCopyApptController(this);
        }

        /**************************************************************************************************/

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof (PedigreeForm).ToString())
            {
                pf = new PedigreeImageView();
                return pf;
            }
            else if (persistString == typeof(RiskScoresView).ToString())
            {
                rsv = new RiskScoresView();
                return rsv;
            }
            else if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                pcv.PatientHeaderVisible = false;
                return pcv;
            }
            else
                return null;
        }

        private void UserGroupsLoaded(HraListLoadedEventArgs e)
        {
            //comboBox1.Items.Clear();
            //comboBox1.Items.Add(SessionManager.Instance.ActiveUser.ToString());

            //int numadded = 0;
            //foreach (UserGroupMembership group in SessionManager.Instance.MetaData.UserGroups)
            //{

            //    if (SessionManager.Instance.ActiveUser.userLogin == group.userLogin)
            //    {
            //        numadded++;
            //        comboBox1.Items.Add(group.userGroup);
            //    }
            //}
            //if (numadded > 1)
            //{
            //    comboBox1.Items.Add("All my groups");
            //}
            //comboBox1.Items.Add("Entire Clinic");
        }

        /**************************************************************************************************/

        private void HighRiskFollowupView_Load(object sender, EventArgs e)
        {
            trigger = false;
            object defaultClinic = null;
            foreach (Clinic c in SessionManager.Instance.ActiveUser.UserClinicList)
            {
                comboBox2.Items.Add(c);
                if (c.clinicID == defaultClinicId)
                {
                    defaultClinic = c;
                }
            }
            if (defaultClinic != null)
            {
                comboBox2.SelectedItem = defaultClinic;
            }
            if (SessionManager.Instance.ActiveUser.ModuleName == "HRAPedigreeModule")
            {
                checkBox1.Checked = false;
                newTask.Visible = false;
                notes.Visible = false;
                orders.Visible = false;
                copyApptButton.Visible = false;
                riskClinic.Text = "Clinic";
                button1.Text = "Delete Patient";
                addApptButton.Text = "Add Patient";
                editApptButton.Text = "Edit Patient";

                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.ColumnStyles[2].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
                tableLayoutPanel1.ColumnStyles[3].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;
                tableLayoutPanel1.ColumnStyles[4].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[4].Width = 0;

                fastDataListView1.Columns.Remove(olvColumn2);

                olvColumn10.DisplayIndex = 2;
            }
            else if (SessionManager.Instance.ActiveUser.ModuleName == "Pediatric")
            {
                newTask.Visible = false;
                notes.Visible = false;
                orders.Visible = false;

                tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[0].Width = 50;
                tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Percent;
                tableLayoutPanel1.ColumnStyles[1].Width = 50;
                tableLayoutPanel1.ColumnStyles[2].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[2].Width = 0;
                tableLayoutPanel1.ColumnStyles[3].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[3].Width = 0;
                tableLayoutPanel1.ColumnStyles[4].SizeType = SizeType.Absolute;
                tableLayoutPanel1.ColumnStyles[4].Width = 0;
                riskClinic.Text = "Pediatric Clinic";
            }
            else if (SessionManager.Instance.ActiveUser.ModuleName == "Breast Imaging")
            {
                riskClinic.Text = "Add/Edit Data";
            }

            fastDataListView1.Columns.Remove(olvColumn5);

            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;


            SessionManager.Instance.ClearActivePatient();

            SessionManager.Instance.NewActivePatient +=
                new SessionManager.NewActivePatientEventHandler(NewActivePatient);

            //SessionManager.Instance.MetaData.UserGroups.AddHandlersWithLoad(null, UserGroupsLoaded, null);

            string configFile = SessionManager.SelectDockConfig("MyPatientsView.config");

            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                pf = new PedigreeImageView();
                pf.Show(theDockPanel);
                pf.DockState = DockState.Document;

                //rsv = new RiskScoresView();
                //rsv.Show(theDockPanel);
                //rsv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                //pcv = new PatientCommunicationView();
                //pcv.Show(theDockPanel);
                //pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                
            }

            dateTimePicker1.Value = DateTime.Now;

            if (string.IsNullOrEmpty(SessionManager.Instance.UserGroup))
            {
                comboBox1.Text = SessionManager.Instance.ActiveUser.ToString();
            }
            else
            {
                comboBox1.Text = SessionManager.Instance.UserGroup;
            }

            CompositeAllFilter currentFilter = null;
            OlderApptFilter oaf = new OlderApptFilter();
            List<IModelFilter> listOfFilters = new List<IModelFilter>();
            listOfFilters.Add(oaf); //add the filter
            currentFilter = new CompositeAllFilter(listOfFilters);
            fastDataListView1.ModelFilter = currentFilter;


            trigger = true;
            GetNewAppointmentList();

            if (pf != null)
            {
                pf.Enabled = false;
                pf.Show();
            }
        }

        private void GetNewAppointmentList()
        {
            //checkBox2_CheckedChanged(this, null);

            //loadingCircle1.Enabled = true;
            //loadingCircle1.Visible = true;
            //loadingCircle1.Active = true;
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
                appts.clinicId = ((Clinic) comboBox2.SelectedItem).clinicID;
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
                Appointment theAppt = (Appointment) e.hraOperand;

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
                CompositeAllFilter currentFilter = (CompositeAllFilter) fastDataListView1.ModelFilter;
                currentFilter.Filters.Clear();
            }
            lock (appts)
            {
                //loadingCircle1.Enabled = false;
                //loadingCircle1.Visible = false;

                //CompositeAllFilter currentFilter = null;
                //OlderApptFilter oaf = new OlderApptFilter();
                //List<IModelFilter> listOfFilters = new List<IModelFilter>();
                //listOfFilters.Add(oaf);  //add the filter
                //currentFilter = new CompositeAllFilter(listOfFilters);
                //fastDataListView1.ModelFilter = currentFilter;


                Appointment a = ((Appointment) (fastDataListView1.SelectedObject));
                bool found = false;
                fastDataListView1.ClearObjects();
                fastDataListView1.SetObjects(appts);

                if (a != null)
                {
                    foreach (object o in fastDataListView1.Objects)
                    {
                        if (((Appointment) o).apptID == ((Appointment) a).apptID)
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

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            patientRecordHeader1.setPatient(e.newActivePatient);
            this.riskClinic.Enabled = true;
            this.pedigree.Enabled = true;
            this.notes.Enabled = true;
            this.newTask.Enabled = true;
            this.orders.Enabled = true;
        }

        private void SetStateApptSelected()
        {
            this.theDockPanel.Visible = true;
            this.NoApptSelectedLabel.Visible = false;
            Appointment appt = (Appointment) (fastDataListView1.SelectedObject);

            if (string.IsNullOrEmpty(appt.Unitnum) == false)
            {
                //if (SessionManager.Instance.GetActivePatient() == null)
                //{
                SessionManager.Instance.SetActivePatient(appt.Unitnum, -1);
                //}
                //else
                //{
                //    if (appt.unitnum != SessionManager.Instance.GetActivePatient().unitnum)
                //    {
                //        SessionManager.Instance.SetActivePatient(appt.unitnum, appt.apptID);
                //    }
                //}
            }
        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        public void updateStatusLine(ObjectListView olv)
        {
            label1.Text = fastDataListView1.Items.Count.ToString() + " Results";

            //IList objects = olv.Objects as IList;
            //if (olv.Items.Count != objects.Count)
            //{
            //    this.label1.Text =
            //        String.Format("Filtered list showing {0} patients of {1} patients",
            //                      olv.Items.Count,
            //                      objects.Count
            //                      );
            //}
            //else
            //{
            //    this.label1.Text = String.Format("{0} patients", olv.Items.Count);
            //}
        }


        private void buttonApplyTextSearch_Click(object sender, EventArgs e)
        {
            if (trigger)
                GetNewAppointmentList();
        }

        private void buttonClearTextSearch_Click(object sender, EventArgs e)
        {
            textBoxFilterData.Text = "";
        }

        private void HighRiskFollowupView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (importExportBackgroundWorker.IsBusy)
            {
                DialogResult result = MessageBox.Show("WARNING: Data import or export is in progress.  You may lose data. Continue?", 
                    "WARNING",  MessageBoxButtons.YesNo);

                if (result == DialogResult.No) 
                {
                    return; // Don't close this form.
                }
            }
            fastDataListView1.DataSource = null;

            string configFile = SessionManager.SelectDockConfig("MyPatientsView.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            SessionManager.Instance.MetaData.UserGroups.ReleaseListeners(this);
            SessionManager.Instance.RemoveHraView(this);

            patientRecordHeader1.ReleaseListeners();

            if (pf != null)
                pf.ViewClosing = true;

            if (pf != null)
                pf.Close();
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

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (trigger)
                GetNewAppointmentList();
        }

        public void RefreshPatientList()
        {
            GetNewAppointmentList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == SessionManager.Instance.ActiveUser.ToString())
            {
                SessionManager.Instance.UserGroup = "";
            }
            else
            {
                SessionManager.Instance.UserGroup = comboBox1.SelectedItem.ToString();
            }
            if (trigger)
                GetNewAppointmentList();
        }

        private void addApptButton_Click(object sender, EventArgs e)
        {
            int clinicId = ((Clinic) comboBox2.SelectedItem).clinicID;
            
            this._addEditApptController.AddAppt(clinicId);

            GetNewAppointmentList();
        }

        private void editApptButton_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment) (fastDataListView1.SelectedObject));
                int clinicId = ((Clinic)comboBox2.SelectedItem).clinicID;
                this._addEditApptController.EditAppt(clinicId, selectedAppt);
                
                GetNewAppointmentList();
            }
            else
            {
                this._addEditApptController.ShowNoAppointmentSelectedErrorMessage();
            }
        }

        private void copyApptButton_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment) (fastDataListView1.SelectedObject));
                int clinicId = ((Clinic) comboBox2.SelectedItem).clinicID;
                this._addEditApptController.CopyAppt(clinicId, selectedAppt);

                GetNewAppointmentList();
            }
            else
            {
                this._addEditApptController.ShowNoAppointmentSelectedErrorMessage();
            }
        }

        private void todayButton_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Text = DateTime.Now.ToString(LegacyCoreAPI.getShortDateFormatString());
        }

        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            //ensure that 'no appt selected label' is centered
            this.NoApptSelectedLabel.Center();
        }

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                SetStateApptSelected();
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, appt.GoldenAppt);
                        apptid = appt.GoldenAppt;
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                }
            }
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid, appt.Unitnum);
            msapf.ShowDialog();
            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof (RiskClinicFamilyHistoryView);
            PushViewStack(rcmf, DockState.Document);

        }

        public override void PoppedToFront()
        {
            pf.RedrawPedigree();
            RefreshPatientList();
        }
        private void button3_Click(object sender, EventArgs e)
        {

            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }
            Appointment appt = ((Appointment) (fastDataListView1.SelectedObject));
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
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, appt.GoldenAppt);
                        apptid = appt.GoldenAppt;
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                }
            }
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid, appt.Unitnum);
            msapf.ShowDialog();

            PedigreeForm pf = new PedigreeForm();
            PushViewStack(pf, DockState.Document);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment) (fastDataListView1.SelectedObject));
                string strMessage = "Are you sure you want to delete this appointment:\n\n";
                strMessage = strMessage + "Unit Number:\t" + selectedAppt.Unitnum +
                             "\n";
                strMessage = strMessage + "Patient Name:\t" + selectedAppt.PatientName +
                             "\n";
                strMessage = strMessage + "Date of Birth:\t" + selectedAppt.Dob +
                             "\n\n";
                strMessage = strMessage + "Deletions cannot be undone!";
                if (MessageBox.Show(strMessage, "riskApps™", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("apptID", selectedAppt.apptID);
                    pc.Add("keepApptRecord", 0);
                    pc.Add("purgeAuditData", 0);
                    pc.Add("userLoginID", SessionManager.Instance.ActiveUser.userLogin);
                    pc.Add("machineNameID", Environment.MachineName);
                    pc.Add("applicationID", "RiskApps3 - MyScheduleView");
                    BCDB2.Instance.RunSPWithParams("sp_deleteWebAppointment", pc);
                }

                GetNewAppointmentList();
            }
            else
            {
                MessageBox.Show("No appointment selected.", "RiskApps", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fastDataListView1_Click(object sender, EventArgs e)
        {
        }

        private void fastDataListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(fastDataListView1.PointToScreen(e.Location));
            }
        }

        private void importExportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment) (fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }
            appt.MarkIncomplete();
            MessageBox.Show("Appointment Marked Incomplete.");
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Application.DoEvents();

            CompositeAllFilter currentFilter = null;

            if (checkBox2.Checked)
            {
                //de-activate OlderApptFilter
                if (fastDataListView1.ModelFilter != null)
                {
                    currentFilter = (CompositeAllFilter) fastDataListView1.ModelFilter;
                    foreach (IModelFilter m in currentFilter.Filters)
                    {
                        if (m is OlderApptFilter)
                        {
                            currentFilter.Filters.Remove(m);
                            break;
                        }
                    }

                    if (currentFilter.Filters.Count == 0)
                    {
                        fastDataListView1.ModelFilter = null;
                        updateStatusLine(fastDataListView1);
                        return;
                    }
                }
            }
            else //activate OlderApptFilter
            {
                IModelFilter olderApptFilter = new OlderApptFilter();
                if (fastDataListView1.ModelFilter != null)
                {
                    currentFilter = (CompositeAllFilter) fastDataListView1.ModelFilter;
                    currentFilter = new CompositeAllFilter(currentFilter.Filters as List<IModelFilter>);
                    currentFilter.Filters.Add(olderApptFilter);
                }
                else
                {
                    List<IModelFilter> listOfFilters = new List<IModelFilter>();
                    listOfFilters.Add(olderApptFilter); //add the filter
                    currentFilter = new CompositeAllFilter(listOfFilters);
                }
            }

            fastDataListView1.ModelFilter = currentFilter;

            updateStatusLine(fastDataListView1);
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (trigger)
                GetNewAppointmentList();
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                Appointment selectedAppt = ((Appointment) (fastDataListView1.SelectedObject));
                int clinicId = ((Clinic) comboBox2.SelectedItem).clinicID;
                this._addEditApptController.EditAppt(clinicId, selectedAppt);
                GetNewAppointmentList();
            }
            else
            {
                this._addEditApptController.ShowNoAppointmentSelectedErrorMessage();
            }
        }

        private void markCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment) (fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }
            appt.MarkComplete();
            MessageBox.Show("Appointment Marked Complete.");
        }

        private void markForAutomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Appointment appt = ((Appointment) (fastDataListView1.SelectedObject));
            if (appt == null)
            {
                return;
            }
            appt.MarkComplete();
            RunRiskModelsDialog rsmd = new RunRiskModelsDialog(true);
            rsmd.ShowDialog();
            //Appointment.MarkForAutomation(appt.apptID);
            MessageBox.Show("Risk data recalculated and automation documents run (if any.)", "Automation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Appointment.MarkForAutomation(appt.apptID);
            //MessageBox.Show("Appointment Marked for Automation.");
        }

        /*
         * ***********************************************************************************************************
         * * Import/Export HL7 and XML
         * ***********************************************************************************************************
         */
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
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, appt.GoldenAppt);
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
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

        /***************************************************************/
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
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
                    }
                    else
                    {
                        SessionManager.Instance.ClearActivePatient();
                        SessionManager.Instance.SetActivePatient(appt.Unitnum, appt.GoldenAppt);
                    }
                }
                else
                {
                    SessionManager.Instance.ClearActivePatient();
                    SessionManager.Instance.SetActivePatient(appt.Unitnum, apptid);
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
                catch(Exception ee)
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
                catch(Exception eeeee)
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

        /***************************************************************/
        
        private bool unitnumExistsInDB(string unitnum)
        {
            bool exists = false;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_CheckUnitnum", pc);

            if (reader.Read())
            {
                if (reader.IsDBNull(0) == false)
                {
                    exists = reader.GetBoolean(0);
                }
            }
            return exists;
        }

        private int xmlImport(string fileName, int apptid, string existingUnitnum)
        {
            return FamilyHistory.XmlImport(fileName, apptid, existingUnitnum);
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

        /***************************************************************/
        private bool testSGDoc(string fileName)
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

        private void disableButtons()
        {
            //this.ControlBox = false;
            //fastDataListView1.Enabled = false;
            contextMenuStrip1.Enabled = false;
            button1.Enabled = false;
            addApptButton.Enabled = false;
            editApptButton.Enabled = false;
            progressBar1.Enabled = true;
            progressBar1.Visible = true;
        }
        ImportExportArgs args;
        private readonly AddEditCopyApptController _addEditApptController;

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
                            if (string.IsNullOrEmpty(args.patient.name)==false)
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

        private void importExportBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void importExportBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.RefreshPatientList();
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

        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    string configFile = SessionManager.SelectDockConfig("MyPatientsView.config");
            
        //    theDockPanel.SaveAsXml(configFile);
        //}

        /*
         * ***********************************************************************************************************
         * * End Import/Export HL7 and XML
         * ***********************************************************************************************************
         */




    }

}

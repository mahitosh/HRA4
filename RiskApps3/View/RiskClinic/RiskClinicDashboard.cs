using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.Clinic.Dashboard;
using System.Diagnostics;
using RiskApps3.View.Admin;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Utilities;

namespace RiskApps3.View.RiskClinic
{
    public partial class RiskClinicDashboard : HraView
    {
        /**************************************************************************************************/
        //private RiskApps3.Model.Clinic.Dashboard.QueueData p_UpdateQueueData;
        
        private RiskApps3.Model.Clinic.Dashboard.MyPatients p_MyPatients;
        private RiskApps3.Model.Clinic.Dashboard.AtRisk p_AtRisk;

        private RiskApps3.Model.Clinic.Dashboard.HighRiskBrcaQueue p_HighRiskBrcaQueue;
        private RiskApps3.Model.Clinic.Dashboard.HighRiskLifetimeBreastQueue p_HighRiskLifetimeBreastQueue;
        private RiskApps3.Model.Clinic.Dashboard.HighRiskColonQueue p_HighRiskColonQueue;
        private RiskApps3.Model.Clinic.Dashboard.BrcaPositiveQueue p_BrcaPositiveQueue;
        private RiskApps3.Model.Clinic.Dashboard.PendingTaskQueue p_PendingTaskQueue;
        private RiskApps3.Model.Clinic.Dashboard.myPatientsQueue p_MyPatientsQueue;
        

        private RiskApps3.Model.Clinic.Dashboard.PendingTasks p_PendingTasks;
        private RiskApps3.Model.Clinic.Dashboard.PendingGeneticTests p_PendingGeneticTests;

        private static System.Drawing.Color defaultBitmapButtonInnerBorderColor = Color.LightGray;
        private static System.Drawing.Color defaultBitmapButtonInnerBorderColor_MouseOver = Color.Gold;

        /**************************************************************************************************/


        public RiskClinicDashboard()
        {
            p_MyPatients = new RiskApps3.Model.Clinic.Dashboard.MyPatients();
            p_MyPatients.date = DateTime.Now.ToShortDateString();
            p_AtRisk = new RiskApps3.Model.Clinic.Dashboard.AtRisk();
            //p_UpdateQueueData = new RiskApps3.Model.Clinic.Dashboard.QueueData();

            p_HighRiskBrcaQueue = new HighRiskBrcaQueue();
            p_HighRiskLifetimeBreastQueue = new HighRiskLifetimeBreastQueue();
            p_HighRiskColonQueue = new HighRiskColonQueue();
            p_BrcaPositiveQueue = new BrcaPositiveQueue();
            p_PendingTaskQueue = new PendingTaskQueue();
            p_MyPatientsQueue = new myPatientsQueue();

            p_PendingTasks = new RiskApps3.Model.Clinic.Dashboard.PendingTasks();
            p_PendingGeneticTests = new RiskApps3.Model.Clinic.Dashboard.PendingGeneticTests();
            InitializeComponent();
        }

        private void UserGroupsLoaded(HraListLoadedEventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add(SessionManager.Instance.ActiveUser.ToString());
            
            int numadded = 0;
            foreach (UserGroupMembership group in SessionManager.Instance.MetaData.UserGroups)
            {
                
                if (SessionManager.Instance.ActiveUser.userLogin == group.userLogin)
                {
                    numadded++;
                    comboBox2.Items.Add(group.userGroup);
                }
            }
            if (numadded > 1)
            {
                comboBox2.Items.Add("All my groups");
            }
            comboBox2.Text = "Entire Clinic";
            comboBox2.Items.Add("Entire Clinic");
        }


        /**************************************************************************************************/
        private void RiskClinicDashboard_Load(object sender, EventArgs e)
        {
            label19.Text = DateTime.Now.Day.ToString();
            if (label19.Text.Length == 1)
            {
                label19.Location = new Point(label19.Location.X + 5, label19.Location.Y);
            }
           
            RiskApps3.Utilities.RegistrationService.logUserAccess();
            
            foreach (Clinic c in SessionManager.Instance.ActiveUser.userClinicList)
            {
                comboBox1.Items.Add(c);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                p_MyPatients.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_HighRiskBrcaQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_HighRiskColonQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_HighRiskLifetimeBreastQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_PendingTasks.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_PendingTaskQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_PendingGeneticTests.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_BrcaPositiveQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_AtRisk.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                p_MyPatientsQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            }

            p_AtRisk.AddHandlersWithLoad(null, atRiskLoaded, null);            

            p_HighRiskBrcaQueue.AddHandlersWithLoad(null, HighRiskBrcaQueueLoaded, null);
            p_HighRiskLifetimeBreastQueue.AddHandlersWithLoad(null, HighRiskLifetimeBreastQueueLoaded, null);
            p_HighRiskColonQueue.AddHandlersWithLoad(null, HighRiskColonQueueLoaded, null);
            p_BrcaPositiveQueue.AddHandlersWithLoad(null, BrcaPositiveQueueLoaded, null);
            p_PendingTaskQueue.AddHandlersWithLoad(null, PendingTaskQueueLoaded, null);

            p_PendingTasks.AddHandlersWithLoad(null, myPendingTasksCountsLoaded, null);
            p_PendingGeneticTests.AddHandlersWithLoad(null, myPendingGeneticTestsCountsLoaded, null);

            p_MyPatients.AddHandlersWithLoad(null, myPatientsLoaded, null);
            p_MyPatientsQueue.AddHandlersWithLoad(null, myPatientsQueueLoaded, null);
            
            //SessionManager.Instance.MetaData.UserGroups.AddHandlersWithLoad(null, UserGroupsLoaded, null);
            //p_UpdateQueueData.AddHandlersWithLoad(null, myUpdateQueueDataLoaded, null);
        }
        /**************************************************************************************************/
        private void HighRiskBrcaQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void BeingFollowedQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void HighRiskLifetimeBreastQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void HighRiskColonQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void BrcaPositiveQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void PendingTaskQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
 
        /**************************************************************************************************/
        private void atRiskLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            SetAtRiskData();
        }

        /**************************************************************************************************/
        private void SetAtRiskData()
        {
            NumAtRiskBreast.Text = p_AtRisk.NumAtRiskBreast.ToString();
            NumAtRiskColon.Text = p_AtRisk.NumAtRiskColon.ToString();
            NumMaxLifetimeBreastGE20.Text = p_AtRisk.NumMaxLifetimeBreastGE20.ToString();
            NumBrcaPosFamilies.Text = p_AtRisk.NumBrcaPosFamilies.ToString();
            NumBrcaPosRelativesTestable.Text = p_AtRisk.NumBrcaPosRelativesTestable.ToString();
            NumPrintQueue.Text = p_AtRisk.PrintQueueCount.ToString();
            myPatientsCountLabel.Text = p_AtRisk.NumPatients.ToString();
        }

        /**************************************************************************************************/
        private void myPatientsLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            SetMyPatientData();

        }

        /**************************************************************************************************/
        private void myPendingTasksCountsLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            TotalPendingTasks.Text = p_PendingTasks.NumPendingTasks.ToString();
            YourPendingTasks.Text = p_PendingTasks.NumAssignedToYou.ToString(); 
        }

        /**************************************************************************************************/
        private void myPatientsQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            //myPatientsCountLabel.Text = p_MyPatientsQueue
        }
        /**************************************************************************************************/
        private void myPendingGeneticTestsCountsLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            TotalPendingGeneticTests.Text = p_PendingGeneticTests.NumPendingGeneticTests.ToString();
        }
        /**************************************************************************************************/
        private void SetMyPatientData()
        {
            NumPatientsToday.Text = p_MyPatients.NumPatientsToday.ToString();
            NumNewPatientsToday.Text = p_MyPatients.NumNewPatientsToday.ToString();
            NumFollowUpPtsToday.Text = p_MyPatients.NumFollowUpPtsToday.ToString();
            NumPatientsTotal.Text = p_MyPatients.NumPatientsTotal.ToString();
        }

        public override void PoppedToFront()
        {
            RefreshDashboard();
        }
        /**************************************************************************************************/
        private void bitmapButton1_Click(object sender, EventArgs e)
        {
            MyScheduleView myrcp = new MyScheduleView(0, null, true);
            myrcp.defaultClinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            myrcp.PushViewStack = PushViewStack;
            PushViewStack(myrcp, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            p_AtRisk.ProviderName = comboBox2.SelectedItem.ToString();

            if (comboBox2.Text == SessionManager.Instance.ActiveUser.ToString())
            {
                p_MyPatients.groupName = "";
                SessionManager.Instance.UserGroup = "";
            }
            else
            {
                p_MyPatients.groupName = comboBox2.SelectedItem.ToString();
                SessionManager.Instance.UserGroup = comboBox2.SelectedItem.ToString();
            }

            comboBox2.Text = "loading...";
            p_AtRisk.LoadObject();
            p_MyPatients.LoadObject();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (backgroundWorker2.IsBusy == false)
            {
                buttonRefresh.Text = "Calculating...";
                backgroundWorker2.RunWorkerAsync();
            }
        }
        private void bitmapButtonBreastRisk_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskBrcaQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void bitmapButtonBreastLifetime_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskLifetimeBreastQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void bitmapButtonColonRisk_Click(object sender, EventArgs e)
        {
            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_HighRiskColonQueue);
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonBreastRisk.Refresh();
        }
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonBreastRisk.Refresh();
        }

        private void NumAtRiskBreast_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonBreastRisk.Refresh();
        }
        private void NumAtRiskBreast_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonBreastRisk.Refresh();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk_Click(sender, e);
        }

        private void NumAtRiskBreast_Click(object sender, EventArgs e)
        {
            bitmapButtonBreastRisk_Click(sender, e);
        }

        private void label9_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonBreastLifetime.Refresh();
        }
        private void label9_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonBreastLifetime.Refresh();
        }

        private void NumMaxLifetimeBreastGE20_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonBreastLifetime.Refresh();
        }
        private void NumMaxLifetimeBreastGE20_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonBreastLifetime.Refresh();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime_Click(sender, e);
        }

        private void NumMaxLifetimeBreastGE20_Click(object sender, EventArgs e)
        {
            bitmapButtonBreastLifetime_Click(sender, e);
        }


        private void label8_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonColonRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonColonRisk.Refresh();
        }
        private void label8_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonColonRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonColonRisk.Refresh();
        }
        private void NumAtRiskColon_MouseEnter(object sender, EventArgs e)
        {
            bitmapButtonColonRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor_MouseOver;
            bitmapButtonColonRisk.Refresh();
        }
        private void NumAtRiskColon_MouseLeave(object sender, EventArgs e)
        {
            bitmapButtonColonRisk.InnerBorderColor = defaultBitmapButtonInnerBorderColor;
            bitmapButtonColonRisk.Refresh();
        }
        private void label8_Click(object sender, EventArgs e)
        {
            bitmapButtonColonRisk_Click(sender, e);
        }

        private void NumAtRiskColon_Click(object sender, EventArgs e)
        {
            bitmapButtonColonRisk_Click(sender, e);
        }


        //patients followed
        private void bitmapButton4_Click(object sender, EventArgs e)
        {
            using (var form = new PrintQueueForm())
            {
                form.ShowDialog(this);
            }

            RefreshDashboard();
        }

        private void bitmapButton5_Click(object sender, EventArgs e)
        {
            //HighRiskFollowupView hrfv = new HighRiskFollowupView(p_BrcaPositiveQueue); //brca pos families
            ////hrfv.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            //hrfv.WindowState = FormWindowState.Maximized;
            //hrfv.ShowDialog();
            //RefreshDashboard();

            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_BrcaPositiveQueue); //brca pos families
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }

        private void label10_Click(object sender, EventArgs e)
        {
            bitmapButton5_Click(this, new EventArgs());
        }

        private void NumBrcaPosFamilies_Click(object sender, EventArgs e)
        {
            bitmapButton5_Click(this, new EventArgs());
        }

        private void label11_Click(object sender, EventArgs e)
        {
            bitmapButton5_Click(this, new EventArgs());
        }

        private void NumBrcaPosRelativesTestable_Click(object sender, EventArgs e)
        {
            bitmapButton5_Click(this, new EventArgs());
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D)) //Ctrl-D toggles Developer mode
            {
                //if (LastQueueUpdate.Visible)
                //{
                //    LastQueueUpdate.Visible = false;
                //}
                //else
                //{
                //    LastQueueUpdate.Visible = true;
                //}

                //if (buttonRefreshTables.Visible)
                //{
                //    buttonRefreshTables.Visible = false;
                //}
                //else
                //{
                //    buttonRefreshTables.Visible = true;
                //}

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void adminButton_Click(object sender, EventArgs e)
        {
            AdminMainForm adminMainForm = new AdminMainForm();
            adminMainForm.WindowState = FormWindowState.Maximized;
            adminMainForm.ShowDialog();
        }

        private void bitmapButton2_Click(object sender, EventArgs e)
        {
            PendingToDoView ptdv = new PendingToDoView(p_PendingTaskQueue);
            ptdv.PushViewStack = PushViewStack;
            PushViewStack(ptdv, WeifenLuo.WinFormsUI.Docking.DockState.Document);

            //ptdv.WindowState = FormWindowState.Maximized;
            //ptdv.ShowDialog();
            //RefreshDashboard();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainFormLoading fml = new MainFormLoading();
            fml.UseNtAuthentication = false;
            fml.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.ClearActivePatient();
        }

        private void NumPatientsFollowed_Click(object sender, EventArgs e)
        {
            bitmapButton4_Click(sender, e);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == false)
            {
                MyScheduleView myrcp = new MyScheduleView(0, textBox1.Text, false);
                myrcp.defaultClinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                myrcp.PushViewStack = PushViewStack;
                PushViewStack(myrcp, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            else
            {
                MessageBox.Show("Please enter a name or MRN to search for.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            backgroundWorker1.RunWorkerAsync();
        }


        private void RiskClinicDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.MetaData.UserGroups.ReleaseListeners(this);
        }

        private void bitmapButton3_Click_1(object sender, EventArgs e)
        {
            //PendingGeneticTestsView pgtv = new PendingGeneticTestsView();
            //pgtv.clinicID = ((Clinic)comboBox1.SelectedItem).clinicID;
            //pgtv.WindowState = FormWindowState.Maximized;
            //pgtv.ShowDialog();
            //RefreshDashboard();

            PendingGeneticTestsView pgtv = new PendingGeneticTestsView();
            pgtv.PushViewStack = PushViewStack;
            PushViewStack(pgtv, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }
        public void RefreshMyPatients()
        {
            p_MyPatients.LoadObject();
        }
        public void RefreshDashboard()
        {
            Cursor = Cursors.WaitCursor;

            p_HighRiskBrcaQueue.LoadObject();
            p_HighRiskLifetimeBreastQueue.LoadObject();
            p_HighRiskColonQueue.LoadObject();
            p_BrcaPositiveQueue.LoadObject();
            p_PendingTaskQueue.LoadObject();
            p_MyPatientsQueue.LoadObject();

            p_AtRisk.LoadObject();
            p_MyPatients.LoadObject();
            p_PendingTasks.LoadObject();
            p_PendingGeneticTests.LoadObject();
            Cursor = Cursors.Default;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BCDB2.Instance.ExecuteNonQuery("EXEC sp_3_populateBigQueue");
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshDashboard();
            buttonRefresh.Text = "Refresh";
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    MyScheduleView myrcp = new MyScheduleView(0, textBox1.Text, false);
                    myrcp.defaultClinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
                    myrcp.PushViewStack = PushViewStack;
                    PushViewStack(myrcp, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                }
                else
                {
                    MessageBox.Show("Please enter a name or MRN to search for.");
                }
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            NumPatientsToday.Text = "Loading...";
            NumNewPatientsToday.Text = "Loading...";
            NumFollowUpPtsToday.Text = "Loading...";
            NumPatientsTotal.Text = "Loading...";
            p_MyPatients.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;

            p_MyPatients.LoadObject();

            p_HighRiskBrcaQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_HighRiskBrcaQueue.LoadObject();

            p_HighRiskColonQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_HighRiskColonQueue.LoadObject();

            p_AtRisk.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_AtRisk.LoadObject();

            p_HighRiskLifetimeBreastQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_HighRiskLifetimeBreastQueue.LoadObject();

            p_PendingTasks.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_PendingTasks.LoadObject();

            p_PendingTaskQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_PendingTaskQueue.LoadObject();

            p_PendingGeneticTests.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_PendingGeneticTests.LoadObject();

            p_BrcaPositiveQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_BrcaPositiveQueue.LoadObject();

            p_MyPatientsQueue.clinicId = ((Clinic)comboBox1.SelectedItem).clinicID;
            p_MyPatientsQueue.LoadObject();
        }

        private void buttonRefreshTables_Click(object sender, EventArgs e)
        {
            //force refresh of tables from Server via Rest API
            RefreshTables.updateTables(true);
        }

        private void myPatientsLabel_Click(object sender, EventArgs e)
        {
            myPatientsBitmapButton_Click(sender, e);
        }

        private void myPatientsBitmapButton_Click(object sender, EventArgs e)
        {
            //HighRiskFollowupView hrfmf = new HighRiskFollowupView(p_MyPatientsQueue);
            //hrfmf.WindowState = FormWindowState.Maximized;
            //hrfmf.ShowDialog();
            //RefreshDashboard();

            HighRiskFollowupView hrfv = new HighRiskFollowupView(p_MyPatientsQueue); 
            hrfv.PushViewStack = PushViewStack;
            PushViewStack(hrfv, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void myPatientsCountLabel_Click(object sender, EventArgs e)
        {
            myPatientsBitmapButton_Click(sender, e);
        }

        private void label19_Click(object sender, EventArgs e)
        {
            bitmapButton1_Click(sender, e);
        }

    }
}

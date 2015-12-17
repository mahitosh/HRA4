using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Utilities;
using System.Xml;
using System.Xml.Serialization;
using RiskApps3.Model.PatientRecord;
using System.Runtime.Serialization;
using System.IO;
using RiskApps3.Model.Clinic;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;
using System.Collections;

namespace RiskApps3.View.Appointments
{
    public partial class DeleteTestAppointmentsPopup : Form
    {
        TestAppointmentList appts;

        public DeleteTestAppointmentsPopup()
        {
            
            appts = new TestAppointmentList();

            InitializeComponent();

            progressLabel.Text = "Fetching Test Appointments...";
            progressLabel.Visible = true;
            progressBar1.Enabled = true;
            objectListView1.Enabled = false;
            deleteButton.Enabled = false;
            refreshButton.Enabled = false;

            objectListView1.ShowGroups = false;


            fetchBackgroundWorker.RunWorkerAsync();

        }

        private void DeleteTestAppointmentsPopup_Load(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            if (fetchBackgroundWorker.IsBusy == false && deleteBackgroundWorker.IsBusy == false)
            {
                refreshButton.Enabled = false;

                objectListView1.ClearObjects();

                progressLabel.Text = "Fetching Test Appointments...";
                progressLabel.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Enabled = true;
                deleteButton.Enabled = false; 
                button1.Enabled = false;
                fetchBackgroundWorker.RunWorkerAsync();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int count = objectListView1.CheckedObjects.Count;
            if (fetchBackgroundWorker.IsBusy == false && deleteBackgroundWorker.IsBusy == false && count > 0)
            {
                if (MessageBox.Show("Are you sure you want to permanently delete " + count.ToString() + " record(s)?") == System.Windows.Forms.DialogResult.OK)
                {
                    refreshButton.Enabled = false;
                    objectListView1.Enabled = false;
                    progressLabel.Text = "Deleting Selected Appointments...";
                    progressLabel.Visible = true;
                    progressBar1.Visible = true;
                    progressBar1.Enabled = true; 
                    button1.Enabled = false;
                    deleteBackgroundWorker.RunWorkerAsync(objectListView1.CheckedObjects);
                }
            }
        }

        private void fetchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            appts.BackgroundListLoad();
            foreach (TestAppointment appt in appts)
            {
                fetchBackgroundWorker.ReportProgress(0, appt);
            }
        }

        private void fetchBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                objectListView1.AddObject(e.UserState);
            }
        }

        private void fetchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressLabel.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Enabled = false;
            refreshButton.Enabled = true; 
            deleteButton.Enabled = true;
            button1.Enabled = true;
            objectListView1.Enabled = true;
            updateCountLabel(); 
        }

        private void deleteBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Collections.ArrayList appts = (System.Collections.ArrayList)(e.Argument);
            foreach (object o in appts)
            {
                TestAppointment to = (TestAppointment)o;
                Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
                pc.Add("apptID", to.apptID);
                BCDB2.Instance.RunSPWithParams("sp_deleteWebAppointment", pc);
            }

        }

        private void deleteBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshList();
        }


        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {
            objectListView1.CheckedObjects = objectListView1.SelectedObjects;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Collections.ArrayList appts = (System.Collections.ArrayList)(e.Argument);
            foreach (object o in appts)
            {
                TestAppointment to = (TestAppointment)o;
                Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
                pc.Add("apptID", to.apptID);
                BCDB2.Instance.RunSPWithParams("sp_AddTestPatientExclusion", pc);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            refreshList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = objectListView1.CheckedObjects.Count;
            if (backgroundWorker1.IsBusy == false && count > 0)
            { 
                refreshButton.Enabled = false;
                objectListView1.Enabled = false;
                progressLabel.Text = "Marking Selected Appointments...";
                progressLabel.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Enabled = true; 
                button1.Enabled = false;
                backgroundWorker1.RunWorkerAsync(objectListView1.CheckedObjects);
      
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objectListView1.SelectObjects(appts); 
        }

        private void objectListView1_Filter(object sender, BrightIdeasSoftware.FilterEventArgs e)
        {
            updateCountLabel();
        }

        private void updateCountLabel()
        {

            
        }
    }
}

namespace RiskApps3.Model.PatientRecord
{
    public class TestAppointmentList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public TestAppointmentList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs("sp_3_LoadTestPatients",
                                                pc,
                                                typeof(TestAppointment),
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

namespace RiskApps3.Model.Clinic
{
    public class TestAppointment : HraObject
    {
        public int apptID;
        public string unitnum;
        public string patientName;
        public string DOB;
        public DateTime apptdatetime;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Diagnostics;
using RiskAppUtils;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class NewToDoView : HraView
    {
        private Patient proband;

        public NewToDoView()
        {
            InitializeComponent();
        }
        private void NewFYI_Click(object sender, EventArgs e)
        {
            Task t = new Task(proband, "FYI", null, SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }

        private void NewPatientFollowup_Click(object sender, EventArgs e)
        {
            string assignedBy = "";
            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();
                }
            }

            Task t = new Task(proband, "Patient Followup", null, assignedBy, DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }
        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            proband.Tasks.AddHandlersWithLoad(null,
                                         TaskListLoaded,
                                         null);
        }
        private void TaskListLoaded(HraListLoadedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            flowLayoutPanel1.Enabled = false;

            if (proband != null)
                proband.ReleaseListeners(this);

            proband = e.newActivePatient;
            proband.AddHandlersWithLoad(null, activePatientLoaded, null);
        }

        private void NewNote_Click(object sender, EventArgs e)
        {
            Task t = new Task(proband, "Note", null, SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Task t = new Task(proband, "Task", "Pending", SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }

        private void NewToDoView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += NewActivePatient;

            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                proband.AddHandlersWithLoad(null, activePatientLoaded, null);
            }
        }

        private void NewToDoView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            //Process process = new Process();
            //process.StartInfo.FileName = "RiskAppUtils.exe";
            //process.StartInfo.Arguments = "PrintDocuments " + proband.apptid.ToString();
            //process.Start();
            //process.WaitForExit();
            PrintDocumentsForm pf = new PrintDocumentsForm();
            RiskAppCore.Globals.setApptID(proband.apptid);
            pf.ShowDialog();
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    
        //}

        private void NewDocument_Click(object sender, EventArgs e)
        {
               if (backgroundWorker2.IsBusy == false)
                    backgroundWorker2.RunWorkerAsync();
        }

        private void NewOrders_Click(object sender, EventArgs e)
        {

        }




    }
}

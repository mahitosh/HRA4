using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class TaskView : HraView
    {
        public TaskView(Task t)
        {
            InitializeComponent();
            this.Task = t;
        }

        public Task Task
        {
            get
            {
                return this.TaskUserControl.Task;
            }
            set
            {
                this.TaskUserControl.Task = value;
            }
        }

        public bool PatientHeaderVisible
        {
            get
            {
                return patientRecordHeader1.Visible;
            }
            set
            {
                patientRecordHeader1.Visible = value;
                if (value)
                {
                    this.TaskUserControl.Location = new Point(this.TaskUserControl.Location.X, this.TaskUserControl.Location.Y + patientRecordHeader1.Height);
                }
                else
                {
                    this.TaskUserControl.Location = new Point(this.TaskUserControl.Location.X, this.TaskUserControl.Location.Y - patientRecordHeader1.Height);
                }
            }
        }

        private void TaskView_Load(object sender, EventArgs e)
        {
            Patient proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                if (patientRecordHeader1.Visible)
                {
                    patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
                }
            }
            this.Text = Task.Task_Type.Replace("To Do","Task") + " - " + Task.Task_Date.ToShortDateString();
        }


        /**************************************************************************************************/
        private void FollowupListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                PtFollowup theFollowup = (PtFollowup)e.hraOperand;
            }
        }

        /**************************************************************************************************/

        private void TaskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();

            patientRecordHeader1.ReleaseListeners();
            SessionManager.Instance.RemoveHraView(this);
        }

        private void TaskView_VisibleChanged(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}

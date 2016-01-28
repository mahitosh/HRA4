using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.View.RiskClinic;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class PendingTaskSummaryRow : UserControl
    {
        Task task;


        public PendingTaskSummaryRow(Task t)
        {
            this.task = t;
            InitializeComponent();

            FillControls();

        }

        private void FillControls()
        {
            label2.Text = task.Task_Type.Replace(" ","\n");
            label1.Text = task.Task_Date.ToShortDateString();


            switch (task.Task_Type)
            {
                case "Task":
                    orientedTextLabel1.Text = task.Task_Status.Replace(" ", "\n");
                    label4.Text = "Assigned To " + task.Task_AssignedTo + " by " + task.Task_AssignedBy;

                    break;
                case "Note":
                    orientedTextLabel1.Visible = false;
                    label4.Text = "Authored by " + task.Task_AssignedBy;

                    break;
                case "FYI":
                    orientedTextLabel1.Visible = false;
                    label4.Text = "Authored by " + task.Task_AssignedBy;

                    break;
                case "Patient Followup":
                    orientedTextLabel1.Visible = false;
                    label4.Text = "Contacted by " + task.Task_AssignedBy;

                    break;

                default:
                    break;
            }

            if (task.Task_Text != null)
            {
                if (task.Task_Text.Length > 97)
                {
                    label3.Text = task.Task_Text.Substring(0, 97) + "...";
                }
                else
                {
                    label3.Text = task.Task_Text;
                }
            }
            else
                label3.Text = "";

            if (task.Task_Status == "Pending")
            {
                orientedTextLabel1.ForeColor = Color.Red;
            }
        }
        public Task GetTask()
        {
            return task;
        }

        internal void SetTask(Task task)
        {
            FillControls();
        }

        private void PendingTaskSummaryRow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //PatientCommunicationView pcv = new PatientCommunicationView();
            //pcv.InitialTask = task;
            //pcv.ShowDialog();

         //   QueueNotesView qnv = new QueueNotesView();
         //   qnv.InitialTask = task;
        //    qnv.ShowDialog();

            RiskClinicNotesView riskClinicNotesView = new RiskClinicNotesView();
            riskClinicNotesView.InitialTask = task;
            riskClinicNotesView.ShowDialog();
        }
    }
}

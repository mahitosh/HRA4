using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Model;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class PendingTasksSummaryView : HraView
    {
        private Patient proband;
        private TaskList tasks;

        public PendingTasksSummaryView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void PendingTasksSummaryView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += NewActivePatient;

            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
                proband.AddHandlersWithLoad(null, activePatientLoaded, null);
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            //ClearControls();

            if (proband != null)
                proband.ReleaseListeners(this);

            if (e.newActivePatient != null)
            {
                proband = e.newActivePatient;
                proband.AddHandlersWithLoad(null, activePatientLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (tasks != null)
                tasks.ReleaseListeners(this);

            tasks = proband.Tasks;
            tasks.AddHandlersWithLoad(TaskListChanged,
                                      TaskListLoaded,
                                      TaskChanged);
        }
        /**************************************************************************************************/
        private void TaskChanged(object sender, HraModelChangedEventArgs e)
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (((PendingTaskSummaryRow)c).GetTask() == sender)
                {
                    ((PendingTaskSummaryRow)c).SetTask((Task)sender);
                }
            }
        }
        /**************************************************************************************************/
        private void TaskListLoaded(HraListLoadedEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            tasks.Sort( Task.CompareTasksByReverseDate);
            foreach (Task t in tasks)
            {
                PendingTaskSummaryRow ptsr = new PendingTaskSummaryRow(t);
                ptsr.Width = flowLayoutPanel1.Width - 10;
                flowLayoutPanel1.Controls.Add(ptsr);
            }
        }
        /**************************************************************************************************/
        private void TaskListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Task t = (Task)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        PendingTaskSummaryRow ptsr = new PendingTaskSummaryRow(t);
                        ptsr.Width = flowLayoutPanel1.Width - 10;
                        flowLayoutPanel1.Controls.Add(ptsr);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        Control doomedptsr = null;
                        foreach (Control c in flowLayoutPanel1.Controls)
                        {
                            if (((PendingTaskSummaryRow)c).GetTask() == t)
                            {
                                doomedptsr = c;
                            }
                        }
                        if (doomedptsr != null)
                            flowLayoutPanel1.Controls.Remove(doomedptsr);
                        break;
                }
            }
        }
        /**************************************************************************************************/
        private void PendingTasksSummaryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proband != null)
            {
                this.proband.ReleaseListeners(this);
            }
            if (this.tasks != null)
            {
                this.tasks.ReleaseListeners(this);
            }
            SessionManager.Instance.RemoveHraView(this);
        }
    }
}

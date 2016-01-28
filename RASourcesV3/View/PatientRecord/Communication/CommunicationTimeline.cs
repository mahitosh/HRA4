using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
using dotnetCHARTING.WinForms;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class CommunicationTimeline : HraView
    {
        private Patient proband;
        private TaskList tasks;
        List<PtFollowup> timelineData = new List<PtFollowup>();

        public CommunicationTimeline()
        {
            InitializeComponent();
            
        }

        private void CommunicationTimeline_Load(object sender, EventArgs e)
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

            proband = e.newActivePatient;
            proband.AddHandlersWithLoad(null, activePatientLoaded, null);
        }
        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (tasks != null)
                tasks.ReleaseListeners(this);

            tasks = proband.Tasks;
            tasks.AddHandlersWithLoad(TaskListChanged,
                                      TaskListLoaded,
                                      null);
        }

        /**************************************************************************************************/
        private void TaskListLoaded(HraListLoadedEventArgs e)
        {
            
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
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }        /**************************************************************************************************/
        private void FollowupListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Task t = (Task)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        foreach (PtFollowup f in t.FollowUps)
                        {
                            timelineData.Add(f);
                        }
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void FollowupListLoaded(RunWorkerCompletedEventArgs e)
        {
            timeLineChart1.SeriesCollection.Clear();
            timelineData.Clear();

            foreach (Task t in tasks)
            {
                foreach (PtFollowup f in t.FollowUps)
                {
                    timelineData.Add(f);
                }
            }
            if (timelineData.Count == 0)
            {
                timeLineChart1.Visible = false;
            }
            else
            {
                timeLineChart1.Visible = true;
                SeriesCollection sc = new SeriesCollection();
                foreach (PtFollowup theFollowup in timelineData)
                {
                    timeLineChart1.Series.Element = new dotnetCHARTING.WinForms.Element(theFollowup.TypeOfFollowup, theFollowup.Date, 2, 2);
                    timeLineChart1.Series.Elements.Add();
                }
                timeLineChart1.SeriesCollection.Add();
                timeLineChart1.Refresh();
            }
        }

        private void FollowupChanged(object sender, HraModelChangedEventArgs e)
        {
            //update the appropriate row with the new
        }

        private void CommunicationTimeline_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }
    }
}

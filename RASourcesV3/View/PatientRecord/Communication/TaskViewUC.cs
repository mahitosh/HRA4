using System;
using System.ComponentModel;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using System.Drawing;
using RiskApps3.Utilities;
using System.Linq;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class TaskViewUC : UserControl
    {
        private Task task;

        #region this is just to appease the designer
        public void SetTask(Task task)
        {
            this.task = task;
        }

        public TaskViewUC()
        {
            InitializeComponent();
        }
        #endregion

        public TaskViewUC(Task t)
        {
            task = t;
            InitializeComponent();
        }

        public Task Task
        {
            get
            {
                return this.task;
            }
            set
            {
                this.task = value;
                this.FillControls();
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            //there may be a bug in the framework that causes us to have to do this
            //search for UserPreferenceChangedEventHandler memory leak
            this.taskDatePicker.Dispose();
            this.DueDatePicker.Dispose();
            this.LongDescriptionBox.Dispose();
            
            base.Dispose(disposing);
        }

        /**************************************************************************************************/

        private void FollowupListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                PtFollowup theFollowup = (PtFollowup) e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        PtFollowupRow ptr = new PtFollowupRow(theFollowup);
                        ptr.Width = FollowupFlowPanel.Width - 30;
                        FollowupFlowPanel.Controls.Add(ptr);
                        FollowupFlowPanel.Controls.SetChildIndex(ptr, 0);
                        //SetSplitterDist(ptr.Height + ptr.Margin.Top); 
                        
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        Control doomed = null;
                        foreach (Control row in FollowupFlowPanel.Controls)
                        {
                            PtFollowupRow r = (PtFollowupRow) row;
                            if (r.GetFollowup() == theFollowup)
                            {
                                doomed = row;
                            }
                        }
                        if (doomed != null)
                        {
                            FollowupFlowPanel.Controls.Remove(doomed);
                            splitContainer1.SplitterDistance -= doomed.Height;
                        }
                        break;
                }
            }
        }

        /**************************************************************************************************/

        private void FollowupListLoaded(HraListLoadedEventArgs e)
        {
            //if (task.FollowUps.Count == 0)
            //{
            //    if (task.Task_Type == "Patient Followup" || task.Task_Type == "To Do")
            //    {
            //        PtFollowup newFollowup = new PtFollowup(task);
            //        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            //        args.Persist = false;
            //        task.FollowUps.AddToList(newFollowup, args);
            //    }
            //}
            //else
            //{
                foreach (PtFollowup theFollowup in task.FollowUps.OrderBy(f => ((PtFollowup)f).Date))
                {
                    PtFollowupRow ptr = new PtFollowupRow(theFollowup);
                    ptr.SetScrollState(false);
                    ptr.Width = FollowupFlowPanel.Width - 30;
                    FollowupFlowPanel.Controls.Add(ptr);
                   
                }
            //}
        }


        private void FollowupChanged(object sender, HraModelChangedEventArgs e)
        {
            //update the appropriate row with the new
        }

        private void TypeCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (TypeCombo.Visible)
                task.Task_Type = TypeCombo.SelectedItem.ToString();
        }

        private void StatusCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (StatusCombo.Visible)
                task.Task_Status = StatusCombo.SelectedItem.ToString();
        }

        private void AssignedByCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AssignedByCombo.Visible)
                task.Task_AssignedBy = AssignedByCombo.SelectedItem.ToString();
        }

        private void AssignedToCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (AssignedToCombo.Visible)
                task.Task_AssignedTo = AssignedToCombo.SelectedItem.ToString();
        }

        private void ActionCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (ActionCombo.Visible)
                task.Task_Action = ActionCombo.SelectedItem.ToString();
        }

        private void LongDescriptionBox_Validated(object sender, EventArgs e)
        {
            if (LongDescriptionBox.Visible)
                task.Task_Text = LongDescriptionBox.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PtFollowup newFollowup = new PtFollowup(task);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = false;
            task.FollowUps.AddToList(newFollowup, args);
        }

        private void taskDatePicker_Validating(object sender, EventArgs e)
        {
            if (taskDatePicker.Visible)
                task.Task_Date = taskDatePicker.Value;
        }

        private void DueDatePicker_Validating(object sender, EventArgs e)
        {
            if (DueDatePicker.Visible)
                task.Task_Duedate = DueDatePicker.Value;
        }

        private void release()
        {
            ValidateChildren();
            task.ReleaseListeners(this);
            task.FollowUps.ReleaseListeners(this);
        }

        public void Release()
        {
            task.ReleaseListeners(this);
            task.FollowUps.ReleaseListeners(this);
        }

        public void FillControls()
        {
            if (this.task != null)
            {
                this.Text = task.Task_Type;
                TypeCombo.Text = task.Task_Type;
                ActionCombo.Text = task.Task_Action;
                AssignedByCombo.Text = task.Task_AssignedBy;
                AssignedToCombo.Text = task.Task_AssignedTo;
                if (task.Task_Duedate > DateTime.MinValue)
                {
                    DueDatePicker.Value = task.Task_Duedate;
                }
                if (task.Task_Date > DateTime.MinValue)
                {
                    taskDatePicker.Value = task.Task_Date;
                }
                StatusCombo.Text = task.Task_Status;

                LongDescriptionBox.Text = task.Task_Text;

                task.FollowUps.AddHandlersWithLoad(FollowupListChanged,
                                             FollowupListLoaded,
                                             FollowupChanged);

                foreach (User u in SessionManager.Instance.MetaData.Users)
                {
                    AssignedToCombo.Items.Add(u);
                    AssignedByCombo.Items.Add(u);
                }

                //panel5.Visible = false;
                if (task.Task_Type == "Patient Followup")
                {
                    groupBox2.Visible = false;
                    splitContainer1.Location = groupBox2.Location;
                    splitContainer1.Height += groupBox2.Height;

                    panel6.Visible = false;
                    panel4.Visible = false;
                    panel3.Visible = false;
                    label3.Text = "Contacted By";
                }
                if (task.Task_Type == "Note" || task.Task_Type == "FYI")
                {
                    //assignedTo
                    panel3.Visible = false;

                    label3.Text = "Author";

                    groupBox1.Visible = false;

                    panel4.Visible = false;
                    panel6.Visible = false;

                    try
                    {
                        splitContainer1.SplitterDistance = 1;
                        splitContainer1.IsSplitterFixed = true;
                    }
                    catch { }

                }

                this.Text = task.Task_Type + " - " + task.Task_Date.ToShortDateString();
            }
        }

        private void FollowupFlowPanel_Resize(object sender, EventArgs e)
        {
            this.FollowupFlowPanel.Width = this.groupBox1.Width - 10;

            foreach (Control c in this.FollowupFlowPanel.Controls)
            {
                c.Width = this.groupBox1.Width - 12;
            }
        }

        private void LongDescriptionBox_Leave(object sender, EventArgs e)
        {

        }
    }
}

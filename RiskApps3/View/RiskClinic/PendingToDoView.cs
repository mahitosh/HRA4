using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using BrightIdeasSoftware;
using System.Collections;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.Clinic.Dashboard;
using RiskApps3.View.PatientRecord.Pedigree;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.View.Common;
using RiskApps3.Controllers;
using System.Diagnostics;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;

namespace RiskApps3.View.RiskClinic
{
    public partial class PendingToDoView : HraView
    {
        /**************************************************************************************************/
        //   Members
        //
        private RiskApps3.Model.Clinic.Dashboard.Queue theQueue;        //working version

        public string InitialSearch;

        /**************************************************************************************************/

        public PendingToDoView(RiskApps3.Model.Clinic.Dashboard.Queue queue)
        {
            theQueue = queue;
            InitializeComponent();
        }

        private void ActivePatientTaskListChanged(HraListChangedEventArgs e)
        {
            Task t = (Task)e.hraOperand;

            switch (e.hraListChangeType)
            {
                case HraListChangedEventArgs.HraListChangeType.ADD:
                    if (t.Task_Type == "Task" && t.Task_Status == "Pending")
                    {
                        AddTab(t);
                    }
                    break;
                case HraListChangedEventArgs.HraListChangeType.DELETE:
                    
                    RemoveTabPage(t.taskID);
                    
                    break;
            }
        }
        delegate void RemoveTabPageCallback(int taskID);
        private void RemoveTabPage(int taskID)
        {
            if (tabControl.InvokeRequired)
            {
                RemoveTabPageCallback aoc = new RemoveTabPageCallback(RemoveTabPage);
                object[] args = new object[1];
                args[0] = taskID;
                this.Invoke(aoc, args);
            }
            else
            {
                TabPage tabPage2 = null;
                foreach (TabPage tp in tabControl.TabPages)
                {
                    if (tp.Tag != null)
                        if (tp.Tag is int)
                            if ((int)(tp.Tag) == taskID)
                                tabPage2 = tp;
                }
                if (tabPage2 != null)
                {
                    if (tabControl.TabPages.Contains(tabPage2))
                        tabControl.TabPages.Remove(tabPage2);

                    if (tabControl.TabPages.Count == 0)
                    {
                        fastDataListView1.RemoveObject(fastDataListView1.SelectedObject);
                    }
                }
            }
        }


        delegate void UpdateTabPageCallback(int taskID);
        private void UpdateTabPage(int taskID)
        {
            if (tabControl.InvokeRequired)
            {
                UpdateTabPageCallback aoc = new UpdateTabPageCallback(UpdateTabPage);
                object[] args = new object[1];
                args[0] = taskID;
                this.Invoke(aoc, args);
            }
            else
            {
                TabPage tabPage2 = null;
                foreach (TabPage tp in tabControl.TabPages)
                {
                    if (tp.Tag != null)
                        if (tp.Tag is int)
                            if ((int)(tp.Tag) == taskID)
                                tabPage2 = tp;
                }
                if (tabPage2 != null)
                {
                    if (tabPage2.Controls.Count > 0)
                        if (tabPage2.Controls[0] is TaskViewUC)
                            ((TaskViewUC)(tabPage2.Controls[0])).FillControls();
                }
            }
        }

        private void ActivePatientTasksLoaded(HraListLoadedEventArgs e)
        {
            Patient p = SessionManager.Instance.GetActivePatient();
            if (p!=null)
            {
                TaskList tasks = p.Tasks;
                if (tasks!=null)
                {
                    foreach (Task task in tasks
                        .Where(t => 
                            ((Task)t).Task_Type == "Task" && 
                            ((Task)t).Task_Status == "Pending")
                        .OrderByDescending(t => ((Task)t).Task_Date))
                    {
                        AddTab(task);
                    }  
                }
            }
            fastDataListView1.Focus();
        }

        private void AddTab(Task task)
        {
            TaskViewUC tabContent = new TaskViewUC();
            tabContent.Task = task;
            tabContent.Enabled = false;

            TabPage tabPage = new TabPage();
            tabPage.Text = task.ToString();
            tabPage.Tag = task.taskID;
            tabPage.Controls.Add(tabContent);

            Control c = (Control)tabContent;
            c.Dock = DockStyle.Fill;

           
            this.tabControl.Controls.Add(tabPage);

        }

        /**************************************************************************************************/
        private void PendingToDoView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.ClearActivePatient();
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            fastDataListView1.Visible = false;
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            theQueue.AddHandlersWithLoad(null, QueueDataLoaded, null);            

            if (!(string.IsNullOrEmpty(InitialSearch)))
            {
                textBoxFilterData.Text = InitialSearch;
                TimedFilter(fastDataListView1, InitialSearch);
            }
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            ClearExistingNotes();

            patientRecordHeader1.setPatient(e.newActivePatient);
            
            SessionManager.Instance.GetActivePatient().Tasks.AddHandlersWithLoad(
                ActivePatientTaskListChanged,
                ActivePatientTasksLoaded,
                ActivePatientTasksChanged);
        }

        private void ClearExistingNotes()
        {
            this.tabControl.Controls.Clear();
        }
                
        /**************************************************************************************************/
        private void ActivePatientTasksChanged(object sender, HraModelChangedEventArgs e)
        {
            Task t = (Task)sender;
            if (t != null)
            {
                if (string.IsNullOrEmpty(t.Task_Status) == false)
                {
                    if (t.Task_Status != "Pending")
                    {
                        RemoveTabPage(t.taskID);
                    }
                    else
                    {
                        UpdateTabPage(t.taskID);
                    }
                }
            }
        }
        /**************************************************************************************************/
        private void QueueDataLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshListView();
            fastDataListView1.Visible = true;
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
        }

        private void RefreshListView()
        {
            if (fastDataListView1.DataSource == null)
            {
                FillControls();
            }
            else
            {
                fastDataListView1.DataSource = SetBindingSource();
                fastDataListView1.Refresh();
            }
        }

        private object SetBindingSource()
        {
            BindingSource source = new BindingSource(theQueue.dt, "");
            return source;
        }
        private void RefreshQueue()
        {
            if (SessionManager.Instance.GetActivePatient() != null)
            {
                fastDataListView1.Visible = false;
                loadingCircle1.Enabled = true;
                loadingCircle1.Visible = true;
                QueueData.UpdateBigQueueByMrn(SessionManager.Instance.GetActivePatient().unitnum);
                theQueue.LoadObject();
            }
        }
        /**************************************************************************************************/
        private void FillControls()
        {
            fastDataListView1.DataSource = SetBindingSource();

            //initialize the row count
            ListViewDataSetSelectedIndexChanged(fastDataListView1, new System.EventArgs());

            if (theQueue.dt.Rows.Count > 0)
            {
                fastDataListView1.SelectedIndex = 0;
            }
            foreach (OLVColumn c in fastDataListView1.AllColumns)
            {
                    int w = c.Width;
                    c.Width = -2;
                    if (c.Width < w)
                        c.Width = w;
            }
        }

        private void EnableControls()
        {
            this.NoApptSelectedLabel.Visible = false;

            this.button1.Enabled = true;
            this.button2.Enabled = true;
            this.button3.Enabled = true;
            this.button4.Enabled = true;
            this.button5.Enabled = true;
        }

        private void DisableControls()
        {
            this.NoApptSelectedLabel.Visible = true;

            //this.button1.Enabled = false;
            this.button2.Enabled = false;
            //this.button3.Enabled = false;
            this.button4.Enabled = false;
            this.button5.Enabled = false;       
        }

        /**************************************************************************************************/
        void ListViewDataSetSelectedIndexChanged(object sender, System.EventArgs e)
        {
            BrightIdeasSoftware.FastDataListView listView = (FastDataListView)sender;
            DataRowView row = (DataRowView)listView.SelectedObject;
            if (row == null)
            {
                this.DisableControls();
                this.label1.Text = String.Format("{0} patients", listView.Items.Count);
            }
            else
            {
                this.EnableControls();

                string unitnum = row["unitnum"].ToString();
                string msg = "'" + row["patientname"] + "'";
                this.label1.Text = String.Format("Selected {0} from {1} patients", msg, listView.Items.Count);
                if (string.IsNullOrEmpty(unitnum) == false)
                {

                    if (SessionManager.Instance.GetActivePatient() == null)
                    {
                        ActivateSelection(unitnum);
                    }
                    else
                    {
                        if (unitnum != SessionManager.Instance.GetActivePatient().unitnum)
                        {
                            SessionManager.Instance.GetActivePatient().ReleaseListeners(this);
                            ActivateSelection(unitnum);
                        }
                    }
                }
            }
        }

        private void ActivateSelection(string p)
        {
            Patient current = SessionManager.Instance.GetActivePatient();
            if (current != null)
            {
                current.ReleaseListeners(this);
                if (current.Tasks != null)
                {
                    current.Tasks.ReleaseListeners(this);
                }
            }

            SessionManager.Instance.SetActivePatient(p,-1);
        }

        /**************************************************************************************************/
        private void fastDataListView1_FormatRow(object sender, FormatRowEventArgs e)
        {
            //e.Item.SubItems[1].Text = (e.DisplayIndex + 1).ToString() + ": ";
        }

       
        public void removeAllNotTextFilters()
        {
            this.label1.Text = "thinking...";

            CompositeAllFilter currentFilter = null;

            //de-activate any existing *non* text match filters
            if (fastDataListView1.ModelFilter != null)
            {
                currentFilter = (CompositeAllFilter)fastDataListView1.ModelFilter;
                foreach (IModelFilter m in currentFilter.Filters)
                {
                    if (!(m is TextMatchFilter))
                    {
                        currentFilter.Filters.Remove(m);
                        break;
                    }
                }

                if (currentFilter.Filters.Count == 0)
                {
                    fastDataListView1.ModelFilter = null;
                }
            }

            fastDataListView1.ModelFilter = currentFilter;
            //GetColumnsFromlkpBigQueueColumnMetaData();
            //buttonRestoreState_Click(this, new EventArgs());
            updateStatusLine(fastDataListView1);
        }
 
        public void updateStatusLine(ObjectListView olv)
        {
            IList objects = olv.Objects as IList;
            if (olv.Items.Count != objects.Count)
            {
                this.label1.Text =
                    String.Format("Filtered list showing {0} patients of {1} patients",
                                  olv.Items.Count,
                                  objects.Count
                                  );
            }
            else
            {
                this.label1.Text = String.Format("{0} patients", olv.Items.Count);
            }
        }
        

        public class GroupQueueFilter : IModelFilter
        {
            private PatientsByGroup map;

            public GroupQueueFilter(PatientsByGroup map)
            {
                this.map = map;
            }

            public bool Filter(object x)
            {
                String mrn = ((DataRowView)x).Row["mrn"].ToString();
                return map.Contains(mrn);
            }
        }


        public class PendingTasksQueueFilter : IModelFilter
        {
            public bool Filter(object x)
            {
                if (((DataRowView)x).Row["PendingTaskCount"] is int)
                {
                    if ((int)((DataRowView)x).Row["PendingTaskCount"] > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
        }

        
        void TimedFilter(ObjectListView olv, string txt)
        {
            TextMatchFilter filter = null;
            if (!String.IsNullOrEmpty(txt))
            {
                filter = TextMatchFilter.Contains(olv, txt);
            }
            // Setup a default renderer to draw the filter matches
            if (filter == null)
                olv.DefaultRenderer = null;
            else
            {
                HighlightTextRenderer htr = new HighlightTextRenderer(filter);
                htr.FillBrush = Brushes.AliceBlue;
                olv.DefaultRenderer = htr; //new HighlightTextRenderer(filter);

                // Uncomment this line to see how the GDI+ rendering looks
                //olv.DefaultRenderer = new HighlightTextRenderer { Filter = filter, UseGdiTextRendering = false };
            }

            // Some lists have renderers already installed
            HighlightTextRenderer highlightingRenderer = olv.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
                highlightingRenderer.Filter = filter;

            CompositeAllFilter currentFilter = null;

            if (filter != null)
            {
                // Get the existing model filters, if any, remove any existing TextMatchFilters,
                // then add the new TextMatchFilter
                if (olv.ModelFilter == null)  // easy, just add the new one
                {
                    List<IModelFilter> listOfFilters = new List<IModelFilter>();
                    listOfFilters.Add(filter);  //add the TextMatchFilter
                    CompositeAllFilter compositeFilter = new CompositeAllFilter(listOfFilters);
                    olv.ModelFilter = compositeFilter;
                }
                else  //need to remove existing TextMatchFilters, if any, than add the new one
                {
                    currentFilter = (CompositeAllFilter)olv.ModelFilter;
                    //find the first existing TextMatchFilter (should be at most one) and remove it
                    foreach (IModelFilter m in currentFilter.Filters)
                    {
                        if (m is TextMatchFilter)
                        {
                            currentFilter.Filters.Remove(m);
                            break;
                        }
                    }

                    //add the new TextMatchFilter
                    if (olv.ModelFilter != null)
                    {
                        (olv.ModelFilter as CompositeAllFilter).Filters.Add(filter);
                    }
                    else
                    {
                        List<IModelFilter> listOfFilters = new List<IModelFilter>();
                        listOfFilters.Add(filter);  //add the TextMatchFilter
                        CompositeAllFilter compositeFilter = new CompositeAllFilter(listOfFilters);
                        olv.ModelFilter = compositeFilter;
                    }
                }
            }
            else //remove text filter
            {
                if (olv.ModelFilter != null)
                {
                    currentFilter = (CompositeAllFilter)olv.ModelFilter;
                    //find and remove the first existing TextMatchFilter if any
                    foreach (IModelFilter m in currentFilter.Filters)
                    {
                        if (m is TextMatchFilter)
                        {
                            currentFilter.Filters.Remove(m);
                            break;
                        }
                    }
                    if (currentFilter.Filters.Count == 0)
                    {
                        fastDataListView1.ModelFilter = null;
                    }
                }
            }

            if (currentFilter != null)
                fastDataListView1.ModelFilter = currentFilter;

            updateStatusLine(fastDataListView1);
        }

        private void ShrinkListView()
        {
            this.fastDataListView1.Size = 
                new Size(
                    this.fastDataListView1.Size.Width, 
                    this.fastDataListView1.Size.Height - 50);
            this.fastDataListView1.Location = 
                new Point(
                    this.fastDataListView1.Location.X,
                    this.fastDataListView1.Location.Y + 50);
        }

        private void ExpandListView()
        {
            this.fastDataListView1.Size =
                new Size(
                    this.fastDataListView1.Size.Width,
                    this.fastDataListView1.Size.Height + 50);
            this.fastDataListView1.Location =
                new Point(
                    this.fastDataListView1.Location.X,
                    this.fastDataListView1.Location.Y - 50);
        }

        private void buttonApplyTextSearch_Click(object sender, EventArgs e)
        {
            TimedFilter(fastDataListView1, textBoxFilterData.Text);
        }

        private void buttonClearTextSearch_Click(object sender, EventArgs e)
        {
            textBoxFilterData.Text = "";
            TimedFilter(fastDataListView1, textBoxFilterData.Text);
        }

        private void HighRiskFollowupView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.MetaData.UserGroups.ReleaseListeners(this);
            patientRecordHeader1.ReleaseListeners();
            theQueue.ReleaseListeners(this);

            Patient p = SessionManager.Instance.GetActivePatient();
            if(p!=null)
            {
                p.Tasks.ReleaseListeners(this);
            }
            fastDataListView1.Clear();
            SessionManager.Instance.RemoveHraView(this);
        }

        private OLVColumnMetaData GetOLVColumnMetaDataTableRow(DataRow dr)
        {
            OLVColumnMetaData oOLVMD = new OLVColumnMetaData();
            oOLVMD.columnHeader = dr["columnHeader"].ToString();
            oOLVMD.columnWidth = (int)dr["columnWidth"];
            oOLVMD.columnDisplayOrder = (int)dr["columnDisplayOrder"];
            return oOLVMD;
        }

        public class OLVColumnMetaData
        {
            private String strColumnHeader = "";
            private int intColumnWidth = 0;
            private int intColumnDisplayOrder = -1;

            public String columnHeader
            {
                get { return strColumnHeader; }
                set { strColumnHeader = value; }
            }
            public int columnWidth
            {
                get { return intColumnWidth; }
                set { intColumnWidth = value; }
            }
            public int columnDisplayOrder
            {
                get { return intColumnDisplayOrder; }
                set { intColumnDisplayOrder = value; }
            }
        }



        void Panel2_Resize(object sender, System.EventArgs e)
        {
            this.NoApptSelectedLabel.Center();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (fastDataListView1.SelectedObject == null)
            //{
            //    MessageBox.Show("Please select a patient first.", "WARNING");
            //    return;
            //}
            
            //RiskClinicMainForm rcmf = new RiskClinicMainForm();
            //rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);
            //rcmf.WindowState = FormWindowState.Maximized;
            //rcmf.ShowDialog();
            //RefreshQueue();


            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            int apptid = SessionManager.Instance.GetActivePatient().apptid;
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid);
            msapf.ShowDialog();

            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(PatientCommunicationView);

            PushViewStack(rcmf, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }

        public override void PoppedToFront()
        {
            RefreshQueue();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            int apptid = SessionManager.Instance.GetActivePatient().apptid;
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid);
            msapf.ShowDialog();

            PedigreeForm pf = new PedigreeForm();
            PushViewStack(pf, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PatientCommunicationView pcv = new PatientCommunicationView();
            pcv.Orientation = System.Windows.Forms.Orientation.Vertical;
            pcv.ShowDialog();
            RefreshQueue();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {
                p.Tasks.AddHandlersWithLoad(null, TaskListLoadedForPopup, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void TaskListLoadedForPopup(HraListLoadedEventArgs e)
        {
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {

                Task t = new Task(p, "Task", "Pending", SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                p.Tasks.AddToList(t, args);

                TaskView tv = new TaskView(t);
                tv.ShowDialog();
                p.Tasks.ReleaseListeners(this);
                RefreshQueue();
            }
        }
    }
}





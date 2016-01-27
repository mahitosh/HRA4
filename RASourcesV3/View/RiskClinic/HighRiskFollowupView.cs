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
using RiskApps3.View.PatientRecord;

namespace RiskApps3.View.RiskClinic
{
    public partial class HighRiskFollowupView : HraView
    {
        /**************************************************************************************************/
        //   Members
        //

        public int clinicId = -1;

        bool trigger = false;

        private RiskApps3.Model.Clinic.Dashboard.Queue theQueue = null;        //working version
        //private bool useLookupMap = false;  //to filter or not
        //private PatientsByGroup lookupMap;  //used to filter qd

        public OLVColumn brcaProScoreCol = null;

        private PedigreeImageView pf;
        private PedigreeModuleNavigation pnm;
        private SummaryFollowupView sfv;
        private PatientCommunicationView pcv;
        /**************************************************************************************************/

        public HighRiskFollowupView(RiskApps3.Model.Clinic.Dashboard.Queue queueData)
        {
            theQueue = queueData;
            //this.lookupMap = new PatientsByGroup(SessionManager.Instance.ActiveUser.userLogin, null);
            InitializeComponent();

            if (theQueue is myPatientsQueue)
            {
                toolStrip1.Visible = false;
            }
        }


        /**************************************************************************************************/
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (theQueue is BeingFollowedQueue)
            {
                if (persistString == typeof(SummaryFollowupView).ToString())
                {
                    sfv = new SummaryFollowupView();
                    return sfv;
                }
                else
                    return null;
            }
            else
            {
                if (persistString == typeof(PedigreeForm).ToString())
                {
                    pf = new PedigreeImageView();
                   // pf.SetMode("MANUAL");
                    return pf;
                }
                else
                    return null;
            }
        }

        /**************************************************************************************************/
        private void HighRiskFollowupView_Load(object sender, EventArgs e)
        {
            trigger = false;

            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;
            SessionManager.Instance.ClearActivePatient();
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

            string configFile = SessionManager.SelectDockConfig("HighRiskFollowupView.config");
            
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                if (theQueue is BeingFollowedQueue)
                {
                    sfv = new SummaryFollowupView();
                    sfv.Show(theDockPanel);
                    sfv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                }
                else
                {
                    pf = new PedigreeImageView();
                    if (theQueue is BrcaPositiveQueue)  //show brca scores on pedigree when BrcaPos families
                    {
                        pf.showBrcaScores = true;
                    }
                    pf.Show(theDockPanel);
                    pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                }
            }

            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            theQueue.HraState = HraObject.States.Null;
            theQueue.AddHandlersWithLoad(null, QueueDataLoaded, null);

            if (pf != null)
            {
                pf.Enabled = false;
            }

            trigger = true;
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            patientRecordHeader1.setPatient(e.newActivePatient);
        }
        /**************************************************************************************************/
        private void QueueDataLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshListView();
            TimedFilter(fastDataListView1, textBoxFilterData.Text);

            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
            fastDataListView1.Visible = true;
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
                fastDataListView1.BuildList();
            }
            updateStatusLine(fastDataListView1);
        }

        private object SetBindingSource()
        {
            DataTable dt = filterDataTable();
            BindingSource source = new BindingSource(dt, ""); // (theQueue.dt, "");
            return source;
        }

        
        /**************************************************************************************************/
        private void FillControls()
        {
            DataTable dt = BCDB2.Instance.getDataTable(@"EXEC sp_3_GetValuesFromlkpBigQueueColumnMetaData @filterName=N'" + theQueue.QueueName + "'");

            List<OLVColumnMetaData> columnMetaDataList ; //= new List<OLVColumnMetaData>();
            columnMetaDataList = new List<OLVColumnMetaData>(
                           (from dRow in dt.AsEnumerable()
                            select (GetOLVColumnMetaDataTableRow(dRow)))
                           );


            //create special empty first column
            //so that we can avoid the extra space in front of every value
            BrightIdeasSoftware.OLVColumn olvc = new BrightIdeasSoftware.OLVColumn();
            fastDataListView1.AllColumns.Add(olvc);
            System.Windows.Forms.ColumnHeader[] colHeaderArray = new System.Windows.Forms.ColumnHeader[] { olvc };
            fastDataListView1.Columns.AddRange(colHeaderArray);
            olvc.Text = "firstCol";
            olvc.MinimumWidth = 0;
            olvc.MaximumWidth = 0;

            foreach (DataColumn c in theQueue.dt.Columns)
            {
                Application.DoEvents();
                String colName = c.ColumnName;
                OLVColumnMetaData metadataColumn = null;
                foreach (OLVColumnMetaData mdc in columnMetaDataList)
                {
                    if (string.Compare(mdc.columnHeader,colName,true)==0)
                        metadataColumn = mdc;
                }

                if (metadataColumn != null)
                {
                    olvc = new BrightIdeasSoftware.OLVColumn();
                    olvc.Tag = metadataColumn;
                    fastDataListView1.AllColumns.Add(olvc);
                    colHeaderArray = new System.Windows.Forms.ColumnHeader[] { olvc };
                    fastDataListView1.Columns.AddRange(colHeaderArray);
                    olvc.AspectName = colName;
                    olvc.Text = colName;
                }
                else //if (currentFilterSelectionIndex == 0)
                {
                    olvc = new BrightIdeasSoftware.OLVColumn();
                    fastDataListView1.AllColumns.Add(olvc);
                    colHeaderArray = new System.Windows.Forms.ColumnHeader[] { olvc };
                    fastDataListView1.Columns.AddRange(colHeaderArray);
                    olvc.AspectName = colName;
                    olvc.Text = colName;
                    olvc.IsVisible = false;
                }
            }

            fastDataListView1.RebuildColumns();

            fastDataListView1.UseAlternatingBackColors = true;
            fastDataListView1.AlternateRowBackColor = Color.WhiteSmoke;
            fastDataListView1.AllColumns[0].Width = 0;



            int count = 0;
            foreach (OLVColumn columnToSize in fastDataListView1.AllColumns)
            {
                Application.DoEvents();
                columnToSize.Width = -2;
                count++;

                try
                {
                    if (columnToSize.Tag != null)
                    {
                        OLVColumnMetaData md = (OLVColumnMetaData)columnToSize.Tag;
                        if (columnToSize.Width < md.columnWidth)
                        {
                            columnToSize.Width = md.columnWidth;
                        }
                        if (md.columnDisplayOrder < fastDataListView1.AllColumns.Count)
                        {
                            columnToSize.DisplayIndex = md.columnDisplayOrder;
                        }
                        if (string.IsNullOrEmpty(md.displayText) == false)
                        {
                            columnToSize.Text = md.displayText;
                        }
                        if (string.IsNullOrEmpty(md.AspectToStringFormat) == false)
                        {
                            columnToSize.AspectToStringFormat = md.AspectToStringFormat;
                        }
                    }
                }
                catch(Exception excp)
                {
                    Logger.Instance.WriteToLog(excp.ToString());
                }
            }

                        fastDataListView1.DataSource = SetBindingSource();

            //initialize the row count
            ListViewDataSetSelectedIndexChanged(fastDataListView1, new System.EventArgs());

            //relabel the tab
            this.Text = theQueue.QueueText;

            if (theQueue.dt.Rows.Count > 0)
            {
                fastDataListView1.SelectedIndex = 0;
            }
            else
            {
                SessionManager.Instance.ClearActivePatient();
            }
        }

        /**************************************************************************************************/
        void ListViewDataSetSelectedIndexChanged(object sender, System.EventArgs e)
        {
            BrightIdeasSoftware.FastDataListView listView = (FastDataListView)sender;
            DataRowView row = (DataRowView)listView.SelectedObject;
            if (row == null)
            {
                this.NoApptSelectedLabel.Visible = true;

                this.label1.Text = String.Format("{0} patients", listView.Items.Count);
            }
            else
            {
                this.NoApptSelectedLabel.Visible = false;

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
            SessionManager.Instance.SetActivePatient(p,-1);
        }

       

        public void updateStatusLine(ObjectListView olv)
        {
            IList objects = olv.Objects as IList;
            int rows = theQueue.dt.Rows.Count;

            this.label1.Text = String.Format("{0} patients", olv.Items.Count);
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
            if (pf != null)
                pf.Close();
            if (pnm != null)
                pnm.Close();
            if (sfv != null)
                sfv.Close();
            if (pcv != null)
                pcv.Close();

            patientRecordHeader1.ReleaseListeners();

            string configFile = SessionManager.SelectDockConfig("HighRiskFollowupView.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            SessionManager.Instance.MetaData.UserGroups.ReleaseListeners(this);
            theQueue.ReleaseListeners(this);
            SessionManager.Instance.RemoveHraView(this);

            fastDataListView1.Clear();
        }

        private OLVColumnMetaData GetOLVColumnMetaDataTableRow(DataRow dr)
        {
            OLVColumnMetaData oOLVMD = new OLVColumnMetaData();
            oOLVMD.columnHeader = dr["columnHeader"].ToString();
            oOLVMD.columnWidth = (int)dr["columnWidth"];
            oOLVMD.columnDisplayOrder = (int)dr["columnDisplayOrder"];
            oOLVMD.AspectToStringFormat = dr["AspectToStringFormat"].ToString();
            oOLVMD.displayText = dr["displayText"].ToString();
            return oOLVMD;
        }

        public class OLVColumnMetaData
        {
            private String strColumnHeader = "";
            private int intColumnWidth = 0;
            private int intColumnDisplayOrder = -1;
            public string displayText = "";
            public string AspectToStringFormat = "";

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

        private void RefreshQueue()
        {
            if (SessionManager.Instance.GetActivePatient() != null)
            {
                fastDataListView1.Visible = false;
                loadingCircle1.Enabled = true;
                loadingCircle1.Visible = true;
                //QueueData.UpdateBigQueueByMrn(SessionManager.Instance.GetActivePatient().unitnum);
                theQueue.LoadObject();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            int apptid = SessionManager.Instance.GetActivePatient().apptid;
            string unit = SessionManager.Instance.GetActivePatient().unitnum;
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid, unit);
            msapf.ShowDialog();

            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);

            PushViewStack(rcmf, WeifenLuo.WinFormsUI.Docking.DockState.Document);

        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject == null)
            {
                MessageBox.Show("Please select a patient first.", "WARNING");
                return;
            }

            int apptid = SessionManager.Instance.GetActivePatient().apptid;
            string unit = SessionManager.Instance.GetActivePatient().unitnum;
            MarkStartedAndPullForwardForm msapf = new MarkStartedAndPullForwardForm(apptid, unit);
            msapf.ShowDialog();

            PedigreeForm pf = new PedigreeForm();
            PushViewStack(pf, WeifenLuo.WinFormsUI.Docking.DockState.Document);

            //if (fastDataListView1.SelectedObject == null)
            //{
            //    MessageBox.Show("Please select a patient first.", "WARNING");
            //    return;
            //}
            
            //PedigreeForm pf = new PedigreeForm();
            //pf.WindowState = FormWindowState.Maximized;
            //pf.ShowDialog();
            //RefreshQueue();
        }

        public override void PoppedToFront()
        {
            pf.RedrawPedigree();
            RefreshQueue();
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
                p.Tasks.AddHandlersWithLoad(null, TaskListLoaded, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);
            rcmf.WindowState = FormWindowState.Maximized;
            rcmf.SetInitialView(RiskClinicMainForm.TargetView.Orders);
            rcmf.ShowDialog();
            RefreshQueue();
        }

        private void TaskListLoaded(HraListLoadedEventArgs e)
        {
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {
                string assignedBy = "";
                if (SessionManager.Instance.ActiveUser != null)
                {
                    if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                    {
                        assignedBy = SessionManager.Instance.ActiveUser.ToString();
                    }
                }
                Task t = new Task(p, "Task", "Pending", assignedBy, DateTime.Now);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                p.Tasks.AddToList(t, args);
                TaskView tv = new TaskView(t);
                tv.ShowDialog();
                p.Tasks.ReleaseListeners(this);
                RefreshQueue();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetDataAsSring());
        }

        private string GetDataAsSring()
        {
            string output = "";
            foreach (DataColumn col in theQueue.dt.Columns)
            {
                output += col.ToString() + "\t";
            }
            output += Environment.NewLine;

            foreach (DataRow row in theQueue.dt.Rows)
            {
                foreach (DataColumn col in theQueue.dt.Columns)
                {
                    if (row[col] != null)
                    {
                        output += row[col].ToString() + "\t";
                    }
                    else
                    {
                        output += "\t";
                    }
                }
                output += Environment.NewLine;
            }

            return output;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.DefaultExt = "txt";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter outfile = new StreamWriter(sfd.FileName);
                outfile.Write(GetDataAsSring());
                outfile.Close();
            }
        }
        
        ////////////////////////////////////////////////////////////////////////////////////////////
        // filterDataTable() follows the logic for NWH exclusionary parameters in sp_highRiskFilter.
        ////////////////////////////////////////////////////////////////////////////////////////////
        private DataTable filterDataTable()
        {
            DataTable filteredTable;
            if (theQueue.dt.AsEnumerable().Any())
            {
                filteredTable = theQueue.dt.AsEnumerable().CopyToDataTable();
            }
            else
            {
                filteredTable = theQueue.dt.Clone();
            }
            try
            {
                if (this.ExcludeCancerGeneticsPatients.Checked)
                {
                    var dt = filteredTable.AsEnumerable().Where(row => row.Field<int?>("isRCPt").GetValueOrDefault() == 0);
                    if (dt.Any())
                    {
                        filteredTable = dt.CopyToDataTable();
                    }
                    else
                    {
                        filteredTable.Clear();
                    }
                }

                if (this.excludePatientsWithGeneticTesting.Checked)
                {
                    var dt = filteredTable.AsEnumerable().Where(row => row.Field<int?>("genTested").GetValueOrDefault() == 0);

                    if (dt.Any())
                    {
                        filteredTable = dt.CopyToDataTable();
                    }
                    else
                    {
                        filteredTable.Clear();
                    }
                }

                if (this.ExcludeDoNotContactPatients.Checked)
                {
                    var dt = filteredTable.AsEnumerable().Where(row => row.Field<int?>("DoNotContact").GetValueOrDefault() == 0);

                    if (dt.Any())
                    {
                        filteredTable = dt.CopyToDataTable();
                    }
                    else
                    {
                        filteredTable.Clear();
                    }
                }

            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("filterDataTable()" + e.ToString());
                return theQueue.dt;
            }
            

            return filteredTable;
        }

        private void ExcludeCancerGeneticsPatients_Click(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void ExcludeDoNotContactPatients_Click(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void excludePatientsWithGeneticTesting_Click(object sender, EventArgs e)
        {
            RefreshListView();
        }

        private void fastDataListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (theQueue.QueueName == "Lifetime Breast Risk = 20%")
                {
                    contextMenuStrip1.Show(fastDataListView1.PointToScreen(e.Location));
                }
            }
        }

        private void addMRIExamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RiskApps3.Model.PatientRecord.Patient proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                BreastImagingStudy bis = new BreastImagingStudy();
                bis.unitnum = proband.unitnum;
                bis.type = "MRI";
                bis.date = DateTime.Today;
                bis.imagingType = "MRI";
                bis.side = "Bilateral";

                AddImagingForm aif = new AddImagingForm();
                aif.Text = "Add new MRI Study";
                aif.study = bis;
                aif.proband = proband;

                if (aif.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    int a = SessionManager.Instance.GetActivePatient().apptid;
                    string u = SessionManager.Instance.GetActivePatient().unitnum;
                    FinalizeRecordForm frm = new FinalizeRecordForm(a, u);
                    frm.ShowDialog();

                    RefreshQueue();

                }
            }
        }
        /*************************************************************************************************/
    }
}



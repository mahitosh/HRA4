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
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.View.PatientRecord.GeneticTesting;
using RiskApps3.Model.PatientRecord.Communication;

namespace RiskApps3.View.RiskClinic
{
    public partial class PendingGeneticTestsView : HraView
    {
        /**************************************************************************************************/
        //   Members

        public int clinicID = -1;

        PendingGeneticTestList pendingGenTests = null;

        private PedigreeImageView pf;
        private GeneticTestingFamilySummaryView gtfsv;

        /**************************************************************************************************/

        public PendingGeneticTestsView()
        {
            InitializeComponent();

            fastDataListView1.AlternateRowBackColor = Color.WhiteSmoke;
        }
                
        /**************************************************************************************************/
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PedigreeForm).ToString())
            {
                pf = new PedigreeImageView();
                //pf.SetMode("MANUAL");
                return pf;
            }

            else if (persistString == typeof(GeneticTestingFamilySummaryView).ToString())
            {
                gtfsv = new GeneticTestingFamilySummaryView();
                return gtfsv;
            }
            else
                return null;
        }
        /**************************************************************************************************/
        private void PendingGeneticTestsView_Load(object sender, EventArgs e)
        {
            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;
            SessionManager.Instance.ClearActivePatient();
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

            string configFile = SessionManager.SelectDockConfig("PendingGeneticTestsView.config");
                                             
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                pf = new PedigreeImageView();
                //pf.SetMode("MANUAL");
                pf.Show(theDockPanel);
                pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                gtfsv = new GeneticTestingFamilySummaryView();  //can't modify view
                gtfsv.Show(theDockPanel);
                gtfsv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;

            }
            
            GetNewPendingGenTestList();

            if (pf != null)
            {
                pf.Enabled = false;
                pf.Show();
            }
        }

        private void GetNewPendingGenTestList()
        {
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            loadingCircle1.Active = true;

            if (gtfsv != null)
                gtfsv.Reset();

            if (pendingGenTests != null)
                pendingGenTests.ReleaseListeners(this);

            pendingGenTests = new PendingGeneticTestList();
            pendingGenTests.clinicId = clinicID;
            pendingGenTests.AddHandlersWithLoad(PendingGenTestListChanged,
                                      PendingGenTestListLoaded,
                                      PendingGenTestChanged);
        }

        /**************************************************************************************************/
        private void PendingGenTestListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                PendingGeneticTest theGenTest = (PendingGeneticTest)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }

        private void PendingGenTestListLoaded(HraListLoadedEventArgs e)
        {
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
            loadingCircle1.Active = false;

            fastDataListView1.SetObjects(pendingGenTests);

            if (pendingGenTests.Count > 0)
            {
                fastDataListView1.SelectedObject = pendingGenTests[0];
                fastDataListView1.SelectObject(pendingGenTests[0], true);
            }
            label1.Text = pendingGenTests.Count.ToString() + " Patients";

            foreach (OLVColumn c in fastDataListView1.Columns)
            {
                int w = c.Width;
                c.Width = -2;
                if (c.Width < w)
                    c.Width = w;
            }
        }

        private void PendingGenTestChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            patientRecordHeader1.setPatient(e.newActivePatient);
        }

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
        
        }

        /**************************************************************************************************/
        private void fastDataListView1_FormatRow(object sender, FormatRowEventArgs e)
        {
            //e.Item.SubItems[1].Text = (e.DisplayIndex + 1).ToString() + ": ";

            //if (e.Item.SubItems[3].Text == "1/1/0001")
            //if ((DateTime)((PendingGeneticTest)(e.Model)).datePanelOrdered == DateTime.MinValue)
            //{
            //    e.Item.SubItems[3].Text = "";
            //}
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

        private void buttonApplyTextSearch_Click(object sender, EventArgs e)
        {
            TimedFilter(fastDataListView1, textBoxFilterData.Text);
        }

        private void buttonClearTextSearch_Click(object sender, EventArgs e)
        {
            textBoxFilterData.Text = "";
            TimedFilter(fastDataListView1, textBoxFilterData.Text);
        }

        private void PendingGeneticTestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("PendingGeneticTestsView.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            SessionManager.Instance.RemoveHraView(this);

            patientRecordHeader1.ReleaseListeners();

            if (pf != null)
                pf.ViewClosing = true;

            if (gtfsv != null)
                gtfsv.ViewClosing = true;

            if (pf != null)
                pf.Close();

            if (gtfsv != null)
                gtfsv.Close();
        }

        private void buttonSaveState_Click(object sender, EventArgs e)
        {
            byte[] olvState = fastDataListView1.SaveState();
            ParameterCollection pc = new ParameterCollection("user", SessionManager.Instance.ActiveUser.ToString());
            pc.Add("filterName", this.Text);
            pc.Add("olvState", olvState);
            BCDB2.Instance.RunSPWithParams("sp_3_olvSaveState", pc);

            //**TEMPORARY** JUST AS A CONVENIENCE FOR DEVELOPERS TO CREATE DEFAULTS
            //Save state to lkpBigQueueColumnMetaData as well
            //Set SAVE_DEFAULTS_WHEN_SAVE_OLVCOLUMNSTATE true when you want to save new fall back defaults
            //this can be handy when the columns in tblBigQueue change
            bool SAVE_DEFAULTS_WHEN_SAVE_OLVCOLUMNSTATE = false;
            if (SAVE_DEFAULTS_WHEN_SAVE_OLVCOLUMNSTATE)
            {
                foreach (OLVColumn c in fastDataListView1.AllColumns)
                {
                    if (c.IsVisible)
                    {
                        pc = new ParameterCollection("filterName", this.Text);
                        pc.Add("columnHeader", c.Text);
                        pc.Add("columnWidth", c.Width);
                        pc.Add("columnDisplayOrder", c.LastDisplayIndex);
                        BCDB2.Instance.RunSPWithParams("sp_3_Save_OLVColumnMetaData", pc);
                    }
                }
            }
        }



        public byte[] getOlvState(String userName, String filterName)
        {
            byte[] result = new byte[0];
            try
            {
                //////////////////////
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = new SqlCommand("sp_3_olvGetState", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;

                    cmdProcedure.Parameters.Add("@user", SqlDbType.NVarChar);
                    cmdProcedure.Parameters["@user"].Value = userName;

                    cmdProcedure.Parameters.Add("@filterName", SqlDbType.NVarChar);
                    cmdProcedure.Parameters["@filterName"].Value = filterName;

                    try
                    {
                        SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0) == false)
                                {
                                    result = (byte[])reader.GetValue(0);
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception exception)
                    {
                        Logger.Instance.WriteToLog(exception.ToString());
                    }

                } //end of using connection
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog("getOlvState: " + exc.ToString());
                //Console.WriteLine(exc.StackTrace);
            }
            return result;
        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.D)) //Ctrl-D toggles Developer mode
            {
                if (buttonSaveState.Visible)
                {
                    buttonSaveState.Visible = false;
                    buttonRestoreState.Visible = false;
                }
                else
                {
                    buttonSaveState.Visible = true;
                    buttonRestoreState.Visible = true;
                }

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
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

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                SessionManager.Instance.SetActivePatient(((PendingGeneticTest)(fastDataListView1.SelectedObject)).unitnum,-1);
            }
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
            //if (SessionManager.Instance.GetActivePatient() != null)
            //    QueueData.UpdateQueueByMrn(SessionManager.Instance.GetActivePatient().unitnum);
            //GetNewPendingGenTestList();

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

        public override void PoppedToFront()
        {
            GetNewPendingGenTestList();
        }

        private void button2_Click(object sender, EventArgs e)
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PedigreeForm pf = new PedigreeForm();
            pf.WindowState = FormWindowState.Maximized;
            pf.ShowDialog();
            if (SessionManager.Instance.GetActivePatient() != null)
                QueueData.UpdateBigQueueByMrn(SessionManager.Instance.GetActivePatient().unitnum);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {
                p.Tasks.AddHandlersWithLoad(null, TaskListLoaded, null);
            }
        }
        /*********************************************************************************/
        private void TaskListLoaded(HraListLoadedEventArgs e)
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
                if (SessionManager.Instance.GetActivePatient() != null)
                    QueueData.UpdateBigQueueByMrn(SessionManager.Instance.GetActivePatient().unitnum);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //TODO finish
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fastDataListView1.CopyObjectsToClipboard((IList)fastDataListView1.Objects);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fastDataListView1.CopyObjectsToClipboard((IList)fastDataListView1.Objects);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.DefaultExt = "txt";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter outfile = new StreamWriter(sfd.FileName);
                outfile.Write(Clipboard.GetText());
                outfile.Close();
            }
        }
    }
}


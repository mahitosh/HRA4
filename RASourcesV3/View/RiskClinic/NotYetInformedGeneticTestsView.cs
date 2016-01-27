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

namespace RiskApps3.View.RiskClinic
{
    public partial class NotYetInformedGeneticTestsView : HraView
    {
        /**************************************************************************************************/
        //   Members

        NotYetInformedGeneticTestList nyiGenTests = null;

        private PedigreeImageView pf;
        private PatientNavigation pn;
        private SummaryFollowupView sfv;

        private PatientCommunicationView pcv;
        private GeneticTestingView gtv;

        /**************************************************************************************************/

        public NotYetInformedGeneticTestsView()
        {
            InitializeComponent();

            //find the datePanelOrdered column
            OLVColumn olvDPO = fastDataListView1.AllColumns.Find(
                    delegate(OLVColumn c)
                    {
                        return c.AspectName == "datePanelOrdered";
                    }
                );

            //old way: fastDataListView1.AllColumns[3].AspectToStringConverter = delegate(object x) {
            if (olvDPO != null)
            {
                olvDPO.AspectToStringConverter = delegate(object x)
                {
                    DateTime orderDate = (DateTime)x;
                    if (orderDate == DateTime.MinValue)
                        return String.Format("", "");
                    else
                        return String.Format("{0:d}", orderDate);
                };
            }

            for (int i = 1; i < fastDataListView1.Columns.Count; i++)
            {
                fastDataListView1.AllColumns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int cw1 = fastDataListView1.AllColumns[i].Width;
                fastDataListView1.AllColumns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                int cw2 = fastDataListView1.AllColumns[i].Width;
                fastDataListView1.AllColumns[i].Width = 2 + ((cw1 >= cw2) ? cw1 : cw2); //two pixels added for when filtered
            }

            //fastDataListView1.UseAlternatingBackColors = true;
            fastDataListView1.AlternateRowBackColor = Color.WhiteSmoke;

            buttonRestoreState_Click(this, new EventArgs());
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
            else if (persistString == typeof(PatientNavigation).ToString())
            {
                pn = new PatientNavigation();
                //pn.AddViewToParent = this.AddViewToParent;
                return pn;
            }
            else if (persistString == typeof(SummaryFollowupView).ToString())
            {
                sfv = new SummaryFollowupView();
                //sfv.AddViewToParent = this.AddViewToParent;
                return sfv;
            }
            else if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                //pcv.AddViewToParent = this.AddViewToParent;
                return pcv;
            }
            else if (persistString == typeof(GeneticTestingView).ToString())
            {
                gtv = new GeneticTestingView(false);
                return gtv;
            }
            else
                return null;
        }
        /**************************************************************************************************/
        private void NotYetInformedGeneticTestsView_Load(object sender, EventArgs e)
        {
            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;
            SessionManager.Instance.ClearActivePatient();
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

            string configFile = SessionManager.SelectDockConfig("NotYetInformedGeneticTestsView.config");
            
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                pf = new PedigreeImageView();
                //pf.SetMode("MANUAL");
                pf.Show(theDockPanel);
                pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                sfv = new SummaryFollowupView();
                sfv.Show(theDockPanel);
                sfv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                pcv = new PatientCommunicationView();
                pcv.Show(theDockPanel);
                pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                gtv = new GeneticTestingView(false);  //can't modify view
                gtv.Show(theDockPanel);
                gtv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;

                pn = new PatientNavigation();
                pn.Show(theDockPanel);
                pn.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
            }
            
            GetNewNotYetInformedGenTestList();

            if (pf != null)
            {
                pf.Enabled = false;
                pf.Show();
            }
        }

        private void GetNewNotYetInformedGenTestList()
        {
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            loadingCircle1.Active = true;

            if (nyiGenTests != null)
                nyiGenTests.ReleaseListeners(this);

            nyiGenTests = new NotYetInformedGeneticTestList();

            nyiGenTests.AddHandlersWithLoad(NotYetInformedGenTestListChanged,
                                      NotYetInformedGenTestListLoaded,
                                      NotYetInformedGenTestChanged);
        }

        /**************************************************************************************************/
        private void NotYetInformedGenTestListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                NotYetInformedGeneticTest theGenTest = (NotYetInformedGeneticTest)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }

        private void NotYetInformedGenTestListLoaded(HraListLoadedEventArgs e)
        {
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
            loadingCircle1.Active = false;

            fastDataListView1.SetObjects(nyiGenTests);

            if (nyiGenTests.Count > 0)
            {
                fastDataListView1.SelectedObject = nyiGenTests[0];
                fastDataListView1.SelectObject(nyiGenTests[0], true);
            }
            label1.Text = nyiGenTests.Count.ToString() + " Patients";
        }

        private void NotYetInformedGenTestChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            patientRecordHeader1.setPatient(e.newActivePatient);
        }


        /**************************************************************************************************/
        //void ListViewDataSetSelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (fastDataListView1.SelectedObject != null)
        //    {
        //        SessionManager.Instance.SetActivePatient(((NotYetInformedGeneticTest)(fastDataListView1.SelectedObject)).unitnum);
        //    }
        //}

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
        
        }

        /**************************************************************************************************/
        private void fastDataListView1_FormatRow(object sender, FormatRowEventArgs e)
        {
            //e.Item.SubItems[1].Text = (e.DisplayIndex + 1).ToString() + ": ";
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

            buttonRestoreState_Click(this, new EventArgs());
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

        private void NotYetInformedGeneticTestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("NotYetInformedGeneticTestsView.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            SessionManager.Instance.RemoveHraView(this);

            patientRecordHeader1.ReleaseListeners();

            if (pf != null)
                pf.ViewClosing = true;

            if (pn != null)
                pn.ViewClosing = true;

            if (sfv != null)
                sfv.ViewClosing = true;

            if (pcv != null)
                pcv.ViewClosing = true;

            if (gtv != null)
                gtv.ViewClosing = true;


            if (pf != null)
                pf.Close();

            if (pn != null)
                pn.Close();

            if (sfv != null)
                sfv.Close();

            if (pcv != null)
                pcv.Close();

            if (gtv != null)
                gtv.Close();
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

        private void buttonRestoreState_Click(object sender, EventArgs e)
        {
            byte[] previouslySavedState = getOlvState(SessionManager.Instance.ActiveUser.ToString(), this.Text);
            bool restoreSuccessful = false;

            if (previouslySavedState.Length != 0)
            {
                restoreSuccessful = fastDataListView1.RestoreState(previouslySavedState);
                if (!restoreSuccessful)
                {
                    Logger.Instance.WriteToLog("Could not restore previously saved olv column state for " + this.Text);
                }
                else return;
            }

            //set up columns the old fashioned way, one at a time
            //show only desired columns
            DataTable dt = BCDB2.Instance.getDataTable(@"EXEC sp_3_GetValuesFromlkpBigQueueColumnMetaData @filterName=N'" + this.Text + "'");
            List<OLVColumnMetaData> columnMetaDataList = new List<OLVColumnMetaData>();
            columnMetaDataList = new List<OLVColumnMetaData>(
                           (from dRow in dt.AsEnumerable()
                            select (GetOLVColumnMetaDataTableRow(dRow)))
                           );

            foreach (OLVColumn c in fastDataListView1.AllColumns)
            {
                OLVColumnMetaData r = null;
                r = columnMetaDataList.Find(item => item.columnHeader == c.Text);
                if (r != null)
                {
                    c.IsVisible = true;
                    c.Width = r.columnWidth;
                    c.LastDisplayIndex = r.columnDisplayOrder;
                }
                else
                {
                    c.IsVisible = false;
                }
            }

            fastDataListView1.RebuildColumns();
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
                SessionManager.Instance.SetActivePatient(((NotYetInformedGeneticTest)(fastDataListView1.SelectedObject)).unitnum,-1);
            }
        }

    }
}

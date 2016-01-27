using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    public partial class GeneticTestingView : HraView
    {
        /**************************************************************************************************/
        private Person selectedRelative;
        private PastMedicalHistory pmh;
        public Boolean canModify;
        private GeneticTestList testList;

        private const int pad = 10;

        /**************************************************************************************************/

        public GeneticTestingView()
        {
            InitializeComponent();
            this.canModify = true;
            this.flowLayoutPanel1.ControlAdded += new ControlEventHandler(flowLayoutPanel1_ControlAdded);
            this.flowLayoutPanel1.ControlRemoved += new ControlEventHandler(flowLayoutPanel1_ControlRemoved);
        }

        /**************************************************************************************************/

        public GeneticTestingView(Boolean canModify)
        {
            this.canModify = canModify;
            InitializeComponent();

            if (!canModify)
            {
                newTestButton.Visible = false;
            }
            this.flowLayoutPanel1.ControlAdded += new ControlEventHandler(flowLayoutPanel1_ControlAdded);
            this.flowLayoutPanel1.ControlRemoved += new ControlEventHandler(flowLayoutPanel1_ControlRemoved);
        }

        #region handle resizing of parent container and trickle down to children

        /*
         * 
         * This is instead of simply setting the width to whatever i want it to be and
         * then anchoring left|right.
         * 
         * For some reason, if you do those things at the time that you handle the add
         * genetic testing button click, the width is set to 0 and the control 
         * becomes invisible.  This might be a .net bug?
         * 
         * http://www.codeproject.com/Questions/174875/Anchor-to-Flow-layout-at-runtime
         * 
         * */

        void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.Equals(flowLayoutPanel1.Controls[0]))
            {
                e.Control.Size = new System.Drawing.Size(flowLayoutPanel1.Width - pad, flowLayoutPanel1.Controls[0].Height);
            }
            else
            {
                e.Control.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            }
        }

        void flowLayoutPanel1_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count>0)
            {
                this.flowLayoutPanel1.Controls[0].Anchor = AnchorStyles.None;
                this.flowLayoutPanel1.Controls[0].Size = new System.Drawing.Size(flowLayoutPanel1.Width - pad, flowLayoutPanel1.Controls[0].Height);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.loadingCircle1.CenterHorizontally();
            this.noLabel.CenterHorizontally();

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls[0].Size = new System.Drawing.Size(flowLayoutPanel1.Width - pad, flowLayoutPanel1.Controls[0].Height);
                return;
            }
        }

        #endregion

        /**************************************************************************************************/
        private void setupGrouping()
        {
            testGroupComboBox.Items.Clear();
            testGroupComboBox.Autocompleteitems.Clear();

            string[] groupings = this.testList
                    .Select(t => ((GeneticTestObject)t).groupingName)
                    .Distinct()
                    .ToArray();

            testGroupComboBox.Items.AddRange(groupings);

            testGroupComboBox.Autocompleteitems.AddRange(
                groupings
                    .Select(g => new AutoCompleteEntry(g, g))
                    .ToList());

            if (!testGroupComboBox.Items.Contains("All Groups"))
            {
                testGroupComboBox.Items.Add("All Groups");
                testGroupComboBox.Autocompleteitems.Add(
                    new AutoCompleteEntry(
                        "All Groups",
                        "All Groups")
                    );
            }
            testGroupComboBox.Sorted = true;

            int defaultGroupingID = GetDefaultGroupingId();  //todo: find out default grouping ID based on user
            String defaultGroupingName = this.testList
                .Where(t => ((GeneticTestObject)t).groupingID == defaultGroupingID)
                .Select(t => ((GeneticTestObject)t).groupingName)
                .Distinct()
                .SingleOrDefault();

            if (!String.IsNullOrEmpty(defaultGroupingName))
            {
                testGroupComboBox.Text = defaultGroupingName;
            }
            else
            {
                testGroupComboBox.Text = "";
            }
        }

        private int GetDefaultGroupingId()
        {
            int grouping = SessionManager.Instance.ActiveUser.groupingID;
            if (grouping != 0)
            {
                return grouping;
            }
            else
            {
                return 1;
            }
        }

        /**************************************************************************************************/
        private void setupPanels()
        {
            if (selectedRelative == null)
            {
                return;
            }

            int selectedGroupID = 1;

            String groupName = testGroupComboBox.Text;
            if (!String.IsNullOrEmpty(groupName))
            {
                selectedGroupID = this.testList
                    .Where(t => ((GeneticTestObject)t).groupingName.Equals(groupName))
                    .Select(t => ((GeneticTestObject)t).groupingID)
                    .Distinct()
                    .SingleOrDefault();

                if (selectedGroupID == 0)
                {
                    selectedGroupID = -1;
                }
            }

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is GeneticTestingRowExpandable)
                {
                    ((GeneticTestingRowExpandable)control).setGroupID(selectedGroupID);
                }
            }
        }

        /**************************************************************************************************/

        private void GeneticTestingView_Load(object sender, EventArgs e)
        {
            this.testList = SessionManager.Instance.MetaData.GeneticTests;
            SessionManager.Instance.MetaData.GeneticTests.AddHandlersWithLoad(null, TestsLoaded, null);
            SessionManager.Instance.NewActivePatient += NewActivePatient;
            SessionManager.Instance.RelativeSelected +=RelativeSelected;
            InitSelectedRelative();
        }

        private void TestsLoaded(HraListLoadedEventArgs e)
        {
            this.setupGrouping();
            this.setupPanels();
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void InitSelectedRelative()
        {
            
            flowLayoutPanel1.Controls.Clear();

            selectedRelative = SessionManager.Instance.GetSelectedRelative();

            if (selectedRelative != null)
            {
                this.newTestButton.Enabled = true;
                selectedRelative.AddHandlersWithLoad(selectedRelativeChanged, selectedRelativeLoaded, null);
            }
            else
            {
                this.newTestButton.Enabled = false;
                relativeHeader1.setRelative(null);
            }
        }

        /**************************************************************************************************/
        private void LoadOrGetPMH()
        {
            if (pmh != null)
            {
                pmh.GeneticTests.ReleaseListeners(this);
            }

            pmh = SessionManager.Instance.GetSelectedRelative().PMH;

            pmh.GeneticTests.AddHandlersWithLoad(GeneticTestListChanged,
                                         GeneticTestListLoaded,
                                         GeneticTestChanged);
        }

        /**************************************************************************************************/
        private void AddGeneticTest_Click(object sender, EventArgs e)
        {
            GeneticTest geneticTest = new GeneticTest(pmh);
            geneticTest.status = "Pending";
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = false;
            pmh.GeneticTests.AddToList(geneticTest, args);

            noLabel.Visible = false;
        }

        /**************************************************************************************************/

        private void GeneticTestListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                GeneticTest geneticTest = (GeneticTest)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:

                        GeneticTestingRowExpandable newRow = new GeneticTestingRowExpandable(this, geneticTest);
                        flowLayoutPanel1.Controls.Add(newRow);

                        //setup grouping
                        int selectedGroupID = 1;
                        String groupName = testGroupComboBox.Text;
                        if (!String.IsNullOrEmpty(groupName))
                        {
                            selectedGroupID = this.testList
                                    .Where(t => ((GeneticTestObject)t).groupingName.Equals(groupName))
                                    .Select(t => ((GeneticTestObject)t).groupingID)
                                    .Distinct()
                                    .SingleOrDefault();
                        }
                        newRow.setGroupID(selectedGroupID);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        Control doomed = null;
                        foreach (Control c in flowLayoutPanel1.Controls)
                        {
                            GeneticTestingRowExpandable targetRow = (GeneticTestingRowExpandable)c;
                            if (targetRow.GetGeneticTest() == geneticTest)
                                doomed = c;
                        }
                        if (doomed != null)
                            flowLayoutPanel1.Controls.Remove(doomed);

                        if (pmh.GeneticTests.Count == 0)
                            noLabel.Visible = true;

                        break;
                }
            }
        }
        
        /**************************************************************************************************/
        private void GeneticTestListLoaded(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/

        private void GeneticTestChanged(object sender, HraModelChangedEventArgs e)
        {
            //update the appropriate row with the new
        }

        /**************************************************************************************************/
        delegate void fillControlsCallback();

        public void FillControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                fillControlsCallback fcc = new fillControlsCallback(FillControls);
                this.Invoke(fcc, null);
            }
            else
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

                relativeHeader1.setRelative(selectedRelative);

                flowLayoutPanel1.Controls.Clear();

                foreach (GeneticTest geneticTest in pmh.GeneticTests)
                {
                    GeneticTestingRowExpandable newRow = new GeneticTestingRowExpandable(this, geneticTest);
                    flowLayoutPanel1.Controls.Add(newRow);
                    //Application.DoEvents();
                }
                setupPanels();

                if (pmh.GeneticTests.Count == 0)
                    noLabel.Visible = true;
                else
                    noLabel.Visible = false;
            }
        }

        /**************************************************************************************************/
        private void selectedRelativeChanged(object sender, HraModelChangedEventArgs e)
        {
            relativeHeader1.setRelative(selectedRelative);
        }

        /**************************************************************************************************/
        private void selectedRelativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetPMH();
        }

        /**************************************************************************************************/
        private void testGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupPanels();
        }

        /**************************************************************************************************/
        private void GeneticTestingView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }
    }
}
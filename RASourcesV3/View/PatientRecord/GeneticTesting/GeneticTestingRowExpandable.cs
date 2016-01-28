#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System.Globalization;

#endregion

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    public partial class GeneticTestingRowExpandable : UserControl
    {
        private GeneticTest geneticTest;
        private GeneticTestingView geneticTestingView;
        public RiskApps3.MainForm.PushViewCallbackType AddViewToParent;

        private const int ScrollIntervalValue = 5;
        private const int HeaderHeightValue = 27;
        private const int pad = 7;  //the magic number that makes resizing do something reasonable - use caution!
        private ToggleStateEnum State = ToggleStateEnum.Closed;

        GeneticTestResultComparer gtrc = new GeneticTestResultComparer();

        public enum ScrollEnum
        {
            Grow,
            Shrink,
            Toggle
        }

        private enum ToggleStateEnum
        {
            Open,
            Closed
        }        

        public GeneticTest GetGeneticTest()
        {
            return geneticTest;
        }

        public GeneticTestingRowExpandable(GeneticTestingView geneticTestingView, GeneticTest geneticTest)
        {
            this.geneticTestingView = geneticTestingView;
            this.geneticTest = geneticTest;
            InitializeComponent();

            string dateFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            if (dateFormat == "M/d/yyyy")
            {
                dateFormat = "MM/dd/yyyy";
            }
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = dateFormat;

            if (!geneticTestingView.canModify)
            {
                dateTimePicker1.Enabled = false;
                testMonthComboBox.Enabled = false;
                testYearComboBox.Enabled = false;
                //statusComboBox.Enabled = false;
                deleteButton.Visible = false;
                addResultButton.Visible = false;
            }

            if (this.geneticTest.GeneticTestResults.Count > 0)
            {
                this.addResultButton.Enabled = true;
                this.addResultButton.Visible = true;
            }

            if (this.geneticTest.IsASOTest && this.geneticTest.GeneticTestResults.Count < 1)
            {
                this.addResultButton.Visible = true;
                this.addResultButton.Enabled = true;
            }
            else if(this.geneticTest.IsASOTest && this.geneticTest.GeneticTestResults.Count>0)
            {
                this.addResultButton.Visible = false;
                this.addResultButton.Enabled = false;
            }

            //if (geneticTest.panelID > 0)
            if (geneticTest.GeneticTestResults.Count > 0)
            {
                this.panelComboBox.Enabled = false;
            }

            this.geneticTest.AddHandlersWithLoad(GeneticTestChanged, null, GeneticTestPersisted);

            FillControls();

            if (geneticTest.panelID > 0 && geneticTest.GeneticTestResults.Count == 0)
            {
                addResultRows();
                addResultButton.Enabled = true;
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
            //important to prevent memory leaks
            this.geneticTest.ReleaseListeners(this);
            base.Dispose(disposing);
        }

        #region handle resizing

        /**
         * http://www.codeproject.com/Questions/174875/Anchor-to-Flow-layout-at-runtime 
         */

        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
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

        void flowLayoutPanel1_ControlRemoved(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 1)
            {
                this.flowLayoutPanel1.Controls[1].Anchor = AnchorStyles.None;
                this.flowLayoutPanel1.Controls[1].Size = new System.Drawing.Size(flowLayoutPanel1.Width - pad, flowLayoutPanel1.Controls[1].Height);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (flowLayoutPanel1.Controls.Count > 0)
            {
                flowLayoutPanel1.Controls[0].Size = new Size(flowLayoutPanel1.Width - pad, flowLayoutPanel1.Controls[0].Height);
            }
        }

        #endregion

        private void FillControls()
        {
            testYearComboBox.Items.AddRange(UIUtils.getYearList());

            testMonthComboBox.Text = geneticTest.testMonth;
            testYearComboBox.Text = geneticTest.testYear;
            DateTime temp_date;
            if (DateTime.TryParse(geneticTest.testMonth + "/" + geneticTest.testDay + "/" + geneticTest.testYear, new CultureInfo("en-US"), DateTimeStyles.None, out temp_date))
            {
                dateTimePicker1.Value = temp_date;
            }
            String panelName = geneticTest.GetPanelName();
            panelComboBox.Text = panelName;
            statusComboBox.Text = geneticTest.status;
            resultsSummary.Text = geneticTest.GetResultSummaryText();
            if (string.IsNullOrEmpty(geneticTest.GeneticTest_accession) == false)
            {
                textBox1.Text = geneticTest.GeneticTest_accession;
            }
            //if (geneticTest.panelID > 0)
            if(geneticTest.GeneticTestResults.Count>0)
            {
                this.panelComboBox.Enabled = false;
            }

            //if (
            //    geneticTest.status == "Complete" && 
            //    !geneticTest.GeneticTestResults
            //        .Select(tr => tr.resultSignificance)
            //        .All(str => string.IsNullOrEmpty(str))
            //    )
            //{
            //    this.statusComboBox.Enabled = false;
            //}

            //if (
            //    geneticTest.IsASOTest &&
            //    geneticTest.GeneticTestResults
            //        .Select(tr => tr.ASOResult)
            //        .All(str => !string.IsNullOrEmpty(str)))
            //{
            //    this.statusComboBox.Enabled = false;
            //}

            flowLayoutPanel1.Controls.Clear();
            if (geneticTest.GeneticTestResults.Count > 0)
            {
                this.addResultButton.Enabled = true;

                if (this.geneticTest.IsASOTest)
                {
                    flowLayoutPanel1.Controls.Add(new GeneticTestingASOResultRowHeader());
                }
                else 
                {
                    flowLayoutPanel1.Controls.Add(new GeneticTestingResultRowHeader());
                }
                

                List<string> genes = new List<string>();
                geneticTest.GeneticTestResults.Sort(gtrc);
                foreach (GeneticTestResult geneticTestResult in geneticTest.GeneticTestResults)
                {
                    Boolean canDelete = genes.Contains(geneticTestResult.geneName) ||
                                        string.IsNullOrEmpty(geneticTestResult.geneName);
                    genes.Add(geneticTestResult.geneName);

                    Control newRow = null;
                    if (geneticTest.IsASOTest)
                    {
                        newRow = new GeneticTestingASOResultRow(geneticTestResult, canDelete);
                    }
                    else
                    {
                        newRow = new GeneticTestingResultRow(geneticTestResult, canDelete);
                    }
                    
                    if (!geneticTestingView.canModify)
                    {
                        newRow.Enabled = false;
                    }
                    flowLayoutPanel1.Controls.Add(newRow);
                }
            }
            
            if (!string.IsNullOrEmpty(geneticTest.testDay))
            {
                DisableDateTimeDropdowns();
            }

            DoScroll(ScrollEnum.Grow);
        }

        private void DisableDateTimeDropdowns()
        {
            testMonthComboBox.Visible = false;
            testYearComboBox.Visible = false;

            dateTimePicker1.Size = new Size(102, 20);
        }

        private void GeneticTestChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.updatedMembers.Contains(geneticTest.GetMemberByName("panelID")))
            {
                if (
                    geneticTest.IsASOTest && 
                    !geneticTest.owningPMH.RelativeOwningPMH.owningFHx
                        .HasUniqueNonNegativeGeneticTestResults(
                            geneticTest.owningPMH.RelativeOwningPMH.relativeID))
                {
                    MessageBox.Show(
                        "This option is only valid for family histories with a known mutation.  " +
                            "Please enter a positive mutation for a relative of this individual before proceeding.",
                        "No known positive mutations in this family.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.addResultButton.Visible = true;
                    this.addResultButton.Enabled = true;

                    return;
                }
               addResultRows();
            }
            if (e.updatedMembers.Contains(geneticTest.GetMemberByName("GeneticTestResults")))
            {
                if (geneticTest.status == "Pending")
                {
                    foreach (Control c in flowLayoutPanel1.Controls)
                    {
                        if (c is GeneticTestingASOResultRow)
                        {
                            GeneticTestingASOResultRow rr = (GeneticTestingASOResultRow)c;
                            rr.SignalTetstObject();
                        }
                        else if (c is GeneticTestingResultRow)
                        {
                            GeneticTestingResultRow rr = (GeneticTestingResultRow)c;
                            rr.SignalTetstObject();
                        }
                    }
                }
                geneticTest.status = "Complete";
                //statusComboBox.Enabled = false;
                statusComboBox.SelectedItem = "Complete";
                this.panelComboBox.Enabled = false;

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
                args.updatedMembers.Add(geneticTest.GetMemberByName("status"));

                geneticTest.SignalModelChanged(args);

                this.resultsSummary.Text = geneticTest.GetResultSummaryText();
                this.ResultSignificanceNegativeButton.Visible = false;
                this.ResultSignificanceNegativeButton.Enabled = false;

            }
            if (e.updatedMembers.Contains(geneticTest.GetMemberByName("status")))
            {
                if (geneticTest.status.Contains("Informed"))
                {
                    this.geneticTest.GeneticTest_isPtAware = 1;
                }
                if (geneticTest.status == "Complete")
                {
                    this.geneticTest.GeneticTest_isPtAware = 0;
                }
            }
        }

        public void DoScroll(ScrollEnum instruction)
        {
            switch (instruction)
            {
                case ScrollEnum.Grow:
                    this.Grow();
                    return;
                case ScrollEnum.Shrink:
                    this.Shrink();
                    return;
                case ScrollEnum.Toggle:
                    this.Toggle();
                    return;
            }
        }

        private void Toggle()
        {
            switch (this.State)
            {
                case ToggleStateEnum.Closed:
                    Grow();
                    return;
                case ToggleStateEnum.Open:
                    Shrink();
                    return;
            }
        }

        private void Grow()
        {
            int x = flowLayoutPanel1.Location.Y + SumHeight(flowLayoutPanel1) + pad;
            while (this.Height < (x))
            {
                Application.DoEvents();
                this.Height += ScrollIntervalValue;
            }
            this.expanderToggle.ImageIndex = 0;
            this.Height = x;

            this.State = ToggleStateEnum.Open;
        }

        private static int SumHeight(Control control)
        {
            int i = 0;
            foreach (Control c in control.Controls)
            {
                i += c.Height;
            }
            return i;
        }

        private void Shrink()
        {
            while (this.Height > this.lblTop.Height)
            {
                Application.DoEvents();
                this.Height -= ScrollIntervalValue;
            }
            this.expanderToggle.ImageIndex = 1;
            this.Height = this.lblTop.Height + datelabel.Height + pad;

            this.State = ToggleStateEnum.Closed;
        }


        private void ScrollablePanel_Load(object sender, EventArgs e)
        {
            this.lblTop.Height = GeneticTestingRowExpandable.HeaderHeightValue;
        }

        private void addResultRows()
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = false;
            
            ClearExistingResults();

            if (geneticTest.IsASOTest)
            {
                this.ResultSignificanceNegativeButton.Enabled = false;
                this.ResultSignificanceNegativeButton.Visible = false;

                List<GeneticTestResult> geneticTestResults = geneticTest.owningPMH.RelativeOwningPMH.owningFHx.
                    GetUniqueNonNegativeGeneticTestResults(
                    geneticTest.owningPMH.RelativeOwningPMH.relativeID);
                
                geneticTestResults.Sort(gtrc);

                if (geneticTestResults.Count > 0)
                {
                    flowLayoutPanel1.Controls.Add(new GeneticTestingASOResultRowHeader());
                    this.ResultSignificanceNegativeButton.Visible = true;
                    this.ResultSignificanceNegativeButton.Enabled = true;
                    this.ResultSignificanceNegativeButton.Text = "Set results Not Found.";

                    this.addResultButton.Visible = false;
                    this.addResultButton.Enabled = false;
                }
                foreach (GeneticTestResult gtr in geneticTestResults)
                {
                    GeneticTestResult geneticTestResult = new GeneticTestResult(geneticTest);
                    geneticTestResult.geneName = gtr.geneName;
                    geneticTestResult.testInstanceID = geneticTest.instanceID;
                    geneticTestResult.ASOResultSignificance = gtr.resultSignificance;
                    geneticTestResult.ASOMutationName = gtr.mutationName;
                    geneticTestResult.ASOMutationAA = gtr.mutationAA;
                    geneticTestResult.relativeIDofRelative =
                        gtr.owningGeneticTest.owningPMH.RelativeOwningPMH.relativeID;
                    geneticTestResult.instanceIDofRelative =
                        gtr.owningGeneticTest.instanceID;
                    geneticTestResult.ASOResult = "Unknown";
                    
                    geneticTest.GeneticTestResults.Add(geneticTestResult);

                    GeneticTestingASOResultRow row = new GeneticTestingASOResultRow(geneticTestResult, false);
                    flowLayoutPanel1.Controls.Add(row);
                }
            }
            else
            {
                List<String> geneList = SessionManager.Instance.MetaData.GeneticTests.GetGenesInPanel(geneticTest.panelID);
                if (geneList.Count > 0)
                {
                    flowLayoutPanel1.Controls.Add(new GeneticTestingResultRowHeader());
                    this.ResultSignificanceNegativeButton.Visible = true;
                    this.ResultSignificanceNegativeButton.Enabled = true;
                }
                geneList.Sort();
                foreach (String geneName in geneList)
                {
                    GeneticTestResult gtr = geneticTest
                        .GeneticTestResults
                        .SingleOrDefault(
                            r => 
                                ((GeneticTestResult)r).instanceID == geneticTest.instanceID && 
                                ((GeneticTestResult)r).instanceID!=0);


                    if (gtr == null)
                    {
                        gtr = new GeneticTestResult(geneticTest);
                        gtr.testInstanceID = geneticTest.instanceID;
                    }

                    gtr.geneName = geneName;

                    //TODO turn genetic test results into HRAList and use AddToList
                    geneticTest.GeneticTestResults.Add(gtr);

                    Control gtrr = null;
                    if (geneticTest.IsASOTest)
                    {
                        gtrr = new GeneticTestingASOResultRow(gtr, false);
                    }
                    else
                    {
                        gtrr = new GeneticTestingResultRow(gtr, false);
                    }
                    flowLayoutPanel1.Controls.Add(gtrr);
                }
            }

            DoScroll(ScrollEnum.Grow);
        }

        private void ClearExistingResults()
        {
            HraModelChangedEventArgs deleteArgs = new HraModelChangedEventArgs(null);
            deleteArgs.Delete = true;

            foreach (GeneticTestResult result in geneticTest.GeneticTestResults)
            {
                result.SignalModelChanged(deleteArgs);
            }
            geneticTest.GeneticTestResults.Clear();
            flowLayoutPanel1.Controls.Clear();
        }

        private void addResultButton_Click(object sender, EventArgs e)
        {
            //handles the little '+' button
            GeneticTestResult geneticTestResult = new GeneticTestResult(geneticTest);
            geneticTestResult.testInstanceID = geneticTest.instanceID;
            geneticTest.GeneticTestResults.Add(geneticTestResult);
            
            Control toAdd = null;
            if (this.geneticTest.IsASOTest)
            {
                //make this load known mutations or throw the error message
                if (
                    !geneticTest.owningPMH.RelativeOwningPMH.owningFHx
                        .HasUniqueNonNegativeGeneticTestResults(
                            geneticTest.owningPMH.RelativeOwningPMH.relativeID))
                {
                    MessageBox.Show(
                        "This option is only valid for family histories with a known mutation.  " +
                            "Please enter a positive mutation for a relative of this individual before proceeding.",
                        "No known positive mutations in this family.",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.addResultButton.Visible = true;
                    this.addResultButton.Enabled = true;

                    return;
                }
                else
                {
                    addResultRows();
                }
            }
            else
            {
                toAdd = new GeneticTestingResultRow(geneticTestResult, true);
            }

            flowLayoutPanel1.Controls.Add(toAdd);

            DoScroll(ScrollEnum.Grow);
        }

        private void updatePMH(bool persist)
        {
            HraModelChangedEventArgs pmh_args = new RiskApps3.Model.HraModelChangedEventArgs(geneticTestingView);
            pmh_args.Persist = persist;
            geneticTest.owningPMH.SignalModelChanged(pmh_args);
        }

        private void testMonthComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (geneticTest.testMonth != testMonthComboBox.Text)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
                geneticTest.testMonth = testMonthComboBox.Text;

                args.updatedMembers.Add(geneticTest.GetMemberByName("testMonth"));
                geneticTest.SignalModelChanged(args);
            }
            //  updatePMH();
        }

        public void setGroupID(int groupID)
        {
            string [] geneticTestList = SessionManager.Instance.MetaData.GeneticTests
                .OrderBy(g => ((GeneticTestObject)g).displayOrder)
                .ThenBy(g => ((GeneticTestObject)g).panelName)
                .Where(g => ((GeneticTestObject)g).groupingID == groupID || groupID == 0)
                .Select(g => ((GeneticTestObject)g).panelName)
                .Distinct()                
                .ToArray();

            panelComboBox.Items.Clear();

            panelComboBox.Items.AddRange(geneticTestList);
            //panelComboBox.Sorted = true;
        }

        private void testYearComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (geneticTest.testYear != testYearComboBox.Text)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
                geneticTest.testYear = testYearComboBox.Text;

                args.updatedMembers.Add(geneticTest.GetMemberByName("testYear"));
                geneticTest.SignalModelChanged(args);
            }
        }
        private void updatePMH()
        {
            HraModelChangedEventArgs pmh_args = new RiskApps3.Model.HraModelChangedEventArgs(null);
            pmh_args.Persist = false;
            geneticTest.owningPMH.SignalModelChanged(pmh_args);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            String date = dateTimePicker1.Value.ToString("MM/dd/yyy");
            string day = date.Substring(3, 2);
            string month = date.Substring(0, 2);
            string year = date.Substring(6, 4);
            if (day != geneticTest.testDay || month != geneticTest.testMonth || year != geneticTest.testYear)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
                geneticTest.testMonth = month;
                geneticTest.testDay = day;
                geneticTest.testYear = year;

                args.updatedMembers.Add(geneticTest.GetMemberByName("testMonth"));
                args.updatedMembers.Add(geneticTest.GetMemberByName("testDay"));
                args.updatedMembers.Add(geneticTest.GetMemberByName("testYear"));

                DisableDateTimeDropdowns();
                geneticTest.SignalModelChanged(args);
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            geneticTest.owningPMH.GeneticTests.RemoveFromList(geneticTest, SessionManager.Instance.securityContext);
        }

        private void panelComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string panelString = panelComboBox.SelectedItem.ToString();
            geneticTest.status = "Pending";
            statusComboBox.Enabled = true;

            this.addResultButton.Enabled = true;

            int panelID = SessionManager.Instance.MetaData.GeneticTests.GetPanelIDFromName(panelString);
            geneticTest.panelID = panelID;
            geneticTest.panelShortName = SessionManager.Instance.MetaData.GeneticTests.GetPanelShortNameFromID(panelID);
            geneticTest.panelName = SessionManager.Instance.MetaData.GeneticTests.GetPanelNameFromID(panelID);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
            args.updatedMembers.Add(geneticTest.GetMemberByName("panelID"));
            args.updatedMembers.Add(geneticTest.GetMemberByName("status"));

            geneticTest.SignalModelChanged(args);
        }
        
        public void GeneticTestPersisted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (geneticTest.GeneticTestResults.Count == 0)
            {
                if (
                    geneticTest.IsASOTest && 
                    !geneticTest.owningPMH.RelativeOwningPMH.owningFHx
                        .HasUniqueNonNegativeGeneticTestResults(
                            geneticTest.owningPMH.RelativeOwningPMH.relativeID))
                {
                    return;
                }
                addResultRows();
            }
        }

        private void statusComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (geneticTest.GeneticTest_status != statusComboBox.SelectedItem.ToString())
            {
                geneticTest.GeneticTest_status = statusComboBox.SelectedItem.ToString();

                if (statusComboBox.SelectedItem.ToString().StartsWith("Complete"))
                {
                    panelComboBox.Enabled = false;
                    //statusComboBox.Enabled = false;
                    this.ResultSignificanceNegativeButton.Visible = false;
                    this.ResultSignificanceNegativeButton.Enabled = false;
                    geneticTest.SetNegativeResults();
                }

                if (statusComboBox.SelectedItem.ToString().Contains("Informed"))
                {
                    this.geneticTest.GeneticTest_isPtAware = 1;
                }
                if (statusComboBox.SelectedItem.ToString() == "Complete")
                {
                    this.geneticTest.GeneticTest_isPtAware = 0;
                }
            }
        }
        private void expanderToggle_Click(object sender, EventArgs e)
        {
            DoScroll(ScrollEnum.Toggle);
        }

        private void ResultSignificanceNegativeButton_Click(object sender, EventArgs e)
        {
            this.ResultSignificanceNegativeButton.Visible = false;

            geneticTest.SetNegativeResults();
            statusComboBox.Enabled = true;

            geneticTest.status = "Complete";

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(geneticTestingView);
            args.updatedMembers.Add(geneticTest.GetMemberByName("status"));
            geneticTest.SignalModelChanged(args);

            //statusComboBox.Enabled = false;
            statusComboBox.SelectedItem = "Complete";
            this.panelComboBox.Enabled = false;

            foreach (GeneticTestResult result in geneticTest.GeneticTestResults)
            {
                HraModelChangedEventArgs resultArgs = new HraModelChangedEventArgs(null);
                resultArgs.updatedMembers.Add(result.GetMemberByName("geneName"));
                result.SignalModelChanged(resultArgs);
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                this.geneticTest.GeneticTest_accession = textBox1.Text;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    public partial class GeneticTestingASOResultRow : UserControl
    {
        private GeneticTestResult geneticTestResult;

        public GeneticTestingASOResultRow(GeneticTestResult geneticTestResult, bool canDelete)
        {
            this.geneticTestResult = geneticTestResult;

            this.geneticTestResult.AddHandlersWithLoad(TestResultChanged, null, null);
            
            InitializeComponent();

            updateUI();

            List<String> geneList = SessionManager.Instance.MetaData.GeneticTests.GetGenesInPanel(geneticTestResult.owningGeneticTest.panelID);
            
            FillDataFromTestResult(geneticTestResult);
            //SaveASOData();
            
            deleteButton.Visible = canDelete;
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
            //necessary for preventing memory leaks
            this.geneticTestResult.ReleaseListeners(this);
            base.Dispose(disposing);
        }

        delegate void FillDataCallback(GeneticTestResult result);
        private void FillDataFromTestResult(GeneticTestResult geneticTestResult)
        {            
            if (geneticTestResult != null)
            {
                if (this.InvokeRequired)
                {
                    FillDataCallback fdc = new FillDataCallback(FillDataFromTestResult);
                    object[] args = new object[1];
                    args[0] = geneticTestResult;
                    this.Invoke(fdc, args);
                }
                else
                {   
                    allelicStateComboBox.Text = geneticTestResult.allelicState;
                    commentsTextBox.Text = geneticTestResult.comments;

                    ASOInfoTextBox.Text = geneticTestResult.GetASOSummary();
                    ASOResultComboBox.Text = geneticTestResult.ASOResult;
                }
            }
        }

        private void TestResultChanged(object sender, HraModelChangedEventArgs e)
        {
            FillDataFromTestResult(this.geneticTestResult);
        }
        public void SignalTetstObject()
        {
            geneticTestResult.SignalModelChanged(new HraModelChangedEventArgs(null));
            //geneticTestResult.BackgroundPersistWork(new HraModelChangedEventArgs(null));
        }
        private void updateUI()
        {
            //if (geneticTestResult.owningGeneticTest.GetPanelName()== "Familial Known Mutation Test")
            if (geneticTestResult.owningGeneticTest.IsASOTest)
            {
                ASOInfoTextBox.Visible = true;
                ASOResultComboBox.Visible = true;
            }
            else
            {
                ASOInfoTextBox.Visible = false;
                ASOResultComboBox.Visible = false;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Delete = true;
            geneticTestResult.SignalModelChanged(args);
            geneticTestResult.owningGeneticTest.GeneticTestResults.Remove(geneticTestResult);

            HraModelChangedEventArgs resultListArgs = new HraModelChangedEventArgs(null);
            args.updatedMembers.Add(geneticTestResult.owningGeneticTest.GetMemberByName("GeneticTestResults"));

            geneticTestResult.owningGeneticTest.SignalModelChanged(args);

            HraModelChangedEventArgs gt_args = new HraModelChangedEventArgs(null);
            gt_args.Persist = false;
            geneticTestResult.owningGeneticTest.SignalModelChanged(gt_args);

            HraModelChangedEventArgs pmh_args = new HraModelChangedEventArgs(null);
            pmh_args.Persist = false;
            geneticTestResult.owningGeneticTest.owningPMH.SignalModelChanged(pmh_args);

            RemoveFromUI(((Control)sender).Parent);
        }

        private void RemoveFromUI(Control control)
        {
            Control flowLayoutPanel = this.Parent;
            flowLayoutPanel.Controls.Remove(control);

            GeneticTestingRowExpandable expander = (GeneticTestingRowExpandable)flowLayoutPanel.Parent;
            expander.DoScroll(GeneticTestingRowExpandable.ScrollEnum.Shrink);
            expander.DoScroll(GeneticTestingRowExpandable.ScrollEnum.Grow);
        }

        private void SignalOwningTestCompleted()
        {
            //save the test
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Delete = false;
            args.Persist = false;
            args.updatedMembers.Add(this.geneticTestResult.owningGeneticTest.GetMemberByName("GeneticTestResults"));
            this.geneticTestResult.owningGeneticTest.SignalModelChanged(args);

            //we should also save the test *result* at this time
           // HraModelChangedEventArgs resultArgs = new HraModelChangedEventArgs(null);
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("geneName"));
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("ASOMutationAA"));
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("ASOMutationName"));
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("ASOResultSignificance"));
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("relativeIDofRelative"));
            //resultArgs.updatedMembers.Add(this.geneticTestResult.GetMemberByName("instanceIDofRelative"));
           // this.geneticTestResult.SignalModelChanged(resultArgs);
        }

        private void allelicStateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            geneticTestResult.allelicState = allelicStateComboBox.SelectedItem.ToString();

            args.updatedMembers.Add(geneticTestResult.GetMemberByName("allelicState"));
            geneticTestResult.SignalModelChanged(args);

            SignalOwningTestCompleted();
        }

        private void ASOResultComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            geneticTestResult.ASOResult = ASOResultComboBox.Text;

            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOResultSignificance"));
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOMutationName"));
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOMutationAA"));

            if (geneticTestResult.ASOResult.ToUpper().Equals("FOUND"))
            {
                geneticTestResult.resultSignificance = geneticTestResult.ASOResultSignificance;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));

                geneticTestResult.mutationName = geneticTestResult.ASOMutationName;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));

                geneticTestResult.mutationAA = geneticTestResult.ASOMutationAA;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
            }
            else
            {
                geneticTestResult.resultSignificance = "";
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));

                geneticTestResult.mutationName = "";
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));

                geneticTestResult.mutationAA = "";
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
            }
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOResult"));
            geneticTestResult.SignalModelChanged(args);

            SignalOwningTestCompleted();
        }

        private void SaveASOData()
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("geneName"));
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOMutationName"));
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOResultSignificance"));
            args.updatedMembers.Add(geneticTestResult.GetMemberByName("ASOMutationAA"));
            this.geneticTestResult.SignalModelChanged(args);
        }

        private void commentsTextBox_Validated(object sender, EventArgs e)
        {
            if (
                !string.IsNullOrEmpty(commentsTextBox.Text) || 
                
                    (string.IsNullOrEmpty(commentsTextBox.Text) && 
                    !string.IsNullOrEmpty(geneticTestResult.comments))
                )
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.comments = commentsTextBox.Text;

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("comments"));
                geneticTestResult.SignalModelChanged(args);

                SignalOwningTestCompleted();
            }            
        }
    }
}
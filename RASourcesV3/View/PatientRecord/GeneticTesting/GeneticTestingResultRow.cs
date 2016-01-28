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
    public partial class GeneticTestingResultRow : UserControl
    {
        private GeneticTestResult geneticTestResult;
        private bool initialized = false;

        public GeneticTestingResultRow(GeneticTestResult geneticTestResult, bool canDelete)
        {
            this.geneticTestResult = geneticTestResult;

            this.geneticTestResult.AddHandlersWithLoad(TestResultChanged, null, null);
            
            InitializeComponent();

            updateUI();

            UIUtils.fillComboBoxFromLookups(resultSignificanceComboBox, "tblRiskGeneticTest", "resultSignificance", true);
                 
            List<String> geneList = SessionManager.Instance.MetaData.GeneticTests.GetGenesInPanel(geneticTestResult.owningGeneticTest.panelID);
            UIUtils.fillComboBoxFromList(geneNameComboBox,geneList,false);

            FillDataFromTestResult(geneticTestResult);
            
            //deleteButton.Visible = canDelete;
            geneNameComboBox.Visible = canDelete;
            geneNameTextBox.Visible = !canDelete;

            populateMutationComboBoxes();

            initialized = true;
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
            //important for preventing memory leaks
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
                    if (!string.IsNullOrEmpty(geneticTestResult.geneName))
                    {
                        geneNameComboBox.Text = geneticTestResult.geneName;
                        geneNameTextBox.Text = geneticTestResult.geneName;
                    } 
                    else 
                    {
                        geneNameTextBox.ReadOnly = false;
                    }                    
                    mutationNameComboBox.Text = geneticTestResult.mutationName;
                    mutationAAComboBox.Text = geneticTestResult.mutationAA;
                    if (geneticTestResult.resultSignificance != null)
                    {
                        if (resultSignificanceComboBox.Items.Contains(geneticTestResult.resultSignificance) == false)
                        {
                            resultSignificanceComboBox.Items.Add(geneticTestResult.resultSignificance);
                        }
                    }
                    resultSignificanceComboBox.Text = geneticTestResult.resultSignificance;
                    allelicStateComboBox.Text = geneticTestResult.allelicState;
                    commentsTextBox.Text = geneticTestResult.comments;
                    var selection = SessionManager.Instance.MetaData.Mutations
                        .Where(m =>
                            ((MutationObject)m).geneName == geneticTestResult.geneName &&
                            ((MutationObject)m).mutationDNA == geneticTestResult.mutationName);
                    string lkpresultSignificance = selection.Select(m => ((MutationObject)m).significance).FirstOrDefault();
                    if (lkpresultSignificance != geneticTestResult.resultSignificance)
                    {
                        //BD - VariantSerive
                        //label1.Visible = true;
                    }
                    else
                    {
                        label1.Visible = false;
                    }
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
        }
        private void populateMutationComboBoxes()
        {
            String geneName = geneNameComboBox.Text;

            string[] mutationDNAs = SessionManager
                    .Instance.MetaData.Mutations
                    .Where(mutation => 
                        ((MutationObject)mutation).geneName == geneName && 
                        ((MutationObject)mutation).mutationDNA != null)
                    .Select(mutation => ((MutationObject)mutation).mutationDNA)
                    .ToArray();

            mutationNameComboBox.Items.Clear();
            mutationNameComboBox.Items.Add("");
            mutationNameComboBox.Items.AddRange(mutationDNAs);

            string[] mutationAAs = SessionManager
                    .Instance.MetaData.Mutations
                    .Where(mutation => 
                        ((MutationObject)mutation).geneName == geneName && 
                        ((MutationObject)mutation).mutationAA != null)
                    .Select(mutation => ((MutationObject)mutation).mutationAA)
                    .ToArray();

            mutationAAComboBox.Items.Clear();
            mutationAAComboBox.Items.Add("");
            mutationAAComboBox.Items.AddRange(mutationAAs);            
        }

        private void updateUI()
        {
            if (geneticTestResult.owningGeneticTest.IsASOTest)
            {
                geneNameComboBox.Visible = false;
                geneNameTextBox.Visible = false;
                mutationNameComboBox.Visible = false;
                mutationAAComboBox.Visible = false;
                resultSignificanceComboBox.Visible = false;
            }
            else
            {
                geneNameComboBox.Visible = true;
                geneNameTextBox.Visible = true;
                mutationNameComboBox.Visible = true;
                mutationAAComboBox.Visible = true;
                resultSignificanceComboBox.Visible = true;
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Delete = true;
            geneticTestResult.SignalModelChanged(args);
            geneticTestResult.owningGeneticTest.GeneticTestResults.Remove(geneticTestResult);

            //HraModelChangedEventArgs resultListArgs = new HraModelChangedEventArgs(null);
            //args.updatedMembers.Add(geneticTestResult.owningGeneticTest.GetMemberByName("GeneticTestResults"));
            //geneticTestResult.owningGeneticTest.SignalModelChanged(args);

            HraModelChangedEventArgs gt_args = new HraModelChangedEventArgs(null);
            //gt_args.Persist = false;
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
           // this.geneticTestResult.SignalModelChanged(resultArgs);
        }

        private void allelicStateComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (initialized)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.allelicState = allelicStateComboBox.SelectedItem.ToString();

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("allelicState"));
                geneticTestResult.SignalModelChanged(args);

                SignalOwningTestCompleted();
            }
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

        private void mutationNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void mutationNameComboBox_Validated(object sender, EventArgs e)
        {
            if (geneticTestResult.mutationName != this.mutationNameComboBox.Text)
            {
                if (
                    this.mutationNameComboBox.Items.Count == 0 ||
                    !this.mutationNameComboBox.Items.Contains(this.mutationNameComboBox.Text))
                {
                    //in this case the SelectionChangedCommitted event will never fire

                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    geneticTestResult.mutationName = this.mutationNameComboBox.Text;

                    args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));
                    geneticTestResult.SignalModelChanged(args);

                    SignalOwningTestCompleted();
                }
            }
        }
        private void mutationAAComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
        private void mutationAAComboBox_Validated(object sender, EventArgs e)
        {

            if (geneticTestResult.mutationAA != this.mutationAAComboBox.Text)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.mutationAA = this.mutationAAComboBox.Text;

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
                geneticTestResult.SignalModelChanged(args);

                SignalOwningTestCompleted();
            }

        }

        private void resultSignificanceComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (initialized)
            {
                if (geneticTestResult.resultSignificance != resultSignificanceComboBox.SelectedItem.ToString())
                {
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    geneticTestResult.resultSignificance = resultSignificanceComboBox.SelectedItem.ToString();

                    args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));
                    geneticTestResult.SignalModelChanged(args);

                    SignalOwningTestCompleted();
                }
            }
        }
        private void geneNameComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if (initialized)
            //{
            //    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            //    geneticTestResult.geneName = geneNameComboBox.SelectedItem.ToString();

            //    args.updatedMembers.Add(geneticTestResult.GetMemberByName("geneName"));
            //    geneticTestResult.SignalModelChanged(args);

            //    populateMutationComboBoxes();

            //    SignalOwningTestCompleted();
            //}
        }
        private void geneNameTextBox_Validated(object sender, EventArgs e)
        {
            if (
                !string.IsNullOrEmpty(geneNameTextBox.Text) ||

                    (string.IsNullOrEmpty(geneNameTextBox.Text) &&
                    !string.IsNullOrEmpty(geneticTestResult.geneName))
                )
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.geneName = geneNameTextBox.Text;

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("geneName"));
                geneticTestResult.SignalModelChanged(args);

                populateMutationComboBoxes();

                SignalOwningTestCompleted();
            }
        }

        private void mutationNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                if (string.IsNullOrEmpty(geneticTestResult.mutationName) == false)
                    if (geneticTestResult.mutationName == mutationNameComboBox.SelectedItem.ToString())
                        return;

                if (this.mutationNameComboBox.Items.Count != 0)
                {
                    if (geneNameComboBox.SelectedItem != null)
                    {
                        //if this fails, the Validated even handler should fire
                        string selectedItemGeneName = geneNameComboBox.SelectedItem.ToString();
                        string selectedItemMutationName = mutationNameComboBox.SelectedItem.ToString();

                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        geneticTestResult.mutationName = selectedItemMutationName;

                        if (
                            !string.IsNullOrEmpty(selectedItemGeneName) &&
                            !string.IsNullOrEmpty(selectedItemMutationName))
                        {
                            var selection = SessionManager.Instance.MetaData.Mutations
                                .Where(m =>
                                    ((MutationObject)m).geneName == selectedItemGeneName &&
                                    ((MutationObject)m).mutationDNA == selectedItemMutationName);

                            string mutationAA = selection.Select(m => ((MutationObject)m).mutationAA).FirstOrDefault();
                            string resultSignificance = selection.Select(m => ((MutationObject)m).significance).FirstOrDefault();
                            string externalMutationID = selection.Select(m => ((MutationObject)m).externalID).FirstOrDefault();

                            if (!string.IsNullOrEmpty(mutationAA))
                            {
                                if (string.IsNullOrEmpty(geneticTestResult.mutationAA))
                                {
                                    geneticTestResult.mutationAA = mutationAA;
                                    args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
                                }
                            }
                            if (!string.IsNullOrEmpty(resultSignificance))
                            {
                                if (string.IsNullOrEmpty(geneticTestResult.resultSignificance) || geneticTestResult.resultSignificance.ToLower().StartsWith("neg"))
                                {
                                    geneticTestResult.resultSignificance = resultSignificance;
                                    args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));
                                }
                            }
                            if (!string.IsNullOrEmpty(externalMutationID))
                            {
                                if (string.IsNullOrEmpty(geneticTestResult.externalMutationID))
                                {
                                    geneticTestResult.externalMutationID = externalMutationID;
                                    args.updatedMembers.Add(geneticTestResult.GetMemberByName("externalMutationID"));
                                }
                            }
                        }

                        args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));
                        geneticTestResult.SignalModelChanged(args);

                        SignalOwningTestCompleted();
                    }
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            MutationDeltaPopup mdp = new MutationDeltaPopup();
            mdp.ShowDialog();
        }

        private void geneNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (initialized)
            {
                if (string.IsNullOrEmpty(geneticTestResult.geneName) == false)
                    if (geneticTestResult.geneName == geneNameComboBox.Text)
                        return;

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.geneName = geneNameComboBox.Text;

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("geneName"));
                geneticTestResult.SignalModelChanged(args);

                populateMutationComboBoxes();

                SignalOwningTestCompleted();
            }
        }

        private void geneNameComboBox_Validated(object sender, EventArgs e)
        {
            if (initialized)
            {
                if (string.IsNullOrEmpty(geneticTestResult.geneName)==false)
                    if (geneticTestResult.geneName == geneNameComboBox.Text)
                        return;

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                geneticTestResult.geneName = geneNameComboBox.Text;

                args.updatedMembers.Add(geneticTestResult.GetMemberByName("geneName"));
                geneticTestResult.SignalModelChanged(args);

                populateMutationComboBoxes();

                SignalOwningTestCompleted();
            }
        }
    }
}



/////////////////////////////////

//if (this.mutationNameComboBox.Items.Count != 0)
//{
//    //if this fails, the Validated even handler should fire
//    string selectedItemGeneName = geneNameComboBox.SelectedItem.ToString();
//    string selectedItemMutationName = mutationNameComboBox.SelectedItem.ToString();

//    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
//    geneticTestResult.mutationName = selectedItemMutationName;

//    if (
//        !string.IsNullOrEmpty(selectedItemGeneName) &&
//        !string.IsNullOrEmpty(selectedItemMutationName))
//    {
//        var selection = SessionManager.Instance.MetaData.Mutations
//            .Where(m =>
//                ((MutationObject)m).geneName == selectedItemGeneName &&
//                ((MutationObject)m).mutationDNA == selectedItemMutationName);

//        string mutationAA = selection.Select(m => ((MutationObject)m).mutationAA).FirstOrDefault();
//        string resultSignificance = selection.Select(m => ((MutationObject)m).significance).FirstOrDefault();
//        string externalMutationID = selection.Select(m => ((MutationObject)m).externalID).FirstOrDefault();

//        if (!string.IsNullOrEmpty(mutationAA))
//        {
//            geneticTestResult.mutationAA = mutationAA;
//            args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
//        }
//        if (!string.IsNullOrEmpty(resultSignificance))
//        {
//            geneticTestResult.resultSignificance = resultSignificance;
//            args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));
//        }
//        if (!string.IsNullOrEmpty(externalMutationID))
//        {
//            geneticTestResult.externalMutationID = externalMutationID;
//            args.updatedMembers.Add(geneticTestResult.GetMemberByName("externalMutationID"));
//        }
//    }

//    args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));
//    geneticTestResult.SignalModelChanged(args);

//    SignalOwningTestCompleted(); 
//}









/*
if (this.mutationAAComboBox.Items.Count != 0)
{
    if (geneNameComboBox.SelectedItem != null)
    {
        string selectedGeneName = geneNameComboBox.SelectedItem.ToString();
        string selectedMutationAA = mutationAAComboBox.SelectedItem.ToString();

        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
        geneticTestResult.mutationAA = selectedMutationAA;

        if (
            !string.IsNullOrEmpty(selectedGeneName) &&
            !string.IsNullOrEmpty(selectedMutationAA))
        {
            var selection = SessionManager.Instance.MetaData.Mutations
                .Where(m =>
                    ((MutationObject)m).geneName == selectedGeneName &&
                    ((MutationObject)m).mutationDNA == selectedMutationAA);

            string mutationDNA = selection.Select(m => ((MutationObject)m).mutationDNA).FirstOrDefault();
            string significance = selection.Select(m => ((MutationObject)m).significance).FirstOrDefault();
            string externalId = selection.Select(m => ((MutationObject)m).externalID).FirstOrDefault();

            if (!string.IsNullOrEmpty(mutationDNA))
            {
                geneticTestResult.mutationName = mutationDNA;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationName"));
            }
            if (!string.IsNullOrEmpty(significance))
            {
                geneticTestResult.resultSignificance = significance;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("resultSignificance"));
            }
            if (!string.IsNullOrEmpty(externalId))
            {
                geneticTestResult.externalMutationID = externalId;
                args.updatedMembers.Add(geneticTestResult.GetMemberByName("externalMutationID"));
            }
        }

        args.updatedMembers.Add(geneticTestResult.GetMemberByName("mutationAA"));
        geneticTestResult.SignalModelChanged(args);

        SignalOwningTestCompleted();
    }
}
 */
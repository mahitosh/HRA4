using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.View.PatientRecord.PMH
{
    public partial class PMHRow : UserControl
    {   
        private ClincalObservation co;
        private HraView owningView;
        public RiskApps3.MainForm.PushViewCallbackType AddViewToParent;

        public PMHRow(ClincalObservation co, HraView ParentView)
        {
            this.co = co;
            InitializeComponent();
            owningView = ParentView;
            disease.Text = co.disease;
            ageDiagnosis.Text = co.ageDiagnosis;
            comments.Text = co.comments;
        }

        public ClincalObservation GetCO()
        {
            return co;
        }

        private void ageDiagnosis_Validated(object sender, EventArgs e)
        {
            co.Set_ageDiagnosis(ageDiagnosis.Text, this.owningView);
        }

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            co.owningPMH.Observations.RemoveFromList(co,SessionManager.Instance.securityContext);
        }

        private void comments_Validated(object sender, EventArgs e)
        {
            co.Set_comments(comments.Text, this.owningView);
        }

        public void setGroupID(int groupID)
        {
            string[] diseaseList = SessionManager.Instance.MetaData.Diseases
                .Where(
                    d => 
                        (((DiseaseObject)d).groupingID == groupID || groupID == 0) && 
                        (
                            ((DiseaseObject)d).diseaseGender.Equals(co.owningPMH.RelativeOwningPMH.gender) ||
                            ((DiseaseObject)d).diseaseGender.Equals("Both")
                        )
                    )
                .Select(d => ((DiseaseObject)d).diseaseName).Distinct()
                .ToArray();

            Array.Sort(diseaseList);

            disease.Items.Clear();
            disease.AutoCompleteItems.Clear();

            disease.Items.AddRange(diseaseList);
            disease.AutoCompleteItems.AddRange(
                diseaseList
                    .Select(n => new AutoCompleteEntry(n, n))
                    .ToArray());
        }

        private void disease_Validated(object sender, EventArgs e)
        {
            co.Set_disease(disease.Text, this.owningView);
            //SessionManager.Instance.MetaData.Diseases.SetDataFromDiseaseName(ref co);
            //if (co.disease.Contains("Breast Cancer"))
            //{
            //    co.Details = new BreastCancerDetails();
            //    co.Details.owningClincalObservation = co;
            //    co.Details.BackgroundLoadWork();
            //}
            //else
            //{
            //    co.Details = null;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (co.Details != null)
            {
                if (co.Details.GetType().ToString() == "RiskApps3.Model.PatientRecord.PMH.BreastCancerDetails")
                {
                    BreastCancerDetailsView bcdv = new BreastCancerDetailsView((BreastCancerDetails)(co.Details));
                    bcdv.ShowDialog();
                }
                if (co.Details.GetType().ToString() == "RiskApps3.Model.PatientRecord.PMH.ColonCancerDetails")
                {
                    ColonCancerDetailsView bcdv = new ColonCancerDetailsView((ColonCancerDetails)(co.Details));
                    bcdv.ShowDialog();
                }
            }
        }
    }
}
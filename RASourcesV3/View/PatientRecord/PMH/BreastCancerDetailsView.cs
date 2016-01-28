using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.View.PatientRecord.PMH
{
    public partial class BreastCancerDetailsView : HraView
    {
        private BreastCancerDetails details;

        public BreastCancerDetailsView(BreastCancerDetails Details)
        {
            details = Details;
            InitializeComponent();
        }

        private void BreastCancerDetailsView_Load(object sender, EventArgs e)
        {
            int y = DateTime.Now.Year;
            for (int i = -2; i < 100; i++)
            {
                surgeryYearComboBox.Items.Add(y - i);
                diagnosisYearComboBox.Items.Add(y - i);
            }
            sideComboBox.Text = details.BreastCancerDetails_side;
            diagnosisMonthComboBox.Text = details.BreastCancerDetails_diagnosisMonth;
            diagnosisYearComboBox.Text = details.BreastCancerDetails_diagnosisYear;
            surgeryMonthComboBox.Text = details.BreastCancerDetails_surgeryMonth;
            surgeryYearComboBox.Text = details.BreastCancerDetails_surgeryYear;
            breastSurgeryComboBox.Text = details.BreastCancerDetails_breastSurgery;
            axillarySurgeryComboBox.Text = details.BreastCancerDetails_axillarySurgery;
            reconstructionComboBox.Text = details.BreastCancerDetails_reconstruction;
            invasiveHistologyComboBox.Text = details.BreastCancerDetails_invasiveHistology;
            invasiveHistologyGradeComboBox.Text = details.BreastCancerDetails_invasiveHistologyGrade;
            sizeCMMaskedTextBox.Text = details.BreastCancerDetails_sizeCM;
            ERStatusComboBox.Text = details.BreastCancerDetails_ERStatus;
            ERPercentTextBox.Text = details.BreastCancerDetails_ERPercent;
            ERIntensityTextBox.Text = details.BreastCancerDetails_ERIntensity;
            PRStatusComboBox.Text = details.BreastCancerDetails_PRStatus;
            PRPercentTextBox.Text = details.BreastCancerDetails_PRPercent;
            PRIntensityTextBox.Text = details.BreastCancerDetails_PRIntensity;
            her2neuIHCComboBox.Text = details.BreastCancerDetails_Her2NeuIHC;
            //HER2IntensityTextBox.Text = details.BreastCancerDetails_HER2Intensity;
            //HER2PercentTextBox.Text = details.BreastCancerDetails_HER2Percent;
            //her2neuFISHComboBox.Text = details.BreastCancerDetails_Her2NeuFISH;
            DCISHistologyComboBox.Text = details.BreastCancerDetails_DCISHistology;
            DCISNucGradeComboBox.Text = details.BreastCancerDetails_DCISNucGrade;
            DCISSizeTextBox.Text = details.BreastCancerDetails_DCISSize;
            DCISERStatusComboBox.Text = details.BreastCancerDetails_DCISERStatus;
            DCISERPercentTextBox.Text = details.BreastCancerDetails_DCISERPercent;
            DCISERIntensityTextBox.Text = details.BreastCancerDetails_DCISERIntensity;
            DCISPRStatusComboBox.Text = details.BreastCancerDetails_DCISPRStatus;
            DCISPRPercentTextBox.Text = details.BreastCancerDetails_DCISPRPercent;
            DCISPRIntensityTextBox.Text = details.BreastCancerDetails_DCISPRIntensity;
            commentsTextBox.Text = details.owningClincalObservation.comments;

            UpdateDCISControls();
            UpdateInvasiveControls();
        }

        private void sideComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_side = sideComboBox.Text;
        }

        private void diagnosisMonthComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_diagnosisMonth = diagnosisMonthComboBox.Text;
        }

        private void diagnosisYearComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_diagnosisYear = diagnosisYearComboBox.Text;
        }

        private void surgeryMonthComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_surgeryMonth = surgeryMonthComboBox.Text;
        }

        private void surgeryYearComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_surgeryYear = surgeryYearComboBox.Text;
        }

        private void breastSurgeryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_breastSurgery = breastSurgeryComboBox.Text;
        }

        private void axillarySurgeryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_axillarySurgery = axillarySurgeryComboBox.Text;
        }

        private void reconstructionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_reconstruction = reconstructionComboBox.Text;
        }

        private void invasiveHistologyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_invasiveHistology = invasiveHistologyComboBox.Text;
            UpdateInvasiveControls();
        }

        private void sizeCMMaskedTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_sizeCM = sizeCMMaskedTextBox.Text;
        }

        private void ERPercentTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_ERPercent = ERPercentTextBox.Text;
        }


        private void PRPercentTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_PRPercent = PRPercentTextBox.Text;
        }


        private void DCISHistologyComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISHistology = DCISHistologyComboBox.Text;

            UpdateDCISControls();
        }

        private void UpdateInvasiveControls()
        {
            if (string.IsNullOrEmpty(invasiveHistologyComboBox.Text))
            {
                invasiveHistologyGradeComboBox.Enabled = false;
                sizeCMMaskedTextBox.Enabled = false;
                ERStatusComboBox.Enabled = false;
                ERPercentTextBox.Enabled = false;
                ERIntensityTextBox.Enabled = false;
                PRStatusComboBox.Enabled = false;
                PRPercentTextBox.Enabled = false;
                PRIntensityTextBox.Enabled = false;
                her2neuIHCComboBox.Enabled = false;

                invasiveHistologyGradeComboBox.Text = "";
                sizeCMMaskedTextBox.Text = "";
                ERStatusComboBox.Text = "";
                ERPercentTextBox.Text = "";
                ERIntensityTextBox.Text = "";
                PRStatusComboBox.Text = "";
                PRPercentTextBox.Text = "";
                PRIntensityTextBox.Text = "";
                her2neuIHCComboBox.Text = "";
            }
            else
            {
                invasiveHistologyGradeComboBox.Enabled = true;
                sizeCMMaskedTextBox.Enabled = true;
                ERStatusComboBox.Enabled = true;
                ERPercentTextBox.Enabled = true;
                ERIntensityTextBox.Enabled = true;
                PRStatusComboBox.Enabled = true;
                PRPercentTextBox.Enabled = true;
                PRIntensityTextBox.Enabled = true;
                her2neuIHCComboBox.Enabled = true;
            }
        }

        private void UpdateDCISControls()
        {
            if (string.IsNullOrEmpty(DCISHistologyComboBox.Text))
            {
                DCISSizeTextBox.Enabled = false;
                DCISNucGradeComboBox.Enabled = false;
                groupBox9.Enabled = false;
                groupBox10.Enabled = false;
                DCISERStatusComboBox.Enabled = false;
                DCISPRStatusComboBox.Enabled = false;

                DCISSizeTextBox.Text = "";
                DCISNucGradeComboBox.Text = "";
                DCISERPercentTextBox.Text = "";
                DCISERIntensityTextBox.Text = "";
                DCISPRPercentTextBox.Text = "";
                DCISPRIntensityTextBox.Text = "";
                DCISERStatusComboBox.Text = "";
                DCISPRStatusComboBox.Text = "";
            }
            else
            {
                DCISSizeTextBox.Enabled = true;
                DCISNucGradeComboBox.Enabled = true;
                groupBox9.Enabled = true;
                groupBox10.Enabled = true;
                DCISERStatusComboBox.Enabled = true;
                DCISPRStatusComboBox.Enabled = true;
            }
        }

        private void DCISSizeTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISSize = DCISSizeTextBox.Text;
        }

        private void DCISERPercentTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISERPercent = DCISERPercentTextBox.Text;
        }

        private void DCISPRPercentTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISPRPercent = DCISPRPercentTextBox.Text;
        }


        private void commentsTextBox_Validated(object sender, EventArgs e)
        {
            details.owningClincalObservation.Set_comments(commentsTextBox.Text, this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            this.Close();
        }

        private void DCISNucGradeComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISNucGrade = DCISNucGradeComboBox.Text;
        }

        private void DCISERStatusComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISERStatus = DCISERStatusComboBox.Text;
        }

        private void DCISERIntensityTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISERIntensity = DCISERIntensityTextBox.Text;
        }

        private void DCISPRStatusComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISPRStatus = DCISPRStatusComboBox.Text;
        }

        private void DCISPRIntensityTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_DCISPRIntensity = DCISPRIntensityTextBox.Text;
        }

        private void invasiveHistologyGradeComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_invasiveHistologyGrade = invasiveHistologyGradeComboBox.Text;
        }


        private void ERIntensityTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_ERIntensity = ERIntensityTextBox.Text;
        }

        private void ERStatusComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_ERStatus = ERStatusComboBox.Text;
        }

        private void PRStatusComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_PRStatus = PRStatusComboBox.Text;
        }

        private void PRIntensityTextBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_PRIntensity = PRIntensityTextBox.Text;
        }

        private void her2neuIHCComboBox_Validated(object sender, EventArgs e)
        {
            details.BreastCancerDetails_Her2NeuIHC = her2neuIHCComboBox.Text;
        }
    }
}
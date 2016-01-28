using System;
using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.View.PatientRecord.PMH
{
    public partial class ColonCancerDetailsView : HraView
    {
        private ColonCancerDetails details;

        public ColonCancerDetailsView(ColonCancerDetails Details)
        {
            details = Details;
            InitializeComponent();
        }

        private void BreastCancerDetailsView_Load(object sender, EventArgs e)
        {
            int y = DateTime.Now.Year;
            diagnosisYearComboBox.Items.Add("");
            for (int i = -2; i < 100; i++)
            {
                diagnosisYearComboBox.Items.Add(y - i);
            }

            diagnosisMonthComboBox.Text = "" + details.ColonCancerDetails_diagnosisMonth;
            diagnosisYearComboBox.Text = "" + details.ColonCancerDetails_diagnosisYear;

            msiResultsComboBox.Text = details.ColonCancerDetails_msiResults;
            ihcComboBox.Text = details.ColonCancerDetails_immunohistochemistry;
            locationComboBox.Text = details.ColonCancerDetails_location;

            BRAFV600EComboBox.Text = details.ColonCancerDetails_BRAFV600E;
            MLH1PromoterMethylationComboBox.Text = details.ColonCancerDetails_MLH1PromoterMethylation;

            PMS2AbsentCheckBox.Checked = details.ColonCancerDetails_PMS2Absent;
            MSH2AbsentCheckBox.Checked = details.ColonCancerDetails_MSH2Absent;
            MSH6AbsentCheckBox.Checked = details.ColonCancerDetails_MSH6Absent;
            MLH1AbsentCheckBox.Checked = details.ColonCancerDetails_MLH1Absent;

            commentsTextBox.Text = details.owningClincalObservation.comments;
            updateIHCCheckBoxes();
        }


        private void diagnosisMonthComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (diagnosisMonthComboBox.Text == "")
            {
                details.ColonCancerDetails_diagnosisMonth = -1;
            }
            else
            {
                details.ColonCancerDetails_diagnosisMonth = int.Parse(diagnosisMonthComboBox.Text);
            }
        }

        private void diagnosisYearComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (diagnosisYearComboBox.Text == "")
            {
                details.ColonCancerDetails_diagnosisYear = -1;
            }
            else
            {
                details.ColonCancerDetails_diagnosisYear = int.Parse(diagnosisYearComboBox.Text);
            }
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

        private void updateIHCCheckBoxes()
        {
            if (ihcComboBox.Text == "Absent")
            {
                label8.Visible = true;
                PMS2AbsentCheckBox.Visible = true;
                MSH2AbsentCheckBox.Visible = true;
                MSH6AbsentCheckBox.Visible = true;
                MLH1AbsentCheckBox.Visible = true;
            }
            else
            {
                label8.Visible = false;
                PMS2AbsentCheckBox.Visible = false;
                MSH2AbsentCheckBox.Visible = false;
                MSH6AbsentCheckBox.Visible = false;
                MLH1AbsentCheckBox.Visible = false;  
            }
        }
        private void msiResultsComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.ColonCancerDetails_msiResults = msiResultsComboBox.Text;
        }

        private void ihcComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.ColonCancerDetails_immunohistochemistry = ihcComboBox.Text;
            updateIHCCheckBoxes();
        }

        private void locationComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.ColonCancerDetails_location = locationComboBox.Text;
        }

        private void BRAFV600EComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.ColonCancerDetails_BRAFV600E = BRAFV600EComboBox.Text;
        }

        private void MLH1PromoterMethylationComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            details.ColonCancerDetails_MLH1PromoterMethylation = MLH1PromoterMethylationComboBox.Text;
        }

        private void PMS2AbsentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            details.ColonCancerDetails_PMS2Absent = PMS2AbsentCheckBox.Checked;
        }

        private void MSH2AbsentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            details.ColonCancerDetails_MSH2Absent = MSH2AbsentCheckBox.Checked;
        }

        private void MSH6AbsentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            details.ColonCancerDetails_MSH6Absent = MSH6AbsentCheckBox.Checked;
        }

        private void MLH1AbsentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            details.ColonCancerDetails_MLH1Absent = MLH1AbsentCheckBox.Checked;
        }

        private void ihcComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.Admin
{
    public partial class ActiveSurveyRow : UserControl
    {
        ActiveSurvey survey;
        bool initializing = false;

        public ActiveSurveyRow()
        {
            InitializeComponent();
        }
        public ActiveSurveyRow(ActiveSurvey survey)
        {
            initializing = true;

            InitializeComponent();
            this.survey = survey;
            this.surveyIdLabel.Text = survey.surveyID.ToString();
            this.surveyNameLabel.Text = survey.surveyName;

            if (survey.inactive == false)
            {
                this.checkBox1.Checked = true;
                checkBox1.Text = "Active";
            }
            else
            {
                this.checkBox1.Checked = false;
                checkBox1.Text = "Inactive";
            }
            initializing = false;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (initializing)
            {
                return;
            }
            if (this.checkBox1.Checked)
            {
                survey.inactive = false;
                checkBox1.Text = "Active";
            }
            else
            {
                survey.inactive = true;
                checkBox1.Text = "Inactive";
            }
            RiskApps3.Model.HraModelChangedEventArgs args = new RiskApps3.Model.HraModelChangedEventArgs(null);

            survey.BackgroundPersistWork(args);
        }
    }
}

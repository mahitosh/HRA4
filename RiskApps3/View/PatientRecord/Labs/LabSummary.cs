using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Labs;

namespace RiskApps3.View.PatientRecord.Labs
{
    public partial class LabSummary : UserControl
    {
        LabResult theStudy;

        public LabSummary(LabResult lr)
        {
            theStudy = lr;

            InitializeComponent();
            textBox1.Text = theStudy.LabResult_Res;
            comboBox2.Text = theStudy.LabResult_RU;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            theStudy.LabResult_RU = comboBox2.Text;
        }

        private void comboBox2_Validated(object sender, EventArgs e)
        {
            theStudy.LabResult_RU = comboBox2.Text;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            theStudy.LabResult_Res = textBox1.Text;
        }
    }
}

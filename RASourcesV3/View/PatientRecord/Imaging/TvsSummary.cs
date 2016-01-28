using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.PatientRecord.Imaging
{
    public partial class TvsSummary : UserControl
    {
        TransvaginalImagingStudy theStudy;

        public TvsSummary(TransvaginalImagingStudy tvs)
        {
            theStudy = tvs;
            InitializeComponent();
            comboBox1.Text = theStudy.Tvs_impression;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            theStudy.Tvs_impression = comboBox1.Text;
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            theStudy.Tvs_impression = comboBox1.Text;
        }
    }
}

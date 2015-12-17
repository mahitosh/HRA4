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
    public partial class MammographySummary : UserControl
    {
        BreastImagingStudy theStudy;

        public MammographySummary(BreastImagingStudy bis)
        {
            theStudy = bis;
            InitializeComponent();

            comboBox1.Text = bis.BreastImaging_side;
            comboBox2.Text = bis.BreastImaging_leftBirads;
            comboBox3.Text = bis.BreastImaging_rightBirads;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            theStudy.BreastImaging_leftBirads = comboBox2.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            theStudy.BreastImaging_side = comboBox1.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            theStudy.BreastImaging_rightBirads = comboBox3.Text;
        }
    }
}

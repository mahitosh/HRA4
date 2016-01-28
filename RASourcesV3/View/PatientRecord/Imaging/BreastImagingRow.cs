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
    public partial class BreastImagingRow : UserControl
    {
        private int ScrollIntervalValue = 5;
        private int HeaderHeightValue = 27;
        public HraView owningView;

        BreastImagingStudy study;

        public BreastImagingRow(BreastImagingStudy bis)
        {
            study = bis;
            InitializeComponent();
            dateTimePicker1.Value = study.date;
            if (string.IsNullOrEmpty(study.report) == false)
            {
                richTextBox1.Text = study.report;
            }
            if (string.IsNullOrEmpty(study.imagingType) == false)
            {
                comboBox1.Text = study.imagingType;
            }
            if (string.IsNullOrEmpty(study.leftBirads) == false)
            {
                comboBox2.Text = study.leftBirads;
            }
            if (string.IsNullOrEmpty(study.rightBirads) == false)
            {
                comboBox3.Text = study.rightBirads;
            }
            if (string.IsNullOrEmpty(study.BIRADS) == false)
            {
                comboBox4.Text = study.BIRADS;
            }
            if (string.IsNullOrEmpty(study.normal) == false)
            {
                if (study.normal.ToLower() == "yes")
                    checkBox1.Checked = true;
                else
                    checkBox1.Checked = false;
            }

        }

        public void SetScrollState(bool expanded)
        {
            if (expanded)
            {
                this.Height = richTextBox1.Location.Y + richTextBox1.Height + 3;
            }
            else
            {
                this.Height = this.lblTop.Height;
            }
        }
        private void DoScroll()
        {
            if (this.Height > this.lblTop.Height)
            {
                while (this.Height > this.lblTop.Height)
                {
                    Application.DoEvents();
                    this.Height -= ScrollIntervalValue;
                }
                this.lblTop.ImageIndex = 1;
                this.Height = this.lblTop.Height;
            }
            else if (this.Height == this.lblTop.Height)
            {
                //  int x = this.FixedHeight;
                int x = richTextBox1.Location.Y + richTextBox1.Height + 3;
                while (this.Height <= (x))
                {
                    Application.DoEvents();
                    this.Height += ScrollIntervalValue;
                }
                this.lblTop.ImageIndex = 0;
                this.Height = x;
            }
        }

        private void lblTop_Click(object sender, EventArgs e)
        {
            DoScroll();
        }

        private void DataField_Leave(object sender, EventArgs e)
        {
            if (study != null)
            {
                study.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(owningView));
            }
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            study.leftBirads = comboBox2.Text;
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            study.rightBirads = comboBox3.Text;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            study.imagingType = comboBox1.Text;
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            study.BIRADS = comboBox4.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                study.normal = "Yes";
            else
                study.normal = "No";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            study.date = dateTimePicker1.Value;
        }
    }
}

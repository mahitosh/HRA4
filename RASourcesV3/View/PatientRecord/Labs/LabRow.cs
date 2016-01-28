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
    public partial class LabRow : UserControl
    {
        private int ScrollIntervalValue = 5;
        private int HeaderHeightValue = 27;
        public HraView owningView;

        LabResult result;

        public LabRow(LabResult lab)
        {
            result = lab;
            InitializeComponent();
            if (result != null)
            {
                if (string.IsNullOrEmpty(result.unitnum) == false)
                {
                    dateTimePicker1.Value = result.date;
                    comboBox1.Text = result.TestShort;
                    textBox1.Text = result.Res;
                    textBox5.Text = result.RU;
                    if (string.IsNullOrEmpty(result.AbnFlg))
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;
                    comboBox2.Text = result.CDRTestClass;
                    textBox6.Text = result.RRR;
                    textBox2.Text = result.TOXRNG;
                    textBox3.Text = result.SpecMID;
                    textBox4.Text = result.Loinc;
                    textBox7.Text = result.Com;
                }
            }

        }

        public void SetScrollState(bool expanded)
        {
            if (expanded)
            {
                this.Height = panel1.Location.Y + panel1.Height + 3;
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
                int x = panel1.Location.Y + panel1.Height + 3;
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
            if (result != null)
            {
                result.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(owningView));
            }
        }
    }
}

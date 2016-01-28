using RiskApps3.Model.PatientRecord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord
{
    public partial class AddImagingForm : Form
    {
        public Diagnostic study;    //TODO is this ever not a BreastImagingStudy???
        public Patient proband;

        public AddImagingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (study != null)
            {
                if (study is BreastImagingStudy)
                {
                    BreastImagingStudy bis = (BreastImagingStudy)study;
                    bis.date = dateTimePicker1.Value;
                    bis.normal = comboBox1.Text;
                    bis.status = comboBox5.Text;
                    bis.side = comboBox2.Text;
                    bis.leftBirads = comboBox4.Text;
                    bis.rightBirads = comboBox3.Text;
                    bis.report = richTextBox1.Text;
                }
            }

            if (proband != null)
            {
                BreastImagingStudy imagingStudy = study as BreastImagingStudy;
                if (imagingStudy != null)
                {
                    BreastImagingStudy bis = imagingStudy;
                    proband.breastImagingHx.AddToList(bis, new Model.HraModelChangedEventArgs(null));
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddImagingForm_Load(object sender, EventArgs e)
        {
            if (study != null)
            {
                if (study is BreastImagingStudy)
                {
                    BreastImagingStudy bis = (BreastImagingStudy)study;
                    dateTimePicker1.Value = bis.date;
                    comboBox1.Text = bis.normal;
                    comboBox5.Text = bis.status;
                    comboBox2.Text = bis.side;
                    comboBox4.Text = bis.leftBirads;
                    comboBox3.Text = bis.rightBirads;

                    richTextBox1.Text = bis.report;
                }
            }
        }
    }
}

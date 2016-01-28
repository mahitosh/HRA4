using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;

namespace RiskApps3.View.RiskClinic.PatientSummary
{
    public partial class SummaryItemRow : UserControl
    {
        private int ScrollIntervalValue = 5;
        public HraView parentView;
        Diagnostic test;
        
        public event EventHandler DeleteClicked;

        public SummaryItemRow(Diagnostic theTest, HraView view)
        {
            parentView = view;
            InitializeComponent();
            
            test = theTest;
            if(theTest.date > DateTime.MinValue)
                dateTimePicker1.Value = theTest.date;


            comboBox1.Text = test.normal;
            //richTextBox1.Text = test.summary;
            comboBox2.Text = test.value;
            comboBox3.Text = test.status;
        }

        public Diagnostic GetDiagnostic()
        {
            return test;
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            test.date = dateTimePicker1.Value;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            test.normal = comboBox1.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            //test.summary = richTextBox1.Text;
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            test.value = comboBox2.Text;
        }

        private void comboBox2_Leave_1(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }
        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }
        //public void SetScrollState(bool expanded)
        //{
        //    if (expanded)
        //    {
        //        this.Height = dropDown.Location.Y + dropDown.Height + 3;
        //    }
        //    else
        //    {
        //        this.Height = this.header.Height;
        //    }
        //}
        //private void DoScroll()
        //{
        //    if (this.Height > this.header.Height)
        //    {
        //        while (this.Height > this.header.Height)
        //        {
        //            Application.DoEvents();
        //            this.Height -= ScrollIntervalValue;
        //        }
        //        this.Height = this.header.Height;
        //    }
        //    else if (this.Height == this.header.Height)
        //    {
        //        //  int x = this.FixedHeight;
        //        int x = richTextBox1.Location.Y + dropDown.Height + 3;
        //        while (this.Height <= (x))
        //        {
        //            Application.DoEvents();
        //            this.Height += ScrollIntervalValue;
        //        }
        //        this.Height = x;
        //    }
        //}

        //private void header_Click(object sender, EventArgs e)
        //{
        //    DoScroll();
        //}

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            test.status = comboBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(parentView);
            args.Delete = true;

            test.SignalModelChanged(args);

            if (DeleteClicked != null)
                DeleteClicked(this, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FullReportTextPopup frtp = new FullReportTextPopup();
            frtp.FullReportText = test.report;
            frtp.ShowDialog();
            if (frtp.FullReportText != test.report)
            {
                test.report = frtp.FullReportText;
                test.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(parentView));
            }
        }




    }
}

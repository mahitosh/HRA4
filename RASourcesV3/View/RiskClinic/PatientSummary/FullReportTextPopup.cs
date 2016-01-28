using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.RiskClinic.PatientSummary
{
    public partial class FullReportTextPopup : Form
    {
        public string FullReportText
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }

        public FullReportTextPopup()
        {
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Reporting
{
    public partial class ReportInfoMessage : Form
    {
        public ReportInfoMessage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetText(string text)
        {
            richTextBox1.Text = text;
        }
    }
}

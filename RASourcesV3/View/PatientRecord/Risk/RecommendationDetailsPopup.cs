using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class RecommendationDetailsPopup : Form
    {
        public RecommendationDetailsPopup(string bullet, string paragraph)
        {
            InitializeComponent();
            richTextBox1.Text = bullet;
            richTextBox2.Text = paragraph;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

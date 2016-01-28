using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Common
{
    public partial class LegacyApptForm : Form
    {
        public DateTime selected = DateTime.MinValue;
        public DateTime golden = DateTime.MinValue;

        public string result;

        public LegacyApptForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = "Continue";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result = "Use Golden";
            this.Close();
        }

        private void LegacyApptForm_Load(object sender, EventArgs e)
        {
            if (selected > DateTime.MinValue)
            {
                label5.Text = selected.ToString();
            }
            if (golden > DateTime.MinValue)
            {
                label6.Text = golden.ToString();
            }
        }
    }
}

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
    public partial class CloseAppValidationForm : Form
    {
        public string lastView = "";

        public CloseAppValidationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        private void CloseAppValidationForm_Load(object sender, EventArgs e)
        {
            if (lastView.Length > 0)
            {
                button3.Text = lastView;
                //button3.Width = TextRenderer.MeasureText(button3.Text, button3.Font).Width + 7;
            }
        }
    }
}

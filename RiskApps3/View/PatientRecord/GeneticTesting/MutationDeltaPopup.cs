using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    public partial class MutationDeltaPopup : Form
    {
        public MutationDeltaPopup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MutationDeltaPopup_Load(object sender, EventArgs e)
        {
            label1.Text = "";
        }
    }
}

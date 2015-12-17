using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord
{
    public partial class AddingFilesForm : Form
    {
        public int totalFileCount = 0;
        public int currentFile = 0;

        public AddingFilesForm()
        {
            InitializeComponent();
        }
        public void SetLabelText(string msg)
        {
            label1.Text = msg;
        }
        public void SetProgressPercent(int percent)
        {
            progressBar1.Value = percent;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace RiskApps3.Model.PatientRecord
{
    public partial class RunCdsForm : Form
    {
        public int apptid;
        public string unitnum;

        public RunCdsForm()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            RiskAppCore.Globals.setApptID(apptid);
            RiskAppCore.Globals.setUnitNum(unitnum);
            Process process = new Process();
            process.StartInfo.FileName = "RiskAppUtils.exe";
            process.StartInfo.Arguments = "runDecisionSupport " + apptid.ToString();
            process.Start();

            process.WaitForExit();

            RiskAppCore.Globals.setApptID(-1);
            RiskAppCore.Globals.setUnitNum("");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Close();
        }

        private void RunCdsForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }
    }
}

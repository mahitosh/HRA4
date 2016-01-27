using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.View.RiskClinic
{
    public partial class MarkStartedAndPullForwardForm : Form
    {
        int apptid;
        string unitnum = "";

        public MarkStartedAndPullForwardForm(int p_apptid, string p_unitnum)
        {
            InitializeComponent();
            apptid = p_apptid;
            unitnum = p_unitnum;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            LegacyCoreAPI.StartWorkingWithAppointment(apptid, "");

            ParameterCollection pc = new ParameterCollection();
            pc.Add("application", "RiskApps3");
            pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);
            pc.Add("machineName", System.Environment.MachineName);
            pc.Add("message", "Accessed appointment");
            pc.Add("apptID", apptid);

            BCDB2.Instance.RunSPWithParams("sp_3_AuditUserActivity", pc);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SessionManager.Instance.ClearActivePatient();
            SessionManager.Instance.SetActivePatientNoCallback(unitnum, apptid);

            this.Close();
        }

        private void MarkStartedAndPullForwardForm_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Enabled = true;
            backgroundWorker1.RunWorkerAsync();
        }
    }
}

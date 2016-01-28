using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.Utilities
{
    public partial class RunRiskModelsDialog : Form
    {
        private bool bRunAutomation = false;

        public RunRiskModelsDialog(bool runAutomation)
        {
            bRunAutomation = runAutomation; // new constructor jdg 8/31/15
            InitializeComponent();
        }
        public RunRiskModelsDialog()
        {
            InitializeComponent();
        }

        private void RunRiskModelsDialog_Load(object sender, EventArgs e)
        {
            Patient proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
                backgroundWorker1.RunWorkerAsync(proband);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Patient proband = (Patient)e.Result;

            foreach (Person p in proband.FHx.Relatives)
            {
                p.RP.ForceLoadedEvent();
            }
            proband.RP.BracproCancerRisk.ForceListLoadedEvent();
            proband.RP.MmrproCancerRiskList.ForceListLoadedEvent();
            proband.RP.GailModel.ForceListLoadedEvent();
            proband.RP.ClausModel.ForceListLoadedEvent();
            proband.RP.TyrerCuzickModel.ForceListLoadedEvent();
            proband.RP.TyrerCuzickModel_v7.ForceListLoadedEvent();

            proband.RP.CCRATModel.ForceListLoadedEvent();
            proband.RP.NCCNGuideline.ForceListLoadedEvent();

            this.Close();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Patient proband = (Patient)e.Argument;
            proband.RecalculateRisk();
            if (bRunAutomation) // jdg 8/31/15
            {
                try
                {
                    proband.RunAutomation();
                }
                catch (Exception e2)
                {
                    // eat me
                    Logger.Instance.WriteToLog("Failed to run automation for appointment #" + proband.apptid.ToString() + ".  Underlying error = " + e2.Message); 
                }
            }
            e.Result = proband;
        }
    }
}

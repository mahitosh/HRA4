using System;
using System.ComponentModel;
using System.Drawing;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Windows.Forms;
using System.Reflection;
using RiskApps3.Controllers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.Utilities;


namespace RiskApps3.View.Risk
{
    public partial class RiskScoresView : HraView
    {
        /**************************************************************************************************/
        //private Person selectedRelative;
        private Patient proband;

        /**************************************************************************************************/
        public RiskScoresView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void RiskScoresView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            InitActivePatient();
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitActivePatient();
        }

        /**************************************************************************************************/
        private void InitActivePatient()
        {
            mutBRCA.Text = "";
            mutMyriad.Text = "";
            mutTC.Text = "";
            mutTCv7.Text = ""; 
            riskBC5Yr_Brca.Text = "";
            riskBCLifetime_Brca.Text = "";
            riskBC5Yr_Claus.Text = "";
            riskBCLifetime_Claus.Text = "";
            riskBC5Yr_Gail.Text = "";
            riskBCLifetime_Gail.Text = "";
            riskBC5Yr_TC.Text = "";
            riskBCLifetime_TC.Text = "";
            riskBC5Yr_TCv7.Text = "";
            riskBCLifetime_TCv7.Text = ""; 
            riskOC5Yr_Brca.Text = "";
            riskOCLifetime_Brca.Text = "";

            mutMMRPRO.Text = "";
            mutPREMM.Text = "";
            riskCC5Yr_mmrpro.Text = "";
            riskCC5Yr_CCRAT.Text = "";
            riskCCLifetime_mmrpro.Text = "";
            riskCCLifetime_CCRAT.Text = "";
            riskEC5Yr_mmrpro.Text = "";
            riskECLifetime_mmrpro.Text = "";

            buttonCalcRiskScores.Enabled = false;

            //  get selected relative object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            if (proband == null)
            {

                return;
            }

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            proband.RP.AddHandlersWithLoad(probandRPChanged, probandRPLoaded, null);

        }

        /**************************************************************************************************/
        private void FHItemChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void FHChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void FHLoaded(HraListLoadedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void ClinicalObservationListChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void ClinicalObservationListLoaded(HraListLoadedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void ClinicalObservationChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        delegate void FillControlsCallback();
        private void FillControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                FillControlsCallback aoc = new FillControlsCallback(FillControls);
                this.Invoke(aoc, null);
            }
            else
            {

                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

                mutBRCA.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_1_2_Mut_Prob);
                mutMyriad.Text = decorateNullableScore(proband.RP.RiskProfile_Myriad_Brca_1_2);
                mutTC.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_Brca_1_2);
                mutTCv7.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_v7_Brca_1_2);

                riskBC5Yr_Brca.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_5Year_Breast);
                riskBCLifetime_Brca.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_Lifetime_Breast);
                riskBC5Yr_Claus.Text = decorateNullableScore(proband.RP.RiskProfile_Claus_5Year_Breast);
                riskBCLifetime_Claus.Text = decorateNullableScore(proband.RP.RiskProfile_Claus_Lifetime_Breast);
                riskBC5Yr_Gail.Text = decorateNullableScore(proband.RP.RiskProfile_Gail_5Year_Breast);
                riskBCLifetime_Gail.Text = decorateNullableScore(proband.RP.RiskProfile_Gail_Lifetime_Breast);
                riskBC5Yr_TC.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_5Year_Breast);
                riskBCLifetime_TC.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_Lifetime_Breast);
                riskBC5Yr_TCv7.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_v7_5Year_Breast);
                riskBCLifetime_TCv7.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_v7_Lifetime_Breast);


                riskOC5Yr_Brca.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_5Year_Ovary);
                riskOCLifetime_Brca.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_Lifetime_Ovary);

                mutMMRPRO.Text = decorateNullableScore(proband.RP.MmrPro_1_2_6_Mut_Prob);
                //mutPREMM.Text = decorateNullableScore(proband.RP.PREMM);
                PREMMlabel.Text = (proband.RP.PREMM == null) ? "PREMM2" : "PREMM";
                mutPREMM.Text = decorateNullableScore((proband.RP.PREMM2 != null) ? proband.RP.RiskProfile_PREMM2 : proband.RP.RiskProfile_PREMM);
                riskCC5Yr_mmrpro.Text = decorateNullableScore(proband.RP.MmrPro_5Year_Colon);
                riskCC5Yr_CCRAT.Text = decorateNullableScore(proband.RP.CCRATModel.Details.CCRAT_FiveYear_CRC);
                riskCCLifetime_mmrpro.Text = decorateNullableScore(proband.RP.MmrPro_Lifetime_Colon);
                riskCCLifetime_CCRAT.Text = decorateNullableScore(proband.RP.CCRATModel.Details.CCRAT_Lifetime_CRC);
                riskEC5Yr_mmrpro.Text = decorateNullableScore(proband.RP.MmrPro_5Year_Endometrial);
                riskECLifetime_mmrpro.Text = decorateNullableScore(proband.RP.MmrPro_Lifetime_Endometrial);
                  
                buttonCalcRiskScores.Enabled = true;
            }
        }

        /**************************************************************************************************/
        private string decorateNullableScore(double? nullableScore)
        {
            double score = nullableScore ?? -1;
            if (score > 0)
                return String.Format("{0:#0.0}", Math.Round(score, 1)) + "%";
            else
                return "";
        }

        /**************************************************************************************************/
        private void probandRPChanged(object sender, HraModelChangedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void probandRPLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }


        /**************************************************************************************************/
        private void RiskScoresView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void buttonCalcRiskScores_Click(object sender, EventArgs e)
        {
            if (proband != null)
            {
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;
                this.Enabled = false;

                RunRiskModelsDialog rrmd = new RunRiskModelsDialog(false);
                rrmd.ShowDialog();
                //proband.RecalculateRisk();

                //proband.RP.BackgroundLoadWork();

                //FillControls();

                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
                this.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord.PMH;
using BrightIdeasSoftware;

namespace RiskApps3.View.RiskClinic.PatientSummary
{
    public partial class PatientSummaryHeader : UserControl
    {
        Patient proband;
        ClinicalObservationList observations;

        ToolTip ttBreastSurgeryValue = new ToolTip();
        ToolTip ttBreastChemoValue = new ToolTip();
        ToolTip ttOvarySurgeryValue = new ToolTip();
        ToolTip ttOvaryChemoValue = new ToolTip();

        public bool ReadOnly
        {
            get
            {
                return !(breastSurgeryRec.Enabled);
            }
            set
            {
                //breastSurgeryRec.Enabled = !value;
                //ovarySurgeryRec.Enabled = !value;
                //breastChemoRec.Enabled = !value;
                //ovaryChemoRec.Enabled = !value;
            }
        }
        public PatientSummaryHeader()
        {
            InitializeComponent();
        }

        private void setToolTip30SecDelay(ToolTip tt)
        {
            //This works despite official MS documentation that says TT can only be visible for max of 5 seconds
            tt.AutomaticDelay = 30;
            tt.AutoPopDelay = 30000; //keep tool tip visible as long as 30 seconds
            tt.InitialDelay = 30;
            tt.ReshowDelay = 100;
        }
        /**************************************************************************************************/
        public void InitNewPatient()
        {
            if (proband != null)
                proband.ReleaseListeners(this);

            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                proband.follupSatus.AddHandlersWithLoad(theStatusChanged, theStatusLoaded, null);
                proband.cdsBreastOvary.AddHandlersWithLoad(theStatusChanged, theStatusLoaded, null);
                proband.PMH.Observations.AddHandlersWithLoad(theClinObservationsChanged, theClinObservationsLoaded, null);
            }
        }
        /**************************************************************************************************/
        public void ReleaseListeners()
        {
            if (proband != null)
            {
                if (proband.follupSatus != null)
                    proband.follupSatus.ReleaseListeners(this);
                if (proband.cdsBreastOvary != null)
                    proband.cdsBreastOvary.ReleaseListeners(this);
                if (proband.PMH != null)
                    proband.PMH.ReleaseListeners(this);
            }
        }
        /**************************************************************************************************/
        private void theStatusChanged(object sender, HraModelChangedEventArgs e)
        {
            FillStatusControls();
        }

        /**************************************************************************************************/
        private void theStatusLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillStatusControls();
        }

        /**************************************************************************************************/
        private void theClinObservationsChanged(HraListChangedEventArgs e)
        {
            FillStatusControls();
        }

        /**************************************************************************************************/
        private void theClinObservationsLoaded(HraListLoadedEventArgs e)
        {
            FillStatusControls();
        }

        /**************************************************************************************************/

        delegate void FillStatusControlsCallback();
        public void FillStatusControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                FillStatusControlsCallback aoc = new FillStatusControlsCallback(FillStatusControls);
                this.Invoke(aoc);
            }
            else
            {
                Font f = new Font("Tahoma", 8.0f, FontStyle.Bold);

                objectListView1.ColumnsInDisplayOrder[1].Renderer = null;
                //TODO how to modify column style???

                List<PatientStatusRow> listPSR = new List<PatientStatusRow>();
                listPSR.Add(new PatientStatusRow("Hx:", String.IsNullOrEmpty(proband.follupSatus.Diseases) ? "No Known Diseases" : proband.follupSatus.Diseases));
                listPSR.Add(new PatientStatusRow("Risk of Mutation:", (proband.follupSatus.MaxBRCAProMyriadScore != null) ? ((double)proband.follupSatus.MaxBRCAProMyriadScore).ToString("##.#") + "%" : "N/A"));
                listPSR.Add(new PatientStatusRow("Lifetime Risk Breast Cancer:", (proband.follupSatus.MaxLifetimeScore != null) ? ((double)proband.follupSatus.MaxLifetimeScore).ToString("##.#") + "%" : "N/A"));

                string brcaTestFamily = "";
                if (String.IsNullOrEmpty(proband.follupSatus.BreastFamilyGene))
                    brcaTestFamily = "No Testing In Family";
                else if (proband.follupSatus.BreastFamilyGene.Contains(","))
                    brcaTestFamily = proband.follupSatus.BreastFamilyMutationSignificance.Replace("BRCA1: Negative, BRCA2: Negative", "Negative");
                else
                    brcaTestFamily = proband.follupSatus.BreastFamilyGene + ": " + proband.follupSatus.BreastFamilyMutationSignificance;

                listPSR.Add(new PatientStatusRow("BRCA Test, Family:", brcaTestFamily));
                listPSR.Add(new PatientStatusRow("BRCA Test, Patient:", String.IsNullOrEmpty(proband.follupSatus.BreastProbandResult) ? "No Testing In Family" : proband.follupSatus.BreastProbandResult));
                listPSR.Add(new PatientStatusRow("Atypia/LCIS:", proband.follupSatus.AtypiaLCIS));
                objectListView1.SetObjects(listPSR);


                //Get relevant surgeries from disease history
                observations = proband.PMH.Observations;
                lock (observations)
                {
                    String breastSurgeryStatusStr = "Breasts Intact";

                    ClincalObservation co = (ClincalObservation)(observations.SingleOrDefault(v => ((ClincalObservation)v).disease == "Bilateral Mastectomy"));
                    if (co != null)
                        breastSurgeryStatusStr = "Bilateral Mastectomy";
                    else
                    {
                        co = (ClincalObservation)(observations.SingleOrDefault(v => ((ClincalObservation)v).disease == "Unilateral Mastectomy"));
                        if (co != null)
                            breastSurgeryStatusStr = "Unilateral Mastectomy";
                    }
                    if (breastSurgeryStatusStr == "Breasts Intact")
                    {
                        co = (ClincalObservation)(observations.SingleOrDefault(v => ((ClincalObservation)v).disease == "Mastectomy"));
                        if (co != null)
                            breastSurgeryStatusStr = "Mastectomy";
                    }

                    breastSurgeryStatus.Text = breastSurgeryStatusStr;

                    String ovarySurgeryStatusStr = "Ovaries Intact";

                    co = (ClincalObservation)(observations.SingleOrDefault(v => ((ClincalObservation)v).disease == "Bilateral Oophorectomy"));
                    if (co != null)
                        ovarySurgeryStatusStr = "Bilateral Oophorectomy";

                
                    ovarySurgeryStatus.Text = ovarySurgeryStatusStr;
                }

                //////////////////////
                breastSurgeryRec.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(proband.cdsBreastOvary.ProphMastRec);
                breastChemoRec.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(proband.cdsBreastOvary.ChemoRec);
                ovarySurgeryRec.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(proband.cdsBreastOvary.ProphOophRec);
                ovaryChemoRec.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(proband.cdsBreastOvary.OCRec);


                //set long lasting tooltips Breast
                ttBreastSurgeryValue.SetToolTip(breastSurgeryRec, breastSurgeryRec.Text);
                setToolTip30SecDelay(ttBreastSurgeryValue);
                ttBreastChemoValue.SetToolTip(breastChemoRec, breastChemoRec.Text);
                setToolTip30SecDelay(ttBreastChemoValue);

                //set long lasting tooltips Ovary
                ttOvarySurgeryValue.SetToolTip(ovarySurgeryRec, ovarySurgeryRec.Text);
                setToolTip30SecDelay(ttOvarySurgeryValue);
                ttOvaryChemoValue.SetToolTip(ovaryChemoRec, ovaryChemoRec.Text);
                setToolTip30SecDelay(ttOvaryChemoValue);

                if (proband.transvaginalImagingHx.IsLoaded &&
                    proband.follupSatus.HraState == HraObject.States.Ready &&
                    proband.labsHx.IsLoaded &&
                    proband.breastImagingHx.IsLoaded)
                {
                    loadingCircle1.Active = false;
                    loadingCircle1.Visible = false;
                }
            }
        }

        class PatientStatusRow
        {
            public string label;
            public string value;
            
            public PatientStatusRow(string x, string y)
            {
                this.label = x;
                this.value = y;
            }
        }

    }
}

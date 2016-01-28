using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.View.PatientRecord.Imaging;
using RiskApps3.Model.PatientRecord.Labs;
using RiskApps3.View.PatientRecord.Labs;
using System.Diagnostics;
using System.IO;
using RiskApps3.View.RiskClinic.PatientSummary;
using RiskApps3.Controllers;

namespace RiskApps3.View.RiskClinic
{
    public partial class SummaryFollowupView : HraView
    {
        Patient proband;

        /**************************************************************************************************/
        public SummaryFollowupView()
        {
            InitializeComponent();
            patientSummaryHeader1.ReadOnly = true;
        }

        /**************************************************************************************************/
        private void SummaryFollowupView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            ResetLastKnownValues();

            //  get active patient object from session manager
            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                proband.breastImagingHx.AddHandlersWithLoad(theBreastImagingHxChanged, theBreastImagingHxLoaded, null);
                proband.transvaginalImagingHx.AddHandlersWithLoad(theTransvaginalImagingHxChanged, theTransvaginalImagingHxLoaded, null);
                proband.labsHx.AddHandlersWithLoad(theLabsChanged, theLabsLoaded, null);
                patientSummaryHeader1.InitNewPatient();
            }
        }

        /**************************************************************************************************/
        private void ResetLastKnownValues()
        {

            MammoLKV.Clear();
            MRILKV.Clear();
            TVSLKV.Clear();
            CA125LKV.Clear();

        }

        /**************************************************************************************************/
        private void theBreastImagingHxLoaded(HraListLoadedEventArgs e)
        {
            FillBreastImagingControls();

            CheckForFullyLoaded();
        }

        /**************************************************************************************************/
        private void CheckForFullyLoaded()
        {
            if (proband.breastImagingHx.IsLoaded && proband.transvaginalImagingHx.IsLoaded && proband.labsHx.IsLoaded)
            {
                loadingCircle1.Enabled = false;
                loadingCircle1.Visible = false;
            }
        }
        /**************************************************************************************************/
        private void theBreastImagingHxChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                BreastImagingStudy theStudy = (BreastImagingStudy)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        if (theStudy.BreastImaging_imagingType == "MammographyHxView")
                        {
                            MammoLKV.AddDiagnostic(theStudy);
                        }
                        else if (theStudy.BreastImaging_imagingType == "MRI")
                        {
                            MRILKV.AddDiagnostic(theStudy);
                        }
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        if (theStudy.BreastImaging_imagingType == "MammographyHxView")
                        {
                            MammoLKV.RemoveDiagnostic(theStudy);
                        }
                        else if (theStudy.BreastImaging_imagingType == "MRI")
                        {
                            MRILKV.RemoveDiagnostic(theStudy);
                        }
                        break;
                }
            }
        }
        /**************************************************************************************************/
        private void theTransvaginalImagingHxLoaded(HraListLoadedEventArgs e)
        {
            FillTvsControls();
            CheckForFullyLoaded();
        }
        /**************************************************************************************************/
        private void theTransvaginalImagingHxChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                TransvaginalImagingStudy theStudy = (TransvaginalImagingStudy)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void theLabsLoaded(HraListLoadedEventArgs e)
        {
            FillLabControls();
            CheckForFullyLoaded();
        }
        /**************************************************************************************************/
        private void theLabsChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                RiskApps3.Model.PatientRecord.Labs.LabResult theStudy = (RiskApps3.Model.PatientRecord.Labs.LabResult)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        break;
                }
            }
        } 

        /**************************************************************************************************/
        private void FillBreastImagingControls()
        {
            MammoLKV.Clear();
            MRILKV.Clear();

            FillImagingStatus("MammographyHxView", MammoLKV);
            FillImagingStatus("MRI", MRILKV);

            MammoLKV.SetRec(proband.follupSatus.MammoRec);
            MRILKV.SetRec(proband.follupSatus.MRIRec);

            if (proband.transvaginalImagingHx.IsLoaded && 
                proband.follupSatus.HraState == HraObject.States.Ready && 
                proband.labsHx.IsLoaded && 
                proband.breastImagingHx.IsLoaded)
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
        }

        /**************************************************************************************************/
        private void FillImagingStatus(string p, LastKnownValueRow LKV)
        {
            if (proband.breastImagingHx.Count > 0)
            {
                proband.breastImagingHx.Sort(BreastImagingStudy.CompareDiagnosticByDate);
                    
                foreach (BreastImagingStudy bis in proband.breastImagingHx)
                {
                    if (string.IsNullOrEmpty(bis.imagingType) == false)
                    {
                        if (bis.imagingType == p)
                        {
                            LKV.AddDiagnostic(bis);
                        }
                    }
                }
            }
        }


       

        /**************************************************************************************************/
        private void FillLabControls()
        {
            CA125LKV.Clear();
            if (proband.labsHx.Count > 0)
            {
                foreach (LabResult lab in proband.labsHx)
                {
                    if (string.IsNullOrEmpty(lab.unitnum) == false &&
                        string.IsNullOrEmpty(lab.TestDesc) == false)
                    {
                        if (lab.TestDesc.ToLower().Contains("ca125"))
                        {
                            CA125LKV.AddDiagnostic(lab);
                        }
                    }
                }
            }

            CA125LKV.SetRec(proband.follupSatus.Ca125Rec);

            if (proband.transvaginalImagingHx.IsLoaded &&
                proband.follupSatus.HraState == HraObject.States.Ready &&
                proband.labsHx.IsLoaded &&
                proband.breastImagingHx.IsLoaded)
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
        }
        /**************************************************************************************************/
        private void FillTvsControls()
        {
            TVSLKV.Clear();
            if (proband.transvaginalImagingHx.Count > 0)
            {
                foreach (TransvaginalImagingStudy tis in proband.transvaginalImagingHx)
                {
                    TVSLKV.AddDiagnostic(tis);
                }
            }

            TVSLKV.SetRec(proband.follupSatus.TvsRec);

            if (proband.transvaginalImagingHx.IsLoaded &&
                proband.follupSatus.HraState == HraObject.States.Ready &&
                proband.labsHx.IsLoaded &&
                proband.breastImagingHx.IsLoaded)
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
        }


        /**************************************************************************************************/
        private void LabsChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
                FillLabControls();
        }
        /**************************************************************************************************/
        private void BreastImagingChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
                FillBreastImagingControls();
        }

        /**************************************************************************************************/
        private void BreastImagingLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillBreastImagingControls();
        }

        /**************************************************************************************************/
        private void SummaryFollowupView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (proband != null)
            {
                proband.breastImagingHx.ReleaseListeners(this);
                proband.transvaginalImagingHx.ReleaseListeners(this);
                proband.labsHx.ReleaseListeners(this);
            }
            patientSummaryHeader1.ReleaseListeners();
            SessionManager.Instance.RemoveHraView(this);
        }

        public static string NullableDoubleToString(double? d)
        {
            return ((d != null) ? ((double)d).ToString("##.#") : "N/A");
        }

        private void patientSummaryHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}

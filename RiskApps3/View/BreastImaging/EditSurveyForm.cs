using RiskApps3.Controllers;
using RiskApps3.Utilities;
using RiskApps3.View.Common;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.FamilyHistory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View.RiskClinic;
using WeifenLuo.WinFormsUI.Docking;

namespace RiskApps3.View.BreastImaging
{
    public partial class EditSurveyForm : HraView
    {
        ExpressFamilyHistoryView efhv;
        AdditionalCancerRiskFactorsView acrf;
        private MammographyHxView mmHx;
        PatientCommunicationView pcv;

        public Type InitialView;

        public EditSurveyForm()
        {
            InitializeComponent();
            InitialView = typeof(ExpressFamilyHistoryView);

        }

        public enum TargetView
        {
            ExpressFamilyHistory,
            BreastCancerRiskFactors,
            PatientCommunication,
        }

        public void SetInitialView(TargetView view)
        {
            switch (view)
            {
                case TargetView.ExpressFamilyHistory:
                    InitialView = typeof(ExpressFamilyHistoryView);
                    break;
                case TargetView.BreastCancerRiskFactors:
                    InitialView = typeof(BreastCancerRiskFactors);
                    break;
                case TargetView.PatientCommunication:
                    InitialView = typeof(PatientCommunicationView);
                    break;
                default:
                    InitialView = typeof(ExpressFamilyHistoryView);
                    break;
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(ExpressFamilyHistoryView).ToString())
            {
                efhv = new ExpressFamilyHistoryView();
                return efhv;
            }
            if (persistString == typeof(MammographyHxView).ToString())
            {
                mmHx = new MammographyHxView();
                return mmHx;
            }
            if (persistString == typeof(AdditionalCancerRiskFactorsView).ToString())
            {
                acrf = new AdditionalCancerRiskFactorsView();
                return acrf;
            }

            else if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                pcv.routineName = "screening";
                pcv.PatientHeaderVisible = false;
                return pcv;
            }
            else
                return null;
        }

        private void EditSurveyForm_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.Visible = false;

                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("EditSurveyFormDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                {
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);

                }
                else
                {
                    efhv = new ExpressFamilyHistoryView();
                    efhv.Show(theDockPanel);
                    efhv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                    /**/
                    acrf = new AdditionalCancerRiskFactorsView();
                    acrf.Show(theDockPanel);
                    acrf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    mmHx = new MammographyHxView();
                    mmHx.Show(theDockPanel);
                    mmHx.DockState = DockState.Document;

                    pcv = new PatientCommunicationView();
                    pcv.routineName = "screening";
                    pcv.PatientHeaderVisible = false;
                    pcv.Show(theDockPanel);
                    pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                }

                if (InitialView == typeof(ExpressFamilyHistoryView))
                {
                    if (efhv != null)
                        efhv.Show();
                }
                if (InitialView == typeof (MammographyHxView))
                {
                    if (mmHx != null)
                    {
                        mmHx.Show();
                    }
                }
                if (InitialView == typeof(AdditionalCancerRiskFactorsView))
                {
                    if (acrf != null)
                        acrf.Show();
                }
                else if (InitialView == typeof(PatientCommunicationView))
                {
                    if (pcv != null)
                        pcv.Show();
                }

                //TODO implement other InitialViews as needed
                patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
            }
            theDockPanel.Visible = true;
        }

        private void EditSurveyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to calculate risk and generate doucments?", "Run Risk and Generate Documents", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                RunRiskModelsDialog rrmd = new RunRiskModelsDialog(true);
                rrmd.ShowDialog();
            }

            int a = SessionManager.Instance.GetActivePatient().apptid;
            string u = SessionManager.Instance.GetActivePatient().unitnum;
            FinalizeRecordForm frm = new FinalizeRecordForm(a, u);
            frm.ShowDialog();



            theDockPanel.Visible = false;

            string configFile = SessionManager.SelectDockConfig("EditSurveyFormDockPanel.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            CloseChildView(efhv);
            CloseChildView(pcv);
            CloseChildView(acrf);
            CloseChildView(mmHx);

            patientRecordHeader1.ReleaseListeners();
        }

        private void CloseChildView(HraView view)
        {
            if (view != null)
            {
                view.ViewClosing = true;
                view.Close();
            }
        }

        private void theDockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {

        }
    }
}

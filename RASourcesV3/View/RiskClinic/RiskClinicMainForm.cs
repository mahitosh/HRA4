using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.PatientRecord.FamilyHistory;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.Risk;
//using RiskApps3.View.PatientRecord.DocumentViewer;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.Controllers;
using RiskApps3.View.PatientRecord.Risk;
using RiskApps3.View.Common;

namespace RiskApps3.View.RiskClinic
{
    public partial class RiskClinicMainForm : HraView
    {
        RiskClinicFamilyHistoryView rcfhv;
        AdditionalCancerRiskFactorsView acrf;
        SimpleRiskModelView srmv;
        Recommendations brecs;
        PatientCommunicationView pcv;
        OrdersView ov;
        TestsView tv;
        CancerRiskView crv;
        PediatricRecsView prv;

        public Type InitialView;

        public RiskClinicMainForm()
        {
            InitializeComponent();
            InitialView = typeof(RiskClinicFamilyHistoryView);
        }

        public enum TargetView
        {
            RiskClinicFamilyHistory,
            BreastCancerRiskFactors,
            SimpleRiskModel,
            Recommendations,
            PatientCommunication,
            Orders,
            Tests,
            CancerRisk
        }

        /// <summary>
        /// User while intializing view before it has been shown.
        /// </summary>
        /// <param name="view"></param>
        public void SetInitialView(TargetView view)
        {
            switch (view)
            {
                case TargetView.BreastCancerRiskFactors:
                    InitialView = typeof(BreastCancerRiskFactors);
                    break;
                case TargetView.CancerRisk:
                    InitialView = typeof(BreastCancerRiskFactors);
                    break;
                case TargetView.Orders:
                    InitialView = typeof(OrdersView);
                    break;
                case TargetView.PatientCommunication:
                    InitialView = typeof(PatientCommunicationView);
                    break;
                case TargetView.Recommendations:
                    InitialView = typeof(Recommendations);
                    break;
                case TargetView.RiskClinicFamilyHistory:
                    InitialView = typeof(RiskClinicFamilyHistoryView);
                    break;
                case TargetView.SimpleRiskModel:
                    InitialView = typeof(SimpleRiskModelView);
                    break;
                case TargetView.Tests:
                    InitialView = typeof(TestsView);
                    break;
                default:
                    InitialView = typeof(RiskClinicFamilyHistoryView);
                    break;
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(RiskClinicFamilyHistoryView).ToString())
            {
                rcfhv = new RiskClinicFamilyHistoryView();
                return rcfhv;
            }
            if (persistString == typeof(AdditionalCancerRiskFactorsView).ToString())
            {
                acrf = new AdditionalCancerRiskFactorsView();
                return acrf;
            }
            else if (persistString == typeof(CancerRiskView).ToString())
            {
                crv = new CancerRiskView();
                return crv;
            }
            else if (persistString == typeof(SimpleRiskModelView).ToString())
            {
                srmv = new SimpleRiskModelView();
                return srmv;
            }
            //else if (persistString == typeof(RiskClinicNotesView).ToString())
            //{
            //    rcnv = new RiskClinicNotesView();
            //    rcnv.PatientHeaderVisible = false;
            //    return rcnv;
            //}
            else if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                pcv.PatientHeaderVisible = false;
                return pcv;
            }
            else if (persistString == typeof(Recommendations).ToString())
            {
                brecs = new Recommendations();
                return brecs;
            }
            else if (persistString == typeof(TestsView).ToString())
            {
                tv = new TestsView();
                //drv.PatientHeaderVisible = false;
                return tv;
            }
            else if (persistString == typeof(OrdersView).ToString())
            {
                ov = new OrdersView();
                return ov;
            }
            else if (persistString == typeof(PediatricRecsView).ToString())
            {
                prv = new PediatricRecsView();
                return prv;
            }
            else
                return null;
        }

        private void RiskClinicMainForm_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.Visible = false;

                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("RiskClinicMainFormDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                {
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);

                }
                else
                {
                    rcfhv = new RiskClinicFamilyHistoryView();
                    rcfhv.Show(theDockPanel);
                    rcfhv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
/**/
                    acrf = new AdditionalCancerRiskFactorsView();
                    acrf.Show(theDockPanel);
                    acrf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    srmv = new SimpleRiskModelView();
                    srmv.Show(theDockPanel);
                    srmv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    brecs = new Recommendations();
                    brecs.Show(theDockPanel);
                    brecs.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    //rcnv = new RiskClinicNotesView();
                    //rcnv.PatientHeaderVisible = false;
                    //rcnv.Show(theDockPanel);
                    //rcnv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    pcv = new PatientCommunicationView();
                    pcv.PatientHeaderVisible = false;
                    pcv.Show(theDockPanel);
                    pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    tv = new TestsView();
                    //drv.Text = "Tests";
                    //drv.PatientHeaderVisible = false;
                    tv.Show(theDockPanel);
                    tv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    crv = new CancerRiskView();
                    crv.Show(theDockPanel);
                    crv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    ov = new OrdersView();
                    ov.Show(theDockPanel);
                    ov.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
/*
                    prv = new PediatricRecsView();
                    prv.Show(theDockPanel);
                    prv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
*/

                    //pcv = new PatientCommunicationView();
                    //pcv.PatientHeaderVisible = false;
                    //pcv.Orientation = Orientation.Vertical;
                    //pcv.Show(theDockPanel);
                    //pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                }

                if (InitialView == typeof(RiskClinicFamilyHistoryView))
                {
                    if (rcfhv != null)
                        rcfhv.Show();
                }
                else if (InitialView == typeof(PatientCommunicationView))
                {
                    if (pcv != null)
                        pcv.Show();
                }
                //else if (InitialView == typeof(RiskClinicNotesView))
                //{
                //    if (rcnv != null)
                //        rcnv.Show();
                //}
                else if (InitialView == typeof(OrdersView))
                {
                    if (this.ov != null)
                    {
                        this.ov.Show();
                    }
                }
                //TODO implement other InitialViews as needed
                patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
            }
            theDockPanel.Visible = true;
        }

        private void RiskClinicMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            int a = SessionManager.Instance.GetActivePatient().apptid;
            string u = SessionManager.Instance.GetActivePatient().unitnum;
            FinalizeRecordForm frm = new FinalizeRecordForm(a, u);
            frm.ShowDialog();

            theDockPanel.Visible = false;

            string configFile = SessionManager.SelectDockConfig("RiskClinicMainFormDockPanel.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            CloseChildView(prv);
            CloseChildView(pcv);
            CloseChildView(rcfhv);
            CloseChildView(acrf);
            CloseChildView(srmv);
            CloseChildView(brecs);
            CloseChildView(tv);
            CloseChildView(ov);
            CloseChildView(crv);

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
            if (theDockPanel.ActiveDocument is SimpleRiskModelView)
            {
                ((SimpleRiskModelView)theDockPanel.ActiveDocument).RefreshView();
            }
        }


    }
}

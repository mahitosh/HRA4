using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View.PatientRecord.FamilyHistory;
using RiskApps3.View.PatientRecord.GeneticTesting;
using RiskApps3.View.PatientRecord.PMH;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.Pedigree;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Controllers;

namespace RiskApps3.View.RiskClinic
{
    public partial class RiskClinicFamilyHistoryView : HraView
    {
        PedigreeForm pf;
        FamilyHistoryView fhv; 
        RelativeDetailsView rdv; 
        DocumentUploadView duv;
        PastMedicalHistoryView pmhv;
        GeneticTestingView gtv;

        string configFile = "";

        bool m_fullScreenPed = false;

        public RiskClinicFamilyHistoryView()
        {
            InitializeComponent();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PedigreeForm).ToString())
            {
                pf = new PedigreeForm();
                //pf.SetMode("SELF_ORGANIZING");
                return pf;
            }
            if (persistString == typeof(FamilyHistoryView).ToString())
            {
                fhv = new FamilyHistoryView();
                return fhv;
            }
            else if (persistString == typeof(RelativeDetailsView).ToString())
            {
                rdv = new RelativeDetailsView();
                return rdv;
            }
            else if (persistString == typeof(DocumentUploadView).ToString())
            {
                duv = new DocumentUploadView();
                return duv;
            }
            else if (persistString == typeof(PastMedicalHistoryView).ToString())
            {
                pmhv = new PastMedicalHistoryView();
                return pmhv;
            }
            else if (persistString == typeof(GeneticTestingView).ToString())
            {
                gtv = new GeneticTestingView();
                return gtv;
            }
            else
                throw new NullDockingConfigException();
        }

        private void RiskClinicFamilyHistoryView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                configFile = SessionManager.SelectDockConfig("RiskClinicFamilyHistoryViewDockPanel.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                else
                {
                    pf = new PedigreeForm();
                    //pf.SetMode("SELF_ORGANIZING");
                    pf.Show(theDockPanel);
                    pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    fhv = new FamilyHistoryView();
                    fhv.Show(theDockPanel);
                    fhv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    rdv = new RelativeDetailsView();
                    rdv.Show(theDockPanel);
                    rdv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;

                    duv = new DocumentUploadView();
                    duv.Show(rdv.Pane, rdv);

                    pmhv = new PastMedicalHistoryView();
                    pmhv.Show(theDockPanel);
                    pmhv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;

                    gtv = new GeneticTestingView();
                    gtv.Show(theDockPanel);
                    gtv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
                }
            }
        }
        
        private void RiskClinicFamilyHistoryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("RiskClinicFamilyHistoryViewDockPanel.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (pf != null)
                pf.ViewClosing = true;

            if (fhv != null)
                fhv.ViewClosing = true;

            if (rdv != null)
                rdv.ViewClosing = true;

            if (duv != null)
                duv.ViewClosing = true;

            if (pmhv != null)
                pmhv.ViewClosing = true;

            if (gtv != null)
                gtv.ViewClosing = true;

            if (pf != null)
                pf.Close();

            if (fhv != null)
                fhv.Close();

            if (rdv != null)
                rdv.Close();

            if (duv != null)
                duv.Close();

            if (pmhv != null)
                pmhv.Close();

            if (gtv != null)
                gtv.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Less")
                button1.Text = "More";
            else
                button1.Text = "Less";

            if (rdv != null)
            {
                if (rdv.IsHidden == false)
                    rdv.IsHidden = true;
                else
                    rdv.IsHidden = false;
            }
            if (duv != null)
            {
                if (duv.IsHidden == false)
                    duv.IsHidden = true;
                else
                    duv.IsHidden = false;
            }
            if (pmhv != null)
            {
                if (pmhv.IsHidden == false)
                    pmhv.IsHidden = true;
                else
                    pmhv.IsHidden = false;
            }
            if (gtv != null)
            {
                if (gtv.IsHidden == false)
                    gtv.IsHidden = true;
                else
                    gtv.IsHidden = false;
            }
        }

    }
}
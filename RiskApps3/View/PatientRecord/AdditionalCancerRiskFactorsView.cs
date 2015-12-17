using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using RiskApps3.View.RiskClinic;

namespace RiskApps3.View.PatientRecord
{
    public partial class AdditionalCancerRiskFactorsView : HraView
    {
        BreastCancerRiskFactors bcrf;
        ColonCancerRiskFactors ccrf;

        public AdditionalCancerRiskFactorsView()
        {
            InitializeComponent();
        }

        private void AdditionalCancerRiskFactorsView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("AdditionalCancerRiskFactorsView.config");
                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                {
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);

                }
                else
                {
                    bcrf = new BreastCancerRiskFactors();
                    bcrf.Show(theDockPanel);
                    bcrf.DockState = DockState.Document;
                    
                    ccrf = new ColonCancerRiskFactors();
                    ccrf.Show(theDockPanel);
                    ccrf.DockState = DockState.Document;
                }
            }
        }
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(BreastCancerRiskFactors).ToString())
            {
                bcrf = new BreastCancerRiskFactors();
                return bcrf;
            }
            if (persistString == typeof(ColonCancerRiskFactors).ToString())
            {
                ccrf = new ColonCancerRiskFactors();
                return ccrf;
            }

            return null;
        }
        private void AdditionalCancerRiskFactorsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("AdditionalCancerRiskFactorsView.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);


            if (bcrf != null)
                bcrf.ViewClosing = true;
            if (ccrf != null)
                ccrf.ViewClosing = true;

            if (bcrf != null)
                bcrf.Close();
            if (ccrf != null)
                ccrf.Close();

            SessionManager.Instance.RemoveHraView(this);
        }
    }
}

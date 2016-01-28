using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic.Dashboard;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.FamilyHistory;
using RiskApps3.View.PatientRecord.GeneticTesting;
using RiskApps3.View.PatientRecord.PMH;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.Pedigree;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.View.Common;
//using RiskApps3.View.PatientRecord.DocumentViewer;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class PatientContactView : HraView
    {
        
        private PedigreeForm pf;
        private RelativeDetailsView rdv;
        private PatientCommunicationView pcv;


        public PatientContactView()
        {
            InitializeComponent();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PedigreeForm).ToString())
            {
                pf = new PedigreeForm();
                pf.SetMode("MANUAL");
                //pf.Register(sessionManager);
                return pf;
            }
            else if (persistString == typeof(RelativeDetailsView).ToString())
            {
                rdv = new RelativeDetailsView();
               //rdv.Register(sessionManager);
                return rdv;
            }
            else if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                //pcv.Register(sessionManager);
                return pcv;
            }
            
            return null;
        }

        private void PatientContactView_Load(object sender, EventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("PatientContactView.config");
            
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {

                pf = new PedigreeForm();
                pf.SetMode("MANUAL");
                //pf.Register(sessionManager);
                pf.Show(theDockPanel);
                pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockLeft;

                rdv = new RelativeDetailsView();
                //rdv.Register(sessionManager);
                rdv.Show(theDockPanel);
                rdv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;

                pcv = new PatientCommunicationView();
                //pcv.Register(sessionManager);
                pcv.Show(theDockPanel);
                pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
            }
        }

        private void PatientContactView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("PatientContactView.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile); 
            
            theDockPanel.Controls.Clear();

            if (pcv != null)
                pcv.Close();

            if (rdv != null)
                rdv.Close();

            if (pf != null)
                pf.Close();
        }
    }
}
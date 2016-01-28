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
using RiskApps3.View.PatientRecord.Pedigree;
using System.IO;
using RiskApps3.View.RiskClinic;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.View.Common;
//using RiskApps3.View.PatientRecord.DocumentViewer;
using RiskApps3.Controllers;

namespace RiskApps3.View.Admin
{
    public partial class AdminMainForm : HraView
    {
        private MasterParagraphListView masterParagraphView;

        /**************************************************************************************************/
        public AdminMainForm()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(HighRiskFollowupView).ToString())
            {
                masterParagraphView = new MasterParagraphListView();
                return masterParagraphView;
            }
            
            return null;
        }

        /**************************************************************************************************/
        private void RiskClinicFamilyHistoryView_Load(object sender, EventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("AdminMainForm.config");
            
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
            else
            {
                masterParagraphView = new MasterParagraphListView();
                masterParagraphView.Show(theDockPanel);
                masterParagraphView.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            }
        }

        /**************************************************************************************************/
        private void RiskClinicFamilyHistoryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("AdminMainForm.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (masterParagraphView != null)
                masterParagraphView.Close();

        }
    }
}
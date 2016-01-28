using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using RiskApps3.Model;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace RiskApps3.View.PatientRecord
{
    public partial class DiagnosticReportsView : HraView
    {
        TestsView tv;
        OrdersView ov;

        public Diagnostic InitialDx;

        public DiagnosticReportsView()
        {
            InitializeComponent();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(TestsView).ToString())
            {
                tv = new TestsView();
                return tv;
            }
            if (persistString == typeof(OrdersView).ToString())
            {
                ov = new OrdersView();
                return ov;
            }
            else
                throw new NullDockingConfigException();
        }

        private void DiagnosticReportsView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;
                string configFile = SessionManager.SelectDockConfig("RiskClinicDiagnosticReportsViewDockPanel.config");

                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                else
                {
                    tv = new TestsView();
                    tv.InitialDx = InitialDx;
                    tv.Show(theDockPanel);
                    tv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    ov = new OrdersView();
                    ov.Show(theDockPanel);
                    ov.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;
                }

                patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
            }
        }
        private void DiagnosticReportsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("RiskClinicDiagnosticReportsViewDockPanel.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (tv != null)
                tv.Close();

            if (ov != null)
                ov.Close();
            patientRecordHeader1.ReleaseListeners();
        }

        public void setInitialDx(Diagnostic dx)
        {
            InitialDx = dx;
        }
    }
}

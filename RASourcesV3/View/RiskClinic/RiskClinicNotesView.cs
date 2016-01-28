using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.Communication;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.RiskClinic
{
    public partial class RiskClinicNotesView : HraView
    {
        private PatientCommunicationView pcv;
        private NewToDoView ntdv;

        public Task InitialTask;

        public bool ShowNewToDo = true;

        public bool PatientHeaderVisible
        {
            get { return patientRecordHeader1.Visible; }
            set
            {
                patientRecordHeader1.Visible = value;
                if (value)
                {
                    theDockPanel.Dock = DockStyle.None;
                }
                else
                {
                    theDockPanel.Dock = DockStyle.Fill;
                }
            }
        }

        public RiskClinicNotesView()
        {
            InitializeComponent();
        }

        private void RiskClinicNotesView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

                string configFile = SessionManager.SelectDockConfig("RiskClinicNotesViewDockPanel.config");

                DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

                if (File.Exists(configFile))
                {
                    theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);
                }
                else
                {
                    pcv = new PatientCommunicationView();
                    pcv.PatientHeaderVisible = false;
                    pcv.Orientation = Orientation.Vertical;
                    pcv.InitialTask = InitialTask;
                    pcv.Show(theDockPanel);
                    pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                    if (ShowNewToDo)
                    {
                        ntdv = new NewToDoView();
                        ntdv.Show(theDockPanel);
                        ntdv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
                    }
                }

                Patient proband = SessionManager.Instance.GetActivePatient();
                if (proband != null)
                {
                    if (patientRecordHeader1.Visible)
                    {
                        patientRecordHeader1.setPatient(proband);
                    }
                }
            }
        }
        private void RiskClinicNotesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("RiskClinicNotesViewDockPanel.config");

            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (pcv != null)
                pcv.Close();

            if (ntdv != null)
                ntdv.Close();
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof (PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                pcv.PatientHeaderVisible = false;
                pcv.Orientation = Orientation.Vertical;

                return pcv;
            }

            else if (persistString == typeof (NewToDoView).ToString())
            {
                if (ShowNewToDo)
                {
                    ntdv = new NewToDoView();
                    return ntdv;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
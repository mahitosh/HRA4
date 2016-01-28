using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.View.PatientRecord.Pedigree;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Controllers;

namespace RiskApps3.View.RiskClinic
{
    public partial class QueueNotesView : HraView
    {
        PatientCommunicationView pcv;
        PedigreeForm pf;
        NewToDoView ntdv;


        public Task InitialTask;

        public QueueNotesView()
        {
            InitializeComponent();
        }

        private void QueueNotesView_Load(object sender, EventArgs e)
        {
            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

            string configFile = SessionManager.SelectDockConfig("QueueNotesViewDockPanel.config");
            DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            if (File.Exists(configFile))
            {
                theDockPanel.LoadFromXml(configFile, m_deserializeDockContent);

            }
            else
            {
                pf = new PedigreeForm();
                pf.Show(theDockPanel);
                pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                pcv = new PatientCommunicationView();
                pcv.PatientHeaderVisible = false;
                pcv.InitialTask = InitialTask;
                //pcv.SplitterDistance = 0;
                pcv.Show(theDockPanel);
                pcv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

                ntdv = new NewToDoView();
                ntdv.Show(theDockPanel);
                ntdv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottom;
            }

            patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
        }

        private void QueueNotesView_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = SessionManager.SelectDockConfig("QueueNotesViewDockPanel.config");
            
            if (SessionManager.Instance.SaveLayoutOnClose)
                theDockPanel.SaveAsXml(configFile);

            if (pcv != null)
                pcv.Close();

            if (pf != null)
                pf.Close();

            if (ntdv != null)
                ntdv.Close();

        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(PatientCommunicationView).ToString())
            {
                pcv = new PatientCommunicationView();
                pcv.PatientHeaderVisible = false;
                pcv.InitialTask = InitialTask;
                //pcv.SplitterDistance = 0;
                return pcv;
            }

            else if (persistString == typeof(PedigreeForm).ToString())
            {
                pf = new PedigreeForm();
                return pf;
            }

            else if (persistString == typeof(NewToDoView).ToString())
            {
                ntdv = new NewToDoView();
                return ntdv;
            }
            else
                return null;
        }


    }
}

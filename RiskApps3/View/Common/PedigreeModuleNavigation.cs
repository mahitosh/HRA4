using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View.RiskClinic;
using RiskApps3.View.PatientRecord.Communication;
using RiskApps3.Model.PatientRecord.Communication;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.RiskClinic.PatientSummary;
using RiskApps3.View.PatientRecord;

namespace RiskApps3.View.Common
{
    public partial class PedigreeModuleNavigation : HraView
    {
        private bool m_riskClinic = true;
        private bool m_contact = true;
        private bool m_documents = true;
        private bool m_followup = true;
        private bool m_appointment = true;

        public bool RiskClinic
        {
            get
            {
                return m_riskClinic;
            }
            set
            {
                m_riskClinic = value;
            }
        }
        public bool Contact
        {
            get
            {
                return m_contact;
            }
            set
            {
                m_contact = value;
            }
        }
        public bool Documents
        {
            get
            {
                return m_documents;
            }
            set
            {
                m_documents = value;
            }
        }
        public bool Followup
        {
            get
            {
                return m_followup;
            }
            set
            {
                m_followup = value;
            }
        }
        public bool Appointment
        {
            get
            {
                return m_appointment;
            }
            set
            {
                m_appointment = value;
            }
        }


        public PedigreeModuleNavigation()
        {
            InitializeComponent();
        }

        private void pedigreeButton_Click(object sender, EventArgs e)
        {
            PedigreeForm pf = new PedigreeForm();
            pf.WindowState = FormWindowState.Maximized;
            pf.ShowDialog();
        }

        private void clinicButton_Click(object sender, EventArgs e)
        {
            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);
            rcmf.WindowState = FormWindowState.Maximized;
            rcmf.ShowDialog();
        }

        /*********************************************************************************/
        private void TaskListLoaded(HraListLoadedEventArgs e)
        {
            Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {
                string assignedBy = "";
                if (SessionManager.Instance.ActiveUser != null)
                {
                    if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                    {
                        assignedBy = SessionManager.Instance.ActiveUser.ToString();
                    }
                }
                Task t = new Task(p, "Task", "Pending", assignedBy, DateTime.Now);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                p.Tasks.AddToList(t,args);
                TaskView tv = new TaskView(t);
                tv.ShowDialog();
            }
        }

        private void PatientNavigation_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += NewActivePatient;
        }
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            if (e.newActivePatient != null)
            {
                flowLayoutPanel1.Enabled = true;
            }
            else
            {
                flowLayoutPanel1.Enabled = false;
            }
        }

        private void PatientNavigation_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);

        }
    }
}

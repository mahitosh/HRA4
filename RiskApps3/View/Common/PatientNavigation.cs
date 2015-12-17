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
    public partial class PatientNavigation : HraView
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
                bitmapButton1.Visible = value;
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
                bitmapButton5.Visible = value;
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
                bitmapButton2.Visible = value;
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
                bitmapButton3.Visible = value;
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
                bitmapButton4.Visible = value;
            }
        }

        
        public PatientNavigation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RiskClinicMainForm rcmf = new RiskClinicMainForm();
            rcmf.InitialView = typeof(RiskClinicFamilyHistoryView);
            rcmf.WindowState = FormWindowState.Maximized;
            rcmf.ShowDialog();

        }

        private void bitmapButton3_Click(object sender, EventArgs e)
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
                Task t = new Task(p, "Note", null, assignedBy,DateTime.Now);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                p.Tasks.AddToList(t,args);


                //TaskView tv = new TaskView(t);
                //tv.ShowDialog();

                QueueNotesView qnv = new QueueNotesView();
                qnv.InitialTask = t;
                qnv.ShowDialog();

            }
        
        }

        private void bitmapButton5_Click(object sender, EventArgs e)
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
                Task t = new Task(p, "Patient Followup", null, assignedBy, DateTime.Now);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                args.Persist = true;
                p.Tasks.AddToList(t, args);

                //TaskView tv = new TaskView(t);
                //tv.ShowDialog();

                QueueNotesView qnv = new QueueNotesView();
                qnv.InitialTask = t;
                qnv.ShowDialog();
            }
        }

        private void bitmapButton6_Click(object sender, EventArgs e)
        {
            Patient p = SessionManager.Instance.GetActivePatient();
            if (p != null)
            {
                p.Tasks.AddHandlersWithLoad(null, TaskListLoaded, null); 
            }
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

        private void bitmapButton2_Click(object sender, EventArgs e)
        {
            //QueueNotesView qnv = new QueueNotesView();
            //qnv.WindowState = FormWindowState.Maximized;
            //qnv.ShowDialog();

            PatientCommunicationView pcv = new PatientCommunicationView();
            pcv.Orientation = System.Windows.Forms.Orientation.Vertical;
            pcv.ShowDialog();
        }

        private void bitmapButton7_Click(object sender, EventArgs e)
        {
            PedigreeForm pf = new PedigreeForm();
            pf.WindowState = FormWindowState.Maximized;
            pf.ShowDialog();
        }

        private void bitmapButton8_Click(object sender, EventArgs e)
        {
            //SummaryEditView sev = new SummaryEditView(SessionManager.Instance.GetActivePatient());
            ////sev.WindowState = FormWindowState.Maximized;
            //sev.ShowDialog();
            DiagnosticReportsView drv = new DiagnosticReportsView();
            drv.WindowState = FormWindowState.Maximized;
            drv.ShowDialog();
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

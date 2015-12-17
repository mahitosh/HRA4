using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;

namespace RiskApps3.View.Appointments
{
    public partial class WebApptSynchToSchedule : Form
    {
        AppointmentList appts;

        public int SelectedApptID = -1;

        public WebApptSynchToSchedule()
        {
            InitializeComponent();
        }
        public void Setup(AppointmentList schedule)
        {
            appts = schedule;
            fastDataListView1.ClearObjects();
            fastDataListView1.SetObjects(appts);
        }
        private void WebApptSynchToSchedule_Load(object sender, EventArgs e)
        {
            foreach (Clinic c in SessionManager.Instance.ActiveUser.userClinicList)
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    comboBox1.Text = c.clinicName;
                }
                comboBox1.Items.Add(c);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fastDataListView1.SelectedObject != null)
            {
                SelectedApptID = ((Appointment)fastDataListView1.SelectedObject).apptID;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectedApptID = -1;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            this.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            appts.Date = dateTimePicker1.Value.ToShortDateString();
            appts.BackgroundListLoad();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            fastDataListView1.ClearObjects();
            fastDataListView1.SetObjects(appts);
            this.Enabled = true;

        }
    }
}

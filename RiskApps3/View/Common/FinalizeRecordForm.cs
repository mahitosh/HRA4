using RiskApps3.Model.Clinic.Dashboard;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Common
{
    public partial class FinalizeRecordForm : Form
    {
        private int a;
        private string u;

        public FinalizeRecordForm(int apptid, string unitnum)
        {
            a = apptid;
            u = unitnum;
            InitializeComponent();
        }

        private void FinalizeRecordForm_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Appointment.MarkPrinted(a);

            LegacyCoreAPI.stopWorkingWithAppointment(a);

            QueueData.UpdateBigQueueByMrn(u);
        }
    }
}

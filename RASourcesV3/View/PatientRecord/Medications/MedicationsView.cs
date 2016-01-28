using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Medications
{
    public partial class MedicationsView : HraView
    { 
        /**************************************************************************************************/
        
        /**************************************************************************************************/
        Patient proband;
        Model.PatientRecord.MedicationHx medHx;

        /**************************************************************************************************/
        public MedicationsView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void MedicationsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            listView1.Items.Clear();
            InitNewPatient();

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patinet object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            if (proband != null)
            {
                proband.AddHandlersWithLoad(activePatientChanged,activePatientLoaded,null);

            }
        }

        /**************************************************************************************************/
        private void LoadOrGetMedHx()
        {
            //  get active patinet object from session manager
            medHx = SessionManager.Instance.GetActivePatient().MedHx;

            if (medHx != null)
            {
                medHx.AddHandlersWithLoad(MedHxChanged, MedHxLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }

            listView1.Items.Clear();

            InitNewPatient();
            
        }

        /**************************************************************************************************/
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetMedHx();
        }

        /**************************************************************************************************/
        private void MedHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControls();
            }
        }

        /**************************************************************************************************/
        private void MedHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void FillControls()
        {
            listView1.Items.Clear();

            foreach (Medication med in medHx.Medications)
            {
                ListViewItem lvi = new ListViewItem(med.medication);
                lvi.SubItems.Add(med.dose);
                lvi.SubItems.Add(med.route);
                listView1.Items.Add(lvi);
            }

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;

        }

        /**************************************************************************************************/
        private void MedicationsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }
    }
}

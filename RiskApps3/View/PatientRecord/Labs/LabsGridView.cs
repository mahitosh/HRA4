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
using RiskApps3.Model.PatientRecord.Labs;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Labs
{
    public partial class LabsGridView : HraView
    {
        /**************************************************************************************************/
        LabsHx theLabs;

        /**************************************************************************************************/
        public LabsGridView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void LabsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

           // InitNewPatient();
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patinet object from session manager
            //Patient proband = SessionManager.Instance.GetActivePatient();
            //if (proband != null)
            //{
            //    theLabs = proband.labsHx;

            //    if (theLabs != null)
            //    {

            //        theLabs.Changed += new HraObject.ChangedEventHandler(theLabsChanged);
            //        theLabs.Loaded += new HraObject.LoadFinishedEventHandler(theLabsLoaded);

            //        switch (theLabs.hra_state)
            //        {
            //            case HraObject.States.NULL:
            //                loadingCircle1.Active = true;
            //                loadingCircle1.Visible = true;

            //                theLabs.LoadObject();
            //                break;

            //            case HraObject.States.Loading:
            //                break;

            //            case HraObject.States.Ready:
            //                FillControls();
            //                break;

            //        }
            //    }
            //}
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void theLabsChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControls();
            }
        }
        /**************************************************************************************************/
        private void theLabsLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void FillControls()
        {
            //objectListView1.SetObjects(theLabs.labs);

            //objectListView1.Columns[0].Width = -1;
            //objectListView1.Columns[1].Width = -1;

            //loadingCircle1.Active = false;
            //loadingCircle1.Visible = false;
        }

        private void BreastImagingView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

    }
}

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
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord
{
    public partial class DocumentUploadView : HraView
    {

        /**************************************************************************************************/
        private Person selectedRelative;
        private string baseOutputPath= "C:\\Program Files\\RiskAppsV2\\documents";

        /**************************************************************************************************/
        public DocumentUploadView()
        {
            baseOutputPath = Configurator.getNodeValue("globals", "DocumentStorage");
            InitializeComponent();
        }

        /**************************************************************************************************/

        private void DocumentUploadView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            SessionManager.Instance.RelativeSelected +=
                new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            if (e.sendingView != this)
                InitSelectedRelative();

            FillControls();
        }

        /**************************************************************************************************/

        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            if (e.sendingView != this)
                InitSelectedRelative();

            FillControls();
        }

        /**************************************************************************************************/

        private void InitSelectedRelative()
        {
            //  get selected relative object from session manager
            selectedRelative = SessionManager.Instance.GetSelectedRelative();

            if (selectedRelative != null)
            {
                selectedRelative.AddHandlersWithLoad(activeRelativeChanged, activeRelativeLoaded, null);
                FillControls();
            }
        }


        /**************************************************************************************************/

        private void FillControls()
        {
            if (selectedRelative != null)
            {
                this.Enabled = true;
                this.fileUploadControl1.Enable();
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

                relativeHeader1.setRelative(selectedRelative);

                fileUploadControl1.mrn = SessionManager.Instance.GetActivePatient().unitnum;
                fileUploadControl1.relativeID = selectedRelative.relativeID;
                fileUploadControl1.patientName = SessionManager.Instance.GetActivePatient().name;
                fileUploadControl1.path = baseOutputPath;

                fileUploadControl1.Setup();
            }
            else
            {
                relativeHeader1.setRelative(null);  //this is safe
                this.Enabled = false;
                this.fileUploadControl1.Disable();
            }
        }

        /**************************************************************************************************/
        private void activeRelativeChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void activeRelativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void DocumentUploadView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using System.Reflection;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord
{
    public partial class DemographicsView : HraView
    {
        /**************************************************************************************************/
        Patient activePatient;

        /**************************************************************************************************/
        public DemographicsView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void DemographicsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            //SessionManager.Instance.NewActivePatient += NewActivePatient;

            InitNewPatient();
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patinet object from session manager
            activePatient = SessionManager.Instance.GetActivePatient();

            if (activePatient != null)
            {

                activePatient.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            foreach (Control c in panel2.Controls)
            {
                TextBox t = (TextBox)c;
                t.Text = "";
            }
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControls();
            }
        }
        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void FillControls()
        {
            foreach (Control c in panel2.Controls)
            {
                TextBox t = (TextBox)c;
                t.Text = "";

                foreach (FieldInfo fi in activePatient.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    string name = fi.Name;
                    if (name == t.Name)
                    {
                        t.Text = fi.GetValue(activePatient).ToString();
                        break;
                    }
                }
            }

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        /**************************************************************************************************/
        private void DemographicsTextBox_Leave(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;

            foreach (FieldInfo fi in activePatient.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                string name = fi.Name;
                if (name == t.Name)
                {
                    if (t.Text != fi.GetValue(activePatient).ToString())
                    {
                        fi.SetValue(activePatient, t.Text);
                        activePatient.SignalModelChanged(new HraModelChangedEventArgs(this));
                    }
                    break;
                }
            }
        }

        private void DemographicsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }



        /**************************************************************************************************/
    }
}

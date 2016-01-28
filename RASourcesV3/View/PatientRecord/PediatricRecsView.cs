using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord
{
    public partial class PediatricRecsView : HraView
    {
        private Patient proband;
        private PediatricConsiderations cdsbo;

        public PediatricRecsView()
        {
            InitializeComponent();
        }

        private void PediatricRecsView_Load(object sender, EventArgs e)
        {
            if (ViewClosing == false)
            {
                SessionManager.Instance.NewActivePatient +=
                    new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
                InitNewPatient();
            }
        }
        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patient object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            ClearControls();

            if (proband != null)
            {
                loadingCircle1.Visible = true;
                loadingCircle1.Enabled = true;
                label1.Visible = false;

                proband.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
                
            }
        }

        /**************************************************************************************************/
        private void ClearControls()
        {
            foreach (Control c in Controls)
            {

            }
        }
        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }


        /**************************************************************************************************/

        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            proband.PediatricCDS.AddHandlersWithLoad(PediatricCDSChanged, PediatricCDSLoaded, null);
        }
        /**************************************************************************************************/
        private void PediatricCDSChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void PediatricCDSLoaded(HraListLoadedEventArgs list_e)
        {
            loadingCircle1.Visible = false;
            loadingCircle1.Enabled = false;
            fastDataListView1.ClearObjects();
            fastDataListView1.AddObjects(proband.PediatricCDS);

            try
            {
                label1.Text = proband.PediatricCDS.Max(g => ((PediatricRule)g).PediatricRule_created).ToString();
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog(ee.ToString());
            }
            label1.Visible = true;
        }

        private void PediatricRecsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadingCircle1.Visible = true;
            loadingCircle1.Enabled = true;
            label1.Visible = false;
            proband.PediatricCDS.LoadList();
        }
    }
}

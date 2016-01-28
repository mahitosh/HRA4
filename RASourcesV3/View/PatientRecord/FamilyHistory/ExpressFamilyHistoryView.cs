using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class ExpressFamilyHistoryView : HraView
    {
        /**************************************************************************************************/
        private delegate void NewRelativePmhCallback(ListView lv, PastMedicalHistory pmh);


        /**************************************************************************************************/
        private Patient proband;

        private Model.PatientRecord.FHx.FamilyHistory fhx;

        /**************************************************************************************************/
        public ExpressFamilyHistoryView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void ExpressFamilyHistoryView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            
            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

           InitNewPatient(); 

        }

        /**************************************************************************************************/
        private void ExpressFamilyHistoryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proband != null)
                this.proband.ReleaseListeners(this);
            if (this.fhx != null)
                this.fhx.ReleaseListeners(this);
            SessionManager.Instance.RemoveHraView(this);
        }
        
        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patinet object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            if (proband != null)
            {
                proband.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
            }
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            proband.ReleaseListeners(this);

            InitNewPatient();
        }

        /**************************************************************************************************/
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetFhx();
        }

        /**************************************************************************************************/
        private void LoadOrGetFhx()
        {
            //  get active patient object from session manager
            fhx = SessionManager.Instance.GetActivePatient().FHx;

            if (fhx != null)
            {
                fhx.AddHandlersWithLoad(listChanged, loadFinished, itemChanged);
            }
        }

        private void listChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Person target = (Person)e.hraOperand;
                FamilyHistoryRelativeRow fhrr = null;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        Application.DoEvents();
                        fhrr = new FamilyHistoryRelativeRow(target);
                        flowLayoutPanel1.Controls.Add(fhrr);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        Control toRemove = null;
                        foreach(Control c in flowLayoutPanel1.Controls)
                        {
                            fhrr = (FamilyHistoryRelativeRow)c;
                            if (fhrr.GetRelative().relativeID == target.relativeID)
                            {
                                toRemove = fhrr;
                                break;
                            }
                        }
                        if (toRemove != null)
                        {
                            flowLayoutPanel1.Controls.Remove(toRemove);
                        }
                        break;
                }

                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    fhrr = (FamilyHistoryRelativeRow)c;
                    fhrr.SetDeleteButton();
                }
            }
        }

        private void loadFinished(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        private void itemChanged(object sender, HraModelChangedEventArgs e)
        {

        }


        /**************************************************************************************************/
        delegate void FillControlsCallback();
        private void FillControls()
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                FillControlsCallback rmc = new FillControlsCallback(FillControls);
                this.Invoke(rmc, null);
            }
            else
            {
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;

                foreach (Person p in proband.FHx.Relatives)
                {
                    Application.DoEvents();
                    FamilyHistoryRelativeRow fhrr = new FamilyHistoryRelativeRow(p);
                    flowLayoutPanel1.Controls.Add(fhrr);
                }
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string relationship = "";
            string bloodline = "";

            switch (comboBox1.Text)
            {
                case "Son":
                case "Daughter":
                case "Brother":
                case "Sister":
                case "Cousin":
                case "Cousin (Male)":
                case "Cousin (Female)":
                case "Niece":
                case "Nephew":
                case "Other":
                case "Other (Male)":
                case "Other (Female)":
                    relationship = comboBox1.Text;
                    break;
                case "Aunt - Maternal":
                    relationship = "Aunt";
                    bloodline = "Maternal";
                    break;
                case "Aunt - Paternal":
                    relationship = "Aunt";
                    bloodline = "Paternal";
                    break;
                case "Uncle - Maternal":
                    relationship = "Uncle";
                    bloodline = "Maternal";
                    break;
                case "Uncle - Paternal":
                    relationship = "Uncle";
                    bloodline = "Paternal";
                    break;

            }

            if (relationship.Length > 0)
            {
                List<Person> newRels = fhx.AddRelativeByType(relationship, bloodline, 1);
            }
        }

    }
}

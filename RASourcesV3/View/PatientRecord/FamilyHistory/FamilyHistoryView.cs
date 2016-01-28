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
using System.Reflection;
using RiskApps3.View.Common;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class FamilyHistoryView : HraView
    {
        /**************************************************************************************************/

        private delegate void NewRelativePmhCallback(ListView lv, PastMedicalHistory pmh);

        /**************************************************************************************************/
        private Patient proband;

        private Model.PatientRecord.FHx.FamilyHistory fhx;
        
        private bool selectionSetByUser = true;

        /**************************************************************************************************/

        public FamilyHistoryView()
        {
            InitializeComponent();
           
            listView1.ListViewItemSorter = new FhxSortByRelativeID();
        }

        /**************************************************************************************************/
        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            if (e.sendingView != this)
            {
                foreach (ListViewItem lvi in listView1.Items)
                {
                    Person relative = (Person)lvi.Tag;
                    if (e.selectedRelative == relative)
                    {
                        selectionSetByUser = false;
                        
                        lvi.Selected = true;
                    }
                }
            }
        }

        /**************************************************************************************************/
        private void FamilyHistoryView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

            SessionManager.Instance.RelativeSelected += new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);

            InitNewPatient();
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

        private void LoadOrGetFhx()
        {
            //  get active patient object from session manager
            fhx = SessionManager.Instance.GetActivePatient().FHx;

            if (fhx != null)
            {
                fhx.AddHandlersWithLoad(listChanged, loadFinished, itemChanged);

                if (fhx.IsLoaded)
                {
                    FillControls();
                }
                else
                {
                    loadingCircle1.Active = true;
                    loadingCircle1.Visible = true;
                }
            }
        }

        private void listChanged(HraListChangedEventArgs e)
        {
            FillControls();
        }

        private void loadFinished(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        private void itemChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                bool allPositionUpdate = true;
                foreach (MemberInfo mi in e.updatedMembers)
                {
                    if (mi.Name != "x_position" && mi.Name != "x_norm" && mi.Name != "y_position" && mi.Name != "y_norm")
                        allPositionUpdate = false;
                }
                if (!allPositionUpdate)
                    FillControls();
            }
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            //if (backgroundWorker1.IsBusy)
            //{
            //    backgroundWorker1.CancelAsync();
            //}
            proband.ReleaseListeners(this);
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
            //InitNewPatient();
            LoadOrGetFhx();
        }

        /**************************************************************************************************/

        private void FillControls()
        {
            listView1.Items.Clear();

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            foreach (Person p in proband.FHx.Relatives)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = p.name;
                lvi.SubItems.Add(p.relationship);
                lvi.SubItems.Add(p.bloodline);
                lvi.SubItems.Add(p.age);
                lvi.SubItems.Add(p.vitalStatus);
                lvi.SubItems.Add("");
                lvi.Tag = p;

                listView1.Items.Add(lvi);

                //p.PMH.Observations.AddHandlersWithLoad(RelativePmhChanged, RelativePmhLoaded, null);
                p.PMH.Observations.AddHandlersWithLoad(ClinicalObservationListChanged,
                             ClinicalObservationListLoaded,
                             ClinicalObservationChanged);
            }
            listView1.Sort();
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        /**************************************************************************************************/
        private void ClinicalObservationListLoaded(HraListLoadedEventArgs e)
        {
            if (e.sender != null)
            {
                ClinicalObservationList col = (ClinicalObservationList)(e.sender);
                SetNewRelativePMH(listView1, col.OwningPMH);
            }

        }
        /**************************************************************************************************/
        private void ClinicalObservationChanged(object sender, HraModelChangedEventArgs e)
        {
            if (sender != null)
            {
                ClincalObservation col = (ClincalObservation)(sender);
                SetNewRelativePMH(listView1, col.owningPMH);
            }
        }
        /**************************************************************************************************/
        private void ClinicalObservationListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                ClincalObservation col = (ClincalObservation)(e.hraOperand);
                SetNewRelativePMH(listView1, col.owningPMH);
            }
        }
        /**************************************************************************************************/

        private void SetNewRelativePMH(ListView lv, PastMedicalHistory pmh)
        {
            if (pmh != null)
            {
                if (lv.InvokeRequired)
                {
                    lv.BeginInvoke(new NewRelativePmhCallback(SetNewRelativePMH), lv, pmh);
                }
                else
                {
                    foreach (ListViewItem lvi in lv.Items)
                    {
                        if (lvi.Tag != null)
                        {
                            Person p = (Person)(lvi.Tag);
                            if (pmh.RelativeOwningPMH != null)
                            {
                                if (p == pmh.RelativeOwningPMH)
                                {
                                    lvi.SubItems[5].Text = pmh.GerSummaryText();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        /**************************************************************************************************/

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    listView1.Sort();
        //    loadingCircle1.Active = false;
        //    loadingCircle1.Visible = false;
        //}

        /**************************************************************************************************/

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (selectionSetByUser)
                {
                    ListViewItem listViewItem = listView1.SelectedItems[0];
                    Person relative = (Person)listViewItem.Tag;
                    SessionManager.Instance.SetActiveRelative(this, relative);
                }
                else
                {
                    selectionSetByUser = true;
                }
            }
        }

        /**************************************************************************************************/

        //private void RelativePmhChanged(object sender, HraModelChangedEventArgs e)
        //{
        //    if (sender != this)
        //    {
        //        SetNewRelativePMH(listView1, (PastMedicalHistory) sender);
        //    }
        //}

        ///**************************************************************************************************/

        //private void RelativePmhLoaded(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    SetNewRelativePMH(listView1, (PastMedicalHistory) sender);
        //}

        /**************************************************************************************************/

        private void FamilyHistoryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.proband != null)
                this.proband.ReleaseListeners(this);
            if (this.fhx != null)
                this.fhx.ReleaseListeners(this);
            SessionManager.Instance.RemoveHraView(this);
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
                //case "Cousin - Male":
                //    relationship = "Cousin";
                //    break;
                //case "Cousin - Female":
                //    relationship = "Cousin";
                //    break;
            }

            if (relationship.Length > 0)
            {
                List<Person> newRels = fhx.AddRelativeByType(relationship, bloodline, 1);
            
                //foreach (Person np in newRels)
                //{
                //    np.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(this));
                //}
            }

            //    fhx.SetIDsFromRelationships();

            //    if (newRels.Count > 0)
            //    {
            //RiskApps3.Model.HraModelChangedEventArgs fhx_args = new RiskApps3.Model.HraModelChangedEventArgs(this);
            //fhx_args.Persist = false;
           
            //        FillControls();
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<Person> newRels = new List<Person>();

            int BroCount = 0;
            Int32.TryParse(BroBox.Text, out BroCount);
            if (BroCount > 0)
                fhx.AddRelativeByType("Brother", "", BroCount);

            int SisCount = 0;
            Int32.TryParse(SisBox.Text, out SisCount);
            if (SisCount > 0)
                fhx.AddRelativeByType("Sister", "", SisCount);

            int SonCount = 0;
            Int32.TryParse(SonBox.Text, out SonCount);
            if (SonCount > 0)
                fhx.AddRelativeByType("Son", "", SonCount);

            int DaughterCount = 0;
            Int32.TryParse(DaughterBox.Text, out DaughterCount);
            if (DaughterCount > 0)
                fhx.AddRelativeByType("Daughter", "", DaughterCount);

            int MauntCount = 0;
            Int32.TryParse(MauntBox.Text, out MauntCount);
            if (MauntCount > 0)
                fhx.AddRelativeByType("Aunt", "Maternal", MauntCount);

            int PauntCount = 0;
            Int32.TryParse(PauntBox.Text, out PauntCount);
            if (PauntCount > 0)
                fhx.AddRelativeByType("Aunt", "Paternal", PauntCount);

            int PuncleCount = 0;
            Int32.TryParse(PuncleBox.Text, out PuncleCount);
            if (PuncleCount > 0)
                fhx.AddRelativeByType("Uncle", "Paternal", PuncleCount);

            int MuncleCount = 0;
            Int32.TryParse(MuncleBox.Text, out MuncleCount);
            if (MuncleCount > 0)
                fhx.AddRelativeByType("Uncle", "Maternal", MuncleCount);

            //foreach (Person np in newRels)
            //{
            //    np.SignalModelChanged(new RiskApps3.Model.HraModelChangedEventArgs(this));
            //}

            //fhx.SetIDsFromRelationships();

            //if (newRels.Count > 0)
            //{
            //    //RiskApps3.Model.HraModelChangedEventArgs fhx_args = new RiskApps3.Model.HraModelChangedEventArgs(this);
            //    //fhx_args.Persist = false;
            //    ////TODO fix fhx.SignalModelChanged(fhx_args);
            //    //fhx.lo
            //    //FillControls();
            //}
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            this.loadingCircle1.Center();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Person p in fhx)
                {
                    DateTime dt;
                    if (DateTime.TryParse(p.dob, out dt))
                    {
                        if (p.vitalStatus != "Dead")
                        {
                            p.Person_age = (DateTime.Now.Year - dt.Year).ToString();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }
        }

        private void serializeButton_Click(object sender, EventArgs e)
        {
            FamilyHistoryViewSerializer serializer = new FamilyHistoryViewSerializer(proband, fhx);
            serializer.ShowDialog();
        }

        /**************************************************************************************************/
    }
}
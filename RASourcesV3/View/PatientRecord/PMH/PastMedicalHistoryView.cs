using System;
using System.Collections;
using System.Linq;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Utilities;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.PatientRecord.PMH
{
    public partial class PastMedicalHistoryView : HraView
    {
        /**************************************************************************************************/
        private Person selectedRelative;
        private PastMedicalHistory pmh;
        private DiseaseList diseases;

        /**************************************************************************************************/

        public PastMedicalHistoryView()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            this.loadingCircle1.CenterHorizontally();
            this.noLabel.CenterHorizontally();
        }

        /**************************************************************************************************/

        private void setupGrouping()
        {
            String existingGroupValue = diseaseGroupComboBox.Text;
            
            diseaseGroupComboBox.Items.Clear();
            diseaseGroupComboBox.Autocompleteitems.Clear();
            
            string[] groupingNames = this.diseases
                    .Select(d => ((DiseaseObject)d).groupingName)
                    .Distinct()
                    .ToArray();

            diseaseGroupComboBox.Items.AddRange(groupingNames);

            diseaseGroupComboBox.Autocompleteitems.AddRange(
                groupingNames
                    .Select(gn => new AutoCompleteEntry(gn, gn))
                    .ToList());

            if (!diseaseGroupComboBox.Items.Contains("All Groups"))
            {
                diseaseGroupComboBox.Items.Add("All Groups");
                diseaseGroupComboBox.Autocompleteitems.Add(
                    new AutoCompleteEntry(
                        "All Groups", 
                        "All Groups")
                    );
            }
            diseaseGroupComboBox.Sorted = true;    

            if (!String.IsNullOrEmpty(existingGroupValue))
            {
                if (!diseaseGroupComboBox.Items.Contains(existingGroupValue))
                {
                    diseaseGroupComboBox.Items.Add(existingGroupValue);
                    diseaseGroupComboBox.Autocompleteitems.Add(
                        new AutoCompleteEntry(
                            existingGroupValue, 
                            existingGroupValue)
                        );
                }
            }

            int defaultGroupingID = GetDefaultGroupingId();  //todo: find out default grouping ID based on user
            String defaultGroupingName = this.diseases
                .Where(d => ((DiseaseObject)d).groupingID == defaultGroupingID)
                .Select(d => ((DiseaseObject)d).groupingName)
                .Distinct()
                .SingleOrDefault();

            if (!String.IsNullOrEmpty(defaultGroupingName))
            {
                diseaseGroupComboBox.Text = defaultGroupingName;
            }
            else
            {
                diseaseGroupComboBox.Text = "";
            }
        }

        private int GetDefaultGroupingId()
        {
            int grouping = SessionManager.Instance.ActiveUser.groupingID;
            if (grouping != 0)
            {
                return grouping;
            }
            else
            {
                return 1;
            }
        }

        /**************************************************************************************************/

        private void setupDiseases()
        {
            if (selectedRelative == null)
            {
                return;
            }
            int selectedGroupID = 1;

            String groupName = diseaseGroupComboBox.Text;
            if (!String.IsNullOrEmpty(groupName))
            {
                selectedGroupID = this.diseases
                    .Where(d => ((DiseaseObject)d).groupingName.Equals(groupName))
                    .Select(d => ((DiseaseObject)d).groupingID)
                    .Distinct()
                    .SingleOrDefault();

                if (selectedGroupID == 0)
                {
                    selectedGroupID = -1;
                }
            }

            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is PMHRow)
                {
                    ((PMHRow) control).setGroupID(selectedGroupID);
                }
            }
        }

        /**************************************************************************************************/

        private void PastMedicalHistoryView_Load(object sender, EventArgs e)
        {
            this.diseases = SessionManager.Instance.MetaData.Diseases;
            SessionManager.Instance.MetaData.Diseases.AddHandlersWithLoad(null, DiseasesLoaded, null);
            SessionManager.Instance.NewActivePatient +=NewActivePatient;
            SessionManager.Instance.RelativeSelected +=RelativeSelected;
            InitSelectedRelative();
        }

        private void DiseasesLoaded(HraListLoadedEventArgs e)
        {
            this.setupGrouping();
            this.setupDiseases();
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {            
            InitSelectedRelative();
        }

        /**************************************************************************************************/

        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            InitSelectedRelative();
        }

        /**************************************************************************************************/
        private void InitSelectedRelative()
        {
            flowLayoutPanel1.Controls.Clear();
            
            selectedRelative = SessionManager.Instance.GetSelectedRelative();

            if (selectedRelative != null)
            {
                this.button1.Enabled = true;
                selectedRelative.AddHandlersWithLoad(selectedRelativeChanged, selectedRelativeLoaded, null);
            }
            else
            {
                this.button1.Enabled = false;
                relativeHeader1.setRelative(null);
            }
        }

        /**************************************************************************************************/
        private void LoadOrGetPMH()
        {
            if (pmh != null)
            {
                pmh.Observations.ReleaseListeners(this);
            }

            pmh = SessionManager.Instance.GetSelectedRelative().PMH;

            pmh.Observations.AddHandlersWithLoad(ClinicalObservationListChanged,
                             ClinicalObservationListLoaded,
                             ClinicalObservationChanged);
        }

        /**************************************************************************************************/
        private void AddDiseaseButton_Click(object sender, EventArgs e)
        {
            ClincalObservation co = new ClincalObservation(pmh);
            //SessionManager.Instance.MetaData.Diseases.SetDataFromDiseaseName(ref co);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = false;
            pmh.Observations.AddToList(co, args);
   
            noLabel.Visible = false;
        }

        /**************************************************************************************************/
        private void ClinicalObservationListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null && selectedRelative != null)
            {
                ClincalObservation co = (ClincalObservation)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        PMHRow pmhRow = new PMHRow(co, this);
                        pmhRow.disease.Text = co.disease;
                        flowLayoutPanel1.Controls.Add(pmhRow);
                        pmhRow.disease.Focus(); // drive focus to the first input control jdg 11/16/12

                        //setup grouping
                        int selectedGroupID = 1;
                        String groupName = diseaseGroupComboBox.Text;
                        if (!String.IsNullOrEmpty(groupName))
                        {
                            selectedGroupID = this.diseases
                                .Where(t => ((DiseaseObject)t).groupingName.Equals(groupName))
                                .Select(t => ((DiseaseObject)t).groupingID)
                                .Distinct()
                                .SingleOrDefault();
                        }
                        pmhRow.setGroupID(selectedGroupID);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:

                        Control doomed = null;

                        foreach (Control c in flowLayoutPanel1.Controls.OfType<PMHRow>())
                        {
                            PMHRow targetRow = (PMHRow)c;
                            if (targetRow.GetCO() == co)
                                doomed = c;
                        }
                        if (doomed != null)
                            flowLayoutPanel1.Controls.Remove(doomed);

                        if (pmh.Observations.Count == 0)
                            noLabel.Visible = true;

                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void ClinicalObservationListLoaded(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void ClinicalObservationChanged(object sender, HraModelChangedEventArgs e)
        {
            int senderId = ((ClincalObservation)sender).instanceID;

            PMHRow rowToUpdate = this.flowLayoutPanel1.Controls
                .OfType<PMHRow>()
                //.SingleOrDefault(row => row.GetCO().instanceID == senderId);
                .SingleOrDefault(row => row.GetCO() == sender);

            if (e.Delete)
            {
                this.flowLayoutPanel1.Controls.Remove(rowToUpdate);
            }
            else
            {
                if (e.sendingView != this)
                {
                    foreach (object o in e.updatedMembers)
                    {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)o;
                        if (field.Name == "disease")
                        {
                            rowToUpdate.disease.Text = rowToUpdate.GetCO().disease;
                        }
                        if (field.Name == "age")
                        {
                            rowToUpdate.ageDiagnosis.Text = rowToUpdate.GetCO().ageDiagnosis;
                        }
                        if (field.Name == "comments")
                        {
                            rowToUpdate.comments.Text = rowToUpdate.GetCO().comments;
                        }
                    }
                }
            }

            //FillControls();
        }

        /**************************************************************************************************/
        delegate void fillControlsCallback();

        public void FillControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                fillControlsCallback fcc = new fillControlsCallback(FillControls);
                this.Invoke(fcc, null);
            }
            else
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

                relativeHeader1.setRelative(selectedRelative);

                flowLayoutPanel1.Controls.Clear();

                flowLayoutPanel1.Controls.Add(new PMHRowHeader());

                foreach (ClincalObservation co in pmh.Observations)
                {
                    PMHRow pmhRow = new PMHRow(co, this);
                    pmhRow.disease.Text = co.disease;
                    flowLayoutPanel1.Controls.Add(pmhRow);
                    //Application.DoEvents();
                }

                setupDiseases();

                if (pmh.Observations.Count == 0)
                    noLabel.Visible = true;
                else
                    noLabel.Visible = false;
            }
        }
        /**************************************************************************************************/
        private void selectedRelativeChanged(object sender, HraModelChangedEventArgs e)
        {
            SetRelativeHeaderToSelected();
        }

        private delegate void SetRelativeHeaderToSelectedCallback();
        public void SetRelativeHeaderToSelected()
        {
            if (relativeHeader1.InvokeRequired)
            {
                SetRelativeHeaderToSelectedCallback srhc = new SetRelativeHeaderToSelectedCallback(SetRelativeHeaderToSelected);
                this.Invoke(srhc, null);
            }
            else
            {
                relativeHeader1.setRelative(selectedRelative);
            }
        }
        /**************************************************************************************************/
        private void selectedRelativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetPMH();
        }

        /**************************************************************************************************/
        private void diseaseGroupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setupDiseases();
        }

        /**************************************************************************************************/
        private void PastMedicalHistoryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }
    }
}
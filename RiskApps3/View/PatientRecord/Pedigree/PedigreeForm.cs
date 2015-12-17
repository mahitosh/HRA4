using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Controllers.Pedigree;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using System.Reflection;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Controllers;
using System.Linq;
using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.View.RiskClinic;
using System.Drawing.Printing;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeForm : HraView
    {
        /**************************************************************************************************/
        public delegate void CloseFormCallbackType();
        public CloseFormCallbackType CloseFormDelegate;

        public delegate void PedigreeModelLoadedCallbackType(PedigreeModel model);
        public PedigreeModelLoadedCallbackType PedigreeModelLoadedDelegate;

        public delegate void SetPedigreeParametersCallbackType(PedigreeParameters parameters);
        public SetPedigreeParametersCallbackType SetPedigreeParametersDelegate;

        public delegate void SelectedIndividualCallbackType(Person individual);
        public SelectedIndividualCallbackType SelectedIndividualDelegate;

        public delegate void UpdateAnnotationsCallbackType();
        public UpdateAnnotationsCallbackType UpdateAnnotationsDelegate;

        public delegate void ForeceRefreshCallbackType();
        public ForeceRefreshCallbackType ForeceRefreshDelegate;

        delegate void NewRelativePmhCallback(ListView lv, PastMedicalHistory pmh);

        private string defaultMode = "";

        GUIPreference currentPrefs;
        
        public Dictionary<GeneticTestResult,List<Person>> FamilialVariants;
        //public Dictionary<GeneticTestResult, int> FamilialVariants = new Dictionary<GeneticTestResult, int>();
        
        /**************************************************************************************************/
        PedigreeSettingsForm SettingsForm = new PedigreeSettingsForm(SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.annotations);
        
        char[] trimChars = new char[] { ',', ' ' };

        Patient proband;
        Model.PatientRecord.FHx.FamilyHistory fhx;

        GUIPreferenceList guiPreferenceList;

        /**************************************************************************************************/
        public PedigreeForm()
        {
            InitializeComponent();

            SettingsForm.FrameRateChanged+=new PedigreeSettingsForm.ChangedFrameRateHandler(PedigreeFrameRateChanged);

            CloseFormDelegate = CloseForm;
            PedigreeModelLoadedDelegate = PedigreeModelLoaded;
            SelectedIndividualDelegate = SelectedIndividual;

            pedigreeControl1.Register(CloseForm, PedigreeModelLoadedDelegate, SelectedIndividualDelegate);

        }

        /**************************************************************************************************/
        private void PedigreeForm_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            SessionManager.Instance.RelativeSelected += new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);

            InitNewPatient();
        }

        /**************************************************************************************************/
        private void PedigreeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();

            if (pedigreeControl1.controller != null)
            {
                if (pedigreeControl1.controller.GetMode() == "SELF_ORGANIZING")
                {
                    if (pedigreeControl1.model.converged == false)
                        pedigreeControl1.SavePositions(true);
                }
            }
            
            
            if (guiPreferenceList != null)
                guiPreferenceList.ReleaseListeners(this);

            pedigreeControl1.Close();
            pedigreeControl1.ReleaseListeners();

            if (proband != null)
            {
                proband.FHx.ReleaseListeners(this);
                proband.ReleaseListeners(this);
            }
            SessionManager.Instance.RemoveHraView(this);

            bool riskClinicChild = RiskClinicMainFormInParentChain(this);
            if (riskClinicChild == false)
            {
                int a = SessionManager.Instance.GetActivePatient().apptid;
                string u = SessionManager.Instance.GetActivePatient().unitnum;
                RiskApps3.View.Common.FinalizeRecordForm frm = new RiskApps3.View.Common.FinalizeRecordForm(a, u);
                frm.ShowDialog();

            }
        }
        private static bool RiskClinicMainFormInParentChain(Control f)
        {
            if (f is RiskClinicMainForm)
                return true;
            else
                if (f.Parent == null)
                    return false;
                else
                    return RiskClinicMainFormInParentChain(f.Parent);
        }
        /**************************************************************************************************/
        private void RelativeSelected(RelativeSelectedEventArgs e)
        {
            if (e.sendingView != this)
                pedigreeControl1.SetSelection(e.selectedRelative);
        }

        /**************************************************************************************************/
        public void SetMode(string mode)
        {
            defaultMode = mode;
        }

        /**************************************************************************************************/
        public void PedigreeFrameRateChanged(int newRate)
        {

            pedigreeControl1.FrameRate = newRate;
        }

        /**************************************************************************************************/
        private void relChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                bool found = false;
                foreach (MemberInfo mi in e.updatedMembers)
                {
                    if (mi.Name == "x_norm" || mi.Name == "y_norm")
                    {
                        found = true;
                    }
                }
                if (found)
                {
                    pedigreeControl1.SetPersonPosition((Person)sender);
                }
            }
        }
        
        /**************************************************************************************************/
        delegate void PedigreeModelLoadedCallback(PedigreeModel model);
        public void PedigreeModelLoaded(PedigreeModel model)
        {
            if (this.InvokeRequired)
            {
                PedigreeModelLoadedCallback rmc = new PedigreeModelLoadedCallback(PedigreeModelLoaded);
                object[] args = new object[1];
                args[0] = model;
                this.Invoke(rmc, args);
            }
            else
            {

                model.FamilialVariants = FamilialVariants;

                if (guiPreferenceList != null)
                {
                    guiPreferenceList.ReleaseListeners(this);
                }
                else
                {
                    guiPreferenceList = proband.guiPreferences;

                    guiPreferenceList.AddHandlersWithLoad(GUIPreferenceListChanged,
                                              GUIPreferenceListLoaded,
                                              GUIPreferenceChanged);
                }
                if (defaultMode.Length > 0)
                {
                    pedigreeControl1.controller.SetMode(defaultMode);
                }
                model.parameters.annotation_areas = SettingsForm.annotation_areas;

                float target = ((float)ZoomSlider.Value) / (float)((ZoomSlider.Maximum - ZoomSlider.Minimum) / 2);
                pedigreeControl1.model.parameters.scale = target;
                pedigreeControl1.model.parameters.verticalSpacing = colorSlider1.Value;

                double hscrollRatio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
                double vscrollRatio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;

                pedigreeControl1.model.parameters.hOffset = -1 * ((int)((pedigreeControl1.model.displayXMax * hscrollRatio) - ((double)(this.Width / 2)) / target));
                pedigreeControl1.model.parameters.vOffset = -1 * ((int)((pedigreeControl1.model.displayYMax * vscrollRatio) - ((double)(this.Height / 2)) / target));

                hScrollBar1.Minimum = 0;
                hScrollBar1.Maximum = (int)model.displayXMax;
                hScrollBar1.Value = (int)((double)hScrollBar1.Maximum * hscrollRatio);
                vScrollBar1.Minimum = 0;
                vScrollBar1.Maximum = (int)model.displayYMax;
                vScrollBar1.Value = (int)((double)vScrollBar1.Maximum * vscrollRatio);

                Person selected = SessionManager.Instance.GetSelectedRelative();
                if (selected != null)
                {
                    pedigreeControl1.SetSelection(selected);
                }

                if (model.parameters.multiSelect)
                {
                    pedigreeControl1.Cursor = Cursors.Default;
                    toolStripButton12.Checked = false;
                }
                else
                {
                    pedigreeControl1.Cursor = Cursors.Hand;
                    toolStripButton12.Checked = true;
                }

                if (pedigreeControl1.controller.GetMode() == "SELF_ORGANIZING")
                {
                    toolStripButton3.Checked = true;
                }
                else
                {
                    toolStripButton3.Checked = false;
                }
                
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

            }
        }
        /**************************************************************************************************/
        public void CloseForm()
        {
            this.Close();
        }
        /**************************************************************************************************/
        private void SelectedIndividual(Person individual)
        {
            SessionManager.Instance.SetActiveRelative(this,individual);
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patinet object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            if (proband != null)
            {
                proband.AddHandlersWithLoad(FhxItemChanged, activePatientLoaded, activePatientChanged);
            }
        }

        private void activePatientChanged(object sender, RunWorkerCompletedEventArgs e)
        {
            if (pedigreeComment1.Text != proband.Patient_Comment)
            {
                UpdateComment();//pedigreeComment1.Text = proband.Patient_Comment;
            }
        }
        /**************************************************************************************************/
        delegate void UpdateCommentCallback();
        private void UpdateComment()
        {
            if (this.InvokeRequired)
            {
                UpdateCommentCallback apc = new UpdateCommentCallback(UpdateComment);
                this.Invoke(apc, null);
            }
            else
            {
                pedigreeComment1.Text = proband.Patient_Comment;
            }
        }
        /**************************************************************************************************/
        private void LoadOrGetFhx()
        {
            //  get active patinet object from session manager
            fhx = SessionManager.Instance.GetActivePatient().FHx;
            pedigreeLegend1.ClearObservations();
            if (fhx != null)
            {
                fhx.AddHandlersWithLoad(FhxChanged, loadFinished, FhxItemChanged);
            }
        }
        /**************************************************************************************************/
        private void FhxItemChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                foreach (MemberInfo mi in e.updatedMembers)
                {
                    if (mi.Name.Contains("position"))
                    {
                        if (pedigreeControl1.model != null)
                        {
                            foreach (PedigreeIndividual pi in pedigreeControl1.model.individuals)
                            {
                                if (pi.HraPerson == sender)
                                {
                                    pedigreeControl1.SetPersonFromHraValues(pi, pedigreeControl1.model);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetFhx();
        }

        /**************************************************************************************************/
        private void FhxChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Person p = (Person)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        p.PMH.Observations.AddHandlersWithLoad(ClinicalObservationListChanged,
                             ClinicalObservationListLoaded,
                             ClinicalObservationChanged);
                        p.PMH.GeneticTests.AddHandlersWithLoad(GeneticTestListChanged,
                                       GeneticTestListLoaded,
                                       GeneticTestChanged);
                        p.Ethnicity.AddHandlersWithLoad(null, null, null);
                        p.Nationality.AddHandlersWithLoad(null, null, null);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        p.ReleaseListeners(this);
                        break;
                }

                fhx.SetIDsFromRelationships();
                pedigreeControl1.SetPedigreeFromFHx(fhx);

            }
        }
        /**************************************************************************************************/
        private void loadFinished(HraListLoadedEventArgs e)
        {
            
            FamilialVariants = new Dictionary<GeneticTestResult,List<Person>>();
            FamilialVariants.Clear();
            pedigreeTitleBlock1.SetVariantLabel("");

            pedigreeTitleBlock1.NameText = fhx.proband.name;
            pedigreeTitleBlock1.MRN = fhx.proband.unitnum;
            pedigreeTitleBlock1.DOB = fhx.proband.dob;

            pedigreeComment1.proband = SessionManager.Instance.GetActivePatient();
            pedigreeComment1.Text = fhx.proband.family_comment;

            fhx.SetIDsFromRelationships();
            pedigreeControl1.SetPedigreeFromFHx(fhx);

            lock (SessionManager.Instance.GetActivePatient().FHx)
            {
                //bd note collection modified 
                foreach (Person p in SessionManager.Instance.GetActivePatient().FHx)
                {
                    p.PMH.Observations.AddHandlersWithLoad(ClinicalObservationListChanged,
                                 ClinicalObservationListLoaded,
                                 ClinicalObservationChanged);

                    p.PMH.GeneticTests.AddHandlersWithLoad(GeneticTestListChanged,
                                           GeneticTestListLoaded,
                                           GeneticTestChanged);

                    p.Ethnicity.AddHandlersWithLoad(null, null, null);
                    p.Nationality.AddHandlersWithLoad(null, null, null);
                }
            }
        }

        /**************************************************************************************************/
        private void ClinicalObservationListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                ClincalObservation co = (ClincalObservation)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        co.AddHandlersWithLoad(ClinicalObservationChanged, null, null);
                        if (!String.IsNullOrEmpty(co.disease))
                        {
                            pedigreeLegend1.AddSingleObservation(co, true);
                            if (currentPrefs != null)
                            {
                                currentPrefs.GUIPreference_LegendX = pedigreeLegend1.Location.X;
                                currentPrefs.GUIPreference_LegendY = pedigreeLegend1.Location.Y;
                                currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Size.Width;
                                currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Size.Height;
                            }
                        }

                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        bool remove = true;
                        foreach (Person p in proband.FHx)
                        {
                            foreach (ClincalObservation dx in p.PMH.Observations)
                            {
                                if (string.Compare(co.ClinicalObservation_diseaseDisplayName, dx.ClinicalObservation_diseaseDisplayName, true) == 0 &&
                                    string.Compare(co.ClinicalObservation_diseaseShortName, dx.ClinicalObservation_diseaseShortName) == 0)
                                {
                                    remove = false;
                                }
                            }
                        }
                        if (remove)
                        {
                            pedigreeLegend1.RemoveObservation(co);
                            if (currentPrefs != null)
                            {
                                currentPrefs.GUIPreference_LegendX = pedigreeLegend1.Location.X;
                                currentPrefs.GUIPreference_LegendY = pedigreeLegend1.Location.Y;
                                currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Size.Width;
                                currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Size.Height;
                            }
                        }

                        co.ReleaseListeners(this);
                        break;
                }

                if (currentPrefs != null)
                {
                    //currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Height;
                    //currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Width;

                    if (currentPrefs.GUIPreference_ShowLegend)
                        pedigreeLegend1.CheckForEmpty();
                    else
                        pedigreeLegend1.Visible = false;
                }
            }
        }

        /**************************************************************************************************/
        private void ClinicalObservationListLoaded(HraListLoadedEventArgs e)
        {
            if (e.sender != null)
            {
                ClinicalObservationList theList = (ClinicalObservationList)(e.sender);
                if (theList.Count > 0)
                {
                    foreach (ClincalObservation co in theList)
                    {
                        co.AddHandlersWithLoad(ClinicalObservationChanged, null, null);
                    }
                    pedigreeLegend1.AddObservations(theList);

                }

                if (currentPrefs != null)
                {
                    //currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Height;
                    //currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Width;
                    if (currentPrefs.GUIPreference_ShowLegend)
                        pedigreeLegend1.CheckForEmpty();
                    else
                        pedigreeLegend1.Visible = false;
                }
            }
        }

        /**************************************************************************************************/
        private void ClinicalObservationChanged(object sender, HraModelChangedEventArgs e)
        {
            if (sender != null)
            {
                ClincalObservation co = (ClincalObservation)sender;
                foreach (MemberInfo mi in e.updatedMembers)
                {
                    if (mi.Name == "disease")
                    {
                        pedigreeLegend1.AddSingleObservation(co, true);
                        if (currentPrefs != null)
                        {
                            currentPrefs.GUIPreference_LegendX = pedigreeLegend1.Location.X;
                            currentPrefs.GUIPreference_LegendY = pedigreeLegend1.Location.Y;
                            currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Size.Width;
                            currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Size.Height;

                            // currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Height;
                            //currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Width;

                            //if (currentPrefs.GUIPreference_ShowLegend)
                            //    pedigreeLegend1.CheckForEmpty();
                            //else
                            //    pedigreeLegend1.Visible = false;
                        }
                    }
                }
            }
        }
        /**************************************************************************************************/
        private void GeneticTestListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                GeneticTest gt = (GeneticTest)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        gt.AddHandlersWithLoad(GeneticTestChanged, null, null);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        lock (FamilialVariants)
                        {
                            FamilialVariants = this.fhx.ReloadFamilialVariants();

                            if(true)
                            {
                                string label = "";
                                bool found = false;
                                foreach (GeneticTestResult pos in FamilialVariants.Keys)
                                {
                                    label += pos.geneName + " " + pos.mutationName + " " + pos.resultSignificance + "\n";
                                }
                                pedigreeTitleBlock1.SetVariantLabel(label);
                            }
                        }
                        gt.ReleaseListeners(this);
                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void GeneticTestListLoaded(HraListLoadedEventArgs e)
        {
            if (e.sender != null)
            {
                GeneticTestList theList = (GeneticTestList)(e.sender);
                if (theList.Count > 0)
                {
                    foreach (GeneticTest gt in theList)
                    {
                        ProcessGeneticTest();

                    }
                }
            }
        }

        private void ProcessGeneticTest()
        {
            string label;

            lock (FamilialVariants)
            {
                FamilialVariants = this.fhx.ReloadFamilialVariants();

                label = this.fhx.BuildFamilialVariantsLabel();
            }
            pedigreeTitleBlock1.SetVariantLabel(label);
        }

        /**************************************************************************************************/
        private void GeneticTestChanged(object sender, HraModelChangedEventArgs e)
        {
            if (sender != null)
            {
                GeneticTest gt = (GeneticTest)sender;
                ProcessGeneticTest();
                this.pedigreeControl1.SetPedigreeFromFHx(this.fhx);
            }
        }

        /**************************************************************************************************/
        private void ZoomSlider_Scroll(object sender, ScrollEventArgs e)
        {
            if (pedigreeControl1.model!=null)
            {
                float target = ((float)ZoomSlider.Value) / (float)((ZoomSlider.Maximum - ZoomSlider.Minimum) / 2);
                pedigreeControl1.model.parameters.scale = target;

                double hscrollRatio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
                double vscrollRatio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;

                pedigreeControl1.model.parameters.hOffset = -1 * ((int)((pedigreeControl1.model.displayXMax * hscrollRatio) - ((double)(this.Width / 2)) / target));
                pedigreeControl1.model.parameters.vOffset = -1 * ((int)((pedigreeControl1.model.displayYMax * vscrollRatio) - ((double)(this.Height / 2)) / target));

                if (e.Type == ScrollEventType.EndScroll)
                {
                    if (currentPrefs != null)
                    {
                        currentPrefs.GUIPreference_zoomValue = ZoomSlider.Value;
                    }
                } 
            }
        }
        
        /**************************************************************************************************/
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            float target = ((float)ZoomSlider.Value) / (float)((ZoomSlider.Maximum - ZoomSlider.Minimum) / 2);

            double hscrollRatio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
            double vscrollRatio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;

            pedigreeControl1.model.parameters.hOffset = -1 * ((int)((pedigreeControl1.model.displayXMax * hscrollRatio) - ((double)(this.Width / 2)) / target));
            pedigreeControl1.model.parameters.vOffset = -1 * ((int)((pedigreeControl1.model.displayYMax * vscrollRatio) - ((double)(this.Height / 2)) / target));
        }

        /**************************************************************************************************/
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            float target = ((float)ZoomSlider.Value) / (float)((ZoomSlider.Maximum - ZoomSlider.Minimum) / 2);

            double hscrollRatio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
            double vscrollRatio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;

            pedigreeControl1.model.parameters.hOffset = -1 * ((int)((pedigreeControl1.model.displayXMax * hscrollRatio) - ((double)(this.Width / 2)) / target));
            pedigreeControl1.model.parameters.vOffset = -1 * ((int)((pedigreeControl1.model.displayYMax * vscrollRatio) - ((double)(this.Height / 2)) / target));
        }

        /**************************************************************************************************/
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (pedigreeControl1.controller != null)
            {
                if (toolStripButton3.Checked)
                {
                    //toolStripButton3.Text = "Organizing";
                    pedigreeControl1.PushPositionHistory();
                    pedigreeControl1.controller.SetMode("SELF_ORGANIZING");
                }
                else
                {
                    //toolStripButton3.Text = "Manual";
                    pedigreeControl1.controller.SetMode("MANUAL");
                }
            }
        }

        /**************************************************************************************************/
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            pedigreeControl1.UndoLastPosition();
        }

        /**************************************************************************************************/
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pedigreeControl1.controller.SetMode("SNAP");
        }

        /**************************************************************************************************/
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            pedigreeControl1.CenterIndividuals();
            hScrollBar1.Value = (int)((double)hScrollBar1.Maximum / 2);
            vScrollBar1.Value = (int)((double)hScrollBar1.Maximum / 2);

            float target = ((float)ZoomSlider.Value) / (float)((ZoomSlider.Maximum - ZoomSlider.Minimum) / 2);

            double hscrollRatio = (double)hScrollBar1.Value / (double)hScrollBar1.Maximum;
            double vscrollRatio = (double)vScrollBar1.Value / (double)vScrollBar1.Maximum;

            pedigreeControl1.model.parameters.hOffset = -1 * ((int)((pedigreeControl1.model.displayXMax * hscrollRatio) - ((double)(this.Width / 2)) / target));
            pedigreeControl1.model.parameters.vOffset = -1 * ((int)((pedigreeControl1.model.displayYMax * vscrollRatio) - ((double)(this.Height / 2)) / target));
            pedigreeControl1.SavePositions(true);
        }

        /**************************************************************************************************/
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SettingsForm.FrameRate = pedigreeControl1.FrameRate;
            SettingsForm.preferences = currentPrefs;

            if (SettingsForm.preferences.owningPatient==null)
            {
                SettingsForm.preferences.owningPatient = SessionManager.Instance.GetActivePatient();
                SettingsForm.preferences.SetParent(SessionManager.Instance.ActiveUser.userLogin);
                SettingsForm.preferences.SetForm(this.Text);
                
                //triggers first save to db of all fields
                SettingsForm.preferences.ConsumeSettings(SettingsForm.preferences);
            }
            
            SettingsForm.FillControls();
            SettingsForm.Show();
        }

        /**************************************************************************************************/
        private void colorSlider1_Scroll(object sender, ScrollEventArgs e)
        {
            if (pedigreeControl1.model!=null)
            {
                pedigreeControl1.model.parameters.verticalSpacing = colorSlider1.Value;

                if (e.Type == ScrollEventType.EndScroll)
                {
                    if (currentPrefs != null)
                    {
                        currentPrefs.GUIPreference_verticalSpacing = colorSlider1.Value;
                    }
                } 
            }
        }

        /**************************************************************************************************/
        public PedigreeSettingsForm getPedigreeSettingsForm()
        {
            return SettingsForm;
        }

        /**************************************************************************************************/
        private GUIPreference getBestFitExistingGuiPreference(bool exactMatchOnly)
        {
            GUIPreference bestFitGuiPreference = null;
            List<GUIPreference> localList = proband.guiPreferences.ConvertAll(x => (GUIPreference)x);
            String parentFormText = (this.ParentForm != null) ? this.ParentForm.Text : "";

            if (localList.Count == 0)
            {
                GUIPreference guiPreference;
                //String parentFormText = (this.ParentForm != null) ? this.ParentForm.Text : "";

                guiPreference = new GUIPreference(proband, DateTime.Now, this.Text, parentFormText, this.Width, this.Height);
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
                args.Persist = true;
                proband.guiPreferences.AddToList(guiPreference, args);
                return guiPreference;
            }
            else
            {
                GUIPreference guiPreference = null;
                foreach (GUIPreference gp in localList)
                {
                    guiPreference = gp;
                    guiPreference.ReadOnly = false;
                    guiPreference.ConsumeSettings(guiPreference);    //TODO this is kind of a hack

                    guiPreference.PersistFullObject(new HraModelChangedEventArgs(this));
                    guiPreference.GUIPreference_height = pedigreeControl1.Height;   //TODO and so is this
                    guiPreference.GUIPreference_width = pedigreeControl1.Width;     //TODO ...and this....find better places for all of this...
                }
                return guiPreference;
            }
        }

        /**************************************************************************************************/
        private void GUIPreferenceListLoaded(HraListLoadedEventArgs e)
        {
            currentPrefs = getBestFitExistingGuiPreference(false);
            SettingsForm.preferences = currentPrefs;
            pedigreeControl1.currentPrefs = currentPrefs;

            pedigreeControl1.currentPrefs.GUIPreference_height = pedigreeControl1.Height;
            pedigreeControl1.currentPrefs.GUIPreference_width = pedigreeControl1.Width;

            ApplyPrefs();
        }

        /**************************************************************************************************/
        delegate void ApplyPrefsCallback();
        private void ApplyPrefs()
        {
            if (this.InvokeRequired)
            {
                ApplyPrefsCallback apc = new ApplyPrefsCallback(ApplyPrefs);
                this.Invoke(apc, null);
            }
            else
            {
                if (currentPrefs != null)
                {
                    ZoomSlider.Value = currentPrefs.GUIPreference_zoomValue;
                    ZoomSlider_Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, currentPrefs.GUIPreference_zoomValue));

                    colorSlider1.Value = currentPrefs.GUIPreference_verticalSpacing;
                    colorSlider1_Scroll(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, currentPrefs.GUIPreference_verticalSpacing));


                    //pedigreeTitleBlock1.NameVis = currentPrefs.GUIPreference_ShowTitle;

                    //    pedigreeControl1.model.parameters.drawIdLabels = SettingsForm.ShowRelIds;

                    if (currentPrefs.GUIPreference_ShowLegend)
                        pedigreeLegend1.CheckForEmpty();
                    else
                        pedigreeLegend1.Visible = false;

                    pedigreeLegend1.Background = currentPrefs.GUIPreference_LegendBackground;
                    pedigreeLegend1.BorderStyle = currentPrefs.GUIPreference_LegendBorder;
                    pedigreeLegend1.LegendFont = currentPrefs.GUIPreference_LegendFont;
                    pedigreeLegend1.LegendRadius = currentPrefs.GUIPreference_LegendRadius;
                    pedigreeComment1.Visible = currentPrefs.GUIPreference_ShowComment;
                    pedigreeComment1.Background = currentPrefs.GUIPreference_CommentBackground;
                    pedigreeComment1.BorderStyle = currentPrefs.GUIPreference_CommentBorder;
                    pedigreeComment1.CommentFont = currentPrefs.GUIPreference_CommentFont;
                    pedigreeTitleBlock1.Visible = currentPrefs.GUIPreference_ShowTitle;
                    pedigreeTitleBlock1.SetFonts(currentPrefs.GUIPreference_NameFont, currentPrefs.GUIPreference_UnitnumFont, currentPrefs.GUIPreference_DobFont);
                    pedigreeTitleBlock1.NameVis = currentPrefs.GUIPreference_ShowName;
                    pedigreeTitleBlock1.MRNVis = currentPrefs.GUIPreference_ShowUnitnum;
                    pedigreeTitleBlock1.DOBVis = currentPrefs.GUIPreference_ShowDob;
                    pedigreeTitleBlock1.Spacing = currentPrefs.GUIPreference_TitleSpacing;
                    pedigreeTitleBlock1.BackColor = currentPrefs.GUIPreference_TitleBackground;
                    pedigreeTitleBlock1.BorderStyle = currentPrefs.GUIPreference_TitleBorder;
                    pedigreeControl1.model.parameters.BackgroundBrush = new SolidBrush(currentPrefs.GUIPreference_PedigreeBackground);
                    colorSlider1.BackColor = currentPrefs.GUIPreference_PedigreeBackground;
                    ZoomSlider.BackColor = currentPrefs.GUIPreference_PedigreeBackground;
                    pedigreeControl1.model.parameters.nameWidth = currentPrefs.GUIPreference_nameWidth;
                    pedigreeControl1.model.parameters.limitedEthnicity = currentPrefs.GUIPreference_limitedEthnicity;
                    pedigreeControl1.model.parameters.limitedNationality = currentPrefs.GUIPreference_limitedNationality;

                    pedigreeControl1.model.parameters.VariantFoundText = currentPrefs.VariantFoundText;
                    pedigreeControl1.model.parameters.VariantFoundVusText = currentPrefs.VariantFoundVusText;
                    pedigreeControl1.model.parameters.VariantNotFoundText = currentPrefs.VariantNotFoundText;
                    pedigreeControl1.model.parameters.VariantUnknownText = currentPrefs.VariantUnknownText;
                    pedigreeControl1.model.parameters.VariantNotTestedText = currentPrefs.VariantNotTestedText;
                    pedigreeControl1.model.parameters.VariantHeteroText = currentPrefs.VariantHeteroText;

                    pedigreeLegend1.Location = new Point(currentPrefs.GUIPreference_LegendX, currentPrefs.GUIPreference_LegendY);

                    if (!(currentPrefs.GUIPreference_LegendHeight == SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.GUIPreference_LegendHeight))
                    {
                        pedigreeLegend1.Height = currentPrefs.GUIPreference_LegendHeight;
                    }

                    if (!(currentPrefs.GUIPreference_LegendWidth == SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.GUIPreference_LegendWidth))
                    {
                        pedigreeLegend1.Width = currentPrefs.GUIPreference_LegendWidth;
                    }

                    pedigreeTitleBlock1.Location = new Point(currentPrefs.GUIPreference_TitleX, currentPrefs.GUIPreference_TitleY);
                    pedigreeTitleBlock1.Height = currentPrefs.GUIPreference_TitleHeight;
                    pedigreeTitleBlock1.Width = currentPrefs.GUIPreference_TitleWidth;

                    pedigreeComment1.Location = new Point(currentPrefs.GUIPreference_CommentX, currentPrefs.GUIPreference_CommentY);
                    pedigreeComment1.Height = currentPrefs.GUIPreference_CommentHeight;
                    pedigreeComment1.Width = currentPrefs.GUIPreference_CommentWidth;

                    foreach (PedigreeAnnotation pa in SessionManager.Instance.MetaData.SystemWideDefaultPedigreePrefs.annotations)
                    {
                        SettingsForm.SetPedigreeAnnotation(pa);
                    }
                    foreach (PedigreeAnnotation pa in currentPrefs.annotations)
                    {
                        SettingsForm.SetPedigreeAnnotation(pa);
                    }

                    toolStripButton6.Checked = currentPrefs.hideNonBloodRelatives;
                    pedigreeControl1.model.parameters.hideNonBloodRelatives = currentPrefs.hideNonBloodRelatives;
                    //toolStrip1.Refresh();
                    //pedigreeControl1.model.parameters.hOffset = currentPrefs.GUIPreference_xOffset;
                    //pedigreeControl1.model.parameters.vOffset = currentPrefs.GUIPreference_yOffset;

                    //this.Refresh();
                }
            }
        
        }
        /**************************************************************************************************/
        private void GUIPreferenceListChanged(HraListChangedEventArgs e)
        {
        }
        /**************************************************************************************************/
        private void GUIPreferenceChanged(object sender, HraModelChangedEventArgs e)
        {
            foreach (MemberInfo fi in e.updatedMembers)
            {
                switch (fi.Name)
                {
                    case "ShowLegend":
                        if (currentPrefs.GUIPreference_ShowLegend)
                            pedigreeLegend1.CheckForEmpty();
                        else
                            pedigreeLegend1.Visible = false;
                        break;
                    case "LegendBackground":
                        pedigreeLegend1.Background = currentPrefs.GUIPreference_LegendBackground;
                        break;
                    case "LegendBorder":
                        pedigreeLegend1.BorderStyle = currentPrefs.GUIPreference_LegendBorder;
                        break;
                    case "LegendFont":
                        pedigreeLegend1.LegendFont = currentPrefs.GUIPreference_LegendFont;
                        break;
                    case "LegendRadius":
                        pedigreeLegend1.LegendRadius = currentPrefs.GUIPreference_LegendRadius;
                        break;
                    case "ShowComment":
                        pedigreeComment1.Visible = currentPrefs.GUIPreference_ShowComment;
                        break;
                    case "CommentBackground":
                        pedigreeComment1.Background = currentPrefs.GUIPreference_CommentBackground;
                        break;
                    case "CommentBorder":
                        pedigreeComment1.BorderStyle = currentPrefs.GUIPreference_CommentBorder;
                        break;
                    case "CommentFont":
                        pedigreeComment1.CommentFont = currentPrefs.GUIPreference_CommentFont;
                        break;
                    case "ShowTitle":
                        pedigreeTitleBlock1.Visible = currentPrefs.GUIPreference_ShowTitle;
                        break;
                    case "NameFont":
                    case "UnitnumFont":
                    case "DobFont":
                        pedigreeTitleBlock1.SetFonts(currentPrefs.GUIPreference_NameFont, currentPrefs.GUIPreference_UnitnumFont, currentPrefs.GUIPreference_DobFont);
                        break;
                    case "ShowName":
                        pedigreeTitleBlock1.NameVis = currentPrefs.GUIPreference_ShowName;
                        break;
                    case "ShowUnitnum":
                        pedigreeTitleBlock1.MRNVis = currentPrefs.GUIPreference_ShowUnitnum;
                        break;
                    case "ShowDob":
                        pedigreeTitleBlock1.DOBVis = currentPrefs.GUIPreference_ShowDob;
                        break;
                    case "TitleSpacing":
                        pedigreeTitleBlock1.Spacing = currentPrefs.GUIPreference_TitleSpacing;
                        break;
                    case "TitleBackground":
                        pedigreeTitleBlock1.BackColor = currentPrefs.GUIPreference_TitleBackground;
                        break;
                    case "TitleBorder":
                        pedigreeTitleBlock1.BorderStyle = currentPrefs.GUIPreference_TitleBorder;
                        break;
                    case "PedigreeBackground":
                        pedigreeControl1.model.parameters.BackgroundBrush = new SolidBrush(currentPrefs.GUIPreference_PedigreeBackground);
                        colorSlider1.BackColor = currentPrefs.GUIPreference_PedigreeBackground;
                        ZoomSlider.BackColor = currentPrefs.GUIPreference_PedigreeBackground;
                        break;
                    case "nameWidth":
                        pedigreeControl1.model.parameters.nameWidth = currentPrefs.GUIPreference_nameWidth;
                        break;
                    case "limitedEthnicity":
                        pedigreeControl1.model.parameters.limitedEthnicity = currentPrefs.GUIPreference_limitedEthnicity;
                        break;
                    case "VariantFoundText":
                        pedigreeControl1.model.parameters.VariantFoundText = currentPrefs.GUIPreference_VariantFoundText;
                        break;
                    case "VariantFoundVusText":
                        pedigreeControl1.model.parameters.VariantFoundVusText = currentPrefs.GUIPreference_VariantFoundVusText;
                        break;
                    case "VariantNotFoundText":
                        pedigreeControl1.model.parameters.VariantNotFoundText = currentPrefs.GUIPreference_VariantNotFoundText;
                        break;
                    case "VariantNotTestedText":
                        pedigreeControl1.model.parameters.VariantNotTestedText = currentPrefs.GUIPreference_VariantNotTestedText;
                        break;
                    case "VariantUnknownText":
                        pedigreeControl1.model.parameters.VariantUnknownText = currentPrefs.GUIPreference_VariantUnknownText;
                        break;
                    case "VariantHeteroText":
                        pedigreeControl1.model.parameters.VariantHeteroText = currentPrefs.GUIPreference_VariantHeteroText;
                        break;
                    default:
                        break;
                }
            }
        }


        /**************************************************************************************************/
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            foreach (PedigreeIndividual pi in pedigreeControl1.model.individuals)
            {
                if (pedigreeControl1.model.pointsBeingDragged.Contains(pi.point) == false)
                {
                    double deltaX = pi.point.x % pedigreeControl1.model.parameters.gridWidth;
                    if (deltaX > 0.0)
                    {
                        if (deltaX >= (pedigreeControl1.model.parameters.gridWidth / 2.0))
                            pi.point.x += (pedigreeControl1.model.parameters.gridWidth - deltaX);
                        else
                            pi.point.x -= (deltaX);
                    }

                    double deltaY = pi.point.y % pedigreeControl1.model.parameters.gridHeight;
                    if (deltaY > 0.0)
                    {
                        if (deltaY >= (pedigreeControl1.model.parameters.gridHeight / 2.0))
                            pi.point.y += (pedigreeControl1.model.parameters.gridHeight - deltaY);
                        else
                            pi.point.y -= (deltaY);
                    }

                }
            }
        }

        /**************************************************************************************************/
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            pedigreeControl1.AlignHorizontaly();
        }

        /**************************************************************************************************/
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            pedigreeControl1.AlignVertically();
        }

        /**************************************************************************************************/
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            pedigreeControl1.DistributeHorizontaly(1);
        }

        /**************************************************************************************************/
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            pedigreeControl1.DistributeVertically();
        }

        /**************************************************************************************************/
        private void pedigreeControl1_Load(object sender, EventArgs e)
        {

        }

        /**************************************************************************************************/
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            if (pedigreeControl1.model != null)
            {
                if (toolStripButton12.Checked)
                {
                    pedigreeControl1.Cursor = Cursors.Hand;
                    pedigreeControl1.model.parameters.multiSelect = false;
                }
                else
                {
                    pedigreeControl1.Cursor = Cursors.Default;
                    pedigreeControl1.model.parameters.multiSelect = true;
                }
            }
        }

        /**************************************************************************************************/
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            pedigreeControl1.DistributeHorizontaly(1.1);
        }

        /**************************************************************************************************/
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            pedigreeControl1.DistributeHorizontaly(0.9);
        }

        /**************************************************************************************************/
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (toolStripButton6.Checked)
            {
                SettingsForm.preferences.GUIPreference_hideNonBloodRelatives = true;
                pedigreeControl1.MoveHiddenToSpouse();
            }
            else
            {
                SettingsForm.preferences.GUIPreference_hideNonBloodRelatives = false;
            }

            pedigreeControl1.model.parameters.hideNonBloodRelatives = SettingsForm.preferences.GUIPreference_hideNonBloodRelatives;
            
            //if (toolStripButton6.Checked)
            //{
            //    pedigreeControl1.model.FindBloodRelatives();
            //    //pedigreeControl1.model.parameters.hideNonBloodRelatives = true;
            //    SettingsForm.preferences.GUIPreference_hideNonBloodRelatives = true;
            //    //currentPrefs.hideNonBloodRelatives = true;
            //}
            //else
            //{
            //    //pedigreeControl1.model.parameters.hideNonBloodRelatives = false;
            //    SettingsForm.preferences.GUIPreference_hideNonBloodRelatives = false;
            //    //currentPrefs.hideNonBloodRelatives = false;
            //}
        }

        /**************************************************************************************************/
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            pedigreeControl1.PushPositionHistory();
            List<PedigreeIndividual> remainder = pedigreeControl1.StaticLayout(pedigreeControl1.model.individuals, 0, 0, 0, 0);
            foreach (PedigreeIndividual pi in remainder)
            {
                //pi.point.x = 0;
            }
        }

        private void pedigreeLegend1_Resize(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            //Bitmap b = new Bitmap(Width, Height); 
            //pedigreeControl1.DrawToBitmap(b, new Rectangle(pedigreeControl1.Location, pedigreeControl1.Size));

            Bitmap b = new Bitmap(Width, Height);
            pedigreeControl1.DrawToBitmap(b, new Rectangle(pedigreeControl1.Location, pedigreeControl1.Size));

            if (pedigreeLegend1.Visible &&
                pedigreeLegend1.Location.X > 0 &&
                pedigreeLegend1.Location.Y > 0 &&
                pedigreeLegend1.Size.Width > 0 &&
                pedigreeLegend1.Size.Height > 0)
                pedigreeLegend1.DrawToBitmap(b, new Rectangle(pedigreeLegend1.Location, pedigreeLegend1.Size));

            if (pedigreeComment1.Visible && 
                pedigreeComment1.Location.X > 0 &&
                pedigreeComment1.Location.Y > 0 &&
                pedigreeComment1.Size.Width > 0 &&
                pedigreeComment1.Size.Height > 0)
                pedigreeComment1.DrawToBitmap(b, new Rectangle(pedigreeComment1.Location, pedigreeComment1.Size));

            if (pedigreeTitleBlock1.Visible && 
                pedigreeTitleBlock1.Location.X > 0 &&
                pedigreeTitleBlock1.Location.Y > 0 &&
                pedigreeTitleBlock1.Size.Width > 0 &&
                pedigreeTitleBlock1.Size.Height > 0)
                pedigreeTitleBlock1.DrawToBitmap(b, new Rectangle(pedigreeTitleBlock1.Location, pedigreeTitleBlock1.Size));

            Clipboard.SetImage(b);
        }

        private void pedigreeComment1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentPrefs != null)
            {
                currentPrefs.GUIPreference_CommentX = pedigreeComment1.Location.X;
                currentPrefs.GUIPreference_CommentY = pedigreeComment1.Location.Y;
                currentPrefs.GUIPreference_CommentWidth = pedigreeComment1.Size.Width;
                currentPrefs.GUIPreference_CommentHeight = pedigreeComment1.Size.Height;
            }
        }

        private void pedigreeTitleBlock1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentPrefs != null)
            {
                currentPrefs.GUIPreference_TitleX = pedigreeTitleBlock1.Location.X;
                currentPrefs.GUIPreference_TitleY = pedigreeTitleBlock1.Location.Y;
                currentPrefs.GUIPreference_TitleWidth = pedigreeTitleBlock1.Size.Width;
                currentPrefs.GUIPreference_TitleHeight = pedigreeTitleBlock1.Size.Height;
            }
        }

        private void pedigreeLegend1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentPrefs != null)
            {
                currentPrefs.GUIPreference_LegendX = pedigreeLegend1.Location.X;
                currentPrefs.GUIPreference_LegendY = pedigreeLegend1.Location.Y;
                currentPrefs.GUIPreference_LegendWidth = pedigreeLegend1.Size.Width;
                currentPrefs.GUIPreference_LegendHeight = pedigreeLegend1.Size.Height;
            }
        }

        private void PedigreeForm_Resize(object sender, EventArgs e)
        {
            if (pedigreeControl1.model != null)
            {
                toolStripButton7_Click(sender, e);

                if (pedigreeControl1.currentPrefs != null)
                {
                    pedigreeControl1.currentPrefs.GUIPreference_height = pedigreeControl1.Height;
                    pedigreeControl1.currentPrefs.GUIPreference_width = pedigreeControl1.Width;
                }
            }
        }
        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            if (proband != null)
            {
                RiskAppCore.Globals.setApptID(proband.apptid);
                RiskAppCore.Globals.setUnitNum(proband.unitnum);
                RiskApp.frmLinkFamilies frm = new RiskApp.frmLinkFamilies();
                frm.ShowDialog();
                RiskAppCore.Globals.setApptID(-1);
                RiskAppCore.Globals.setUnitNum("");
            }
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

            //if (MessageBox.Show("WARNING!  Importing data will overwrite existing data for this patient.", 
            //                    "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //{
            //    RiskAppCore.Globals.setApptID(proband.apptid);
            //    RiskApp.frmImportExport frm = new RiskApp.frmImportExport();
            //    frm.ShowDialog();
            //    proband.FHx.LoadList();
                //backgroundWorker1.RunWorkerAsync();
            //}
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            pedigreeControl1.DistributeByAge();
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(Width, Height);
            pedigreeControl1.DrawToBitmap(b, new Rectangle(pedigreeControl1.Location, pedigreeControl1.Size));

            if (pedigreeLegend1.Visible &&
                pedigreeLegend1.Location.X > 0 &&
                pedigreeLegend1.Location.Y > 0 &&
                pedigreeLegend1.Size.Width > 0 &&
                pedigreeLegend1.Size.Height > 0)
                pedigreeLegend1.DrawToBitmap(b, new Rectangle(pedigreeLegend1.Location, pedigreeLegend1.Size));

            if (pedigreeComment1.Visible && pedigreeComment1.Text.Length > 0 &&
                pedigreeComment1.Location.X > 0 &&
                pedigreeComment1.Location.Y > 0 &&
                pedigreeComment1.Size.Width > 0 &&
                pedigreeComment1.Size.Height > 0)
                pedigreeComment1.DrawToBitmap(b, new Rectangle(pedigreeComment1.Location, pedigreeComment1.Size));

            if (pedigreeTitleBlock1.Visible &&
                pedigreeTitleBlock1.Location.X > 0 &&
                pedigreeTitleBlock1.Location.Y > 0 &&
                pedigreeTitleBlock1.Size.Width > 0 &&
                pedigreeTitleBlock1.Size.Height > 0)
                pedigreeTitleBlock1.DrawToBitmap(b, new Rectangle(pedigreeTitleBlock1.Location, pedigreeTitleBlock1.Size));

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += (object printSender, PrintPageEventArgs printE) =>
            {
                //printE.Graphics.DrawImageUnscaled(b, new Point(0, 0));
                float newWidth = b.Width * 100 / b.HorizontalResolution;
                float newHeight = b.Height * 100 / b.VerticalResolution;

                float widthFactor = newWidth / printE.PageBounds.Width;
                float heightFactor = newHeight / printE.PageBounds.Height;

                if (widthFactor > 1 | heightFactor > 1)
                {
                    if (widthFactor > heightFactor)
                    {
                        newWidth = newWidth / widthFactor;
                        newHeight = newHeight / widthFactor;
                    }
                    else
                    {
                        newWidth = newWidth / heightFactor;
                        newHeight = newHeight / heightFactor;
                    }
                }
               printE.Graphics.DrawImage(b, 0, 0, (int)newWidth, (int)newHeight);
            };


            PrintDialog dialog = new PrintDialog();
            dialog.PrinterSettings.DefaultPageSettings.Landscape = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pd.PrinterSettings = dialog.PrinterSettings;
                pd.Print();
            }
        }

        private void gedcomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RiskApps3.Utilities.GedcomParser gp = new RiskApps3.Utilities.GedcomParser();
            gp.ShowDialog();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void hL7ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton17_Click_1(object sender, EventArgs e)
        {
            pedigreeControl1.ShrinkCouples();
        }

        private void progenyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gedcomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RiskApps3.Utilities.GedcomParser gp = new RiskApps3.Utilities.GedcomParser();
            gp.ShowDialog();
        }

        private void otherToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}












///**************************************************************************************************/
//private void UpdatePedigree()
//{
//    fhx.SetIDsFromRelationships();
//    pedigreeControl1.SetPedigreeFromFHx(fhx);

//    pedigreeLegend1.ClearObservations();
//    foreach (Person p in fhx.Relatives)
//    {
//        p.PMH.Observations.AddHandlersWithLoad(null, ObservationsLoaded, null);
//    }

//}

///**************************************************************************************************/
//public void ObservationsLoaded(HraListLoadedEventArgs e)
//{
//    pedigreeLegend1.AddObservations((ClinicalObservationList)(e.sender));

//    bool ready = true;
//    lock (fhx.Relatives)
//    {
//        foreach (Person p in fhx.Relatives)
//        {
//            if (p.PMH.Observations.IsLoaded == false)
//                ready = false;
//        }
//    }

//    if (ready)
//        RefreshLegend();
//}




///**************************************************************************************************/
//delegate void RefreshLegendCallback();
//public void RefreshLegend()
//{
//    if (pedigreeLegend1.InvokeRequired)
//    {
//        RefreshLegendCallback rlc = new RefreshLegendCallback(RefreshLegend);
//        this.Invoke(rlc, null);
//    }
//    else
//    {
//        pedigreeLegend1.Refresh();
//    }
//}

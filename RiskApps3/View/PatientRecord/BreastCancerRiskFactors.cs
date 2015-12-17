using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Controllers;
using RiskApps3.Utilities;
using System.Collections.Generic;

namespace RiskApps3.View.PatientRecord
{
    public partial class BreastCancerRiskFactors : HraView
    {
        private bool Initialized = false;

        private Patient proband;
        private SocialHistory socialHx;
        private PhysicalExamination physicalExam;
        private ObGynHistory obGynHx;
        private Chemoprevention chemoprevention;
        public ProcedureHx procHx;
        private PastMedicalHistory PMHHx;

        public BreastCancerRiskFactors()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void BreastCancerRiskFactors_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += NewActivePatient;

                InitNewPatient();
                // TODO: Fill the rest of the combo boxes from metadata
                // Curiously, biopsyAtypical isn't in lkpLookups...
                //UIUtils.fillComboBoxFromLookups(biopsyAtypical, "tblRiskDataCustom", "biospyAtypical", true);

                //loadingCircle1.Active = true;
                //loadingCircle1.Visible = true;
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
                proband.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void ClearControls()
        {
            foreach (Control c in Controls)
            {
                if (c is TextBox || c is ComboBox)
                {
                    c.Text = "";
                }
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
            isAshkenazi.Text = proband.Person_isAshkenazi;
            isHispanic.Text = proband.Person_isHispanic;
        }

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            genTest.Text = proband.Patient_geneticTesting;
            genTestResult.Text = proband.Patient_geneticTestingResult;

            LoadOrGetSocialHx();
            LoadOrGetPhysicalExam();
            LoadOrGetObGynHx();
            LoadOrGetChemoprevention();
            LoadOrGetRaceEthnicity();
            LoadOrGetProcHx();
            LoadOrGetPMHHx();

            proband.Patient_riskFactorsConfirmed = 1;
        }

        /**************************************************************************************************/
        private void LoadOrGetPMHHx()
        {
            //  get active patinet object from session manager
            PMHHx = SessionManager.Instance.GetActivePatient().PMH;

            if (PMHHx != null)
            {
                PMHHx.AddHandlersWithLoad(PMHHxChanged, PMHHxLoaded, PMHHxItemChanged);
                //PMHHx.AddHandlersWithLoad(PMHHxChanged, PMHHxLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void LoadOrGetSocialHx()
        {
            //  get active patinet object from session manager
            socialHx = SessionManager.Instance.GetActivePatient().SocialHx;

            if (socialHx != null)
            {
                socialHx.AddHandlersWithLoad(SocialHxChanged, SocialHxLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void PMHHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillPMHHxControls();
            }
        }

        /**************************************************************************************************/
        private void PMHHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillPMHHxControls();
        }

        /**************************************************************************************************/
        private void PMHHxItemChanged(object sender, RunWorkerCompletedEventArgs e)
        {
              FillPMHHxControls();
        }
        /**************************************************************************************************/
        private void SocialHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillSocialHxControls();
            }
        }

        /**************************************************************************************************/
        private void SocialHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillSocialHxControls();
        }


        /**************************************************************************************************/
        private void LoadOrGetPhysicalExam()
        {
            //  get active patinet object from session manager
            physicalExam = SessionManager.Instance.GetActivePatient().PhysicalExam;

            if (physicalExam != null)
            {
                physicalExam.AddHandlersWithLoad(PhysicalExamChanged, PhysicalExamLoaded, null);
            }
        }


        /**************************************************************************************************/
        private void LoadOrGetObGynHx()
        {
            //  get active patinet object from session manager
            obGynHx = SessionManager.Instance.GetActivePatient().ObGynHx;

            if (obGynHx != null)
            {
                obGynHx.AddHandlersWithLoad(ObGynHxChanged, ObGynHxLoaded, null);
            }
        }

        /**************************************************************************************************/
        private void LoadOrGetChemoprevention()
        {
            chemoprevention = SessionManager.Instance.GetActivePatient().MedHx.chemoprevention;

            if (chemoprevention != null)
            {
                chemoprevention.AddHandlersWithLoad(ChemoPreventionChanged, ChemoPreventionLoaded, null);
            }
        }


        /**************************************************************************************************/
        private void LoadOrGetRaceEthnicity()
        {
            //proband.Ethnicity.AddHandlersWithLoad(RaceEthnicityChanged, RaceEthnicityLoaded, null);
            proband.Ethnicity = SessionManager.Instance.GetActivePatient().Ethnicity;
            if (proband.Ethnicity != null)
            {
                proband.Ethnicity.AddHandlersWithLoad(RaceEthnicityChanged, RaceEthnicityLoaded, null);
                
            }
        }


        /**************************************************************************************************/
        private void LoadOrGetProcHx()
        {
            procHx = SessionManager.Instance.GetActivePatient().procedureHx;

            if (procHx != null)
            {
                procHx.AddHandlersWithLoad(ProcHxChanged, ProcHxLoaded, null);
            }
        }


        /**************************************************************************************************/
        private void PhysicalExamChanged(object sender, HraModelChangedEventArgs e)  // todo Examine eventargs and only update those items
        {
            if (e.sendingView != this)
            {
                FillPhysicalExamControls();
            }
        }

        /**************************************************************************************************/
        private void PhysicalExamLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillPhysicalExamControls();
        }

        /**************************************************************************************************/
        private void ObGynHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillObGynHxControls();
            }
        }

        /**************************************************************************************************/
        private void ObGynHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillObGynHxControls();
        }

        /**************************************************************************************************/
        private void ChemoPreventionChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillChemoPreventionControls();
            }
        }

        /**************************************************************************************************/
        private void ChemoPreventionLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillChemoPreventionControls();
        }


        /**************************************************************************************************/
        private void RaceEthnicityChanged(HraListChangedEventArgs e)
        {
            
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }

            foreach (Race r in proband.Ethnicity)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (string.Compare(checkedListBox1.Items[i].ToString(), r.race, true) == 0)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                        break;
                    }
                }
            }

            FillRaceEthnicityControls();
        }

        /**************************************************************************************************/
        //private void RaceEthnicityLoaded(object sender, RunWorkerCompletedEventArgs e)
        private void RaceEthnicityLoaded(HraListLoadedEventArgs e)
        {
            checkedListBox1.Enabled = true;

            foreach (Race r in proband.Ethnicity)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (string.Compare(checkedListBox1.Items[i].ToString(), r.race, true) == 0)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                        break;
                    }
                }
            }

            FillRaceEthnicityControls();
        }

        /**************************************************************************************************/
        private void ProcHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillBreastBxControls();
            }
        }

        /**************************************************************************************************/
        private void ProcHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillBreastBxControls();
        }

        /**************************************************************************************************/
        private void FillSocialHxControls()
        {
            if (string.IsNullOrEmpty(socialHx.SocialHistory_hasSmoked) == false)
            {
                if (hasSmoked.Items.Contains(socialHx.SocialHistory_hasSmoked))
                {
                    hasSmoked.Items.Add(socialHx.SocialHistory_hasSmoked);
                }
            }
            hasSmoked.Text = socialHx.SocialHistory_hasSmoked;


            if (string.IsNullOrEmpty(socialHx.SocialHistory_hasAlcohol) == false)
            {
                if (hasAlcohol.Items.Contains(socialHx.SocialHistory_hasAlcohol))
                {
                    hasAlcohol.Items.Add(socialHx.SocialHistory_hasAlcohol);
                }
            }
            hasAlcohol.Text = socialHx.SocialHistory_hasAlcohol;
        }

        /**************************************************************************************************/
        private void FillPMHHxControls()
        {
            ageDiagnosis.Text = getOophAge();
        }

        private string getOophAge()
        {
            // Helper function:  Iterate through Clinical Observations to see if there was a previous Bilateral Oophorectomy
            foreach (ClincalObservation co in PMHHx.Observations)
            {
                if (co.disease.Equals("Bilateral Oophorectomy"))
                    if (string.IsNullOrEmpty(co.ageDiagnosis)==false)
                        return co.ageDiagnosis.ToString();
            }
            return "";
        }
        /**************************************************************************************************/
        private void FillPhysicalExamControls()
        {
            weightPounds.Text = physicalExam.PhysicalExamination_weightPounds;

            string heightInches = physicalExam.PhysicalExamination_heightInches;

            if (!string.IsNullOrEmpty(heightInches))
            {
                int totalInches = Int32.Parse(heightInches);

                feet.Text = "" + (totalInches/12);
                inches.Text = "" + (totalInches%12);
            }

        }

        /**************************************************************************************************/
        private void FillObGynHxControls()
        {
            timesPregnant.Text = obGynHx.ObGynHistory_timesPregnant;
            numChildren.Text = obGynHx.ObGynHistory_numChildren;
            ageFirstChildBorn.Text = obGynHx.ObGynHistory_ageFirstChildBorn;

            currentlyPregnant.Text = obGynHx.ObGynHistory_currentlyPregnant;
            currentlyNursing.Text = obGynHx.ObGynHistory_currentlyNursing;
            doneChild.Text = obGynHx.ObGynHistory_doneChild;

            birthControlUse.Text = obGynHx.ObGynHistory_birthControlUse;
            birthControlAge.Text = obGynHx.ObGynHistory_birthControlAge;
            birthControlYears.Text = obGynHx.ObGynHistory_birthControlYears;
            birthControlContinuously.Text = obGynHx.ObGynHistory_birthControlContinuously;


            startedMenstruating.Text = obGynHx.ObGynHistory_startedMenstruating;
            currentlyMenstruating.Text = obGynHx.ObGynHistory_currentlyMenstruating;
            stoppedMenstruating.Text = obGynHx.ObGynHistory_stoppedMenstruating;
            hadHysterectomy.Text = obGynHx.ObGynHistory_hadHysterectomy;
            hysterectomyAge.Text = obGynHx.ObGynHistory_hysterectomyAge;
            bothOvariesRemoved.Text = obGynHx.ObGynHistory_bothOvariesRemoved;
            menopausalStatus.Text = obGynHx.ObGynHistory_menopausalStatus;

            hormoneUse.Text = obGynHx.ObGynHistory_hormoneUse;
            hormoneCombined.Text = obGynHx.ObGynHistory_hormoneCombined;
            hormoneUseYears.Text = obGynHx.ObGynHistory_hormoneUseYears;
            hormoneIntendedLength.Text = obGynHx.ObGynHistory_hormoneIntendedLength;
            hormoneYearsSinceLastUse.Text = obGynHx.ObGynHistory_hormoneYearsSinceLastUse;

            lmp.Text = obGynHx.ObGynHistory_lastPeriodDate;
            lmp_confidence.Text = obGynHx.ObGynHistory_LMP_Confidence;

        }

        /**************************************************************************************************/
        private void FillChemoPreventionControls()
        {
            takenTamoxifen.Text = chemoprevention.Chemoprevention_takenTamoxifen;
            takenRaloxifene.Text = chemoprevention.Chemoprevention_takenRaloxifene;

        }

        /**************************************************************************************************/
        private void FillRaceEthnicityControls()
        {
            isAshkenazi.Text = proband.Person_isAshkenazi;
            isHispanic.Text = proband.Person_isHispanic;

        }


        /**************************************************************************************************/
        private void FillBreastBxControls()
        {
            //breastBiopsies.Text = procHx.BreastBx_breastBiopsies;
            //biopsyAtypical.Text = procHx.BreastBx_biopsyAtypical;

            chestRadiationBox.Text = procHx.ProcedureHx_chestRadiation;
            
            breastBiopsies.Text = procHx.breastBx.BreastBx_breastBiopsies;
            biopsyAtypical.Text = procHx.breastBx.BreastBx_biopsyAtypical;
            biopsyLCIS.Text = procHx.breastBx.BreastBx_biopsyLCIS;

        }


        /**************************************************************************************************/
        private void height_Leave(object sender, EventArgs e)
        {
            String str = "";
            int totalInches = 0;
            if (feet.Text != "")
            {
                str = str + feet.Text + "'";
                totalInches = totalInches + Int32.Parse(feet.Text)*12;
            }
            if (inches.Text != "")
            {
                str = str + inches.Text + "\"";
                totalInches = totalInches + Int32.Parse(inches.Text);
            }

            physicalExam.PhysicalExamination_heightInches = totalInches.ToString();
            physicalExam.PhysicalExamination_heightFeetInches = str;

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);

            args.updatedMembers.Add(physicalExam.GetMemberByName("heightInches"));
            args.updatedMembers.Add(physicalExam.GetMemberByName("heightFeetInches"));

            physicalExam.SignalModelChanged(args);
        }

        private void BreastCancerRiskFactors_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        private void weightPounds_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                physicalExam.PhysicalExamination_weightPounds = weightPounds.Text;
        }

        private void currentlyPregnant_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_currentlyPregnant = currentlyPregnant.Text;
        }

        private void isAshkenazi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                proband.Person_isAshkenazi = isAshkenazi.Text;
        }

        private void birthControlUse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_birthControlUse = birthControlUse.Text;
        }

        private void birthControlAge_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_birthControlAge = birthControlAge.Text;
        }

        private void birthControlYears_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_birthControlYears = birthControlYears.Text;
        }

        private void birthControlContinuously_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_birthControlContinuously = birthControlContinuously.Text;
        }

        private void breastBiopsies_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                procHx.breastBx.BreastBx_breastBiopsies = breastBiopsies.Text;
        }

        private void biopsyAtypical_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                procHx.breastBx.BreastBx_biopsyAtypical = biopsyAtypical.Text;
                MessageBox.Show("WARNING! - Confirming this information can impact the patient’s risk.  Please make sure that the Family History tab contains the proper information for patient diseases.", "Warning!", MessageBoxButtons.OK);
            }
        }
        private void isHispanic_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                proband.Person_isHispanic = isHispanic.Text;
        }

        private void timesPregnant_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_timesPregnant = timesPregnant.Text;
        }

        private void numChildren_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_numChildren = numChildren.Text;
        }

        private void ageFirstChildBorn_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_ageFirstChildBorn = ageFirstChildBorn.Text;
        }

        private void currentlyNursing_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_currentlyNursing = currentlyNursing.Text;
        }

        private void doneChild_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_doneChild = doneChild.Text;
        }

        private void hormoneUse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneUse = hormoneUse.Text;
        }

        private void hormoneCombined_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneCombined = hormoneCombined.Text;
        }

        private void hormoneUseYears_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneUseYears = hormoneUseYears.Text;
        }

        private void hormoneIntendedLength_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneIntendedLength = hormoneIntendedLength.Text;
        }

        private void hormoneYearsSinceLastUse_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneYearsSinceLastUse = hormoneYearsSinceLastUse.Text;
        }

        private void takenTamoxifen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                chemoprevention.Chemoprevention_takenTamoxifen = takenTamoxifen.Text;
        }

        private void takenRaloxifene_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                chemoprevention.Chemoprevention_takenRaloxifene = takenRaloxifene.Text;
        }

        private void startedMenstruating_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_startedMenstruating = startedMenstruating.Text;
        }

        private void currentlyMenstruating_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_currentlyMenstruating = currentlyMenstruating.Text;
        }

        private void stoppedMenstruating_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_stoppedMenstruating = stoppedMenstruating.Text;
        }

        private void hadHysterectomy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hadHysterectomy = hadHysterectomy.Text;
        }

        private void hysterectomyAge_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hysterectomyAge = hysterectomyAge.Text;
        }

        private void bothOvariesRemoved_SelectionChangeCommitted(object sender, EventArgs e)
        {
            obGynHx.ObGynHistory_bothOvariesRemoved = bothOvariesRemoved.Text;
            if (bothOvariesRemoved.Text != "Yes")
            {
                ageDiagnosis.Text = "";
                foreach (ClincalObservation co in PMHHx.Observations)
                {
                    if (co.disease.Equals("Bilateral Oophorectomy"))
                    {
                        PMHHx.Observations.RemoveFromList(co, SessionManager.Instance.securityContext);
                        return;
                    }
                }
            }
        }

        private void menopausalStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_menopausalStatus = menopausalStatus.Text;
        }

        private void hasSmoked_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                socialHx.SocialHistory_hasSmoked = hasSmoked.Text;
        }

        private void hasAlcohol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                socialHx.SocialHistory_hasAlcohol = hasAlcohol.Text;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Contains(checkedListBox1.SelectedItem))
            {
                List<int> descedants = new List<int>();
                descedants.Add(proband.relativeID);
                proband.owningFHx.GetDescendants(proband.relativeID, ref descedants);
                foreach (int i in descedants)
                {
                    Person p = proband.owningFHx.getRelative(i);
                    if (p != null)
                    {
                        Race r = new Race(p.Ethnicity);
                        r.race = checkedListBox1.SelectedItem.ToString();
                        p.Ethnicity.AddToList(r, new HraModelChangedEventArgs(this));
                    }
                }
            }
            else
            {
                Race doomed = null;
                foreach (Race r in proband.Ethnicity)
                {
                    if (string.Compare(checkedListBox1.SelectedItem.ToString(), r.race, true) == 0)
                    {
                        doomed = r;
                        break;
                    }

                }
                if (doomed != null)
                {
                    //proband.Ethnicity.RemoveFromList(doomed, SessionManager.Instance.securityContext);
                    proband.owningFHx.getRelative(1).Ethnicity.RemoveFromList(doomed, SessionManager.Instance.securityContext);
                }
            }
            
        }

        private void ageDiagnosis_Validated(object sender, EventArgs e)
        {

            foreach (ClincalObservation co in PMHHx.Observations)
            {
                if (co.disease.Equals("Bilateral Oophorectomy"))
                {
                    if ((String.IsNullOrEmpty(ageDiagnosis.Text)) || (bothOvariesRemoved.Text != "Yes"))
                    {
                        PMHHx.Observations.RemoveFromList(co, SessionManager.Instance.securityContext);
                    }
                    else
                    {
                        co.ageDiagnosis = ageDiagnosis.Text;
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        args.Persist = true;
                        args.updatedMembers.Add(co.GetMemberByName("ageDiagnosis"));
                        co.SignalModelChanged(args);
                    }
                    return;
                }
            }

            // add the disease... 
            if (bothOvariesRemoved.Text.Equals("Yes"))
            {
                ClincalObservation co2 = new ClincalObservation(PMHHx);
                co2.disease = "Bilateral Oophorectomy";
                co2.SetDiseaseDetails();
                //SessionManager.Instance.MetaData.Diseases.SetDataFromDiseaseName(ref co2);
                co2.ageDiagnosis = ageDiagnosis.Text;
                HraModelChangedEventArgs args2 = new HraModelChangedEventArgs(null);
                args2.Persist = true;
                PMHHx.Observations.AddToList(co2, args2);
            }
        }

        private void lmp_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_lastPeriodDate = lmp.Text;
        }

        private void lmp_confidence_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_LMP_Confidence = lmp_confidence.Text;
        }

        private void chestRadiationBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                procHx.ProcedureHx_chestRadiation = chestRadiationBox.Text;
        }

        private void biopsyLCIS_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                procHx.breastBx.BreastBx_biopsyLCIS = biopsyLCIS.Text;
                MessageBox.Show("WARNING! - Confirming this information can impact the patient’s risk.  Please make sure that the Family History tab contains the proper information for patient diseases.","Warning!",MessageBoxButtons.OK);
            }
        }
        private void genTest_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                proband.Patient_geneticTesting = genTest.Text;
        }

        private void genTestResult_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ViewClosing)
                proband.Patient_geneticTestingResult = genTestResult.Text;
        }
    }
}
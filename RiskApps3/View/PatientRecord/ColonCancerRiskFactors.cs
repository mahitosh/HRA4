using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.View;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;

namespace RiskApps3.View.PatientRecord
{
    public partial class ColonCancerRiskFactors : HraView
    {
        private bool Initialized = false;

        private Patient proband;
        private SocialHistory socialHx;
        private PhysicalExamination physicalExam;
        private ObGynHistory obGynHx;

        public ColonCancerRiskFactors()
        {
            InitializeComponent();
        }
        private void ColonCancerRiskFactors_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += NewActivePatient;

                InitNewPatient();
            }
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }
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
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetSocialHx();
            LoadOrGetPhysicalExam();
            LoadOrGetObGynHx();
            LoadOrGetRaceEthnicity();
        }

        /**************************************************************************************************/
        private void LoadOrGetRaceEthnicity()
        {
            proband.Ethnicity = SessionManager.Instance.GetActivePatient().Ethnicity;
            if (proband.Ethnicity != null)
            {
                proband.Ethnicity.AddHandlersWithLoad(RaceEthnicityChanged, RaceEthnicityLoaded, null);

            }
        }
        /**************************************************************************************************/
        private void FillRaceEthnicityControls()
        {
            isAshkenazi.Text = proband.Person_isAshkenazi;
            isHispanic.Text = proband.Person_isHispanic;

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
        private void FillObGynHxControls()
        {

            currentlyMenstruating.Text = obGynHx.ObGynHistory_currentlyMenstruating;
            stoppedMenstruating.Text = obGynHx.ObGynHistory_stoppedMenstruating;

            hormoneUse.Text = obGynHx.ObGynHistory_hormoneUse;
            hormoneYearsSinceLastUse.Text = obGynHx.ObGynHistory_hormoneYearsSinceLastUse;

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
        private void FillPhysicalExamControls()
        {
            weightPounds.Text = physicalExam.PhysicalExamination_weightPounds;

            string heightInches = physicalExam.PhysicalExamination_heightInches;

            if (!string.IsNullOrEmpty(heightInches))
            {
                int totalInches = Int32.Parse(heightInches);

                feet.Text = "" + (totalInches / 12);
                inches.Text = "" + (totalInches % 12);
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
        private void FillSocialHxControls()
        {
            tblRiskDataCustom_numYearsSmokedCigarettes.Text = socialHx.SocialHistory_numYearsSmokedCigarettes;
            tblRiskDataCustom_numCigarettesPerDay.Text = socialHx.SocialHistory_numCigarettesPerDay;
            tblRiskDataExt2_vegetableServingsPerDay.Text = socialHx.SocialHistory_vegetableServingsPerDay;
            tblRiskDataExt2_vigorousPhysicalActivityHoursPerWeek.Text = socialHx.vigorousPhysicalActivityHoursPerWeek;

            tblRiskDataExt2_aspirinRegularUse.Text = socialHx.SocialHistory_RegularAspirinUser;
            tblRiskDataExt2_ibuprofenRegularUse.Text = socialHx.SocialHistory_RegularIbuprofenUser;
            tblRiskDataExt2_colonoscopyLast10Years.Text = socialHx.SocialHistory_colonoscopyLast10Years;
            tblRiskDataExt2_colonPolypLast10Years.Text = socialHx.SocialHistory_colonPolypLast10Years;
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

        private void weightPounds_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                physicalExam.PhysicalExamination_weightPounds = weightPounds.Text;

        }

        private void feet_Leave(object sender, EventArgs e)
        {
            String str = "";
            int totalInches = 0;
            if (feet.Text != "")
            {
                str = str + feet.Text + "'";
                totalInches = totalInches + Int32.Parse(feet.Text) * 12;
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

        private void isAshkenazi_SelectionChangeCommitted(object sender, EventArgs e)
        {
            proband.Person_isAshkenazi = isAshkenazi.Text;
        }

        private void isHispanic_SelectionChangeCommitted(object sender, EventArgs e)
        {
            proband.Person_isHispanic = isHispanic.Text;
        }

        private void tblRiskDataCustom_numYearsSmokedCigarettes_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                socialHx.SocialHistory_numYearsSmokedCigarettes = tblRiskDataCustom_numYearsSmokedCigarettes.Text;
        }

        private void tblRiskDataCustom_numCigarettesPerDay_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                socialHx.SocialHistory_numCigarettesPerDay = tblRiskDataCustom_numCigarettesPerDay.Text;
        }

        private void tblRiskDataExt2_vegetableServingsPerDay_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_vegetableServingsPerDay = tblRiskDataExt2_vegetableServingsPerDay.Text;

        }

        private void tblRiskDataExt2_vigorousPhysicalActivityHoursPerWeek_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_vigorousPhysicalActivityHoursPerWeek = tblRiskDataExt2_vigorousPhysicalActivityHoursPerWeek.Text;
        }

        private void currentlyMenstruating_SelectionChangeCommitted(object sender, EventArgs e)
        {
            obGynHx.ObGynHistory_currentlyMenstruating = currentlyMenstruating.Text;
        }

        private void stoppedMenstruating_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_stoppedMenstruating = stoppedMenstruating.Text;

        }

        private void hormoneUse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            obGynHx.ObGynHistory_hormoneUse = hormoneUse.Text;
        }

        private void hormoneYearsSinceLastUse_Validated(object sender, EventArgs e)
        {
            if (!ViewClosing)
                obGynHx.ObGynHistory_hormoneYearsSinceLastUse = hormoneYearsSinceLastUse.Text;

        }

        private void tblRiskDataExt2_aspirinRegularUse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_RegularAspirinUser = tblRiskDataExt2_aspirinRegularUse.Text;

        }

        private void tblRiskDataExt2_ibuprofenRegularUse_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_RegularIbuprofenUser = tblRiskDataExt2_ibuprofenRegularUse.Text;
        }

        private void tblRiskDataExt2_colonoscopyLast10Years_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_colonoscopyLast10Years = tblRiskDataExt2_colonoscopyLast10Years.Text;
        }

        private void tblRiskDataExt2_colonPolypLast10Years_SelectionChangeCommitted(object sender, EventArgs e)
        {
            socialHx.SocialHistory_colonPolypLast10Years = tblRiskDataExt2_colonPolypLast10Years.Text;

        }

        private void ColonCancerRiskFactors_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }





    }
}

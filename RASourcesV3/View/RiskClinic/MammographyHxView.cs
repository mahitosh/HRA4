using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;

namespace RiskApps3.View.RiskClinic
{
    public partial class MammographyHxView : HraView
    {
        private const string TblRiskDataExt1 = "tblRiskDataExt1";
        private const string Yes = "Yes";
        private const string No = "No";
        private const string ForwardSlashPattern = "/";

        private Patient _proband;
        private MammographyHx _mammoHx;
        private BreastBx _breastBx;

        public MammographyHxView()
            : base(TblRiskDataExt1)
        {
            InitializeComponent();

            InitializeDropDowns();
            UserList users = new UserList();
            users.LoadFullList();

            List<string> userList = users.Select(user => string.Format("{0}, {1}", ((User)user).userLastName, ((User)user).userFirstName)).ToList();

            UIUtils.fillComboBoxFromList(this.createdby, userList, true);

        }


        private void InitializeMDYComboBoxes(ComboBox monthDropdown, ComboBox dayDropdown, ComboBox yearDropdown)
        {
            monthDropdown.Items.Add("");
            for (int i = 1; i <= 12; i++)
            {
                monthDropdown.Items.Add(String.Format("{0:D2}", i));
            }

            dayDropdown.Items.Add("");
            for (int i = 1; i <= 31; i++)
            {
                dayDropdown.Items.Add(String.Format("{0:D2}", i));
            }

            yearDropdown.Items.Add("");
            for (int i = 0; i < 50; i++)
            {
                yearDropdown.Items.Add((DateTime.Now.Year - i).ToString());
            }

        }

        private void MammographyHxView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += NewActivePatientEventHandler;

                InitNewPatient();
            }
        }

        void NewActivePatientEventHandler(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        private void InitNewPatient()
        {
            this._proband = SessionManager.Instance.GetActivePatient();

            this.groupBox.Controls.Cast<Control>()
                .Where(control => control is TextBox || control is ComboBox)
                .ToList()
                .ForEach(control => control.Text = string.Empty);

            if (this._proband != null)
            {
                this._proband.AddHandlersWithLoad(null, ActivePatientLoadedHandler, null);
            }
        }

        private void ActivePatientLoadedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetMammoHxAndBreastBx();
        }

        private void LoadOrGetMammoHxAndBreastBx()
        {
            //TODO what to do if HraObjects are indeed null for some reason???
            Patient activePatient = SessionManager.Instance.GetActivePatient();

            this._mammoHx = activePatient.MammographyHx;

            if (this._mammoHx != null)
            {
                this._mammoHx.AddHandlersWithLoad(MammoHxChangedHandler, MammoHxLoadedHandler, null);
            }

            if (activePatient.procedureHx != null)
            {
                this._breastBx = activePatient.procedureHx.breastBx;

                if (this._breastBx != null)
                {
                    this._breastBx.AddHandlersWithLoad(BreastBxChangedHander, BreastBxLoadedHandler, null);
                }
            }
        }

        #region ProcedureHx and BreastBx handlers
        private void BreastBxLoadedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControlsByControlName(this._breastBx);
            FillBreastBxControls();
            ToggleDateFields();
        }

        private void BreastBxChangedHander(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControlsByControlName(this._breastBx);
                FillBreastBxControls();
                ToggleDateFields();
            }

        }

        private void MammoHxLoadedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControlsByControlName(this._mammoHx);
            FillMammoControls();
            ToggleDateFields();
        }

        private void MammoHxChangedHandler(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControlsByControlName(this._mammoHx);
                FillMammoControls();
                ToggleDateFields();
            }
        }


        #endregion

        private void FillMDYDropdowns(ComboBox monthDropdown, ComboBox dayDropdown, ComboBox yearDropdown, string value)
        {
            if (!(String.IsNullOrEmpty(value)))
            {
                if (value.Length > 0)
                {
                    string[] parts = value.Split('/');
                    if (parts.Length >= 3)
                    {
                        monthDropdown.Text = parts[0];
                        dayDropdown.Text = parts[1];
                        yearDropdown.Text = parts[2];
                    }
                }

            }

        }


        private void InitializeDropDowns()
        {
            InitializeMDYComboBoxes(otherBreastSurgeryMonth, otherBreastSurgeryDay, otherBreastSurgeryYear);
            InitializeMDYComboBoxes(reconstructionMonth, reconstructionDay, reconstructionYear);
            InitializeMDYComboBoxes(reductionMonth, reductionDay, reductionYear);
            InitializeMDYComboBoxes(implantsMonth, implantsDay, implantsYear);
            InitializeMDYComboBoxes(radiationRightMonth, radiationRightDay, radiationRightYear);
            InitializeMDYComboBoxes(radiationLeftMonth, radiationLeftDay, radiationLeftYear);

            InitializeMDYComboBoxes(lumpectomyLeftMonth, lumpectomyLeftDay, lumpectomyLeftYear);
            InitializeMDYComboBoxes(lumpectomyRightMonth, lumpectomyRightDay, lumpectomyRightYear);

            InitializeMDYComboBoxes(mastectomyLeftMonth, mastectomyLeftDay, mastectomyLeftYear);
            InitializeMDYComboBoxes(mastectomyRightMonth, mastectomyRightDay, mastectomyRightYear);

            InitializeMDYComboBoxes(mastectomyMonth, mastectomyDay, mastectomyYear);
            InitializeMDYComboBoxes(lumpectomyMonth, lumpectomyDay, lumpectomyYear);


            InitializeMDYComboBoxes(biopsyRightMonth1, biopsyRightDay1, biopsyRightYear1);
            InitializeMDYComboBoxes(biopsyRightMonth2, biopsyRightDay2, biopsyRightYear2);
            InitializeMDYComboBoxes(biopsyLeftMonth1, biopsyLeftDay1, biopsyLeftYear1);
            InitializeMDYComboBoxes(biopsyLeftMonth2, biopsyLeftDay2, biopsyLeftYear2);
            InitializeMDYComboBoxes(priorMammogramMonth, priorMammogramDay, priorMammogramYear);
        }


        private void FillMammoControls()
        {
            FillMDYDropdowns(otherBreastSurgeryMonth, otherBreastSurgeryDay, otherBreastSurgeryYear, _mammoHx.OtherBreastSurgeryDate);
            FillMDYDropdowns(reconstructionMonth, reconstructionDay, reconstructionYear, _mammoHx.ReconstructionDate);
            FillMDYDropdowns(reductionMonth, reductionDay, reductionYear, _mammoHx.BreastReductionDate);
            FillMDYDropdowns(implantsMonth, implantsDay, implantsYear, _mammoHx.BreastImplantsDate);
            FillMDYDropdowns(radiationRightMonth, radiationRightDay, radiationRightYear, _mammoHx.RadiationRightDate1);
            FillMDYDropdowns(radiationLeftMonth, radiationLeftDay, radiationLeftYear, _mammoHx.RadiationLeftDate1);

            FillMDYDropdowns(lumpectomyLeftMonth, lumpectomyLeftDay, lumpectomyLeftYear, _mammoHx.LumpectomyLeftDate1);
            FillMDYDropdowns(lumpectomyRightMonth, lumpectomyRightDay, lumpectomyRightYear, _mammoHx.LumpectomyRightDate1);

            FillMDYDropdowns(mastectomyLeftMonth, mastectomyLeftDay, mastectomyLeftYear, _mammoHx.MastectomyLeftDate);
            FillMDYDropdowns(mastectomyRightMonth, mastectomyRightDay, mastectomyRightYear, _mammoHx.MastectomyRightDate);

            FillMDYDropdowns(mastectomyMonth, mastectomyDay, mastectomyYear, _mammoHx.MastectomyDate);
            FillMDYDropdowns(lumpectomyMonth, lumpectomyDay, lumpectomyYear, _mammoHx.LumpectomyDate);

            FillMDYDropdowns(priorMammogramMonth, priorMammogramDay, priorMammogramYear, _mammoHx.PriorMammogramYearsDate);


            mammogramRoutine.Text = _mammoHx.MammogramRoutine;
            visitReason_Lump.Checked = (_mammoHx.VisitReason_Lump != null && _mammoHx.VisitReason_Lump.Equals(Yes));
            visitReason_Discharge.Checked = (_mammoHx.VisitReason_Discharge != null && _mammoHx.VisitReason_Discharge.Equals(Yes));
            visitReason_Retraction.Checked = (_mammoHx.VisitReason_Retraction != null && _mammoHx.VisitReason_Retraction.Equals(Yes));
            visitReason_Thickening.Checked = (_mammoHx.VisitReason_Thickening != null && _mammoHx.VisitReason_Thickening.Equals(Yes));
            visitReason_Pain.Checked = (_mammoHx.VisitReason_Pain != null && _mammoHx.VisitReason_Pain.Equals(Yes));
            visitReason_Other.Checked = (_mammoHx.VisitReason_Other != null && _mammoHx.VisitReason_Other.Equals(Yes));
            visitReason_Other_Explain.Text = _mammoHx.VisitReason_Other_Explain;

        }


        private void FillBreastBxControls()
        {

            FillMDYDropdowns(biopsyRightMonth1, biopsyRightDay1, biopsyRightYear1, _breastBx.BreastBx_breastBiopsiesRightDate1);
            FillMDYDropdowns(biopsyRightMonth2, biopsyRightDay2, biopsyRightYear2, _breastBx.BreastBx_breastBiopsiesRightDate2);

            FillMDYDropdowns(biopsyLeftMonth1, biopsyLeftDay1, biopsyLeftYear1, _breastBx.BreastBx_breastBiopsiesLeftDate1);
            FillMDYDropdowns(biopsyLeftMonth2, biopsyLeftDay2, biopsyLeftYear2, _breastBx.BreastBx_breastBiopsiesLeftDate2);
        }
        private void FillControlsByControlName(HraObject hraObject)
        {
            var hraObjectGetters = hraObject.GetType()
                .GetMembers(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var property in hraObjectGetters)
            {
                // ReSharper disable StringLastIndexOfIsCultureSpecific.1   - Not Applicable
                int indexOfLastunderscore = property.Name.LastIndexOf("_");
                // ReSharper restore StringLastIndexOfIsCultureSpecific.1
                string propertyNameWithoutGetPrefix = property.Name.Remove(0, indexOfLastunderscore + 1);

                Control controlToUpdate = this.groupBox.Controls.Cast<Control>()
                    .SingleOrDefault(control => String.Equals(control.Name, propertyNameWithoutGetPrefix, StringComparison.CurrentCultureIgnoreCase));

                if (controlToUpdate != null)
                {
                    var memberWithName = hraObject.GetMemberByName(propertyNameWithoutGetPrefix);
                    FieldInfo propertyInfo = memberWithName as FieldInfo;
                    if (propertyInfo != null)
                    {
                        object hraObjectValue = propertyInfo.GetValue(hraObject);
                        if (hraObjectValue != null)
                        {
                            controlToUpdate.Text = hraObjectValue.ToString();
                        }
                    }
                }
            }

        }

        private static void UpdateModelFromControl(object sender, HraObject hraObject)
        {
            Control control = sender as Control;
            if (control != null)
            {
                string name = control.Name;
                PropertyInfo property = hraObject.GetPropertyWithNameLike(name);
                if (property != null)
                {
                    object text = control.Text;
                    property.SetValue(hraObject, text, null);
                }
            }
        }

        private void priorMammogramYearsDate_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void priorMammogramWhere_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void breastBiopsiesRightDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
        }

        private void breastBiopsiesRightDate2_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
        }

        private void breastBiopsiesLeftDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
        }

        private void breastBiopsiesLeftDate2_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
        }

        private void createdby_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void priorMammogramFilmsAtHosp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void filmsOutsideRequested_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void filmsOutsideReleaseSigned_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void breastBiopsiesRight1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
            this.UpdateBreastBiopsies();
            this.ToggleDateFields();
        }

        private void breastBiopsiesRight2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
            this.UpdateBreastBiopsies();
            this.ToggleDateFields();
        }

        private void breastBiopsiesLeft1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
            this.UpdateBreastBiopsies();
            this.ToggleDateFields();
        }

        private void breastBiopsiesLeft2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
            this.UpdateBreastBiopsies();
            this.ToggleDateFields();
        }

        private void hadBreastBiopsy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._breastBx);
            this.ToggleDateFields();
        }



        private void mammoHx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void mammoHx_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }





        private void ResetControlAndUpdateModel(Control control)
        {
            control.Text = string.Empty;
            control.Enabled = false;
        }


        private void ToggleDateFields()
        {
            breastBiopsiesLeft1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            breastBiopsiesRight1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            breastBiopsiesLeft2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            breastBiopsiesRight2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftMonth1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftDay1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftYear1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftMonth2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftDay2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyLeftYear2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightMonth1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightDay1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightYear1.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightMonth2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightDay2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));
            biopsyRightYear2.Enabled = (hadBreastBiopsy.Text.Equals(Yes));

            radiationLeft1.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationRight1.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationRightMonth.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationRightDay.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationRightYear.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationLeftMonth.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationLeftDay.Enabled = (radiationTherapy.Text.Equals(Yes));
            radiationLeftYear.Enabled = (radiationTherapy.Text.Equals(Yes));

            breastImplantsSide.Enabled = (breastImplants.Text.Equals(Yes));
            implantsMonth.Enabled = (breastImplants.Text.Equals(Yes));
            implantsDay.Enabled = (breastImplants.Text.Equals(Yes));
            implantsYear.Enabled = (breastImplants.Text.Equals(Yes));

            breastReductionSide.Enabled = (breastReduction.Text.Equals(Yes));
            reductionMonth.Enabled = (breastReduction.Text.Equals(Yes));
            reductionDay.Enabled = (breastReduction.Text.Equals(Yes));
            reductionYear.Enabled = (breastReduction.Text.Equals(Yes));

            lumpectomyMonth.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyDay.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyYear.Enabled = (lumpectomyYesNo.Text.Equals(Yes));

            lumpectomyLeft1.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyRight1.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyRightMonth.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyRightDay.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyRightYear.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyLeftMonth.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyLeftDay.Enabled = (lumpectomyYesNo.Text.Equals(Yes));
            lumpectomyLeftYear.Enabled = (lumpectomyYesNo.Text.Equals(Yes));

            mastectomyMonth.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyDay.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyYear.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyLeft.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyRight.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyRightMonth.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyRightDay.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyRightYear.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyLeftMonth.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyLeftDay.Enabled = (hadMastectomy.Text.Equals(Yes));
            mastectomyLeftYear.Enabled = (hadMastectomy.Text.Equals(Yes));

            reconstructionSide.Enabled = (reconstructiveSurgery.Text.Equals(Yes));
            reconstructionMonth.Enabled = (reconstructiveSurgery.Text.Equals(Yes));
            reconstructionDay.Enabled = (reconstructiveSurgery.Text.Equals(Yes));
            reconstructionYear.Enabled = (reconstructiveSurgery.Text.Equals(Yes));

            otherBreastSurgerySide.Enabled = (otherBreastSurgeryYesNo.Text.Equals(Yes));
            otherBreastSurgery.Enabled = (otherBreastSurgeryYesNo.Text.Equals(Yes));
            otherBreastSurgeryMonth.Enabled = (otherBreastSurgeryYesNo.Text.Equals(Yes));
            otherBreastSurgeryDay.Enabled = (otherBreastSurgeryYesNo.Text.Equals(Yes));
            otherBreastSurgeryYear.Enabled = (otherBreastSurgeryYesNo.Text.Equals(Yes));


            visitReason_Lump.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Discharge.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Retraction.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Thickening.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Pain.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Other.Enabled = (mammogramRoutine.Text.Equals(No));
            visitReason_Other_Explain.Enabled = (mammogramRoutine.Text.Equals(No)) && visitReason_Other.Checked;

            var disabledControls = this.groupBox.Controls.Cast<Control>().Where(control => control.Enabled == false);

            foreach (Control control in disabledControls)
            {
                control.Text = string.Empty;
                control.Enabled = false;
            }

          /*  if (mammogramRoutine.Text.Equals(Yes))
            {
                visitReason_Discharge.Checked = false;
                visitReason_Retraction.Checked = false;
                visitReason_Thickening.Checked = false;
                visitReason_Pain.Checked = false;
                visitReason_Other.Checked = false;
                visitReason_Other_Explain.Text = "";
            }*/
        }


        protected override IEnumerable<Control> GetControlsForLookups()
        {
            return this.groupBox.Controls.Cast<Control>().Where(control => control is ComboBox);
        }

        private void UpdateBreastBiopsies()
        {
            var biopsyFields = new[]
            {
                this.breastBiopsiesLeft1, this.breastBiopsiesLeft2, 
                this.breastBiopsiesRight1, this.breastBiopsiesRight2
            };

            int biopsyCount = biopsyFields.Count(control => control.Text == Yes);
            this._breastBx.BreastBx_breastBiopsies = biopsyCount.ToString();
        }

        private IEnumerable<Control> GetControlsByName(String pattern)
        {
            return this.groupBox.Controls.Cast<Control>()
                .Where(control => control.Name.Contains(pattern))
                .ToList();
        }

        private static Boolean ControlSetHasData(IEnumerable<Control> controls)
        {
            return controls.Any(ControlHasData);
        }

        private static Boolean ControlHasData(Control c)
        {
            String tempText = Regex.Replace(c.Text, ForwardSlashPattern, string.Empty);
            tempText = tempText.Trim();
            return !tempText.Equals(string.Empty);
        }

        private void MammographyHxView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._mammoHx != null)
                this._mammoHx.ReleaseListeners(this);

            if (this._breastBx != null)
                this._breastBx.ReleaseListeners(this);

            if (this._proband != null)
                this._proband.ReleaseListeners(this);

            //TODO could this be pushed up to HraView?
            PersistDisabledFields();
            SessionManager.Instance.RemoveHraView(this);
        }

        private void PersistDisabledFields()
        {
            var disabledControls = this.groupBox
                .Controls.Cast<Control>()
                .Where(control => control.Enabled == false || control.Text == string.Empty);

            foreach (Control control in disabledControls)
            {
                if (this._breastBx != null)
                    UpdateModelFromControl(control, this._breastBx);

                if (this._mammoHx != null)
                    UpdateModelFromControl(control, this._mammoHx);
            }
        }

        private string GetValueFromMDYDropdowns(ComboBox monthDropdown, ComboBox dayDropdown, ComboBox yearDropdown)
        {

            String answerString = "";

            if (monthDropdown.Text.Length > 0 || dayDropdown.Text.Length > 0 || yearDropdown.Text.Length > 0)
            {
                answerString = monthDropdown.Text + "/" + dayDropdown.Text + "/" + yearDropdown.Text;
            }

            return answerString;
        }

        private void otherBreastSurgeryDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.OtherBreastSurgeryDate = GetValueFromMDYDropdowns(otherBreastSurgeryMonth, otherBreastSurgeryDay, otherBreastSurgeryYear);
        }

        private void reconstructionDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.ReconstructionDate = GetValueFromMDYDropdowns(reconstructionMonth, reconstructionDay, reconstructionYear);
        }

        private void reductionDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.BreastReductionDate = GetValueFromMDYDropdowns(reductionMonth, reductionDay, reductionYear);
        }

        private void implantsDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.BreastImplantsDate = GetValueFromMDYDropdowns(implantsMonth, implantsDay, implantsYear);
        }

        private void radiationRightDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.RadiationRightDate1 = GetValueFromMDYDropdowns(radiationRightMonth, radiationRightDay, radiationRightYear);
        }

        private void radiationLeftDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.RadiationLeftDate1 = GetValueFromMDYDropdowns(radiationLeftMonth, radiationLeftDay, radiationLeftYear);
        }


        private void lumpectomyRightDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.LumpectomyRightDate1 = GetValueFromMDYDropdowns(lumpectomyRightMonth, lumpectomyRightDay, lumpectomyRightYear);
        }

        private void lumpectomyLeftDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.LumpectomyLeftDate1 = GetValueFromMDYDropdowns(lumpectomyLeftMonth, lumpectomyLeftDay, lumpectomyLeftYear);
        }

        private void mastectomyRightDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.MastectomyRightDate = GetValueFromMDYDropdowns(mastectomyRightMonth, mastectomyRightDay, mastectomyRightYear);
        }

        private void mastectomyLeftDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.MastectomyLeftDate = GetValueFromMDYDropdowns(mastectomyLeftMonth, mastectomyLeftDay, mastectomyLeftYear);
        }


        private void lumpectomyDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.LumpectomyDate = GetValueFromMDYDropdowns(lumpectomyMonth, lumpectomyDay, lumpectomyYear);
        }

        private void mastectomyDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.MastectomyDate = GetValueFromMDYDropdowns(mastectomyMonth, mastectomyDay, mastectomyYear);
        }

        private void biopsyRightDate1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _breastBx.BreastBx_breastBiopsiesRightDate1 = GetValueFromMDYDropdowns(biopsyRightMonth1, biopsyRightDay1, biopsyRightYear1);
        }


        private void biopsyRightDate2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _breastBx.BreastBx_breastBiopsiesRightDate2 = GetValueFromMDYDropdowns(biopsyRightMonth2, biopsyRightDay2, biopsyRightYear2);
        }

        private void biopsyLeftDate1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _breastBx.BreastBx_breastBiopsiesLeftDate1 = GetValueFromMDYDropdowns(biopsyLeftMonth1, biopsyLeftDay1, biopsyLeftYear1);
        }

        private void biopsyLeftDate2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _breastBx.BreastBx_breastBiopsiesLeftDate2 = GetValueFromMDYDropdowns(biopsyLeftMonth2, biopsyLeftDay2, biopsyLeftYear2);
        }

        private void priorMammogramYearsDate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _mammoHx.PriorMammogramYearsDate = GetValueFromMDYDropdowns(priorMammogramMonth, priorMammogramDay, priorMammogramYear);
        }

        private void mammogramRoutine_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            ToggleDateFields();
            if (_mammoHx.MammogramRoutine.Equals(Yes))
            {
                  _mammoHx.VisitReason_Lump = "No";
                  _mammoHx.VisitReason_Discharge = "No";
                  _mammoHx.VisitReason_Retraction = "No";
                  _mammoHx.VisitReason_Thickening = "No";
                  _mammoHx.VisitReason_Pain = "No";
                  _mammoHx.VisitReason_Other = "No";
                  _mammoHx.VisitReason_Other_Explain = "";
            }


        }

        private void visitReason_Lump_CheckStateChanged(object sender, EventArgs e)
        {
            if (visitReason_Lump.Checked)
            {
                _mammoHx.VisitReason_Lump = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Lump = "No";
            }
            ToggleDateFields();
        }

        private void visitReason_Discharge_CheckStateChanged(object sender, EventArgs e)
        {
            if (visitReason_Discharge.Checked)
            {
                _mammoHx.VisitReason_Discharge = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Discharge = "No";
            }
            ToggleDateFields();
        }

        private void visitReason_Retraction_CheckedChanged(object sender, EventArgs e)
        {
            if (visitReason_Retraction.Checked)
            {
                _mammoHx.VisitReason_Retraction = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Retraction = "No";
            }
            ToggleDateFields();
        }

        private void visitReason_Thickening_CheckedChanged(object sender, EventArgs e)
        {
            if (visitReason_Thickening.Checked)
            {
                _mammoHx.VisitReason_Thickening = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Thickening = "No";
            }
            ToggleDateFields();
        }

        private void visitReason_Pain_CheckedChanged(object sender, EventArgs e)
        {
            if (visitReason_Pain.Checked)
            {
                _mammoHx.VisitReason_Pain = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Pain = "No";
            }
            ToggleDateFields();
        }

        private void visitReason_Other_CheckedChanged(object sender, EventArgs e)
        {
            if (visitReason_Other.Checked)
            {
                _mammoHx.VisitReason_Other = "Yes";
            }
            else
            {
                _mammoHx.VisitReason_Other = "No";
                _mammoHx.VisitReason_Other_Explain = "";
            }
            ToggleDateFields();
        }

        private void visitReason_Other_Explain_Validated(object sender, EventArgs e)
        {
            _mammoHx.VisitReason_Other_Explain = visitReason_Other_Explain.Text;
        }


    }
}

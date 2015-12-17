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
        private const string ForwardSlashPattern = "/";
        private const string WarningSelectingNoWillClearAnySurgeryTreatmentDataYouMayHaveEnteredAreYouSure = "Warning: Selecting No will clear any surgery/treatment data you may have entered.  Are you sure?";
        private const string ConfirmSelection = "Confirm Selection";

        private readonly List<Control> _priorSurgeriesAndTreatmentsControls;

        private Patient _proband;
        private MammographyHx _mammoHx;
        private BreastBx _breastBx;

        public MammographyHxView() : base (TblRiskDataExt1)
        {
            InitializeComponent();

            UserList users = new UserList();
            users.LoadFullList();

            List<string> userList = users.Select(user => string.Format("{0}, {1}", ((User)user).userLastName, ((User)user).userFirstName)).ToList();

            UIUtils.fillComboBoxFromList(this.createdby, userList, true);
            
            this._priorSurgeriesAndTreatmentsControls = new List<Control>();
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("breastBiopsies"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("lumpectomy"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("radiation"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("mastectomy"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("Reduction"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("Implant"));
            _priorSurgeriesAndTreatmentsControls.AddRange(this.GetControlsByName("other"));
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
            this.UpdateGuiForType(this._breastBx);
        }

        private void BreastBxChangedHander(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                this.UpdateGuiForType(this._breastBx);
            }
            ToggleWidgetsOnOff();
        }

        private void MammoHxLoadedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UpdateGuiForType(this._mammoHx);
        }

        private void MammoHxChangedHandler(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                this.UpdateGuiForType(this._mammoHx);
            }
            ToggleWidgetsOnOff();
        }

        #endregion

        private void UpdateGuiForType(HraObject hraObject)
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

            ToggleWidgetsOnOff();
            ToggleDateFields();

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

        private void mastectomyRightDate_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void mastectomyLeftDate_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void lumpectomyRightDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void lumpectomyLeftDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void radiationRightDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void radiationLeftDate1_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void breastImplantsDate_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
        }

        private void breastReductionDate_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
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

        private void mastectomyRight_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void mastectomyLeft_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void lumpectomyRight1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void lumpectomyLeft1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void radiationRight1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void radiationLeft1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void breastImplants_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void breastReduction_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void breastImplantsSide_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void breastReductionSide_SelectionChangeCommitted(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void otherBreastSurgery_Validated(object sender, EventArgs e)
        {
            UpdateModelFromControl(sender, this._mammoHx);
            this.ToggleDateFields();
        }

        private void surgeryTreatmentNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.surgeryTreatmentNo.Checked)
            {
                if (ControlSetHasData(this._priorSurgeriesAndTreatmentsControls))
                {
                    //popup warning and clear entries
                    if (
                        MessageBox.Show(
                            WarningSelectingNoWillClearAnySurgeryTreatmentDataYouMayHaveEnteredAreYouSure,
                            ConfirmSelection,
                            MessageBoxButtons.YesNo) == DialogResult.Yes
                        )
                    {
                        //if any of the yes/no boxes are yes, set the radio button to yes
                        ClearAllSurgeriesAndTreatments();
                        
                    }
                    else
                    {
                        SetAnySurgeryOrTreatmentsToYes();
                    }
                }
            }
        }

        private void ClearAllSurgeriesAndTreatments()
        {
            foreach (Control control in this._priorSurgeriesAndTreatmentsControls)
            {
                if (control is ComboBox)
                {
                    ComboBox cb = (ComboBox) control;
                    cb.SelectedIndex = 0;
                    cb.Enabled = false;
                }
                else
                {
                    ResetControlAndUpdateModel(control);
                }
            }
        }

        private void ResetControlAndUpdateModel(Control control)
        {
            control.Text = string.Empty;
            control.Enabled = false;
        }

        private void SetAnySurgeryOrTreatmentsToYes()
        {
            this.surgeryTreatmentNo.Checked = false;
            this.surgeryTreatmentYes.Checked = true;
        }

        private void surgeryTreatmentYes_CheckedChanged(object sender, EventArgs e)
        {
            if (this.surgeryTreatmentYes.Checked)
            {
                EnableSurgeriesAndTreatmentsDropdowns();

                ToggleDateFields();
            }
            else
            {
                DisableSurgeriesAndTreatmentsAll();
            }
        }

        private void DisableSurgeriesAndTreatmentsAll()
        {
            this._priorSurgeriesAndTreatmentsControls.ForEach(control => control.Enabled = false);
        }

        private void EnableSurgeriesAndTreatmentsDropdowns()
        {
            this._priorSurgeriesAndTreatmentsControls
                .Where(control => control is ComboBox || control.Name == "otherBreastSurgery")
                .ToList()
                .ForEach(control => control.Enabled = true);
        }

        private void ToggleDateFields()
        {
            breastBiopsiesLeftDate1.Enabled = (breastBiopsiesLeft1.Text.Equals(Yes));
            breastBiopsiesLeftDate2.Enabled = (breastBiopsiesLeft2.Text.Equals(Yes));
            breastBiopsiesRightDate1.Enabled = (breastBiopsiesRight1.Text.Equals(Yes));
            breastBiopsiesRightDate2.Enabled = (breastBiopsiesRight2.Text.Equals(Yes));
            breastImplantsSide.Enabled = (breastImplants.Text.Equals(Yes));
            breastImplantsDate.Enabled = (breastImplants.Text.Equals(Yes));
            breastReductionSide.Enabled = (breastReduction.Text.Equals(Yes));
            breastReductionDate.Enabled = (breastReduction.Text.Equals(Yes));
            lumpectomyLeftDate1.Enabled = (lumpectomyLeft1.Text.Equals(Yes));
            lumpectomyRightDate1.Enabled = (lumpectomyRight1.Text.Equals(Yes));
            radiationLeftDate1.Enabled = (radiationLeft1.Text.Equals(Yes));
            radiationRightDate1.Enabled = (radiationRight1.Text.Equals(Yes));
            mastectomyLeftDate.Enabled = (mastectomyLeft.Text.Equals(Yes));
            mastectomyRightDate.Enabled = (mastectomyRight.Text.Equals(Yes));

            var disabledControls = this.groupBox.Controls.Cast<Control>().Where(control => control.Enabled == false);

            foreach (Control control in disabledControls)
            {
                ResetControlAndUpdateModel(control);
            }
        }

        /// <summary>
        /// If any of the yes/no boxes are yes, set the radio button to yes
        /// </summary>
        /// <remarks>to be called once model objects are loaded</remarks>
        private void ToggleWidgetsOnOff()
        {
            foreach (Control control in this._priorSurgeriesAndTreatmentsControls.Where(control => control.Name!= "otherBreastSurgery"))
            {
                if (control is ComboBox)
                {
                    ComboBox cb = (ComboBox)control;
                    if ((String)cb.SelectedItem == Yes)
                    {
                        surgeryTreatmentYes.Select();
                        break;
                    }
                }
            }

            surgeryTreatmentNo.Refresh();
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
            this._mammoHx.ReleaseListeners(this);
            this._breastBx.ReleaseListeners(this);
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
                UpdateModelFromControl(control, this._breastBx);
                UpdateModelFromControl(control, this._mammoHx);
            }
        }
    }
}

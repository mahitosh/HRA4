using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.Admin;
using RiskApps3.View.Common;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.View.RiskClinic;

namespace RiskApps3.View.Appointments
{
    public partial class AddAppointmentView : HraView
    {
        private const string OnlyEditOrCopyIsApplicableForThisConstructor = "Only Edit or Copy is applicable for this constructor.";
        private const string RiskappsAddAppointment = "riskApps™ Add Appointment";
        private const string RiskappsCopyAppointment = "riskApps™ Copy Appointment";
        private const string RiskappsEditAppointment = "riskApps™ Edit Appointment";
        private const int OneHundredTwentyYearsInDays = (int)(365.25*120);
        private const string NoProviderSelected = "No provider selected.";
        private const string ClinicIsRequired = "Clinic is required.";
        private const string SurveyTypeIsRequired = "Survey Type is required.";
        private const string EightOClockAm = "08:00 AM";
        private const string AppointmentDateIsRequired = "Appointment Date is required.";
        private const string DateOfBirthIsNotFormattedCorrectly = "Date of Birth is not formatted correctly.";
        private const string UnitNumberIsRequired = "Unit Number is required.";
        private const string DateOfBirthIsRequired = "Date of Birth is required.";
        private const string PatientNameIsRequired = "Patient name is required.";

        public enum Mode
        {
            Add,
            Edit,
            Copy
        }

        private bool _hasChanged;
        
        private readonly Mode _mode;

        private readonly Appointment _appointment;

        private readonly Patient _patient;

        private readonly int? _clinicId;
        private readonly ClinicList _clinics = SessionManager.Instance.ActiveUser.UserClinicList;
        private readonly AllProviders _allProviders = SessionManager.Instance.MetaData.AllProviders;

        /// <summary>
        /// Base functionality for all instances of this form.  Always call this constructor a la:
        /// <code>public AddAppointmentView(...my crazy args here...) : this () { ... }</code>
        /// </summary>
        private AddAppointmentView()
            : base("tblAppointments", "tblPatients")
        {
            InitializeComponent();

            _allProviders.AddHandlersWithLoad(AllProvidersListChanged, AllProvidersLoaded, AllProvidersItemChanged);
        }

        private void AllProvidersLoaded(HraListLoadedEventArgs e)
        {
            SetProviderSelectionItems();
        }

        private void SetProviderSelectionItems()
        {
            var providers = this._allProviders
                .Where(p => p.HasPrintableName)
                .Select(p => new AutoCompleteEntry(p.PrintableName, p.providerID,  p.PrintableName)).ToArray();

            this.pcpTextBox.Items.Clear();
            this.pcpTextBox.Items
                .AddRange(providers);

            this.patientProvidedRefPhys.Items.Clear();
            this.patientProvidedRefPhys.Items
                .AddRange(providers);
        }

        private void AllProvidersItemChanged(object sender, HraModelChangedEventArgs e)
        {
            this.SetProviderSelectionItems();
        }

        private void AllProvidersListChanged(HraListChangedEventArgs e)
        {
            if (this._allProviders.IsLoaded)
            {
                this.SetProviderSelectionItems();
            }
        }

        /// <summary>
        /// User for new appointment.
        /// </summary>
        /// <param name="mrn"></param>
        /// <param name="clinicId">clinic to default to when opening wizard</param>
        public AddAppointmentView(string mrn, int? clinicId) : this()
        {
            this._mode = Mode.Add;

            InitializeModeConditions();

            this._patient = new Patient(mrn);
            this._patient.Providers.LoadFullList();

            SessionManager.Instance.SetActivePatient(this._patient.unitnum, this._patient.apptid);

            this._appointment = new Appointment (clinicId, mrn) { ClinicList = this._clinics};

            this._clinicId = clinicId;

            MarkStartedAndPullForwardForm mark = new MarkStartedAndPullForwardForm(this._appointment.apptID, this._appointment.unitnum);
            mark.ShowDialog();
        }

        /// <summary>
        /// Use for existing appointment.
        /// </summary>
        /// <param name="currentAppointment"></param>
        /// <param name="clinicId">clinic to default to when opening wizard</param>
        /// <param name="editOrCopy">Are we editing or copying the selected <code>Appointment</code></param>
        public AddAppointmentView(Appointment currentAppointment, int? clinicId, Mode editOrCopy) : this()
        {
            if (editOrCopy == Mode.Add)
            {
                throw new InvalidEnumArgumentException(OnlyEditOrCopyIsApplicableForThisConstructor);
            }
            else
            {
                this._mode = editOrCopy;
            }

            InitializeModeConditions();
            
            this._patient = SessionManager.Instance.GetActivePatient();
            this._patient.Providers.LoadFullList();

            this._appointment = currentAppointment;
            this._appointment.ClinicList = this._clinics;

            this._clinicId = clinicId;
        }

        private void SetLoadedValuesInForm()
        {
            if (this._clinicId.HasValue)
            {
                this.clinic.SelectedItem = this._clinics.Single(c => c.clinicID == _clinicId);
            }

            //TODO this.FillControls is broken but i'm not sure why - figure out and fix???
            //this.FillControls();  //could skip all or most of the below boilerplate crap if we fix this

            this.patientNameTextBox.Text = BuildName();
            this.unitnumTextBox.Text = this._patient.Patient_unitnum;
            this.dobMaskedTextBox.Text = this._appointment.Dob;
            this.gender.Text = this._appointment.Gender;
            this.apptdateDatePicker.Text = this._appointment.ApptDate;
            this.appttime.Text = this._appointment.ApptTime;
            this.surveyType.Text = this._appointment.SurveyType;
            this.ra.Text = this._appointment.AssessmentName;
            this.apptIDTextBox.Text = this._appointment.apptID.ToString();
            this.language.Text = this._appointment.Language;
            this.race.Text = this._appointment.Race;
            this.nationality.Text = this._appointment.Nationality;
            this.occupation.Text = this._patient.Person_occupation;
            this.educationLevel.Text = this._patient.Person_educationLevel;
            this.maritalstatus.Text = this._patient.Person_maritalstatus;
            this.address1TextBox.Text = this._patient.Person_address1;
            this.address2TextBox.Text = this._patient.Person_address2;
            this.cityTextBox.Text = this._patient.Person_city;
            this.state.Text = this._patient.Person_state;
            this.zipTextBox.Text = this._patient.Person_zip;
            this.country.Text = this._patient.Person_country;
            this.homePhoneTextBox.Text = this._patient.Person_homephone;
            this.workPhoneTextBox.Text = this._patient.Person_workphone;
            this.cellPhoneTextBox.Text = this._patient.Person_cellphone;
            this.emailAddressTextBox.Text = this._patient.Person_emailAddress;
            this.apptphysname.Text = this._appointment.Apptphysname;
            FillProviderControls();
            this.comments1TextBox.Text = this._patient.Patient_Comment; //maps to family comment as in v2 view

            this._hasChanged = false;
        }

        private void FillProviderControls()
        {
            Provider rp = this._patient.Providers.FirstOrDefault(provider => provider.refPhys);
            if (rp != null)
            {
                this.patientProvidedRefPhys.Text = rp.PrintableName;
                this.patientProvidedRefPhys.Value = rp.providerID;
            }
            Provider pcp = this._patient.Providers.FirstOrDefault(provider => provider.PCP);
            if (pcp != null)
            {
                this.pcpTextBox.Text = pcp.PrintableName;
                this.pcpTextBox.Value = pcp.providerID;
            }
        }

        private string BuildName()
        {
            if (!string.IsNullOrEmpty(this._patient.Person_firstName) &&
                !string.IsNullOrEmpty(this._patient.Person_lastName))
            {
                string name = string.Format(
                    "{0}, {1} {2}",
                    this._patient.Person_lastName,
                    this._patient.Person_firstName,
                    this._patient.Person_middleName);

                return PruneWhiteSpace(name);
            }
            else
            {
                return PruneWhiteSpace(this._appointment.PatientName);
            }
        }

        protected override IEnumerable<Control> GetControlsForFill()
        {
            return this.panel1.Controls.Cast<Control>().ToList();
        }

        protected override IEnumerable<HraObject> GetData()
        {
            return new List<HraObject> {this._appointment, this._patient};
        }

        protected override void PostLoadHook()
        {
            this.clinic.Items.Clear();
            this.clinic.Items.Add("");
            this.clinic.Items.AddRange(this._clinics.Cast<object>().ToArray());

            this.ra.Items.AddRange(this._lookups["clinic"].Cast<object>().ToArray());

            SetLoadedValuesInForm();

            ConfigureMrnControls();
        }

        private void ConfigureMrnControls()
        {
            if (unitnumTextBox.Text.Length == 0)
            {
                unitnumTextBox.ReadOnly = false;
                changeMRNButton.Visible = false;
                lookupLegacy.Visible = false;
            }
            else
            {
                unitnumTextBox.ReadOnly = true;
                changeMRNButton.Visible = true;
                //lookupLegacy.Visible = true;  //TODO Determine if we still want this feature?
            }
        }

        private void InitializeModeConditions()
        {
            switch (this._mode)
            {
                case Mode.Add:
                    this.Text = RiskappsAddAppointment;
                    break;
                case Mode.Copy:
                    this.Text = RiskappsCopyAppointment;
                    this.apptdateDatePicker.Text = string.Empty;
                    this.appttime.Text = string.Empty;
                    this.apptIDTextBox.Text = string.Empty;
                    break;
                case Mode.Edit:
                    this.Text = RiskappsEditAppointment;
                    break;
                default:
                    this.Text = RiskappsEditAppointment;
                    break;
            }
        }

        protected override IEnumerable<Control> GetControlsForLookups()
        {
            return this.panel1.Controls.OfType<ComboBox>().Cast<Control>().ToList();
        }

        private void Save()
        {
            this.GuessAndSaveNameParts(this.patientNameTextBox.Text);
            this._appointment.Unitnum = this.unitnumTextBox.Text;
            this._appointment.Dob = this.dobMaskedTextBox.Text;
            this._patient.Person_dob = this.dobMaskedTextBox.Text;
            this._appointment.Gender = this.gender.Text;
            this._patient.Person_gender = this.gender.Text;
            this._appointment.ApptDate = this.apptdateDatePicker.Text;
            this._appointment.ApptTime = this.appttime.Text;
            this._appointment.SurveyType = this.surveyType.Text;
            this._appointment.Clinic = (Clinic)clinic.SelectedItem;
            this._appointment.AssessmentName = this.ra.Text;
            this._appointment.Language = this.language.Text;
            this._appointment.Race = this.race.Text;
            this._appointment.Nationality = this.nationality.Text;
            this._patient.Person_occupation = this.occupation.Text;
            this._patient.Person_educationLevel = this.educationLevel.Text;
            this._patient.Person_maritalstatus = this.maritalstatus.Text;
            this._patient.Person_address1 = this.address1TextBox.Text;
            this._patient.Person_address2 = this.address2TextBox.Text;
            this._patient.Person_city = this.cityTextBox.Text;
            this._patient.Person_state = this.state.Text;
            this._patient.Person_zip = this.zipTextBox.Text;
            this._patient.Person_country = this.country.Text;
            this._patient.Person_homephone = this.homePhoneTextBox.Text;
            this._patient.Person_workphone = this.workPhoneTextBox.Text;
            this._patient.Person_cellphone = this.cellPhoneTextBox.Text;
            this._patient.Person_emailAddress = this.emailAddressTextBox.Text;
            this._appointment.Apptphysname = this.apptphysname.Text;

            this.SaveProvider(this.pcpTextBox, provider => provider.PCP, true, false);
            this.SaveProvider(this.patientProvidedRefPhys, provider => provider.refPhys, false, true);

            this._patient.Patient_Comment = this.comments1TextBox.Text;
        }

        private void GuessAndSaveNameParts(string patientName)
        {
            string first;
            string middle;
            string last;
            GuessNameParts(out first, patientName, out middle, out last);

            this._patient.Person_firstName = first;
            this._patient.Person_middleName= middle;
            this._patient.Person_lastName = last;
            string name = string.Format(
                "{0}, {1} {2}", 
                last, 
                first, 
                middle);
            this._appointment.PatientName = PruneWhiteSpace(name);
        }

        private static string PruneWhiteSpace(string toPrune)
        {
            if (string.IsNullOrEmpty(toPrune))
            {
                return string.Empty;
            }
            else
            {
                return Regex.Replace(toPrune, @"[\s]+", " ", RegexOptions.Compiled);
            }
            
        }

        private static void GuessNameParts(out string first, string patientName, out string middle, out string last)
        {
            string[] nameParts = Regex.Split(patientName, @"(?<=[\b,.;])");
            int count = nameParts.Length;

            string flrst = nameParts[0];
            string second = string.Empty;
            string third = string.Empty;

            if (nameParts.Length >= 2)
            {
                second = nameParts[1];
            }
            if (nameParts.Length >= 3)
            {
                third = nameParts[2];
            }

            switch (count)
            {
                case 1:
                    ParseNamePartsFrom1Token(out first, nameParts, out middle, out last);
                    break;

                case 2:
                    ParseNamePartsFrom2Tokens(out first, flrst, second, out last, out middle);
                    break;

                case 3:
                    ParseNamePartsFrom3Tokens(out first, flrst, second, third, out last, out middle);
                    break;
                default:
                    ParseNamePartsFromXTokens(out first, nameParts, out middle, out last);
                    break;
            }
        }

        private static void ParseNamePartsFrom1Token(out string first, string[] nameParts, out string middle, out string last)
        {
            first = string.Empty;
            middle = string.Empty;
            last = nameParts[0];
        }

        private static void ParseNamePartsFrom2Tokens(out string first, string flrst, string second, out string last,
            out string middle)
        {
            if (flrst.EndsWith(","))
            {
                last= flrst.Replace(",", string.Empty);
                first = second;
                middle = string.Empty;
            }
            else
            {
                first = second;
                last = flrst;
                middle = string.Empty;
            }
        }

        private static void ParseNamePartsFrom3Tokens(out string first, string flrst, string second, string third,
            out string last, out string middle)
        {
            if (flrst.EndsWith(","))
            {
                first = flrst.Replace(",", string.Empty);
                last = second;
                middle = third;
            }
            else
            {
                first = flrst;
                middle = second;
                last = third;
            }
        }

        private static void ParseNamePartsFromXTokens(out string first, string[] nameParts, out string middle, out string last)
        {
            List<string> partsList = new List<string>(nameParts);

            string lastNamePart = partsList.FirstOrDefault(part => part.Contains(","));

            if (lastNamePart != null)
            {
                //last name probably is everything preceding the comma
                int locationOfLastNamePart = partsList.IndexOf(lastNamePart);
                StringBuilder lastName = new StringBuilder();
                for (int i = 0; i < locationOfLastNamePart; i++)
                {
                    lastName.AppendFormat("{0} ", partsList[i]);
                }
                lastName.Replace(", ", string.Empty);

                if (nameParts.Length >= locationOfLastNamePart)
                {
                    first = nameParts[locationOfLastNamePart];
                }
                else
                {
                    first = string.Empty;
                }
                if (nameParts.Length >= locationOfLastNamePart + 1)
                {
                    middle = nameParts[locationOfLastNamePart + 1];
                }
                else
                {
                    middle = string.Empty;
                }
                last = lastName.ToString();
            }
            else
            {
                first = nameParts[0];
                middle = nameParts[1];

                StringBuilder lastName = new StringBuilder();
                for (int i = 2; i < nameParts.Length; i++)
                {
                    lastName.AppendFormat("{0} ", partsList[i]);
                }
                lastName.Replace(", ", string.Empty);
                last = lastName.ToString();
            }
        }
        
        private void patientNameTextBox_Validated(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.patientNameTextBox, PatientNameIsRequired);
        }

        private void unitnumTextBox_Validated(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.unitnumTextBox, UnitNumberIsRequired);
        }

        private void dobMaskedTextBox_Validated(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.dobMaskedTextBox, DateOfBirthIsRequired);
            if (IsDobFormattedCorrectly())
            {
                this.MarkValid(this.dobMaskedTextBox);
            }
            else
            {
                this.MarkInvalid(this.dobMaskedTextBox, DateOfBirthIsNotFormattedCorrectly);
            }
        }

        private bool IsDobFormattedCorrectly()
        {
            DateTime dob;
            if (DateTime.TryParse(this.dobMaskedTextBox.Text, out dob) == false)
            {
                return false;
            }
            else
            {
                DateTime today = DateTime.Today;
                TimeSpan span = today.Subtract(dob);
                if (span.Days > OneHundredTwentyYearsInDays)
                {
                    return false;
                }
            }
            return true;
        }

        private void apptdateDatePicker_Validated(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.apptdateDatePicker, AppointmentDateIsRequired);
        }

        private void appttimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.appttime.Text))
            {
                this.appttime.Text = EightOClockAm;
            }
        }

        private void surveyTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.surveyType, SurveyTypeIsRequired);
        }

        private void clinicComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateControlForEmptyString(this.clinic, ClinicIsRequired);
        }

        private void SaveProvider(CoolTextBox providerEntryControl, Func<Provider, bool> providerTypeSelectorExpression, bool pcp, bool refPhys)
        {
            //always do this
            ProviderList providersForThisAppt = this._patient.Providers;
            providersForThisAppt.RemoveAll(providerTypeSelectorExpression);

            //break out if they set it empty
            string providerName = providerEntryControl.Text;
            int? providerId = (int?)providerEntryControl.Value;
            
            if (string.IsNullOrEmpty(providerName))
            {
                return;
            }

            Provider selectedProvider;
            if (providerId.HasValue)
            {
                selectedProvider = _allProviders.Single(provider => provider.providerID == providerId);
            }
            else
            {
                //if it's a new provider, instantiate it
                selectedProvider = new Provider
                {
                    displayName = providerName
                };

                this._allProviders.AddToList(selectedProvider, new HraModelChangedEventArgs(null));
            }

            selectedProvider.PCP = pcp;
            selectedProvider.refPhys = refPhys;

            //add to patient record
            this._patient.Providers.Add(selectedProvider);
            this._patient.Providers.PersistFullList(new HraModelChangedEventArgs(this));
        }
        
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (this._hasChanged)
            {
                if (UserConfirmedCloseWithoutSave)
                {
                    CloseWithoutSave();
                }
            }
            else
            {
                CloseWithoutSave();
            }
        }

        private void CloseWithoutSave()
        {
            if (this._mode == Mode.Add || this._mode==Mode.Copy)
            {
                Appointment.DeleteApptData(this._appointment.apptID, true);
            }
            this.Close();
        }

        private void saveAndClose_Click(object sender, EventArgs e)
        {
            if (FormIsValid())
            {
                this._appointment.CreateAppointmentRecordsIfNeeded();
                this._patient.AddStockRelatives();

                this.Save();

                this.Close();
            }
            else
            {
                ShowValidationErrors();
            }
        }

        private void moreComments_Click(object sender, EventArgs e)
        {
            EditComments editComments = new EditComments(this._patient);
            editComments.ShowDialog();
        }

        private void editRpButton_Click(object sender, EventArgs e)
        {
            string providerName = this.patientProvidedRefPhys.Text;
            int? id = (int?)this.patientProvidedRefPhys.Value;

            Provider toEdit;
            if (EditProvider(id, providerName, out toEdit))
            {
                this.patientProvidedRefPhys.Value = toEdit.providerID;
                this.patientProvidedRefPhys.Text = toEdit.PrintableName;
            }
        }

        private void editPcpButton_Click(object sender, EventArgs e)
        {
            string providerName = this.pcpTextBox.Text;
            int? id = (int?)this.pcpTextBox.Value;

            Provider toEdit;
            if (EditProvider(id, providerName, out toEdit))
            {
                this.pcpTextBox.Value = toEdit.providerID;
                this.pcpTextBox.Text = toEdit.PrintableName;
            }
        }

        private bool EditProvider(int? id, string providerName, out Provider modified)
        {
            modified = null;
            if (string.IsNullOrEmpty(providerName))
            {
                MessageBox.Show(NoProviderSelected);
                return false;
            }
            else
            {
                
                if (id.HasValue)
                {
                    modified = _allProviders
                        .SingleOrDefault(p => p.providerID == id);
                }
                else
                {
                    modified = new Provider(providerName);
                }
                
                EditProviderForm editProviderForm = new EditProviderForm(modified, this._allProviders);

                DialogResult result = editProviderForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    return true;
                } else return false;
            }
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            this._hasChanged = true;
        }

        private void changeMRNButton_Click(object sender, EventArgs e)
        {
            using (var form = new ChangeMrnPopup(this._appointment))
            {
                form.ShowDialog(this);
            }

            this.unitnumTextBox.Text = this._appointment.unitnum;
        }

        private void AddAppointmentView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._allProviders.ReleaseListeners(this);
        }
    }
}

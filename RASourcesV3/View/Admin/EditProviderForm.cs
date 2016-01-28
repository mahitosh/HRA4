using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.MetaData;
using RiskApps3.Utilities;

namespace RiskApps3.View.Admin
{
    public partial class EditProviderForm : HraView
    {
        private bool _hasChanged;
        
        private readonly Provider _provider;

        private readonly AllProviders _allAllProviders;

        private readonly Mode _mode;

        private bool _international;

        private const string UnitedStates = "United States";
        private const string ZipIsRequired = "Zip is required.";
        private const string StateIsRequired = "State is required.";
        private const string CityIsRequired = "City is required.";
        private const string AddressIsRequired = "Address is required.";
        private const string LastNameIsRequired = "Last Name is required.";
        private const string FirstNameIsRequired = "First Name is required.";
        private const string SelectFile = "Select File";
        private const string DefaultExt = ".jpg";
        private const string ImagesJpgJpg = "Images (*.jpg)|*.jpg";

        private enum Mode
        {
            Add,
            Edit
        }

        private EditProviderForm() 
            : base ("lkpProviders", "tblApptProviders", "tblAppointments")
        {
            InitializeComponent();
            this._international = true;
        }

        protected override IEnumerable<Control> GetControlsForLookups()
        {
            return this.Controls.OfType<ComboBox>().Cast<Control>().ToList();
        }

        public EditProviderForm(Provider provider, AllProviders allProviders) : this()
        {
            this._provider = provider;
            this._allAllProviders = allProviders;
            if (this._provider.providerID <= 0)
            {
                this._mode = Mode.Add;
            }
            else
            {
                this._mode = Mode.Edit;
            }
        }

        protected override void PostLoadHook()
        {
            FillControls();
        }

        private new void FillControls()
        {
            this.title.Text = this._provider.title;
            this.firstNameTextBox.Text = this._provider.firstName;
            this.middleNameTextBox.Text = this._provider.middleName;
            this.lastNameTextBox.Text = this._provider.lastName;
            this.degree.Text = this._provider.degree;
            this.institutionTextBox.Text = this._provider.institution;
            this.address1TextBox.Text = this._provider.address1;
            this.address2TextBox.Text = this._provider.address2;
            this.cityTextBox.Text = this._provider.city;
            this.state.Text = this._provider.state;
            this.zipTextBox.Text = this._provider.zipcode;
            this.phoneTextBox.Text = this._provider.phone;
            this.faxTextBox.Text = this._provider.fax;
            this.emailTextBox.Text = this._provider.email;
            this.role.Text = this._provider.defaultRole;
            this.country.Text = this._provider.country;
            this.nationalProviderIDTextBox.Text = this._provider.nationalProviderID;
            this.UPINTextBox.Text = this._provider.UPIN;
            this.localProviderIDTextBox.Text = this._provider.localProviderID;
            this.displayNameTextBox.Text = this._provider.displayName;
            this.DocumentUploadPath.Text = this._provider.uploadPath;
            this.providerPhoto.ImageLocation = this._provider.photoPath;
            this.NidTextBox.Text = this._provider.networkID;
            this.dataSourceTextBox.Text = this._provider.dataSource;

            this._hasChanged = false;
        }

        private void setPhotoButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = SelectFile,
                DefaultExt = DefaultExt,
                Filter = ImagesJpgJpg,
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (!string.IsNullOrEmpty(this._provider.providerPhotosPath))
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        FileInfo photo = new FileInfo(ofd.FileName);

                        DirectoryInfo photoDirInfo = new DirectoryInfo(this._provider.providerPhotosPath);
                        if (!photoDirInfo.Exists)
                        {
                            photoDirInfo.Create();
                        }

                        FileInfo copiedPhotoInfo = photo.CopyTo(Path.Combine(photoDirInfo.FullName, this._provider.providerID + DefaultExt), true);

                        if (copiedPhotoInfo.FullName != this._provider.photoPath)
                        {
                            this._provider.photoPath = copiedPhotoInfo.FullName;
                            providerPhoto.ImageLocation = this._provider.photoPath;

                            this._hasChanged = true;
                        }
                    }
                    catch (Exception ee)
                    {
                        Logger.Instance.WriteToLog(ee.ToString());
                    }
                }
            }
            else
            {
                Logger.Instance.WriteToLog("No Provider Photo Directory set in BCDB2");
            }
        }

        private void control_ValueChanged(object sender, EventArgs e)
        {
            this._hasChanged = true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (this._hasChanged)
            {
                if (UserConfirmedCloseWithoutSave)
                {
                    CloseWithoutSave();
                }
                else
                {
                    return;
                }
            }
            else
            {
                CloseWithoutSave();
            }

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void CloseWithoutSave()
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (FormIsValid())
            {
                this.UpdateProviderModel();

                this.PersistProvider();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ShowValidationErrors();
            }
        }

        private void PersistProvider()
        {
            this._provider.BackgroundPersistWork(new HraModelChangedEventArgs(this));

            if (this._mode == Mode.Add)
            {
                this._allAllProviders.AddToList(this._provider, new HraModelChangedEventArgs(this));
            }
            else
            {
                this._provider.SignalModelChanged(new HraModelChangedEventArgs(this));
            }
        }

        private void UpdateProviderModel()
        {
            this._provider.title = this.title.Text;
            this._provider.firstName = this.firstNameTextBox.Text;
            this._provider.middleName = this.middleNameTextBox.Text;
            this._provider.lastName = this.lastNameTextBox.Text;
            this._provider.degree = this.degree.Text;
            this._provider.institution = this.institutionTextBox.Text;
            this._provider.address1 = this.address1TextBox.Text;
            this._provider.address2 = this.address2TextBox.Text;
            this._provider.city = this.cityTextBox.Text;
            this._provider.state = this.state.Text;
            this._provider.zipcode = this.zipTextBox.Text;
            this._provider.phone = this.phoneTextBox.Text;
            this._provider.fax = this.faxTextBox.Text;
            this._provider.email = this.emailTextBox.Text;
            this._provider.defaultRole = this.role.Text;
            this._provider.country = this.country.Text;
            this._provider.nationalProviderID = this.nationalProviderIDTextBox.Text;
            this._provider.UPIN = this.UPINTextBox.Text;
            this._provider.localProviderID = this.localProviderIDTextBox.Text;
            this._provider.displayName = this.displayNameTextBox.Text;
            this._provider.uploadPath = this.DocumentUploadPath.Text;
            this._provider.fullName = this.MakeFullName(
                this.firstNameTextBox.Text, 
                this.middleNameTextBox.Text,
                this.lastNameTextBox.Text);
            this._provider.dataSource = this.dataSourceTextBox.Text;
            this._provider.networkID = this.NidTextBox.Text;
        }

        private string MakeFullName(params string [] names)
        {
            string last = names.Last();

            StringBuilder nameBuilder = new StringBuilder();

            foreach (string namePart in names)
            {
                if (namePart != last)
                {
                    nameBuilder.AppendFormat("{0} ", namePart);
                }
                else
                {
                    nameBuilder.AppendFormat("{0}", namePart);
                }
            }

            return nameBuilder.ToString().TrimEnd();
        }

        private void country_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.country.Text == UnitedStates || country.Text == string.Empty)
            {
                this._international = false;
            }
            this._hasChanged = true;
        }

        private void firstNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidateControlForEmptyString(this.firstNameTextBox, FirstNameIsRequired);
            this._hasChanged = true;
        }

        private void lastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidateControlForEmptyString(this.lastNameTextBox, LastNameIsRequired);
            this._hasChanged = true;
        }

        private void address1TextBox_TextChanged(object sender, EventArgs e)
        {
            this.ValidateControlForEmptyString(this.address1TextBox, AddressIsRequired);
            this._hasChanged = true;
        }

        private void cityTextBox_TextChanged(object sender, EventArgs e)
        {
            if(!this._international) this.ValidateControlForEmptyString(this.cityTextBox, CityIsRequired);
            this._hasChanged = true;
        }

        private void state_TextChanged(object sender, EventArgs e)
        {
            if (!this._international) this.ValidateControlForEmptyString(this.state, StateIsRequired);
            this._hasChanged = true;
        }

        private void zipTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this._international) this.ValidateControlForEmptyString(this.zipTextBox, ZipIsRequired);
            this._hasChanged = true;
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.DocumentUploadPath.Text = this.folderBrowserDialog.SelectedPath;
                this._provider.uploadPath = this.folderBrowserDialog.SelectedPath;
            }
        }
    }
}
namespace RiskApps3.View.Admin
{
    partial class EditProviderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditProviderForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.NidTextBox = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dataSourceTextBox = new System.Windows.Forms.TextBox();
            this.setPhotoButton = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.providerPhoto = new System.Windows.Forms.PictureBox();
            this.localProviderIDTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.UPINTextBox = new System.Windows.Forms.TextBox();
            this.middleNameTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.country = new System.Windows.Forms.ComboBox();
            this.role = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.messageLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.nationalProviderIDTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.institutionTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.degree = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.providerIDTextBox = new System.Windows.Forms.TextBox();
            this.faxTextBox = new RiskAppUtils.MaskedTextBox.MaskedTextBox();
            this.phoneTextBox = new RiskAppUtils.MaskedTextBox.MaskedTextBox();
            this.zipTextBox = new System.Windows.Forms.TextBox();
            this.state = new System.Windows.Forms.ComboBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.address2TextBox = new System.Windows.Forms.TextBox();
            this.address1TextBox = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.DocumentUploadPath = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.displayNameTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.providerPhoto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Choose a folder to store your photos.  Please be sure this is a folder that you a" +
    "re able to write to.";
            // 
            // NidTextBox
            // 
            this.NidTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.NidTextBox.Location = new System.Drawing.Point(435, 200);
            this.NidTextBox.Name = "NidTextBox";
            this.NidTextBox.Size = new System.Drawing.Size(91, 23);
            this.NidTextBox.TabIndex = 10;
            this.NidTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label17.Location = new System.Drawing.Point(397, 203);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 16);
            this.label17.TabIndex = 201;
            this.label17.Text = "NID:";
            // 
            // dataSourceTextBox
            // 
            this.dataSourceTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.dataSourceTextBox.Location = new System.Drawing.Point(435, 171);
            this.dataSourceTextBox.Name = "dataSourceTextBox";
            this.dataSourceTextBox.Size = new System.Drawing.Size(91, 23);
            this.dataSourceTextBox.TabIndex = 7;
            this.dataSourceTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // setPhotoButton
            // 
            this.setPhotoButton.Location = new System.Drawing.Point(667, 114);
            this.setPhotoButton.Name = "setPhotoButton";
            this.setPhotoButton.Size = new System.Drawing.Size(58, 50);
            this.setPhotoButton.TabIndex = 27;
            this.setPhotoButton.Text = "Set\r\nPhoto";
            this.setPhotoButton.UseVisualStyleBackColor = true;
            this.setPhotoButton.Click += new System.EventHandler(this.setPhotoButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label16.Location = new System.Drawing.Point(397, 174);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 16);
            this.label16.TabIndex = 199;
            this.label16.Text = "Site:";
            // 
            // providerPhoto
            // 
            this.providerPhoto.Location = new System.Drawing.Point(510, 15);
            this.providerPhoto.Name = "providerPhoto";
            this.providerPhoto.Size = new System.Drawing.Size(150, 150);
            this.providerPhoto.TabIndex = 197;
            this.providerPhoto.TabStop = false;
            // 
            // localProviderIDTextBox
            // 
            this.localProviderIDTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.localProviderIDTextBox.Location = new System.Drawing.Point(581, 171);
            this.localProviderIDTextBox.Name = "localProviderIDTextBox";
            this.localProviderIDTextBox.Size = new System.Drawing.Size(143, 23);
            this.localProviderIDTextBox.TabIndex = 8;
            this.localProviderIDTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label12.Location = new System.Drawing.Point(532, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 16);
            this.label12.TabIndex = 195;
            this.label12.Text = "Site ID:";
            // 
            // UPINTextBox
            // 
            this.UPINTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.UPINTextBox.Location = new System.Drawing.Point(581, 201);
            this.UPINTextBox.Name = "UPINTextBox";
            this.UPINTextBox.Size = new System.Drawing.Size(143, 23);
            this.UPINTextBox.TabIndex = 11;
            this.UPINTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // middleNameTextBox
            // 
            this.middleNameTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.middleNameTextBox.Location = new System.Drawing.Point(158, 103);
            this.middleNameTextBox.Name = "middleNameTextBox";
            this.middleNameTextBox.Size = new System.Drawing.Size(339, 23);
            this.middleNameTextBox.TabIndex = 4;
            this.middleNameTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label10.Location = new System.Drawing.Point(61, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 16);
            this.label10.TabIndex = 189;
            this.label10.Text = "Middle Name:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label9.Location = new System.Drawing.Point(91, 356);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 16);
            this.label9.TabIndex = 188;
            this.label9.Text = "Country:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label11.Location = new System.Drawing.Point(539, 201);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 16);
            this.label11.TabIndex = 193;
            this.label11.Text = "UPIN: ";
            // 
            // country
            // 
            this.country.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.country.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.country.FormattingEnabled = true;
            this.country.Location = new System.Drawing.Point(158, 352);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(203, 24);
            this.country.TabIndex = 18;
            this.country.SelectedIndexChanged += new System.EventHandler(this.country_SelectedIndexChanged);
            // 
            // role
            // 
            this.role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.role.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.role.FormattingEnabled = true;
            this.role.Location = new System.Drawing.Point(158, 170);
            this.role.Name = "role";
            this.role.Size = new System.Drawing.Size(230, 24);
            this.role.TabIndex = 6;
            this.role.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label5.Location = new System.Drawing.Point(54, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 16);
            this.label5.TabIndex = 187;
            this.label5.Text = "Role/Specialty:";
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(45, 15);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(140, 16);
            this.messageLabel.TabIndex = 186;
            this.messageLabel.Text = "                                 ";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(213, 564);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(147, 41);
            this.cancelButton.TabIndex = 26;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.Location = new System.Drawing.Point(379, 564);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(147, 41);
            this.saveButton.TabIndex = 25;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.Red;
            this.label27.Location = new System.Drawing.Point(31, 569);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(78, 16);
            this.label27.TabIndex = 184;
            this.label27.Text = "* Required";
            // 
            // nationalProviderIDTextBox
            // 
            this.nationalProviderIDTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.nationalProviderIDTextBox.Location = new System.Drawing.Point(158, 201);
            this.nationalProviderIDTextBox.Name = "nationalProviderIDTextBox";
            this.nationalProviderIDTextBox.Size = new System.Drawing.Size(230, 23);
            this.nationalProviderIDTextBox.TabIndex = 9;
            this.nationalProviderIDTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label8.Location = new System.Drawing.Point(22, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 16);
            this.label8.TabIndex = 183;
            this.label8.Text = "National Provider ID:";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.emailTextBox.Location = new System.Drawing.Point(158, 414);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(566, 23);
            this.emailTextBox.TabIndex = 21;
            this.emailTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label7.Location = new System.Drawing.Point(104, 417);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 182;
            this.label7.Text = "Email:";
            // 
            // institutionTextBox
            // 
            this.institutionTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.institutionTextBox.Location = new System.Drawing.Point(158, 231);
            this.institutionTextBox.Name = "institutionTextBox";
            this.institutionTextBox.Size = new System.Drawing.Size(566, 23);
            this.institutionTextBox.TabIndex = 12;
            this.institutionTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label6.Location = new System.Drawing.Point(79, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 16);
            this.label6.TabIndex = 181;
            this.label6.Text = "Institution:";
            // 
            // degree
            // 
            this.degree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.degree.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.degree.FormattingEnabled = true;
            this.degree.Location = new System.Drawing.Point(324, 39);
            this.degree.Name = "degree";
            this.degree.Size = new System.Drawing.Size(173, 24);
            this.degree.TabIndex = 2;
            this.degree.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label4.Location = new System.Drawing.Point(264, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 180;
            this.label4.Text = "Degree:";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.lastNameTextBox.Location = new System.Drawing.Point(158, 136);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(339, 23);
            this.lastNameTextBox.TabIndex = 5;
            this.lastNameTextBox.TextChanged += new System.EventHandler(this.lastNameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(56, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 16);
            this.label3.TabIndex = 179;
            this.label3.Text = "Last Name: *";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.firstNameTextBox.Location = new System.Drawing.Point(158, 71);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(339, 23);
            this.firstNameTextBox.TabIndex = 3;
            this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(56, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 178;
            this.label2.Text = "First Name: *";
            // 
            // title
            // 
            this.title.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.title.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.title.FormattingEnabled = true;
            this.title.Location = new System.Drawing.Point(158, 38);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(100, 24);
            this.title.TabIndex = 1;
            this.title.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label1.Location = new System.Drawing.Point(110, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 177;
            this.label1.Text = "Title:";
            // 
            // providerIDTextBox
            // 
            this.providerIDTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.providerIDTextBox.Location = new System.Drawing.Point(11, 13);
            this.providerIDTextBox.Name = "providerIDTextBox";
            this.providerIDTextBox.ReadOnly = true;
            this.providerIDTextBox.Size = new System.Drawing.Size(28, 23);
            this.providerIDTextBox.TabIndex = 176;
            this.providerIDTextBox.Visible = false;
            // 
            // faxTextBox
            // 
            this.faxTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.faxTextBox.Location = new System.Drawing.Point(510, 383);
            this.faxTextBox.Masked = RiskAppUtils.MaskedTextBox.Mask.PhoneWithArea;
            this.faxTextBox.Name = "faxTextBox";
            this.faxTextBox.Size = new System.Drawing.Size(214, 23);
            this.faxTextBox.TabIndex = 20;
            this.faxTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.phoneTextBox.Location = new System.Drawing.Point(158, 383);
            this.phoneTextBox.Masked = RiskAppUtils.MaskedTextBox.Mask.PhoneWithArea;
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(203, 23);
            this.phoneTextBox.TabIndex = 19;
            this.phoneTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // zipTextBox
            // 
            this.zipTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.zipTextBox.Location = new System.Drawing.Point(510, 323);
            this.zipTextBox.Name = "zipTextBox";
            this.zipTextBox.Size = new System.Drawing.Size(215, 23);
            this.zipTextBox.TabIndex = 17;
            this.zipTextBox.TextChanged += new System.EventHandler(this.zipTextBox_TextChanged);
            // 
            // state
            // 
            this.state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.state.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.state.FormattingEnabled = true;
            this.state.Location = new System.Drawing.Point(397, 322);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(100, 24);
            this.state.TabIndex = 16;
            this.state.TextChanged += new System.EventHandler(this.state_TextChanged);
            // 
            // cityTextBox
            // 
            this.cityTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cityTextBox.Location = new System.Drawing.Point(158, 323);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(203, 23);
            this.cityTextBox.TabIndex = 15;
            this.cityTextBox.TextChanged += new System.EventHandler(this.cityTextBox_TextChanged);
            // 
            // address2TextBox
            // 
            this.address2TextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.address2TextBox.Location = new System.Drawing.Point(158, 292);
            this.address2TextBox.Name = "address2TextBox";
            this.address2TextBox.Size = new System.Drawing.Size(566, 23);
            this.address2TextBox.TabIndex = 14;
            this.address2TextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // address1TextBox
            // 
            this.address1TextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.address1TextBox.Location = new System.Drawing.Point(158, 261);
            this.address1TextBox.Name = "address1TextBox";
            this.address1TextBox.Size = new System.Drawing.Size(566, 23);
            this.address1TextBox.TabIndex = 13;
            this.address1TextBox.TextChanged += new System.EventHandler(this.address1TextBox_TextChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label23.Location = new System.Drawing.Point(471, 386);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(33, 16);
            this.label23.TabIndex = 175;
            this.label23.Text = "Fax:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label22.Location = new System.Drawing.Point(100, 386);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(48, 16);
            this.label22.TabIndex = 174;
            this.label22.Text = "Phone:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Red;
            this.label21.Location = new System.Drawing.Point(28, 326);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(120, 16);
            this.label21.TabIndex = 172;
            this.label21.Text = "City/State/Zip: *";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label20.Location = new System.Drawing.Point(78, 295);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(70, 16);
            this.label20.TabIndex = 173;
            this.label20.Text = "Address 2:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(57, 264);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 16);
            this.label19.TabIndex = 171;
            this.label19.Text = "Address 1: *";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectFolderButton);
            this.groupBox1.Controls.Add(this.DocumentUploadPath);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.displayNameTextBox);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(11, 454);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(713, 104);
            this.groupBox1.TabIndex = 196;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced Options:";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(591, 65);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(97, 23);
            this.selectFolderButton.TabIndex = 28;
            this.selectFolderButton.Text = "Select Folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // DocumentUploadPath
            // 
            this.DocumentUploadPath.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.DocumentUploadPath.Location = new System.Drawing.Point(164, 65);
            this.DocumentUploadPath.Name = "DocumentUploadPath";
            this.DocumentUploadPath.Size = new System.Drawing.Size(421, 23);
            this.DocumentUploadPath.TabIndex = 23;
            this.DocumentUploadPath.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label15.Location = new System.Drawing.Point(50, 68);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 16);
            this.label15.TabIndex = 149;
            this.label15.Text = "Document Path:";
            // 
            // displayNameTextBox
            // 
            this.displayNameTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.displayNameTextBox.Location = new System.Drawing.Point(164, 36);
            this.displayNameTextBox.Name = "displayNameTextBox";
            this.displayNameTextBox.Size = new System.Drawing.Size(523, 23);
            this.displayNameTextBox.TabIndex = 22;
            this.displayNameTextBox.TextChanged += new System.EventHandler(this.control_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label13.Location = new System.Drawing.Point(20, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 16);
            this.label13.TabIndex = 147;
            this.label13.Text = "Import/Export Name:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(149, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(369, 16);
            this.label14.TabIndex = 148;
            this.label14.Text = "Warning: Editing these may impact your import/export routines";
            // 
            // EditProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 619);
            this.Controls.Add(this.NidTextBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.dataSourceTextBox);
            this.Controls.Add(this.setPhotoButton);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.providerPhoto);
            this.Controls.Add(this.localProviderIDTextBox);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.UPINTextBox);
            this.Controls.Add(this.middleNameTextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.country);
            this.Controls.Add(this.role);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.nationalProviderIDTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.emailTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.institutionTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.degree);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.title);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.providerIDTextBox);
            this.Controls.Add(this.faxTextBox);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.zipTextBox);
            this.Controls.Add(this.state);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.address2TextBox);
            this.Controls.Add(this.address1TextBox);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditProviderForm";
            this.Text = "Edit Provider";
            ((System.ComponentModel.ISupportInitialize)(this.providerPhoto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NidTextBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox dataSourceTextBox;
        private System.Windows.Forms.Button setPhotoButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox providerPhoto;
        private System.Windows.Forms.TextBox localProviderIDTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox UPINTextBox;
        private System.Windows.Forms.TextBox middleNameTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DocumentUploadPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox country;
        private System.Windows.Forms.ComboBox role;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox nationalProviderIDTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox institutionTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox degree;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox providerIDTextBox;
        private RiskAppUtils.MaskedTextBox.MaskedTextBox faxTextBox;
        private RiskAppUtils.MaskedTextBox.MaskedTextBox phoneTextBox;
        private System.Windows.Forms.TextBox zipTextBox;
        private System.Windows.Forms.ComboBox state;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.TextBox address2TextBox;
        private System.Windows.Forms.TextBox address1TextBox;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox displayNameTextBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
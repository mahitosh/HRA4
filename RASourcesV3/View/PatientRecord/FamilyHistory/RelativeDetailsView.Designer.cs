namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    partial class RelativeDetailsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelativeDetailsView));
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.label23 = new System.Windows.Forms.Label();
            this.vitalStatus = new System.Windows.Forms.ComboBox();
            this.age = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.isHispanicComboBox = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.isAshkenaziComboBox = new System.Windows.Forms.ComboBox();
            this.gender = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.suffix = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.maidenName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lastName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.middleName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.firstName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dateOfDeathConfidence = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dobConfidence = new System.Windows.Forms.ComboBox();
            this.commentLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.causeOfDeath = new System.Windows.Forms.ComboBox();
            this.dob = new RiskApps3.View.Common.MaskedTextBox.MaskedTextBox();
            this.city = new System.Windows.Forms.TextBox();
            this.zipCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dateOfDeath = new RiskApps3.View.Common.MaskedTextBox.MaskedTextBox();
            this.state = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.adopted = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(211, 83);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(27, 33);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 6;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.SystemColors.Control;
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(148, 13);
            this.label23.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 13);
            this.label23.TabIndex = 250;
            this.label23.Text = "Vital Status:";
            // 
            // vitalStatus
            // 
            this.vitalStatus.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vitalStatus.FormattingEnabled = true;
            this.vitalStatus.Items.AddRange(new object[] {
            "",
            "Alive",
            "Dead",
            "Unknown"});
            this.vitalStatus.Location = new System.Drawing.Point(149, 29);
            this.vitalStatus.Name = "vitalStatus";
            this.vitalStatus.Size = new System.Drawing.Size(86, 21);
            this.vitalStatus.TabIndex = 9;
            this.vitalStatus.SelectionChangeCommitted += new System.EventHandler(this.vitalStatus_SelectionChangeCommitted);
            // 
            // age
            // 
            this.age.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.age.Location = new System.Drawing.Point(5, 29);
            this.age.Name = "age";
            this.age.Size = new System.Drawing.Size(33, 21);
            this.age.TabIndex = 7;
            this.age.Validated += new System.EventHandler(this.age_Validated);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.Control;
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(4, 13);
            this.label22.Margin = new System.Windows.Forms.Padding(0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(26, 13);
            this.label22.TabIndex = 247;
            this.label22.Text = "Age";
            // 
            // isHispanicComboBox
            // 
            this.isHispanicComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No",
            "Unknown"});
            this.isHispanicComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.isHispanicComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isHispanicComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isHispanicComboBox.FormattingEnabled = true;
            this.isHispanicComboBox.Items.AddRange(new object[] {
            "",
            "Don\'t Know",
            "Prefer Not to Answer",
            "No",
            "Yes"});
            this.isHispanicComboBox.Location = new System.Drawing.Point(133, 28);
            this.isHispanicComboBox.Name = "isHispanicComboBox";
            this.isHispanicComboBox.Size = new System.Drawing.Size(125, 21);
            this.isHispanicComboBox.TabIndex = 13;
            this.isHispanicComboBox.SelectionChangeCommitted += new System.EventHandler(this.isHispanicComboBox_SelectionChangeCommitted);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(130, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 13);
            this.label21.TabIndex = 243;
            this.label21.Text = "Hispanic:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.Control;
            this.label20.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 13);
            this.label20.TabIndex = 242;
            this.label20.Text = "Ashkenazi Jewish:";
            // 
            // isAshkenaziComboBox
            // 
            this.isAshkenaziComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No",
            "Unknown"});
            this.isAshkenaziComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.isAshkenaziComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.isAshkenaziComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.isAshkenaziComboBox.FormattingEnabled = true;
            this.isAshkenaziComboBox.Items.AddRange(new object[] {
            "",
            "Don\'t Know",
            "Prefer Not to Answer",
            "No",
            "Yes"});
            this.isAshkenaziComboBox.Location = new System.Drawing.Point(4, 28);
            this.isAshkenaziComboBox.Name = "isAshkenaziComboBox";
            this.isAshkenaziComboBox.Size = new System.Drawing.Size(125, 21);
            this.isAshkenaziComboBox.TabIndex = 12;
            this.isAshkenaziComboBox.SelectionChangeCommitted += new System.EventHandler(this.isAshkenaziComboBox_SelectionChangeCommitted);
            // 
            // gender
            // 
            this.gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gender.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gender.FormattingEnabled = true;
            this.gender.Items.AddRange(new object[] {
            "",
            "Male",
            "Female",
            "Unknown"});
            this.gender.Location = new System.Drawing.Point(44, 29);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(101, 21);
            this.gender.TabIndex = 8;
            this.gender.SelectionChangeCommitted += new System.EventHandler(this.gender_SelectionChangeCommitted);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(41, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(46, 13);
            this.label16.TabIndex = 238;
            this.label16.Text = "Gender:";
            // 
            // suffix
            // 
            this.suffix.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.suffix.Location = new System.Drawing.Point(148, 91);
            this.suffix.Name = "suffix";
            this.suffix.Size = new System.Drawing.Size(45, 21);
            this.suffix.TabIndex = 6;
            this.suffix.Validated += new System.EventHandler(this.suffix_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(146, 75);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 13);
            this.label12.TabIndex = 236;
            this.label12.Text = "Suffix: ";
            // 
            // maidenName
            // 
            this.maidenName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maidenName.Location = new System.Drawing.Point(55, 91);
            this.maidenName.Name = "maidenName";
            this.maidenName.Size = new System.Drawing.Size(89, 21);
            this.maidenName.TabIndex = 5;
            this.maidenName.Validated += new System.EventHandler(this.maidenName_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(52, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 235;
            this.label11.Text = "Maiden:";
            // 
            // lastName
            // 
            this.lastName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastName.Location = new System.Drawing.Point(148, 51);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(109, 21);
            this.lastName.TabIndex = 3;
            this.lastName.Validated += new System.EventHandler(this.lastName_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(149, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 234;
            this.label10.Text = "Last:";
            // 
            // middleName
            // 
            this.middleName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.middleName.Location = new System.Drawing.Point(87, 51);
            this.middleName.Name = "middleName";
            this.middleName.Size = new System.Drawing.Size(55, 21);
            this.middleName.TabIndex = 2;
            this.middleName.Validated += new System.EventHandler(this.middleName_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(84, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 233;
            this.label9.Text = "Middle:";
            // 
            // firstName
            // 
            this.firstName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.firstName.Location = new System.Drawing.Point(6, 52);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(75, 21);
            this.firstName.TabIndex = 1;
            this.firstName.Validated += new System.EventHandler(this.firstName_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(3, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 232;
            this.label8.Text = "First:";
            // 
            // title
            // 
            this.title.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.title.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.FormattingEnabled = true;
            this.title.Items.AddRange(new object[] {
            "",
            "Dr.",
            "Miss.",
            "Mr.",
            "Mrs.",
            "Ms."});
            this.title.Location = new System.Drawing.Point(6, 91);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(43, 21);
            this.title.TabIndex = 4;
            this.title.SelectionChangeCommitted += new System.EventHandler(this.title_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(3, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 231;
            this.label7.Text = "Title:";
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(6, 13);
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Size = new System.Drawing.Size(232, 21);
            this.name.TabIndex = 237;
            this.name.Validated += new System.EventHandler(this.name_Validated);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(2, 189);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(62, 13);
            this.label17.TabIndex = 248;
            this.label17.Text = "Nationality:";
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkedListBox2.CheckOnClick = true;
            this.checkedListBox2.Enabled = false;
            this.checkedListBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "United States",
            "Afghanistan",
            "Albania",
            "Algeria",
            "Andorra",
            "Angola",
            "Antigua and Barbuda",
            "Argentina",
            "Armenia",
            "Australia",
            "Austria",
            "Azerbaijan",
            "Bahamas",
            "Bahrain",
            "Bangladesh ",
            "Barbados ",
            "Belarus ",
            "Belgium ",
            "Belize ",
            "Benin",
            "Bermuda",
            "Bhutan ",
            "Bolivia ",
            "Bosnia and Herzegovina ",
            "Botswana ",
            "Brazil ",
            "Brunei ",
            "Bulgaria ",
            "Burkina Faso ",
            "Burma ",
            "Burundi",
            "Cambodia",
            "Cameroon",
            "Canada",
            "Cape Verde",
            "Central African Republic",
            "Chad",
            "Chile",
            "China",
            "Colombia",
            "Comoros",
            "Congo (Brazzaville)",
            "Congo (Kinshasa)",
            "Costa Rica",
            "Cote d\'Ivoire",
            "Croatia",
            "Cuba",
            "Cyprus",
            "Czech Republic",
            "Denmark",
            "Djibouti",
            "Dominica",
            "Dominican Republic",
            "East Timor",
            "Ecuador",
            "Egypt",
            "El Salvador",
            "Equatorial Guinea",
            "Eritrea",
            "Estonia",
            "Ethiopia",
            "Fiji",
            "Finland",
            "France",
            "Gabon",
            "Gambia, The",
            "Georgia",
            "Germany",
            "Ghana",
            "Greece",
            "Grenada",
            "Guatemala",
            "Guinea",
            "Guinea-Bissau",
            "Guyana",
            "Haiti",
            "Holy See",
            "Honduras",
            "Hungary",
            "Iceland",
            "India",
            "Indonesia",
            "Iran",
            "Iraq",
            "Ireland",
            "Israel",
            "Italy",
            "Jamaica",
            "Japan",
            "Jordan",
            "Kazakhstan",
            "Kenya",
            "Kiribati",
            "Korea, North",
            "Korea, South",
            "Kuwait",
            "Kyrgyzstan",
            "Laos",
            "Latvia",
            "Lebanon",
            "Lesotho",
            "Liberia",
            "Libya",
            "Liechtenstein ",
            "Lithuania",
            "Luxembourg ",
            "Macedonia",
            "Madagascar",
            "Malawi",
            "Malaysia",
            "Maldives",
            "Mali",
            "Malta",
            "Marshall Islands",
            "Mauritania",
            "Mauritius",
            "Mexico",
            "Micronesia",
            "Moldova",
            "Monaco",
            "Mongolia",
            "Montenegro",
            "Morocco",
            "Mozambique",
            "Namibia",
            "Nauru",
            "Nepal",
            "Netherlands Antilles",
            "Netherlands",
            "New Zealand",
            "Nicaragua",
            "Niger",
            "Nigeria",
            "North Korea",
            "Norway",
            "Oman",
            "Pakistan",
            "Palau",
            "Panama",
            "Papua New Guinea",
            "Paraguay",
            "Peru",
            "Philippines",
            "Poland",
            "Portugal",
            "Qatar",
            "Romania",
            "Russia",
            "Rwanda",
            "Saint Kitts and Nevis",
            "Saint Lucia",
            "Saint Vincent and the Grenadines",
            "Samoa",
            "San Marino",
            "Sao Tome and Principe",
            "Saudi Arabia",
            "Senegal",
            "Serbia",
            "Seychelles",
            "Sierra Leone",
            "Singapore",
            "Slovakia",
            "Slovenia",
            "Solomon Islands",
            "Somalia",
            "South Africa",
            "South Korea",
            "Spain",
            "Sri Lanka",
            "Sudan ",
            "Suriname",
            "Swaziland",
            "Sweden ",
            "Switzerland",
            "Syria",
            "Tajikistan",
            "Tanzania",
            "Thailand ",
            "Timor-Leste",
            "Togo",
            "Tonga",
            "Trinidad and Tobago",
            "Tunisia",
            "Turkey",
            "Turkmenistan ",
            "Tuvalu",
            "Uganda",
            "Ukraine",
            "United Arab Emirates",
            "United Kingdom",
            "Uruguay",
            "Uzbekistan",
            "Vanuatu",
            "Venezuela",
            "Vietnam",
            "Yemen",
            "Zambia",
            "Zimbabwe "});
            this.checkedListBox2.Location = new System.Drawing.Point(6, 206);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(240, 148);
            this.checkedListBox2.TabIndex = 15;
            this.checkedListBox2.SelectedIndexChanged += new System.EventHandler(this.checkedListBox2_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(8, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 13);
            this.label15.TabIndex = 246;
            this.label15.Text = "Race:";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Enabled = false;
            this.checkedListBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "African American or Black",
            "American Indian/Aleutian/Eskimo",
            "Asian or Pacific Islander",
            "Caribbean/West Indian",
            "Caucasian or White",
            "Other",
            "Unknown"});
            this.checkedListBox1.Location = new System.Drawing.Point(6, 68);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(240, 116);
            this.checkedListBox1.TabIndex = 14;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.firstName);
            this.groupBox1.Controls.Add(this.suffix);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lastName);
            this.groupBox1.Controls.Add(this.title);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.loadingCircle1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.maidenName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.middleName);
            this.groupBox1.Location = new System.Drawing.Point(3, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 118);
            this.groupBox1.TabIndex = 253;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.dateOfDeathConfidence);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.commentsTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dobConfidence);
            this.groupBox2.Controls.Add(this.commentLabel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.gender);
            this.groupBox2.Controls.Add(this.causeOfDeath);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.dob);
            this.groupBox2.Controls.Add(this.vitalStatus);
            this.groupBox2.Controls.Add(this.city);
            this.groupBox2.Controls.Add(this.age);
            this.groupBox2.Controls.Add(this.zipCode);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dateOfDeath);
            this.groupBox2.Controls.Add(this.state);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.adopted);
            this.groupBox2.Location = new System.Drawing.Point(3, 121);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 107);
            this.groupBox2.TabIndex = 254;
            this.groupBox2.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(190, 166);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 38);
            this.button2.TabIndex = 261;
            this.button2.Text = "Update Age";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.SystemColors.Control;
            this.label24.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(130, 107);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(108, 13);
            this.label24.TabIndex = 260;
            this.label24.Text = "Adopted FHx known:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "Yes",
            "No",
            "Unknown",
            "Prefer not to answer"});
            this.comboBox1.Location = new System.Drawing.Point(131, 124);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 21);
            this.comboBox1.TabIndex = 259;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(3, 108);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(52, 13);
            this.label19.TabIndex = 240;
            this.label19.Text = "Adopted:";
            // 
            // dateOfDeathConfidence
            // 
            this.dateOfDeathConfidence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateOfDeathConfidence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfDeathConfidence.FormattingEnabled = true;
            this.dateOfDeathConfidence.Items.AddRange(new object[] {
            "",
            "Day",
            "Month",
            "Year",
            "Decade",
            "Unknown"});
            this.dateOfDeathConfidence.Location = new System.Drawing.Point(98, 206);
            this.dateOfDeathConfidence.Name = "dateOfDeathConfidence";
            this.dateOfDeathConfidence.Size = new System.Drawing.Size(82, 21);
            this.dateOfDeathConfidence.TabIndex = 219;
            this.dateOfDeathConfidence.SelectionChangeCommitted += new System.EventHandler(this.dateOfDeathConfidence_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(203, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 258;
            this.label1.Text = "More";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.Control;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(96, 151);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(68, 13);
            this.label18.TabIndex = 229;
            this.label18.Text = "Confidence: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(202, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(2, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 228;
            this.label14.Text = "Date of Death: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(96, 191);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 13);
            this.label13.TabIndex = 230;
            this.label13.Text = "Confidence: ";
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentsTextBox.Location = new System.Drawing.Point(4, 74);
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(190, 21);
            this.commentsTextBox.TabIndex = 10;
            this.commentsTextBox.Validated += new System.EventHandler(this.commentsTextBox_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(3, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 227;
            this.label2.Text = "Date of Birth: ";
            // 
            // dobConfidence
            // 
            this.dobConfidence.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dobConfidence.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dobConfidence.FormattingEnabled = true;
            this.dobConfidence.Items.AddRange(new object[] {
            "",
            "Day",
            "Month",
            "Year",
            "Decade",
            "Unknown"});
            this.dobConfidence.Location = new System.Drawing.Point(98, 166);
            this.dobConfidence.Name = "dobConfidence";
            this.dobConfidence.Size = new System.Drawing.Size(82, 21);
            this.dobConfidence.TabIndex = 218;
            this.dobConfidence.SelectionChangeCommitted += new System.EventHandler(this.dobConfidence_SelectionChangeCommitted);
            // 
            // commentLabel
            // 
            this.commentLabel.AutoSize = true;
            this.commentLabel.BackColor = System.Drawing.SystemColors.Control;
            this.commentLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentLabel.Location = new System.Drawing.Point(3, 58);
            this.commentLabel.Name = "commentLabel";
            this.commentLabel.Size = new System.Drawing.Size(61, 13);
            this.commentLabel.TabIndex = 256;
            this.commentLabel.Text = "Comments:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(3, 229);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 237;
            this.label6.Text = "Cause of Death:";
            // 
            // causeOfDeath
            // 
            this.causeOfDeath.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.causeOfDeath.FormattingEnabled = true;
            this.causeOfDeath.Items.AddRange(new object[] {
            "",
            "Cancer",
            "Heart",
            "Unknown",
            "Trauma",
            "Suicide",
            "Stroke",
            "Abortion",
            "Miscarriage"});
            this.causeOfDeath.Location = new System.Drawing.Point(5, 245);
            this.causeOfDeath.Name = "causeOfDeath";
            this.causeOfDeath.Size = new System.Drawing.Size(233, 21);
            this.causeOfDeath.TabIndex = 245;
            this.causeOfDeath.SelectionChangeCommitted += new System.EventHandler(this.causeOfDeath_SelectionChangeCommitted);
            this.causeOfDeath.Validated += new System.EventHandler(this.causeOfDeath_SelectionChangeCommitted);
            // 
            // dob
            // 
            this.dob.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dob.Location = new System.Drawing.Point(5, 166);
            this.dob.Masked = RiskApps3.View.Common.MaskedTextBox.Mask.DOBDate;
            this.dob.Name = "dob";
            this.dob.Size = new System.Drawing.Size(84, 21);
            this.dob.TabIndex = 251;
            this.dob.Validated += new System.EventHandler(this.dob_Validated);
            // 
            // city
            // 
            this.city.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.city.Location = new System.Drawing.Point(6, 285);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(91, 21);
            this.city.TabIndex = 221;
            this.city.Validated += new System.EventHandler(this.city_Validated);
            // 
            // zipCode
            // 
            this.zipCode.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zipCode.Location = new System.Drawing.Point(170, 285);
            this.zipCode.Name = "zipCode";
            this.zipCode.Size = new System.Drawing.Size(68, 21);
            this.zipCode.TabIndex = 223;
            this.zipCode.Validated += new System.EventHandler(this.zipCode_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(167, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 226;
            this.label3.Text = "Zip Code:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 224;
            this.label5.Text = "City:";
            // 
            // dateOfDeath
            // 
            this.dateOfDeath.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateOfDeath.Location = new System.Drawing.Point(7, 206);
            this.dateOfDeath.Masked = RiskApps3.View.Common.MaskedTextBox.Mask.DOBDate;
            this.dateOfDeath.Name = "dateOfDeath";
            this.dateOfDeath.Size = new System.Drawing.Size(83, 21);
            this.dateOfDeath.TabIndex = 252;
            this.dateOfDeath.Validated += new System.EventHandler(this.dateOfDeath_Validated);
            // 
            // state
            // 
            this.state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.state.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.state.FormattingEnabled = true;
            this.state.Items.AddRange(new object[] {
            "",
            "AK",
            "AL",
            "AR",
            "AS",
            "AZ",
            "CA",
            "CO",
            "CT",
            "DC",
            "DE",
            "FL",
            "FM",
            "GA",
            "GU",
            "HI",
            "IA",
            "ID",
            "IL",
            "IN",
            "KS",
            "KY",
            "LA",
            "MA",
            "MD",
            "ME",
            "MH",
            "MI",
            "MN",
            "MO",
            "MP",
            "MS",
            "MT",
            "NC",
            "ND",
            "NE",
            "NH",
            "NJ",
            "NM",
            "NV",
            "NY",
            "OH",
            "OK",
            "OR",
            "PA",
            "PR",
            "RI",
            "SC",
            "SD",
            "TN",
            "TX",
            "UT",
            "VA",
            "VI",
            "VT",
            "WA",
            "WI",
            "WV",
            "WY"});
            this.state.Location = new System.Drawing.Point(103, 285);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(61, 21);
            this.state.TabIndex = 222;
            this.state.SelectionChangeCommitted += new System.EventHandler(this.state_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(100, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 225;
            this.label4.Text = "State:";
            // 
            // adopted
            // 
            this.adopted.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.adopted.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adopted.FormattingEnabled = true;
            this.adopted.Items.AddRange(new object[] {
            "",
            "Yes",
            "No",
            "Unknown",
            "Prefer not to answer"});
            this.adopted.Location = new System.Drawing.Point(5, 124);
            this.adopted.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.adopted.Name = "adopted";
            this.adopted.Size = new System.Drawing.Size(120, 21);
            this.adopted.TabIndex = 239;
            this.adopted.SelectionChangeCommitted += new System.EventHandler(this.adopted_SelectionChangeCommitted);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkedListBox2);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.isHispanicComboBox);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.checkedListBox1);
            this.groupBox3.Controls.Add(this.isAshkenaziComboBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 230);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(263, 376);
            this.groupBox3.TabIndex = 255;
            this.groupBox3.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(266, 599);
            this.flowLayoutPanel1.TabIndex = 256;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.Resize += new System.EventHandler(this.flowLayoutPanel1_Resize);
            // 
            // RelativeDetailsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(266, 599);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(282, 638);
            this.MinimumSize = new System.Drawing.Size(16, 39);
            this.Name = "RelativeDetailsView";
            this.Text = "Details";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RelativeDetailsView_FormClosing);
            this.Load += new System.EventHandler(this.RelativeDetailsView_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox vitalStatus;
        private System.Windows.Forms.TextBox age;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox isHispanicComboBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox isAshkenaziComboBox;
        private System.Windows.Forms.ComboBox gender;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox suffix;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox maidenName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox lastName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox middleName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox firstName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox title;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Label commentLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox dateOfDeathConfidence;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dobConfidence;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox causeOfDeath;
        private RiskApps3.View.Common.MaskedTextBox.MaskedTextBox dob;
        private System.Windows.Forms.TextBox city;
        private System.Windows.Forms.TextBox zipCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private RiskApps3.View.Common.MaskedTextBox.MaskedTextBox dateOfDeath;
        private System.Windows.Forms.ComboBox state;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox adopted;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
    }
}
namespace RiskApps3.View.PatientRecord
{
    partial class DemographicsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemographicsView));
            this.lastName = new System.Windows.Forms.TextBox();
            this.firstName = new System.Windows.Forms.TextBox();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.patientRecordHeader1 = new RiskApps3.View.PatientRecord.PatientRecordHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.EMPI = new System.Windows.Forms.TextBox();
            this.middleName = new System.Windows.Forms.TextBox();
            this.educationLevel = new System.Windows.Forms.TextBox();
            this.maidenName = new System.Windows.Forms.TextBox();
            this.occupation = new System.Windows.Forms.TextBox();
            this.socsecnum = new System.Windows.Forms.TextBox();
            this.contactcellphone = new System.Windows.Forms.TextBox();
            this.address1 = new System.Windows.Forms.TextBox();
            this.contacthomephone = new System.Windows.Forms.TextBox();
            this.address2 = new System.Windows.Forms.TextBox();
            this.contactworkphone = new System.Windows.Forms.TextBox();
            this.city = new System.Windows.Forms.TextBox();
            this.contactmiddlename = new System.Windows.Forms.TextBox();
            this.state = new System.Windows.Forms.TextBox();
            this.contactfirstname = new System.Windows.Forms.TextBox();
            this.zip = new System.Windows.Forms.TextBox();
            this.contactlastname = new System.Windows.Forms.TextBox();
            this.homephone = new System.Windows.Forms.TextBox();
            this.religion = new System.Windows.Forms.TextBox();
            this.country = new System.Windows.Forms.TextBox();
            this.maritalstatus = new System.Windows.Forms.TextBox();
            this.workphone = new System.Windows.Forms.TextBox();
            this.dob = new System.Windows.Forms.TextBox();
            this.cellphone = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lastName
            // 
            this.lastName.Location = new System.Drawing.Point(3, 3);
            this.lastName.Name = "lastName";
            this.lastName.Size = new System.Drawing.Size(100, 20);
            this.lastName.TabIndex = 1;
            this.lastName.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // firstName
            // 
            this.firstName.Location = new System.Drawing.Point(109, 3);
            this.firstName.Name = "firstName";
            this.firstName.Size = new System.Drawing.Size(100, 20);
            this.firstName.TabIndex = 3;
            this.firstName.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(15, 336);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(44, 37);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 4;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // patientRecordHeader1
            // 
            this.patientRecordHeader1.Collapsed = false;
            this.patientRecordHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.patientRecordHeader1.Location = new System.Drawing.Point(0, 0);
            this.patientRecordHeader1.Name = "patientRecordHeader1";
            this.patientRecordHeader1.Size = new System.Drawing.Size(395, 42);
            this.patientRecordHeader1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 282);
            this.panel1.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 248);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lastName);
            this.panel2.Controls.Add(this.firstName);
            this.panel2.Controls.Add(this.EMPI);
            this.panel2.Controls.Add(this.middleName);
            this.panel2.Controls.Add(this.educationLevel);
            this.panel2.Controls.Add(this.maidenName);
            this.panel2.Controls.Add(this.occupation);
            this.panel2.Controls.Add(this.socsecnum);
            this.panel2.Controls.Add(this.contactcellphone);
            this.panel2.Controls.Add(this.address1);
            this.panel2.Controls.Add(this.contacthomephone);
            this.panel2.Controls.Add(this.address2);
            this.panel2.Controls.Add(this.contactworkphone);
            this.panel2.Controls.Add(this.city);
            this.panel2.Controls.Add(this.contactmiddlename);
            this.panel2.Controls.Add(this.state);
            this.panel2.Controls.Add(this.contactfirstname);
            this.panel2.Controls.Add(this.zip);
            this.panel2.Controls.Add(this.contactlastname);
            this.panel2.Controls.Add(this.homephone);
            this.panel2.Controls.Add(this.religion);
            this.panel2.Controls.Add(this.country);
            this.panel2.Controls.Add(this.maritalstatus);
            this.panel2.Controls.Add(this.workphone);
            this.panel2.Controls.Add(this.dob);
            this.panel2.Controls.Add(this.cellphone);
            this.panel2.Location = new System.Drawing.Point(10, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 239);
            this.panel2.TabIndex = 5;
            // 
            // EMPI
            // 
            this.EMPI.Location = new System.Drawing.Point(109, 211);
            this.EMPI.Name = "EMPI";
            this.EMPI.Size = new System.Drawing.Size(100, 20);
            this.EMPI.TabIndex = 27;
            this.EMPI.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // middleName
            // 
            this.middleName.Location = new System.Drawing.Point(215, 3);
            this.middleName.Name = "middleName";
            this.middleName.Size = new System.Drawing.Size(100, 20);
            this.middleName.TabIndex = 4;
            this.middleName.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // educationLevel
            // 
            this.educationLevel.Location = new System.Drawing.Point(3, 211);
            this.educationLevel.Name = "educationLevel";
            this.educationLevel.Size = new System.Drawing.Size(100, 20);
            this.educationLevel.TabIndex = 26;
            this.educationLevel.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // maidenName
            // 
            this.maidenName.Location = new System.Drawing.Point(3, 29);
            this.maidenName.Name = "maidenName";
            this.maidenName.Size = new System.Drawing.Size(100, 20);
            this.maidenName.TabIndex = 5;
            this.maidenName.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // occupation
            // 
            this.occupation.Location = new System.Drawing.Point(215, 185);
            this.occupation.Name = "occupation";
            this.occupation.Size = new System.Drawing.Size(100, 20);
            this.occupation.TabIndex = 25;
            this.occupation.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // socsecnum
            // 
            this.socsecnum.Location = new System.Drawing.Point(109, 29);
            this.socsecnum.Name = "socsecnum";
            this.socsecnum.Size = new System.Drawing.Size(100, 20);
            this.socsecnum.TabIndex = 6;
            this.socsecnum.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contactcellphone
            // 
            this.contactcellphone.Location = new System.Drawing.Point(109, 185);
            this.contactcellphone.Name = "contactcellphone";
            this.contactcellphone.Size = new System.Drawing.Size(100, 20);
            this.contactcellphone.TabIndex = 24;
            this.contactcellphone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // address1
            // 
            this.address1.Location = new System.Drawing.Point(215, 29);
            this.address1.Name = "address1";
            this.address1.Size = new System.Drawing.Size(100, 20);
            this.address1.TabIndex = 7;
            this.address1.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contacthomephone
            // 
            this.contacthomephone.Location = new System.Drawing.Point(3, 185);
            this.contacthomephone.Name = "contacthomephone";
            this.contacthomephone.Size = new System.Drawing.Size(100, 20);
            this.contacthomephone.TabIndex = 22;
            this.contacthomephone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // address2
            // 
            this.address2.Location = new System.Drawing.Point(3, 55);
            this.address2.Name = "address2";
            this.address2.Size = new System.Drawing.Size(100, 20);
            this.address2.TabIndex = 8;
            this.address2.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contactworkphone
            // 
            this.contactworkphone.Location = new System.Drawing.Point(215, 159);
            this.contactworkphone.Name = "contactworkphone";
            this.contactworkphone.Size = new System.Drawing.Size(100, 20);
            this.contactworkphone.TabIndex = 23;
            this.contactworkphone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // city
            // 
            this.city.Location = new System.Drawing.Point(109, 55);
            this.city.Name = "city";
            this.city.Size = new System.Drawing.Size(100, 20);
            this.city.TabIndex = 9;
            this.city.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contactmiddlename
            // 
            this.contactmiddlename.Location = new System.Drawing.Point(109, 159);
            this.contactmiddlename.Name = "contactmiddlename";
            this.contactmiddlename.Size = new System.Drawing.Size(100, 20);
            this.contactmiddlename.TabIndex = 21;
            this.contactmiddlename.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // state
            // 
            this.state.Location = new System.Drawing.Point(215, 55);
            this.state.Name = "state";
            this.state.Size = new System.Drawing.Size(100, 20);
            this.state.TabIndex = 10;
            this.state.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contactfirstname
            // 
            this.contactfirstname.Location = new System.Drawing.Point(3, 159);
            this.contactfirstname.Name = "contactfirstname";
            this.contactfirstname.Size = new System.Drawing.Size(100, 20);
            this.contactfirstname.TabIndex = 20;
            this.contactfirstname.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // zip
            // 
            this.zip.Location = new System.Drawing.Point(3, 81);
            this.zip.Name = "zip";
            this.zip.Size = new System.Drawing.Size(100, 20);
            this.zip.TabIndex = 11;
            this.zip.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // contactlastname
            // 
            this.contactlastname.Location = new System.Drawing.Point(215, 133);
            this.contactlastname.Name = "contactlastname";
            this.contactlastname.Size = new System.Drawing.Size(100, 20);
            this.contactlastname.TabIndex = 19;
            this.contactlastname.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // homephone
            // 
            this.homephone.Location = new System.Drawing.Point(109, 81);
            this.homephone.Name = "homephone";
            this.homephone.Size = new System.Drawing.Size(100, 20);
            this.homephone.TabIndex = 13;
            this.homephone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // religion
            // 
            this.religion.Location = new System.Drawing.Point(109, 133);
            this.religion.Name = "religion";
            this.religion.Size = new System.Drawing.Size(100, 20);
            this.religion.TabIndex = 18;
            this.religion.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // country
            // 
            this.country.Location = new System.Drawing.Point(215, 81);
            this.country.Name = "country";
            this.country.Size = new System.Drawing.Size(100, 20);
            this.country.TabIndex = 12;
            this.country.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // maritalstatus
            // 
            this.maritalstatus.Location = new System.Drawing.Point(3, 133);
            this.maritalstatus.Name = "maritalstatus";
            this.maritalstatus.Size = new System.Drawing.Size(100, 20);
            this.maritalstatus.TabIndex = 17;
            this.maritalstatus.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // workphone
            // 
            this.workphone.Location = new System.Drawing.Point(3, 107);
            this.workphone.Name = "workphone";
            this.workphone.Size = new System.Drawing.Size(100, 20);
            this.workphone.TabIndex = 14;
            this.workphone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // dob
            // 
            this.dob.Location = new System.Drawing.Point(215, 107);
            this.dob.Name = "dob";
            this.dob.Size = new System.Drawing.Size(100, 20);
            this.dob.TabIndex = 16;
            this.dob.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // cellphone
            // 
            this.cellphone.Location = new System.Drawing.Point(109, 107);
            this.cellphone.Name = "cellphone";
            this.cellphone.Size = new System.Drawing.Size(100, 20);
            this.cellphone.TabIndex = 15;
            this.cellphone.Leave += new System.EventHandler(this.DemographicsTextBox_Leave);
            // 
            // DemographicsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 379);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.patientRecordHeader1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DemographicsView";
            this.Text = "Demographics";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DemographicsView_FormClosing);
            this.Load += new System.EventHandler(this.DemographicsView_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox lastName;
        private System.Windows.Forms.TextBox firstName;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private PatientRecordHeader patientRecordHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox state;
        private System.Windows.Forms.TextBox city;
        private System.Windows.Forms.TextBox address2;
        private System.Windows.Forms.TextBox address1;
        private System.Windows.Forms.TextBox socsecnum;
        private System.Windows.Forms.TextBox maidenName;
        private System.Windows.Forms.TextBox middleName;
        private System.Windows.Forms.TextBox EMPI;
        private System.Windows.Forms.TextBox educationLevel;
        private System.Windows.Forms.TextBox occupation;
        private System.Windows.Forms.TextBox contactcellphone;
        private System.Windows.Forms.TextBox contacthomephone;
        private System.Windows.Forms.TextBox contactworkphone;
        private System.Windows.Forms.TextBox contactmiddlename;
        private System.Windows.Forms.TextBox contactfirstname;
        private System.Windows.Forms.TextBox contactlastname;
        private System.Windows.Forms.TextBox religion;
        private System.Windows.Forms.TextBox maritalstatus;
        private System.Windows.Forms.TextBox dob;
        private System.Windows.Forms.TextBox cellphone;
        private System.Windows.Forms.TextBox workphone;
        private System.Windows.Forms.TextBox country;
        private System.Windows.Forms.TextBox homephone;
        private System.Windows.Forms.TextBox zip;
    }
}
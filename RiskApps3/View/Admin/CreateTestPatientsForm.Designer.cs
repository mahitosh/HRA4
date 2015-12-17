namespace RiskApps3.View.Admin
{
    partial class CreateTestPatientsForm
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
            this.cbClinic = new System.Windows.Forms.ComboBox();
            this.cbSurvey = new System.Windows.Forms.ComboBox();
            this.dtAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.cbNumPatients = new System.Windows.Forms.ComboBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label62 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbClinic
            // 
            this.cbClinic.FormattingEnabled = true;
            this.cbClinic.Location = new System.Drawing.Point(509, 50);
            this.cbClinic.Name = "cbClinic";
            this.cbClinic.Size = new System.Drawing.Size(108, 21);
            this.cbClinic.TabIndex = 8;
            // 
            // cbSurvey
            // 
            this.cbSurvey.FormattingEnabled = true;
            this.cbSurvey.Location = new System.Drawing.Point(357, 50);
            this.cbSurvey.Name = "cbSurvey";
            this.cbSurvey.Size = new System.Drawing.Size(121, 21);
            this.cbSurvey.TabIndex = 1;
            // 
            // dtAppointmentDate
            // 
            this.dtAppointmentDate.Location = new System.Drawing.Point(126, 50);
            this.dtAppointmentDate.Name = "dtAppointmentDate";
            this.dtAppointmentDate.Size = new System.Drawing.Size(200, 20);
            this.dtAppointmentDate.TabIndex = 7;
            // 
            // cbNumPatients
            // 
            this.cbNumPatients.FormattingEnabled = true;
            this.cbNumPatients.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "5",
            "10",
            "20"});
            this.cbNumPatients.Location = new System.Drawing.Point(29, 50);
            this.cbNumPatients.Name = "cbNumPatients";
            this.cbNumPatients.Size = new System.Drawing.Size(56, 21);
            this.cbNumPatients.TabIndex = 6;
            this.cbNumPatients.Text = "10";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(141, 106);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(123, 30);
            this.btnCreate.TabIndex = 5;
            this.btnCreate.Text = "Ok";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Location = new System.Drawing.Point(509, 33);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(32, 13);
            this.label62.TabIndex = 4;
            this.label62.Text = "Clinic";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Location = new System.Drawing.Point(126, 33);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(92, 13);
            this.label61.TabIndex = 3;
            this.label61.Text = "Appointment Date";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Location = new System.Drawing.Point(357, 33);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(40, 13);
            this.label60.TabIndex = 2;
            this.label60.Text = "Survey";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Location = new System.Drawing.Point(29, 33);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(44, 13);
            this.label59.TabIndex = 1;
            this.label59.Text = "Number";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(340, 106);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(123, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CreateTestPatientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 166);
            this.Controls.Add(this.cbClinic);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbSurvey);
            this.Controls.Add(this.dtAppointmentDate);
            this.Controls.Add(this.cbNumPatients);
            this.Controls.Add(this.label59);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.label62);
            this.Controls.Add(this.label61);
            this.Name = "CreateTestPatientsForm";
            this.Text = "Create Test Patients";
            this.Load += new System.EventHandler(this.CreateTestPatientsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbClinic;
        private System.Windows.Forms.ComboBox cbSurvey;
        private System.Windows.Forms.DateTimePicker dtAppointmentDate;
        private System.Windows.Forms.ComboBox cbNumPatients;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Button btnCancel;
    }
}
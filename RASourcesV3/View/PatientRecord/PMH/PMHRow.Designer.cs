namespace RiskApps3.View.PatientRecord.PMH
{
    partial class PMHRow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.comments = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.disease = new RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox();
            this.ageDiagnosis = new RiskApps3.Utilities.IntegerTextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(533, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comments
            // 
            this.comments.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comments.Location = new System.Drawing.Point(240, 2);
            this.comments.Name = "comments";
            this.comments.Size = new System.Drawing.Size(287, 21);
            this.comments.TabIndex = 2;
            this.comments.Validated += new System.EventHandler(this.comments_Validated);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(573, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(34, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.DeleteRowButton_Click);
            // 
            // disease
            // 
            this.disease.BackColor = System.Drawing.SystemColors.Window;
            this.disease.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.disease.ComboFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.disease.DropDownWidth = 120;
            this.disease.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disease.Location = new System.Drawing.Point(3, -2);
            this.disease.MaxDropDownItems = 8;
            this.disease.Name = "disease";
            this.disease.Padding = new System.Windows.Forms.Padding(4);
            this.disease.PopupWidth = 300;
            this.disease.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.disease.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.disease.Size = new System.Drawing.Size(177, 29);
            this.disease.TabIndex = 0;
            this.disease.Validated += new System.EventHandler(this.disease_Validated);
            // 
            // ageDiagnosis
            // 
            this.ageDiagnosis.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ageDiagnosis.Location = new System.Drawing.Point(186, 2);
            this.ageDiagnosis.Name = "ageDiagnosis";
            this.ageDiagnosis.Size = new System.Drawing.Size(48, 21);
            this.ageDiagnosis.TabIndex = 1;
            this.ageDiagnosis.Validated += new System.EventHandler(this.ageDiagnosis_Validated);
            // 
            // PMHRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.disease);
            this.Controls.Add(this.ageDiagnosis);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comments);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PMHRow";
            this.Size = new System.Drawing.Size(610, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox comments;
        private System.Windows.Forms.Button button2;
        public RiskApps3.Utilities.IntegerTextBox ageDiagnosis;
        public RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox disease;
    }
}

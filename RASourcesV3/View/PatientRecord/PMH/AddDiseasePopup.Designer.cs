namespace RiskApps3.View.PatientRecord.PMH
{
    partial class AddDiseasePopup
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.AgeTextBox = new RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox();
            this.diseaseComboBox = new RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(320, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 29);
            this.button1.TabIndex = 3;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Disease";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(257, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Age";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(3, 21);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 28);
            this.button2.TabIndex = 0;
            this.button2.Text = "X";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AgeTextBox
            // 
            this.AgeTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.AgeTextBox.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.AgeTextBox.ComboFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AgeTextBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.AgeTextBox.DropDownWidth = 64;
            this.AgeTextBox.Location = new System.Drawing.Point(242, 20);
            this.AgeTextBox.MaxDropDownItems = 8;
            this.AgeTextBox.Name = "AgeTextBox";
            this.AgeTextBox.Padding = new System.Windows.Forms.Padding(4);
            this.AgeTextBox.PopupWidth = 300;
            this.AgeTextBox.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.AgeTextBox.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.AgeTextBox.Size = new System.Drawing.Size(72, 29);
            this.AgeTextBox.TabIndex = 2;
            // 
            // diseaseComboBox
            // 
            this.diseaseComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.diseaseComboBox.BorderColor = System.Drawing.Color.LightSteelBlue;
            this.diseaseComboBox.ComboFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.diseaseComboBox.DropDownWidth = 190;
            this.diseaseComboBox.Location = new System.Drawing.Point(38, 20);
            this.diseaseComboBox.MaxDropDownItems = 8;
            this.diseaseComboBox.Name = "diseaseComboBox";
            this.diseaseComboBox.Padding = new System.Windows.Forms.Padding(4);
            this.diseaseComboBox.PopupWidth = 300;
            this.diseaseComboBox.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.diseaseComboBox.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.diseaseComboBox.Size = new System.Drawing.Size(198, 29);
            this.diseaseComboBox.TabIndex = 1;
            // 
            // AddDiseasePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(367, 54);
            this.ControlBox = false;
            this.Controls.Add(this.AgeTextBox);
            this.Controls.Add(this.diseaseComboBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddDiseasePopup";
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Gray;
            this.Load += new System.EventHandler(this.AddDiseasePopup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox diseaseComboBox;
        private RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox AgeTextBox;
    }
}
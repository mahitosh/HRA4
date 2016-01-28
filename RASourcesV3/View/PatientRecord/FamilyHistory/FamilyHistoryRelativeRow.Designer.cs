namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    partial class FamilyHistoryRelativeRow
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.diseaseDetails1 = new System.Windows.Forms.Button();
            this.diseaseDetails2 = new System.Windows.Forms.Button();
            this.diseaseDetails3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Relationship";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "Alive",
            "Dead"});
            this.comboBox1.Location = new System.Drawing.Point(177, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(72, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Validated += new System.EventHandler(this.comboBox1_Validated);
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "",
            "Brain Cancer",
            "Breast Cancer",
            "Cervical Cancer",
            "Colon or Rectal Cancer",
            "Hodgkins Lymphoma",
            "Kidney or Bladder Cancer",
            "Leukemia",
            "Liver Cancer",
            "Lung Cancer",
            "Lymphoma (Non-Hodgkins)",
            "Melanoma",
            "Other",
            "Ovarian Cancer",
            "Pancreatic Cancer",
            "Prostate Cancer",
            "Sarcoma",
            "Stomach Cancer",
            "Thyroid Cancer",
            "Uterine Cancer"});
            this.comboBox2.Location = new System.Drawing.Point(268, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(155, 21);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.Validated += new System.EventHandler(this.comboBox2_Validated);
            // 
            // comboBox6
            // 
            this.comboBox6.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "",
            "Brain Cancer",
            "Breast Cancer",
            "Cervical Cancer",
            "Colon or Rectal Cancer",
            "Hodgkins Lymphoma",
            "Kidney or Bladder Cancer",
            "Leukemia",
            "Liver Cancer",
            "Lung Cancer",
            "Lymphoma (Non-Hodgkins)",
            "Melanoma",
            "Other",
            "Ovarian Cancer",
            "Pancreatic Cancer",
            "Prostate Cancer",
            "Sarcoma",
            "Stomach Cancer",
            "Thyroid Cancer",
            "Uterine Cancer"});
            this.comboBox6.Location = new System.Drawing.Point(506, 12);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(155, 21);
            this.comboBox6.TabIndex = 13;
            this.comboBox6.Validated += new System.EventHandler(this.comboBox6_Validated);
            // 
            // comboBox8
            // 
            this.comboBox8.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Items.AddRange(new object[] {
            "",
            "Brain Cancer",
            "Breast Cancer",
            "Cervical Cancer",
            "Colon or Rectal Cancer",
            "Hodgkins Lymphoma",
            "Kidney or Bladder Cancer",
            "Leukemia",
            "Liver Cancer",
            "Lung Cancer",
            "Lymphoma (Non-Hodgkins)",
            "Melanoma",
            "Other",
            "Ovarian Cancer",
            "Pancreatic Cancer",
            "Prostate Cancer",
            "Sarcoma",
            "Stomach Cancer",
            "Thyroid Cancer",
            "Uterine Cancer"});
            this.comboBox8.Location = new System.Drawing.Point(739, 12);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(155, 21);
            this.comboBox8.TabIndex = 17;
            this.comboBox8.Validated += new System.EventHandler(this.comboBox8_Validated);
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(968, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 31);
            this.button1.TabIndex = 21;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(40, 20);
            this.textBox1.TabIndex = 22;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox4_KeyPress);
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(426, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(40, 20);
            this.textBox2.TabIndex = 23;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox4_KeyPress);
            this.textBox2.Validated += new System.EventHandler(this.comboBox2_Validated);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(663, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(40, 20);
            this.textBox3.TabIndex = 24;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox4_KeyPress);
            this.textBox3.Validated += new System.EventHandler(this.comboBox6_Validated);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(896, 12);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(40, 20);
            this.textBox4.TabIndex = 25;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBox4_KeyPress);
            this.textBox4.Validated += new System.EventHandler(this.comboBox8_Validated);
            // 
            // diseaseDetails1
            // 
            this.diseaseDetails1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseaseDetails1.Location = new System.Drawing.Point(468, 11);
            this.diseaseDetails1.Name = "diseaseDetails1";
            this.diseaseDetails1.Size = new System.Drawing.Size(27, 23);
            this.diseaseDetails1.TabIndex = 26;
            this.diseaseDetails1.Text = "...";
            this.diseaseDetails1.UseVisualStyleBackColor = true;
            this.diseaseDetails1.Click += new System.EventHandler(this.diseaseDetails1_Click);
            // 
            // diseaseDetails2
            // 
            this.diseaseDetails2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseaseDetails2.Location = new System.Drawing.Point(704, 11);
            this.diseaseDetails2.Name = "diseaseDetails2";
            this.diseaseDetails2.Size = new System.Drawing.Size(27, 23);
            this.diseaseDetails2.TabIndex = 27;
            this.diseaseDetails2.Text = "...";
            this.diseaseDetails2.UseVisualStyleBackColor = true;
            this.diseaseDetails2.Click += new System.EventHandler(this.diseaseDetails2_Click);
            // 
            // diseaseDetails3
            // 
            this.diseaseDetails3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseaseDetails3.Location = new System.Drawing.Point(937, 11);
            this.diseaseDetails3.Name = "diseaseDetails3";
            this.diseaseDetails3.Size = new System.Drawing.Size(27, 23);
            this.diseaseDetails3.TabIndex = 28;
            this.diseaseDetails3.Text = "...";
            this.diseaseDetails3.UseVisualStyleBackColor = true;
            this.diseaseDetails3.Click += new System.EventHandler(this.diseaseDetails3_Click);
            // 
            // FamilyHistoryRelativeRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.diseaseDetails3);
            this.Controls.Add(this.diseaseDetails2);
            this.Controls.Add(this.diseaseDetails1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox8);
            this.Controls.Add(this.comboBox6);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Name = "FamilyHistoryRelativeRow";
            this.Size = new System.Drawing.Size(1010, 44);
            this.Load += new System.EventHandler(this.FamilyHistoryRelativeRow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button diseaseDetails1;
        private System.Windows.Forms.Button diseaseDetails2;
        private System.Windows.Forms.Button diseaseDetails3;
    }
}

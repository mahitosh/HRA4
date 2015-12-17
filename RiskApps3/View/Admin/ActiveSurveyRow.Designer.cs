namespace RiskApps3.View.Admin
{
    partial class ActiveSurveyRow
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
            this.surveyNameLabel = new System.Windows.Forms.Label();
            this.surveyIdLabel = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // surveyNameLabel
            // 
            this.surveyNameLabel.Location = new System.Drawing.Point(80, 4);
            this.surveyNameLabel.Name = "surveyNameLabel";
            this.surveyNameLabel.Size = new System.Drawing.Size(200, 13);
            this.surveyNameLabel.TabIndex = 0;
            this.surveyNameLabel.Text = "Survey Name";
            // 
            // surveyIdLabel
            // 
            this.surveyIdLabel.Location = new System.Drawing.Point(9, 4);
            this.surveyIdLabel.Name = "surveyIdLabel";
            this.surveyIdLabel.Size = new System.Drawing.Size(51, 13);
            this.surveyIdLabel.TabIndex = 1;
            this.surveyIdLabel.Text = "SurveyID";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(280, 2);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(56, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // ActiveSurveyRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.surveyIdLabel);
            this.Controls.Add(this.surveyNameLabel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ActiveSurveyRow";
            this.Size = new System.Drawing.Size(370, 18);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label surveyNameLabel;
        private System.Windows.Forms.Label surveyIdLabel;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

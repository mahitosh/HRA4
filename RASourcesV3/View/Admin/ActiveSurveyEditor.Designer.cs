namespace RiskApps3.View.Admin
{
    partial class ActiveSurveyEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiveSurveyEditor));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.loadCountLabel = new System.Windows.Forms.Label();
            this.labelSurveyID = new System.Windows.Forms.Label();
            this.labelSurveyName = new System.Windows.Forms.Label();
            this.labelSurveyActive = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(49, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 23);
            this.progressBar1.TabIndex = 2;
            this.progressBar1.Visible = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 515);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // loadCountLabel
            // 
            this.loadCountLabel.AutoSize = true;
            this.loadCountLabel.Location = new System.Drawing.Point(339, 13);
            this.loadCountLabel.Name = "loadCountLabel";
            this.loadCountLabel.Size = new System.Drawing.Size(13, 13);
            this.loadCountLabel.TabIndex = 4;
            this.loadCountLabel.Text = "0";
            // 
            // labelSurveyID
            // 
            this.labelSurveyID.AutoSize = true;
            this.labelSurveyID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSurveyID.Location = new System.Drawing.Point(12, 31);
            this.labelSurveyID.Name = "labelSurveyID";
            this.labelSurveyID.Size = new System.Drawing.Size(63, 13);
            this.labelSurveyID.TabIndex = 0;
            this.labelSurveyID.Text = "Survey ID";
            // 
            // labelSurveyName
            // 
            this.labelSurveyName.AutoSize = true;
            this.labelSurveyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSurveyName.Location = new System.Drawing.Point(85, 31);
            this.labelSurveyName.Name = "labelSurveyName";
            this.labelSurveyName.Size = new System.Drawing.Size(82, 13);
            this.labelSurveyName.TabIndex = 1;
            this.labelSurveyName.Text = "Survey Name";
            // 
            // labelSurveyActive
            // 
            this.labelSurveyActive.AutoSize = true;
            this.labelSurveyActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSurveyActive.Location = new System.Drawing.Point(280, 31);
            this.labelSurveyActive.Name = "labelSurveyActive";
            this.labelSurveyActive.Size = new System.Drawing.Size(108, 13);
            this.labelSurveyActive.TabIndex = 2;
            this.labelSurveyActive.Text = "Check / Uncheck";
            // 
            // ActiveSurveyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(400, 562);
            this.Controls.Add(this.labelSurveyActive);
            this.Controls.Add(this.labelSurveyName);
            this.Controls.Add(this.labelSurveyID);
            this.Controls.Add(this.loadCountLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActiveSurveyEditor";
            this.Text = "Active Survey Editor";
            this.Load += new System.EventHandler(this.ActiveSurveyEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label loadCountLabel;
        private System.Windows.Forms.Label labelSurveyID;
        private System.Windows.Forms.Label labelSurveyName;
        private System.Windows.Forms.Label labelSurveyActive;

    }
}
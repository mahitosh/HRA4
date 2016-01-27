namespace RiskApps3.View.Admin
{
    partial class QueueParameterEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueueParameterEditor));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.idLabel = new System.Windows.Forms.Label();
            this.rulesLabel = new System.Windows.Forms.Label();
            this.parameterLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loadCountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(77, 6);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(284, 23);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.idLabel.Location = new System.Drawing.Point(8, 31);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(20, 13);
            this.idLabel.TabIndex = 4;
            this.idLabel.Text = "ID";
            // 
            // rulesLabel
            // 
            this.rulesLabel.AutoSize = true;
            this.rulesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rulesLabel.Location = new System.Drawing.Point(42, 31);
            this.rulesLabel.Name = "rulesLabel";
            this.rulesLabel.Size = new System.Drawing.Size(39, 13);
            this.rulesLabel.TabIndex = 5;
            this.rulesLabel.Text = "Rules";
            // 
            // parameterLabel
            // 
            this.parameterLabel.AutoSize = true;
            this.parameterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parameterLabel.Location = new System.Drawing.Point(168, 31);
            this.parameterLabel.Name = "parameterLabel";
            this.parameterLabel.Size = new System.Drawing.Size(64, 13);
            this.parameterLabel.TabIndex = 6;
            this.parameterLabel.Text = "Parameter";
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.Location = new System.Drawing.Point(350, 31);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(63, 13);
            this.valueLabel.TabIndex = 7;
            this.valueLabel.Text = "Threshold";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(438, 515);
            this.flowLayoutPanel1.TabIndex = 8;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // loadCountLabel
            // 
            this.loadCountLabel.AutoSize = true;
            this.loadCountLabel.Location = new System.Drawing.Point(371, 13);
            this.loadCountLabel.Name = "loadCountLabel";
            this.loadCountLabel.Size = new System.Drawing.Size(13, 13);
            this.loadCountLabel.TabIndex = 5;
            this.loadCountLabel.Text = "0";
            // 
            // QueueParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 562);
            this.Controls.Add(this.loadCountLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.valueLabel);
            this.Controls.Add(this.parameterLabel);
            this.Controls.Add(this.rulesLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "QueueParameterEditor";
            this.Text = "QueueParameterEditor";
            this.Load += new System.EventHandler(this.QueueParameterEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label rulesLabel;
        private System.Windows.Forms.Label parameterLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label loadCountLabel;
    }
}
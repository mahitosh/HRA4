namespace RiskApps3.View.PatientRecord
{
    partial class DocumentUploadView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentUploadView));
            this.fileUploadControl1 = new RiskApps3.View.PatientRecord.FileUploadControl();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.relativeHeader1 = new RiskApps3.View.PatientRecord.FamilyHistory.ShortRelativeHeader();
            this.SuspendLayout();
            // 
            // fileUploadControl1
            // 
            this.fileUploadControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileUploadControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fileUploadControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileUploadControl1.Location = new System.Drawing.Point(0, 32);
            this.fileUploadControl1.MinimumSize = new System.Drawing.Size(40, 40);
            this.fileUploadControl1.Name = "fileUploadControl1";
            this.fileUploadControl1.Size = new System.Drawing.Size(343, 561);
            this.fileUploadControl1.TabIndex = 0;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(144, 143);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(44, 37);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 6;
            this.loadingCircle1.Text = "loadingCircle2";
            this.loadingCircle1.Visible = false;
            // 
            // relativeHeader1
            // 
            this.relativeHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.relativeHeader1.Location = new System.Drawing.Point(12, 3);
            this.relativeHeader1.Name = "relativeHeader1";
            this.relativeHeader1.Size = new System.Drawing.Size(317, 23);
            this.relativeHeader1.TabIndex = 7;
            // 
            // DocumentUploadView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 592);
            this.Controls.Add(this.relativeHeader1);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.fileUploadControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentUploadView";
            this.Text = "Documents";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DocumentUploadView_FormClosing);
            this.Load += new System.EventHandler(this.DocumentUploadView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FileUploadControl fileUploadControl1;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private RiskApps3.View.PatientRecord.FamilyHistory.ShortRelativeHeader relativeHeader1;
    }
}
namespace RiskApps3.View.PatientRecord.Communication
{
    partial class TaskView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskView));
            this.patientRecordHeader1 = new RiskApps3.View.PatientRecord.PatientRecordHeader();
            this.TaskUserControl = new RiskApps3.View.PatientRecord.Communication.TaskViewUC();
            this.SuspendLayout();
            // 
            // patientRecordHeader1
            // 
            this.patientRecordHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patientRecordHeader1.Collapsed = false;
            this.patientRecordHeader1.Location = new System.Drawing.Point(12, 2);
            this.patientRecordHeader1.Name = "patientRecordHeader1";
            this.patientRecordHeader1.Size = new System.Drawing.Size(921, 74);
            this.patientRecordHeader1.TabIndex = 19;
            // 
            // TaskUserControl
            // 
            this.TaskUserControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TaskUserControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TaskUserControl.Location = new System.Drawing.Point(-2, 62);
            this.TaskUserControl.Name = "TaskUserControl";
            this.TaskUserControl.Size = new System.Drawing.Size(947, 603);
            this.TaskUserControl.TabIndex = 20;
            this.TaskUserControl.Task = null;
            // 
            // TaskView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 662);
            this.Controls.Add(this.TaskUserControl);
            this.Controls.Add(this.patientRecordHeader1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Task";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskView_FormClosing);
            this.Load += new System.EventHandler(this.TaskView_Load);
            this.VisibleChanged += new System.EventHandler(this.TaskView_VisibleChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private PatientRecordHeader patientRecordHeader1;
        private TaskViewUC TaskUserControl;
    }
}
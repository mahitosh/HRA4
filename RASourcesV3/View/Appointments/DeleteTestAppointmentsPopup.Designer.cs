namespace RiskApps3.View.Appointments
{
    partial class DeleteTestAppointmentsPopup
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
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle1 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle2 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle3 = new BrightIdeasSoftware.HeaderStateStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteTestAppointmentsPopup));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DeleteColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.progressLabel = new System.Windows.Forms.Label();
            this.refreshButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.fetchBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.deleteBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.headerFormatStyleData = new BrightIdeasSoftware.HeaderFormatStyle();
            this.button1 = new System.Windows.Forms.Button();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(110, 32);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(235, 16);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // DeleteColumn
            // 
            this.DeleteColumn.AspectName = "marked";
            this.DeleteColumn.DisplayIndex = 6;
            this.DeleteColumn.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteColumn.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.DeleteColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DeleteColumn.IsVisible = false;
            this.DeleteColumn.Text = "Delete";
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(25, 32);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(67, 16);
            this.progressLabel.TabIndex = 18;
            this.progressLabel.Text = "Working...";
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(557, 28);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(107, 25);
            this.refreshButton.TabIndex = 19;
            this.refreshButton.Text = "Refresh List";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(451, 33);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(226, 49);
            this.deleteButton.TabIndex = 22;
            this.deleteButton.Text = "Delete selected records";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // fetchBackgroundWorker
            // 
            this.fetchBackgroundWorker.WorkerReportsProgress = true;
            this.fetchBackgroundWorker.WorkerSupportsCancellation = true;
            this.fetchBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fetchBackgroundWorker_DoWork);
            this.fetchBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.fetchBackgroundWorker_ProgressChanged);
            this.fetchBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fetchBackgroundWorker_RunWorkerCompleted);
            // 
            // deleteBackgroundWorker
            // 
            this.deleteBackgroundWorker.WorkerReportsProgress = true;
            this.deleteBackgroundWorker.WorkerSupportsCancellation = true;
            this.deleteBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.deleteBackgroundWorker_DoWork);
            this.deleteBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.deleteBackgroundWorker_RunWorkerCompleted);
            // 
            // headerFormatStyleData
            // 
            headerStateStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            headerStateStyle1.ForeColor = System.Drawing.Color.White;
            this.headerFormatStyleData.Hot = headerStateStyle1;
            headerStateStyle2.BackColor = System.Drawing.Color.Black;
            headerStateStyle2.ForeColor = System.Drawing.Color.Gainsboro;
            this.headerFormatStyleData.Normal = headerStateStyle2;
            headerStateStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            headerStateStyle3.ForeColor = System.Drawing.Color.White;
            headerStateStyle3.FrameColor = System.Drawing.Color.WhiteSmoke;
            headerStateStyle3.FrameWidth = 2F;
            this.headerFormatStyleData.Pressed = headerStateStyle3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(6, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 49);
            this.button1.TabIndex = 24;
            this.button1.Text = "Mark selected as NOT test patients";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvColumn2);
            this.objectListView1.AllColumns.Add(this.olvColumn3);
            this.objectListView1.AllColumns.Add(this.olvColumn4);
            this.objectListView1.AllColumns.Add(this.olvColumn5);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.CheckBoxes = true;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.GridLines = true;
            this.objectListView1.HeaderUsesThemes = false;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(6, 59);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(668, 478);
            this.objectListView1.TabIndex = 25;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseFiltering = true;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.Filter += new System.EventHandler<BrightIdeasSoftware.FilterEventArgs>(this.objectListView1_Filter);
            this.objectListView1.SelectionChanged += new System.EventHandler(this.objectListView1_SelectionChanged);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "unitnum";
            this.olvColumn2.Text = "MRN";
            this.olvColumn2.Width = 113;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "patientName";
            this.olvColumn3.Text = "Name";
            this.olvColumn3.Width = 197;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "DOB";
            this.olvColumn4.Text = "DOB";
            this.olvColumn4.Width = 116;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "apptdatetime";
            this.olvColumn5.Text = "Appt Date & Time";
            this.olvColumn5.Width = 183;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(424, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 25);
            this.button2.TabIndex = 28;
            this.button2.Text = "Select All";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.objectListView1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.progressLabel);
            this.groupBox1.Controls.Add(this.refreshButton);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 543);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "First select the appropriate records.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.deleteButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 561);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(683, 88);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Then choose the action";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(331, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "OR";
            // 
            // DeleteTestAppointmentsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 661);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DeleteTestAppointmentsPopup";
            this.Text = "Delete Test Appointments";
            this.Load += new System.EventHandler(this.DeleteTestAppointmentsPopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        public BrightIdeasSoftware.OLVColumn DeleteColumn;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button deleteButton;
        private System.ComponentModel.BackgroundWorker fetchBackgroundWorker;
        private System.ComponentModel.BackgroundWorker deleteBackgroundWorker;
        private BrightIdeasSoftware.HeaderFormatStyle headerFormatStyleData;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
    }
}
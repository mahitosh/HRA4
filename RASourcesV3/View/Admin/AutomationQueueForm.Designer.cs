namespace RiskApps3.View.Admin
{
    partial class AutomationQueueForm
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
            this.components = new System.ComponentModel.Container();
            this.fastDataListView1 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumnApptID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnUnitnum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDOB = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnApptDateTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRiskDataCompleted = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.numberLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.refreshButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastDataListView1
            // 
            this.fastDataListView1.AllColumns.Add(this.olvColumnApptID);
            this.fastDataListView1.AllColumns.Add(this.olvColumnUnitnum);
            this.fastDataListView1.AllColumns.Add(this.olvColumnDOB);
            this.fastDataListView1.AllColumns.Add(this.olvColumnApptDateTime);
            this.fastDataListView1.AllColumns.Add(this.olvColumnRiskDataCompleted);
            this.fastDataListView1.AllowColumnReorder = true;
            this.fastDataListView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fastDataListView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fastDataListView1.CheckBoxes = false;
            this.fastDataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnApptID,
            this.olvColumnUnitnum,
            this.olvColumnDOB,
            this.olvColumnApptDateTime,
            this.olvColumnRiskDataCompleted});
            this.fastDataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListView1.DataSource = null;
            this.fastDataListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListView1.FullRowSelect = true;
            this.fastDataListView1.GridLines = true;
            this.fastDataListView1.HeaderWordWrap = true;
            this.fastDataListView1.HideSelection = false;
            this.fastDataListView1.Location = new System.Drawing.Point(30, 79);
            this.fastDataListView1.Name = "fastDataListView1";
            this.fastDataListView1.OwnerDraw = true;
            this.fastDataListView1.SelectedColumnTint = System.Drawing.Color.Transparent;
            this.fastDataListView1.ShowCommandMenuOnRightClick = true;
            this.fastDataListView1.ShowGroups = false;
            this.fastDataListView1.ShowItemToolTips = true;
            this.fastDataListView1.Size = new System.Drawing.Size(818, 426);
            this.fastDataListView1.TabIndex = 89;
            this.fastDataListView1.UseCompatibleStateImageBehavior = false;
            this.fastDataListView1.UseHotItem = true;
            this.fastDataListView1.UseOverlays = false;
            this.fastDataListView1.UseTranslucentHotItem = true;
            this.fastDataListView1.View = System.Windows.Forms.View.Details;
            this.fastDataListView1.VirtualMode = true;
            this.fastDataListView1.SelectionChanged += new System.EventHandler(this.fastDataListView1_SelectionChanged);
            // 
            // olvColumnApptID
            // 
            this.olvColumnApptID.AspectName = "apptid";
            this.olvColumnApptID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnApptID.Text = "ApptID";
            this.olvColumnApptID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnApptID.Width = 96;
            // 
            // olvColumnUnitnum
            // 
            this.olvColumnUnitnum.AspectName = "unitnum";
            this.olvColumnUnitnum.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnUnitnum.Text = "MRN";
            this.olvColumnUnitnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnUnitnum.Width = 144;
            // 
            // olvColumnDOB
            // 
            this.olvColumnDOB.AspectName = "dob";
            this.olvColumnDOB.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnDOB.Text = "DOB";
            this.olvColumnDOB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnDOB.Width = 141;
            // 
            // olvColumnApptDateTime
            // 
            this.olvColumnApptDateTime.AspectName = "apptdatetime";
            this.olvColumnApptDateTime.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnApptDateTime.Text = "Appt DateTime";
            this.olvColumnApptDateTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnApptDateTime.Width = 215;
            // 
            // olvColumnRiskDataCompleted
            // 
            this.olvColumnRiskDataCompleted.AspectName = "riskdatacompleted";
            this.olvColumnRiskDataCompleted.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnRiskDataCompleted.Text = "Completed";
            this.olvColumnRiskDataCompleted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnRiskDataCompleted.Width = 217;
            // 
            // numberLabel
            // 
            this.numberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numberLabel.AutoSize = true;
            this.numberLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberLabel.Location = new System.Drawing.Point(236, 528);
            this.numberLabel.Name = "numberLabel";
            this.numberLabel.Size = new System.Drawing.Size(16, 16);
            this.numberLabel.TabIndex = 91;
            this.numberLabel.Text = "0";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 528);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 16);
            this.label1.TabIndex = 90;
            this.label1.Text = "Number of patients in queue: ";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(396, 261);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(87, 62);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 92;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(402, 521);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 31);
            this.refreshButton.TabIndex = 93;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(773, 521);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 31);
            this.closeButton.TabIndex = 94;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // AutomationQueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 585);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.numberLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fastDataListView1);
            this.Name = "AutomationQueueForm";
            this.Text = "AutomationQueueForm";
            this.Load += new System.EventHandler(this.AutomationQueueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastDataListView fastDataListView1;
        private System.Windows.Forms.Label numberLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private BrightIdeasSoftware.OLVColumn olvColumnApptID;
        private BrightIdeasSoftware.OLVColumn olvColumnUnitnum;
        private BrightIdeasSoftware.OLVColumn olvColumnDOB;
        private BrightIdeasSoftware.OLVColumn olvColumnApptDateTime;
        private BrightIdeasSoftware.OLVColumn olvColumnRiskDataCompleted;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button closeButton;
    }
}
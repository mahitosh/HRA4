namespace RiskApps3
{
    partial class PrintQueueForm
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
            this.refreshButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.btnPrintLetters = new System.Windows.Forms.Button();
            this.btnPrintSelectedLetters = new System.Windows.Forms.Button();
            this.lblItemCountSelected = new System.Windows.Forms.Label();
            this.lblItemCountLetters = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.includePrintedCheckBox = new System.Windows.Forms.CheckBox();
            this.populateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.fastDataListView1 = new BrightIdeasSoftware.FastDataListView();
            this.mrnOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.patientNameOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.dobOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.apptDateOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.documentOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.lastPrintedOlvColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.printSelectedBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(842, 483);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 31);
            this.refreshButton.TabIndex = 0;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(842, 521);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 31);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.closeButton_MouseClick);
            // 
            // btnPrintLetters
            // 
            this.btnPrintLetters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintLetters.Location = new System.Drawing.Point(507, 483);
            this.btnPrintLetters.Name = "btnPrintLetters";
            this.btnPrintLetters.Size = new System.Drawing.Size(207, 31);
            this.btnPrintLetters.TabIndex = 17;
            this.btnPrintLetters.Text = "Print All Documents";
            this.btnPrintLetters.UseVisualStyleBackColor = true;
            this.btnPrintLetters.Click += new System.EventHandler(this.btnPrintLetters_Click);
            // 
            // btnPrintSelectedLetters
            // 
            this.btnPrintSelectedLetters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintSelectedLetters.Location = new System.Drawing.Point(294, 483);
            this.btnPrintSelectedLetters.Name = "btnPrintSelectedLetters";
            this.btnPrintSelectedLetters.Size = new System.Drawing.Size(207, 31);
            this.btnPrintSelectedLetters.TabIndex = 21;
            this.btnPrintSelectedLetters.Text = "Print Selected Documents";
            this.btnPrintSelectedLetters.UseVisualStyleBackColor = true;
            this.btnPrintSelectedLetters.Click += new System.EventHandler(this.btnPrintSelectedLetters_Click);
            // 
            // lblItemCountSelected
            // 
            this.lblItemCountSelected.AutoSize = true;
            this.lblItemCountSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCountSelected.Location = new System.Drawing.Point(30, 503);
            this.lblItemCountSelected.Name = "lblItemCountSelected";
            this.lblItemCountSelected.Size = new System.Drawing.Size(155, 15);
            this.lblItemCountSelected.TabIndex = 23;
            this.lblItemCountSelected.Text = "0 Documents Selected.";
            // 
            // lblItemCountLetters
            // 
            this.lblItemCountLetters.AutoSize = true;
            this.lblItemCountLetters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCountLetters.Location = new System.Drawing.Point(30, 483);
            this.lblItemCountLetters.Name = "lblItemCountLetters";
            this.lblItemCountLetters.Size = new System.Drawing.Size(138, 15);
            this.lblItemCountLetters.TabIndex = 22;
            this.lblItemCountLetters.Text = "0 Documents Listed.";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(448, 530);
            this.statusLabel.MaximumSize = new System.Drawing.Size(380, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(72, 13);
            this.statusLabel.TabIndex = 86;
            this.statusLabel.Text = "statusLabel";
            this.statusLabel.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(367, 521);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 31);
            this.cancelButton.TabIndex = 85;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(33, 521);
            this.progressBar.MarqueeAnimationSpeed = 40;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(328, 31);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 84;
            this.progressBar.Visible = false;
            // 
            // includePrintedCheckBox
            // 
            this.includePrintedCheckBox.AutoSize = true;
            this.includePrintedCheckBox.Location = new System.Drawing.Point(699, 14);
            this.includePrintedCheckBox.Name = "includePrintedCheckBox";
            this.includePrintedCheckBox.Size = new System.Drawing.Size(205, 17);
            this.includePrintedCheckBox.TabIndex = 87;
            this.includePrintedCheckBox.Text = "Include Previously Printed Documents";
            this.includePrintedCheckBox.UseVisualStyleBackColor = true;
            this.includePrintedCheckBox.CheckedChanged += new System.EventHandler(this.includePrintedCheckBox_CheckedChanged);
            // 
            // populateBackgroundWorker
            // 
            this.populateBackgroundWorker.WorkerReportsProgress = true;
            this.populateBackgroundWorker.WorkerSupportsCancellation = true;
            this.populateBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.populateBackgroundWorker_DoWork);
            this.populateBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.populateBackgroundWorker_ProgressChanged);
            this.populateBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.populateBackgroundWorker_RunWorkerCompleted);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(421, 194);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(87, 62);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 89;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // fastDataListView1
            // 
            this.fastDataListView1.AllColumns.Add(this.mrnOlvColumn);
            this.fastDataListView1.AllColumns.Add(this.patientNameOlvColumn);
            this.fastDataListView1.AllColumns.Add(this.dobOlvColumn);
            this.fastDataListView1.AllColumns.Add(this.apptDateOlvColumn);
            this.fastDataListView1.AllColumns.Add(this.documentOlvColumn);
            this.fastDataListView1.AllColumns.Add(this.lastPrintedOlvColumn);
            this.fastDataListView1.AllowColumnReorder = true;
            this.fastDataListView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fastDataListView1.CheckBoxes = false;
            this.fastDataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.mrnOlvColumn,
            this.patientNameOlvColumn,
            this.dobOlvColumn,
            this.apptDateOlvColumn,
            this.documentOlvColumn,
            this.lastPrintedOlvColumn});
            this.fastDataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListView1.DataSource = null;
            this.fastDataListView1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListView1.FullRowSelect = true;
            this.fastDataListView1.GridLines = true;
            this.fastDataListView1.HeaderWordWrap = true;
            this.fastDataListView1.HideSelection = false;
            this.fastDataListView1.Location = new System.Drawing.Point(12, 40);
            this.fastDataListView1.Name = "fastDataListView1";
            this.fastDataListView1.OwnerDraw = true;
            this.fastDataListView1.SelectedColumnTint = System.Drawing.Color.Transparent;
            this.fastDataListView1.ShowCommandMenuOnRightClick = true;
            this.fastDataListView1.ShowGroups = false;
            this.fastDataListView1.ShowItemToolTips = true;
            this.fastDataListView1.Size = new System.Drawing.Size(904, 426);
            this.fastDataListView1.TabIndex = 88;
            this.fastDataListView1.UseCompatibleStateImageBehavior = false;
            this.fastDataListView1.UseHotItem = true;
            this.fastDataListView1.UseOverlays = false;
            this.fastDataListView1.UseTranslucentHotItem = true;
            this.fastDataListView1.View = System.Windows.Forms.View.Details;
            this.fastDataListView1.VirtualMode = true;
            this.fastDataListView1.SelectionChanged += new System.EventHandler(this.fastDataListView1_SelectionChanged);
            // 
            // mrnOlvColumn
            // 
            this.mrnOlvColumn.AspectName = "unitnum";
            this.mrnOlvColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mrnOlvColumn.Text = "MRN";
            this.mrnOlvColumn.Width = 90;
            // 
            // patientNameOlvColumn
            // 
            this.patientNameOlvColumn.AspectName = "patientName";
            this.patientNameOlvColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.patientNameOlvColumn.Text = "Patient Name";
            this.patientNameOlvColumn.Width = 275;
            // 
            // dobOlvColumn
            // 
            this.dobOlvColumn.AspectName = "dob";
            this.dobOlvColumn.Text = "DOB";
            this.dobOlvColumn.Width = 80;
            // 
            // apptDateOlvColumn
            // 
            this.apptDateOlvColumn.AspectName = "apptDate";
            this.apptDateOlvColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.apptDateOlvColumn.Text = "Appt Date";
            this.apptDateOlvColumn.Width = 80;
            // 
            // documentOlvColumn
            // 
            this.documentOlvColumn.AspectName = "documentName";
            this.documentOlvColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.documentOlvColumn.Text = "Document Name";
            this.documentOlvColumn.Width = 275;
            // 
            // lastPrintedOlvColumn
            // 
            this.lastPrintedOlvColumn.AspectName = "lastPrinted";
            this.lastPrintedOlvColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lastPrintedOlvColumn.Text = "Last Printed";
            this.lastPrintedOlvColumn.Width = 100;
            // 
            // printSelectedBackgroundWorker
            // 
            this.printSelectedBackgroundWorker.WorkerReportsProgress = true;
            this.printSelectedBackgroundWorker.WorkerSupportsCancellation = true;
            this.printSelectedBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.printSelectedBackgroundWorker_DoWork);
            this.printSelectedBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.printSelectedBackgroundWorker_ProgressChanged);
            this.printSelectedBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.printSelectedBackgroundWorker_RunWorkerCompleted);
            // 
            // PrintQueueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 570);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.fastDataListView1);
            this.Controls.Add(this.includePrintedCheckBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblItemCountSelected);
            this.Controls.Add(this.lblItemCountLetters);
            this.Controls.Add(this.btnPrintSelectedLetters);
            this.Controls.Add(this.btnPrintLetters);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.refreshButton);
            this.Name = "PrintQueueForm";
            this.Text = "PrintQueueForm";
            this.Load += new System.EventHandler(this.PrintQueueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button btnPrintLetters;
        private System.Windows.Forms.Button btnPrintSelectedLetters;
        internal System.Windows.Forms.Label lblItemCountSelected;
        internal System.Windows.Forms.Label lblItemCountLetters;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.CheckBox includePrintedCheckBox;
        private System.ComponentModel.BackgroundWorker populateBackgroundWorker;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private BrightIdeasSoftware.FastDataListView fastDataListView1;
        private BrightIdeasSoftware.OLVColumn mrnOlvColumn;
        private BrightIdeasSoftware.OLVColumn patientNameOlvColumn;
        private BrightIdeasSoftware.OLVColumn dobOlvColumn;
        private BrightIdeasSoftware.OLVColumn apptDateOlvColumn;
        private BrightIdeasSoftware.OLVColumn documentOlvColumn;
        private BrightIdeasSoftware.OLVColumn lastPrintedOlvColumn;
        private System.ComponentModel.BackgroundWorker printSelectedBackgroundWorker;
    }
}
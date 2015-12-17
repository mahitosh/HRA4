namespace RiskApps3.View.Admin
{
    partial class AuditReportsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuditReportsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mrnTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.runButton = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.auditUnitnumV3tabPage = new System.Windows.Forms.TabPage();
            this.loadingCircleAuditUnitnumAcessV3 = new MRG.Controls.UI.LoadingCircle();
            this.fastDataListViewAuditUnitnumAccessV3 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumn4TimeStamp = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2UserName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1unitnum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6apptID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnStoredProcedure = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnHraAttributeValueList = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnRelativeID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnHraObject = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.auditUnitnumV2tabPage = new System.Windows.Forms.TabPage();
            this.loadingCircleAuditUnitnumAccessV2 = new MRG.Controls.UI.LoadingCircle();
            this.fastDataListViewAuditUnitnumAccessV2 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumnStartTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnEndTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCreatedBy = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMachineName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnUnitnum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnApptid = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMessage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnApplication = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnTable = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnField = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFieldMeaning = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnOldValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnNewValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.groupBox1.SuspendLayout();
            this.auditUnitnumV3tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListViewAuditUnitnumAccessV3)).BeginInit();
            this.auditUnitnumV2tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListViewAuditUnitnumAccessV2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.mrnTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.runButton);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 82);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // mrnTextBox
            // 
            this.mrnTextBox.Location = new System.Drawing.Point(22, 40);
            this.mrnTextBox.Name = "mrnTextBox";
            this.mrnTextBox.Size = new System.Drawing.Size(168, 20);
            this.mrnTextBox.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Medical Record Number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(879, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Date";
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start Date";
            this.label1.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(640, 41);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.Visible = false;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(205, 37);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(110, 23);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run Report";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(882, 41);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(99, 20);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // auditUnitnumV3tabPage
            // 
            this.auditUnitnumV3tabPage.Controls.Add(this.loadingCircleAuditUnitnumAcessV3);
            this.auditUnitnumV3tabPage.Controls.Add(this.fastDataListViewAuditUnitnumAccessV3);
            this.auditUnitnumV3tabPage.Location = new System.Drawing.Point(4, 22);
            this.auditUnitnumV3tabPage.Name = "auditUnitnumV3tabPage";
            this.auditUnitnumV3tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.auditUnitnumV3tabPage.Size = new System.Drawing.Size(996, 504);
            this.auditUnitnumV3tabPage.TabIndex = 2;
            this.auditUnitnumV3tabPage.Text = "Audit V3 Access";
            this.auditUnitnumV3tabPage.UseVisualStyleBackColor = true;
            // 
            // loadingCircleAuditUnitnumAcessV3
            // 
            this.loadingCircleAuditUnitnumAcessV3.Active = false;
            this.loadingCircleAuditUnitnumAcessV3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircleAuditUnitnumAcessV3.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircleAuditUnitnumAcessV3.Color = System.Drawing.SystemColors.ControlDark;
            this.loadingCircleAuditUnitnumAcessV3.Enabled = false;
            this.loadingCircleAuditUnitnumAcessV3.InnerCircleRadius = 5;
            this.loadingCircleAuditUnitnumAcessV3.Location = new System.Drawing.Point(401, 191);
            this.loadingCircleAuditUnitnumAcessV3.Name = "loadingCircleAuditUnitnumAcessV3";
            this.loadingCircleAuditUnitnumAcessV3.NumberSpoke = 12;
            this.loadingCircleAuditUnitnumAcessV3.OuterCircleRadius = 11;
            this.loadingCircleAuditUnitnumAcessV3.RotationSpeed = 100;
            this.loadingCircleAuditUnitnumAcessV3.Size = new System.Drawing.Size(145, 122);
            this.loadingCircleAuditUnitnumAcessV3.SpokeThickness = 2;
            this.loadingCircleAuditUnitnumAcessV3.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircleAuditUnitnumAcessV3.TabIndex = 50;
            this.loadingCircleAuditUnitnumAcessV3.Text = "loadingCircle6";
            this.loadingCircleAuditUnitnumAcessV3.Visible = false;
            // 
            // fastDataListViewAuditUnitnumAccessV3
            // 
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumn4TimeStamp);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumn2UserName);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumn1unitnum);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumn6apptID);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumnStoredProcedure);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumnHraAttributeValueList);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumnRelativeID);
            this.fastDataListViewAuditUnitnumAccessV3.AllColumns.Add(this.olvColumnHraObject);
            this.fastDataListViewAuditUnitnumAccessV3.AllowColumnReorder = true;
            this.fastDataListViewAuditUnitnumAccessV3.CheckBoxes = false;
            this.fastDataListViewAuditUnitnumAccessV3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4TimeStamp,
            this.olvColumn2UserName,
            this.olvColumn1unitnum,
            this.olvColumn6apptID,
            this.olvColumnStoredProcedure,
            this.olvColumnHraAttributeValueList,
            this.olvColumnRelativeID,
            this.olvColumnHraObject});
            this.fastDataListViewAuditUnitnumAccessV3.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListViewAuditUnitnumAccessV3.DataSource = null;
            this.fastDataListViewAuditUnitnumAccessV3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastDataListViewAuditUnitnumAccessV3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListViewAuditUnitnumAccessV3.FullRowSelect = true;
            this.fastDataListViewAuditUnitnumAccessV3.GridLines = true;
            this.fastDataListViewAuditUnitnumAccessV3.HeaderWordWrap = true;
            this.fastDataListViewAuditUnitnumAccessV3.HideSelection = false;
            this.fastDataListViewAuditUnitnumAccessV3.Location = new System.Drawing.Point(3, 3);
            this.fastDataListViewAuditUnitnumAccessV3.Name = "fastDataListViewAuditUnitnumAccessV3";
            this.fastDataListViewAuditUnitnumAccessV3.ShowCommandMenuOnRightClick = true;
            this.fastDataListViewAuditUnitnumAccessV3.ShowGroups = false;
            this.fastDataListViewAuditUnitnumAccessV3.Size = new System.Drawing.Size(990, 498);
            this.fastDataListViewAuditUnitnumAccessV3.TabIndex = 6;
            this.fastDataListViewAuditUnitnumAccessV3.UseCompatibleStateImageBehavior = false;
            this.fastDataListViewAuditUnitnumAccessV3.UseFiltering = true;
            this.fastDataListViewAuditUnitnumAccessV3.View = System.Windows.Forms.View.Details;
            this.fastDataListViewAuditUnitnumAccessV3.VirtualMode = true;
            this.fastDataListViewAuditUnitnumAccessV3.SelectedIndexChanged += new System.EventHandler(this.fastDataListViewAuditUnitnumAccessV3_SelectedIndexChanged);
            // 
            // olvColumn4TimeStamp
            // 
            this.olvColumn4TimeStamp.AspectName = "timestamp";
            this.olvColumn4TimeStamp.AspectToStringFormat = "";
            this.olvColumn4TimeStamp.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn4TimeStamp.Text = "Timestamp";
            this.olvColumn4TimeStamp.Width = 144;
            // 
            // olvColumn2UserName
            // 
            this.olvColumn2UserName.AspectName = "userName";
            this.olvColumn2UserName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2UserName.Text = "User Name";
            this.olvColumn2UserName.Width = 100;
            // 
            // olvColumn1unitnum
            // 
            this.olvColumn1unitnum.AspectName = "unitnum";
            this.olvColumn1unitnum.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1unitnum.Text = "Unitnum";
            this.olvColumn1unitnum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn1unitnum.Width = 80;
            // 
            // olvColumn6apptID
            // 
            this.olvColumn6apptID.AspectName = "apptID";
            this.olvColumn6apptID.AspectToStringFormat = "";
            this.olvColumn6apptID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn6apptID.Text = "Appt ID";
            this.olvColumn6apptID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumn6apptID.Width = 52;
            // 
            // olvColumnStoredProcedure
            // 
            this.olvColumnStoredProcedure.AspectName = "storedProcedure";
            this.olvColumnStoredProcedure.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnStoredProcedure.Text = "Stored Procedure";
            this.olvColumnStoredProcedure.Width = 100;
            // 
            // olvColumnHraAttributeValueList
            // 
            this.olvColumnHraAttributeValueList.AspectName = "hraAttributeValueList";
            this.olvColumnHraAttributeValueList.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnHraAttributeValueList.Text = "Attribute Value List";
            this.olvColumnHraAttributeValueList.Width = 180;
            // 
            // olvColumnRelativeID
            // 
            this.olvColumnRelativeID.AspectName = "relativeID";
            this.olvColumnRelativeID.AspectToStringFormat = "";
            this.olvColumnRelativeID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnRelativeID.Text = "Relative ID";
            this.olvColumnRelativeID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnRelativeID.Width = 52;
            // 
            // olvColumnHraObject
            // 
            this.olvColumnHraObject.AspectName = "hraObject";
            this.olvColumnHraObject.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnHraObject.Text = "Entity";
            this.olvColumnHraObject.Width = 170;
            // 
            // auditUnitnumV2tabPage
            // 
            this.auditUnitnumV2tabPage.Controls.Add(this.loadingCircleAuditUnitnumAccessV2);
            this.auditUnitnumV2tabPage.Controls.Add(this.fastDataListViewAuditUnitnumAccessV2);
            this.auditUnitnumV2tabPage.Location = new System.Drawing.Point(4, 22);
            this.auditUnitnumV2tabPage.Name = "auditUnitnumV2tabPage";
            this.auditUnitnumV2tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.auditUnitnumV2tabPage.Size = new System.Drawing.Size(996, 504);
            this.auditUnitnumV2tabPage.TabIndex = 1;
            this.auditUnitnumV2tabPage.Text = "Audit V2 Access";
            this.auditUnitnumV2tabPage.UseVisualStyleBackColor = true;
            // 
            // loadingCircleAuditUnitnumAccessV2
            // 
            this.loadingCircleAuditUnitnumAccessV2.Active = false;
            this.loadingCircleAuditUnitnumAccessV2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircleAuditUnitnumAccessV2.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircleAuditUnitnumAccessV2.Color = System.Drawing.SystemColors.ControlDark;
            this.loadingCircleAuditUnitnumAccessV2.Enabled = false;
            this.loadingCircleAuditUnitnumAccessV2.InnerCircleRadius = 5;
            this.loadingCircleAuditUnitnumAccessV2.Location = new System.Drawing.Point(401, 191);
            this.loadingCircleAuditUnitnumAccessV2.Name = "loadingCircleAuditUnitnumAccessV2";
            this.loadingCircleAuditUnitnumAccessV2.NumberSpoke = 12;
            this.loadingCircleAuditUnitnumAccessV2.OuterCircleRadius = 11;
            this.loadingCircleAuditUnitnumAccessV2.RotationSpeed = 100;
            this.loadingCircleAuditUnitnumAccessV2.Size = new System.Drawing.Size(195, 122);
            this.loadingCircleAuditUnitnumAccessV2.SpokeThickness = 2;
            this.loadingCircleAuditUnitnumAccessV2.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircleAuditUnitnumAccessV2.TabIndex = 49;
            this.loadingCircleAuditUnitnumAccessV2.Text = "loadingCircle6";
            this.loadingCircleAuditUnitnumAccessV2.Visible = false;
            // 
            // fastDataListViewAuditUnitnumAccessV2
            // 
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnStartTime);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnEndTime);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnCreatedBy);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnMachineName);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnUnitnum);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnApptid);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnMessage);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnApplication);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnTable);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnField);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnFieldMeaning);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnOldValue);
            this.fastDataListViewAuditUnitnumAccessV2.AllColumns.Add(this.olvColumnNewValue);
            this.fastDataListViewAuditUnitnumAccessV2.AllowColumnReorder = true;
            this.fastDataListViewAuditUnitnumAccessV2.CheckBoxes = false;
            this.fastDataListViewAuditUnitnumAccessV2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnStartTime,
            this.olvColumnEndTime,
            this.olvColumnCreatedBy,
            this.olvColumnMachineName,
            this.olvColumnUnitnum,
            this.olvColumnApptid,
            this.olvColumnMessage,
            this.olvColumnApplication,
            this.olvColumnTable,
            this.olvColumnField,
            this.olvColumnFieldMeaning,
            this.olvColumnOldValue,
            this.olvColumnNewValue});
            this.fastDataListViewAuditUnitnumAccessV2.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListViewAuditUnitnumAccessV2.DataSource = null;
            this.fastDataListViewAuditUnitnumAccessV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastDataListViewAuditUnitnumAccessV2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListViewAuditUnitnumAccessV2.FullRowSelect = true;
            this.fastDataListViewAuditUnitnumAccessV2.GridLines = true;
            this.fastDataListViewAuditUnitnumAccessV2.HeaderWordWrap = true;
            this.fastDataListViewAuditUnitnumAccessV2.HideSelection = false;
            this.fastDataListViewAuditUnitnumAccessV2.Location = new System.Drawing.Point(3, 3);
            this.fastDataListViewAuditUnitnumAccessV2.Name = "fastDataListViewAuditUnitnumAccessV2";
            this.fastDataListViewAuditUnitnumAccessV2.ShowCommandMenuOnRightClick = true;
            this.fastDataListViewAuditUnitnumAccessV2.ShowGroups = false;
            this.fastDataListViewAuditUnitnumAccessV2.Size = new System.Drawing.Size(990, 498);
            this.fastDataListViewAuditUnitnumAccessV2.TabIndex = 7;
            this.fastDataListViewAuditUnitnumAccessV2.UseCompatibleStateImageBehavior = false;
            this.fastDataListViewAuditUnitnumAccessV2.UseFiltering = true;
            this.fastDataListViewAuditUnitnumAccessV2.View = System.Windows.Forms.View.Details;
            this.fastDataListViewAuditUnitnumAccessV2.VirtualMode = true;
            this.fastDataListViewAuditUnitnumAccessV2.SelectedIndexChanged += new System.EventHandler(this.fastDataListView2_SelectedIndexChanged);
            // 
            // olvColumnStartTime
            // 
            this.olvColumnStartTime.AspectName = "startTime";
            this.olvColumnStartTime.AspectToStringFormat = "";
            this.olvColumnStartTime.Text = "Start Time";
            this.olvColumnStartTime.Width = 94;
            // 
            // olvColumnEndTime
            // 
            this.olvColumnEndTime.AspectName = "endTime";
            this.olvColumnEndTime.AspectToStringFormat = "";
            this.olvColumnEndTime.Text = "End Time";
            this.olvColumnEndTime.Width = 72;
            // 
            // olvColumnCreatedBy
            // 
            this.olvColumnCreatedBy.AspectName = "createdBy";
            this.olvColumnCreatedBy.Text = "Created By";
            this.olvColumnCreatedBy.Width = 111;
            // 
            // olvColumnMachineName
            // 
            this.olvColumnMachineName.AspectName = "machineName";
            this.olvColumnMachineName.Text = "Machine Name";
            // 
            // olvColumnUnitnum
            // 
            this.olvColumnUnitnum.AspectName = "unitnum";
            this.olvColumnUnitnum.Text = "MRN";
            this.olvColumnUnitnum.Width = 103;
            // 
            // olvColumnApptid
            // 
            this.olvColumnApptid.AspectName = "apptid";
            this.olvColumnApptid.Text = "ApptID";
            // 
            // olvColumnMessage
            // 
            this.olvColumnMessage.AspectName = "message";
            this.olvColumnMessage.Text = "Message";
            // 
            // olvColumnApplication
            // 
            this.olvColumnApplication.AspectName = "application";
            this.olvColumnApplication.Text = "Application";
            this.olvColumnApplication.Width = 81;
            // 
            // olvColumnTable
            // 
            this.olvColumnTable.AspectName = "table";
            this.olvColumnTable.Text = "Table";
            this.olvColumnTable.Width = 87;
            // 
            // olvColumnField
            // 
            this.olvColumnField.AspectName = "field";
            this.olvColumnField.Text = "Field";
            // 
            // olvColumnFieldMeaning
            // 
            this.olvColumnFieldMeaning.AspectName = "fieldMeaning";
            this.olvColumnFieldMeaning.Text = "Field Meaning";
            // 
            // olvColumnOldValue
            // 
            this.olvColumnOldValue.AspectName = "oldValue";
            this.olvColumnOldValue.Text = "Old Value";
            // 
            // olvColumnNewValue
            // 
            this.olvColumnNewValue.AspectName = "newValue";
            this.olvColumnNewValue.Text = "New Value";
            this.olvColumnNewValue.Width = 86;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.auditUnitnumV3tabPage);
            this.tabControl1.Controls.Add(this.auditUnitnumV2tabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 110);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1004, 530);
            this.tabControl1.TabIndex = 6;
            // 
            // AuditReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AuditReportsForm";
            this.Text = "Audit Reports";
            this.Load += new System.EventHandler(this.AuditReportsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.auditUnitnumV3tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListViewAuditUnitnumAccessV3)).EndInit();
            this.auditUnitnumV2tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListViewAuditUnitnumAccessV2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox mrnTextBox;
        private System.Windows.Forms.TabPage auditUnitnumV3tabPage;
        private MRG.Controls.UI.LoadingCircle loadingCircleAuditUnitnumAcessV3;
        private BrightIdeasSoftware.FastDataListView fastDataListViewAuditUnitnumAccessV3;
        private BrightIdeasSoftware.OLVColumn olvColumn4TimeStamp;
        private BrightIdeasSoftware.OLVColumn olvColumn2UserName;
        private BrightIdeasSoftware.OLVColumn olvColumn1unitnum;
        private BrightIdeasSoftware.OLVColumn olvColumn6apptID;
        private BrightIdeasSoftware.OLVColumn olvColumnStoredProcedure;
        private BrightIdeasSoftware.OLVColumn olvColumnHraAttributeValueList;
        private BrightIdeasSoftware.OLVColumn olvColumnRelativeID;
        private BrightIdeasSoftware.OLVColumn olvColumnHraObject;
        private System.Windows.Forms.TabPage auditUnitnumV2tabPage;
        private MRG.Controls.UI.LoadingCircle loadingCircleAuditUnitnumAccessV2;
        private BrightIdeasSoftware.FastDataListView fastDataListViewAuditUnitnumAccessV2;
        private BrightIdeasSoftware.OLVColumn olvColumnStartTime;
        private BrightIdeasSoftware.OLVColumn olvColumnEndTime;
        private BrightIdeasSoftware.OLVColumn olvColumnCreatedBy;
        private BrightIdeasSoftware.OLVColumn olvColumnMachineName;
        private BrightIdeasSoftware.OLVColumn olvColumnUnitnum;
        private BrightIdeasSoftware.OLVColumn olvColumnApptid;
        private BrightIdeasSoftware.OLVColumn olvColumnMessage;
        private BrightIdeasSoftware.OLVColumn olvColumnApplication;
        private BrightIdeasSoftware.OLVColumn olvColumnTable;
        private BrightIdeasSoftware.OLVColumn olvColumnField;
        private BrightIdeasSoftware.OLVColumn olvColumnFieldMeaning;
        private BrightIdeasSoftware.OLVColumn olvColumnOldValue;
        private BrightIdeasSoftware.OLVColumn olvColumnNewValue;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
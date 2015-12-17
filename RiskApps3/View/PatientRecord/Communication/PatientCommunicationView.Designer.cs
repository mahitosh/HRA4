namespace RiskApps3.View.PatientRecord.Communication    
{
    partial class PatientCommunicationView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientCommunicationView));
            SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo dictionaryFileInfo1 = new SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.loadingCircle2 = new MRG.Controls.UI.LoadingCircle();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.NewNote = new System.Windows.Forms.Button();
            this.NewTask = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.htmlEditorPanel = new System.Windows.Forms.Panel();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.SaveAsPdfButton = new System.Windows.Forms.Button();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.winFormHtmlEditor1 = new SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.PreparringDocumentLabel = new System.Windows.Forms.Label();
            this.PrintButton = new System.Windows.Forms.Button();
            this.EditButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.NoPreviewPanel = new System.Windows.Forms.Panel();
            this.DefaultOpenButton = new System.Windows.Forms.Button();
            this.NoPreviewLabel = new System.Windows.Forms.Label();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patientRecordHeader1 = new RiskApps3.View.PatientRecord.PatientRecordHeader();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.htmlEditorPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.winFormHtmlEditor1.Toolbar1.SuspendLayout();
            this.winFormHtmlEditor1.Toolbar2.SuspendLayout();
            this.winFormHtmlEditor1.SuspendLayout();
            this.NoPreviewPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(4, 83);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.loadingCircle2);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.objectListView1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.htmlEditorPanel);
            this.splitContainer1.Panel2.Controls.Add(this.NoPreviewPanel);
            this.splitContainer1.Panel2.Controls.Add(this.loadingCircle1);
            this.splitContainer1.Size = new System.Drawing.Size(1110, 610);
            this.splitContainer1.SplitterDistance = 377;
            this.splitContainer1.TabIndex = 20;
            this.splitContainer1.Resize += new System.EventHandler(this.splitContainer1_Resize);
            // 
            // loadingCircle2
            // 
            this.loadingCircle2.Active = false;
            this.loadingCircle2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadingCircle2.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle2.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle2.InnerCircleRadius = 5;
            this.loadingCircle2.Location = new System.Drawing.Point(159, 223);
            this.loadingCircle2.Name = "loadingCircle2";
            this.loadingCircle2.NumberSpoke = 12;
            this.loadingCircle2.OuterCircleRadius = 11;
            this.loadingCircle2.RotationSpeed = 100;
            this.loadingCircle2.Size = new System.Drawing.Size(43, 37);
            this.loadingCircle2.SpokeThickness = 2;
            this.loadingCircle2.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle2.TabIndex = 30;
            this.loadingCircle2.Text = "loadingCircle2";
            this.loadingCircle2.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(21, 528);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(93, 13);
            this.linkLabel1.TabIndex = 29;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Documents Folder";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.NewNote, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.NewTask, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 547);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(370, 60);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // NewNote
            // 
            this.NewNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NewNote.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewNote.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewNote.Location = new System.Drawing.Point(127, 3);
            this.NewNote.Name = "NewNote";
            this.NewNote.Size = new System.Drawing.Size(114, 54);
            this.NewNote.TabIndex = 26;
            this.NewNote.Text = "New Note";
            this.NewNote.UseVisualStyleBackColor = true;
            this.NewNote.Click += new System.EventHandler(this.NewNote_Click);
            // 
            // NewTask
            // 
            this.NewTask.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.NewTask.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewTask.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewTask.Location = new System.Drawing.Point(251, 3);
            this.NewTask.Name = "NewTask";
            this.NewTask.Size = new System.Drawing.Size(114, 54);
            this.NewTask.TabIndex = 27;
            this.NewTask.Text = "New Task";
            this.NewTask.UseVisualStyleBackColor = true;
            this.NewTask.Click += new System.EventHandler(this.NewTask_Click);
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 54);
            this.button2.TabIndex = 29;
            this.button2.Text = "New Document";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvColumn2);
            this.objectListView1.AllColumns.Add(this.olvColumn1);
            this.objectListView1.AllColumns.Add(this.olvColumn3);
            this.objectListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn1,
            this.olvColumn3});
            this.objectListView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectListView1.FullRowSelect = true;
            this.objectListView1.GridLines = true;
            this.objectListView1.HeaderUsesThemes = false;
            this.objectListView1.HideSelection = false;
            this.objectListView1.Location = new System.Drawing.Point(4, 3);
            this.objectListView1.MultiSelect = false;
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.ShowGroups = false;
            this.objectListView1.Size = new System.Drawing.Size(370, 518);
            this.objectListView1.TabIndex = 3;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.UseOverlays = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectionChanged += new System.EventHandler(this.objectListView1_SelectionChanged);
            this.objectListView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.objectListView1_MouseClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "date";
            this.olvColumn2.AspectToStringFormat = "";
            this.olvColumn2.Text = "Date";
            this.olvColumn2.Width = 112;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "title";
            this.olvColumn1.Text = "Title";
            this.olvColumn1.Width = 236;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "author";
            this.olvColumn3.Text = "Author";
            this.olvColumn3.Width = 120;
            // 
            // htmlEditorPanel
            // 
            this.htmlEditorPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlEditorPanel.Controls.Add(this.DiscardButton);
            this.htmlEditorPanel.Controls.Add(this.SaveAsPdfButton);
            this.htmlEditorPanel.Controls.Add(this.FileNameLabel);
            this.htmlEditorPanel.Controls.Add(this.panel1);
            this.htmlEditorPanel.Controls.Add(this.PrintButton);
            this.htmlEditorPanel.Controls.Add(this.EditButton);
            this.htmlEditorPanel.Controls.Add(this.SaveButton);
            this.htmlEditorPanel.Location = new System.Drawing.Point(6, 3);
            this.htmlEditorPanel.Name = "htmlEditorPanel";
            this.htmlEditorPanel.Size = new System.Drawing.Size(713, 601);
            this.htmlEditorPanel.TabIndex = 18;
            // 
            // DiscardButton
            // 
            this.DiscardButton.Enabled = false;
            this.DiscardButton.Location = new System.Drawing.Point(82, 37);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(60, 40);
            this.DiscardButton.TabIndex = 21;
            this.DiscardButton.Text = "Discard Changes";
            this.DiscardButton.UseVisualStyleBackColor = true;
            this.DiscardButton.Click += new System.EventHandler(this.DiscardButton_Click);
            // 
            // SaveAsPdfButton
            // 
            this.SaveAsPdfButton.Location = new System.Drawing.Point(233, 37);
            this.SaveAsPdfButton.Name = "SaveAsPdfButton";
            this.SaveAsPdfButton.Size = new System.Drawing.Size(60, 40);
            this.SaveAsPdfButton.TabIndex = 20;
            this.SaveAsPdfButton.Text = "Save As PDF";
            this.SaveAsPdfButton.UseVisualStyleBackColor = true;
            this.SaveAsPdfButton.Click += new System.EventHandler(this.SaveAsPdfButton_Click);
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileNameLabel.Location = new System.Drawing.Point(12, 10);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(0, 20);
            this.FileNameLabel.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.winFormHtmlEditor1);
            this.panel1.Controls.Add(this.PreparringDocumentLabel);
            this.panel1.Location = new System.Drawing.Point(3, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 513);
            this.panel1.TabIndex = 18;
            // 
            // winFormHtmlEditor1
            // 
            this.winFormHtmlEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.winFormHtmlEditor1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.winFormHtmlEditor1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.winFormHtmlEditor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            // 
            // winFormHtmlEditor1.BtnAlignCenter
            // 
            this.winFormHtmlEditor1.BtnAlignCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignCenter.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignCenter.Image")));
            this.winFormHtmlEditor1.BtnAlignCenter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignCenter.Name = "_factoryBtnAlignCenter";
            this.winFormHtmlEditor1.BtnAlignCenter.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignCenter.Text = "Align Centre";
            // 
            // winFormHtmlEditor1.BtnAlignLeft
            // 
            this.winFormHtmlEditor1.BtnAlignLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignLeft.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignLeft.Image")));
            this.winFormHtmlEditor1.BtnAlignLeft.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignLeft.Name = "_factoryBtnAlignLeft";
            this.winFormHtmlEditor1.BtnAlignLeft.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignLeft.Text = "Align Left";
            // 
            // winFormHtmlEditor1.BtnAlignRight
            // 
            this.winFormHtmlEditor1.BtnAlignRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnAlignRight.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnAlignRight.Image")));
            this.winFormHtmlEditor1.BtnAlignRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnAlignRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnAlignRight.Name = "_factoryBtnAlignRight";
            this.winFormHtmlEditor1.BtnAlignRight.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnAlignRight.Text = "Align Right";
            // 
            // winFormHtmlEditor1.BtnBodyStyle
            // 
            this.winFormHtmlEditor1.BtnBodyStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnBodyStyle.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnBodyStyle.Image")));
            this.winFormHtmlEditor1.BtnBodyStyle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnBodyStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnBodyStyle.Name = "_factoryBtnBodyStyle";
            this.winFormHtmlEditor1.BtnBodyStyle.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnBodyStyle.Text = "Document Style ";
            this.winFormHtmlEditor1.BtnBodyStyle.Visible = false;
            // 
            // winFormHtmlEditor1.BtnBold
            // 
            this.winFormHtmlEditor1.BtnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnBold.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnBold.Image")));
            this.winFormHtmlEditor1.BtnBold.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnBold.Name = "_factoryBtnBold";
            this.winFormHtmlEditor1.BtnBold.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnBold.Text = "Bold";
            // 
            // winFormHtmlEditor1.BtnCopy
            // 
            this.winFormHtmlEditor1.BtnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnCopy.Image")));
            this.winFormHtmlEditor1.BtnCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnCopy.Name = "_factoryBtnCopy";
            this.winFormHtmlEditor1.BtnCopy.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnCopy.Text = "Copy";
            // 
            // winFormHtmlEditor1.BtnCut
            // 
            this.winFormHtmlEditor1.BtnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnCut.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnCut.Image")));
            this.winFormHtmlEditor1.BtnCut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnCut.Name = "_factoryBtnCut";
            this.winFormHtmlEditor1.BtnCut.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnCut.Text = "Cut";
            // 
            // winFormHtmlEditor1.BtnFontColor
            // 
            this.winFormHtmlEditor1.BtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFontColor.Image")));
            this.winFormHtmlEditor1.BtnFontColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnFontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFontColor.Name = "_factoryBtnFontColor";
            this.winFormHtmlEditor1.BtnFontColor.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFontColor.Text = "Apply Font Color";
            // 
            // winFormHtmlEditor1.BtnFormatRedo
            // 
            this.winFormHtmlEditor1.BtnFormatRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatRedo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatRedo.Image")));
            this.winFormHtmlEditor1.BtnFormatRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatRedo.Name = "_factoryBtnRedo";
            this.winFormHtmlEditor1.BtnFormatRedo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFormatRedo.Text = "Redo";
            // 
            // winFormHtmlEditor1.BtnFormatReset
            // 
            this.winFormHtmlEditor1.BtnFormatReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatReset.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatReset.Image")));
            this.winFormHtmlEditor1.BtnFormatReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnFormatReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatReset.Name = "_factoryBtnFormatReset";
            this.winFormHtmlEditor1.BtnFormatReset.Size = new System.Drawing.Size(34, 26);
            this.winFormHtmlEditor1.BtnFormatReset.Text = "Remove Format";
            // 
            // winFormHtmlEditor1.BtnFormatUndo
            // 
            this.winFormHtmlEditor1.BtnFormatUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnFormatUndo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnFormatUndo.Image")));
            this.winFormHtmlEditor1.BtnFormatUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnFormatUndo.Name = "_factoryBtnUndo";
            this.winFormHtmlEditor1.BtnFormatUndo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnFormatUndo.Text = "Undo";
            // 
            // winFormHtmlEditor1.BtnHighlightColor
            // 
            this.winFormHtmlEditor1.BtnHighlightColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHighlightColor.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHighlightColor.Image")));
            this.winFormHtmlEditor1.BtnHighlightColor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHighlightColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHighlightColor.Name = "_factoryBtnHighlightColor";
            this.winFormHtmlEditor1.BtnHighlightColor.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnHighlightColor.Text = "Apply Highlight Color";
            // 
            // winFormHtmlEditor1.BtnHorizontalRule
            // 
            this.winFormHtmlEditor1.BtnHorizontalRule.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHorizontalRule.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHorizontalRule.Image")));
            this.winFormHtmlEditor1.BtnHorizontalRule.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHorizontalRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHorizontalRule.Name = "_factoryBtnHorizontalRule";
            this.winFormHtmlEditor1.BtnHorizontalRule.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnHorizontalRule.Text = "Insert Horizontal Rule";
            // 
            // winFormHtmlEditor1.BtnHyperlink
            // 
            this.winFormHtmlEditor1.BtnHyperlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnHyperlink.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnHyperlink.Image")));
            this.winFormHtmlEditor1.BtnHyperlink.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnHyperlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnHyperlink.Name = "_factoryBtnHyperlink";
            this.winFormHtmlEditor1.BtnHyperlink.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnHyperlink.Text = "Hyperlink";
            // 
            // winFormHtmlEditor1.BtnImage
            // 
            this.winFormHtmlEditor1.BtnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnImage.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnImage.Image")));
            this.winFormHtmlEditor1.BtnImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnImage.Name = "_factoryBtnImage";
            this.winFormHtmlEditor1.BtnImage.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnImage.Text = "Image";
            // 
            // winFormHtmlEditor1.BtnIndent
            // 
            this.winFormHtmlEditor1.BtnIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnIndent.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnIndent.Image")));
            this.winFormHtmlEditor1.BtnIndent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnIndent.Name = "_factoryBtnIndent";
            this.winFormHtmlEditor1.BtnIndent.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnIndent.Text = "Indent";
            // 
            // winFormHtmlEditor1.BtnInsertYouTubeVideo
            // 
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnInsertYouTubeVideo.Image")));
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Name = "_factoryBtnInsertYouTubeVideo";
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Text = "Insert YouTube Video";
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo.Visible = false;
            // 
            // winFormHtmlEditor1.BtnItalic
            // 
            this.winFormHtmlEditor1.BtnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnItalic.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnItalic.Image")));
            this.winFormHtmlEditor1.BtnItalic.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnItalic.Name = "_factoryBtnItalic";
            this.winFormHtmlEditor1.BtnItalic.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnItalic.Text = "Italic";
            // 
            // winFormHtmlEditor1.BtnNew
            // 
            this.winFormHtmlEditor1.BtnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnNew.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnNew.Image")));
            this.winFormHtmlEditor1.BtnNew.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnNew.Name = "_factoryBtnNew";
            this.winFormHtmlEditor1.BtnNew.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnNew.Text = "New";
            this.winFormHtmlEditor1.BtnNew.Visible = false;
            // 
            // winFormHtmlEditor1.BtnOpen
            // 
            this.winFormHtmlEditor1.BtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOpen.Image")));
            this.winFormHtmlEditor1.BtnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOpen.Name = "_factoryBtnOpen";
            this.winFormHtmlEditor1.BtnOpen.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnOpen.Text = "Open";
            this.winFormHtmlEditor1.BtnOpen.Visible = false;
            // 
            // winFormHtmlEditor1.BtnOrderedList
            // 
            this.winFormHtmlEditor1.BtnOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOrderedList.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOrderedList.Image")));
            this.winFormHtmlEditor1.BtnOrderedList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOrderedList.Name = "_factoryBtnOrderedList";
            this.winFormHtmlEditor1.BtnOrderedList.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnOrderedList.Text = "Numbered List";
            // 
            // winFormHtmlEditor1.BtnOutdent
            // 
            this.winFormHtmlEditor1.BtnOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnOutdent.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnOutdent.Image")));
            this.winFormHtmlEditor1.BtnOutdent.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnOutdent.Name = "_factoryBtnOutdent";
            this.winFormHtmlEditor1.BtnOutdent.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnOutdent.Text = "Outdent";
            // 
            // winFormHtmlEditor1.BtnPaste
            // 
            this.winFormHtmlEditor1.BtnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPaste.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPaste.Image")));
            this.winFormHtmlEditor1.BtnPaste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPaste.Name = "_factoryBtnPaste";
            this.winFormHtmlEditor1.BtnPaste.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPaste.Text = "Paste";
            // 
            // winFormHtmlEditor1.BtnPasteFromMSWord
            // 
            this.winFormHtmlEditor1.BtnPasteFromMSWord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPasteFromMSWord.Image")));
            this.winFormHtmlEditor1.BtnPasteFromMSWord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Name = "_factoryBtnPasteFromMSWord";
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Text = "Paste the Content that you Copied from MS Word";
            this.winFormHtmlEditor1.BtnPasteFromMSWord.Visible = false;
            // 
            // winFormHtmlEditor1.BtnPrint
            // 
            this.winFormHtmlEditor1.BtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnPrint.Image")));
            this.winFormHtmlEditor1.BtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnPrint.Name = "_factoryBtnPrint";
            this.winFormHtmlEditor1.BtnPrint.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnPrint.Text = "Print";
            // 
            // winFormHtmlEditor1.BtnSave
            // 
            this.winFormHtmlEditor1.BtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSave.Image")));
            this.winFormHtmlEditor1.BtnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSave.Name = "_factoryBtnSave";
            this.winFormHtmlEditor1.BtnSave.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnSave.Text = "Save";
            this.winFormHtmlEditor1.BtnSave.Visible = false;
            // 
            // winFormHtmlEditor1.BtnSearch
            // 
            this.winFormHtmlEditor1.BtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSearch.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSearch.Image")));
            this.winFormHtmlEditor1.BtnSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSearch.Name = "_factoryBtnSearch";
            this.winFormHtmlEditor1.BtnSearch.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnSearch.Text = "Search";
            // 
            // winFormHtmlEditor1.BtnSpellCheck
            // 
            this.winFormHtmlEditor1.BtnSpellCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSpellCheck.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSpellCheck.Image")));
            this.winFormHtmlEditor1.BtnSpellCheck.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSpellCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSpellCheck.Name = "_factoryBtnSpellCheck";
            this.winFormHtmlEditor1.BtnSpellCheck.Size = new System.Drawing.Size(26, 26);
            this.winFormHtmlEditor1.BtnSpellCheck.Text = "Check Spelling";
            // 
            // winFormHtmlEditor1.BtnStrikeThrough
            // 
            this.winFormHtmlEditor1.BtnStrikeThrough.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnStrikeThrough.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnStrikeThrough.Image")));
            this.winFormHtmlEditor1.BtnStrikeThrough.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnStrikeThrough.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnStrikeThrough.Name = "_factoryBtnStrikeThrough";
            this.winFormHtmlEditor1.BtnStrikeThrough.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnStrikeThrough.Text = "Strike Thru";
            // 
            // winFormHtmlEditor1.BtnSubscript
            // 
            this.winFormHtmlEditor1.BtnSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSubscript.Image")));
            this.winFormHtmlEditor1.BtnSubscript.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSubscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSubscript.Name = "_factoryBtnSubscript";
            this.winFormHtmlEditor1.BtnSubscript.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnSubscript.Text = "Subscript";
            // 
            // winFormHtmlEditor1.BtnSuperScript
            // 
            this.winFormHtmlEditor1.BtnSuperScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSuperScript.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSuperScript.Image")));
            this.winFormHtmlEditor1.BtnSuperScript.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSuperScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSuperScript.Name = "_factoryBtnSuperScript";
            this.winFormHtmlEditor1.BtnSuperScript.Size = new System.Drawing.Size(27, 26);
            this.winFormHtmlEditor1.BtnSuperScript.Text = "Superscript";
            // 
            // winFormHtmlEditor1.BtnSymbol
            // 
            this.winFormHtmlEditor1.BtnSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnSymbol.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnSymbol.Image")));
            this.winFormHtmlEditor1.BtnSymbol.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnSymbol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnSymbol.Name = "_factoryBtnSymbol";
            this.winFormHtmlEditor1.BtnSymbol.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnSymbol.Text = "Insert Symbols";
            // 
            // winFormHtmlEditor1.BtnTable
            // 
            this.winFormHtmlEditor1.BtnTable.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnTable.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnTable.Image")));
            this.winFormHtmlEditor1.BtnTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnTable.Name = "_factoryBtnTable";
            this.winFormHtmlEditor1.BtnTable.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnTable.Text = "Table";
            // 
            // winFormHtmlEditor1.BtnUnderline
            // 
            this.winFormHtmlEditor1.BtnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnUnderline.Image")));
            this.winFormHtmlEditor1.BtnUnderline.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnUnderline.Name = "_factoryBtnUnderline";
            this.winFormHtmlEditor1.BtnUnderline.Size = new System.Drawing.Size(23, 26);
            this.winFormHtmlEditor1.BtnUnderline.Text = "Underline";
            // 
            // winFormHtmlEditor1.BtnUnOrderedList
            // 
            this.winFormHtmlEditor1.BtnUnOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.winFormHtmlEditor1.BtnUnOrderedList.Image = ((System.Drawing.Image)(resources.GetObject("winFormHtmlEditor1.BtnUnOrderedList.Image")));
            this.winFormHtmlEditor1.BtnUnOrderedList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.winFormHtmlEditor1.BtnUnOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.winFormHtmlEditor1.BtnUnOrderedList.Name = "_factoryBtnUnOrderedList";
            this.winFormHtmlEditor1.BtnUnOrderedList.Size = new System.Drawing.Size(24, 26);
            this.winFormHtmlEditor1.BtnUnOrderedList.Text = "Bullet List";
            // 
            // winFormHtmlEditor1.CmbFontName
            // 
            this.winFormHtmlEditor1.CmbFontName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.winFormHtmlEditor1.CmbFontName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.winFormHtmlEditor1.CmbFontName.MaxDropDownItems = 17;
            this.winFormHtmlEditor1.CmbFontName.Name = "_factoryCmbFontName";
            this.winFormHtmlEditor1.CmbFontName.Size = new System.Drawing.Size(125, 29);
            this.winFormHtmlEditor1.CmbFontName.Text = "Times New Roman";
            // 
            // winFormHtmlEditor1.CmbFontSize
            // 
            this.winFormHtmlEditor1.CmbFontSize.Name = "_factoryCmbFontSize";
            this.winFormHtmlEditor1.CmbFontSize.Size = new System.Drawing.Size(75, 29);
            this.winFormHtmlEditor1.CmbFontSize.Text = "12pt";
            // 
            // winFormHtmlEditor1.CmbTitleInsert
            // 
            this.winFormHtmlEditor1.CmbTitleInsert.Name = "_factoryCmbTitleInsert";
            this.winFormHtmlEditor1.CmbTitleInsert.Size = new System.Drawing.Size(100, 29);
            this.winFormHtmlEditor1.EditorContextMenuStrip = null;
            this.winFormHtmlEditor1.HeaderStyleContentElementID = "page_style";
            this.winFormHtmlEditor1.HorizontalScroll = null;
            this.winFormHtmlEditor1.Location = new System.Drawing.Point(3, 3);
            this.winFormHtmlEditor1.Name = "winFormHtmlEditor1";
            this.winFormHtmlEditor1.Options.ConvertFileUrlsToLocalPaths = true;
            this.winFormHtmlEditor1.Options.CustomDOCTYPE = null;
            this.winFormHtmlEditor1.Options.DefaultHtmlType = SpiceLogic.HtmlEditorControl.Domain.BOs.HtmlContentTypes.DocumentHtml;
            this.winFormHtmlEditor1.Options.FooterTagNavigatorFont = null;
            this.winFormHtmlEditor1.Options.FooterTagNavigatorTextColor = System.Drawing.Color.Teal;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.ConnectionMode = SpiceLogic.HtmlEditorControl.Domain.BOs.UserOptions.FTPSettings.ConnectionModes.Active;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Host = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Password = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Port = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.RemoteFolderPath = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.Timeout = 4000;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.UrlOfTheRemoteFolderPath = null;
            this.winFormHtmlEditor1.Options.FTPSettingsForRemoteResources.UserName = null;
            this.winFormHtmlEditor1.Size = new System.Drawing.Size(680, 507);
            this.winFormHtmlEditor1.SpellCheckOptions.CurlyUnderlineImageFilePath = null;
            dictionaryFileInfo1.AffixFilePath = null;
            dictionaryFileInfo1.DictionaryFilePath = null;
            dictionaryFileInfo1.EnableUserDictionary = true;
            dictionaryFileInfo1.UserDictionaryFilePath = null;
            this.winFormHtmlEditor1.SpellCheckOptions.DictionaryFile = dictionaryFileInfo1;
            this.winFormHtmlEditor1.SpellCheckOptions.WaitAlertMessage = "Searching next misspelled word..... (please wait)";
            this.winFormHtmlEditor1.TabIndex = 14;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_Toolbar1
            // 
            // 
            // winFormHtmlEditor1.ToolStripSeparator1
            // 
            this.winFormHtmlEditor1.ToolStripSeparator1.Name = "_toolStripSeparator1";
            this.winFormHtmlEditor1.ToolStripSeparator1.Size = new System.Drawing.Size(6, 29);
            this.winFormHtmlEditor1.ToolStripSeparator1.Visible = false;
            // 
            // winFormHtmlEditor1.ToolStripSeparator2
            // 
            this.winFormHtmlEditor1.ToolStripSeparator2.Name = "_toolStripSeparator2";
            this.winFormHtmlEditor1.ToolStripSeparator2.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator3
            // 
            this.winFormHtmlEditor1.ToolStripSeparator3.Name = "_toolStripSeparator3";
            this.winFormHtmlEditor1.ToolStripSeparator3.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator4
            // 
            this.winFormHtmlEditor1.ToolStripSeparator4.Name = "_toolStripSeparator4";
            this.winFormHtmlEditor1.ToolStripSeparator4.Size = new System.Drawing.Size(6, 29);
            this.winFormHtmlEditor1.Toolbar1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winFormHtmlEditor1.BtnNew,
            this.winFormHtmlEditor1.BtnOpen,
            this.winFormHtmlEditor1.BtnSave,
            this.winFormHtmlEditor1.ToolStripSeparator1,
            this.winFormHtmlEditor1.CmbFontName,
            this.winFormHtmlEditor1.CmbFontSize,
            this.winFormHtmlEditor1.ToolStripSeparator2,
            this.winFormHtmlEditor1.BtnCut,
            this.winFormHtmlEditor1.BtnCopy,
            this.winFormHtmlEditor1.BtnPaste,
            this.winFormHtmlEditor1.BtnPasteFromMSWord,
            this.winFormHtmlEditor1.ToolStripSeparator3,
            this.winFormHtmlEditor1.BtnBold,
            this.winFormHtmlEditor1.BtnItalic,
            this.winFormHtmlEditor1.BtnUnderline,
            this.winFormHtmlEditor1.ToolStripSeparator4,
            this.winFormHtmlEditor1.BtnFormatReset,
            this.winFormHtmlEditor1.BtnFormatUndo,
            this.winFormHtmlEditor1.BtnFormatRedo,
            this.winFormHtmlEditor1.BtnPrint,
            this.winFormHtmlEditor1.BtnSpellCheck,
            this.winFormHtmlEditor1.BtnSearch,
            this.toolStripButton1});
            this.winFormHtmlEditor1.Toolbar1.Location = new System.Drawing.Point(0, 0);
            this.winFormHtmlEditor1.Toolbar1.Name = "WinFormHtmlEditor_Toolbar1";
            this.winFormHtmlEditor1.Toolbar1.Size = new System.Drawing.Size(680, 29);
            this.winFormHtmlEditor1.Toolbar1.TabIndex = 0;
            this.winFormHtmlEditor1.Toolbar1.Visible = false;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_Toolbar2
            // 
            // 
            // winFormHtmlEditor1.ToolStripSeparator5
            // 
            this.winFormHtmlEditor1.ToolStripSeparator5.Name = "_toolStripSeparator5";
            this.winFormHtmlEditor1.ToolStripSeparator5.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator6
            // 
            this.winFormHtmlEditor1.ToolStripSeparator6.Name = "_toolStripSeparator6";
            this.winFormHtmlEditor1.ToolStripSeparator6.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator7
            // 
            this.winFormHtmlEditor1.ToolStripSeparator7.Name = "_toolStripSeparator7";
            this.winFormHtmlEditor1.ToolStripSeparator7.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator8
            // 
            this.winFormHtmlEditor1.ToolStripSeparator8.Name = "_toolStripSeparator8";
            this.winFormHtmlEditor1.ToolStripSeparator8.Size = new System.Drawing.Size(6, 29);
            // 
            // winFormHtmlEditor1.ToolStripSeparator9
            // 
            this.winFormHtmlEditor1.ToolStripSeparator9.Name = "_toolStripSeparator9";
            this.winFormHtmlEditor1.ToolStripSeparator9.Size = new System.Drawing.Size(6, 29);
            this.winFormHtmlEditor1.Toolbar2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.winFormHtmlEditor1.CmbTitleInsert,
            this.winFormHtmlEditor1.BtnHighlightColor,
            this.winFormHtmlEditor1.BtnFontColor,
            this.winFormHtmlEditor1.ToolStripSeparator5,
            this.winFormHtmlEditor1.BtnHyperlink,
            this.winFormHtmlEditor1.BtnImage,
            this.winFormHtmlEditor1.BtnInsertYouTubeVideo,
            this.winFormHtmlEditor1.BtnTable,
            this.winFormHtmlEditor1.BtnSymbol,
            this.winFormHtmlEditor1.BtnHorizontalRule,
            this.winFormHtmlEditor1.ToolStripSeparator6,
            this.winFormHtmlEditor1.BtnOrderedList,
            this.winFormHtmlEditor1.BtnUnOrderedList,
            this.winFormHtmlEditor1.ToolStripSeparator7,
            this.winFormHtmlEditor1.BtnAlignLeft,
            this.winFormHtmlEditor1.BtnAlignCenter,
            this.winFormHtmlEditor1.BtnAlignRight,
            this.winFormHtmlEditor1.ToolStripSeparator8,
            this.winFormHtmlEditor1.BtnOutdent,
            this.winFormHtmlEditor1.BtnIndent,
            this.winFormHtmlEditor1.ToolStripSeparator9,
            this.winFormHtmlEditor1.BtnStrikeThrough,
            this.winFormHtmlEditor1.BtnSuperScript,
            this.winFormHtmlEditor1.BtnSubscript,
            this.winFormHtmlEditor1.BtnBodyStyle});
            this.winFormHtmlEditor1.Toolbar2.Location = new System.Drawing.Point(0, 0);
            this.winFormHtmlEditor1.Toolbar2.Name = "WinFormHtmlEditor_Toolbar2";
            this.winFormHtmlEditor1.Toolbar2.Size = new System.Drawing.Size(680, 29);
            this.winFormHtmlEditor1.Toolbar2.TabIndex = 0;
            this.winFormHtmlEditor1.Toolbar2.Visible = false;
            this.winFormHtmlEditor1.ToolbarContextMenuStrip = null;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_ToolbarFooter
            // 
            this.winFormHtmlEditor1.ToolbarFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.winFormHtmlEditor1.ToolbarFooter.Location = new System.Drawing.Point(0, 482);
            this.winFormHtmlEditor1.ToolbarFooter.Name = "WinFormHtmlEditor_ToolbarFooter";
            this.winFormHtmlEditor1.ToolbarFooter.Size = new System.Drawing.Size(680, 25);
            this.winFormHtmlEditor1.ToolbarFooter.TabIndex = 7;
            this.winFormHtmlEditor1.ToolbarFooter.Visible = false;
            this.winFormHtmlEditor1.VerticalScroll = null;
            this.winFormHtmlEditor1.z__ignore = false;
            this.winFormHtmlEditor1.EditorModeChanged += new System.EventHandler<System.EventArgs>(this.winFormHtmlEditor1_EditorModeChanged);
            this.winFormHtmlEditor1.HtmlChanged += new System.EventHandler<System.EventArgs>(this.winFormHtmlEditor1_HtmlChanged);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 26);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // PreparringDocumentLabel
            // 
            this.PreparringDocumentLabel.AutoSize = true;
            this.PreparringDocumentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PreparringDocumentLabel.Location = new System.Drawing.Point(9, 24);
            this.PreparringDocumentLabel.Name = "PreparringDocumentLabel";
            this.PreparringDocumentLabel.Size = new System.Drawing.Size(191, 20);
            this.PreparringDocumentLabel.TabIndex = 19;
            this.PreparringDocumentLabel.Text = "Preparing the document...";
            // 
            // PrintButton
            // 
            this.PrintButton.Location = new System.Drawing.Point(157, 37);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(60, 40);
            this.PrintButton.TabIndex = 17;
            this.PrintButton.Text = "Print";
            this.PrintButton.UseVisualStyleBackColor = true;
            this.PrintButton.Click += new System.EventHandler(this.Print_Click);
            // 
            // EditButton
            // 
            this.EditButton.Location = new System.Drawing.Point(6, 37);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(60, 40);
            this.EditButton.TabIndex = 15;
            this.EditButton.Text = "Edit";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.Edit_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(6, 37);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(60, 40);
            this.SaveButton.TabIndex = 16;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.Save_Click);
            // 
            // NoPreviewPanel
            // 
            this.NoPreviewPanel.Controls.Add(this.DefaultOpenButton);
            this.NoPreviewPanel.Controls.Add(this.NoPreviewLabel);
            this.NoPreviewPanel.Location = new System.Drawing.Point(3, 3);
            this.NoPreviewPanel.Name = "NoPreviewPanel";
            this.NoPreviewPanel.Size = new System.Drawing.Size(424, 133);
            this.NoPreviewPanel.TabIndex = 23;
            // 
            // DefaultOpenButton
            // 
            this.DefaultOpenButton.Location = new System.Drawing.Point(29, 40);
            this.DefaultOpenButton.Name = "DefaultOpenButton";
            this.DefaultOpenButton.Size = new System.Drawing.Size(175, 31);
            this.DefaultOpenButton.TabIndex = 22;
            this.DefaultOpenButton.Text = "Open With Default Application";
            this.DefaultOpenButton.UseVisualStyleBackColor = true;
            this.DefaultOpenButton.Click += new System.EventHandler(this.DefaultOpenButton_Click);
            // 
            // NoPreviewLabel
            // 
            this.NoPreviewLabel.AutoSize = true;
            this.NoPreviewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoPreviewLabel.Location = new System.Drawing.Point(14, 11);
            this.NoPreviewLabel.Name = "NoPreviewLabel";
            this.NoPreviewLabel.Size = new System.Drawing.Size(266, 20);
            this.NoPreviewLabel.TabIndex = 20;
            this.NoPreviewLabel.Text = "No Preview available for this file type.";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(318, 251);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(133, 53);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 13;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTaskToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 48);
            // 
            // deleteTaskToolStripMenuItem
            // 
            this.deleteTaskToolStripMenuItem.Name = "deleteTaskToolStripMenuItem";
            this.deleteTaskToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.deleteTaskToolStripMenuItem.Text = "Delete Task";
            this.deleteTaskToolStripMenuItem.Click += new System.EventHandler(this.deleteTaskToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.deleteToolStripMenuItem.Text = "Delete Document";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // patientRecordHeader1
            // 
            this.patientRecordHeader1.Collapsed = false;
            this.patientRecordHeader1.Location = new System.Drawing.Point(4, 3);
            this.patientRecordHeader1.Name = "patientRecordHeader1";
            this.patientRecordHeader1.Size = new System.Drawing.Size(1110, 74);
            this.patientRecordHeader1.TabIndex = 21;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.IncludeSubdirectories = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // PatientCommunicationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 705);
            this.Controls.Add(this.patientRecordHeader1);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PatientCommunicationView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notes && Tasks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientCommunication_FormClosing);
            this.Load += new System.EventHandler(this.PatientCommunicationView_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.htmlEditorPanel.ResumeLayout(false);
            this.htmlEditorPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.winFormHtmlEditor1.Toolbar1.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar1.PerformLayout();
            this.winFormHtmlEditor1.Toolbar2.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar2.PerformLayout();
            this.winFormHtmlEditor1.ResumeLayout(false);
            this.winFormHtmlEditor1.PerformLayout();
            this.NoPreviewPanel.ResumeLayout(false);
            this.NoPreviewPanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteTaskToolStripMenuItem;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private PatientRecordHeader patientRecordHeader1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button NewNote;
        private System.Windows.Forms.Button NewTask;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private MRG.Controls.UI.LoadingCircle loadingCircle2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button PrintButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button EditButton;
        private SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor winFormHtmlEditor1;
        private System.Windows.Forms.Panel htmlEditorPanel;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Label PreparringDocumentLabel;
        private System.Windows.Forms.Label FileNameLabel;
        private System.Windows.Forms.Button SaveAsPdfButton;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Button DefaultOpenButton;
        private System.Windows.Forms.Label NoPreviewLabel;
        private System.Windows.Forms.Panel NoPreviewPanel;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}
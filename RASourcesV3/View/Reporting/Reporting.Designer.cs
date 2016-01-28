namespace RiskApps3.View.Reporting
{
    partial class Reporting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reporting));
            SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo dictionaryFileInfo1 = new SpiceLogic.HtmlEditorControl.Domain.DesignTime.DictionaryFileInfo();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClinicCombo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ReportCombo = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.winFormHtmlEditor1 = new SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor();
            this.winFormHtmlEditor1.Toolbar1.SuspendLayout();
            this.winFormHtmlEditor1.Toolbar2.SuspendLayout();
            this.winFormHtmlEditor1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(501, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(119, 23);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(626, 25);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(116, 23);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.CloseUp += new System.EventHandler(this.dateTimePicker_CloseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Run Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(498, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(623, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Date";
            // 
            // ClinicCombo
            // 
            this.ClinicCombo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClinicCombo.FormattingEnabled = true;
            this.ClinicCombo.Location = new System.Drawing.Point(271, 25);
            this.ClinicCombo.Name = "ClinicCombo";
            this.ClinicCombo.Size = new System.Drawing.Size(224, 24);
            this.ClinicCombo.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(272, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Clinic";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Report";
            // 
            // ReportCombo
            // 
            this.ReportCombo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportCombo.FormattingEnabled = true;
            this.ReportCombo.Location = new System.Drawing.Point(12, 25);
            this.ReportCombo.Name = "ReportCombo";
            this.ReportCombo.Size = new System.Drawing.Size(253, 24);
            this.ReportCombo.TabIndex = 8;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.White;
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(133, 57);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(171, 20);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 55;
            this.progressBar1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(313, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 56;
            this.label5.Text = "Processing";
            this.label5.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownHeight = 156;
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.IntegralHeight = false;
            this.comboBox1.Items.AddRange(new object[] {
            "Today",
            "This Week",
            "This Month",
            "This Year",
            "Last Year",
            "Forever"});
            this.comboBox1.Location = new System.Drawing.Point(753, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(163, 24);
            this.comboBox1.TabIndex = 57;
            this.comboBox1.Text = "This Year";
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(786, 54);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(65, 22);
            this.button7.TabIndex = 59;
            this.button7.Text = "Save";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(857, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 22);
            this.button2.TabIndex = 60;
            this.button2.Text = "Print";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // winFormHtmlEditor1
            // 
            this.winFormHtmlEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.winFormHtmlEditor1.BtnPrint.Visible = false;
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
            this.winFormHtmlEditor1.Enabled = false;
            this.winFormHtmlEditor1.HeaderStyleContentElementID = "page_style";
            this.winFormHtmlEditor1.HorizontalScroll = null;
            this.winFormHtmlEditor1.Location = new System.Drawing.Point(15, 83);
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
            this.winFormHtmlEditor1.Size = new System.Drawing.Size(907, 586);
            this.winFormHtmlEditor1.SpellCheckOptions.CurlyUnderlineImageFilePath = null;
            dictionaryFileInfo1.AffixFilePath = null;
            dictionaryFileInfo1.DictionaryFilePath = null;
            dictionaryFileInfo1.EnableUserDictionary = true;
            dictionaryFileInfo1.UserDictionaryFilePath = null;
            this.winFormHtmlEditor1.SpellCheckOptions.DictionaryFile = dictionaryFileInfo1;
            this.winFormHtmlEditor1.SpellCheckOptions.WaitAlertMessage = "Searching next misspelled word..... (please wait)";
            this.winFormHtmlEditor1.TabIndex = 61;
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
            this.winFormHtmlEditor1.BtnSearch});
            this.winFormHtmlEditor1.Toolbar1.Location = new System.Drawing.Point(0, 0);
            this.winFormHtmlEditor1.Toolbar1.Name = "WinFormHtmlEditor_Toolbar1";
            this.winFormHtmlEditor1.Toolbar1.Size = new System.Drawing.Size(907, 29);
            this.winFormHtmlEditor1.Toolbar1.TabIndex = 0;
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
            this.winFormHtmlEditor1.Toolbar2.Location = new System.Drawing.Point(0, 29);
            this.winFormHtmlEditor1.Toolbar2.Name = "WinFormHtmlEditor_Toolbar2";
            this.winFormHtmlEditor1.Toolbar2.Size = new System.Drawing.Size(907, 29);
            this.winFormHtmlEditor1.Toolbar2.TabIndex = 0;
            this.winFormHtmlEditor1.ToolbarContextMenuStrip = null;
            // 
            // winFormHtmlEditor1.WinFormHtmlEditor_ToolbarFooter
            // 
            this.winFormHtmlEditor1.ToolbarFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.winFormHtmlEditor1.ToolbarFooter.Location = new System.Drawing.Point(0, 561);
            this.winFormHtmlEditor1.ToolbarFooter.Name = "WinFormHtmlEditor_ToolbarFooter";
            this.winFormHtmlEditor1.ToolbarFooter.Size = new System.Drawing.Size(907, 25);
            this.winFormHtmlEditor1.ToolbarFooter.TabIndex = 7;
            this.winFormHtmlEditor1.VerticalScroll = null;
            this.winFormHtmlEditor1.z__ignore = false;
            // 
            // Reporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 681);
            this.Controls.Add(this.winFormHtmlEditor1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReportCombo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ClinicCombo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reporting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporting";
            this.Load += new System.EventHandler(this.Reporting_Load);
            this.winFormHtmlEditor1.Toolbar1.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar1.PerformLayout();
            this.winFormHtmlEditor1.Toolbar2.ResumeLayout(false);
            this.winFormHtmlEditor1.Toolbar2.PerformLayout();
            this.winFormHtmlEditor1.ResumeLayout(false);
            this.winFormHtmlEditor1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ClinicCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox ReportCombo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button2;
        private SpiceLogic.WinHTMLEditor.WinForm.WinFormHtmlEditor winFormHtmlEditor1;
    }
}
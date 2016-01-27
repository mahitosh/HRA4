namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingRowExpandable
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneticTestingRowExpandable));
            this.imglstScroll = new System.Windows.Forms.ImageList(this.components);
            this.panelComboBox = new System.Windows.Forms.ComboBox();
            this.testYearComboBox = new System.Windows.Forms.ComboBox();
            this.testMonthComboBox = new System.Windows.Forms.ComboBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.resultsSummary = new System.Windows.Forms.TextBox();
            this.addResultButton = new System.Windows.Forms.Button();
            this.lblTop = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.datelabel = new System.Windows.Forms.Label();
            this.panelLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.expanderToggle = new System.Windows.Forms.Label();
            this.ResultSignificanceNegativeButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imglstScroll
            // 
            this.imglstScroll.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglstScroll.ImageStream")));
            this.imglstScroll.TransparentColor = System.Drawing.SystemColors.Control;
            this.imglstScroll.Images.SetKeyName(0, "icon_up.GIF");
            this.imglstScroll.Images.SetKeyName(1, "icon_down.GIF");
            // 
            // panelComboBox
            // 
            this.panelComboBox.DisplayMember = "value1";
            this.panelComboBox.DropDownHeight = 400;
            this.panelComboBox.DropDownWidth = 300;
            this.panelComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelComboBox.FormattingEnabled = true;
            this.panelComboBox.IntegralHeight = false;
            this.panelComboBox.Location = new System.Drawing.Point(106, 18);
            this.panelComboBox.MaxDropDownItems = 15;
            this.panelComboBox.Name = "panelComboBox";
            this.panelComboBox.Size = new System.Drawing.Size(223, 21);
            this.panelComboBox.TabIndex = 3;
            this.panelComboBox.ValueMember = "value1";
            this.panelComboBox.SelectionChangeCommitted += new System.EventHandler(this.panelComboBox_SelectionChangeCommitted);
            // 
            // testYearComboBox
            // 
            this.testYearComboBox.DisplayMember = "value1";
            this.testYearComboBox.DropDownHeight = 400;
            this.testYearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testYearComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testYearComboBox.FormattingEnabled = true;
            this.testYearComboBox.IntegralHeight = false;
            this.testYearComboBox.Location = new System.Drawing.Point(57, 18);
            this.testYearComboBox.MaxDropDownItems = 15;
            this.testYearComboBox.Name = "testYearComboBox";
            this.testYearComboBox.Size = new System.Drawing.Size(47, 21);
            this.testYearComboBox.TabIndex = 2;
            this.testYearComboBox.ValueMember = "value1";
            this.testYearComboBox.SelectedIndexChanged += new System.EventHandler(this.testYearComboBox_SelectedIndexChanged);
            // 
            // testMonthComboBox
            // 
            this.testMonthComboBox.DropDownHeight = 400;
            this.testMonthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.testMonthComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testMonthComboBox.FormattingEnabled = true;
            this.testMonthComboBox.IntegralHeight = false;
            this.testMonthComboBox.Items.AddRange(new object[] {
            "",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.testMonthComboBox.Location = new System.Drawing.Point(19, 18);
            this.testMonthComboBox.MaxDropDownItems = 13;
            this.testMonthComboBox.Name = "testMonthComboBox";
            this.testMonthComboBox.Size = new System.Drawing.Size(37, 21);
            this.testMonthComboBox.TabIndex = 1;
            this.testMonthComboBox.SelectedIndexChanged += new System.EventHandler(this.testMonthComboBox_SelectedIndexChanged);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(780, 17);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(20, 23);
            this.deleteButton.TabIndex = 7;
            this.deleteButton.Text = "X";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 44);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(842, 80);
            this.flowLayoutPanel1.TabIndex = 9;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlAdded);
            this.flowLayoutPanel1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.flowLayoutPanel1_ControlRemoved);
            // 
            // resultsSummary
            // 
            this.resultsSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsSummary.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultsSummary.Location = new System.Drawing.Point(412, 18);
            this.resultsSummary.Name = "resultsSummary";
            this.resultsSummary.ReadOnly = true;
            this.resultsSummary.Size = new System.Drawing.Size(296, 21);
            this.resultsSummary.TabIndex = 61;
            // 
            // addResultButton
            // 
            this.addResultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addResultButton.Enabled = false;
            this.addResultButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addResultButton.Location = new System.Drawing.Point(801, 17);
            this.addResultButton.Name = "addResultButton";
            this.addResultButton.Size = new System.Drawing.Size(20, 23);
            this.addResultButton.TabIndex = 8;
            this.addResultButton.Text = "+";
            this.addResultButton.UseVisualStyleBackColor = true;
            this.addResultButton.Click += new System.EventHandler(this.addResultButton_Click);
            // 
            // lblTop
            // 
            this.lblTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTop.BackColor = System.Drawing.Color.White;
            this.lblTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTop.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTop.ImageIndex = 0;
            this.lblTop.Location = new System.Drawing.Point(1, 15);
            this.lblTop.MinimumSize = new System.Drawing.Size(546, 26);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(843, 26);
            this.lblTop.TabIndex = 0;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(3, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(14, 21);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // datelabel
            // 
            this.datelabel.AutoSize = true;
            this.datelabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datelabel.Location = new System.Drawing.Point(-2, 1);
            this.datelabel.Name = "datelabel";
            this.datelabel.Size = new System.Drawing.Size(34, 13);
            this.datelabel.TabIndex = 66;
            this.datelabel.Text = "Date";
            // 
            // panelLabel
            // 
            this.panelLabel.AutoSize = true;
            this.panelLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelLabel.Location = new System.Drawing.Point(103, 1);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(38, 13);
            this.panelLabel.TabIndex = 67;
            this.panelLabel.Text = "Panel";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(332, 1);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(44, 13);
            this.statusLabel.TabIndex = 68;
            this.statusLabel.Text = "Status";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(412, 1);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(43, 13);
            this.resultLabel.TabIndex = 69;
            this.resultLabel.Text = "Result";
            // 
            // statusComboBox
            // 
            this.statusComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Items.AddRange(new object[] {
            "",
            "Pending",
            "Complete",
            "Complete, Pt Informed",
            "Cancelled"});
            this.statusComboBox.Location = new System.Drawing.Point(335, 18);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(71, 21);
            this.statusComboBox.TabIndex = 4;
            this.statusComboBox.SelectionChangeCommitted += new System.EventHandler(this.statusComboBox_SelectionChangeCommitted);
            // 
            // expanderToggle
            // 
            this.expanderToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expanderToggle.BackColor = System.Drawing.Color.White;
            this.expanderToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.expanderToggle.ImageIndex = 0;
            this.expanderToggle.ImageList = this.imglstScroll;
            this.expanderToggle.Location = new System.Drawing.Point(822, 18);
            this.expanderToggle.MinimumSize = new System.Drawing.Size(20, 20);
            this.expanderToggle.Name = "expanderToggle";
            this.expanderToggle.Size = new System.Drawing.Size(20, 20);
            this.expanderToggle.TabIndex = 72;
            this.expanderToggle.Click += new System.EventHandler(this.expanderToggle_Click);
            // 
            // ResultSignificanceNegativeButton
            // 
            this.ResultSignificanceNegativeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultSignificanceNegativeButton.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultSignificanceNegativeButton.Location = new System.Drawing.Point(412, 17);
            this.ResultSignificanceNegativeButton.Name = "ResultSignificanceNegativeButton";
            this.ResultSignificanceNegativeButton.Size = new System.Drawing.Size(294, 23);
            this.ResultSignificanceNegativeButton.TabIndex = 5;
            this.ResultSignificanceNegativeButton.Text = "Set Significance Neg.";
            this.ResultSignificanceNegativeButton.UseVisualStyleBackColor = true;
            this.ResultSignificanceNegativeButton.Visible = false;
            this.ResultSignificanceNegativeButton.Click += new System.EventHandler(this.ResultSignificanceNegativeButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(711, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Validated += new System.EventHandler(this.textBox1_Validated);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(712, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 75;
            this.label1.Text = "Accession";
            // 
            // GeneticTestingRowExpandable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ResultSignificanceNegativeButton);
            this.Controls.Add(this.expanderToggle);
            this.Controls.Add(this.statusComboBox);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.panelLabel);
            this.Controls.Add(this.datelabel);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.addResultButton);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.testYearComboBox);
            this.Controls.Add(this.testMonthComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.resultsSummary);
            this.Controls.Add(this.panelComboBox);
            this.Controls.Add(this.lblTop);
            this.MinimumSize = new System.Drawing.Size(550, 2);
            this.Name = "GeneticTestingRowExpandable";
            this.Size = new System.Drawing.Size(845, 123);
            this.Load += new System.EventHandler(this.ScrollablePanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imglstScroll;
        private System.Windows.Forms.ComboBox panelComboBox;
        private System.Windows.Forms.ComboBox testYearComboBox;
        private System.Windows.Forms.ComboBox testMonthComboBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox resultsSummary;
        private System.Windows.Forms.Button addResultButton;
        private System.Windows.Forms.Label lblTop;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label datelabel;
        private System.Windows.Forms.Label panelLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.Label expanderToggle;
        private System.Windows.Forms.Button ResultSignificanceNegativeButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

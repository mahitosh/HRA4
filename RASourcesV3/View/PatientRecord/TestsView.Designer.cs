namespace RiskApps3.View.PatientRecord
{
    partial class TestsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestsView));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mammographyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mRIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tVSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cA125ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fastDataListView1 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.button2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ValueLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mammographyToolStripMenuItem,
            this.mRIToolStripMenuItem,
            this.tVSToolStripMenuItem,
            this.cA125ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 92);
            // 
            // mammographyToolStripMenuItem
            // 
            this.mammographyToolStripMenuItem.Name = "mammographyToolStripMenuItem";
            this.mammographyToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.mammographyToolStripMenuItem.Text = "MammographyHxView";
            this.mammographyToolStripMenuItem.Click += new System.EventHandler(this.mammographyToolStripMenuItem_Click);
            // 
            // mRIToolStripMenuItem
            // 
            this.mRIToolStripMenuItem.Name = "mRIToolStripMenuItem";
            this.mRIToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.mRIToolStripMenuItem.Text = "MRI";
            this.mRIToolStripMenuItem.Click += new System.EventHandler(this.mRIToolStripMenuItem_Click);
            // 
            // tVSToolStripMenuItem
            // 
            this.tVSToolStripMenuItem.Name = "tVSToolStripMenuItem";
            this.tVSToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.tVSToolStripMenuItem.Text = "TVS";
            this.tVSToolStripMenuItem.Click += new System.EventHandler(this.tVSToolStripMenuItem_Click);
            // 
            // cA125ToolStripMenuItem
            // 
            this.cA125ToolStripMenuItem.Name = "cA125ToolStripMenuItem";
            this.cA125ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cA125ToolStripMenuItem.Text = "CA125";
            this.cA125ToolStripMenuItem.Click += new System.EventHandler(this.cA125ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(894, 582);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.fastDataListView1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 582);
            this.panel1.TabIndex = 27;
            // 
            // fastDataListView1
            // 
            this.fastDataListView1.AllColumns.Add(this.olvColumn1);
            this.fastDataListView1.AllColumns.Add(this.olvColumn2);
            this.fastDataListView1.AllColumns.Add(this.olvColumn5);
            this.fastDataListView1.AllColumns.Add(this.olvColumn4);
            this.fastDataListView1.AllColumns.Add(this.olvColumn3);
            this.fastDataListView1.AllowColumnReorder = true;
            this.fastDataListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fastDataListView1.CheckBoxes = false;
            this.fastDataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn5,
            this.olvColumn4,
            this.olvColumn3});
            this.fastDataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListView1.DataSource = null;
            this.fastDataListView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListView1.FullRowSelect = true;
            this.fastDataListView1.GridLines = true;
            this.fastDataListView1.HeaderWordWrap = true;
            this.fastDataListView1.HideSelection = false;
            this.fastDataListView1.Location = new System.Drawing.Point(4, 10);
            this.fastDataListView1.Name = "fastDataListView1";
            this.fastDataListView1.OwnerDraw = true;
            this.fastDataListView1.ShowCommandMenuOnRightClick = true;
            this.fastDataListView1.ShowGroups = false;
            this.fastDataListView1.ShowItemToolTips = true;
            this.fastDataListView1.Size = new System.Drawing.Size(248, 532);
            this.fastDataListView1.TabIndex = 5;
            this.fastDataListView1.UseCompatibleStateImageBehavior = false;
            this.fastDataListView1.UseFiltering = true;
            this.fastDataListView1.UseHotItem = true;
            this.fastDataListView1.UseOverlays = false;
            this.fastDataListView1.UseTranslucentHotItem = true;
            this.fastDataListView1.View = System.Windows.Forms.View.Details;
            this.fastDataListView1.VirtualMode = true;
            this.fastDataListView1.SelectionChanged += new System.EventHandler(this.fastDataListView1_SelectionChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "type";
            this.olvColumn1.Text = "Type";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "date";
            this.olvColumn2.Text = "Date";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "value";
            this.olvColumn5.Text = "Value";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "normal";
            this.olvColumn4.Text = "Result";
            this.olvColumn4.Width = 108;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "status";
            this.olvColumn3.Text = "Status";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(6, 548);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "New Report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.ValueLabel);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.comboBox1);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.comboBox3);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(627, 582);
            this.panel2.TabIndex = 29;
            this.panel2.Visible = false;
            // 
            // ValueLabel
            // 
            this.ValueLabel.AutoSize = true;
            this.ValueLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValueLabel.Location = new System.Drawing.Point(68, 4);
            this.ValueLabel.Name = "ValueLabel";
            this.ValueLabel.Size = new System.Drawing.Size(44, 16);
            this.ValueLabel.TabIndex = 30;
            this.ValueLabel.Text = "Value";
            this.ValueLabel.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Location = new System.Drawing.Point(9, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(611, 50);
            this.panel3.TabIndex = 29;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 143);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(617, 403);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            this.richTextBox1.Validated += new System.EventHandler(this.richTextBox1_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(8, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Report Text";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 16);
            this.label5.TabIndex = 26;
            this.label5.Text = "Type";
            this.label5.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "Normal",
            "Abnormal",
            "Inconclusive"});
            this.comboBox1.Location = new System.Drawing.Point(133, 45);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(129, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.Validated += new System.EventHandler(this.comboBox1_Validated);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(11, 45);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(102, 21);
            this.dateTimePicker1.TabIndex = 18;
            this.dateTimePicker1.Validated += new System.EventHandler(this.dateTimePicker1_Validated);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(11, 548);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "Delete Report";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(134, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Result";
            // 
            // comboBox3
            // 
            this.comboBox3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "",
            "Ordered",
            "Pending",
            "Complete",
            "Awaiting Authorization"});
            this.comboBox3.Location = new System.Drawing.Point(279, 45);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(197, 21);
            this.comboBox3.TabIndex = 23;
            this.comboBox3.Validated += new System.EventHandler(this.comboBox3_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(276, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Date";
            // 
            // TestsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 588);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tests";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestsView_FormClosing);
            this.Load += new System.EventHandler(this.TestsView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mammographyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mRIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tVSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cA125ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private BrightIdeasSoftware.FastDataListView fastDataListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label ValueLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}
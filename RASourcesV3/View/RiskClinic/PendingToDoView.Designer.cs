﻿using RiskApps3.Controllers;

namespace RiskApps3.View.RiskClinic
{
    partial class PendingToDoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingToDoView));
            this.textBoxFilterData = new System.Windows.Forms.TextBox();
            this.buttonClearTextSearch = new System.Windows.Forms.Button();
            this.buttonApplyTextSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fastDataListView1 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.patientRecordHeader1 = new RiskApps3.View.PatientRecord.PatientRecordHeader();
            this.NoApptSelectedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFilterData
            // 
            this.textBoxFilterData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFilterData.Location = new System.Drawing.Point(10, 3);
            this.textBoxFilterData.Name = "textBoxFilterData";
            this.textBoxFilterData.Size = new System.Drawing.Size(237, 21);
            this.textBoxFilterData.TabIndex = 9;
            // 
            // buttonClearTextSearch
            // 
            this.buttonClearTextSearch.AccessibleDescription = "RIGHT(SUSER_SNAME(), LEN(SUSER_SNAME()) - CHARINDEX(\'\\\',SUSER_SNAME()))";
            this.buttonClearTextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearTextSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClearTextSearch.Location = new System.Drawing.Point(313, 1);
            this.buttonClearTextSearch.Name = "buttonClearTextSearch";
            this.buttonClearTextSearch.Size = new System.Drawing.Size(55, 23);
            this.buttonClearTextSearch.TabIndex = 16;
            this.buttonClearTextSearch.Text = "Clear";
            this.buttonClearTextSearch.UseVisualStyleBackColor = true;
            this.buttonClearTextSearch.Click += new System.EventHandler(this.buttonClearTextSearch_Click);
            // 
            // buttonApplyTextSearch
            // 
            this.buttonApplyTextSearch.AccessibleDescription = "RIGHT(SUSER_SNAME(), LEN(SUSER_SNAME()) - CHARINDEX(\'\\\',SUSER_SNAME()))";
            this.buttonApplyTextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyTextSearch.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApplyTextSearch.Location = new System.Drawing.Point(253, 1);
            this.buttonApplyTextSearch.Name = "buttonApplyTextSearch";
            this.buttonApplyTextSearch.Size = new System.Drawing.Size(54, 23);
            this.buttonApplyTextSearch.TabIndex = 15;
            this.buttonApplyTextSearch.Text = "Search";
            this.buttonApplyTextSearch.UseVisualStyleBackColor = true;
            this.buttonApplyTextSearch.Click += new System.EventHandler(this.buttonApplyTextSearch_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // fastDataListView1
            // 
            this.fastDataListView1.AllColumns.Add(this.olvColumn1);
            this.fastDataListView1.AllColumns.Add(this.olvColumn2);
            this.fastDataListView1.AllColumns.Add(this.olvColumn4);
            this.fastDataListView1.AllColumns.Add(this.olvColumn6);
            this.fastDataListView1.AllColumns.Add(this.olvColumn5);
            this.fastDataListView1.AllColumns.Add(this.olvColumn3);
            this.fastDataListView1.AllowColumnReorder = true;
            this.fastDataListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fastDataListView1.CheckBoxes = false;
            this.fastDataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn4,
            this.olvColumn6,
            this.olvColumn5,
            this.olvColumn3});
            this.fastDataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListView1.DataSource = null;
            this.fastDataListView1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListView1.FullRowSelect = true;
            this.fastDataListView1.GridLines = true;
            this.fastDataListView1.HeaderWordWrap = true;
            this.fastDataListView1.HideSelection = false;
            this.fastDataListView1.Location = new System.Drawing.Point(10, 30);
            this.fastDataListView1.Name = "fastDataListView1";
            this.fastDataListView1.OwnerDraw = true;
            this.fastDataListView1.ShowCommandMenuOnRightClick = true;
            this.fastDataListView1.ShowGroups = false;
            this.fastDataListView1.ShowItemToolTips = true;
            this.fastDataListView1.Size = new System.Drawing.Size(358, 414);
            this.fastDataListView1.TabIndex = 4;
            this.fastDataListView1.UseCompatibleStateImageBehavior = false;
            this.fastDataListView1.UseFiltering = true;
            this.fastDataListView1.UseHotItem = true;
            this.fastDataListView1.UseOverlays = false;
            this.fastDataListView1.UseTranslucentHotItem = true;
            this.fastDataListView1.View = System.Windows.Forms.View.Details;
            this.fastDataListView1.VirtualMode = true;
            this.fastDataListView1.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.fastDataListView1_FormatRow);
            this.fastDataListView1.SelectedIndexChanged += new System.EventHandler(this.ListViewDataSetSelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "unitnum";
            this.olvColumn1.Text = "MRN";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "patientname";
            this.olvColumn2.Text = "Name";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "PendingAssignedTo";
            this.olvColumn4.Text = "Assigned To";
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "PendingAssignedLatestIssuedDate";
            this.olvColumn6.AspectToStringFormat = "{0:d}";
            this.olvColumn6.Text = "Date";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "PendingTaskCount";
            this.olvColumn5.Text = "# of Tasks";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "PendingAssignedBy";
            this.olvColumn3.Text = "Assigned by";
            this.olvColumn3.Width = 136;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Panel2.Controls.Add(this.patientRecordHeader1);
            this.splitContainer1.Panel2.Controls.Add(this.NoApptSelectedLabel);
            this.splitContainer1.Panel2.Resize += new System.EventHandler(this.Panel2_Resize);
            this.splitContainer1.Size = new System.Drawing.Size(992, 571);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.loadingCircle1);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.textBoxFilterData);
            this.panel1.Controls.Add(this.buttonApplyTextSearch);
            this.panel1.Controls.Add(this.buttonClearTextSearch);
            this.panel1.Controls.Add(this.fastDataListView1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 571);
            this.panel1.TabIndex = 18;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.SystemColors.ControlDark;
            this.loadingCircle1.Enabled = false;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(153, 187);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(88, 68);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 41;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.81633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.81633F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.122449F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button5, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.button4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 492);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 72);
            this.tableLayoutPanel1.TabIndex = 39;
            // 
            // button1
            // 
            this.button1.AccessibleDescription = "";
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 66);
            this.button1.TabIndex = 33;
            this.button1.Text = "Risk Clinic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button5
            // 
            this.button5.AccessibleDescription = "";
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(349, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(20, 65);
            this.button5.TabIndex = 37;
            this.button5.Text = "Orders";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.AccessibleDescription = "";
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(327, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(16, 65);
            this.button4.TabIndex = 36;
            this.button4.Text = "New Task";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button2
            // 
            this.button2.AccessibleDescription = "";
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(305, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(16, 65);
            this.button2.TabIndex = 34;
            this.button2.Text = "Notes &&\r\nTasks";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.AccessibleDescription = "";
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(154, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(145, 66);
            this.button3.TabIndex = 35;
            this.button3.Text = "Pedigree";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Location = new System.Drawing.Point(3, 81);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(610, 490);
            this.tabControl.TabIndex = 22;
            // 
            // patientRecordHeader1
            // 
            this.patientRecordHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patientRecordHeader1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.patientRecordHeader1.Collapsed = false;
            this.patientRecordHeader1.Location = new System.Drawing.Point(3, 5);
            this.patientRecordHeader1.Name = "patientRecordHeader1";
            this.patientRecordHeader1.Size = new System.Drawing.Size(607, 75);
            this.patientRecordHeader1.TabIndex = 19;
            // 
            // NoApptSelectedLabel
            // 
            this.NoApptSelectedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NoApptSelectedLabel.AutoSize = true;
            this.NoApptSelectedLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoApptSelectedLabel.Location = new System.Drawing.Point(220, 279);
            this.NoApptSelectedLabel.Name = "NoApptSelectedLabel";
            this.NoApptSelectedLabel.Size = new System.Drawing.Size(228, 13);
            this.NoApptSelectedLabel.TabIndex = 21;
            this.NoApptSelectedLabel.Text = "Choose an item from the list at the left.";
            // 
            // PendingToDoView
            // 
            this.AcceptButton = this.buttonApplyTextSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1009, 595);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "PendingToDoView";
            this.Text = "Pending Tasks";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HighRiskFollowupView_FormClosing);
            this.Load += new System.EventHandler(this.PendingToDoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private BrightIdeasSoftware.FastDataListView fastDataListView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilterData;
        private System.Windows.Forms.Button buttonClearTextSearch;
        private System.Windows.Forms.Button buttonApplyTextSearch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private RiskApps3.View.PatientRecord.PatientRecordHeader patientRecordHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label NoApptSelectedLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabControl tabControl;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
    }
}
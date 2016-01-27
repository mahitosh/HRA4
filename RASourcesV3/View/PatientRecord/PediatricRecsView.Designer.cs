namespace RiskApps3.View.PatientRecord
{
    partial class PediatricRecsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PediatricRecsView));
            this.fastDataListView1 = new BrightIdeasSoftware.FastDataListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // fastDataListView1
            // 
            this.fastDataListView1.AllColumns.Add(this.olvColumn1);
            this.fastDataListView1.AllColumns.Add(this.olvColumn2);
            this.fastDataListView1.AllColumns.Add(this.olvColumn3);
            this.fastDataListView1.AllColumns.Add(this.olvColumn4);
            this.fastDataListView1.AllowColumnReorder = true;
            this.fastDataListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fastDataListView1.CheckBoxes = false;
            this.fastDataListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.fastDataListView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.fastDataListView1.DataSource = null;
            this.fastDataListView1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fastDataListView1.FullRowSelect = true;
            this.fastDataListView1.GridLines = true;
            this.fastDataListView1.HeaderWordWrap = true;
            this.fastDataListView1.HideSelection = false;
            this.fastDataListView1.Location = new System.Drawing.Point(12, 49);
            this.fastDataListView1.Name = "fastDataListView1";
            this.fastDataListView1.OwnerDraw = true;
            this.fastDataListView1.ShowCommandMenuOnRightClick = true;
            this.fastDataListView1.ShowGroups = false;
            this.fastDataListView1.ShowItemToolTips = true;
            this.fastDataListView1.Size = new System.Drawing.Size(954, 552);
            this.fastDataListView1.TabIndex = 5;
            this.fastDataListView1.UseCompatibleStateImageBehavior = false;
            this.fastDataListView1.UseFiltering = true;
            this.fastDataListView1.UseHotItem = true;
            this.fastDataListView1.UseOverlays = false;
            this.fastDataListView1.UseTranslucentHotItem = true;
            this.fastDataListView1.View = System.Windows.Forms.View.Details;
            this.fastDataListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "PediatricRule_Syndrome";
            this.olvColumn1.Text = "Syndrome";
            this.olvColumn1.Width = 269;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "PediatricRule_Action";
            this.olvColumn2.Text = "Action";
            this.olvColumn2.Width = 257;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "PediatricRule_Reason";
            this.olvColumn3.Text = "Reason";
            this.olvColumn3.Width = 362;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "PediatricRule_RuleID";
            this.olvColumn4.Text = "Rule";
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
            this.loadingCircle1.Location = new System.Drawing.Point(483, 309);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(70, 53);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 41;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(810, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 43;
            this.button1.Text = "Refresh Considerations";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PediatricRecsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 613);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.fastDataListView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PediatricRecsView";
            this.Text = "Considerations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PediatricRecsView_FormClosing);
            this.Load += new System.EventHandler(this.PediatricRecsView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fastDataListView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.FastDataListView fastDataListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}
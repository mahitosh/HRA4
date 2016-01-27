using RiskApps3.View.Common;
namespace RiskApps3.View.PatientRecord.Pedigree
{
    partial class PedigreeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedigreeForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._centerPedigreeButton = new System.Windows.Forms.ToolStripButton();
            this._snapToGrid = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gedcomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hL7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progenyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gedcomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckForOrganizing = new System.ComponentModel.BackgroundWorker();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.ZoomSlider = new MB.Controls.ColorSlider();
            this.pedigreeControl1 = new RiskApps3.View.PatientRecord.Pedigree.PedigreeControl();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.colorSlider1 = new MB.Controls.ColorSlider();
            this.pedigreeComment1 = new RiskApps3.View.PatientRecord.Pedigree.PedigreeComment();
            this.pedigreeTitleBlock1 = new RiskApps3.View.PatientRecord.Pedigree.PedigreeTitleBlock();
            this.pedigreeLegend1 = new RiskApps3.View.PatientRecord.Pedigree.PedigreeLegend(pedigreeTitleBlock1);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripButton3,
            this.toolStripButton15,
            this.toolStripSeparator4,
            this.toolStripButton12,
            this.toolStripSeparator7,
            this.toolStripButton5,
            this.toolStripSeparator5,
            this.toolStripButton6,
            this.toolStripSeparator2,
            this._centerPedigreeButton,
            this._snapToGrid,
            this.toolStripSeparator6,
            this.toolStripButton8,
            this.toolStripButton9,
            this.toolStripButton10,
            this.toolStripButton11,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton17,
            this.toolStripSeparator9,
            this.toolStripButton18,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator8,
            this.toolStripButton1,
            this.toolStripButton19,
            this.toolStripSeparator10,
            this.toolStripButton16,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(864, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.CheckOnClick = true;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(54, 22);
            this.toolStripButton3.Text = "Organize";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(54, 22);
            this.toolStripButton15.Text = "Relayout";
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.CheckOnClick = true;
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton12.Image = global::RiskApps3.Properties.Resources.BreakpointHS;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton12.Text = "Grab";
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton5.Image = global::RiskApps3.Properties.Resources.Edit_UndoHS;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "Undo";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.CheckOnClick = true;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(73, 22);
            this.toolStripButton6.Text = "Hide In-Laws";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton7
            // 
            this._centerPedigreeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._centerPedigreeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._centerPedigreeButton.Image = global::RiskApps3.Properties.Resources.Center;
            this._centerPedigreeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._centerPedigreeButton.Name = "_centerPedigreeButton";
            this._centerPedigreeButton.Size = new System.Drawing.Size(23, 22);
            this._centerPedigreeButton.Text = "Center Pedigree";
            this._centerPedigreeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this._centerPedigreeButton.Click += new System.EventHandler(this.CenterPedigreeButton_Click);
            // 
            // toolStripButton4
            // 
            this._snapToGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._snapToGrid.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._snapToGrid.Image = global::RiskApps3.Properties.Resources.Snap2Grid;
            this._snapToGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._snapToGrid.Name = "_snapToGrid";
            this._snapToGrid.Size = new System.Drawing.Size(23, 22);
            this._snapToGrid.Text = "Snap To Grid";
            this._snapToGrid.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton8.Image = global::RiskApps3.Properties.Resources.AlignObjectsCenteredVerticalHS;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton8.Text = "Align H";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolStripButton8.ToolTipText = "Align Horizontally";
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton9.Image = global::RiskApps3.Properties.Resources.AlignObjectsCenteredHorizontalHS;
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton9.Text = "Align V";
            this.toolStripButton9.Click += new System.EventHandler(this.toolStripButton9_Click);
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton10.Image = global::RiskApps3.Properties.Resources.DistributeHorizontal;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton10.Text = "Make horizontal spacing equal";
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton11.Image = global::RiskApps3.Properties.Resources.DistributeVertical;
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton11.Text = "Make vertical spacing equal";
            this.toolStripButton11.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton13.Image = global::RiskApps3.Properties.Resources.IncreaseHorSpacing;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton13.Text = "Increase horizontal spacing";
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton14.Image = global::RiskApps3.Properties.Resources.DecreaseHorSpacing;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton14.Text = "Decrease horizontal spacing";
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton17.Image = global::RiskApps3.Properties.Resources.SnapParents;
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton17.Text = "toolStripButton17";
            this.toolStripButton17.ToolTipText = "Bring parents together";
            this.toolStripButton17.Click += new System.EventHandler(this.toolStripButton17_Click_1);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(45, 22);
            this.toolStripButton18.Text = "By Age";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.Image = global::RiskApps3.Properties.Resources.gear_1;
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Settings";
            this.toolStripButton2.ToolTipText = "Settings";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton1.Text = "Copy";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton19.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton19.Image")));
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(33, 22);
            this.toolStripButton19.Text = "Print";
            this.toolStripButton19.Click += new System.EventHandler(this.toolStripButton19_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(62, 22);
            this.toolStripButton16.Text = "Link Family";
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.gedcomToolStripMenuItem1,
            this.otherToolStripMenuItem1});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(54, 22);
            this.toolStripDropDownButton1.Text = "Utilities";
            this.toolStripDropDownButton1.Visible = false;
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gedcomToolStripMenuItem,
            this.hL7ToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.importToolStripMenuItem.Text = "Imp / Exp";
            this.importToolStripMenuItem.Visible = false;
            // 
            // gedcomToolStripMenuItem
            // 
            this.gedcomToolStripMenuItem.Name = "gedcomToolStripMenuItem";
            this.gedcomToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.gedcomToolStripMenuItem.Text = "Gedcom";
            this.gedcomToolStripMenuItem.Click += new System.EventHandler(this.gedcomToolStripMenuItem_Click);
            // 
            // hL7ToolStripMenuItem
            // 
            this.hL7ToolStripMenuItem.Name = "hL7ToolStripMenuItem";
            this.hL7ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.hL7ToolStripMenuItem.Text = "Other";
            this.hL7ToolStripMenuItem.Click += new System.EventHandler(this.hL7ToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progenyToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Visible = false;
            // 
            // progenyToolStripMenuItem
            // 
            this.progenyToolStripMenuItem.Name = "progenyToolStripMenuItem";
            this.progenyToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.progenyToolStripMenuItem.Text = "Gedcom";
            this.progenyToolStripMenuItem.Click += new System.EventHandler(this.progenyToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // gedcomToolStripMenuItem1
            // 
            this.gedcomToolStripMenuItem1.Name = "gedcomToolStripMenuItem1";
            this.gedcomToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.gedcomToolStripMenuItem1.Text = "Gedcom";
            this.gedcomToolStripMenuItem1.Visible = false;
            this.gedcomToolStripMenuItem1.Click += new System.EventHandler(this.gedcomToolStripMenuItem1_Click);
            // 
            // otherToolStripMenuItem1
            // 
            this.otherToolStripMenuItem1.Name = "otherToolStripMenuItem1";
            this.otherToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
            this.otherToolStripMenuItem1.Text = "Imp/Exp";
            this.otherToolStripMenuItem1.Click += new System.EventHandler(this.otherToolStripMenuItem1_Click);
            // 
            // CheckForOrganizing
            // 
            this.CheckForOrganizing.WorkerReportsProgress = true;
            this.CheckForOrganizing.WorkerSupportsCancellation = true;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar1.LargeChange = 5;
            this.vScrollBar1.Location = new System.Drawing.Point(841, 25);
            this.vScrollBar1.Maximum = 200;
            this.vScrollBar1.Minimum = 1;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(23, 530);
            this.vScrollBar1.TabIndex = 5;
            this.vScrollBar1.Value = 100;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 5;
            this.hScrollBar1.Location = new System.Drawing.Point(-1, 555);
            this.hScrollBar1.Maximum = 200;
            this.hScrollBar1.Minimum = 1;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(844, 23);
            this.hScrollBar1.TabIndex = 6;
            this.hScrollBar1.Value = 100;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // ZoomSlider
            // 
            this.ZoomSlider.BackColor = System.Drawing.Color.White;
            this.ZoomSlider.BarInnerColor = System.Drawing.Color.Gainsboro;
            this.ZoomSlider.BarOuterColor = System.Drawing.Color.DarkGray;
            this.ZoomSlider.BarPenColor = System.Drawing.Color.Silver;
            this.ZoomSlider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.ZoomSlider.ElapsedInnerColor = System.Drawing.Color.Gainsboro;
            this.ZoomSlider.ElapsedOuterColor = System.Drawing.Color.DarkGray;
            this.ZoomSlider.LargeChange = ((uint)(5u));
            this.ZoomSlider.Location = new System.Drawing.Point(4, 29);
            this.ZoomSlider.Maximum = 300;
            this.ZoomSlider.Minimum = 1;
            this.ZoomSlider.Name = "ZoomSlider";
            this.ZoomSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ZoomSlider.Size = new System.Drawing.Size(20, 187);
            this.ZoomSlider.SmallChange = ((uint)(1u));
            this.ZoomSlider.TabIndex = 3;
            this.ZoomSlider.Text = "colorSlider1";
            this.ZoomSlider.ThumbInnerColor = System.Drawing.Color.RoyalBlue;
            this.ZoomSlider.ThumbOuterColor = System.Drawing.Color.DarkBlue;
            this.ZoomSlider.ThumbPenColor = System.Drawing.Color.Black;
            this.ZoomSlider.ThumbRoundRectSize = new System.Drawing.Size(15, 15);
            this.ZoomSlider.Value = 100;
            this.ZoomSlider.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ZoomSlider_Scroll);
            // 
            // pedigreeControl1
            // 
            this.pedigreeControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pedigreeControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pedigreeControl1.BackColor = System.Drawing.Color.White;
            this.pedigreeControl1.FrameRate = 35;
            this.pedigreeControl1.Location = new System.Drawing.Point(0, 0);
            this.pedigreeControl1.Name = "pedigreeControl1";
            this.pedigreeControl1.Size = new System.Drawing.Size(865, 578);
            this.pedigreeControl1.TabIndex = 0;
            this.pedigreeControl1.Load += new System.EventHandler(this.pedigreeControl1_Load);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(314, 231);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(54, 50);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 9;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // colorSlider1
            // 
            this.colorSlider1.BackColor = System.Drawing.Color.White;
            this.colorSlider1.BarInnerColor = System.Drawing.Color.Gainsboro;
            this.colorSlider1.BarOuterColor = System.Drawing.Color.DarkGray;
            this.colorSlider1.BarPenColor = System.Drawing.Color.Silver;
            this.colorSlider1.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.colorSlider1.ElapsedInnerColor = System.Drawing.Color.Gainsboro;
            this.colorSlider1.ElapsedOuterColor = System.Drawing.Color.DarkGray;
            this.colorSlider1.LargeChange = ((uint)(5u));
            this.colorSlider1.Location = new System.Drawing.Point(4, 249);
            this.colorSlider1.Maximum = 300;
            this.colorSlider1.Minimum = 30;
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.colorSlider1.Size = new System.Drawing.Size(20, 119);
            this.colorSlider1.SmallChange = ((uint)(1u));
            this.colorSlider1.TabIndex = 10;
            this.colorSlider1.Text = "colorSlider1";
            this.colorSlider1.ThumbInnerColor = System.Drawing.Color.RoyalBlue;
            this.colorSlider1.ThumbOuterColor = System.Drawing.Color.DarkBlue;
            this.colorSlider1.ThumbPenColor = System.Drawing.Color.Black;
            this.colorSlider1.ThumbRoundRectSize = new System.Drawing.Size(15, 15);
            this.colorSlider1.Value = 100;
            this.colorSlider1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.colorSlider1_Scroll);
            // 
            // pedigreeLegend1
            // 
            this.pedigreeLegend1.BackColor = System.Drawing.Color.White;
            this.pedigreeLegend1.Background = System.Drawing.Color.White;
            this.pedigreeLegend1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeLegend1.LegendFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeLegend1.LegendRadius = 15;
            this.pedigreeLegend1.Location = new System.Drawing.Point(220, 50);
            this.pedigreeLegend1.Name = "pedigreeLegend1";
            this.pedigreeLegend1.Size = new System.Drawing.Size(350, 100);
            this.pedigreeLegend1.TabIndex = 11;
            this.pedigreeLegend1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pedigreeLegend1_MouseUp);
            this.pedigreeLegend1.Resize += new System.EventHandler(this.pedigreeLegend1_Resize);
            // 
            // pedigreeComment1
            // 
            this.pedigreeComment1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pedigreeComment1.Background = System.Drawing.SystemColors.Window;
            this.pedigreeComment1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeComment1.CommentFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pedigreeComment1.Location = new System.Drawing.Point(40, 406);
            this.pedigreeComment1.Name = "pedigreeComment1";
            this.pedigreeComment1.Size = new System.Drawing.Size(228, 108);
            this.pedigreeComment1.TabIndex = 12;
            this.pedigreeComment1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pedigreeComment1_MouseUp);
            // 
            // pedigreeTitleBlock1
            // 
            this.pedigreeTitleBlock1.BackColor = System.Drawing.Color.White;
            this.pedigreeTitleBlock1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pedigreeTitleBlock1.DOB = "";
            this.pedigreeTitleBlock1.DOBVis = true;
            this.pedigreeTitleBlock1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pedigreeTitleBlock1.Location = new System.Drawing.Point(40, 50);
            this.pedigreeTitleBlock1.Margin = new System.Windows.Forms.Padding(5);
            this.pedigreeTitleBlock1.MRN = "";
            this.pedigreeTitleBlock1.MRNVis = true;
            this.pedigreeTitleBlock1.Name = "pedigreeTitleBlock1";
            this.pedigreeTitleBlock1.NameText = "";
            this.pedigreeTitleBlock1.NameVis = true;
            this.pedigreeTitleBlock1.Size = new System.Drawing.Size(150, 100);
            this.pedigreeTitleBlock1.Spacing = 3;
            this.pedigreeTitleBlock1.TabIndex = 13;
            this.pedigreeTitleBlock1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pedigreeTitleBlock1_MouseUp);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // PedigreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(864, 577);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.pedigreeComment1);
            this.Controls.Add(this.colorSlider1);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.ZoomSlider);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pedigreeTitleBlock1);
            this.Controls.Add(this.pedigreeLegend1);
            this.Controls.Add(this.pedigreeControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PedigreeForm";
            this.Text = "Pedigree";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PedigreeForm_FormClosing);
            this.Load += new System.EventHandler(this.PedigreeForm_Load);
            this.Resize += new System.EventHandler(this.PedigreeForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PedigreeControl pedigreeControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton _snapToGrid;
        private System.ComponentModel.BackgroundWorker CheckForOrganizing;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private MB.Controls.ColorSlider ZoomSlider;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.ToolStripButton _centerPedigreeButton;
        private MB.Controls.ColorSlider colorSlider1;
        private System.Windows.Forms.ToolStripButton toolStripButton8;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private PedigreeLegend pedigreeLegend1;
        private PedigreeComment pedigreeComment1;
        private PedigreeTitleBlock pedigreeTitleBlock1;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripButton toolStripButton18;
        private System.Windows.Forms.ToolStripButton toolStripButton19;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gedcomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hL7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progenyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.ToolStripMenuItem gedcomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem1;
    }
}
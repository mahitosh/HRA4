using RiskApps3.View.Common;
namespace RiskApps3.View.PatientRecord
{
    partial class FileUploadControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listViewMain = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.hintTextLabel = new RiskApps3.View.Common.OrientedTextLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button4 = new System.Windows.Forms.Button();
            this.orientedTextLabel1 = new RiskApps3.View.Common.OrientedTextLabel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.currentFilesWorker = new System.ComponentModel.BackgroundWorker();
            this.directorySearchWorker = new System.ComponentModel.BackgroundWorker();
            this.dragDropWorker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.newFilesworker = new System.ComponentModel.BackgroundWorker();
            this.CopyFilesWorker = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewMain
            // 
            this.listViewMain.AllowDrop = true;
            this.listViewMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewMain.LabelEdit = true;
            this.listViewMain.Location = new System.Drawing.Point(3, 50);
            this.listViewMain.Name = "listViewMain";
            this.listViewMain.Size = new System.Drawing.Size(357, 200);
            this.listViewMain.TabIndex = 6;
            this.listViewMain.UseCompatibleStateImageBehavior = false;
            this.listViewMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.listViewMain_DragDrop);
            this.listViewMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.listViewMain_DragEnter);
            this.listViewMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewMain_KeyPress);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 300;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Size";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date";
            this.columnHeader6.Width = 160;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(28, 224);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(3, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(357, 188);
            this.listView1.TabIndex = 11;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 360;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Modified";
            this.columnHeader3.Width = 160;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.hintTextLabel);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.cmdRemove);
            this.splitContainer1.Panel1.Controls.Add(this.listViewMain);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.progressBar1);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.orientedTextLabel1);
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel2MinSize = 30;
            this.splitContainer1.Size = new System.Drawing.Size(365, 502);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 17;
            // 
            // hintTextLabel
            // 
            this.hintTextLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.hintTextLabel.BackColor = System.Drawing.SystemColors.Window;
            this.hintTextLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hintTextLabel.Location = new System.Drawing.Point(97, 68);
            this.hintTextLabel.Name = "hintTextLabel";
            this.hintTextLabel.RotationAngle = 0;
            this.hintTextLabel.Size = new System.Drawing.Size(169, 24);
            this.hintTextLabel.TabIndex = 23;
            this.hintTextLabel.Text = "Drag New Documents Here";
            this.hintTextLabel.TextDirection = RiskApps3.View.Common.Direction.Clockwise;
            this.hintTextLabel.TextOrientation = RiskApps3.View.Common.Orientation.Rotate;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::RiskApps3.Properties.Resources.UploadButton;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Location = new System.Drawing.Point(3, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 38);
            this.button1.TabIndex = 24;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackgroundImage = global::RiskApps3.Properties.Resources.viewType;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Location = new System.Drawing.Point(276, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(38, 38);
            this.button2.TabIndex = 16;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdRemove.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRemove.Location = new System.Drawing.Point(320, 6);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(40, 38);
            this.cmdRemove.TabIndex = 5;
            this.cmdRemove.Text = "Clear";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(133, 9);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(175, 18);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 22;
            this.progressBar1.Visible = false;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(285, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "Collage";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // orientedTextLabel1
            // 
            this.orientedTextLabel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orientedTextLabel1.Location = new System.Drawing.Point(3, 6);
            this.orientedTextLabel1.Name = "orientedTextLabel1";
            this.orientedTextLabel1.RotationAngle = 0;
            this.orientedTextLabel1.Size = new System.Drawing.Size(94, 24);
            this.orientedTextLabel1.TabIndex = 19;
            this.orientedTextLabel1.Text = "Current Files";
            this.orientedTextLabel1.TextDirection = RiskApps3.View.Common.Direction.Clockwise;
            this.orientedTextLabel1.TextOrientation = RiskApps3.View.Common.Orientation.Rotate;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // currentFilesWorker
            // 
            this.currentFilesWorker.WorkerReportsProgress = true;
            this.currentFilesWorker.WorkerSupportsCancellation = true;
            this.currentFilesWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.currentFilesWorker_DoWork);
            this.currentFilesWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.currentFilesWorker_RunWorkerCompleted);
            this.currentFilesWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.currentFilesWorker_ProgressChanged);
            // 
            // directorySearchWorker
            // 
            this.directorySearchWorker.WorkerReportsProgress = true;
            this.directorySearchWorker.WorkerSupportsCancellation = true;
            this.directorySearchWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.directorySearchWorker_DoWork);
            this.directorySearchWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.directorySearchWorker_RunWorkerCompleted);
            this.directorySearchWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.directorySearchWorker_ProgressChanged);
            // 
            // dragDropWorker
            // 
            this.dragDropWorker.WorkerReportsProgress = true;
            this.dragDropWorker.WorkerSupportsCancellation = true;
            this.dragDropWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dragDropWorker_DoWork);
            this.dragDropWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.dragDropWorker_RunWorkerCompleted);
            this.dragDropWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.dragDropWorker_ProgressChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.emailFileToolStripMenuItem,
            this.printFileToolStripMenuItem,
            this.deleteFileToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 114);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // emailFileToolStripMenuItem
            // 
            this.emailFileToolStripMenuItem.Name = "emailFileToolStripMenuItem";
            this.emailFileToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.emailFileToolStripMenuItem.Text = "Email File";
            this.emailFileToolStripMenuItem.Click += new System.EventHandler(this.emailFileToolStripMenuItem_Click);
            // 
            // printFileToolStripMenuItem
            // 
            this.printFileToolStripMenuItem.Name = "printFileToolStripMenuItem";
            this.printFileToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.printFileToolStripMenuItem.Text = "Print File";
            this.printFileToolStripMenuItem.Visible = false;
            this.printFileToolStripMenuItem.Click += new System.EventHandler(this.printFileToolStripMenuItem_Click);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete File";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // newFilesworker
            // 
            this.newFilesworker.WorkerReportsProgress = true;
            this.newFilesworker.WorkerSupportsCancellation = true;
            this.newFilesworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.newFilesworker_DoWork);
            this.newFilesworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.newFilesworker_RunWorkerCompleted);
            this.newFilesworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.newFilesworker_ProgressChanged);
            // 
            // CopyFilesWorker
            // 
            this.CopyFilesWorker.WorkerReportsProgress = true;
            this.CopyFilesWorker.WorkerSupportsCancellation = true;
            this.CopyFilesWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CopyFilesWorker_DoWork);
            this.CopyFilesWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.CopyFilesWorker_RunWorkerCompleted);
            this.CopyFilesWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CopyFilesWorker_ProgressChanged);
            // 
            // FileUploadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(40, 40);
            this.Name = "FileUploadControl";
            this.Size = new System.Drawing.Size(371, 512);
            this.Load += new System.EventHandler(this.FileUploadControl_Load);
            this.MouseLeave += new System.EventHandler(this.FileUploadControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FileUploadControl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FileUploadControl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileUploadControl_MouseUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewMain;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private OrientedTextLabel orientedTextLabel1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button button2;
        private System.ComponentModel.BackgroundWorker currentFilesWorker;
        private System.ComponentModel.BackgroundWorker directorySearchWorker;
        private System.ComponentModel.BackgroundWorker dragDropWorker;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printFileToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem emailFileToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker newFilesworker;
        private OrientedTextLabel hintTextLabel;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker CopyFilesWorker;
    }
}

using RiskApps3.Controllers;
namespace RiskApps3.View.PatientRecord.PMH
{
    partial class PastMedicalHistoryView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PastMedicalHistoryView));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noLabel = new System.Windows.Forms.Label();
            this.relativeHeader1 = new RiskApps3.View.PatientRecord.FamilyHistory.RelativeHeader();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.label1 = new System.Windows.Forms.Label();
            this.diseaseGroupComboBox = new RiskApps3.View.Common.AutoSearchTextBox.AutoCompleteComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // noLabel
            // 
            this.noLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noLabel.AutoSize = true;
            this.noLabel.BackColor = System.Drawing.SystemColors.Control;
            this.noLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noLabel.Location = new System.Drawing.Point(289, 95);
            this.noLabel.Name = "noLabel";
            this.noLabel.Size = new System.Drawing.Size(65, 13);
            this.noLabel.TabIndex = 6;
            this.noLabel.Text = "No Diseases";
            // 
            // relativeHeader1
            // 
            this.relativeHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.relativeHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.relativeHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.relativeHeader1.Location = new System.Drawing.Point(2, 1);
            this.relativeHeader1.Name = "relativeHeader1";
            this.relativeHeader1.Size = new System.Drawing.Size(657, 22);
            this.relativeHeader1.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(658, 246);
            this.flowLayoutPanel1.TabIndex = 12;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(296, 125);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(44, 37);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 7;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(724, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Group:";
            // 
            // diseaseGroupComboBox
            // 
            this.diseaseGroupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.diseaseGroupComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diseaseGroupComboBox.FormattingEnabled = true;
            this.diseaseGroupComboBox.Location = new System.Drawing.Point(665, 30);
            this.diseaseGroupComboBox.Name = "diseaseGroupComboBox";
            this.diseaseGroupComboBox.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.diseaseGroupComboBox.PopupOffset = new System.Drawing.Point(12, 0);
            this.diseaseGroupComboBox.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.diseaseGroupComboBox.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.diseaseGroupComboBox.PopupWidth = 300;
            this.diseaseGroupComboBox.Size = new System.Drawing.Size(103, 21);
            this.diseaseGroupComboBox.TabIndex = 14;
            this.diseaseGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.diseaseGroupComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(665, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "New Disease";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.AddDiseaseButton_Click);
            // 
            // PastMedicalHistoryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(769, 287);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.relativeHeader1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.diseaseGroupComboBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.noLabel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PastMedicalHistoryView";
            this.Text = "Diseases";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PastMedicalHistoryView_FormClosing);
            this.Load += new System.EventHandler(this.PastMedicalHistoryView_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private RiskApps3.View.PatientRecord.FamilyHistory.RelativeHeader relativeHeader1;
        private System.Windows.Forms.Label noLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label label1;
        private RiskApps3.View.Common.AutoSearchTextBox.AutoCompleteComboBox diseaseGroupComboBox;
        private System.Windows.Forms.Button button1;
    }
}
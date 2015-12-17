namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneticTestingView));
            this.relativeHeader1 = new RiskApps3.View.PatientRecord.FamilyHistory.RelativeHeader();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.testGroupComboBox = new RiskApps3.View.Common.AutoSearchTextBox.AutoCompleteComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.noLabel = new System.Windows.Forms.Label();
            this.newTestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // relativeHeader1
            // 
            this.relativeHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.relativeHeader1.BackColor = System.Drawing.SystemColors.Control;
            this.relativeHeader1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.relativeHeader1.Location = new System.Drawing.Point(2, 1);
            this.relativeHeader1.Name = "relativeHeader1";
            this.relativeHeader1.Size = new System.Drawing.Size(652, 22);
            this.relativeHeader1.TabIndex = 0;
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(340, 122);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(84, 37);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 6;
            this.loadingCircle1.Text = "loadingCircle1";
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(653, 184);
            this.flowLayoutPanel1.TabIndex = 9;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // testGroupComboBox
            // 
            this.testGroupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testGroupComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testGroupComboBox.FormattingEnabled = true;
            this.testGroupComboBox.Location = new System.Drawing.Point(660, 30);
            this.testGroupComboBox.Name = "testGroupComboBox";
            this.testGroupComboBox.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testGroupComboBox.PopupOffset = new System.Drawing.Point(12, 0);
            this.testGroupComboBox.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.testGroupComboBox.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.testGroupComboBox.PopupWidth = 300;
            this.testGroupComboBox.Size = new System.Drawing.Size(103, 21);
            this.testGroupComboBox.TabIndex = 14;
            this.testGroupComboBox.SelectedIndexChanged += new System.EventHandler(this.testGroupComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(719, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Group:";
            // 
            // noLabel
            // 
            this.noLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.noLabel.AutoSize = true;
            this.noLabel.BackColor = System.Drawing.SystemColors.Control;
            this.noLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noLabel.Location = new System.Drawing.Point(338, 105);
            this.noLabel.Name = "noLabel";
            this.noLabel.Size = new System.Drawing.Size(88, 13);
            this.noLabel.TabIndex = 17;
            this.noLabel.Text = "No Genetic Tests";
            // 
            // newTestButton
            // 
            this.newTestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newTestButton.BackColor = System.Drawing.SystemColors.Control;
            this.newTestButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newTestButton.Location = new System.Drawing.Point(660, 57);
            this.newTestButton.Name = "newTestButton";
            this.newTestButton.Size = new System.Drawing.Size(103, 23);
            this.newTestButton.TabIndex = 18;
            this.newTestButton.Text = "New Test";
            this.newTestButton.UseVisualStyleBackColor = false;
            this.newTestButton.Click += new System.EventHandler(this.AddGeneticTest_Click);
            // 
            // GeneticTestingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(764, 223);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.testGroupComboBox);
            this.Controls.Add(this.newTestButton);
            this.Controls.Add(this.noLabel);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.relativeHeader1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GeneticTestingView";
            this.Text = "Genetic Testing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeneticTestingView_FormClosing);
            this.Load += new System.EventHandler(this.GeneticTestingView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RiskApps3.View.PatientRecord.FamilyHistory.RelativeHeader relativeHeader1;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private RiskApps3.View.Common.AutoSearchTextBox.AutoCompleteComboBox testGroupComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label noLabel;
        private System.Windows.Forms.Button newTestButton;
    }
}
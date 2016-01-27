namespace RiskApps3.View.Common
{
    partial class PedigreeModuleNavigation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedigreeModuleNavigation));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pedigreeButton = new RiskApps3.View.Common.BitmapButton();
            this.clinicButton = new RiskApps3.View.Common.BitmapButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.Controls.Add(this.pedigreeButton);
            this.flowLayoutPanel1.Controls.Add(this.clinicButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Enabled = false;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(747, 90);
            this.flowLayoutPanel1.TabIndex = 9;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pedigreeButton
            // 
            this.pedigreeButton.BorderColor = System.Drawing.Color.DarkBlue;
            this.pedigreeButton.FocusRectangleEnabled = true;
            this.pedigreeButton.Image = null;
            this.pedigreeButton.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.pedigreeButton.ImageBorderEnabled = true;
            this.pedigreeButton.ImageDropShadow = true;
            this.pedigreeButton.ImageFocused = null;
            this.pedigreeButton.ImageInactive = null;
            this.pedigreeButton.ImageMouseOver = null;
            this.pedigreeButton.ImageNormal = null;
            this.pedigreeButton.ImagePressed = null;
            this.pedigreeButton.InnerBorderColor = System.Drawing.Color.LightGray;
            this.pedigreeButton.InnerBorderColor_Focus = System.Drawing.Color.LightBlue;
            this.pedigreeButton.InnerBorderColor_MouseOver = System.Drawing.Color.Gold;
            this.pedigreeButton.Location = new System.Drawing.Point(4, 4);
            this.pedigreeButton.Margin = new System.Windows.Forms.Padding(4);
            this.pedigreeButton.Name = "pedigreeButton";
            this.pedigreeButton.OffsetPressedContent = true;
            this.pedigreeButton.Size = new System.Drawing.Size(75, 76);
            this.pedigreeButton.StretchImage = true;
            this.pedigreeButton.TabIndex = 10;
            this.pedigreeButton.Text = "Pedigree";
            this.pedigreeButton.TextDropShadow = true;
            this.pedigreeButton.UseVisualStyleBackColor = true;
            this.pedigreeButton.Click += new System.EventHandler(this.pedigreeButton_Click);
            // 
            // clinicButton
            // 
            this.clinicButton.BorderColor = System.Drawing.Color.DarkBlue;
            this.clinicButton.FocusRectangleEnabled = true;
            this.clinicButton.Image = null;
            this.clinicButton.ImageBorderColor = System.Drawing.Color.Chocolate;
            this.clinicButton.ImageBorderEnabled = true;
            this.clinicButton.ImageDropShadow = true;
            this.clinicButton.ImageFocused = null;
            this.clinicButton.ImageInactive = null;
            this.clinicButton.ImageMouseOver = null;
            this.clinicButton.ImageNormal = null;
            this.clinicButton.ImagePressed = null;
            this.clinicButton.InnerBorderColor = System.Drawing.Color.LightGray;
            this.clinicButton.InnerBorderColor_Focus = System.Drawing.Color.LightBlue;
            this.clinicButton.InnerBorderColor_MouseOver = System.Drawing.Color.Gold;
            this.clinicButton.Location = new System.Drawing.Point(87, 4);
            this.clinicButton.Margin = new System.Windows.Forms.Padding(4);
            this.clinicButton.Name = "clinicButton";
            this.clinicButton.OffsetPressedContent = true;
            this.clinicButton.Size = new System.Drawing.Size(75, 76);
            this.clinicButton.StretchImage = true;
            this.clinicButton.TabIndex = 11;
            this.clinicButton.Text = "Clinic";
            this.clinicButton.TextDropShadow = true;
            this.clinicButton.UseVisualStyleBackColor = true;
            this.clinicButton.Click += new System.EventHandler(this.clinicButton_Click);
            // 
            // PedigreeModuleNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 90);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PedigreeModuleNavigation";
            this.Text = "Navigation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientNavigation_FormClosing);
            this.Load += new System.EventHandler(this.PatientNavigation_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private BitmapButton pedigreeButton;
        private BitmapButton clinicButton;
    }
}
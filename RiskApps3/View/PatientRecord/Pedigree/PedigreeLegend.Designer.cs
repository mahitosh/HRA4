namespace RiskApps3.View.PatientRecord.Pedigree
{
    partial class PedigreeLegend
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
            this.flowLayoutPanel = new RiskApps3.View.PatientRecord.Pedigree.PedigreeFlowLayoutPanel();
            this.legendTitleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 26);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(350, 84);
            this.flowLayoutPanel.TabIndex = 0;
            // 
            // legendTitleLabel
            // 
            this.legendTitleLabel.AutoSize = true;
            this.legendTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.legendTitleLabel.Location = new System.Drawing.Point(4, 4);
            this.legendTitleLabel.Name = "legendTitleLabel";
            this.legendTitleLabel.Size = new System.Drawing.Size(55, 15);
            this.legendTitleLabel.TabIndex = 1;
            this.legendTitleLabel.Text = "Legend";
            // 
            // PedigreeLegend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.legendTitleLabel);
            this.Controls.Add(this.flowLayoutPanel);
            this.Name = "PedigreeLegend";
            this.Size = new System.Drawing.Size(350, 110);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PedigreeFlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label legendTitleLabel;

    }
}

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingASOResultRowHeader
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
            this.asLabel = new System.Windows.Forms.Label();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.foundLabel = new System.Windows.Forms.Label();
            this.geneLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // asLabel
            // 
            this.asLabel.AutoSize = true;
            this.asLabel.BackColor = System.Drawing.SystemColors.Control;
            this.asLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asLabel.Location = new System.Drawing.Point(351, 2);
            this.asLabel.Name = "asLabel";
            this.asLabel.Size = new System.Drawing.Size(74, 13);
            this.asLabel.TabIndex = 81;
            this.asLabel.Text = "Allelic State";
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.BackColor = System.Drawing.SystemColors.Control;
            this.commentsLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentsLabel.Location = new System.Drawing.Point(433, 2);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(68, 13);
            this.commentsLabel.TabIndex = 79;
            this.commentsLabel.Text = "Comments";
            // 
            // foundLabel
            // 
            this.foundLabel.AutoSize = true;
            this.foundLabel.BackColor = System.Drawing.SystemColors.Control;
            this.foundLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foundLabel.Location = new System.Drawing.Point(236, 2);
            this.foundLabel.Name = "foundLabel";
            this.foundLabel.Size = new System.Drawing.Size(41, 13);
            this.foundLabel.TabIndex = 78;
            this.foundLabel.Text = "Found";
            // 
            // geneLabel
            // 
            this.geneLabel.AutoSize = true;
            this.geneLabel.BackColor = System.Drawing.SystemColors.Control;
            this.geneLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneLabel.Location = new System.Drawing.Point(-3, 2);
            this.geneLabel.Name = "geneLabel";
            this.geneLabel.Size = new System.Drawing.Size(98, 13);
            this.geneLabel.TabIndex = 76;
            this.geneLabel.Text = "Known Mutation";
            // 
            // GeneticTestingASOResultRowHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.asLabel);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.foundLabel);
            this.Controls.Add(this.geneLabel);
            this.MinimumSize = new System.Drawing.Size(550, 15);
            this.Name = "GeneticTestingASOResultRowHeader";
            this.Size = new System.Drawing.Size(550, 15);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label asLabel;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.Label foundLabel;
        private System.Windows.Forms.Label geneLabel;
    }
}

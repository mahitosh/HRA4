namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingResultRowHeader
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
            this.aaLabel = new System.Windows.Forms.Label();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.foundLabel = new System.Windows.Forms.Label();
            this.dnaLabel = new System.Windows.Forms.Label();
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
            // aaLabel
            // 
            this.aaLabel.AutoSize = true;
            this.aaLabel.BackColor = System.Drawing.SystemColors.Control;
            this.aaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aaLabel.Location = new System.Drawing.Point(149, 2);
            this.aaLabel.Name = "aaLabel";
            this.aaLabel.Size = new System.Drawing.Size(68, 13);
            this.aaLabel.TabIndex = 80;
            this.aaLabel.Text = "AA Change";
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
            // sigLabel
            // 
            this.foundLabel.AutoSize = true;
            this.foundLabel.BackColor = System.Drawing.SystemColors.Control;
            this.foundLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foundLabel.Location = new System.Drawing.Point(236, 2);
            this.foundLabel.Name = "sigLabel";
            this.foundLabel.Size = new System.Drawing.Size(74, 13);
            this.foundLabel.TabIndex = 78;
            this.foundLabel.Text = "Significance";
            // 
            // dnaLabel
            // 
            this.dnaLabel.AutoSize = true;
            this.dnaLabel.BackColor = System.Drawing.SystemColors.Control;
            this.dnaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dnaLabel.Location = new System.Drawing.Point(59, 2);
            this.dnaLabel.Name = "dnaLabel";
            this.dnaLabel.Size = new System.Drawing.Size(75, 13);
            this.dnaLabel.TabIndex = 77;
            this.dnaLabel.Text = "DNA Change";
            // 
            // geneLabel
            // 
            this.geneLabel.AutoSize = true;
            this.geneLabel.BackColor = System.Drawing.SystemColors.Control;
            this.geneLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneLabel.Location = new System.Drawing.Point(-3, 2);
            this.geneLabel.Name = "geneLabel";
            this.geneLabel.Size = new System.Drawing.Size(36, 13);
            this.geneLabel.TabIndex = 76;
            this.geneLabel.Text = "Gene";
            // 
            // GeneticTestingResultRowHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.asLabel);
            this.Controls.Add(this.aaLabel);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.foundLabel);
            this.Controls.Add(this.dnaLabel);
            this.Controls.Add(this.geneLabel);
            this.MinimumSize = new System.Drawing.Size(550, 15);
            this.Name = "GeneticTestingResultRowHeader";
            this.Size = new System.Drawing.Size(550, 15);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label asLabel;
        private System.Windows.Forms.Label aaLabel;
        private System.Windows.Forms.Label commentsLabel;
        private System.Windows.Forms.Label dnaLabel;
        private System.Windows.Forms.Label geneLabel;
        private System.Windows.Forms.Label foundLabel;
    }
}

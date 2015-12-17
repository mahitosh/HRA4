namespace RiskApps3.View.PatientRecord.Risk
{
    partial class RelativeToConsiderRow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RelationshipLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.BrcaLabel = new System.Windows.Forms.Label();
            this.testingWillingnessComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // RelationshipLabel
            // 
            this.RelationshipLabel.AutoSize = true;
            this.RelationshipLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RelationshipLabel.Location = new System.Drawing.Point(53, 9);
            this.RelationshipLabel.Name = "RelationshipLabel";
            this.RelationshipLabel.Size = new System.Drawing.Size(65, 13);
            this.RelationshipLabel.TabIndex = 7;
            this.RelationshipLabel.Text = "Relationship";
            this.RelationshipLabel.Click += new System.EventHandler(this.RelationshipLabel_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(141, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(34, 13);
            this.NameLabel.TabIndex = 6;
            this.NameLabel.Text = "Name";
            this.NameLabel.Visible = false;
            this.NameLabel.Click += new System.EventHandler(this.NameLabel_Click);
            // 
            // BrcaLabel
            // 
            this.BrcaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrcaLabel.Location = new System.Drawing.Point(3, 9);
            this.BrcaLabel.Name = "BrcaLabel";
            this.BrcaLabel.Size = new System.Drawing.Size(44, 16);
            this.BrcaLabel.TabIndex = 9;
            this.BrcaLabel.Text = "00.0%";
            this.BrcaLabel.Click += new System.EventHandler(this.BrcaLabel_Click);
            // 
            // testingWillingnessComboBox
            // 
            this.testingWillingnessComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.testingWillingnessComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testingWillingnessComboBox.FormattingEnabled = true;
            this.testingWillingnessComboBox.Location = new System.Drawing.Point(134, 6);
            this.testingWillingnessComboBox.Name = "testingWillingnessComboBox";
            this.testingWillingnessComboBox.Size = new System.Drawing.Size(177, 21);
            this.testingWillingnessComboBox.TabIndex = 8;
            this.testingWillingnessComboBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.testingWillingnessComboBox_MouseClick);
            this.testingWillingnessComboBox.SelectionChangeCommitted += new System.EventHandler(this.testingWillingnessComboBox_SelectedChangeCommitted);
            // 
            // RelativeToConsiderRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.RelationshipLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.BrcaLabel);
            this.Controls.Add(this.testingWillingnessComboBox);
            this.Name = "RelativeToConsiderRow";
            this.Size = new System.Drawing.Size(314, 35);
            this.Load += new System.EventHandler(this.RelativeToConsiderRow_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RelativeRow_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label RelationshipLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label BrcaLabel;
        private System.Windows.Forms.ComboBox testingWillingnessComboBox;
    }
}

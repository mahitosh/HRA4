namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    partial class ShortRelativeHeader
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
            this.name = new System.Windows.Forms.Label();
            this.relationship = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(3, 4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(39, 13);
            this.name.TabIndex = 1;
            this.name.Text = "name";
            // 
            // relationship
            // 
            this.relationship.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.relationship.AutoSize = true;
            this.relationship.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.relationship.Location = new System.Drawing.Point(147, 4);
            this.relationship.Name = "relationship";
            this.relationship.Size = new System.Drawing.Size(74, 13);
            this.relationship.TabIndex = 3;
            this.relationship.Text = "relationship";
            // 
            // ShortRelativeHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.relationship);
            this.Controls.Add(this.name);
            this.Name = "ShortRelativeHeader";
            this.Size = new System.Drawing.Size(224, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label relationship;
    }
}

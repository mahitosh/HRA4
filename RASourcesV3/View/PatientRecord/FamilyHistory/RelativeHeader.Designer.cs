namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    partial class RelativeHeader
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
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.Label();
            this.relationship = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bloodline = new System.Windows.Forms.Label();
            this.bloodlineHeader = new System.Windows.Forms.Label();
            this.age = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(-2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name.Location = new System.Drawing.Point(36, 4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(39, 13);
            this.name.TabIndex = 1;
            this.name.Text = "name";
            // 
            // relationship
            // 
            this.relationship.AutoSize = true;
            this.relationship.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.relationship.Location = new System.Drawing.Point(231, 4);
            this.relationship.Name = "relationship";
            this.relationship.Size = new System.Drawing.Size(74, 13);
            this.relationship.TabIndex = 3;
            this.relationship.Text = "relationship";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(165, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Relationship:";
            // 
            // bloodline
            // 
            this.bloodline.AutoSize = true;
            this.bloodline.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bloodline.Location = new System.Drawing.Point(392, 4);
            this.bloodline.Name = "bloodline";
            this.bloodline.Size = new System.Drawing.Size(58, 13);
            this.bloodline.TabIndex = 5;
            this.bloodline.Text = "bloodline";
            // 
            // bloodlineHeader
            // 
            this.bloodlineHeader.AutoSize = true;
            this.bloodlineHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bloodlineHeader.Location = new System.Drawing.Point(333, 4);
            this.bloodlineHeader.Name = "bloodlineHeader";
            this.bloodlineHeader.Size = new System.Drawing.Size(53, 13);
            this.bloodlineHeader.TabIndex = 4;
            this.bloodlineHeader.Text = "Bloodline:";
            // 
            // age
            // 
            this.age.AutoSize = true;
            this.age.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.age.Location = new System.Drawing.Point(494, 4);
            this.age.Name = "age";
            this.age.Size = new System.Drawing.Size(28, 13);
            this.age.TabIndex = 7;
            this.age.Text = "age";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(462, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Age:";
            // 
            // RelativeHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.age);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bloodline);
            this.Controls.Add(this.bloodlineHeader);
            this.Controls.Add(this.relationship);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Name = "RelativeHeader";
            this.Size = new System.Drawing.Size(549, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label relationship;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label bloodline;
        private System.Windows.Forms.Label bloodlineHeader;
        private System.Windows.Forms.Label age;
        private System.Windows.Forms.Label label5;
    }
}

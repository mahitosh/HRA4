namespace RiskApps3.View.PatientRecord.Communication
{
    partial class PendingTaskSummaryRow
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
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.orientedTextLabel1 = new RiskApps3.View.Common.OrientedTextLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            this.label1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(4, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(69, 58);
            this.panel1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "author";
            this.label4.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Text";
            this.label3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            // 
            // orientedTextLabel1
            // 
            this.orientedTextLabel1.Location = new System.Drawing.Point(78, 3);
            this.orientedTextLabel1.Name = "orientedTextLabel1";
            this.orientedTextLabel1.RotationAngle = 90;
            this.orientedTextLabel1.Size = new System.Drawing.Size(20, 65);
            this.orientedTextLabel1.TabIndex = 5;
            this.orientedTextLabel1.Text = "orientedTextLabel1";
            this.orientedTextLabel1.TextDirection = RiskApps3.View.Common.Direction.Clockwise;
            this.orientedTextLabel1.TextOrientation = RiskApps3.View.Common.Orientation.Rotate;
            this.orientedTextLabel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            // 
            // PendingTaskSummaryRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.orientedTextLabel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "PendingTaskSummaryRow";
            this.Size = new System.Drawing.Size(592, 71);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PendingTaskSummaryRow_MouseDoubleClick);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private RiskApps3.View.Common.OrientedTextLabel orientedTextLabel1;
        private System.Windows.Forms.Label label3;
    }
}

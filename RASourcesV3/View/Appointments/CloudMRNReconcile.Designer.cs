namespace RiskApps3.View.Appointments
{
    partial class CloudMRNReconcile
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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblMRN = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtMRN = new System.Windows.Forms.TextBox();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.Location = new System.Drawing.Point(273, 26);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(225, 25);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Verify Name and MRN";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(695, 386);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(568, 386);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(102, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "Okay";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(183, 67);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(58, 20);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Name:";
            // 
            // lblMRN
            // 
            this.lblMRN.AutoSize = true;
            this.lblMRN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMRN.Location = new System.Drawing.Point(183, 113);
            this.lblMRN.Name = "lblMRN";
            this.lblMRN.Size = new System.Drawing.Size(52, 20);
            this.lblMRN.TabIndex = 4;
            this.lblMRN.Text = "MRN:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(247, 67);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(263, 19);
            this.txtName.TabIndex = 5;
            // 
            // txtMRN
            // 
            this.txtMRN.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMRN.Location = new System.Drawing.Point(247, 107);
            this.txtMRN.Name = "txtMRN";
            this.txtMRN.Size = new System.Drawing.Size(263, 26);
            this.txtMRN.TabIndex = 6;
            // 
            // txtInstructions
            // 
            this.txtInstructions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInstructions.Location = new System.Drawing.Point(70, 166);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.ReadOnly = true;
            this.txtInstructions.Size = new System.Drawing.Size(727, 196);
            this.txtInstructions.TabIndex = 7;
            this.txtInstructions.Text = "Patient Entered MRN Above.  If Not Correct Enter New MRN.";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CloudMRNReconcile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 441);
            this.Controls.Add(this.txtInstructions);
            this.Controls.Add(this.txtMRN);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblMRN);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblInstructions);
            this.Name = "CloudMRNReconcile";
            this.Text = "CloudMRNReconcile";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMRN;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMRN;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
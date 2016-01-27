namespace RiskApps3.View.Appointments
{
    partial class ChangeMrnPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeMrnPopup));
            this.label5 = new System.Windows.Forms.Label();
            this.unitnumTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.newUnitnumTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label5.Location = new System.Drawing.Point(15, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 16);
            this.label5.TabIndex = 59;
            this.label5.Text = "Current Unit Number:";
            // 
            // unitnumTextBox
            // 
            this.unitnumTextBox.Enabled = false;
            this.unitnumTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.unitnumTextBox.Location = new System.Drawing.Point(166, 37);
            this.unitnumTextBox.Name = "unitnumTextBox";
            this.unitnumTextBox.Size = new System.Drawing.Size(177, 23);
            this.unitnumTextBox.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(56, 162);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(147, 31);
            this.cancelButton.TabIndex = 108;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(232, 162);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(183, 31);
            this.button7.TabIndex = 32;
            this.button7.Text = "Save and Close";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.saveAndCloseButton_Click);
            // 
            // newUnitnumTextBox
            // 
            this.newUnitnumTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.newUnitnumTextBox.Location = new System.Drawing.Point(166, 80);
            this.newUnitnumTextBox.Name = "newUnitnumTextBox";
            this.newUnitnumTextBox.Size = new System.Drawing.Size(177, 23);
            this.newUnitnumTextBox.TabIndex = 132;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label1.Location = new System.Drawing.Point(15, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 133;
            this.label1.Text = "New Unit Number:";
            // 
            // ChangeMRNPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(479, 245);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newUnitnumTextBox);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.unitnumTextBox);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeMRNPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox unitnumTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox newUnitnumTextBox;
        private System.Windows.Forms.Label label1;

    }
}


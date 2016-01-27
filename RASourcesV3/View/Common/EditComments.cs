using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.Common
{
    public class EditComments : HraView
    {

        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button cancelButton;

        private Patient _patient;
        private string _oldComments;

        public EditComments(Patient patient)
        {
            this.InitializeComponent();

            this._patient = patient;
            this.commentsTextBox.Text = patient.Patient_Comment;
            this._oldComments = patient.Patient_Comment;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditComments));
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.closeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentsTextBox.Location = new System.Drawing.Point(12, 12);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commentsTextBox.Size = new System.Drawing.Size(732, 644);
            this.commentsTextBox.TabIndex = 109;
            this.commentsTextBox.Validated += commentsTextBox_Validated;
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(563, 672);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(183, 41);
            this.closeButton.TabIndex = 110;
            this.closeButton.Text = "Save and Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(390, 672);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(147, 41);
            this.cancelButton.TabIndex = 111;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditComments
            // 
            this.ClientSize = new System.Drawing.Size(758, 725);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditComments";
            this.Text = "Edit Comments";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void commentsTextBox_Validated(object sender, EventArgs e)
        {
            this._patient.Patient_Comment = this.commentsTextBox.Text;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this._patient.Patient_Comment = this._oldComments;
            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

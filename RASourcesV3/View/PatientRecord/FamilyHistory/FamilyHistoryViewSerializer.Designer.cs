namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    partial class FamilyHistoryViewSerializer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FamilyHistoryViewSerializer));
            this.closeButton = new System.Windows.Forms.Button();
            this.fhvSerializerRichTextBox = new System.Windows.Forms.RichTextBox();
            this.serializeFamilyHistoryButton = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.closeButton.Location = new System.Drawing.Point(238, 471);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // fhvSerializerRichTextBox
            // 
            this.fhvSerializerRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fhvSerializerRichTextBox.Location = new System.Drawing.Point(48, 88);
            this.fhvSerializerRichTextBox.Name = "fhvSerializerRichTextBox";
            this.fhvSerializerRichTextBox.Size = new System.Drawing.Size(455, 353);
            this.fhvSerializerRichTextBox.TabIndex = 1;
            this.fhvSerializerRichTextBox.Text = "";
            // 
            // serializeFamilyHistoryButton
            // 
            this.serializeFamilyHistoryButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.serializeFamilyHistoryButton.Location = new System.Drawing.Point(214, 35);
            this.serializeFamilyHistoryButton.Name = "serializeFamilyHistoryButton";
            this.serializeFamilyHistoryButton.Size = new System.Drawing.Size(138, 23);
            this.serializeFamilyHistoryButton.TabIndex = 2;
            this.serializeFamilyHistoryButton.Text = "Create Family History CCD";
            this.serializeFamilyHistoryButton.UseVisualStyleBackColor = true;
            this.serializeFamilyHistoryButton.Click += new System.EventHandler(this.serializeFamilyHistoryButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(48, 88);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(455, 353);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.Visible = false;
            // 
            // FamilyHistoryViewSerializer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 525);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.serializeFamilyHistoryButton);
            this.Controls.Add(this.fhvSerializerRichTextBox);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FamilyHistoryViewSerializer";
            this.Text = "Family History";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.RichTextBox fhvSerializerRichTextBox;
        private System.Windows.Forms.Button serializeFamilyHistoryButton;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}
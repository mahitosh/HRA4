namespace RiskApps3.View.Common
{
    partial class CoolCollapsibleControl
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
            this.header = new System.Windows.Forms.Panel();
            this.dropDown = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(750, 35);
            this.header.TabIndex = 0;
            // 
            // dropDown
            // 
            this.dropDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropDown.Location = new System.Drawing.Point(0, 35);
            this.dropDown.Name = "dropDown";
            this.dropDown.Size = new System.Drawing.Size(750, 180);
            this.dropDown.TabIndex = 1;
            // 
            // CoolCollapsibleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dropDown);
            this.Controls.Add(this.header);
            this.Name = "CoolCollapsibleControl";
            this.Size = new System.Drawing.Size(750, 215);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Panel dropDown;
    }
}

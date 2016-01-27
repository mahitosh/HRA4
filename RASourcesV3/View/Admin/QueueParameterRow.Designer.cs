namespace RiskApps3.View.Admin
{
    partial class QueueParameterRow
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
            this.IdLabel = new System.Windows.Forms.Label();
            this.rulesLabel = new System.Windows.Forms.Label();
            this.parameterLabel = new System.Windows.Forms.Label();
            this.valueIntegerTextBox = new RiskApps3.Utilities.IntegerTextBox();
            this.SuspendLayout();
            // 
            // IdLabel
            // 
            this.IdLabel.Location = new System.Drawing.Point(8, 3);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(21, 13);
            this.IdLabel.TabIndex = 4;
            this.IdLabel.Text = "ID";
            this.IdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rulesLabel
            // 
            this.rulesLabel.Location = new System.Drawing.Point(42, 3);
            this.rulesLabel.Name = "rulesLabel";
            this.rulesLabel.Size = new System.Drawing.Size(112, 13);
            this.rulesLabel.TabIndex = 5;
            this.rulesLabel.Text = "Rules";
            // 
            // parameterLabel
            // 
            this.parameterLabel.Location = new System.Drawing.Point(168, 3);
            this.parameterLabel.Name = "parameterLabel";
            this.parameterLabel.Size = new System.Drawing.Size(170, 13);
            this.parameterLabel.TabIndex = 6;
            this.parameterLabel.Text = "Parameter";
            // 
            // valueIntegerTextBox
            // 
            this.valueIntegerTextBox.Location = new System.Drawing.Point(350, 0);
            this.valueIntegerTextBox.Name = "valueIntegerTextBox";
            this.valueIntegerTextBox.Size = new System.Drawing.Size(60, 20);
            this.valueIntegerTextBox.TabIndex = 7;
            this.valueIntegerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.valueIntegerTextBox.TextChanged += new System.EventHandler(this.valueIntegerTextBox_TextChanged);
            // 
            // QueueParameterRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueIntegerTextBox);
            this.Controls.Add(this.parameterLabel);
            this.Controls.Add(this.rulesLabel);
            this.Controls.Add(this.IdLabel);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "QueueParameterRow";
            this.Size = new System.Drawing.Size(440, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IdLabel;
        private System.Windows.Forms.Label rulesLabel;
        private System.Windows.Forms.Label parameterLabel;
        private RiskApps3.Utilities.IntegerTextBox valueIntegerTextBox;
    }
}

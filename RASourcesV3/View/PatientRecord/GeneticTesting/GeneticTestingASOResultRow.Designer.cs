namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingASOResultRow
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
            this.components = new System.ComponentModel.Container();
            this.allelicStateComboBox = new System.Windows.Forms.ComboBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ASOInfoTextBox = new System.Windows.Forms.TextBox();
            this.ASOResultComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // allelicStateComboBox
            // 
            this.allelicStateComboBox.DisplayMember = "value1";
            this.allelicStateComboBox.DropDownHeight = 400;
            this.allelicStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.allelicStateComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allelicStateComboBox.FormattingEnabled = true;
            this.allelicStateComboBox.IntegralHeight = false;
            this.allelicStateComboBox.Items.AddRange(new object[] {
            "",
            "Homozygous",
            "Heterozygous",
            "Hemizygous",
            "Unknown"});
            this.allelicStateComboBox.Location = new System.Drawing.Point(357, 2);
            this.allelicStateComboBox.MaxDropDownItems = 15;
            this.allelicStateComboBox.Name = "allelicStateComboBox";
            this.allelicStateComboBox.Size = new System.Drawing.Size(81, 19);
            this.allelicStateComboBox.TabIndex = 50;
            this.allelicStateComboBox.ValueMember = "value1";
            this.allelicStateComboBox.SelectionChangeCommitted += new System.EventHandler(this.allelicStateComboBox_SelectionChangeCommitted);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(525, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(24, 21);
            this.deleteButton.TabIndex = 52;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "X";
            this.toolTip1.SetToolTip(this.deleteButton, "Delete genetic testing result");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsTextBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentsTextBox.Location = new System.Drawing.Point(440, 2);
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(85, 19);
            this.commentsTextBox.TabIndex = 51;
            this.commentsTextBox.Validated += new System.EventHandler(this.commentsTextBox_Validated);
            // 
            // ASOInfoTextBox
            // 
            this.ASOInfoTextBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ASOInfoTextBox.Location = new System.Drawing.Point(1, 2);
            this.ASOInfoTextBox.Name = "ASOInfoTextBox";
            this.ASOInfoTextBox.ReadOnly = true;
            this.ASOInfoTextBox.Size = new System.Drawing.Size(238, 19);
            this.ASOInfoTextBox.TabIndex = 53;
            this.ASOInfoTextBox.TabStop = false;
            // 
            // ASOResultComboBox
            // 
            this.ASOResultComboBox.DisplayMember = "value1";
            this.ASOResultComboBox.DropDownHeight = 400;
            this.ASOResultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ASOResultComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ASOResultComboBox.FormattingEnabled = true;
            this.ASOResultComboBox.IntegralHeight = false;
            this.ASOResultComboBox.Items.AddRange(new object[] {
            "",
            "Unknown",
            "Found",
            "Not Found",
            "Not Tested"});
            this.ASOResultComboBox.Location = new System.Drawing.Point(241, 2);
            this.ASOResultComboBox.MaxDropDownItems = 15;
            this.ASOResultComboBox.Name = "ASOResultComboBox";
            this.ASOResultComboBox.Size = new System.Drawing.Size(114, 19);
            this.ASOResultComboBox.TabIndex = 54;
            this.ASOResultComboBox.ValueMember = "value1";
            this.ASOResultComboBox.SelectionChangeCommitted += new System.EventHandler(this.ASOResultComboBox_SelectionChangeCommitted);
            // 
            // GeneticTestingResultRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.ASOInfoTextBox);
            this.Controls.Add(this.allelicStateComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.ASOResultComboBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(550, 0);
            this.Name = "GeneticTestingResultRow";
            this.Size = new System.Drawing.Size(550, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox allelicStateComboBox;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.TextBox ASOInfoTextBox;
        private System.Windows.Forms.ComboBox ASOResultComboBox;
    }
}

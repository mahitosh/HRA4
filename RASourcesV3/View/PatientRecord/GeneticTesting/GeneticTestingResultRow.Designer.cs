namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    partial class GeneticTestingResultRow
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
            this.mutationAAComboBox = new System.Windows.Forms.ComboBox();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.resultSignificanceComboBox = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mutationNameComboBox = new System.Windows.Forms.ComboBox();
            this.geneNameComboBox = new System.Windows.Forms.ComboBox();
            this.geneNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            this.allelicStateComboBox.TabIndex = 4;
            this.allelicStateComboBox.ValueMember = "value1";
            this.allelicStateComboBox.SelectedValueChanged += new System.EventHandler(this.allelicStateComboBox_SelectionChangeCommitted);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.Location = new System.Drawing.Point(523, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(24, 21);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "X";
            this.toolTip1.SetToolTip(this.deleteButton, "Delete genetic testing result");
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // mutationAAComboBox
            // 
            this.mutationAAComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.mutationAAComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.mutationAAComboBox.DropDownHeight = 400;
            this.mutationAAComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mutationAAComboBox.FormattingEnabled = true;
            this.mutationAAComboBox.IntegralHeight = false;
            this.mutationAAComboBox.Location = new System.Drawing.Point(153, 2);
            this.mutationAAComboBox.MaxDropDownItems = 15;
            this.mutationAAComboBox.Name = "mutationAAComboBox";
            this.mutationAAComboBox.Size = new System.Drawing.Size(86, 19);
            this.mutationAAComboBox.TabIndex = 2;
            this.mutationAAComboBox.SelectedValueChanged += new System.EventHandler(this.mutationAAComboBox_SelectionChangeCommitted);
            this.mutationAAComboBox.Validated += new System.EventHandler(this.mutationAAComboBox_Validated);
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.commentsTextBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commentsTextBox.Location = new System.Drawing.Point(440, 2);
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(79, 19);
            this.commentsTextBox.TabIndex = 5;
            this.commentsTextBox.Validated += new System.EventHandler(this.commentsTextBox_Validated);
            // 
            // resultSignificanceComboBox
            // 
            this.resultSignificanceComboBox.DisplayMember = "value1";
            this.resultSignificanceComboBox.DropDownHeight = 400;
            this.resultSignificanceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.resultSignificanceComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultSignificanceComboBox.FormattingEnabled = true;
            this.resultSignificanceComboBox.IntegralHeight = false;
            this.resultSignificanceComboBox.Location = new System.Drawing.Point(241, 2);
            this.resultSignificanceComboBox.MaxDropDownItems = 15;
            this.resultSignificanceComboBox.Name = "resultSignificanceComboBox";
            this.resultSignificanceComboBox.Size = new System.Drawing.Size(114, 19);
            this.resultSignificanceComboBox.TabIndex = 3;
            this.resultSignificanceComboBox.ValueMember = "value1";
            this.resultSignificanceComboBox.SelectedValueChanged += new System.EventHandler(this.resultSignificanceComboBox_SelectionChangeCommitted);
            // 
            // mutationNameComboBox
            // 
            this.mutationNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.mutationNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.mutationNameComboBox.DropDownHeight = 400;
            this.mutationNameComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mutationNameComboBox.FormattingEnabled = true;
            this.mutationNameComboBox.IntegralHeight = false;
            this.mutationNameComboBox.Location = new System.Drawing.Point(63, 2);
            this.mutationNameComboBox.MaxDropDownItems = 15;
            this.mutationNameComboBox.Name = "mutationNameComboBox";
            this.mutationNameComboBox.Size = new System.Drawing.Size(89, 19);
            this.mutationNameComboBox.TabIndex = 1;
            this.mutationNameComboBox.SelectedValueChanged += new System.EventHandler(this.mutationNameComboBox_SelectedValueChanged);
            this.mutationNameComboBox.Validated += new System.EventHandler(this.mutationNameComboBox_Validated);
            // 
            // geneNameComboBox
            // 
            this.geneNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.geneNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.geneNameComboBox.DropDownHeight = 400;
            this.geneNameComboBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneNameComboBox.FormattingEnabled = true;
            this.geneNameComboBox.IntegralHeight = false;
            this.geneNameComboBox.Location = new System.Drawing.Point(1, 2);
            this.geneNameComboBox.MaxDropDownItems = 15;
            this.geneNameComboBox.Name = "geneNameComboBox";
            this.geneNameComboBox.Size = new System.Drawing.Size(60, 19);
            this.geneNameComboBox.TabIndex = 0;
            this.geneNameComboBox.SelectionChangeCommitted += new System.EventHandler(this.geneNameComboBox_SelectionChangeCommitted);
            this.geneNameComboBox.SelectedValueChanged += new System.EventHandler(this.geneNameComboBox_SelectedValueChanged);
            this.geneNameComboBox.Validated += new System.EventHandler(this.geneNameComboBox_Validated);
            // 
            // geneNameTextBox
            // 
            this.geneNameTextBox.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.geneNameTextBox.Location = new System.Drawing.Point(1, 2);
            this.geneNameTextBox.Name = "geneNameTextBox";
            this.geneNameTextBox.ReadOnly = true;
            this.geneNameTextBox.Size = new System.Drawing.Size(60, 19);
            this.geneNameTextBox.TabIndex = 48;
            this.geneNameTextBox.TabStop = false;
            this.geneNameTextBox.Validated += new System.EventHandler(this.geneNameTextBox_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(328, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 49;
            this.label1.Text = "!";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GeneticTestingResultRow
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.allelicStateComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.mutationAAComboBox);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.mutationNameComboBox);
            this.Controls.Add(this.geneNameComboBox);
            this.Controls.Add(this.geneNameTextBox);
            this.Controls.Add(this.resultSignificanceComboBox);
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
        private System.Windows.Forms.ComboBox mutationAAComboBox;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.ComboBox resultSignificanceComboBox;
        private System.Windows.Forms.ComboBox mutationNameComboBox;
        private System.Windows.Forms.ComboBox geneNameComboBox;
        private System.Windows.Forms.TextBox geneNameTextBox;
        private System.Windows.Forms.Label label1;
    }
}

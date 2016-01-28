namespace RiskApps3.View.PatientRecord
{
    partial class OrderRow
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
            this.orderDateLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.orderComboBox = new RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox();
            this.SuspendLayout();
            // 
            // orderDateLabel
            // 
            this.orderDateLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderDateLabel.Location = new System.Drawing.Point(1, 9);
            this.orderDateLabel.Name = "orderDateLabel";
            this.orderDateLabel.Size = new System.Drawing.Size(81, 19);
            this.orderDateLabel.TabIndex = 0;
            this.orderDateLabel.Text = "12/12/2012";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Image = global::RiskApps3.Properties.Resources.delete_X;
            this.deleteButton.Location = new System.Drawing.Point(774, 6);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(36, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // orderComboBox
            // 
            this.orderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.orderComboBox.BorderColor = System.Drawing.SystemColors.Window;
            this.orderComboBox.ComboFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orderComboBox.DropDownWidth = 668;
            this.orderComboBox.Location = new System.Drawing.Point(76, 2);
            this.orderComboBox.MaxDropDownItems = 8;
            this.orderComboBox.Name = "orderComboBox";
            this.orderComboBox.Padding = new System.Windows.Forms.Padding(4);
            this.orderComboBox.PopupWidth = 650;
            this.orderComboBox.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.orderComboBox.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.orderComboBox.Size = new System.Drawing.Size(676, 29);
            this.orderComboBox.TabIndex = 5;
            this.orderComboBox.Leave += new System.EventHandler(this.orderComboBox_Leave);
            this.orderComboBox.Enter += new System.EventHandler(this.orderComboBox_Enter);
            // 
            // OrderRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.orderComboBox);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.orderDateLabel);
            this.Name = "OrderRow";
            this.Size = new System.Drawing.Size(818, 33);
            this.Load += new System.EventHandler(this.OrderRow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label orderDateLabel;
        private System.Windows.Forms.Button deleteButton;
        private RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox orderComboBox;
    }
}

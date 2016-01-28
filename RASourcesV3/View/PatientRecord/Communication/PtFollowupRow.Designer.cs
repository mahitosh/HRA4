namespace RiskApps3.View.PatientRecord.Communication
{
    partial class PtFollowupRow
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
            this.txtComment = new System.Windows.Forms.TextBox();
            this.lblComment = new System.Windows.Forms.Label();
            this.cboFollowupNoReason = new System.Windows.Forms.ComboBox();
            this.cboFollowupDisposition = new System.Windows.Forms.ComboBox();
            this.cboFollowupType = new System.Windows.Forms.ComboBox();
            this.txtFollowupDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.who = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComment.Location = new System.Drawing.Point(3, 50);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(809, 123);
            this.txtComment.TabIndex = 32;
            this.txtComment.Validated += new System.EventHandler(this.txtComment_Validated);
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComment.Location = new System.Drawing.Point(4, 34);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(65, 13);
            this.lblComment.TabIndex = 37;
            this.lblComment.Text = "Comment:";
            // 
            // cboFollowupNoReason
            // 
            this.cboFollowupNoReason.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFollowupNoReason.FormattingEnabled = true;
            this.cboFollowupNoReason.Location = new System.Drawing.Point(510, 3);
            this.cboFollowupNoReason.Name = "cboFollowupNoReason";
            this.cboFollowupNoReason.Size = new System.Drawing.Size(213, 21);
            this.cboFollowupNoReason.TabIndex = 31;
            this.cboFollowupNoReason.SelectedIndexChanged += new System.EventHandler(this.cboFollowupNoReason_SelectedIndexChanged);
            // 
            // cboFollowupDisposition
            // 
            this.cboFollowupDisposition.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFollowupDisposition.FormattingEnabled = true;
            this.cboFollowupDisposition.Location = new System.Drawing.Point(369, 3);
            this.cboFollowupDisposition.Name = "cboFollowupDisposition";
            this.cboFollowupDisposition.Size = new System.Drawing.Size(135, 21);
            this.cboFollowupDisposition.TabIndex = 30;
            this.cboFollowupDisposition.SelectedIndexChanged += new System.EventHandler(this.cboFollowupDisposition_SelectedIndexChanged);
            // 
            // cboFollowupType
            // 
            this.cboFollowupType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFollowupType.FormattingEnabled = true;
            this.cboFollowupType.Location = new System.Drawing.Point(237, 3);
            this.cboFollowupType.Name = "cboFollowupType";
            this.cboFollowupType.Size = new System.Drawing.Size(126, 21);
            this.cboFollowupType.TabIndex = 29;
            this.cboFollowupType.SelectedIndexChanged += new System.EventHandler(this.cboFollowupType_SelectedIndexChanged);
            // 
            // txtFollowupDate
            // 
            this.txtFollowupDate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFollowupDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtFollowupDate.Location = new System.Drawing.Point(8, 4);
            this.txtFollowupDate.Name = "txtFollowupDate";
            this.txtFollowupDate.Size = new System.Drawing.Size(93, 21);
            this.txtFollowupDate.TabIndex = 28;
            this.txtFollowupDate.Validated += new System.EventHandler(this.txtFollowupDate_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(728, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Comment";
            this.label1.Click += new System.EventHandler(this.button1_Click);
            // 
            // who
            // 
            this.who.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.who.FormattingEnabled = true;
            this.who.Location = new System.Drawing.Point(107, 4);
            this.who.Name = "who";
            this.who.Size = new System.Drawing.Size(124, 21);
            this.who.TabIndex = 39;
            this.who.SelectionChangeCommitted += new System.EventHandler(this.who_SelectionChangeCommitted);
            // 
            // PtFollowupRow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.who);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.cboFollowupNoReason);
            this.Controls.Add(this.cboFollowupDisposition);
            this.Controls.Add(this.cboFollowupType);
            this.Controls.Add(this.txtFollowupDate);
            this.Name = "PtFollowupRow";
            this.Size = new System.Drawing.Size(813, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtComment;
        internal System.Windows.Forms.Label lblComment;
        internal System.Windows.Forms.ComboBox cboFollowupNoReason;
        internal System.Windows.Forms.ComboBox cboFollowupDisposition;
        internal System.Windows.Forms.ComboBox cboFollowupType;
        internal System.Windows.Forms.DateTimePicker txtFollowupDate;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox who;
    }
}

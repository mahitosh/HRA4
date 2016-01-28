namespace RiskApps3.View.PatientRecord
{
    partial class OrdersView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersView));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.todaysOrdersTabPage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.noExistingOrdersLabel = new System.Windows.Forms.Label();
            this.pastOrdersTabPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.noExistingPastOrdersLabel = new System.Windows.Forms.Label();
            this.generateOrdersButton = new System.Windows.Forms.Button();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.addNewOrderButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupComboBox = new RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.finalizeButton = new System.Windows.Forms.Button();
            this.generateDocumentsButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.todaysOrdersTabPage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pastOrdersTabPage.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.todaysOrdersTabPage);
            this.tabControl1.Controls.Add(this.pastOrdersTabPage);
            this.tabControl1.Location = new System.Drawing.Point(37, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(848, 425);
            this.tabControl1.TabIndex = 6;
            // 
            // todaysOrdersTabPage
            // 
            this.todaysOrdersTabPage.Controls.Add(this.label3);
            this.todaysOrdersTabPage.Controls.Add(this.label1);
            this.todaysOrdersTabPage.Controls.Add(this.flowLayoutPanel1);
            this.todaysOrdersTabPage.Location = new System.Drawing.Point(4, 22);
            this.todaysOrdersTabPage.Name = "todaysOrdersTabPage";
            this.todaysOrdersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.todaysOrdersTabPage.Size = new System.Drawing.Size(840, 399);
            this.todaysOrdersTabPage.TabIndex = 0;
            this.todaysOrdersTabPage.Text = "Orders";
            this.todaysOrdersTabPage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(294, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Order";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Date";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.noExistingOrdersLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 40);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(828, 353);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // noExistingOrdersLabel
            // 
            this.noExistingOrdersLabel.AutoSize = true;
            this.noExistingOrdersLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noExistingOrdersLabel.Location = new System.Drawing.Point(3, 0);
            this.noExistingOrdersLabel.Name = "noExistingOrdersLabel";
            this.noExistingOrdersLabel.Padding = new System.Windows.Forms.Padding(30);
            this.noExistingOrdersLabel.Size = new System.Drawing.Size(183, 73);
            this.noExistingOrdersLabel.TabIndex = 0;
            this.noExistingOrdersLabel.Text = "No Non-Finalized Orders";
            this.noExistingOrdersLabel.Visible = false;
            // 
            // pastOrdersTabPage
            // 
            this.pastOrdersTabPage.Controls.Add(this.label6);
            this.pastOrdersTabPage.Controls.Add(this.label8);
            this.pastOrdersTabPage.Controls.Add(this.flowLayoutPanel2);
            this.pastOrdersTabPage.Location = new System.Drawing.Point(4, 22);
            this.pastOrdersTabPage.Name = "pastOrdersTabPage";
            this.pastOrdersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pastOrdersTabPage.Size = new System.Drawing.Size(840, 399);
            this.pastOrdersTabPage.TabIndex = 1;
            this.pastOrdersTabPage.Text = "Past Orders";
            this.pastOrdersTabPage.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(294, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(226, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Order";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(20, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 23);
            this.label8.TabIndex = 6;
            this.label8.Text = "Date";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.noExistingPastOrdersLabel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 40);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(828, 357);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // noExistingPastOrdersLabel
            // 
            this.noExistingPastOrdersLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.noExistingPastOrdersLabel.AutoSize = true;
            this.noExistingPastOrdersLabel.Location = new System.Drawing.Point(3, 0);
            this.noExistingPastOrdersLabel.Name = "noExistingPastOrdersLabel";
            this.noExistingPastOrdersLabel.Padding = new System.Windows.Forms.Padding(30);
            this.noExistingPastOrdersLabel.Size = new System.Drawing.Size(178, 73);
            this.noExistingPastOrdersLabel.TabIndex = 11;
            this.noExistingPastOrdersLabel.Text = "No Existing Past Orders";
            this.noExistingPastOrdersLabel.Visible = false;
            // 
            // generateOrdersButton
            // 
            this.generateOrdersButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.generateOrdersButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateOrdersButton.Location = new System.Drawing.Point(322, 469);
            this.generateOrdersButton.Name = "generateOrdersButton";
            this.generateOrdersButton.Size = new System.Drawing.Size(141, 33);
            this.generateOrdersButton.TabIndex = 9;
            this.generateOrdersButton.Text = "Generate Orders";
            this.generateOrdersButton.UseVisualStyleBackColor = true;
            this.generateOrdersButton.Click += new System.EventHandler(this.generateOrdersButton_Click);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadingCircle1.BackColor = System.Drawing.SystemColors.Control;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(444, 254);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(44, 37);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 10;
            this.loadingCircle1.Text = "loadingCircle1";
            // 
            // addNewOrderButton
            // 
            this.addNewOrderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addNewOrderButton.Location = new System.Drawing.Point(4, 106);
            this.addNewOrderButton.Name = "addNewOrderButton";
            this.addNewOrderButton.Size = new System.Drawing.Size(30, 25);
            this.addNewOrderButton.TabIndex = 8;
            this.addNewOrderButton.Text = " +";
            this.addNewOrderButton.UseVisualStyleBackColor = true;
            this.addNewOrderButton.Click += new System.EventHandler(this.addNewOrderButton_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // groupComboBox
            // 
            this.groupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.groupComboBox.BorderColor = System.Drawing.SystemColors.Control;
            this.groupComboBox.ComboFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.groupComboBox.DropDownWidth = 180;
            this.groupComboBox.Location = new System.Drawing.Point(699, 28);
            this.groupComboBox.MaxDropDownItems = 8;
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Padding = new System.Windows.Forms.Padding(4);
            this.groupComboBox.PopupWidth = 175;
            this.groupComboBox.SelectedItemBackColor = System.Drawing.SystemColors.Highlight;
            this.groupComboBox.SelectedItemForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupComboBox.Size = new System.Drawing.Size(188, 29);
            this.groupComboBox.TabIndex = 11;
            this.groupComboBox.Load += new System.EventHandler(this.groupComboBox_Load);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(657, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Group:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // finalizeButton
            // 
            this.finalizeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.finalizeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.finalizeButton.Location = new System.Drawing.Point(469, 469);
            this.finalizeButton.Name = "finalizeButton";
            this.finalizeButton.Size = new System.Drawing.Size(141, 33);
            this.finalizeButton.TabIndex = 13;
            this.finalizeButton.Text = "Finalize Orders";
            this.finalizeButton.UseVisualStyleBackColor = true;
            this.finalizeButton.Click += new System.EventHandler(this.finalizeButton_Click);
            // 
            // generateDocumentsButton
            // 
            this.generateDocumentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.generateDocumentsButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateDocumentsButton.Location = new System.Drawing.Point(740, 469);
            this.generateDocumentsButton.Name = "generateDocumentsButton";
            this.generateDocumentsButton.Size = new System.Drawing.Size(141, 33);
            this.generateDocumentsButton.TabIndex = 14;
            this.generateDocumentsButton.Text = "Generate Letters";
            this.generateDocumentsButton.UseVisualStyleBackColor = true;
            this.generateDocumentsButton.Visible = false;
            this.generateDocumentsButton.Click += new System.EventHandler(this.generateDocumentsButton_Click);
            // 
            // OrdersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 541);
            this.Controls.Add(this.generateDocumentsButton);
            this.Controls.Add(this.finalizeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupComboBox);
            this.Controls.Add(this.generateOrdersButton);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.addNewOrderButton);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OrdersView";
            this.Text = "Orders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrdersView_FormClosing);
            this.Load += new System.EventHandler(this.OrdersView_Load);
            this.tabControl1.ResumeLayout(false);
            this.todaysOrdersTabPage.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pastOrdersTabPage.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage todaysOrdersTabPage;
        private System.Windows.Forms.TabPage pastOrdersTabPage;
        private System.Windows.Forms.Button generateOrdersButton;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Button addNewOrderButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label noExistingPastOrdersLabel;
        private System.Windows.Forms.Label noExistingOrdersLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private RiskApps3.View.Common.AutoSearchTextBox.CoolComboBox groupComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button finalizeButton;
        private System.Windows.Forms.Button generateDocumentsButton;
    }
}
namespace RiskApps3.View.PatientRecord.Communication
{
    partial class NewToDoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewToDoView));
            this.NewTodo = new System.Windows.Forms.Button();
            this.NewNote = new System.Windows.Forms.Button();
            this.NewSurgery = new System.Windows.Forms.Button();
            this.NewOrders = new System.Windows.Forms.Button();
            this.NewDocument = new System.Windows.Forms.Button();
            this.NewFYI = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewTodo
            // 
            this.NewTodo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewTodo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewTodo.Location = new System.Drawing.Point(130, 10);
            this.NewTodo.Margin = new System.Windows.Forms.Padding(10);
            this.NewTodo.Name = "NewTodo";
            this.NewTodo.Size = new System.Drawing.Size(100, 28);
            this.NewTodo.TabIndex = 22;
            this.NewTodo.Text = "New Task";
            this.NewTodo.UseVisualStyleBackColor = false;
            this.NewTodo.Click += new System.EventHandler(this.button2_Click);
            // 
            // NewNote
            // 
            this.NewNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewNote.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewNote.Location = new System.Drawing.Point(10, 10);
            this.NewNote.Margin = new System.Windows.Forms.Padding(10);
            this.NewNote.Name = "NewNote";
            this.NewNote.Size = new System.Drawing.Size(100, 28);
            this.NewNote.TabIndex = 26;
            this.NewNote.Text = "New Note";
            this.NewNote.UseVisualStyleBackColor = false;
            this.NewNote.Click += new System.EventHandler(this.NewNote_Click);
            // 
            // NewSurgery
            // 
            this.NewSurgery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewSurgery.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewSurgery.Location = new System.Drawing.Point(730, 10);
            this.NewSurgery.Margin = new System.Windows.Forms.Padding(10);
            this.NewSurgery.Name = "NewSurgery";
            this.NewSurgery.Size = new System.Drawing.Size(100, 28);
            this.NewSurgery.TabIndex = 25;
            this.NewSurgery.Text = "Schedule Surgery";
            this.NewSurgery.UseVisualStyleBackColor = false;
            // 
            // NewOrders
            // 
            this.NewOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewOrders.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewOrders.Location = new System.Drawing.Point(610, 10);
            this.NewOrders.Margin = new System.Windows.Forms.Padding(10);
            this.NewOrders.Name = "NewOrders";
            this.NewOrders.Size = new System.Drawing.Size(100, 28);
            this.NewOrders.TabIndex = 24;
            this.NewOrders.Text = "Orders";
            this.NewOrders.UseVisualStyleBackColor = false;
            this.NewOrders.Click += new System.EventHandler(this.NewOrders_Click);
            // 
            // NewDocument
            // 
            this.NewDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewDocument.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewDocument.Location = new System.Drawing.Point(490, 10);
            this.NewDocument.Margin = new System.Windows.Forms.Padding(10);
            this.NewDocument.Name = "NewDocument";
            this.NewDocument.Size = new System.Drawing.Size(100, 28);
            this.NewDocument.TabIndex = 21;
            this.NewDocument.Text = "New Document";
            this.NewDocument.UseVisualStyleBackColor = false;
            this.NewDocument.Click += new System.EventHandler(this.NewDocument_Click);
            // 
            // NewFYI
            // 
            this.NewFYI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewFYI.BackColor = System.Drawing.SystemColors.ControlLight;
            this.NewFYI.Location = new System.Drawing.Point(250, 10);
            this.NewFYI.Margin = new System.Windows.Forms.Padding(10);
            this.NewFYI.Name = "NewFYI";
            this.NewFYI.Size = new System.Drawing.Size(100, 28);
            this.NewFYI.TabIndex = 23;
            this.NewFYI.Text = "Patient Followup";
            this.NewFYI.UseVisualStyleBackColor = false;
            this.NewFYI.Click += new System.EventHandler(this.NewPatientFollowup_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.NewNote);
            this.flowLayoutPanel1.Controls.Add(this.NewTodo);
            this.flowLayoutPanel1.Controls.Add(this.NewFYI);
            this.flowLayoutPanel1.Controls.Add(this.button1);
            this.flowLayoutPanel1.Controls.Add(this.NewDocument);
            this.flowLayoutPanel1.Controls.Add(this.NewOrders);
            this.flowLayoutPanel1.Controls.Add(this.NewSurgery);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(862, 47);
            this.flowLayoutPanel1.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.Location = new System.Drawing.Point(370, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 27;
            this.button1.Text = "New FYI";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.NewFYI_Click);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker2_ProgressChanged);
            this.backgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker2_RunWorkerCompleted);
            // 
            // NewToDoView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 47);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewToDoView";
            this.Text = "New ToDo ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewToDoView_FormClosing);
            this.Load += new System.EventHandler(this.NewToDoView_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NewTodo;
        private System.Windows.Forms.Button NewNote;
        private System.Windows.Forms.Button NewSurgery;
        private System.Windows.Forms.Button NewOrders;
        private System.Windows.Forms.Button NewDocument;
        private System.Windows.Forms.Button NewFYI;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button button1;
    }
}
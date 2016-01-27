namespace RiskApps3.View.PatientRecord
{
    partial class Recommendations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recommendations));
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbeRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.chemoRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.mammoRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.prophMastRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.mriRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ocRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.prophOophRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.ca125RecClinComboBox = new System.Windows.Forms.ComboBox();
            this.tvsRecClinComboBox = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 115;
            this.label5.Text = "MammographyHxView";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 119;
            this.label3.Text = "CBE";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 120;
            this.label4.Text = "Chemoprevention";
            // 
            // cbeRecClinComboBox
            // 
            this.cbeRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.cbeRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.cbeRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbeRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbeRecClinComboBox.FormattingEnabled = true;
            this.cbeRecClinComboBox.Location = new System.Drawing.Point(177, 51);
            this.cbeRecClinComboBox.Name = "cbeRecClinComboBox";
            this.cbeRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.cbeRecClinComboBox.TabIndex = 124;
            this.cbeRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.cbeRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.cbeRecClinComboBox_SelectionChangeCommitted);
            this.cbeRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // chemoRecClinComboBox
            // 
            this.chemoRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.chemoRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.chemoRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chemoRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chemoRecClinComboBox.FormattingEnabled = true;
            this.chemoRecClinComboBox.Location = new System.Drawing.Point(177, 89);
            this.chemoRecClinComboBox.Name = "chemoRecClinComboBox";
            this.chemoRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.chemoRecClinComboBox.TabIndex = 125;
            this.chemoRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.chemoRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.chemoRecClinComboBox_SelectionChangeCommitted);
            this.chemoRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // mammoRecClinComboBox
            // 
            this.mammoRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.mammoRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mammoRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mammoRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mammoRecClinComboBox.FormattingEnabled = true;
            this.mammoRecClinComboBox.Location = new System.Drawing.Point(177, 127);
            this.mammoRecClinComboBox.Name = "mammoRecClinComboBox";
            this.mammoRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.mammoRecClinComboBox.TabIndex = 126;
            this.mammoRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.mammoRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.mammoRecClinComboBox_SelectionChangeCommitted);
            this.mammoRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // prophMastRecClinComboBox
            // 
            this.prophMastRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.prophMastRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.prophMastRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prophMastRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prophMastRecClinComboBox.FormattingEnabled = true;
            this.prophMastRecClinComboBox.Location = new System.Drawing.Point(177, 165);
            this.prophMastRecClinComboBox.Name = "prophMastRecClinComboBox";
            this.prophMastRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.prophMastRecClinComboBox.TabIndex = 170;
            this.prophMastRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.prophMastRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.prophMastRecClinComboBox_SelectionChangeCommitted);
            this.prophMastRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 13);
            this.label6.TabIndex = 169;
            this.label6.Text = "Prophylactic Mastectomy";
            // 
            // mriRecClinComboBox
            // 
            this.mriRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.mriRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.mriRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mriRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mriRecClinComboBox.FormattingEnabled = true;
            this.mriRecClinComboBox.Location = new System.Drawing.Point(177, 203);
            this.mriRecClinComboBox.Name = "mriRecClinComboBox";
            this.mriRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.mriRecClinComboBox.TabIndex = 176;
            this.mriRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.mriRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.mriRecClinComboBox_SelectionChangeCommitted);
            this.mriRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(19, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 175;
            this.label7.Text = "MRI";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ocRecClinComboBox
            // 
            this.ocRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.ocRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ocRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ocRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ocRecClinComboBox.FormattingEnabled = true;
            this.ocRecClinComboBox.Location = new System.Drawing.Point(177, 174);
            this.ocRecClinComboBox.Name = "ocRecClinComboBox";
            this.ocRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.ocRecClinComboBox.TabIndex = 206;
            this.ocRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.ocRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.ocRecClinComboBox_SelectionChangeCommitted);
            this.ocRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(17, 178);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 13);
            this.label16.TabIndex = 205;
            this.label16.Text = "Oral Contraceptives";
            // 
            // prophOophRecClinComboBox
            // 
            this.prophOophRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.prophOophRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.prophOophRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.prophOophRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prophOophRecClinComboBox.FormattingEnabled = true;
            this.prophOophRecClinComboBox.Location = new System.Drawing.Point(177, 136);
            this.prophOophRecClinComboBox.Name = "prophOophRecClinComboBox";
            this.prophOophRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.prophOophRecClinComboBox.TabIndex = 201;
            this.prophOophRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.prophOophRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.prophOophRecClinComboBox_SelectionChangeCommitted);
            this.prophOophRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // ca125RecClinComboBox
            // 
            this.ca125RecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.ca125RecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.ca125RecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ca125RecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ca125RecClinComboBox.FormattingEnabled = true;
            this.ca125RecClinComboBox.Location = new System.Drawing.Point(177, 98);
            this.ca125RecClinComboBox.Name = "ca125RecClinComboBox";
            this.ca125RecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.ca125RecClinComboBox.TabIndex = 200;
            this.ca125RecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.ca125RecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.ca125RecClinComboBox_SelectionChangeCommitted);
            this.ca125RecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // tvsRecClinComboBox
            // 
            this.tvsRecClinComboBox.AutoCompleteCustomSource.AddRange(new string[] {
            "Yes",
            "No"});
            this.tvsRecClinComboBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvsRecClinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tvsRecClinComboBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvsRecClinComboBox.FormattingEnabled = true;
            this.tvsRecClinComboBox.Location = new System.Drawing.Point(177, 60);
            this.tvsRecClinComboBox.Name = "tvsRecClinComboBox";
            this.tvsRecClinComboBox.Size = new System.Drawing.Size(266, 21);
            this.tvsRecClinComboBox.TabIndex = 199;
            this.tvsRecClinComboBox.DropDown += new System.EventHandler(this.RecClinComboBox_DropDown);
            this.tvsRecClinComboBox.SelectionChangeCommitted += new System.EventHandler(this.tvsRecClinComboBox_SelectionChangeCommitted);
            this.tvsRecClinComboBox.DropDownClosed += new System.EventHandler(this.RecClinComboBox_DropDownClosed);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(15, 140);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(139, 13);
            this.label17.TabIndex = 196;
            this.label17.Text = "Prophylactic Oophorectomy";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(15, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 13);
            this.label18.TabIndex = 197;
            this.label18.Text = "Transvaginal Sonography";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(15, 102);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 13);
            this.label19.TabIndex = 198;
            this.label19.Text = "CA-125";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(253, 32);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(119, 16);
            this.label20.TabIndex = 194;
            this.label20.Text = "Recommendation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(269, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 222;
            this.label1.Text = "Recommendation";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbeRecClinComboBox);
            this.groupBox1.Controls.Add(this.chemoRecClinComboBox);
            this.groupBox1.Controls.Add(this.mammoRecClinComboBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.prophMastRecClinComboBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.mriRecClinComboBox);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(492, 244);
            this.groupBox1.TabIndex = 225;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Breast";
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(449, 202);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(36, 23);
            this.button5.TabIndex = 199;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(449, 164);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(36, 23);
            this.button4.TabIndex = 198;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(449, 125);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 23);
            this.button3.TabIndex = 197;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(449, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(36, 23);
            this.button2.TabIndex = 196;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(449, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 195;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.button7);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.tvsRecClinComboBox);
            this.groupBox2.Controls.Add(this.ocRecClinComboBox);
            this.groupBox2.Controls.Add(this.ca125RecClinComboBox);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.prophOophRecClinComboBox);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(492, 230);
            this.groupBox2.TabIndex = 226;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ovarian";
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(449, 172);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(36, 23);
            this.button9.TabIndex = 225;
            this.button9.Text = "...";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(449, 134);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(36, 23);
            this.button8.TabIndex = 224;
            this.button8.Text = "...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(449, 96);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(36, 23);
            this.button7.TabIndex = 223;
            this.button7.Text = "...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(449, 59);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(36, 23);
            this.button6.TabIndex = 200;
            this.button6.Text = "...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadingCircle1.BackColor = System.Drawing.Color.Transparent;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(423, 274);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(41, 31);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 227;
            this.loadingCircle1.Text = "loadingCircle2";
            this.loadingCircle1.Visible = false;
            // 
            // Recommendations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(873, 602);
            this.Controls.Add(this.loadingCircle1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Recommendations";
            this.Text = "Recommendations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Recommendations_FormClosing);
            this.Load += new System.EventHandler(this.Recommendations_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbeRecClinComboBox;
        private System.Windows.Forms.ComboBox chemoRecClinComboBox;
        private System.Windows.Forms.ComboBox mammoRecClinComboBox;
        private System.Windows.Forms.ComboBox prophMastRecClinComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox mriRecClinComboBox;
        private System.Windows.Forms.Label label7;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ComboBox ocRecClinComboBox;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox prophOophRecClinComboBox;
        private System.Windows.Forms.ComboBox ca125RecClinComboBox;
        private System.Windows.Forms.ComboBox tvsRecClinComboBox;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;


    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.MetaData;
using RiskApps3.Controllers;
using RiskApps3.Utilities;
using Configurator = RiskApps3.Utilities.Configurator;
using System.Xml;
using System.Collections;

namespace DevUtil
{
    public partial class CodeUtilsForm : Form
    {
        int count = 0;
        int publishCount = 0;
        // for launcher tab jdg 9/15/15
        FileSystemWatcher watcher = new FileSystemWatcher();
        XmlDocument doc = new XmlDocument();
        private string fileLoc = "";
        string prevName = "";
        // end jdg 9/15/15

        public Dictionary<int, string> surveyDic;

        public Dictionary<int, string> clinicDic;

        public CodeUtilsForm()
        {
            InitializeComponent();
            label15.Text = "";
            label16.Text = "";
            label6.Text = "";
            label17.Text = "";
            labelLogString.Text = "";
            labelLogCount.Text = "0";

            listView2.ListViewItemSorter = new IntegerComparer(0);
            listView3.ListViewItemSorter = new IntegerComparer(0);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            //textBox2.Lines = textBox1.Text.Split(new char[] { ',' });

            string[] lines = textBox1.Text.Split(new char[] { ',' });
            foreach (string s in lines)
            {
                textBox2.Text += "[HraAttribute] public string " + s + ";\r\n";
            }

        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox11.Text = "";

            foreach (string codeLine in textBox3.Lines)
            {
                string trim_codeLine = codeLine.Replace("[HraAttribute] ", "");
                List<string> prototypeParts = new List<string>();
                string[] tokens = trim_codeLine.Split(new char[] { ' ' });
                foreach (string token in tokens)
                {
                    if (string.IsNullOrEmpty(token) == false)
                        prototypeParts.Add(token.Replace(";", ""));
                }
                if (prototypeParts.Count > 2)
                {
                    textBox4.Text += "/*****************************************************/ \r\n" +
                        "public " + prototypeParts[1] + " " + textBox5.Text + "_" + prototypeParts[2] + " \r\n" +
                        "     { \r\n" +
                        "         get \r\n" +
                        "         { \r\n" +
                        "            return " + prototypeParts[2] + "; \r\n" +
                        "         } \r\n" +
                        "         set \r\n" +
                        "         { \r\n" +
                        "             if (value != " + prototypeParts[2] + ") \r\n" +
                        "             { \r\n" +
                        "                 " + prototypeParts[2] + " = value; \r\n" +
                        "                 HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); \r\n" +
                        "                 args.updatedMembers.Add(GetMemberByName(\"" + prototypeParts[2] + "\")); \r\n" +
                        "                 SignalModelChanged(args); \r\n" +
                        "             } \r\n" +
                        "         } \r\n" +
                        "     } \r\n";

                    textBox11.Text +=
                        "/*****************************************************/ \r\n" +
                        "public void Set_" + prototypeParts[2] + "(" + prototypeParts[1] + " value, HraView sendingView)\r\n" +
                        "{ \r\n" +
                        "   if (" + prototypeParts[2] + " != value) \r\n" +
                        "   { \r\n" +
                        "       " + prototypeParts[2] + " = value; \r\n" +
                        "       HraModelChangedEventArgs args = new HraModelChangedEventArgs(null); \r\n" +
                        "       args.sendingView = sendingView; \r\n" +
                        "       args.updatedMembers.Add(GetMemberByName(\"" + prototypeParts[2] + "\")); \r\n" +
                        "       SignalModelChanged(args); \r\n" +
                        "   } \r\n" +
                        "} \r\n";

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void RecursiveScriptRun(string path)
        {
            if (SciptRunningWorker.CancellationPending)
                return;


            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] files = di.GetFiles("*.sql", SearchOption.AllDirectories);
            List<string> filesToRun = new List<string>();
            foreach (FileInfo fi in files)
            {
                filesToRun.Add(fi.FullName);
            }
            filesToRun.Sort();

            //run all script in this folder
            foreach (string fi in filesToRun)
            {
                if (SciptRunningWorker.CancellationPending)
                    return;

                SciptRunningWorker.ReportProgress(0, fi);

                string CommandLineString = "-S " + textBox10.Text + " -d " + textBox7.Text + " -i \"" + fi + "\"";
                //use following instead of above if trusted connection not good enough and need to use bcDB_User
                //string CommandLineString = "-S " + textBox10.Text + " -d " + textBox7.Text + " -U bcDB_User -P dbbc " + " -i \"" + fi + "\"";
                if (checkBox1.Checked == false)
                {
                    CommandLineString += " -U " + textBox12.Text + " -P " + textBox13.Text;
                }
                else
                {
                    CommandLineString += " -E";
                }

                //Console.WriteLine("Running database SQLCMD: " + CommandLineString);
                System.Diagnostics.ProcessStartInfo myInfo = new ProcessStartInfo("SQLCMD", CommandLineString);



                myInfo.UseShellExecute = false;
                myInfo.CreateNoWindow = true;
                myInfo.RedirectStandardOutput = true;

                Process p = new Process();
                p.StartInfo = myInfo;
                p.EnableRaisingEvents = true;
                p.OutputDataReceived += ScriptRunOutput;
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.CancelOutputRead();

            }

        }
        private string logScriptRunOutput()
        {
            string output = string.Empty;
            if (string.IsNullOrEmpty(labelLogCount.Text) == false)
            {
                string countStr = labelLogCount.Text;
                output = countStr + " scripts run.";
            }
            if (string.IsNullOrEmpty(labelLogString.Text) == false)
            {
                string logStr = labelLogString.Text;
                output += "\r\n\r\n" + logStr;
            }
            return output;
        }
        private void ScriptRunOutput(object sendingProcess, DataReceivedEventArgs outLine)
        {
            SciptRunningWorker.ReportProgress(1, outLine.Data);
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = (string)e.Argument;
            RecursiveScriptRun(path);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string f = (string)e.UserState;

            if (e.ProgressPercentage == 0)
            {
                count++;
                label8.Text = count.ToString();
                label6.Text += f + "\n";
                label17.Text += f + "\n";

                if (string.IsNullOrEmpty(f) == false)
                {
                    labelLogString.Text += f + "\r\n";
                }
                int logCount = int.Parse(labelLogCount.Text) + 1;
                labelLogCount.Text = logCount.ToString();
            }
            else if (e.ProgressPercentage == 1)
            {
                label17.Text += f + "\n";
                if (string.IsNullOrEmpty(f) == false)
                {
                    labelLogString.Text += f + "\r\n";
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            button2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SciptRunningWorker.CancelAsync();
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
            /*
             	[userLogin] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	            [userPassword] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [userLastName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [userFirstName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [userProviderID] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [userEmail] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [userFullName] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	            [newLogin] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_tblUsers_changePassword]  DEFAULT (N'Yes'),
	            [passwordChangeDate] [datetime] NULL,
             * 
                [HraAttribute] public int providerID;
                [HraAttribute] public bool defaultParagraph;
                [HraAttribute] public string paragraph;
                [HraAttribute] public string patientParagraph;
             */
            char[] tableSplitter = new char[] { ',' };
            char[] lineSplitter = new char[] { ' ' };

            string[] args = textBox9.Text.Split(tableSplitter);
            foreach (string line in args)
            {
                string trim_line = line.Trim();
                if (trim_line.StartsWith("["))
                {
                    string output = "[HraAttribute] public ";
                    string[] tokens = line.Split(lineSplitter);
                    if (tokens.Length > 1)
                    {

                        if (tokens[1].ToLower().Contains("nvarchar"))
                        {
                            output += "string ";
                        }
                        else if (tokens[1].ToLower().Contains("datetime"))
                        {
                            output += "DateTime ";
                        }
                        else if (tokens[1].ToLower().Contains("int"))
                        {
                            output += "int ";
                        }

                        output += (tokens[0].Replace("[", "").Replace("]", "")).Trim() + " ";

                        textBox8.Text += output.TrimEnd() + ";" + Environment.NewLine;
                    }
                }
            }
        }

        private void BrowseTemplates_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.SelectedPath = SourceTemplates.Text;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                SourceTemplates.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void BrowseSubTemplates_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.SelectedPath = SourceSub.Text;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                SourceSub.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void BrowseFinishedTemplates_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.SelectedPath = FinishedTemplates.Text;
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                FinishedTemplates.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void Publish_Click(object sender, EventArgs e)
        {

            string[] paths = new string[4];
            paths[0] = SourceTemplates.Text;
            paths[1] = SourceSub.Text;
            paths[2] = FinishedTemplates.Text;
            paths[3] = RecursiveCheckBox.Checked.ToString();

            PublishWorker.RunWorkerAsync(paths);
            progressBar2.Visible = true;

            button3.Visible = true;
            label15.Text = "";
            label16.Text = "";
            publishCount = 0;
        }

        private void PublishWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] paths = (string[])e.Argument;

            RecursivePublishRun(paths[0], paths[1], paths[2], paths[3]);
        }
        private void RecursivePublishRun(string source, string sub, string target, string recurse)
        {
            if (PublishWorker.CancellationPending)
                return;

            string CommandLineString = "PublishTemplates \"" + source + "\" \"" + sub + "\" \"" + target + "\"";

            //Console.WriteLine("Running database SQLCMD: " + CommandLineString);
            if (string.Compare(source, sub, true) != 0)
            {
                System.Diagnostics.ProcessStartInfo myInfo = new ProcessStartInfo("RiskAppUtils.exe", CommandLineString);

                myInfo.UseShellExecute = false;
                myInfo.CreateNoWindow = true;
                myInfo.RedirectStandardOutput = true;

                Process p = new Process();
                p.StartInfo = myInfo;
                p.EnableRaisingEvents = true;
                p.OutputDataReceived += PublishOutput;
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();
                p.CancelOutputRead();

                if (recurse == "True")
                {
                    DirectoryInfo di = new DirectoryInfo(source);
                    foreach (DirectoryInfo child in di.GetDirectories())
                    {
                        RecursivePublishRun(child.FullName, sub, Path.Combine(target, child.Name), recurse);
                    }
                }
            }
        }

        private void PublishOutput(object sendingProcess, DataReceivedEventArgs outLine)
        {
            PublishWorker.ReportProgress(0, outLine.Data);
        }


        private void PublishWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar2.Visible = false;
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PublishWorker.CancelAsync();
        }

        private void PublishWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            publishCount++;
            label16.Text = publishCount.ToString();
            string f = (string)e.UserState;
            label15.Text += f + "\n";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox6.Focus();
            progressBar1.Visible = true;
            button2.Visible = true;
            label6.Text = "";
            label17.Text = "";
            count = 0;
            label8.Text = count.ToString();
            SciptRunningWorker.RunWorkerAsync(textBox6.Text);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox12.Enabled = false;
                textBox13.Enabled = false;
            }
            else
            {
                textBox12.Enabled = true;
                textBox13.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DocumentGenerator dg = new DocumentGenerator();
            int docID = -1;
            int.TryParse(docIdtextBox.Text, out docID);

            int apptid = -1;
            int.TryParse(apptidTextBox.Text, out apptid);

            if (docID > 0 && apptid > 0 && string.IsNullOrEmpty(UnitnumTextBox.Text) == false)
            {
                string html = dg.GenerateHtmlDocument(docID, UnitnumTextBox.Text, apptid);
                webBrowser1.DocumentText = html;
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void printButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Print();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataTable data = BCDB2.Instance.getDataTable("select top 100 * from tblscriptsrun order by daterun desc");
            if (data != null)
            {
                dataGridView1.DataSource = data;

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            DataTable data = BCDB2.Instance.getDataTable("select * from tblscriptsrun order by daterun desc");
            if (data != null)
            {
                dataGridView1.DataSource = data;

            }
        }

        private void buttonLogResults_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.RestoreDirectory = true;
                sfd.DefaultExt = "log";
                sfd.Filter = "Log Files (*.log)|*.log|Text Files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.FileName = "DatabaseUpdates-" + DateTime.Now.ToString("yyyy-MM-dd.hh.mm");
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter outfile = new StreamWriter(sfd.FileName);
                    string str = logScriptRunOutput();
                    outfile.Write(str);
                    outfile.Close();
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Error logging database updates: " + ee.InnerException);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            progressBar3.Visible = true;
            progressBar3.Enabled = true;

            lkpDifWorker.RunWorkerAsync();

        }
        public DataTable CompareLkp(string table, SqlConnection con1, SqlConnection con2)
        {
            string query = "SELECT * FROM " + table;

            SqlCommand cmd1 = new SqlCommand(query, con1);
            SqlCommand cmd2 = new SqlCommand(query, con2);

            DataTable dt1 = new DataTable();
            dt1.Load(cmd1.ExecuteReader());

            DataTable dt2 = new DataTable();
            dt2.Load(cmd2.ExecuteReader());

            if (dt1.Columns.Count > 32 || dt2.Columns.Count > 32)
                return null;
            else
                try
                {
                    return getDifferentRecords(dt1, dt2);
                }
                catch (Exception e)
                {
                    return null;
                }
        }

        public List<string> GetLkps(SqlConnection con1, SqlConnection con2)
        {
            List<string> tables = new List<string>();
            string query = "select name from sys.tables where name like '%lkp%' order by name";

            SqlCommand cmd1 = new SqlCommand(query, con1);
            SqlCommand cmd2 = new SqlCommand(query, con2);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(0) == false)
                {
                    string t = reader.GetString(0);
                    if (tables.Contains(t) == false)
                        tables.Add(t);
                }
            }

            reader.Close();

            reader = cmd2.ExecuteReader();
            if (reader.Read())
            {
                if (reader.IsDBNull(0) == false)
                {
                    string t = reader.GetString(0);
                    if (tables.Contains(t) == false)
                        tables.Add(t);
                }
            }

            reader.Close();

            return tables;
        }

        public DataTable getDifferentRecords(DataTable FirstDataTable, DataTable SecondDataTable)
        {
            //Create Empty Table   
            DataTable ResultDataTable = new DataTable("ResultDataTable");

            //use a Dataset to make use of a DataRelation object   
            using (DataSet ds = new DataSet())
            {
                //Add tables   
                ds.Tables.AddRange(new DataTable[] { FirstDataTable.Copy(), SecondDataTable.Copy() });

                //Get Columns for DataRelation   
                DataColumn[] firstColumns = new DataColumn[ds.Tables[0].Columns.Count];
                for (int i = 0; i < firstColumns.Length; i++)
                {
                    firstColumns[i] = ds.Tables[0].Columns[i];
                }

                DataColumn[] secondColumns = new DataColumn[ds.Tables[1].Columns.Count];
                for (int i = 0; i < secondColumns.Length; i++)
                {
                    secondColumns[i] = ds.Tables[1].Columns[i];
                }

                //Create DataRelation   
                DataRelation r1 = new DataRelation(string.Empty, firstColumns, secondColumns, false);
                ds.Relations.Add(r1);

                DataRelation r2 = new DataRelation(string.Empty, secondColumns, firstColumns, false);
                ds.Relations.Add(r2);

                //Create columns for return table   
                for (int i = 0; i < FirstDataTable.Columns.Count; i++)
                {
                    ResultDataTable.Columns.Add(FirstDataTable.Columns[i].ColumnName, FirstDataTable.Columns[i].DataType);
                }

                //If FirstDataTable Row not in SecondDataTable, Add to ResultDataTable.   
                ResultDataTable.BeginLoadData();
                foreach (DataRow parentrow in ds.Tables[0].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r1);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }

                //If SecondDataTable Row not in FirstDataTable, Add to ResultDataTable.   
                foreach (DataRow parentrow in ds.Tables[1].Rows)
                {
                    DataRow[] childrows = parentrow.GetChildRows(r2);
                    if (childrows == null || childrows.Length == 0)
                        ResultDataTable.LoadDataRow(parentrow.ItemArray, true);
                }
                ResultDataTable.EndLoadData();
            }

            return ResultDataTable;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox16.Enabled = false;
                textBox17.Enabled = false;
            }
            else
            {
                textBox16.Enabled = true;
                textBox17.Enabled = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                textBox21.Enabled = false;
                textBox20.Enabled = false;
            }
            else
            {
                textBox21.Enabled = true;
                textBox20.Enabled = true;
            }
        }

        private void lkpDifWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            string con1String = "";
            string con2String = "";

            if (checkBox2.Checked)
                con1String = "Persist Security Info=False;Initial Catalog=" + textBox19.Text + ";Server=" + textBox18.Text + ";Integrated Security=true;";
            else
                con1String = "Persist Security Info=False;User ID=" + textBox17.Text + ";Password=" + textBox16.Text + ";Initial Catalog=" + textBox19.Text + ";Server=" + textBox18.Text + "";

            if (checkBox3.Checked)
                con2String = "Persist Security Info=False;Initial Catalog=" + textBox23.Text + ";Server=" + textBox22.Text + ";Integrated Security=true;";
            else
                con2String = "Persist Security Info=False;User ID=" + textBox21.Text + ";Password=" + textBox20.Text + ";Initial Catalog=" + textBox23.Text + ";Server=" + textBox22.Text + "";

            //richTextBox1.Text = "";

            SqlConnection connection1 = new SqlConnection(con1String);
            connection1.Open();

            SqlConnection connection2 = new SqlConnection(con2String);
            connection2.Open();


            List<string> tables = GetLkps(connection1, connection2);

            foreach (string table in tables)
            {
                DataTable res = CompareLkp(table, connection1, connection2);
                ListViewItem lvi = new ListViewItem();
                lvi.Text = table;

                if (res == null)
                    lkpDifWorker.ReportProgress(0, lvi);
                else if (res.Rows.Count > 0)
                {
                    string text = "";
                    foreach (DataRow row in res.Rows)
                    {
                        ;
                        for (int i = 0; i < res.Columns.Count; i++)
                        {
                            text += " | " + row[i].ToString();
                        }
                        text += "\n";
                    }
                    lvi.Tag = text;
                    lkpDifWorker.ReportProgress(1, lvi);
                }
                else
                    lkpDifWorker.ReportProgress(2, lvi);
            }
            connection1.Close();
            connection2.Close();
        }

        private void lkpDifWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ListViewItem lvi = (ListViewItem)e.UserState;
            if (e.ProgressPercentage == 0)
            {
                listBox3.Items.Add(lvi);
            }
            else if (e.ProgressPercentage == 1)
            {
                listBox1.Items.Add(lvi);
            }
            else if (e.ProgressPercentage == 2)
            {
                listBox2.Items.Add(lvi);
            }
        }

        private void lkpDifWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar3.Enabled = false;
            progressBar3.Visible = false;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem lvi = (ListViewItem)listBox1.SelectedItem;
            if (lvi.Tag != null)
            {
                richTextBox1.Text = (string)lvi.Tag;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int apptid = 0;
            int.TryParse(textBox24.Text, out apptid);

            if (apptid > 0)
            {
                SqlDataReader reader = BCDB2.Instance.ExecuteReader("select unitnum from tblappointments where apptid = " + apptid);
                if (reader.Read())
                {
                    string unit = reader.GetString(0);
                    RiskApps3.Model.PatientRecord.Patient thePatient = new RiskApps3.Model.PatientRecord.Patient(unit);
                    thePatient.apptid = apptid;
                    thePatient.LoadFullObject();


                    //object o = BCDB2.Instance.ExecuteSpWithRetVal("sp_createMasteryAppointment",SqlDbType.Int);

                    //DataContractSerializer ds = new DataContractSerializer(typeof(Patient));
                    //using (Stream s = File.Create("temp.xml"))
                    //{
                    //    ds.WriteObject(s, thePatient);
                    //    s.Position = 0;
                    //    Patient p2 = (Patient)ds.ReadObject(s);

                    //    p2.apptid = (int)o;

                    //    p2.FHx.proband = p2;

                    //    p2.BackgroundPersistWork(new RiskApps3.Model.HraModelChangedEventArgs(null));

                    //    foreach (Person rel in p2.FHx)
                    //    {
                    //        rel.BackgroundPersistWork(new RiskApps3.Model.HraModelChangedEventArgs(null));
                    //    }
                    //}
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            progressBar4.Visible = true;
            progressBar4.Enabled = true; 

            button13.Enabled = false;
            listView2.Enabled = false; 
            work_papers wp = new work_papers();
            wp.mode = 1;
            listView2.Items.Clear();
            migrantWorker.RunWorkerAsync(wp);

        }
        private string GetConnectionStringLeft()
        {
            string retval = "Data Source=" + textBox31.Text + ";Initial Catalog=" + textBox32.Text + ";APP=RISKAPP3;MultipleActiveResultSets=True;Persist Security Info=True;";
            if (checkBox5.Checked)
                retval += "Integrated Security=true;";
            else
                retval += "User ID=" + textBox30.Text + ";PWD=" + textBox29.Text + ";";

            return retval;
        }
        private string GetConnectionStringRight()
        {
            string retval = "Data Source=" + textBox27.Text + ";Initial Catalog=" + textBox28.Text + ";APP=RISKAPP3;MultipleActiveResultSets=True;Persist Security Info=True;";
            if (checkBox4.Checked)
                retval += "Integrated Security=true;";
            else
                retval += "User ID=" + textBox26.Text + ";PWD=" + textBox25.Text + ";";

            return retval;
        }
        private class work_papers
        {
            public int mode;
            public List<Appointment> appts;
        }
        private void migrantWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            work_papers wp = (work_papers)e.Argument;
            e.Result = -1;
            if (wp.mode == 1)
            {
                string orig_con_string = BCDB2.Instance.getConnectionString();
                string new_con_string = GetConnectionStringLeft();
                BCDB2.Instance.setConnectionString(new_con_string);
                SqlDataReader reader = BCDB2.Instance.ExecuteReader("select apptid,patientname,unitnum,apptdatetime,dob from tblappointments " + textBox41.Text);
                BCDB2.Instance.setConnectionString(orig_con_string);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Appointment a = new Appointment();
                        a.apptID = reader.GetInt32(0);
                        a.patientname = reader.GetString(1);
                        a.unitnum = reader.GetString(2);
                        a.apptdatetime = reader.GetDateTime(3);
                        a.dob = reader.GetString(4);

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = a.apptID.ToString();
                        lvi.SubItems.Add(a.patientname);
                        lvi.SubItems.Add(a.unitnum);
                        lvi.SubItems.Add(a.apptdatetime.ToShortDateString());
                        lvi.SubItems.Add(a.dob);
                        lvi.Tag = a;
                        migrantWorker.ReportProgress(1, lvi);
                    }
                }
            }
            else if (wp.mode == 2)
            {
                string orig_con_string = BCDB2.Instance.getConnectionString();
                string new_con_string = GetConnectionStringRight();
                BCDB2.Instance.setConnectionString(new_con_string);
                SqlDataReader reader = BCDB2.Instance.ExecuteReader("select apptid,patientname,unitnum,apptdatetime,dob from tblappointments " + textBox42.Text);
                BCDB2.Instance.setConnectionString(orig_con_string);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Appointment a = new Appointment();
                        a.apptID = reader.GetInt32(0);
                        a.patientname = reader.GetString(1);
                        a.unitnum = reader.GetString(2);
                        a.apptdatetime = reader.GetDateTime(3);
                        a.dob = reader.GetString(4);

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = a.apptID.ToString();
                        //lvi.SubItems.Add(a.apptID);
                        lvi.SubItems.Add(a.patientname);
                        lvi.SubItems.Add(a.unitnum);
                        lvi.SubItems.Add(a.apptdatetime.ToShortDateString());
                        lvi.SubItems.Add(a.dob);
                        lvi.Tag = a;
                        migrantWorker.ReportProgress(2, lvi);
                    }
                }
            }
            else if (wp.mode == 3)
            {
                string orig_con_string = BCDB2.Instance.getConnectionString();
                try
                {
                    foreach (Appointment a in wp.appts)
                    {
                        BCDB2.Instance.setConnectionString(GetConnectionStringLeft());

                        RiskApps3.Model.PatientRecord.Patient thePatient = new RiskApps3.Model.PatientRecord.Patient(a.unitnum);
                        thePatient.apptid = a.apptID;
                        thePatient.LoadFullObject();

                        BCDB2.Instance.setConnectionString(GetConnectionStringRight());

                        
                        DataContractSerializer ds = new DataContractSerializer(typeof(Patient));
                        MemoryStream stm = new MemoryStream();
                        ds.WriteObject(stm, thePatient);
                        stm.Flush();
                        stm.Position = 0;

                        RiskApps3.Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
                        pc.Add("unitnum", thePatient.unitnum);
                        pc.Add("apptdate", thePatient.apptdatetime.ToString("MM/dd/yyyy"));
                        pc.Add("appttime", thePatient.apptdatetime.ToShortTimeString());
                        pc.Add("patientname", thePatient.name);
                        pc.Add("dob", thePatient.dob);
                        object o = BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_createMasteryAppointment", SqlDbType.Int, pc);

                        Patient p2 = (Patient)ds.ReadObject(stm);
                        p2.apptid = (int)o;
                        p2.FHx.proband = p2;
                        p2.PersistFullObject(new RiskApps3.Model.HraModelChangedEventArgs(null));

                        BCDB2.Instance.setConnectionString(orig_con_string);

                        e.Result = 1;
                    }
                }
                catch (Exception ee)
                {
                    BCDB2.Instance.setConnectionString(orig_con_string);
                    MessageBox.Show(ee.ToString()); 
                }
                migrantWorker.ReportProgress(3);
            }
        }

        private void migrantWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                ListViewItem lvi = (ListViewItem)(e.UserState);
                listView2.Items.Add(lvi);
                listView2.Refresh();
                label52.Text = listView2.Items.Count.ToString();
            }
            else if (e.ProgressPercentage == 2)
            {
                ListViewItem lvi = (ListViewItem)(e.UserState);
                listView3.Items.Add(lvi);
                listView3.Refresh();
                label53.Text = listView3.Items.Count.ToString();
            }
            //else if (e.ProgressPercentage == 1)
            //{

            //}
        }

        private void migrantWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            foreach (Control c in tabPage13.Controls)
            {
                c.Enabled = true;
            }
            progressBar4.Visible = false;
            progressBar4.Enabled = false; 

            int result = (int)e.Result;
            if (result > 0)
            {
                work_papers wp = new work_papers();
                wp.mode = 2;
                listView3.Items.Clear();
                button15.Enabled = false;
                listView3.Enabled = false;
                progressBar4.Visible = true;
                progressBar4.Enabled = true;
                migrantWorker.RunWorkerAsync(wp);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            progressBar4.Visible = true;
            progressBar4.Enabled = true;   
            
            button15.Enabled = false;
            listView3.Enabled = false;

            work_papers wp = new work_papers();
            wp.mode = 2; 
            listView3.Items.Clear();
            migrantWorker.RunWorkerAsync(wp);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
            foreach (Control c in tabPage13.Controls)
            {
                c.Enabled = false;
            }
            progressBar4.Visible = true;
            progressBar4.Enabled = true; 
            

            List<Appointment> toCopy = new List<Appointment>();
            foreach (ListViewItem lvi in listView2.SelectedItems)
            {
                toCopy.Add((Appointment)lvi.Tag);
            }
            work_papers wp = new work_papers();
            wp.mode = 3;
            wp.appts = toCopy;
            migrantWorker.RunWorkerAsync(wp);
        }

        /******************** Password Encryption TabPage ********************************/

        private void button17encrypt_Click(object sender, EventArgs e)
        {
            progressBar6encrypt.Visible = true;
            labelEncryptCount.Visible = true;
            labelNumEncrypted.Visible = true;

            encryptWorker.RunWorkerAsync();
        }

        int passwordsEncrypted = 0;
        bool encryptionError = false;
        bool encryptedPasswords = false;
        private void encryptWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> users = new List<string>();
            int progress = 0;
            string userPassword = "";
            string userLogin = "";
            ParameterCollection pc = new ParameterCollection();
            SqlDataReader reader;

            try
            {
                pc.Clear();
                reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_Get_EncryptPasswords_Global", pc);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        encryptedPasswords = (bool)reader.GetSqlBoolean(0);
                    }
                    reader.Close();
                }

                if (encryptedPasswords == false)  // SessionManager.Instance.MetaData.Globals.encryptPasswords
                {
                    BCDB2.Instance.ExecuteNonQuery("sp_3_Backup_UserLoginList");

                    pc.Clear();
                    reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_Get_UserLoginList", pc);
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            users.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }

                    foreach (string name in users)
                    {
                        userLogin = name;

                        pc.Clear();
                        pc.Add("userLogin", userLogin);
                        reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_Get_UserPassword", pc);
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                userPassword = reader.GetString(0);
                            }
                            reader.Close();
                        }
                        if (string.IsNullOrEmpty(userPassword) == false)
                        {
                            pc.Clear();
                            string encryptedPassword = RiskAppCore.User.encryptPassword(userPassword);

                            pc.Add("userLogin", userLogin);
                            pc.Add("userPassword", encryptedPassword);
                            int result = (int)RiskApps3.Utilities.BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_3_Save_UserPassword", SqlDbType.Int, pc);
                        }
                        passwordsEncrypted++;
                        progress = passwordsEncrypted * 100 / users.Count;
                        encryptWorker.ReportProgress(progress, passwordsEncrypted);
                    }
                    BCDB2.Instance.ExecuteNonQuery("sp_3_Set_EncryptPasswords_Global");
                }
                else
                {
                    encryptWorker.ReportProgress(100, users.Count);
                }
            }
            catch (Exception encryptEx)
            {
                Logger.Instance.WriteToLog("Error encrypting password for user " + userLogin + ": " + encryptEx.InnerException.ToString());
                encryptionError = true;
            }
        }

        private void encryptWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelEncryptCount.Text = ((int)e.UserState).ToString("G");
        }

        private void encryptWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar6encrypt.Visible = false;
            if (encryptionError == true)
            {
                MessageBox.Show("Error encrypting passwords.  Recommend restoring table tblUser passwords from table temp_UserLogins* in the database.");
            }
            if (encryptedPasswords == true)
            {
                MessageBox.Show("Password encryption already in effect.");
            }            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                textBox43.Text += (strPrinter + Environment.NewLine);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ExportWorkerArgs ewa = new ExportWorkerArgs();

            ewa.outputPath = textBox44.Text;

            if (radioButton1.Checked)
                ewa.mode = 1;
            else if (radioButton2.Checked)
                ewa.mode = 2;
            else
                ewa.mode = 3;

            progressBar6.Enabled = true;
            progressBar6.Visible = true;

            ewa.sql = richTextBox2.Text;
            
            ExportWorker1.RunWorkerAsync(ewa);
        }

        private void ExportWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            ExportWorkerArgs ewa = (ExportWorkerArgs)(e.Argument);

            SqlDataReader reader = BCDB2.Instance.ExecuteReader(ewa.sql);
            while (reader.Read())
            {
                int apptid = reader.GetInt32(1);
                string unit = reader.GetString(0);
                RiskApps3.Model.PatientRecord.Patient thePatient = new RiskApps3.Model.PatientRecord.Patient(unit);
                thePatient.apptid = apptid;
                thePatient.LoadFullObject();

                string fhAsString = TransformUtils.DataContractSerializeObject<RiskApps3.Model.PatientRecord.FHx.FamilyHistory>(thePatient.FHx);

                if (ewa.mode == 1)
                {
                    System.IO.File.WriteAllText(Path.Combine(ewa.outputPath, unit + ".xml"), fhAsString);
                }
                else if (ewa.mode == 2)
                {
                    //transform it
                    XmlDocument inDOM = new XmlDocument();
                    inDOM.LoadXml(fhAsString);
                    string toolsPath = RiskApps3.Utilities.Configurator.getNodeValue("Globals", "ToolsPath"); // @"C:\Program Files\riskappsv2\tools\";

                    XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hra_to_ccd_remove_namespaces.xsl");
                    XmlDocument hl7FHData = TransformUtils.performTransformWithParam(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "deIdentify", "0");

                    hl7FHData.Save(Path.Combine(ewa.outputPath, unit + ".xml"));
                }
                else
                {
                                        //transform it
                    XmlDocument inDOM = new XmlDocument();
                    inDOM.LoadXml(fhAsString);
                    string toolsPath = RiskApps3.Utilities.Configurator.getNodeValue("Globals", "ToolsPath"); // @"C:\Program Files\riskappsv2\tools\";

                    XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hra_to_ccd_remove_namespaces.xsl");
                    XmlDocument hl7FHData = TransformUtils.performTransformWithParam(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "deIdentify", "1");

                    hl7FHData.Save(Path.Combine(ewa.outputPath, unit + ".xml"));
                }
            }
        }

        private void ExportWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void ExportWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar6.Enabled = false;
            progressBar6.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox44.Text = folderBrowserDialog3.SelectedPath;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox30.Enabled = true;
            textBox29.Enabled = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox25.Enabled = true;
            textBox26.Enabled = true;
        }

        private void checkBox8windowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            textBox44.Enabled = true;
            textBox43.Enabled = true;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            initializeDropDowns();
        }

        private void initializeDropDowns()
        {
            string surveyQuery = "SELECT surveyID, surveyName FROM lkpsurveys WHERE inactive = 0";
            string clinicQuery = "SELECT clinicID, clinicName FROM lkpclinics";

            try
            {
                surveyDic = getDicValues(surveyQuery);
                clinicDic = getDicValues(clinicQuery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            comboBox2.DataSource = new BindingSource(surveyDic, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "key";


            comboBox3.DataSource = new BindingSource(clinicDic, null);
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "key";

            dateTimePicker1.Value = DateTime.Now;

        }

        private Dictionary<int, string> getDicValues(string commandString)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(commandString))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return dictionary;
            }


        }

        private void button21_Click(object sender, EventArgs e)
        {
            int surveyID = 0;

            foreach (KeyValuePair<int, string> pair in surveyDic)
            {
                if (pair.Value == comboBox2.Text)
                {
                    surveyID = pair.Key;
                }
            }


            int x = 0;
            if (comboBox1.SelectedItem != null)
            {
                int i = int.Parse(comboBox1.SelectedItem.ToString());

                string time;

                string nextUnitNum = createTestUnitnum();
                long unitNum = Int64.Parse(nextUnitNum);

                while (i > x)
                {
                    RiskApps3.Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();

                    if (x <= 9)
                    {
                        time = "0" + x.ToString();
                    }
                    else
                    {
                        time = x.ToString();
                    }

                    pc.Add("apptdate", dateTimePicker1.Value.ToShortDateString());
                    pc.Add("appttime", "08:" + time + "AM");
                    pc.Add("firstname", "firstName");
                    pc.Add("lastname", "TestPatient");
                    pc.Add("gender", "F");
                    pc.Add("dob", "11/22/1960");
                    pc.Add("email", "test@test.com");
                    pc.Add("address1", "1 Broadway");
                    pc.Add("city", "Cambridge");
                    pc.Add("state", "MA");
                    pc.Add("zip", "02473");
                    pc.Add("phone", "6175147186");
                    pc.Add("emailaddress", "test@test.com");
                    pc.Add("sendByEmail", 1);
                    pc.Add("machinename", "test");
                    pc.Add("preferredInstitutionName", "test");
                    pc.Add("clinicID", 1);
                    pc.Add("unitnum", unitNum.ToString());
                    pc.Add("createdby", comboBox2.Text);
                    pc.Add("insertApptWebRecord", 1);
                    pc.Add("surveyID", surveyID);

                    BCDB2.Instance.ExecuteReaderSPWithParams("sp_createWebAppointment", pc);
                    unitNum++;
                    x++;
                }
            }
        }

        private string createTestUnitnum()
        {
            String mrn = "999";
            mrn = mrn + String.Format("{0:MMddyy}", DateTime.Now);

            int i = 1;
            while (true)
            {
                String possibleMRN = mrn + i.ToString("00");

                if (BCDB2.Instance.ExecuteReader("SELECT * FROM tblAppointments WHERE unitnum='" + possibleMRN + "'").HasRows == false)
                {
                    return possibleMRN;
                }
                i++;
            }
        }

        public class IntegerComparer : IComparer
        {
            private int _colIndex;
            public IntegerComparer(int colIndex)
            {
                _colIndex = colIndex;
            }
            public int Compare(object x, object y)
            {
                int nx = int.Parse((x as ListViewItem).SubItems[_colIndex].Text);
                int ny = int.Parse((y as ListViewItem).SubItems[_colIndex].Text);
                return nx.CompareTo(ny);
            }
        }

        // begin jdg 9/15/15 supporting code for survey demo launcher tabl
        private void btnDirectoryPicker_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDirectory.Text))
                folderBrowserDialog4.SelectedPath = txtDirectory.Text;
            folderBrowserDialog4.ShowDialog();
            txtDirectory.Text = folderBrowserDialog4.SelectedPath;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDirectory.Text))
            {
                watch(txtDirectory.Text);
                progressBar7.Style = ProgressBarStyle.Marquee;
            }
            else
            {
                MessageBox.Show("Please choose a directory to monitor.", "HRA Demo Document Launcher", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                progressBar7.Style = ProgressBarStyle.Continuous;
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            progressBar7.Style = ProgressBarStyle.Continuous;
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
        }

        private void watch(string path)
        {
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            bool bReady = WaitForFile(e.FullPath);

            if ((bReady) && (prevName != e.FullPath))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ListBoxResults.Items.Add(e.Name);
                });
                Process.Start(e.FullPath);
                prevName = e.FullPath;
            }
            else
            {
                prevName = "";
            }
        }

        /// <summary>
        /// Blocks until the file is not locked any more.
        /// </summary>
        /// <param name="fullPath"></param>
        bool WaitForFile(string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    if (numTries > 10)
                    {
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // same as pause now... when this was a winForm app, this contained a close()
            progressBar7.Style = ProgressBarStyle.Continuous;
            watcher.Changed -= new FileSystemEventHandler(OnChanged);
        }
        // end jdg 9/15/15

        /********************************************************************************************/
    }
    public class ExportWorkerArgs
    {
        public int mode = 1;
        public string outputPath = "";
        public string sql = "";
    }
}

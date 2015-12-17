using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
using System.IO;
using RiskApps3.Controllers;
using RiskApps3.Utilities;
using RiskAppUtils;
using RiskApps3.View.PatientRecord.Pedigree;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.View.RiskClinic;
using System.Diagnostics;
using Microsoft.Win32;
using RiskApps3.Model.Clinic;
using HtmlAgilityPack;
using RiskApps3.View.Common;
using EvoPdf;
using System.Drawing.Printing;
using Foxit.PDF.Printing;


namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class PatientCommunicationView : HraView
    {
        /**************************************************************************************************/
        public Task InitialTask;

        private Patient proband;

        //private List<HraView> windows = new List<HraView>();

        private string baseOutputPath; // = "C:\\Program Files\\RiskAppsV2\\documents";
        private List<string> files = new List<string>();

        private TaskViewUC taskViewUC;

        public string routineName = "";

        bool stateoverride = false;

        bool documentChangeAbort = false;

        private char[] trimchars = { '\\' };
        /**************************************************************************************************/


        public bool PatientHeaderVisible
        {
            get { return patientRecordHeader1.Visible; }
            set
            {
                patientRecordHeader1.Visible = value;
                if (value)
                {
                    splitContainer1.Location = new Point(patientRecordHeader1.Location.X,
                                                         patientRecordHeader1.Height + 3);
                }
                else
                {
                    splitContainer1.Location = patientRecordHeader1.Location;
                    splitContainer1.Height += patientRecordHeader1.Height;
                }
            }
        }

        public System.Windows.Forms.Orientation Orientation
        {
            get { return splitContainer1.Orientation; }
            set
            {
                splitContainer1.Orientation = value;
            }
        }



        /**************************************************************************************************/

        public PatientCommunicationView()
        {
            InitializeComponent();
        }

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

            //2012-12-18 BGS this stuff hangs around forever and i'm not sure why...
            if (this.fileSystemWatcher1 != null)
                this.fileSystemWatcher1.Dispose();

            //base.Dispose(disposing);
        }

        /**************************************************************************************************/

        private void PatientCommunicationView_Load(object sender, EventArgs e)
        {
            //this.winFormHtmlEditor1.BtnImage.Click += winFormHtmlEditor1_BtnImage_Click;
            winFormHtmlEditor1.ToolbarItemOverrider.ImageButtonClicked += winFormHtmlEditor1_BtnImage_Click;

            winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.ReadOnly_Preview;

            htmlEditorPanel.Visible = false;
            NoPreviewPanel.Visible = false;

            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += NewActivePatient;
                proband = SessionManager.Instance.GetActivePatient();
                if (proband != null)
                {
                    proband.AddHandlersWithLoad(null, activePatientLoaded, null);
                    loadingCircle1.Enabled = true;
                    loadingCircle1.Visible = true;

                }
                else
                {
                    Enabled = false;
                }
            }
            try
            {
                string dicPath = Environment.GetEnvironmentVariable("appdata") +  @"\microsoft\uproof";
                if (Directory.Exists(dicPath))
                {
                    foreach (string s in Directory.GetFiles(dicPath, "*.dic", SearchOption.TopDirectoryOnly))
                    {
                        winFormHtmlEditor1.SpellCheckOptions.DictionaryFile.UserDictionaryFilePath = s;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteToLog(ex.ToString());
            }
        }

        private void addFileToList(String fullFileName)
        {
            FileInfo f = new FileInfo(fullFileName);
            if (f.Extension.Length > 0)
            {
                if (fullFileName.Contains('~') == false && files.Contains(fullFileName) == false)
                {
                    files.Add(fullFileName);
                    CommunicationEntry ce = new CommunicationEntry();
                    ce.title = Path.GetFileName(fullFileName);
                    ce.Tag = fullFileName;
                    FileInfo fi = new FileInfo(fullFileName);
                    ce.date = fi.LastWriteTime;
                    objectListView1.AddObject(ce);
                    objectListView1.Columns[1].Width = -1;
                    
                }
            }
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            ClearControls();

            patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());

            loadingCircle1.Visible = true;
            loadingCircle1.Enabled = true;

            if (proband != null)
                proband.ReleaseListeners(this);

            proband = e.newActivePatient;
            if (proband != null)
            {
                Enabled = true;
                proband.AddHandlersWithLoad(null, activePatientLoaded, null);
                
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                Enabled = false;
            }
        }
        /**************************************************************************************************/

        private void PatientCommunication_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (taskViewUC != null)
            {
                taskViewUC.ValidateChildren();
                taskViewUC.Release();
            }

            //Invalidate(true);
            
            //releaseWord();
            patientRecordHeader1.ReleaseListeners();
            
            //not strictly necesssary but allows for quicker disposal of this view and the file watcher
            this.fileSystemWatcher1.Renamed -= new System.IO.RenamedEventHandler(this.fileSystemWatcher1_Renamed);
            this.fileSystemWatcher1.Deleted -= new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            this.fileSystemWatcher1.Created -= new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Changed -= new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);

            //foreach (HraView hv in windows)
            //{
            //    hv.Close();
            //}
            SessionManager.Instance.RemoveHraView(this);
        }

        /**************************************************************************************************/

        private void ClearControls()
        {
            files.Clear();
            objectListView1.Items.Clear();
            foreach(Control c in splitContainer1.Panel2.Controls)
            {
                c.Visible = false;
            }
        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                baseOutputPath = DocumentTemplate.getLetterDirectory(proband.apptid, proband.unitnum, proband.name);
                fileSystemWatcher1.Path = baseOutputPath;

                linkLabel1.Text = baseOutputPath.TrimEnd(trimchars);

                backgroundWorker1.RunWorkerAsync(); 
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }


            Enabled = true;
            proband.Tasks.AddHandlersWithLoad(TaskListChanged,
                                              TaskListLoaded,
                                              TaskChanged);
        }

        /**************************************************************************************************/

        private void TaskListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Task theTask = (Task) e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        CommunicationEntry ce = new CommunicationEntry();
                        ce.title = theTask.Task_Type;
                        ce.Tag = theTask;
                        ce.author = theTask.Task_AssignedBy;
                        ce.date = theTask.Task_Date;
                        objectListView1.AddObject(ce);
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        object doomed = null;
                        ;
                        foreach (object o in objectListView1.Objects)
                        {
                            CommunicationEntry doomed_ce = (CommunicationEntry) o;
                            if (doomed_ce.Tag == theTask)
                            {
                                doomed = o;
                            }
                        }
                        if (doomed != null)
                        {
                            if (splitContainer1.Panel2.Controls.Contains(taskViewUC))
                            {
                                if (taskViewUC.Task == theTask)
                                {
                                    taskViewUC.Release();
                                    splitContainer1.Panel2.Controls.Remove(taskViewUC);

                                }
                                
                            }
                            objectListView1.RemoveObject(doomed);

                        }
                        break;
                }
            }
        }

        /**************************************************************************************************/

        private void TaskListLoaded(HraListLoadedEventArgs e)
        {
            FillControls();
        }

        private delegate void fillControlsCallback();

        public void FillControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                fillControlsCallback fcc = new fillControlsCallback(FillControls);
                this.Invoke(fcc, null);
            }
            else
            {
                foreach (Task theTask in proband.Tasks.OrderByDescending(t => ((Task)t).Task_Date))
                {
                    CommunicationEntry ce = new CommunicationEntry();
                    ce.title = theTask.Task_Type;
                    ce.date = theTask.Task_Date;
                    ce.Tag = theTask;
                    ce.author = theTask.Task_AssignedBy;

                    bool add = true;
                    if (objectListView1.Objects != null)
                    {
                        foreach (object o in objectListView1.Objects)
                        {
                            if (o is CommunicationEntry)
                            {
                                CommunicationEntry ce2 = (CommunicationEntry) o;
                                if (ce2.Tag is Task)
                                {
                                    if (ce2.Tag == theTask)
                                    {
                                        add = false;
                                    }
                                }
                            }
                        }
                    }
                    if (add)
                    {
                        objectListView1.AddObject(ce);
                    }
                    if (theTask == InitialTask)
                    {
                        objectListView1.SelectedObject = ce;
                    }
                }
                
                loadingCircle1.Visible = false;
                loadingCircle1.Enabled = false;
            }
        }

        /**************************************************************************************************/

        private void TaskChanged(object sender, HraModelChangedEventArgs e)
        {
            foreach (object o in objectListView1.Objects)
            {
                CommunicationEntry ce = (CommunicationEntry) o;
                if (ce.Tag == sender)
                {
                    ce.title = ((Task) ce.Tag).Task_Type;
                    objectListView1.RefreshObject(o);
                }
            }
        }

        /**************************************************************************************************/

        private class CommunicationEntry
        {
            public string title;
            public object Tag;
            public DateTime date;
            public string author;
        }

        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (!documentChangeAbort)
            {
                if (DiscardButton.Enabled == true)
                {
                    if (MessageBox.Show("Are you sure you want to change documents?  This will discard all your changes since your last save.", "Discard Changes", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        if (objectListView1.Tag != null)
                        {
                            documentChangeAbort = true;
                            objectListView1.SelectObject(objectListView1.Tag);
                        }
                        return;
                    }
                }

                stateoverride = false;

                winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.ReadOnly_Preview;
                winFormHtmlEditor1.Toolbar1.Visible = false;
                winFormHtmlEditor1.Toolbar2.Visible = false;
                winFormHtmlEditor1.ToolbarFooter.Visible = false;
                EditButton.Visible = true;
                SaveButton.Visible = false;
                DiscardButton.Enabled = false;

                NoPreviewPanel.Visible = false;
                htmlEditorPanel.Visible = false;
                if (objectListView1.SelectedObject != null)
                {
                    AddCommunicationView((CommunicationEntry)objectListView1.SelectedObject);
                    objectListView1.Tag = objectListView1.SelectedObject;
                }
            }
            documentChangeAbort = false;
        }
        private void AddCommunicationView(CommunicationEntry ce)
        {
            FileNameLabel.Text = "";
            if (ce.Tag != null)
            {
                if (ce.Tag is Task)
                {
                    if (splitContainer1.Panel2.Controls.Contains(taskViewUC))
                    {
                        taskViewUC.Release();
                        splitContainer1.Panel2.Controls.Remove(taskViewUC);
                    }

                    htmlEditorPanel.Visible = false;
                    NoPreviewPanel.Visible = false;

                    taskViewUC = new TaskViewUC();
                    taskViewUC.Task = (Task)ce.Tag;
                    taskViewUC.Dock = DockStyle.Fill;

                    splitContainer1.Panel2.Controls.Add(taskViewUC);
                }

                else if (ce.Tag is string)
                {
                    //splitContainer1.Panel2.Controls.Clear();
                    //htmlEditorPanel.Visible = true;
                    splitContainer1.Panel2.Controls.Add(htmlEditorPanel);

                    string name = (string) ce.Tag;

                    if (string.IsNullOrEmpty(name) == false)
                    {
                        switch (Path.GetExtension(name).ToLower())
                        {
                            case ".html":
                            case ".htm":
                                htmlEditorPanel.Visible = true;
                                FileNameLabel.Text = Path.GetFileName(name);
                                string s = File.ReadAllText(name);
                                winFormHtmlEditor1.DocumentHtml = s;
                                winFormHtmlEditor1.Tag = name;
                                break;
                            case ".doc":
                            case ".docx":
                                NoPreviewPanel.Visible = true;
                                htmlEditorPanel.Visible = false;
                                //OpenWithWord(name);
                                break;
                            case ".pdf":
                                NoPreviewPanel.Visible = true;
                                htmlEditorPanel.Visible = false;
                                //OpenPDF(name);
                                break;
                            default:
                                break;

                        }
                    }
                }
            }
        }

        private void OpenWithWord(string name)
        {
            using (var regWord = Registry.ClassesRoot.OpenSubKey("Word.Application"))
            {
                if (regWord == null)
                {
                    MessageBox.Show("This function requires Microsoft Word.\n\nInstall Microsoft Word and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "WINWORD.EXE";
                    startInfo.Arguments = "\"" + name + "\"";
                    Process.Start(startInfo);
                }
            }
        }

        private void OpenPDF(string name)
        {
            using (var regWord = Registry.ClassesRoot.OpenSubKey(".pdf"))
            {
                if (regWord == null)
                {
                    MessageBox.Show("This function requires a PDF viewer.\n\nInstall a PDF reader and try again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    //ProcessStartInfo startInfo = new ProcessStartInfo();
                    //startInfo.FileName = "cmd.exe";
                    //startInfo.Arguments = @"start """" /max " + "\"" + name + "\"";
                    //Process.Start(startInfo);

                    //Process.Start(@""""" /max " + "\"" + name + "\"");

                    Process.Start(name);
                }
            }
        }

        private void objectListView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (objectListView1.SelectedObject != null)
                {
                    CommunicationEntry ce = (CommunicationEntry)objectListView1.SelectedObject;
                    if (ce.Tag != null)
                    {
                        if (ce.Tag is Task)
                        {
                            deleteTaskToolStripMenuItem.Visible = true;
                            deleteToolStripMenuItem.Visible = false;
                        }
                        else if (ce.Tag is string)
                        {
                            deleteTaskToolStripMenuItem.Visible = false;
                            deleteToolStripMenuItem.Visible = true;
                        }
                        contextMenuStrip1.Show(objectListView1.PointToScreen(e.Location));
                    }
                }
            }


        }

        private void deleteTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                CommunicationEntry ce = (CommunicationEntry) objectListView1.SelectedObject;
                if (ce.Tag != null)
                {
                    if (ce.Tag is Task)
                    {
                        Task doomed = (Task)ce.Tag;
                        while (doomed.FollowUps.Count > 0)
                        {
                            doomed.FollowUps.RemoveFromList(doomed.FollowUps[0], SessionManager.Instance.securityContext);
                        }
                        proband.Tasks.RemoveFromList(doomed, SessionManager.Instance.securityContext);
                    }
                }
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (proband != null)
            {
                string fullFilePath = Path.Combine(fileSystemWatcher1.Path, e.Name);

                if (fullFilePath.Contains('~') == false &&
                    fullFilePath.Contains(".tmp") == false &&
                    files.Contains(fullFilePath) == false &&
                    e.Name.Contains("_" + proband.unitnum + "_"))
                {
                    addFileToList(fullFilePath);
                }
            }
        }
        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (proband != null)
            {
                string fullFilePath = Path.Combine(fileSystemWatcher1.Path, e.Name);

                if (fullFilePath.Contains('~') == false &&
                    fullFilePath.Contains(".tmp") == false &&
                    files.Contains(fullFilePath) == false &&
                    e.Name.Contains("_" + proband.unitnum + "_"))
                {
                    addFileToList(fullFilePath);
                }
            }
        }
        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            if (proband != null)
            {
                string fullFilePath = Path.Combine(fileSystemWatcher1.Path, e.Name);

                if (fullFilePath.Contains('~') == false &&
                    fullFilePath.Contains(".tmp") == false &&
                    files.Contains(fullFilePath) == false &&
                    e.Name.Contains("_" + proband.unitnum + "_"))
                {
                    addFileToList(fullFilePath);
                }
            }
        }


        private void NewNote_Click(object sender, EventArgs e)
        {
            Task t = new Task(proband, "Note", null, SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }

        private void NewTask_Click(object sender, EventArgs e)
        {
            Task t = new Task(proband, "Task", "Pending", SessionManager.Instance.ActiveUser.ToString(), DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = true;
            proband.Tasks.AddToList(t, args);
        }

        private void splitContainer1_Resize(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.SplitterDistance = 510;
            }
            catch(Exception)
            {}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewHtmlDocumentForm nhdf = new NewHtmlDocumentForm();
            if (string.IsNullOrEmpty(routineName) == false)
                nhdf.routine = routineName;

            nhdf.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(baseOutputPath);
            if (di.Exists)
            {
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = di.FullName;
                try
                {
                    prc.Start();
                }
                catch (Exception)
                {

                    MessageBox.Show("Unable to open directory: " + di.FullName +
                        ".  Consider contacting your system administrator.",
                        "Unable to open directory",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string templateDirectory = Configurator.GetDocumentTemplateStorage();   // jdg 7/29/15
            if (templateDirectory.Substring(templateDirectory.Length - 1) != "\\") templateDirectory = templateDirectory + "\\";

            if (patientRecordHeader1.Visible)
            {
                patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
            }

            foreach (string f in Directory.GetFiles(baseOutputPath, "*_" + proband.unitnum + "_*", SearchOption.AllDirectories))
            {
                backgroundWorker1.ReportProgress(0, f);
            }
            foreach (string f in Directory.GetFiles(baseOutputPath, proband.unitnum + "_*", SearchOption.AllDirectories))
            {
                backgroundWorker1.ReportProgress(0, f);
            }
            //if (File.Exists(templateDirectory + "html\\pdfHeader.html"))
            //{
            //    backgroundWorker1.ReportProgress(0,templateDirectory + "html\\pdfHeader.html");
            //}
            //if (File.Exists(templateDirectory + "html\\pdfFooter.html"))
            //{
            //    backgroundWorker1.ReportProgress(0, templateDirectory + "html\\pdfFooter.html");
            //}
            if (baseOutputPath.Contains(proband.unitnum))
            {
                string root = (Directory.GetParent(baseOutputPath.TrimEnd(trimchars))).FullName;
                foreach (string f in Directory.GetFiles(root, "*_" + proband.unitnum + "_*", SearchOption.AllDirectories))
                {
                    backgroundWorker1.ReportProgress(0, f);
                }
                foreach (string f in Directory.GetFiles(root, proband.unitnum + "_*", SearchOption.AllDirectories))
                {
                    backgroundWorker1.ReportProgress(0, f);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                addFileToList((string)e.UserState);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                CommunicationEntry ce = (CommunicationEntry)objectListView1.SelectedObject;
                if (ce.Tag != null)
                {
                    if (ce.Tag is string)
                    {
                        FileInfo fi = new FileInfo((string)ce.Tag);
                        if (fi.Exists)
                        {
                            if (MessageBox.Show(
                                "Are you sure you want to delete this file?  This is permanent and cannot be undone.\n" + fi.Name.ToString(), "Delete Document", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            {
                                try
                                {
                                    fi.Delete();
                                    files.Remove(fi.FullName);
                                    objectListView1.RemoveObject(ce);
                                }
                                catch
                                {
                                    MessageBox.Show("An error occured while deleting this file, please make sure it is not open in another application and try again.");
                                }
                                
                            }
                        }
                    }
                }
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            PreparringDocumentLabel.Visible = true;
            winFormHtmlEditor1.Visible = false;
            winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.HTML_Edit;
            winFormHtmlEditor1.Toolbar1.Visible = true;
            winFormHtmlEditor1.Toolbar2.Visible = true;
            winFormHtmlEditor1.ToolbarFooter.Visible = true;
            EditButton.Visible = false;
            SaveButton.Visible = true;
            DiscardButton.Enabled = true;
            stateoverride = true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            stateoverride = false;

            if (winFormHtmlEditor1.Tag != null)
            {
                string name = (string)winFormHtmlEditor1.Tag;
                File.WriteAllText(name, winFormHtmlEditor1.DocumentHtml);
            }
            string s = winFormHtmlEditor1.DocumentHtml;
            winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.ReadOnly_Preview;
            winFormHtmlEditor1.DocumentHtml = s;
            winFormHtmlEditor1.Toolbar1.Visible = false;
            winFormHtmlEditor1.Toolbar2.Visible = false;
            winFormHtmlEditor1.ToolbarFooter.Visible = false;
            EditButton.Visible = true;
            SaveButton.Visible = false;
            DiscardButton.Enabled = false;

            Cursor = Cursors.Default;
        }
        private void Print_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DocumentTemplate.Print(winFormHtmlEditor1.DocumentHtml, pd.PrinterSettings.PrinterName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItem != null)
            {
                CommunicationEntry ce = (CommunicationEntry)objectListView1.SelectedObject;
                if (ce.Tag is string)
                {
                    string name = (string)ce.Tag;
                    OpenWithWord(name);
                }
            }
        }

        private void winFormHtmlEditor1_EditorModeChanged(object sender, EventArgs e)
        {
            if (stateoverride == false)
            {
                if (winFormHtmlEditor1.EditorMode == SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.HTML_Edit)
                {
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    winFormHtmlEditor1.EditorMode = SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.WYSIWYG_Design;
                }
                else if (winFormHtmlEditor1.EditorMode == SpiceLogic.HtmlEditorControl.Domain.BOs.EditorModes.WYSIWYG_Design)
                {
                    winFormHtmlEditor1.Visible = true;
                    PreparringDocumentLabel.Visible = false;
                    Cursor = Cursors.Default;
                }
            }
            else
            {

            }
        }

        private void winFormHtmlEditor1_HtmlChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void SaveAsPdfButton_Click(object sender, EventArgs e)
        {
            Save_Click(sender, e);

            Cursor = Cursors.WaitCursor;

            string templateDirectory = Configurator.GetDocumentTemplateStorage();   // override save location with config.xml
            
            if (templateDirectory.Substring(templateDirectory.Length - 1) != "\\")
                templateDirectory = templateDirectory + "\\";

            if (winFormHtmlEditor1.Tag != null)
            {
                string s = (string)winFormHtmlEditor1.Tag;
                string name = Path.Combine(Path.GetDirectoryName(s), Path.GetFileNameWithoutExtension(s));
                name += ".pdf";

                if (DocumentTemplate.ConvertToPdf(winFormHtmlEditor1.DocumentHtml, name) != 0)
                {
                    MessageBox.Show("An error has occured in saving the PDF file.  Check to make sure the file is not open in another process.");
                }
            }

            Cursor = Cursors.Default;
        }

        private void winFormHtmlEditor1_BtnImage_Click(object sender, EventArgs e)
        {
            /**/
            Image i = Clipboard.GetImage();
            if (i != null)
            {
                string data = GetBase64tImage(ScaleImage(i,625,625));
                winFormHtmlEditor1.Content.InsertHtml(data, false);
            }            
        }

        private string GetBase64tImage(Image image)
        {

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            String imageData = "data:image/png;base64,";

            imageData = imageData + base64Data;
            return
                "<img class=\"autoResizeImage\" src=\"" + imageData + "\">";
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            return newImage;
        }

        private void DiscardButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard all your changes since your last save?","Discard Changes",MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                string name = (string)winFormHtmlEditor1.Tag;
                htmlEditorPanel.Visible = true;
                NoPreviewPanel.Visible = false;
                FileNameLabel.Text = Path.GetFileName(name);
                string s = File.ReadAllText(name);
                winFormHtmlEditor1.DocumentHtml = s;
                Save_Click(sender, e);
            }
        }

        private void DefaultOpenButton_Click(object sender, EventArgs e)
        {
            string name = "";
            if (objectListView1.SelectedObject != null)
            {
                CommunicationEntry ce = (CommunicationEntry)objectListView1.SelectedObject;
                if (ce.Tag != null)
                {
                    if (ce.Tag is string)
                    {
                        name = (string)ce.Tag;
                    }
                }
            }


            if (string.IsNullOrEmpty(name) == false)
            {
                switch (Path.GetExtension(name).ToLower())
                {

                    case ".doc":
                    case ".docx":
                        OpenWithWord(name);
                        break;
                    case ".pdf":
                        OpenPDF(name);
                        break;
                    default:
                        break;

                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (winFormHtmlEditor1.StateQuery.IsImage())
            {

            }
            else
            {
                winFormHtmlEditor1.ToolbarItemOverrider.OnCopyButtonClicked(sender, e);
            }
        }
    }
}

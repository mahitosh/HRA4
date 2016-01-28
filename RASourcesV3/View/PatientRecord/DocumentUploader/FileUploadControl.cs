using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Diagnostics;
using RiskApps3.View.Common;
using RiskApps3.Utilities;
namespace RiskApps3.View.PatientRecord
{
    public partial class FileUploadControl : UserControl
    {
        private int directoryLoadCount = 0;

        private AddingFilesForm aff = new AddingFilesForm();

        private const int DATE_TAKEN_TAG_ID = 0x9003;

        public string viewType = "LargeIcon";
        public int relativeID = 1;
        public string path = "";
        public string patientName;
        public string mrn;
        public string comment = "";
        public List<string> otherDirs = new List<string>();
        private bool bIsResizing;
        private Point oldPoint;
        private Size oldSize;

        private DirectoryInfo outputPathBase;

        DirectoryInfo root = null;
        DirectoryInfo info = null;

        /// <summary>
        /// Original filenames of imported images
        /// </summary>
        //private List<String> dropContainer = new List<string>();

        /// <summary>
        /// Actual Images
        /// </summary>
        private ImageList imageList = new ImageList();
        private ImageList currentPhotoImageList = new ImageList();

        /// <summary>
        /// Destination filenames
        /// </summary>
        private List<String> filePaths = new List<String>();

        private const Int16 THUMB_SIZE = 108;

        private static List<String> VALID_EXTENSIONS =
            new List<string>(new string[] {
                ".jpg",
                ".jpeg",
                ".bmp",
                ".gif",
                ".tiff",
                ".png"
        });
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHELLEXECUTEINFO { 
            public int cbSize; 
            public uint fMask; 
            public IntPtr hwnd;     
            [MarshalAs(UnmanagedType.LPTStr)] 
            public string lpVerb;     
            [MarshalAs(UnmanagedType.LPTStr)] 
            public string lpFile;     
            [MarshalAs(UnmanagedType.LPTStr)] 
            public string lpParameters;     
            [MarshalAs(UnmanagedType.LPTStr)] 
            public string lpDirectory; 
            public int nShow; 
            public IntPtr hInstApp; 
            public IntPtr lpIDList;     
            [MarshalAs(UnmanagedType.LPTStr)] 
            public string lpClass; 
            public IntPtr hkeyClass; 
            public uint dwHotKey; 
            public IntPtr hIcon; 
            public IntPtr hProcess; 
        }  
        private const int SW_SHOW = 5; 
        private const uint SEE_MASK_INVOKEIDLIST = 12; 


        public FileUploadControl()
        {
            InitializeComponent();
            this.Resize += new EventHandler(FileUploadControl_Resize);
            if (this.listViewMain.Items.Count > 0)
            {
                this.hintTextLabel.Visible = false;
            }
        }

        void FileUploadControl_Resize(object sender, EventArgs e)
        {
            this.hintTextLabel.CenterHorizontally();
        }

        public void Setup()
        {
            listView1.Items.Clear();
            listViewMain.Items.Clear();

            if (viewType == "LargeIcon")
            {
                listViewMain.View = System.Windows.Forms.View.LargeIcon;
                listView1.View = System.Windows.Forms.View.LargeIcon;
            }
            else
            {
                listViewMain.View = System.Windows.Forms.View.Details;
                listView1.View = System.Windows.Forms.View.Details;
            }

            outputPathBase = new DirectoryInfo(path);
            if (!outputPathBase.Exists)
            {
                outputPathBase.Create();
            }
            
            imageList.ImageSize = new Size(THUMB_SIZE, THUMB_SIZE);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            currentPhotoImageList.ImageSize = new Size(THUMB_SIZE, THUMB_SIZE);
            currentPhotoImageList.ColorDepth = ColorDepth.Depth32Bit;

            listView1.LargeImageList = currentPhotoImageList;
            listViewMain.LargeImageList = imageList;

            progressBar1.Visible = true;
            directoryLoadCount++;
            if (directorySearchWorker.IsBusy == false)
                directorySearchWorker.RunWorkerAsync();

        }
        private void directorySearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (directoryLoadCount > 0)
            {
                List<string> allDirs = new List<string>(Directory.GetDirectories(this.outputPathBase.FullName, "*" + this.mrn + "*"));
                foreach (string hit in allDirs)
                {
                    directorySearchWorker.ReportProgress(0, hit);
                }
                directoryLoadCount--;
            }
        }

        private void directorySearchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState!=null)
            {
                string s = (string)e.UserState;

                root = new DirectoryInfo(s);


            }
        }
        private void directorySearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (root == null)
            {
                root = new DirectoryInfo(Path.Combine(this.outputPathBase.FullName, patientName + "_" + mrn));
                if (!root.Exists)
                {
                    root.Create();
                }
            }

            info = new DirectoryInfo(Path.Combine(root.FullName, "_" + relativeID.ToString()));
            if (!info.Exists)
            {
                info.Create();
            }

            fileSystemWatcher1.Path = info.FullName;
            linkLabel1.Text = info.FullName;
            
            InitListBox();

            progressBar1.Visible = false;

        }
        private void InitListBox()
        {

            if (currentFilesWorker.IsBusy == false)
            {
                //    listView1.Items.Clear();
                //    foreach (Image i in currentPhotoImageList.Images)
                //    {

                //        i.Dispose();
                //    }

                //    listView1.LargeImageList = null;
                //    currentPhotoImageList.Images.Clear();
                //    currentPhotoImageList = null;
                //    GC.Collect();

                foreach (FileInfo fi in OutputPath.GetFiles())
                {
                    try
                    {
                        if (fi.Extension.Contains("ini") == false && fi.Extension.ToLower().Contains("tmp") == false)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = fi.Name;
                            lvi.Tag = fi;
                            lvi.SubItems.Add(fi.Length.ToString());
                            lvi.SubItems.Add(fi.LastWriteTime.ToString());
                            listView1.Items.Add(lvi);
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteToLog(e.ToString());
                    }
                }
                foreach (string dir in otherDirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        try
                        {
                            if (fi.Extension.Contains("ini") == false && fi.Extension.ToLower().Contains("tmp") == false)
                            {
                                ListViewItem lvi = new ListViewItem();
                                lvi.Text = fi.Name;
                                lvi.Tag = fi;
                                lvi.SubItems.Add(fi.Length.ToString());
                                lvi.SubItems.Add(fi.LastWriteTime.ToString());
                                listView1.Items.Add(lvi);
                            }
                        }

                        catch (Exception e)
                        {
                            Logger.Instance.WriteToLog(e.ToString());
                        }
                    }
                }

                currentFilesWorker.RunWorkerAsync();
            }


        }


        private void currentFilesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<FileInfo> toSearch = new List<FileInfo>(OutputPath.GetFiles());
            foreach (string dir in otherDirs)
            {
                DirectoryInfo di = new DirectoryInfo(dir);
                foreach (FileInfo fi in di.GetFiles())
                {
                    toSearch.Add(fi);
                }
            }
            foreach (FileInfo fi in toSearch)
            {
                try
                {
                    if (currentFilesWorker.CancellationPending == false)
                    {
                        if (fi.Extension.Contains("ini") == false && fi.Extension.ToLower().Contains("tmp") == false)
                        {
                            if (viewType != "LargeIcon")
                            {
                                currentFilesWorker.ReportProgress(0, null);
                                break;
                            }
                            if (VALID_EXTENSIONS.Contains(fi.Extension.ToLower()))
                            {

                                Image img = Image.FromFile(fi.FullName);

                                int thumbWidth = THUMB_SIZE;
                                int thumbHeight = THUMB_SIZE;
                                double aspectRatio = (double)img.Width / (double)img.Height;

                                if (img.Height > img.Width)
                                {
                                    thumbWidth = (int)((double)thumbHeight * aspectRatio);
                                }
                                else
                                {
                                    thumbHeight = (int)((double)thumbWidth / aspectRatio);
                                }
                                Image thumb = new Bitmap(THUMB_SIZE, THUMB_SIZE);  //img.GetThumbnailImage(thumbWidth, thumbHeight, ThumbnailCallback, System.IntPtr.Zero);
                                Graphics g = Graphics.FromImage(thumb);
                                g.DrawImage(img, (THUMB_SIZE - thumbWidth) / 2, THUMB_SIZE - thumbHeight, thumbWidth, thumbHeight);
                                g.Dispose();
                                img.Dispose();
                                GC.Collect();
                                thumb.Tag = fi.Name;
                                currentFilesWorker.ReportProgress(0, thumb);
                            }
                            else
                            {
                                FileInfo fi2 = new FileInfo("temp" + fi.Extension);
                                if (fi2.Exists == false)
                                {
                                    fi2.Create();
                                }
                                Image img = System.Drawing.Icon.ExtractAssociatedIcon(fi2.FullName).ToBitmap();
                                Image thumb = img.GetThumbnailImage(THUMB_SIZE, THUMB_SIZE, ThumbnailCallback, System.IntPtr.Zero);
                                img.Dispose();

                                try
                                {
                                    fi2.Delete();
                                }
                                catch { }
                                GC.Collect();

                                thumb.Tag = fi.Name;
                                currentFilesWorker.ReportProgress(0, thumb);
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch { }
            }
        }

        private void currentFilesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null)
            {
                SetView(System.Windows.Forms.View.Details);
            }
            else
            {
                Image thumb = (Image)e.UserState;
                if (thumb.Tag != null)
                {
                    string n = (string)thumb.Tag;
                    currentPhotoImageList.Images.Add(n, thumb);

                    foreach (ListViewItem lvi in listView1.Items)
                    {
                        if (lvi.Text == n)
                        {
                            int imageindex = currentPhotoImageList.Images.IndexOfKey(n);
                            if (imageindex >= 0)
                            {
                                lvi.ImageIndex = imageindex;
                                currentPhotoImageList.Images[lvi.ImageIndex].Tag = thumb.Tag;
                            }
                        }
                    }
                }
                thumb.Dispose();
                listView1.Refresh();
            }

            //Console.Out.WriteLine("image list length = " + currentPhotoImageList.Images.Count.ToString());
        }

        private void currentFilesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }


        public DirectoryInfo OutputPath
        {
            get
            {
                return info;
            }
        }
        private void FileUploadControl_Load(object sender, EventArgs e)
        {
            
        }
        public void SetDate(DateTime dt)
        {
            //dateTimePicker1.Value = dt;
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (OutputPath.Exists)
            {

            }
            else
            {
                OutputPath.Create();
            }
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = OutputPath.FullName;
            try
            {
                prc.Start();
            }
            catch (Exception ex)
            {
                Logger.Instance.WriteToLog(
                    "Unable to open image upload directory for mrn: " +
                    mrn + ".  " + ex.ToString());

                MessageBox.Show("Unable to open directory: " + OutputPath.FullName +
                    ".  Consider contacting your system administrator.",
                    "Unable to open directory",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cmdRemove_Click(object sender, EventArgs e)
        {
            
            
            foreach (Image i in imageList.Images)
            {
                i.Dispose();

            }
            imageList.Images.Clear();
            this.hintTextLabel.Visible = true;
            imageList.Dispose();
            //imageList = null;
            GC.Collect();
            imageList = new ImageList();
            listViewMain.Items.Clear();
            listViewMain.LargeImageList = imageList;
            GC.Collect();
        }
        private void RenameTiles()
        {

        }
        private static string MakeValidFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidReStr = string.Format(@"[{0}]", invalidChars);
            return Regex.Replace(name, invalidReStr, "_");
        }
        private static int FileNumber(string filename)
        {
            try
            {
                int indexOf_ = filename.LastIndexOf("_") + 1;
                int indexOfDot = filename.LastIndexOf(".");

                string numberPart = filename.Substring(indexOf_, indexOfDot - indexOf_);
                int number = int.Parse(numberPart);
                return number;
            }
            catch (Exception)
            {
                throw new Exception("Some one has mangled the file names - this is ok since we no longer need to worry about overwriting those ones.");
            }
        }
        private int NextFileNumber()
        {
            List<int> ints = new List<int>();

            DirectoryInfo info = new DirectoryInfo(this.OutputPath.FullName);
            foreach (FileInfo f in info.GetFiles("*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    ints.Add(FileNumber(f.Name));
                }
                catch (Exception)
                {

                }
            }

            //get highest
            int temp = -1;
            foreach (int i in ints)
            {
                if (i > temp)
                {
                    temp = i;
                }
            }
            return temp + 1;
        }
        //private static string GetPictureDateTaken(string file)
        //{
        //    try
        //    {
        //        // include date time taken like this [DT:date format]
        //        using (Image img = Image.FromFile(file))
        //        {
        //            string dateTakenTag = Encoding.ASCII.GetString(img.GetPropertyItem(DATE_TAKEN_TAG_ID).Value); ;
        //            string[] parts = dateTakenTag.Split(':', ' ');
        //            int year = int.Parse(parts[0]);
        //            int month = int.Parse(parts[1]);
        //            int day = int.Parse(parts[2]);
        //            int hour = int.Parse(parts[3]);
        //            int minute = int.Parse(parts[4]);
        //            int second = int.Parse(parts[5]);

        //            img.Dispose();
        //            GC.Collect();
        //            return new DateTime(year, month, day, hour, minute, second).ToString();
        //        }
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        public void GetFilesToUpload(ref Dictionary<string, FileInfo> theList)
        {
            if (theList != null)
            {
                foreach (ListViewItem lvi in listViewMain.Items)
                {
                    if (lvi.Tag != null)
                    {
                        theList.Add(Path.Combine(OutputPath.FullName, lvi.Text), (FileInfo)lvi.Tag);
                    }
                }
            }
        }


        private void listViewMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
                e.Effect = DragDropEffects.All;
        }
        private static string ExtractExtension(string file)
        {
            string extension = new FileInfo(file).Extension.ToLower();
            return extension;
        }
        public bool ThumbnailCallback()
        {
            return true;
        }
        private static bool ValidFileTypes(string[] additions)
        {
            foreach (string file in additions)
            {
                string extension = ExtractExtension(file);
                if (!VALID_EXTENSIONS.Contains(extension))
                {
                    return false;
                }
            }
            return true;
        }
        private static List<String> Append(List<String> original, string[] additions)
        {
            List<string> temp = new List<string>(original.Count + additions.Length);
            temp.AddRange(original);

            foreach (string str in additions)
            {
                temp.Add(str);
            }
            return temp;
        }
        private void listViewMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] additions = (string[])e.Data.GetData(DataFormats.FileDrop);

            //aff.totalFileCount = additions.Length;

            foreach (string s in additions)
            {
                FileInfo fi = new FileInfo(s);
                ListViewItem lvi = new ListViewItem();
                string newfile = MakeValidFileName(this.mrn + "_" + this.patientName + "_" + fi.LastWriteTime.ToShortDateString());
                string item = newfile + "_" + (listViewMain.Items.Count + NextFileNumber()).ToString() + ExtractExtension(s);
                lvi.Text = item;
                lvi.Tag = fi;
                lvi.SubItems.Add(fi.Length.ToString());
                lvi.SubItems.Add(fi.LastWriteTime.ToString());
                listViewMain.Items.Add(lvi);
                this.hintTextLabel.Visible = false;
            }

            dragDropWorker.RunWorkerAsync(additions);
            
        }
        private void dragDropWorker_DoWork(object sender, DoWorkEventArgs e)
        {
                              
            //long before = GC.GetTotalMemory(true);   
            
            if (e.Argument != null)
            {
                string[] additions = (string[])e.Argument;

                foreach (string name in additions)
                {
                    if (dragDropWorker.CancellationPending == false)
                    {
                        FileInfo fi = new FileInfo(name);
                        if (viewType != "LargeIcon")
                        {
                            //if (MessageBox.Show("This is a large picture, do you want to skip thumbnail and save memory?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            //{
                            //    dragDropWorker.ReportProgress(0, null);
                            break;
                            //}
                        }
                        string extension = ExtractExtension(name);
                        if (VALID_EXTENSIONS.Contains(extension))
                        {

                            Image img = Image.FromFile(name);


                            int thumbWidth = THUMB_SIZE;
                            int thumbHeight = THUMB_SIZE;
                            double aspectRatio = (double)img.Width / (double)img.Height;

                            if (img.Height > img.Width)
                            {
                                thumbWidth = (int)((double)thumbHeight * aspectRatio);
                            }
                            else
                            {
                                thumbHeight = (int)((double)thumbWidth / aspectRatio);
                            }
                            Image thumb = new Bitmap(THUMB_SIZE, THUMB_SIZE);  //img.GetThumbnailImage(thumbWidth, thumbHeight, ThumbnailCallback, System.IntPtr.Zero);
                            Graphics g = Graphics.FromImage(thumb);
                            g.DrawImage(img, (THUMB_SIZE - thumbWidth) / 2, THUMB_SIZE - thumbHeight, thumbWidth, thumbHeight);
                            g.Dispose();
                            img.Dispose();
                            GC.Collect();
                            thumb.Tag = name;
                            dragDropWorker.ReportProgress(0, thumb);
                        }
                        else
                        {
                            FileInfo fi2 = new FileInfo("temp" + extension);
                            if (fi2.Exists == false)
                            {
                                fi2.Create();
                            }
                            Image img = System.Drawing.Icon.ExtractAssociatedIcon(fi2.FullName).ToBitmap();
                            Image thumb = img.GetThumbnailImage(THUMB_SIZE, THUMB_SIZE, ThumbnailCallback, System.IntPtr.Zero);
                            img.Dispose();

                            try
                            {
                                fi2.Delete();
                            }
                            catch { }
                            GC.Collect();
                            thumb.Tag = name;
                            dragDropWorker.ReportProgress(0, thumb);
                        }
                    }
                    
                }
            }
            //long after = GC.GetTotalMemory(true);
            //long delta = before - after;
        }
        public void CopyImageListEntry()
        {
            foreach (Image i in imageList.Images)
            {
                if (i.Tag != null)
                {

                }
            }

        }
        private void dragDropWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null)
            {
                SetView(System.Windows.Forms.View.Details);
            }
            else
            {
                Image thumb = (Image)e.UserState;
                if (thumb.Tag != null)
                {
                    string name = (string)thumb.Tag;
                    imageList.Images.Add(name, thumb);
                    thumb.Dispose();
                    foreach (ListViewItem lvi in listViewMain.Items)
                    {
                        if (lvi.Tag != null)
                        {
                            FileInfo fi = (FileInfo)lvi.Tag;
                            if (name == fi.FullName)
                            {
                                int index = imageList.Images.IndexOfKey(name);
                                lvi.ImageIndex = index;
                                //imageList.Images[index].Tag = name;
                                
                            }
                        }
                    }
                    listViewMain.Refresh();
                }
                
            }
        }

        private void dragDropWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FileUploadControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Location.Y >= Height - 10)
                {
                    bIsResizing = true;
                    oldPoint = e.Location;
                    oldSize = Size;
                }
            }
        }

        private void FileUploadControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.Y >= Height - 10)
            {
                Cursor = Cursors.SizeNS;
            }
            else
            {
                Cursor = Cursors.Default;
            }
            if (bIsResizing)
            {
                Height = oldSize.Height + (e.Location.Y - oldPoint.Y);
            }
        }

        private void FileUploadControl_MouseUp(object sender, MouseEventArgs e)
        {
                if (e.Button == MouseButtons.Left)
                {
                    bIsResizing = false;
                }
            
        }

        private void FileUploadControl_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            bIsResizing = false;
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            //if (!(e.Name.ToLower().EndsWith("tmp")))
            //    UpdateListBox();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag != null)
                {
                    FileInfo fi = (FileInfo)lvi.Tag;

                    System.Diagnostics.Process prc = new System.Diagnostics.Process();
                    prc.StartInfo.FileName = fi.FullName;
                    try
                    {
                        prc.Start();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(
                            "Unable to open image upload directory for mrn: " +
                            mrn + ".  " + ex.ToString());

                        MessageBox.Show("Unable to open directory: " + OutputPath.FullName +
                            ".  Consider contacting your system administrator.",
                            "Unable to open directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }



        internal void SetView(System.Windows.Forms.View view)
        {
            listViewMain.View = view;
            listView1.View = view;
            viewType = view.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listViewMain.View == System.Windows.Forms.View.Details)
            {
                listViewMain.View = System.Windows.Forms.View.LargeIcon;
                listView1.View = System.Windows.Forms.View.LargeIcon;
                viewType = "LargeIcon";
                GetNewThumbs();
            }
            else if (listViewMain.View == System.Windows.Forms.View.LargeIcon)
            {
                listViewMain.View = System.Windows.Forms.View.Details;
                listView1.View = System.Windows.Forms.View.Details;
                viewType = "Details";
            }
        }

        private void listViewMain_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.View == System.Windows.Forms.View.Details)
                listView1.View = System.Windows.Forms.View.LargeIcon;
            else if (listView1.View == System.Windows.Forms.View.LargeIcon)
                listView1.View = System.Windows.Forms.View.Details;
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            FileInfo fi = new FileInfo(e.FullPath);

            if (fi.Extension.Contains("ini") == false && fi.Extension.ToLower().Contains("tmp") == false)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = fi.Name;
                lvi.Tag = fi;
                lvi.SubItems.Add(fi.Length.ToString());
                lvi.SubItems.Add(fi.LastWriteTime.ToString());
                listView1.Items.Add(lvi);
            }
            GetNewThumbs();
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            ListViewItem toRemove = null;
            //MessageBox.Show(e.Name + " has just been deleted!");
            foreach (ListViewItem lvi in listView1.Items)
            {
                if (lvi.Text == e.Name)
                {
                    toRemove = lvi;
                }
            }

            if (toRemove != null)
                listView1.Items.Remove(toRemove);
        }

        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            
            //MessageBox.Show(e.OldName + " has just been renamed to " + e.Name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //List<string> folders = new List<string>();
            //folders.Add(info.FullName);
            //foreach (string s in otherDirs)
            //{
            //    folders.Add(s);
            //}
            //ClinicianPhotoUploader.PhotoEditor pe = new ClinicianPhotoUploader.PhotoEditor(folders);
            //pe.Show();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag != null)
                {
                    FileInfo fi = (FileInfo)lvi.Tag;

                    System.Diagnostics.Process prc = new System.Diagnostics.Process();
                    prc.StartInfo.FileName = fi.FullName;
                    try
                    {
                        prc.Start();
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(
                            "Unable to open image upload directory for mrn: " +
                            mrn + ".  " + ex.ToString());

                        MessageBox.Show("Unable to open directory: " + OutputPath.FullName +
                            ".  Consider contacting your system administrator.",
                            "Unable to open directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.SelectedItems.Count < 1)
                {
                    openFileToolStripMenuItem.Enabled = false;
                    deleteFileToolStripMenuItem.Enabled = false;
                    propertiesToolStripMenuItem.Enabled = false;
                }
                else
                {
                    openFileToolStripMenuItem.Enabled = true;
                    deleteFileToolStripMenuItem.Enabled = true;
                    propertiesToolStripMenuItem.Enabled = true;
                }
                contextMenuStrip1.Show(listView1.PointToScreen(e.Location));
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the selected file(s)?\nTHIS IS PERMANENT AND CANNOT BE UNDONE! ","",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (ListViewItem lvi in listView1.SelectedItems)
                {
                    if (lvi.Tag != null)
                    {
                        FileInfo fi = (FileInfo)lvi.Tag;
                        try
                        {
                            fi.Delete();

                        }
                        catch (Exception ex)
                        {
                            Logger.Instance.WriteToLog(ex.ToString());

                            MessageBox.Show("Unable to delete  file: " + fi.FullName,"Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        public static void ShowFileProperties(string Filename) 
        { 
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = SW_SHOW; 
            info.fMask = SEE_MASK_INVOKEIDLIST; 
            ShellExecuteEx(ref info); 
        }
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag != null)
                {
                    FileInfo fi = (FileInfo)lvi.Tag;
                    try
                    {
                        ShowFileProperties(fi.FullName);

                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(ex.ToString());
                    }
                }
            }
        }

        private void printFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag != null)
                {
                    FileInfo fi = (FileInfo)lvi.Tag;

                    try
                    {
                        Process p = new System.Diagnostics.Process();
                        ProcessStartInfo si = new ProcessStartInfo();
                        si.FileName = "print";
                        si.Arguments = fi.FullName;
                        si.WindowStyle = ProcessWindowStyle.Normal;
                        p.StartInfo = si;
                        p.Start();

                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(
                            "Unable to open image upload directory for mrn: " +
                            mrn + ".  " + ex.ToString());

                        MessageBox.Show("Unable to open directory: " + OutputPath.FullName +
                            ".  Consider contacting your system administrator.",
                            "Unable to open directory",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void emailFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                if (lvi.Tag != null)
                {
                    FileInfo fi = (FileInfo)lvi.Tag;


                    try
                    {
                        MapiMailMessage message = new MapiMailMessage("Send Secure Patient " + patientName + " " + mrn, "");
                        message.Files.Add(fi.FullName);
                        message.ShowDialog();

                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog(ex.ToString());
                    }
                }
            }

        }


        internal void ReleaseImageData()
        {
            currentFilesWorker.CancelAsync();

            //int count = 0;
            //while (currentFilesWorker.IsBusy)
            //{
            //    count++;
            //    Thread.Sleep(10);
            //    if (count > 1000)
            //    {
            //        break;
            //    }
            //}

            foreach (Image i in imageList.Images)
            {
                i.Dispose();

            }
            imageList.Dispose();

            
            foreach (Image i in currentPhotoImageList.Images)
            {
                i.Dispose();

            }
            currentPhotoImageList.Dispose();
 

            listView1.LargeImageList = null;
            listViewMain.LargeImageList = null;
            GC.Collect();


        }

        public void GetNewThumbs()
        {
            if (newFilesworker.IsBusy == false)
            {
                List<FileInfo> toSearch = new List<FileInfo>(OutputPath.GetFiles());
                foreach (ListViewItem lvi in listView1.Items)
                {
                    if (lvi.ImageIndex >= 0)
                    {

                    }
                    else
                    {
                        toSearch.Add((FileInfo)(lvi.Tag));
                    }
                }
                newFilesworker.RunWorkerAsync(toSearch);
            }
        }

        private void newFilesworker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<FileInfo> toSearch = (List<FileInfo>)(e.Argument);
            foreach (FileInfo fi in toSearch)
            {
                try
                {
                    if (newFilesworker.CancellationPending == false)
                    {
                        if (fi.Extension.Contains("ini") == false && fi.Extension.ToLower().Contains("tmp") == false)
                        {
                            if (viewType != "LargeIcon")
                            {
                                newFilesworker.ReportProgress(0, null);
                                break;
                            }
                            if (VALID_EXTENSIONS.Contains(fi.Extension.ToLower()))
                            {

                                Image img = Image.FromFile(fi.FullName);

                                int thumbWidth = THUMB_SIZE;
                                int thumbHeight = THUMB_SIZE;
                                double aspectRatio = (double)img.Width / (double)img.Height;

                                if (img.Height > img.Width)
                                {
                                    thumbWidth = (int)((double)thumbHeight * aspectRatio);
                                }
                                else
                                {
                                    thumbHeight = (int)((double)thumbWidth / aspectRatio);
                                }
                                Image thumb = new Bitmap(THUMB_SIZE, THUMB_SIZE);  //img.GetThumbnailImage(thumbWidth, thumbHeight, ThumbnailCallback, System.IntPtr.Zero);
                                Graphics g = Graphics.FromImage(thumb);
                                g.DrawImage(img, (THUMB_SIZE - thumbWidth) / 2, THUMB_SIZE - thumbHeight, thumbWidth, thumbHeight);
                                g.Dispose();
                                img.Dispose();
                                GC.Collect();
                                thumb.Tag = fi.Name;
                                newFilesworker.ReportProgress(0, thumb);
                            }
                            else
                            {
                                FileInfo fi2 = new FileInfo("temp" + fi.Extension);
                                if (fi2.Exists == false)
                                {
                                    fi2.Create();
                                }
                                Image img = System.Drawing.Icon.ExtractAssociatedIcon(fi2.FullName).ToBitmap();
                                Image thumb = img.GetThumbnailImage(THUMB_SIZE, THUMB_SIZE, ThumbnailCallback, System.IntPtr.Zero);
                                img.Dispose();

                                try
                                {
                                    fi2.Delete();
                                }
                                catch { }
                                GC.Collect();

                                thumb.Tag = fi.Name;
                                newFilesworker.ReportProgress(0, thumb);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                catch { }
            }
        }

        private void newFilesworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                Image thumb = (Image)e.UserState;
                if (thumb.Tag != null)
                {
                    string n = (string)thumb.Tag;
                    currentPhotoImageList.Images.Add(n, thumb);

                    foreach (ListViewItem lvi in listView1.Items)
                    {
                        if (lvi.Text == n)
                        {
                            int imageIndex = currentPhotoImageList.Images.IndexOfKey(n);
                            if (imageIndex >=0)
                            {
                                lvi.ImageIndex = imageIndex;
                                currentPhotoImageList.Images[lvi.ImageIndex].Tag = thumb.Tag;
                            }
                        }
                    }
                }
                thumb.Dispose();
                listView1.Refresh();
            }
        }

        private void newFilesworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void CopyFilesWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null)
            {
                long denom = 1;
                long num = 0;
                Dictionary<string, FileInfo> fiList = (Dictionary<string, FileInfo>)(e.Argument);
                foreach (FileInfo fi in fiList.Values)
                {
                    denom += fi.Length;
                }
                try
                {
                    foreach (string fileName in fiList.Keys)
                    {
                        FileInfo fi = fiList[fileName];
                        if (fi != null)
                        {
                            double percent = 100.00 * ((double)num / (double)denom);
                            CopyFilesWorker.ReportProgress((int)percent, fi.Name);

                            fi.CopyTo(fileName);

                            num += fi.Length;


                            //string dt = GetPictureDateTaken(fi.FullName);

                            //SqlCommand cmdProcedure = new SqlCommand("sp_insertCommentByMrn", (SqlConnection)DBUtils.getDbConnection());
                            //cmdProcedure.CommandType = CommandType.StoredProcedure;
                            //cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@unitnum"].Value = mrn;
                            //cmdProcedure.Parameters.Add("@provider", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@provider"].Value = surgeonName;
                            //cmdProcedure.Parameters.Add("@path", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@path"].Value = fi.FullName;
                            //cmdProcedure.Parameters.Add("@comment", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@comment"].Value = textBox1.Text;
                            //cmdProcedure.Parameters.Add("@dateTaken", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@dateTaken"].Value = fi.LastWriteTime.ToShortDateString();
                            //cmdProcedure.Parameters.Add("@procedure", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@procedure"].Value = comboBox1.Text;
                            //cmdProcedure.Parameters.Add("@size", SqlDbType.Int);
                            //cmdProcedure.Parameters["@size"].Value = fi.Length;
                            //cmdProcedure.Parameters.Add("@createdBy", SqlDbType.NVarChar);
                            //cmdProcedure.Parameters["@createdBy"].Value = User.getUserLogin();

                            //cmdProcedure.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ee)
                {
                    Logger.Instance.WriteToLog(ee.ToString());
                }

            }
        }
        private void CopyFilesWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                string f = (string)(e.UserState);
                aff.SetLabelText("Copying file: " + f);
                aff.SetProgressPercent(e.ProgressPercentage);
            }
        }

        private void CopyFilesWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

                cmdRemove_Click(sender, null);
                GetNewThumbs();

            aff.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            aff.Show();

            Dictionary<string, FileInfo> fiList = new Dictionary<string, FileInfo>();

            GetFilesToUpload(ref fiList);

            CopyFilesWorker.RunWorkerAsync(fiList);

        }

        public void Disable()
        {
            this.hintTextLabel.Text = "Select A Relative To Proceed";
            this.hintTextLabel.BackColor = System.Drawing.SystemColors.Control;
        }

        public void Enable()
        {
            this.hintTextLabel.Text = "Drag New Documents Here";
            this.hintTextLabel.BackColor = System.Drawing.SystemColors.Window;
        }
    }
}


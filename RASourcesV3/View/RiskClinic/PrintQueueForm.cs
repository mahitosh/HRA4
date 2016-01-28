using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using BrightIdeasSoftware;

using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using RiskApps3.View;

using LetterGenerator;


namespace RiskApps3
{
    public partial class PrintQueueForm : Form
    {
        private PrintQueueEntryList pqeList;
        private PrintQueueEntryList selectedList;

        private bool includePrinted = false;
        private HeaderFormatStyle headerStyle = new HeaderFormatStyle();

        public PrintQueueForm()
        {
            InitializeComponent();

            headerStyle.Normal.FrameColor = Color.Black;
            headerStyle.Normal.BackColor = Color.WhiteSmoke;
            headerStyle.Normal.ForeColor = Color.Black;

            headerStyle.Pressed.FrameColor = Color.DarkBlue;
            headerStyle.Pressed.BackColor = Color.LightBlue;
            headerStyle.Pressed.ForeColor = Color.DarkBlue;

            fastDataListView1.HeaderFormatStyle = headerStyle;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            refresh(includePrinted);
        }

        private void includePrintedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            includePrinted = includePrintedCheckBox.Checked;
            refresh(includePrinted);
        }

        private void refresh(bool include)
        {
            includePrinted = include;

            fastDataListView1.Visible = false;

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            while (populateBackgroundWorker.IsBusy == true)
                Application.DoEvents();

            populateBackgroundWorker.RunWorkerAsync();
        }
        private delegate void clearViewCallback();
        private void clearView()
        {
            if (this.InvokeRequired)
            {
                clearViewCallback cvc = new clearViewCallback(clearView);
                this.Invoke(cvc);
            }
            else
            {
                fastDataListView1.ClearObjects();
                fastDataListView1.Refresh();
            }
        }
        private void populateBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (populateBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            HashSet<int> added = new HashSet<int>();
            int count = 0;

            clearView();

            pqeList = new PrintQueueEntryList(includePrinted);
            pqeList.BackgroundListLoad();

            if (populateBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            if (pqeList != null)
            {
                pqeList.Sort((a, b) => a.unitnum.CompareTo(b.unitnum));

                foreach (PrintQueueEntry pqe in pqeList)
                {
                    if (populateBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    if (added.Contains(pqe.ID) == false)
                    {
                        double percent = 100 * count / (double)(pqeList.Count);

                        added.Add(pqe.ID);
                        populateBackgroundWorker.ReportProgress((int)percent, pqe);
                    }
                }
            }
        }

        private void populateBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (populateBackgroundWorker.CancellationPending)
            {
                return;
            }
            fastDataListView1.AddObject(e.UserState);
        }

        private void populateBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string s = (fastDataListView1.GetItemCount() == 1) ? " Document Listed" : " Documents Listed";
            lblItemCountLetters.Text = fastDataListView1.GetItemCount().ToString() + s;

            s = (fastDataListView1.SelectedObjects.Count == 1) ? " Document Selected" : " Documents Selected";
            lblItemCountSelected.Text = fastDataListView1.SelectedObjects.Count.ToString() + s;

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
            fastDataListView1.Visible = true;
        }

        private void PrintQueueForm_Load(object sender, EventArgs e)
        {
            refresh(includePrinted);
        }

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            string s = (fastDataListView1.SelectedObjects != null && fastDataListView1.SelectedObjects.Count == 1) ? " Document Selected" : " Documents Selected";
            int count = (fastDataListView1.SelectedObjects == null) ? 0 : fastDataListView1.SelectedObjects.Count;
            lblItemCountSelected.Text =  count.ToString() + s;
        }
        private void print(bool selected)
        {
            if (selected == false)
            {
                fastDataListView1.SelectAll();
            }
            getSelectedPrintQueueEntries();

            statusLabel.Text = "";
            statusLabel.Show();
            cancelButton.Show();
            progressBar.Show();

            while (printSelectedBackgroundWorker.IsBusy)
                Application.DoEvents();

            printSelectedBackgroundWorker.RunWorkerAsync();
        }
        private void closeButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void getSelectedPrintQueueEntries()
        {
            selectedList = new PrintQueueEntryList();

            if (fastDataListView1.SelectedObjects != null)
            {
                foreach (PrintQueueEntry p in fastDataListView1.SelectedObjects)
                {
                    selectedList.Add(p);
                }
            }
        }

        private void printSelectedBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (printSelectedBackgroundWorker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            float i = 0;
            float count = selectedList.Count;
            foreach (PrintQueueEntry pqe in selectedList)
            {
                if (printSelectedBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                int percent = (int)((++i * 100.0f) / count);

                int apptID = -1;
                int documentTemplateID = -1;
                int ID = -1;
                string unitnum = "";
                
                apptID = pqe.apptID;
                documentTemplateID = pqe.documentTemplateID;
                ID = pqe.ID;
                unitnum = pqe.unitnum;

                if (string.IsNullOrEmpty(unitnum) || apptID < 1 || ID < 1 || documentTemplateID < 1)
                {
                    string mrn = string.IsNullOrEmpty(unitnum) ? "" : unitnum;
                    Logger.Instance.WriteToLog("RiskApps3 PrintQueueForm: Cannot print document { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                    printSelectedBackgroundWorker.ReportProgress(percent,
                            "RiskApps3 PrintQueueForm: Cannot print document { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                    continue; 
                }
                bool printFailed = false;
                Model.Clinic.Letters.PrintUtils printUtils = new Model.Clinic.Letters.PrintUtils();

                ////////////////////////////////////////////////////////
                // documentType defaults to WORD in V3 Batch Printing //
                ////////////////////////////////////////////////////////
                if (string.IsNullOrEmpty(pqe.docType))
                {
                    pqe.docType = "WORD";
                }

                if (pqe.docType.ToUpper() == "HTML")
                {
                    if (printUtils.printHtmlDoc(apptID, unitnum, documentTemplateID) == null)
                    {
                        printFailed = true;
                        string mrn = string.IsNullOrEmpty(unitnum) ? "" : unitnum;
                        Logger.Instance.WriteToLog("RiskApps3 PrintQueueForm: Printing failed for { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                        printSelectedBackgroundWorker.ReportProgress(percent, 
                            "RiskApps3 PrintQueueForm: HTML printing failed for { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                    }
                }
                else if (pqe.docType.ToUpper() == "WORD")
                {
                    if (printUtils.printWordDoc(apptID, unitnum, documentTemplateID) == false)
                    {
                        printFailed = true;
                        string mrn = string.IsNullOrEmpty(unitnum) ? "" : unitnum;
                        Logger.Instance.WriteToLog("RiskApps3 PrintQueueForm: Printing failed for { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                        printSelectedBackgroundWorker.ReportProgress(percent, 
                            "RiskApps3 PrintQueueForm: Word printing failed for { unitnum=" + mrn + ", apptID=" + apptID.ToString() + ", documentTemplateID=" + documentTemplateID.ToString() + " }");
                    }
                }
                if (printFailed == false)
                {
                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("apptID", apptID);
                    pc.Add("documentTemplateID", documentTemplateID);
                    pc.Add("dateTime", DateTime.Now);
                    string sqlStr = "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(@apptID, @documentTemplateID, @dateTime, 'BatchPrint');";

                    BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);

                    pc.Clear();
                    pc.Add("ID", ID);
                    sqlStr = "UPDATE tblPrintQueue SET printed='" + DateTime.Now + "' WHERE ID = @ID;";
                    BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);

                    printSelectedBackgroundWorker.ReportProgress(percent, "Printed document: template " + documentTemplateID.ToString() + "\r\nFor appt " + apptID.ToString() + " (MRN: " + unitnum + ")");
                }
                else
                {
                    printSelectedBackgroundWorker.ReportProgress(percent, "Failed to print document: template " + documentTemplateID.ToString() + "\r\nFor appt " + apptID.ToString() + " (MRN: " + unitnum + ")");
                }
                //Thread.Sleep(1000);
            }
        }

        private void printSelectedBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            statusLabel.Text = (string)e.UserState;

            if (e.ProgressPercentage > 0)
            {
                progressBar.Value = e.ProgressPercentage;
            }
            refresh(includePrinted);
        }

        private void printSelectedBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cancelButton.Hide();
            progressBar.Hide();
            statusLabel.Hide();
            statusLabel.Text = "";
        }

        private void btnPrintSelectedLetters_Click(object sender, EventArgs e)
        {
            print(true);
        }

        private void btnPrintLetters_Click(object sender, EventArgs e)
        {
            print(false);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (populateBackgroundWorker.IsBusy)
            {
                populateBackgroundWorker.CancelAsync();
            }
            if (printSelectedBackgroundWorker.IsBusy)
            {
                printSelectedBackgroundWorker.CancelAsync();
            }
        }
    }
}

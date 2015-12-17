using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Threading;

using BrightIdeasSoftware;

using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using RiskApps3.View;

namespace RiskApps3.View.Admin
{
    public partial class AutomationQueueForm : Form
    {
        private AutomationQueueEntryList aqeList;
        int timerTickInterval = 60000;

        public AutomationQueueForm()
        {
            InitializeComponent();
            timer1.Interval = timerTickInterval;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            refresh();
        }
        private void refresh()
        {
            fastDataListView1.Visible = false;

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            while (backgroundWorker1.IsBusy == true)
                Application.DoEvents();

            backgroundWorker1.RunWorkerAsync();
        }
        private delegate void clearQueueViewCallback();
        private void clearQueueView()
        {
            if (this.InvokeRequired)
            {
                clearQueueViewCallback cqvc = new clearQueueViewCallback(clearQueueView);
                this.Invoke(cqvc);
            }
            else
            {
                fastDataListView1.ClearObjects();
                fastDataListView1.Refresh();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            HashSet<int> added = new HashSet<int>();
            int count = 0;

            clearQueueView();
            
            aqeList = new AutomationQueueEntryList();
            aqeList.BackgroundListLoad();

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            if (aqeList != null)
            {
                aqeList.Sort(delegate(HraObject a, HraObject b)
                {
                    return ((AutomationQueueEntry)a).unitnum.CompareTo(((AutomationQueueEntry)b).unitnum);
                });
            }
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            foreach (AutomationQueueEntry aqe in aqeList)
            {
                if (added.Contains(aqe.apptid) == false)
                {
                    double percent = 100 * count / (double)(aqeList.Count);
                    
                    added.Add(aqe.apptid);
                    backgroundWorker1.ReportProgress((int)percent, aqe);
                }
            }
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (backgroundWorker1.CancellationPending)
            {
                return;
            }
            fastDataListView1.AddObject(e.UserState);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int count = aqeList.Count(); // (fastDataListView1.SelectedObjects == null) ? 0 : fastDataListView1.SelectedObjects.Count;
            numberLabel.Text = count.ToString();

            fastDataListView1.Visible = true;

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        private void AutomationQueueForm_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            int count = aqeList.Count(); //(fastDataListView1.SelectedObjects == null) ? 0 : fastDataListView1.SelectedObjects.Count;
            numberLabel.Text = count.ToString();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            refresh();
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

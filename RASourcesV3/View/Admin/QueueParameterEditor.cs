using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;

namespace RiskApps3.View.Admin
{
    public partial class QueueParameterEditor : Form
    {
        public QueueParameterEditor()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            HashSet<int> added = new HashSet<int>();
            double count = 0;

            QueueParameterList parameterList = new QueueParameterList();
            parameterList.BackgroundListLoad();

            if (parameterList != null)
            {
                parameterList.Sort((a, b) => a.ID.CompareTo(b.ID));

                foreach (QueueParameter o in parameterList)
                {
                    count++;
                    if (added.Contains(o.ID) == false)
                    {
                        added.Add(o.ID);
                        double percent = 100 * count / (double)(parameterList.Count);
                        QueueParameterRow asr = new QueueParameterRow(o);
                        backgroundWorker1.ReportProgress((int)percent, asr);
                        System.Threading.Thread.Sleep(25);
                    }
                }
            }
            e.Result = count;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            QueueParameterRow qpr = (QueueParameterRow)e.UserState;
            flowLayoutPanel1.Controls.Add(qpr);
            loadCountLabel.Text = flowLayoutPanel1.Controls.Count.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            loadCountLabel.Visible = false;
            flowLayoutPanel1.Focus();
        }

        private void QueueParameterEditor_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            loadCountLabel.Visible = true;
            backgroundWorker1.RunWorkerAsync();
            Application.DoEvents();
        }
    }
}

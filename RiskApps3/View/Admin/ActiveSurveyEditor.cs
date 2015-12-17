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
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;

namespace RiskApps3.View.Admin
{
    public partial class ActiveSurveyEditor : Form
    {
        public ActiveSurveyEditor()
        {
            InitializeComponent();
        }

        private void ActiveSurveyEditor_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            loadCountLabel.Visible = true;
            backgroundWorker1.RunWorkerAsync();
            Application.DoEvents();
            //flowLayoutPanel1.Focus();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            HashSet<int> added = new HashSet<int>();
            double count = 0;

            ActiveSurveyList surveyList = new ActiveSurveyList();
            surveyList.BackgroundListLoad();

            if (surveyList != null)
            {
                surveyList.Sort(delegate(HraObject a, HraObject b)
                {
                    return ((ActiveSurvey)a).surveyID.CompareTo(((ActiveSurvey)b).surveyID);
                });
                foreach (ActiveSurvey o in surveyList)
                {
                    count++;
                    if (added.Contains(o.surveyID) == false)
                    {
                        added.Add(o.surveyID);
                        double percent = 100 * count / (double)(surveyList.Count);
                        ActiveSurveyRow asr = new ActiveSurveyRow(o);
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
            ActiveSurveyRow asr = (ActiveSurveyRow)e.UserState;
            flowLayoutPanel1.Controls.Add(asr);
            loadCountLabel.Text = flowLayoutPanel1.Controls.Count.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            loadCountLabel.Visible = false;
            flowLayoutPanel1.Focus();
        }


    }
}

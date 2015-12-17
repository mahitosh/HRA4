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

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeSymbolEditor : Form
    {
        public PedigreeSymbolEditor()
        {
            InitializeComponent();
        }

        private void PedigreeSymbolEditor_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync(comboBox1.Text);
            flowLayoutPanel1.Focus();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string grouping = (string)e.Argument;
            HashSet<string> added = new HashSet<string>();
            double count = 0;

            List<HraObject> dxList;
            if ((string)(e.Argument) == "All")
                dxList = SessionManager.Instance.MetaData.Diseases.ToList();
            else
                dxList = SessionManager.Instance.MetaData.Diseases.Where(t => ((DiseaseObject)t).groupingName == grouping).ToList();

            if (dxList != null)
            {
                dxList.Sort(delegate(HraObject a, HraObject b)
                {
                    return ((DiseaseObject)a).diseaseName.CompareTo(((DiseaseObject)b).diseaseName);
                });
                foreach (DiseaseObject o in dxList)
                {
                    count++;
                    if (added.Contains(o.diseaseName) == false)
                    {
                        added.Add(o.diseaseName);
                        double percent = 100 * count / (double)(dxList.Count);
                        PedigreeSymbolRow psr = new PedigreeSymbolRow(o);
                        backgroundWorker1.ReportProgress((int)percent, psr);
                        System.Threading.Thread.Sleep(75);
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            PedigreeSymbolRow psr = (PedigreeSymbolRow)e.UserState; 
            flowLayoutPanel1.Controls.Add(psr);
            label1.Text = flowLayoutPanel1.Controls.Count.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Visible = false;
            comboBox1.Enabled = true;
            flowLayoutPanel1.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            comboBox1.Enabled = false;
            progressBar1.Visible = true;
            backgroundWorker1.RunWorkerAsync(comboBox1.Text);
            flowLayoutPanel1.Focus();

        }
    }
}

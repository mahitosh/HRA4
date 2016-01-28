using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic.Reports;
using dotnetCHARTING.WinForms;

namespace RiskApps3.View.Reporting
{
    public partial class EthnicityFrequencyControl : UserControl
    {
        public EthnicityFrequency ethnicities = new EthnicityFrequency();

        public string title = "Ethnicity Distribution";
        public int total = 0;

        public EthnicityFrequencyControl()
        {
            
            InitializeComponent();
            TitleLabel.Text = title;
            //chart1.LegendBox.Visible = false;
            chart1.DefaultElement.SmartLabel.Text = "%YValue, %Name";
            chart1.Palette = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            label1.Text = "";
        }

        private void EthnicityFrequencyControl_Load(object sender, EventArgs e)
        {
            ethnicities.AddHandlersWithLoad(null, finished, null);
        }

        private void finished(Model.HraListLoadedEventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SeriesCollection sc = new SeriesCollection();
            
            foreach (object o in ethnicities)
            {
                if (o is EthnicityFrequency.EthnicityStat)
                {
                    Series theSeries = new Series();
                    EthnicityFrequency.EthnicityStat stat = (EthnicityFrequency.EthnicityStat)o;
                    Element elem = new Element();
                    theSeries.Name = stat.racialBackground;
                    elem.Name = stat.racialBackground;
                    elem.YValue = stat.frequency;
                    total += stat.frequency;
                    theSeries.AddElements(elem);
                    sc.Add(theSeries);
                }
            }
            e.Result = sc;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SeriesCollection s = (SeriesCollection)e.Result;
            chart1.SeriesCollection.Add(s);
            chart1.RefreshChart();
            label1.Text = "There were " + total.ToString("#,###,###") + " unique patients.";
        }

    }
}

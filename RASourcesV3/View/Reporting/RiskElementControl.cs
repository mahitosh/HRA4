using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic.Dashboard;
using dotnetCHARTING.WinForms;
using RiskApps3.View.Reporting;

namespace RiskApps3.View.Reporting
{
    public partial class RiskElementControl : UserControl
    {
        char[] trimChars = { ' ', '-' };

        public enum Mode { Incidence, Prevelance };

        public BreastImagingModelElement data;
        
        public Mode current_mode = Mode.Incidence;

        public string numeratorLegend = "At Risk";
        public string denominatorLegend = "Not At Risk";
        public string title = "";

        Color case_color = Color.Red;

        RiskApps3.View.BreastImaging.BreastImagingDashboard.FollowupCallbackType FollowupCallback;

        public RiskElementControl()
        {
            data = new BreastImagingModelElement();
            InitializeComponent();
            chart1.LegendBox.Visible = false;
        }

        private void BreastImagingDashboardElement_Load(object sender, EventArgs e)
        {
            data.AddHandlersWithLoad(null, dataLoaded, null);
        }

        /**************************************************************************************************/
        private void dataLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (data.denominator == 0)
            {
                ValueLabel.Text = "No Data Available";
                button1.Visible = false;
                button2.Visible = false;
                chart1.Visible = false;

            }
            else
            {
                if (current_mode == Mode.Incidence)
                {
                    ValueLabel.Text = data.incidence.ToString("#,###,###") + " / " + data.denominator.ToString("#,###,###") + GetPercent(data.incidence, data.denominator);
                }
                else
                {
                    ValueLabel.Text = data.prevelance.ToString("#,###,###") + " / " + data.denominator.ToString("#,###,###") + GetPercent(data.prevelance, data.denominator);
                }

                backgroundWorker1.RunWorkerAsync(data);
            }
        }

        public void SetTitle(string p)
        {
            TitleLabel.Text = p;
            title = p;
        }
        public void ShowFollow(bool b)
        {
            button2.Visible = b;
        }
        private string GetPercent(int a, int b)
        {
            string retval = "";

            double d = 100 * ((double)a / (double)b);

            retval = " (" + Math.Round(d, 1).ToString() + "%)";

            return retval;
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BreastImagingModelElement dataparam = (BreastImagingModelElement)e.Argument;

            SeriesCollection sc = new SeriesCollection();
            Series n = new Series();
            n.Name = numeratorLegend;

            Series d = new Series();
            d.Name = denominatorLegend;

            int numerator = 0;
            if (current_mode == Mode.Incidence)
            {
                numerator = data.incidence;
            }
            else
            {
                numerator = data.prevelance;
            }


            Element num = new Element();
            num.Name = numerator.ToString();
            num.YValue = numerator;
            n.Elements.Add(num);

            Element den = new Element();
            den.Name = (dataparam.denominator - numerator).ToString();
            den.YValue = (dataparam.denominator - numerator);
            d.Elements.Add(den);


            n.DefaultElement.Color = case_color;
            sc.Add(n);

            d.DefaultElement.Color = Color.White;
            sc.Add(d);

            chart1.BackColor = SystemColors.Control;
            chart1.Background.Color = SystemColors.Control;
            e.Result = sc;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SeriesCollection sc = (SeriesCollection)e.Result;
            chart1.SeriesCollection.Add(sc);
            chart1.RefreshChart();
            button1.Enabled = true;
            button2.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (FollowupCallback != null)
                FollowupCallback.Invoke(data.type);
        }

        public void Register(RiskApps3.View.BreastImaging.BreastImagingDashboard.FollowupCallbackType FollowupDelegate)
        {
            FollowupCallback = FollowupDelegate;
        }

        protected void button1_Click_1(object sender, EventArgs e)
        {
            ReportInfoMessage bidm = new ReportInfoMessage();
            string preamble = "";
            string incidence_modifier = "";
            int numerator = 0;
            if (current_mode == Mode.Incidence)
            {
                numerator = data.incidence;
                incidence_modifier = " for the first time:";

            }
            else
            {
                numerator = data.prevelance;
                incidence_modifier = ":";
            }


            preamble = "There were " + numerator.ToString() + " non test patients (MRN not like 999*) who had an appointment between " +
                        data.startTime.ToShortDateString() + " and " + data.endTime.ToShortDateString() +
                        " (inclusively), who met AT LEAST ONE of the following conditions" + incidence_modifier + Environment.NewLine;

                

          
            string formatted = "\t- " + data.info.Replace(", ", Environment.NewLine + "\t- ").Trim(trimChars);
            bidm.SetText(preamble + formatted);
            bidm.ShowDialog();
        }



        public void SetColor(Color color)
        {
            case_color = color;
            button2.BackColor = color;
        }

        public void SetReportMode(bool report)
        {
            if (report)
            {
                button2.Text = "";
                button1.Visible = false;

            }
        }
    }
}

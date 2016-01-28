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
using HtmlAgilityPack;
using RiskApps3.Utilities;

namespace RiskApps3.View.Reporting
{
    public partial class BreastLifetimeRiskElement : UserControl
    {
        public BreastImagingModelElement highrisk = new BreastImagingModelElement();
        public BreastImagingModelElement moderaterisk = new BreastImagingModelElement();

        int high_risk_prevelance = 0;
        int high_risk_incidence = 0;
        //int high_risk_seenInRC = 0;
        
        int moderate_risk_prevelance = 0;
        int moderate_risk_incidence = 0;
        //int moderate_risk_seenInRC = 0;

        public BreastLifetimeRiskElement(DateTime StartTime, DateTime EndTime, int clinicId)
        {
            highrisk.type = "BreastCancer";
            highrisk.clinicId = clinicId;
            highrisk.output = 1;
            highrisk.startTime = StartTime;
            highrisk.endTime = EndTime;

            moderaterisk.type = "ModerateBreastCancer"; 
            moderaterisk.clinicId = clinicId;
            moderaterisk.output = 1;
            moderaterisk.startTime = StartTime;
            moderaterisk.endTime = EndTime;

            InitializeComponent();

            chart1.LegendBox.Visible = false;
            chart2.LegendBox.Visible = false;

            Color[] p = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            chart1.Palette = p;
            chart2.Palette = p;
        }

        private void BIDashboardLifetimeRisk_Load(object sender, EventArgs e)
        {

        }

        internal void GetData()
        {
            highrisk.BackgroundLoadWork();
            moderaterisk.BackgroundLoadWork();
            LoadChart();
        }

        private void LoadChart()
        {
            Series high = new Series();
            high.Name = "High Lifetime Breast Cancer Risk";

            high.DefaultElement.Color = Color.Red;

            Element high_prev_elem = new Element();
            high_prev_elem.Name = "All High Risk";
            high_prev_elem.YValue = highrisk.prevelance;
            high_risk_prevelance = highrisk.prevelance;
            high.Elements.Add(high_prev_elem);

            Element high_incidence_elem = new Element();
            high_incidence_elem.YValue = highrisk.incidence;
            high_incidence_elem.Name = "New High Risk";
            high_risk_incidence = highrisk.incidence;
            high.Elements.Add(high_incidence_elem);

            //Element high_rc_elem = new Element();
            //high_rc_elem.YValue = highrisk.seenInRC;
            //high_rc_elem.Name = "Seen In Risk Clinic";
            //high_risk_seenInRC = highrisk.seenInRC;
            //high.Elements.Add(high_rc_elem);

            chart1.SeriesCollection.Add(high);


            Series moderate = new Series();
            moderate.Name = "Moderate Lifetime Breast Cancer Risk";

            moderate.DefaultElement.Color = Color.Orange;

            Element moderate_prev_elem = new Element();
            moderate_prev_elem.Name = "All Moderate Risk";
            moderate_prev_elem.YValue = moderaterisk.prevelance;
            moderate_risk_prevelance = moderaterisk.prevelance;
            moderate.Elements.Add(moderate_prev_elem);

            Element moderate_incidence_elem = new Element();
            moderate_incidence_elem.Name = "New Moderate Risk";
            moderate_incidence_elem.YValue = moderaterisk.incidence;
            moderate_risk_incidence = moderaterisk.incidence;
            moderate.Elements.Add(moderate_incidence_elem);

            //Element moderate_rc_elem = new Element();
            //moderate_rc_elem.YValue = moderaterisk.seenInRC;
            //moderate_rc_elem.Name = "Seen In Risk Clinic";
            //moderate_risk_seenInRC = moderaterisk.seenInRC;
            //moderate.Elements.Add(moderate_rc_elem);

            chart2.SeriesCollection.Add(moderate);
        }

        internal void ToHTML(HtmlAgilityPack.HtmlNode node)
        {
            HtmlNode table = node.OwnerDocument.CreateElement("table");

            UIUtils.AddTwoColumnRowToTable(table,
                                           GetTable1Node(node),
                                           UIUtils.GetImageHtmlFromChart(chart1, node));

            UIUtils.AddTwoColumnRowToTable(table,
                                           GetTable2Node(node),
                                           UIUtils.GetImageHtmlFromChart(chart2, node));

            node.ChildNodes.Add(table);
        }


        private HtmlNode GetTable1Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);

            UIUtils.AddColumnRowToTable(table,
                                           "Total Number of Patients",
                                            highrisk.denominator.ToString("#,###,###"), "");

            int percent = (int)Math.Round(100 * (double)high_risk_prevelance / (double)highrisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "All High Lifetime Breast Cancer Risk Patients Seen",
                                            high_risk_prevelance.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)high_risk_incidence / (double)highrisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "New High Lifetime Risk Breast Cancer",
                                            high_risk_incidence.ToString("#,###,###"),
                                            percent.ToString() + "%");

            return table;
        }

        private HtmlNode GetTable2Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);

            UIUtils.AddColumnRowToTable(table,
                               "Total Number of Patients",
                                moderaterisk.denominator.ToString("#,###,###"), "");

            int percent = (int)Math.Round(100 * (double)moderate_risk_prevelance / (double)moderaterisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "All Moderate Lifetime Risk Breast Cancer",
                                            moderate_risk_prevelance.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)moderate_risk_incidence / (double)moderaterisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "New Moderate Lifetime Risk Breast Cancer",
                                            moderate_risk_incidence.ToString("#,###,###"),
                                            percent.ToString() + "%");

            return table;
        }


    }
}




        //private string GetPercent(int a,int b)
        //{
        //    string retval = "";

        //    double d = 100 * ((double)a / (double)b);

        //    retval = " (" + Math.Round(d,1).ToString() + "%)";

        //    return retval;
        //}

            //Element modElem = new Element();
            //modElem.Name = moderaterisk.incidence.ToString();
            //modElem.YValue = moderaterisk.incidence;
            //moderate_incidence.Elements.Add(modElem);

            //int low_incidence_cuont = highrisk.denominator - (highrisk.incidence + moderaterisk.incidence);
            //Element lowElem = new Element();
            //lowElem.Name = low_incidence_cuont.ToString();
            //lowElem.YValue = low_incidence_cuont;
            //low_incidence.Elements.Add(lowElem);

            //Element highElem_prev = new Element();
            //highElem_prev.Name = highrisk.prevelance.ToString();
            //highElem_prev.YValue = highrisk.prevelance;
            //high.Elements.Add(highElem_prev);

            //Element modElem = new Element();
            //modElem.Name = moderaterisk.prevelance.ToString();
            //modElem.YValue = moderaterisk.prevelance;
            //moderate.Elements.Add(modElem);

            //int denom = highrisk.denominator - (highrisk.prevelance + moderaterisk.prevelance);
            //Element lowElem = new Element();
            //lowElem.Name = denom.ToString();
            //lowElem.YValue = denom;
            //low.Elements.Add(lowElem);


            //high.DefaultElement.Color = Color.Red;
            //sc.Add(high);

            //moderate.DefaultElement.Color = Color.Orange;
            //sc.Add(moderate);

            //low.DefaultElement.Color = Color.White;
            //sc.Add(low);

            //chart1.BackColor = SystemColors.Control;
            //chart1.Background.Color = SystemColors.Control;

            //chart1.SeriesCollection.Add(r.collection);
            //chart1.RefreshChart();





        ///**************************************************************************************************/
        //private void dataLoaded(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    if (highrisk.hra_state == Model.HraObject.States.Ready &&
        //        moderaterisk.hra_state == Model.HraObject.States.Ready)
        //    {
        //        backgroundWorker1.RunWorkerAsync();
        //    }
        //}

        //public void SetDetails(DateTime start, DateTime end, int clinicID, RiskApps3.View.Reporting.RiskElementControl.Mode mode)
        //{
        //    highrisk.startTime = start;
        //    highrisk.endTime = end;
        //    moderaterisk.startTime = start;
        //    moderaterisk.endTime = end;
        //}
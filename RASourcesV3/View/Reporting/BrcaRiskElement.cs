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
    public partial class BrcaRiskElement : UserControl
    {
        public BreastImagingModelElement highrisk = new BreastImagingModelElement();

        int high_risk_prevelance = 0;
        int high_risk_incidence = 0;
        int high_risk_seenInRC = 0;

        public BrcaRiskElement(DateTime StartTime, DateTime EndTime, int clinicId)
        {
            highrisk.type = "BRCA";
            highrisk.clinicId = clinicId;
            highrisk.output = 1;
            highrisk.startTime = StartTime;
            highrisk.endTime = EndTime;

            InitializeComponent();

            chart1.LegendBox.Visible = false;

            Color[] p = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            chart1.Palette = p;

        }

        internal void GetData()
        {
            highrisk.BackgroundLoadWork();
            LoadChart();
        }

        private void LoadChart()
        {
            Series high = new Series();
            high.Name = "High BRCA Mutation Risk";

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

            Element high_rc_elem = new Element();
            high_rc_elem.YValue = highrisk.seenInRC;
            high_rc_elem.Name = "Seen In Risk Clinic";
            high_risk_seenInRC = highrisk.seenInRC;
            high.Elements.Add(high_rc_elem);

            chart1.SeriesCollection.Add(high);

        }
        internal void ToHTML(HtmlAgilityPack.HtmlNode node)
        {
            HtmlNode table = node.OwnerDocument.CreateElement("table");

            UIUtils.AddTwoColumnRowToTable(table,
                                           GetTable1Node(node),
                                           UIUtils.GetImageHtmlFromChart(chart1, node));

            node.ChildNodes.Add(table);
        }


        private HtmlNode GetTable1Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);

            UIUtils.AddColumnRowToTable(table,
                                           "Total Number of Patients",
                                            highrisk.denominator.ToString("#,###,###"),"");

            int percent = (int)Math.Round(100 * (double)high_risk_prevelance / (double)highrisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "All High Risk BRCA Patients",
                                            high_risk_incidence.ToString("#,###,###"), 
                                            percent.ToString() + "%");
            
            percent = (int)Math.Round(100 * (double)high_risk_prevelance / (double)highrisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "New High Risk BRCA",
                                            high_risk_incidence.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)high_risk_seenInRC / (double)highrisk.denominator, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "All High Risk BRCA Seen In Cancer Genetics",
                                            high_risk_seenInRC.ToString("#,###,###"),
                                            percent.ToString() + "%");
            return table;
        }
    }
}

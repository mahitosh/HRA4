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
using HtmlAgilityPack;
using RiskApps3.Utilities;

namespace RiskApps3.View.Reporting
{
    public partial class BrOvCancerFrequencyControl : UserControl
    {
        public BrOvCancerFrequency frequency = new BrOvCancerFrequency();

        int total_bc = 0;
        int uni_bc = 0;
        int bil_bc = 0;
        int oc = 0;
        int oc_uni = 0;
        int oc_bil = 0;

        int hr_bc = 0;
        int hr_oc = 0;
        int hr_both = 0;

        public BrOvCancerFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();
            frequency.StartTime = StartTime;
            frequency.EndTime = EndTime;
            frequency.clinicID = clinicId;
            frequency.type = type;

            chart1.Width = 500;
            chart2.Width = 500;
            chart1.LegendBox.Visible = false;
            chart2.LegendBox.Visible = false;

            chart2.Title = "Patients at High Risk Because of Cancer Dx";
            chart2.TitleBox.Background.Color = Color.White;

            //Color[] p = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            //chart1.Palette = p;
            //chart2.Palette = p;
        }

        private void BrOvCancerFrequencyControl_Load(object sender, EventArgs e)
        {
        }

        internal void GetData()
        {
            frequency.BackgroundListLoad();
            LoadChart();
        }

        private void LoadChart()
        {
            SeriesCollection sc = new SeriesCollection();

            Series cancerSeries = new Series("Breast Cancer");
            cancerSeries.DefaultElement.Color = Color.LightBlue;

            Element unilateral_elem = new Element();
            unilateral_elem.Name = "Unilateral Breast Cancer";
            unilateral_elem.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcCount == 1 &&
                                                           ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcCount == 0).Count();
            uni_bc = (int)unilateral_elem.YValue;
            total_bc += uni_bc;
            cancerSeries.AddElements(unilateral_elem);

            Element bilateral_elem = new Element();
            bilateral_elem.Name = "Bilateral Breast Cancer";
            bilateral_elem.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcCount == 2 &&
                                                          ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcCount == 0).Count();
            bil_bc = (int)bilateral_elem.YValue;
            total_bc += bil_bc;
            cancerSeries.AddElements(bilateral_elem);

            Element ovarian_elem = new Element();
            ovarian_elem.Name = "Ovarian Cancers";
            ovarian_elem.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcCount == 1 &&
                                                        ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcCount == 0).Count();
            oc = (int)ovarian_elem.YValue;
            cancerSeries.AddElements(ovarian_elem);

            Element ovarian_unilateral_elem = new Element();
            ovarian_unilateral_elem.Name = "Ovarian and Unilateral Breast Cancer";
            ovarian_unilateral_elem.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcCount == 1 &&
                                                                   ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcCount == 1).Count();
            oc_uni = (int)ovarian_unilateral_elem.YValue;
            total_bc += oc_uni;
            cancerSeries.AddElements(ovarian_unilateral_elem);

            Element ovarian_bilateral_elem = new Element();
            ovarian_bilateral_elem.Name = "Ovarian and Bilateral Breast Cancer";
            ovarian_bilateral_elem.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcCount == 1 &&
                                                                   ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcCount == 2).Count();
            oc_bil = (int)ovarian_bilateral_elem.YValue;
            total_bc += oc_bil;
            cancerSeries.AddElements(ovarian_bilateral_elem);

            sc.Add(cancerSeries);

            chart1.SeriesCollection.Add(sc);

            SeriesCollection sc2 = new SeriesCollection();

            Series passed_series = new Series();
            passed_series.DefaultElement.Color = Color.Red;

            Element bc_passed = new Element();
            bc_passed.Name = "High Risk by Age of Breast Cancer";
            bc_passed.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcPassed == 1).Count();
            hr_bc = (int)bc_passed.YValue;
            passed_series.Elements.Add(bc_passed);


            Element oc_passed = new Element();
            oc_passed.Name = "High Risk by Ovarian Cancer Dx";
            oc_passed.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcPassed == 1).Count();
            hr_oc = (int)oc_passed.YValue; 
            passed_series.Elements.Add(oc_passed);


            Element either_passed = new Element();
            either_passed.Name = "High Risk by Breast Cancer Age or Ovarian Cancer Dx";
            either_passed.YValue = frequency.Where(mr => ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).OcPassed == 1 ||
                                                         ((RiskApps3.Model.Clinic.Reports.BrOvCancerFrequency.BrOvCancerStat)mr).BcPassed == 1).Count();
            hr_both = (int)either_passed.YValue; 
            passed_series.Elements.Add(either_passed);

            sc2.Add(passed_series);

            chart2.SeriesCollection.Add(sc2);
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
                                           "Total Patients",
                                            frequency.Count.ToString("#,###,###"),
                                            "");

            int percent = (int)Math.Round(100 * (double)total_bc / (double)frequency.Count,0);

            UIUtils.AddColumnRowToTable(table,
                                           "Total Breast Cancer Patients",
                                            total_bc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)uni_bc / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Unilateral Breast Cancer",
                                            uni_bc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)bil_bc / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Bilateral Breast Cancer",
                                            bil_bc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)oc / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Ovarian Cancer",
                                            oc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)oc_uni / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Ovarian w/ Unilateral Breast",
                                            oc_uni.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)oc_bil / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Ovarian w/ Bilateral Breast",
                                            oc_bil.ToString("#,###,###"),
                                            percent.ToString() + "%");

            return table;
        }

        private HtmlNode GetTable2Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);

            int percent = (int)Math.Round(100 * (double)hr_bc / (double)frequency.Count, 0);

            UIUtils.AddColumnRowToTable(table,
                                           "Early Dx Breast Cancer",
                                            hr_bc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)hr_oc / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Ovarian Cancer",
                                            hr_oc.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)hr_both / (double)frequency.Count, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Ovarian or Early Breast",
                                            hr_both.ToString("#,###,###"),
                                            percent.ToString() + "%");
            return table;
        }

    }
}

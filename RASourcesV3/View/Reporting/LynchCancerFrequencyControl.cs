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
    public partial class LynchCancerFrequencyControl : UserControl
    {
        public LynchCancerFrequency frequency = new LynchCancerFrequency();

        int colorectal = 0;
        int endouterine = 0;
        int both = 0;
        int hr = 0;

        
        public LynchCancerFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();
            frequency.StartTime = StartTime;
            frequency.EndTime = EndTime;
            frequency.clinicID = clinicId;
            frequency.type = type;

            chart1.Width = 500;
            chart1.LegendBox.Visible = false;
        }

        internal void GetData()
        {
            frequency.BackgroundListLoad();
            LoadChart();
        }
        private void LoadChart()
        {
            SeriesCollection sc = new SeriesCollection();

            Series ColoRectal = new Series("Lynch Syndrome Cancers");
            ColoRectal.DefaultElement.Color = Color.LightBlue;

            Element coloRectal_elem = new Element();
            coloRectal_elem.Name = "ColoRectal Cancer";
            coloRectal_elem.YValue = frequency.Where(mr => ((LynchCancerFrequency.LynchCancerStat)mr).ColoRectalCount > 0 &&
                                                           ((LynchCancerFrequency.LynchCancerStat)mr).EndoUterineCount == 0).Count();
            colorectal += (int)coloRectal_elem.YValue;
            ColoRectal.AddElements(coloRectal_elem);

            Element endoUterine_elem = new Element();
            endoUterine_elem.Name = "Endometrial or Uterine Cancer";
            endoUterine_elem.YValue = frequency.Where(mr => ((LynchCancerFrequency.LynchCancerStat)mr).EndoUterineCount > 0 &&
                                                          ((LynchCancerFrequency.LynchCancerStat)mr).ColoRectalCount == 0).Count();
            endouterine += (int)endoUterine_elem.YValue;
            ColoRectal.AddElements(endoUterine_elem);

            Element both_elem = new Element();
            both_elem.Name = "Both";
            both_elem.YValue = frequency.Where(mr => ((LynchCancerFrequency.LynchCancerStat)mr).EndoUterineCount > 0 &&
                                                        ((LynchCancerFrequency.LynchCancerStat)mr).ColoRectalCount > 0).Count();
            both += (int)both_elem.YValue;
            ColoRectal.AddElements(both_elem);

            Element hr_elem = new Element();
            hr_elem.Name = "Early Age of Dx";
            hr_elem.YValue = frequency.Where(mr => ((LynchCancerFrequency.LynchCancerStat)mr).EarlyDx > 0).Count();
            hr += (int)both_elem.YValue;
            ColoRectal.AddElements(hr_elem);

            sc.Add(ColoRectal);

            chart1.SeriesCollection.Add(sc);

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

            int total = (colorectal + endouterine + both);

            UIUtils.AddColumnRowToTable(table,
                                           "Total Lynch Cancer Patients",
                                            total.ToString("#,###,###"), "");

            int percent = (int)Math.Round(100 * (double)colorectal / (double)total, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Colorectal Cancer",
                                            colorectal.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)endouterine / (double)total, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Endometrial/Uterine Cancer",
                                            endouterine.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)both / (double)total, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Both",
                                            both.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)hr / (double)total, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Early Age of Dx",
                                            hr.ToString("#,###,###"),
                                            percent.ToString() + "%");

            return table;
        }
    }
}

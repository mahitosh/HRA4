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
    public partial class ColonRiskFrequencyControl : UserControl
    {
        public ColonRiskFrequency colonRisk = new ColonRiskFrequency();

        public ColonRiskFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();
            colonRisk.StartTime = StartTime;
            colonRisk.EndTime = EndTime;
            colonRisk.clinicID = clinicId;
            colonRisk.type = type;

            chart1.Title = "Distribution of Colon Cancer Lifetime Risk by CCRAT";
            chart1.TitleBox.Background.Color = Color.White;
            chart1.TitleBox.HeaderLabel.Font = new Font(chart1.TitleBox.HeaderLabel.Font.FontFamily, 12);
            chart1.Palette = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            chart1.YAxis.SmartScaleBreak = true;
            chart1.XAxis.Minimum = 0;
            chart1.LegendBox.Background.Color = Color.White;

            chart2.Title = "Distribution of Lynch Syndrome Cancer Lifetime Risk by MMRPRO";
            chart2.TitleBox.Background.Color = Color.White;
            chart2.TitleBox.HeaderLabel.Font = new Font(chart1.TitleBox.HeaderLabel.Font.FontFamily, 12);
            chart2.Palette = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            chart2.YAxis.SmartScaleBreak = true;
            chart2.XAxis.Minimum = 0;
            chart2.LegendBox.Background.Color = Color.White;

        }
        internal void GetData()
        {
            colonRisk.BackgroundListLoad();

            LoadChart();
        }

        private void LoadChart()
        {
            SeriesCollection sc = new SeriesCollection();

            Series mmrpro_colon_series = new Series("MMRPRO - Colon Cancer");
            Series mmrpro_endo_series = new Series("MMRPRO - Endometrial Cancer");
            Series CCRAT_series = new Series("CCRAT - Colon Cancer");

            mmrpro_colon_series.LegendEntry.Value = "";
            mmrpro_endo_series.LegendEntry.Value = "";
            CCRAT_series.LegendEntry.Value = "";

            int cap = 5;

            for (int i = 0; i < cap; i++)
            {
                Element mmrpro_colon = new Element();
                mmrpro_colon.Name = GetElementName(i);
                if (i == cap)
                {
                    mmrpro_colon.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).MMRPRO_Lifetime_Colon >= i).Count();
                }
                else
                {
                    mmrpro_colon.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).MMRPRO_Lifetime_Colon == i).Count();
                }
                mmrpro_colon_series.AddElements(mmrpro_colon);

                Element mmrpro_endo = new Element();
                mmrpro_endo.Name = GetElementName(i);
                if (i == cap)
                {
                    mmrpro_endo.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).MMRPRO_Lifetime_Endo >= i).Count();
                }
                else
                {
                    mmrpro_endo.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).MMRPRO_Lifetime_Endo == i).Count();
                }
                mmrpro_endo_series.AddElements(mmrpro_endo);

                Element ccrat_elem = new Element();
                ccrat_elem.Name = GetElementName(i);
                if (i == cap)
                {
                    ccrat_elem.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).CCRAT >= i).Count();
                }
                else
                {
                    ccrat_elem.YValue = colonRisk.Where(mr => ((ColonRiskFrequency.ColonRiskStat)mr).CCRAT == i).Count();
                }
                CCRAT_series.AddElements(ccrat_elem);
            }

            sc.Add(mmrpro_colon_series);
            sc.Add(mmrpro_endo_series);
            sc.Add(CCRAT_series);

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


            return table;
        }
        private string GetElementName(int i)
        {
            string retval = "";

            switch(i)
            {
                case 0:
                    retval = "0 - 4%";
                    break;
                case 1:
                    retval = "5 - 9%";
                    break;
                case 2:
                    retval = "10 - 14%";
                    break;
                case 3:
                    retval = "15 - 20%";
                    break;
                case 4:
                    retval = "Over 20%";
                    break;
            }

            return retval;
        }
    }
}

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
    public partial class LynchRiskFrequencyControl : UserControl
    {
        public LynchRiskFrequency mutationRisk = new LynchRiskFrequency();

        double low = 0;
        double mid = 0;
        double high = 0;
        double passed = 0;
        double all = 0;

        public LynchRiskFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();

            mutationRisk.StartTime = StartTime;
            mutationRisk.EndTime = EndTime;
            mutationRisk.clinicID = clinicId;
            mutationRisk.type = type;

            chart1.Title = "Distribution of Lynch Syndrome Risk";
            chart1.TitleBox.Background.Color = Color.White;
            chart1.TitleBox.HeaderLabel.Font = new Font(chart1.TitleBox.HeaderLabel.Font.FontFamily, 12);
            chart1.Palette = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            chart1.YAxis.SmartScaleBreak = true;
            chart1.XAxis.Minimum = 0;
            chart1.LegendBox.Background.Color = Color.White;

            chart2.TitleBox.HeaderLabel.Font = new Font(chart2.TitleBox.HeaderLabel.Font.FontFamily, 12);
            chart2.Title = "Patients Meeting High Risk Threshold By Model - All Comers";
            chart2.TitleBox.Background.Color = Color.White;
            chart2.LegendBox.Visible = false;
            chart2.DefaultElement.SmartLabel.Text = "%Name";

            chart3.TitleBox.HeaderLabel.Font = new Font(chart3.TitleBox.HeaderLabel.Font.FontFamily, 12);
            chart3.Title = "Patients Meeting High Risk Threshold By Model - Among Those Found by Any model";
            chart3.DefaultTitleBox.Background.Color = Color.White;
            chart3.LegendBox.Visible = false;
            chart3.DefaultElement.SmartLabel.Text = "%Name";
        }
    
        internal void GetData()
        {
            mutationRisk.BackgroundListLoad();

            LoadChart();
        }

        private void LoadChart()
        {

            all = mutationRisk.Count();
            low = mutationRisk.Where(mr => ((LynchRiskFrequency.LynchMutationRisk)mr).MMRPro== 0 &&
                                            ((LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 0
                                    ).Count();



            mid = mutationRisk.Where(mr => (
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 1 || 
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 1
                                            )
                                             &&
                                            !(
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 2 || 
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 2
                                            )
                                    ).Count();

            high = mutationRisk.Where(mr => (
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 2 || 
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 2
                                            )
                                    ).Count();

            passed = mutationRisk.Where(mr => 
                                            (
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1 || 
                                                ((LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1
                                            )
                                        ).Count();


            SeriesCollection sc = new SeriesCollection();

            Series mmrpro = new Series("MMRPro");
            Series premm = new Series("PREMM");

            mmrpro.LegendEntry.Value = "";
            premm.LegendEntry.Value = "";

            Element mmrpro_elem0 = new Element();
            mmrpro_elem0.Name = "0-4%";
            mmrpro_elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 0).Count();
            mmrpro.AddElements(mmrpro_elem0);
            Element mmrpro_elem1 = new Element();
            mmrpro_elem1.Name = "5-9%";
            mmrpro_elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 1).Count();
            mmrpro.AddElements(mmrpro_elem1);
            Element mmrpro_elem2 = new Element();
            mmrpro_elem2.Name = "10% or Greater";
            mmrpro_elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro == 2).Count();
            mmrpro.AddElements(mmrpro_elem2);


            Element premm__elem0 = new Element();
            premm__elem0.Name = "0-4%";
            premm__elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 0).Count();
            premm.AddElements(premm__elem0);
            Element premm__elem1 = new Element();
            premm__elem1.Name = "5-9%";
            premm__elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 1).Count();
            premm.AddElements(premm__elem1);
            Element premm__elem2 = new Element();
            premm__elem2.Name = "10% or Greater";
            premm__elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM == 2).Count();
            premm.AddElements(premm__elem2);




            sc.Add(mmrpro);
            sc.Add(premm);

            chart1.SeriesCollection.Add(sc);

            SeriesCollection passedCollection = new SeriesCollection();
            Series mmrpro_passed = new Series("MMRPro");
            Series premm__passed = new Series("PREMM");

            Element mmrpro_yes = new Element();
            mmrpro_yes.Name = "At or Above Threshold";
            mmrpro_yes.Color = Color.Red;
            mmrpro_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1).Count();
            mmrpro_passed.AddElements(mmrpro_yes);

            Element mmrpro_no = new Element();
            mmrpro_no.Name = "Below Threshold";
            mmrpro_no.Color = Color.White;
            mmrpro_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 0).Count();
            mmrpro_passed.AddElements(mmrpro_no);

            Element premm__yes = new Element();
            premm__yes.Name = "At or Above Threshold";
            premm__yes.Color = Color.Red;
            premm__yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1).Count();
            premm__passed.AddElements(premm__yes);

            Element premm__no = new Element();
            premm__no.Name = "Below Threshold";
            premm__no.Color = Color.White;
            premm__no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 0).Count();
            premm__passed.AddElements(premm__no);
            

            passedCollection.Add(mmrpro_passed);
            passedCollection.Add(premm__passed);

            chart2.SeriesCollection.Add(passedCollection);

            SeriesCollection passedbyModelCollection = new SeriesCollection();
            Series mmrpro_passedbyModel = new Series("MMRPro");
            Series premm__passedbyModel = new Series("PREMM");

            Element brcapropassedby_yes = new Element();
            brcapropassedby_yes.Name = "At or Above Threshold";
            brcapropassedby_yes.Color = Color.Red;
            brcapropassedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1)).Count();
            mmrpro_passedbyModel.AddElements(brcapropassedby_yes);

            Element brcapropassedby_no = new Element();
            brcapropassedby_no.Name = "Below Threshold";
            brcapropassedby_no.Color = Color.White;
            brcapropassedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1)).Count();
            mmrpro_passedbyModel.AddElements(brcapropassedby_no);

            Element myriadpassedby_yes = new Element();
            myriadpassedby_yes.Name = "At or Above Threshold";
            myriadpassedby_yes.Color = Color.Red;
            myriadpassedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1)).Count();
            premm__passedbyModel.AddElements(myriadpassedby_yes);

            Element myriadpassedby_no = new Element();
            myriadpassedby_no.Name = "Below Threshold";
            myriadpassedby_no.Color = Color.White;
            myriadpassedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).PREMM_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.LynchRiskFrequency.LynchMutationRisk)mr).MMRPro_passed == 1)).Count();
            premm__passedbyModel.AddElements(myriadpassedby_no);

            


            passedbyModelCollection.Add(mmrpro_passedbyModel);
            passedbyModelCollection.Add(premm__passedbyModel);

            chart3.SeriesCollection.Add(passedbyModelCollection);
        }

        private string GetAxisLabel(int bin)
        {
            switch(bin)
            {
                case 0:
                    return "0-4%";
                case 1:
                    return "5-9%";
                case 2:
                    return "10 or Greater";
                default:
                    return "";
            }
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

            //UIUtils.AddTwoColumnRowToTable(table,
            //                               GetTable3Node(node),
            //                               UIUtils.GetImageHtmlFromChart(chart3, node));

            node.ChildNodes.Add(table);
        }


        private HtmlNode GetTable1Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);

            UIUtils.AddColumnRowToTable(table,
                                           "Total Patients with Scores From Report Period",
                                            all.ToString("#,###,###"), "");

            int percent = (int)Math.Round(100 * (double)low / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "0-4% Patients",
                                            low.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)mid / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "5-9%",
                                            mid.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)high / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "10% or Greater",
                                            high.ToString("#,###,###"), 
                                            percent.ToString() + "%");


            return table;
        }

        private HtmlNode GetTable2Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);
            UIUtils.AddColumnRowToTable(table,
                               "Total Patients with Scores",
                                all.ToString("#,###,###"), "");

            int percent = (int)Math.Round(100 * (double)passed / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                               "At or Above Threshold",
                                passed.ToString("#,###,###"),
                                percent.ToString() + "%");
            return table;
        }

        //private HtmlNode GetTable3Node(HtmlNode node)
        //{
        //    HtmlNode table = UIUtils.CreateReportTableNode(node);

        //    return table;
        //}

    }
}


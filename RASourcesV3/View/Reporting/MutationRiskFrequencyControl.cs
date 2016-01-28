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
    public partial class MutationRiskFrequencyControl : UserControl
    {
        public MutationRiskFrequency mutationRisk = new MutationRiskFrequency();

        double low = 0;
        double mid = 0;
        double high = 0;
        double passed = 0;
        double all = 0;

        double brcaproCount = 0;
        double myriadCount = 0;
        double tc6Count = 0;
        double tc7Count = 0;

        string brcaproPercent = "";
        string myriadPercent = "";
        string tc6Percent = "";
        string tc7Percent = "";

        public MutationRiskFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();

            mutationRisk.StartTime = StartTime;
            mutationRisk.EndTime = EndTime;
            mutationRisk.clinicID = clinicId;
            mutationRisk.type = type;

            chart1.Title = "Distribution of BRCA Mutation Risk";
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
            chart3.TitleBox.Background.Color = Color.White;
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
            low = mutationRisk.Where(mr => ((MutationRiskFrequency.MutationRisk)mr).Myriad == 0 &&
                                            ((MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 0 &&
                                            ((MutationRiskFrequency.MutationRisk)mr).TC6 == 0 &&
                                            ((MutationRiskFrequency.MutationRisk)mr).TC7 == 0
                                    ).Count();



            mid = mutationRisk.Where(mr => (
                                                ((MutationRiskFrequency.MutationRisk)mr).Myriad == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC6 == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC7 == 1
                                            )
                                             &&
                                            !(
                                                ((MutationRiskFrequency.MutationRisk)mr).Myriad == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC6 == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC7 == 2
                                            )
                                    ).Count();

            high = mutationRisk.Where(mr => (
                                                ((MutationRiskFrequency.MutationRisk)mr).Myriad == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC6 == 2 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC7 == 2
                                            )
                                    ).Count();

            passed = mutationRisk.Where(mr => 
                                            (
                                                ((MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                ((MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1
                                            )
                                        ).Count();


            SeriesCollection sc = new SeriesCollection();

            Series brcapro = new Series("BRCAPRO");
            Series myriad = new Series("Myriad");
            Series TC6 = new Series("TC6");
            Series TC7 = new Series("TC7");

            brcapro.LegendEntry.Value = "";
            myriad.LegendEntry.Value = "";
            TC6.LegendEntry.Value = "";
            TC7.LegendEntry.Value = "";

            Element brcapro_elem0 = new Element();
            brcapro_elem0.Name = "0-4%";
            brcapro_elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 0).Count();
            brcapro.AddElements(brcapro_elem0);
            Element brcapro_elem1 = new Element();
            brcapro_elem1.Name = "5-9%";
            brcapro_elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 1).Count();
            brcapro.AddElements(brcapro_elem1);
            Element brcapro_elem2 = new Element();
            brcapro_elem2.Name = "10% or Greater";
            brcapro_elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO == 2).Count();
            brcapro.AddElements(brcapro_elem2);


            Element myriad_elem0 = new Element();
            myriad_elem0.Name = "0-4%";
            myriad_elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad == 0).Count();
            myriad.AddElements(myriad_elem0);
            Element myriad_elem1 = new Element();
            myriad_elem1.Name = "5-9%";
            myriad_elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad == 1).Count();
            myriad.AddElements(myriad_elem1);
            Element myriad_elem2 = new Element();
            myriad_elem2.Name = "10% or Greater";
            myriad_elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad == 2).Count();
            myriad.AddElements(myriad_elem2);


            Element TC6_elem0 = new Element();
            TC6_elem0.Name = "0-4%";
            TC6_elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6 == 0).Count();
            TC6.AddElements(TC6_elem0);
            Element TC6_elem1 = new Element();
            TC6_elem1.Name = "5-9%";
            TC6_elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6 == 1).Count();
            TC6.AddElements(TC6_elem1);
            Element TC6_elem2 = new Element();
            TC6_elem2.Name = "10% or Greater";
            TC6_elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6 == 2).Count();
            TC6.AddElements(TC6_elem2);


            Element TC7_elem0 = new Element();
            TC7_elem0.Name = "0-4%";
            TC7_elem0.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7 == 0).Count();
            TC7.AddElements(TC7_elem0);
            Element TC7_elem1 = new Element();
            TC7_elem1.Name = "5-9%";
            TC7_elem1.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7 == 1).Count();
            TC7.AddElements(TC7_elem1);
            Element TC7_elem2 = new Element();
            TC7_elem2.Name = "10% or Greater";
            TC7_elem2.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7 == 2).Count();
            TC7.AddElements(TC7_elem2);


            sc.Add(brcapro);
            sc.Add(myriad);
            sc.Add(TC6);
            sc.Add(TC7);

            chart1.SeriesCollection.Add(sc);

            SeriesCollection passedCollection = new SeriesCollection();
            Series brcapro_passed = new Series("BRCAPRO");
            Series myriad_passed = new Series("Myriad");
            Series TC6_passed = new Series("TC6");
            Series TC7_passed = new Series("TC7");


            Element brcapro_yes = new Element();
            brcapro_yes.Color = Color.Red;
            brcapro_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1).Count();
            brcaproCount = brcapro_yes.YValue;
            brcaproPercent = Math.Round(brcapro_yes.YValue / all * 100, 1).ToString() + "%";
            brcapro_yes.Name = brcaproPercent;
            brcapro_passed.AddElements(brcapro_yes);

            Element brcapro_no = new Element();
            brcapro_no.Color = Color.White;
            brcapro_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 0).Count();
            brcapro_no.Name = Math.Round(brcapro_no.YValue / all * 100, 1).ToString() + "%";
            brcapro_passed.AddElements(brcapro_no);

            Element myriad_yes = new Element();
            myriad_yes.Color = Color.Red;
            myriad_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1).Count();
            myriadCount = myriad_yes.YValue;
            myriadPercent = Math.Round(myriad_yes.YValue / all * 100, 1).ToString() + "%";
            myriad_yes.Name = myriadPercent;
            myriad_passed.AddElements(myriad_yes);

            Element myriad_no = new Element();
            myriad_no.Color = Color.White;
            myriad_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 0).Count();
            myriad_no.Name = Math.Round(myriad_no.YValue / all * 100, 1).ToString() + "%";
            myriad_passed.AddElements(myriad_no);

            Element TC6_yes = new Element();
            TC6_yes.Color = Color.Red;
            TC6_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1).Count();
            tc6Count = TC6_yes.YValue;
            tc6Percent = Math.Round(TC6_yes.YValue / all * 100, 1).ToString() + "%";
            TC6_yes.Name = tc6Percent;
            TC6_passed.AddElements(TC6_yes);

            Element TC6_no = new Element();
            TC6_no.Color = Color.White;
            TC6_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 0).Count();
            TC6_no.Name = Math.Round(TC6_no.YValue / all * 100, 1).ToString() + "%";
            TC6_passed.AddElements(TC6_no);

            Element TC7_yes = new Element();
            TC7_yes.Color = Color.Red;
            TC7_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1).Count();
            tc7Count = TC7_yes.YValue;
            tc7Percent = Math.Round(TC7_yes.YValue / all * 100, 1).ToString() + "%";
            TC7_yes.Name = tc7Percent;
            TC7_passed.AddElements(TC7_yes);

            Element TC7_no = new Element();
            TC7_no.Color = Color.White;
            TC7_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 0).Count();
            TC7_no.Name = Math.Round(TC7_no.YValue / all * 100, 1).ToString() + "%";
            TC7_passed.AddElements(TC7_no);


            passedCollection.Add(brcapro_passed);
            passedCollection.Add(myriad_passed);
            passedCollection.Add(TC6_passed);
            passedCollection.Add(TC7_passed);

            chart2.SeriesCollection.Add(passedCollection);

            double denominator = passed;

            SeriesCollection passedbyModelCollection = new SeriesCollection();
            Series brcapro_passedbyModel = new Series("BRCAPRO");
            Series myriad_passedbyModel = new Series("Myriad");
            Series TC6_passedbyModel = new Series("TC6");
            Series TC7_passedbyModel = new Series("TC7");

            Element brcapropassedby_yes = new Element();
            //brcapropassedby_yes.Name = "At or Above Threshold";
            brcapropassedby_yes.Color = Color.Red;
            brcapropassedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            brcapropassedby_yes.Name = Math.Round(brcapropassedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_passedbyModel.AddElements(brcapropassedby_yes);

            Element brcapropassedby_no = new Element();
            //brcapropassedby_no.Name = "Below Threshold";
            brcapropassedby_no.Color = Color.White;
            brcapropassedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            brcapropassedby_no.Name = Math.Round(brcapropassedby_no.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_passedbyModel.AddElements(brcapropassedby_no);

            Element myriadpassedby_yes = new Element();
            //myriadpassedby_yes.Name = "At or Above Threshold";
            myriadpassedby_yes.Color = Color.Red;
            myriadpassedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            myriadpassedby_yes.Name = Math.Round(myriadpassedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            myriad_passedbyModel.AddElements(myriadpassedby_yes);

            Element myriadpassedby_no = new Element();
            //myriadpassedby_no.Name = "Below Threshold";
            myriadpassedby_no.Color = Color.White;
            myriadpassedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            myriadpassedby_no.Name = Math.Round(myriadpassedby_no.YValue / denominator * 100, 1).ToString() + "%";
            myriad_passedbyModel.AddElements(myriadpassedby_no);

            Element TC6passedby_yes = new Element();
            //TC6passedby_yes.Name = "At or Above Threshold";
            TC6passedby_yes.Color = Color.Red;
            TC6passedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            TC6passedby_yes.Name = Math.Round(TC6passedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC6_passedbyModel.AddElements(TC6passedby_yes);

            Element TC6passedby_no = new Element();
            //TC6passedby_no.Name = "Below Threshold";
            TC6passedby_no.Color = Color.White;
            TC6passedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            TC6passedby_no.Name = Math.Round(TC6passedby_no.YValue / denominator * 100, 1).ToString() + "%";
            TC6_passedbyModel.AddElements(TC6passedby_no);

            Element TC7passedby_yes = new Element();
            //TC7passedby_yes.Name = "At or Above Threshold";
            TC7passedby_yes.Color = Color.Red;
            TC7passedby_yes.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            TC7passedby_yes.Name = Math.Round(TC7passedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC7_passedbyModel.AddElements(TC7passedby_yes);

            Element TC7passedby_no = new Element();
            //TC7passedby_no.Name = "Below Threshold";
            TC7passedby_no.Color = Color.White;
            TC7passedby_no.YValue = mutationRisk.Where(mr => ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 0 &&
                                                                   (((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).Myriad_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).BRCAPRO_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC6_passed == 1 ||
                                                                     ((RiskApps3.Model.Clinic.Reports.MutationRiskFrequency.MutationRisk)mr).TC7_passed == 1)).Count();
            TC7passedby_no.Name = Math.Round(TC7passedby_no.YValue / denominator * 100, 1).ToString() + "%";
            TC7_passedbyModel.AddElements(TC7passedby_no);


            passedbyModelCollection.Add(brcapro_passedbyModel);
            passedbyModelCollection.Add(myriad_passedbyModel);
            passedbyModelCollection.Add(TC6_passedbyModel);
            passedbyModelCollection.Add(TC7_passedbyModel);

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

            UIUtils.AddTwoColumnRowToTable(table,
                                           UIUtils.CreateReportTableNode(node),
                                           UIUtils.GetImageHtmlFromChart(chart3, node));

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

            UIUtils.AddColumnRowToTable(table,
                   "BRCAPRO",
                    brcaproCount.ToString("#,###,###"),
                    brcaproPercent);

            UIUtils.AddColumnRowToTable(table,
                   "Myriad",
                    myriadCount.ToString("#,###,###"),
                    myriadPercent);

            UIUtils.AddColumnRowToTable(table,
                   "TC6",
                    tc6Count.ToString("#,###,###"),
                    tc6Percent);

            UIUtils.AddColumnRowToTable(table,
                   "TC7",
                    tc7Count.ToString("#,###,###"),
                    tc7Percent);
            
            return table;
        }
    }
}


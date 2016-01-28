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
    public partial class BreastRiskFrequencyControl : UserControl
    {
        public BreastRiskFrequency breastRisk = new BreastRiskFrequency();

        double low = 0;
        double mid = 0;
        double high = 0;
        double non = 0;
        double passed = 0;
        double all = 0;

        double brcaproCount = 0;
        double clausCount = 0;
        double tc6Count = 0;
        double tc7Count = 0;

        string brcaproPercent = "";
        string clausPercent = "";
        string tc6Percent = "";
        string tc7Percent = "";

        public BreastRiskFrequencyControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();
            breastRisk.StartTime = StartTime;
            breastRisk.EndTime = EndTime;
            breastRisk.clinicID = clinicId;
            breastRisk.type = type;

            chart1.Title = "Distribution of Breast Cancer Lifetime Risk";
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
            breastRisk.BackgroundListLoad();
            
            LoadChart();
        }

        private void LoadChart()
        {
            all = breastRisk.Count();
            non = breastRisk.Where(mr =>    ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0).Count();

            low = breastRisk.Where(mr =>    ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                            ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 0 &&
                                            ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 0 &&
                                            ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 0 &&
                                            ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 0
                                    ).Count();



            mid = breastRisk.Where(mr =>    ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                            (
                                                ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 1
                                            )
                                             &&
                                            !(
                                                ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 2
                                            )
                                    ).Count();

            high = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                            (
                                                ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 2 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 2
                                            )
                                    ).Count();

            passed = breastRisk.Where(mr =>    ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                            (
                                                ((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1
                                            )
                                        ).Count();

            SeriesCollection sc = new SeriesCollection();

            Series brcapro = new Series("BRCAPRO");
            Series claus = new Series("Claus");
            Series TC6 = new Series("TC6");
            Series TC7 = new Series("TC7");

            brcapro.LegendEntry.Value = "";
            claus.LegendEntry.Value = "";
            TC6.LegendEntry.Value = "";
            TC7.LegendEntry.Value = "";

            Element brcapro_elem0 = new Element();
            brcapro_elem0.Name = "0-14%";
            brcapro_elem0.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 0).Count();
            brcapro.AddElements(brcapro_elem0);
            Element brcapro_elem1 = new Element();
            brcapro_elem1.Name = "15-19%";
            brcapro_elem1.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 1).Count();
            brcapro.AddElements(brcapro_elem1);
            Element brcapro_elem2 = new Element();
            brcapro_elem2.Name = "20% or Greater";
            brcapro_elem2.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO == 2).Count();
            brcapro.AddElements(brcapro_elem2);


            Element claus_elem0 = new Element();
            claus_elem0.Name = "0-14%";
            claus_elem0.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 0).Count();
            claus.AddElements(claus_elem0);
            Element claus_elem1 = new Element();
            claus_elem1.Name = "15-19%";
            claus_elem1.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 1).Count();
            claus.AddElements(claus_elem1);
            Element claus_elem2 = new Element();
            claus_elem2.Name = "20% or Greater";
            claus_elem2.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus == 2).Count();
            claus.AddElements(claus_elem2);


            Element TC6_elem0 = new Element();
            TC6_elem0.Name = "0-14%";
            TC6_elem0.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 0).Count();
            TC6.AddElements(TC6_elem0);
            Element TC6_elem1 = new Element();
            TC6_elem1.Name = "15-19%";
            TC6_elem1.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 1).Count();
            TC6.AddElements(TC6_elem1);
            Element TC6_elem2 = new Element();
            TC6_elem2.Name = "20% or Greater";
            TC6_elem2.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6 == 2).Count();
            TC6.AddElements(TC6_elem2);


            Element TC7_elem0 = new Element();
            TC7_elem0.Name = "0-14%";
            TC7_elem0.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 0).Count();
            TC7.AddElements(TC7_elem0);
            Element TC7_elem1 = new Element();
            TC7_elem1.Name = "15-19%";
            TC7_elem1.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 1).Count();
            TC7.AddElements(TC7_elem1);
            Element TC7_elem2 = new Element();
            TC7_elem2.Name = "20% or Greater";
            TC7_elem2.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7 == 2).Count();
            TC7.AddElements(TC7_elem2);

            sc.Add(brcapro);
            sc.Add(claus);
            sc.Add(TC6);
            sc.Add(TC7);

            chart1.SeriesCollection.Add(sc);

            double denominator = breastRisk.Count();

            SeriesCollection passedCollection = new SeriesCollection();
            Series brcapro_passed = new Series("BRCAPRO");
            Series claus_passed = new Series("Claus");
            Series TC6_passed = new Series("TC6");
            Series TC7_passed = new Series("TC7");

            Element brcapro_yes = new Element();
            brcapro_yes.Color = Color.Red;
            brcapro_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1).Count();
            brcaproCount = brcapro_yes.YValue;
            brcaproPercent = Math.Round(brcapro_yes.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_yes.Name = brcaproPercent;
            brcapro_passed.AddElements(brcapro_yes);

            Element brcapro_no = new Element();
            brcapro_no.Color = Color.White;
            brcapro_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 0).Count();
            brcapro_no.Name = Math.Round(brcapro_no.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_passed.AddElements(brcapro_no);

            Element claus_yes = new Element(); 
            claus_yes.Color = Color.Red;
            claus_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1).Count();
            clausCount = claus_yes.YValue;
            clausPercent = Math.Round(claus_yes.YValue / denominator * 100, 1).ToString() + "%";
            claus_yes.Name = clausPercent;
            claus_passed.AddElements(claus_yes);

            Element claus_no = new Element(); 
            claus_no.Color = Color.White;
            claus_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 0).Count();
            claus_no.Name = Math.Round(claus_no.YValue / denominator * 100, 1).ToString() + "%";
            claus_passed.AddElements(claus_no);

            Element TC6_yes = new Element(); 
            TC6_yes.Color = Color.Red;
            TC6_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1).Count();
            tc6Count = TC6_yes.YValue;
            tc6Percent = Math.Round(TC6_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC6_yes.Name = tc6Percent; 
            TC6_passed.AddElements(TC6_yes);

            Element TC6_no = new Element();
            TC6_no.Color = Color.White;
            TC6_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 0).Count();
            TC6_no.Name = Math.Round(TC6_no.YValue / denominator * 100, 1).ToString() + "%";
            TC6_passed.AddElements(TC6_no);

            Element TC7_yes = new Element();
            TC7_yes.Color = Color.Red;
            TC7_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1).Count();
            tc7Count = TC7_yes.YValue;
            tc7Percent = Math.Round(TC7_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC7_yes.Name = tc7Percent; 
            TC7_passed.AddElements(TC7_yes);

            Element TC7_no = new Element();
            TC7_no.Color = Color.White;
            TC7_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 0).Count();
            TC7_no.Name = Math.Round(TC7_no.YValue / denominator * 100, 1).ToString() + "%";
            TC7_passed.AddElements(TC7_no);


            passedCollection.Add(brcapro_passed);
            passedCollection.Add(claus_passed);
            passedCollection.Add(TC6_passed);
            passedCollection.Add(TC7_passed);

            chart2.SeriesCollection.Add(passedCollection);

            denominator = passed;

            SeriesCollection passedbyModelCollection = new SeriesCollection();
            Series brcapro_passedbyModel = new Series("BRCAPRO");
            Series claus_passedbyModel = new Series("Claus");
            Series TC6_passedbyModel = new Series("TC6");
            Series TC7_passedbyModel = new Series("TC7");

            Element brcapropassedby_yes = new Element();
            brcapropassedby_yes.Color = Color.Red;
            brcapropassedby_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            brcapropassedby_yes.Name = Math.Round(brcapropassedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_passedbyModel.AddElements(brcapropassedby_yes);

            Element brcapropassedby_no = new Element();
            brcapropassedby_no.Color = Color.White;
            brcapropassedby_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 0 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            brcapropassedby_no.Name = Math.Round(brcapropassedby_no.YValue / denominator * 100, 1).ToString() + "%";
            brcapro_passedbyModel.AddElements(brcapropassedby_no);

            Element clauspassedby_yes = new Element();
            clauspassedby_yes.Color = Color.Red;
            clauspassedby_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            clauspassedby_yes.Name = Math.Round(clauspassedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            claus_passedbyModel.AddElements(clauspassedby_yes);

            Element clauspassedby_no = new Element();
            clauspassedby_no.Color = Color.White;
            clauspassedby_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 0 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            clauspassedby_no.Name = Math.Round(clauspassedby_no.YValue / denominator * 100, 1).ToString() + "%";
            claus_passedbyModel.AddElements(clauspassedby_no);

            Element TC6passedby_yes = new Element();
            TC6passedby_yes.Color = Color.Red;
            TC6passedby_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            TC6passedby_yes.Name = Math.Round(TC6passedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC6_passedbyModel.AddElements(TC6passedby_yes);

            Element TC6passedby_no = new Element();
            TC6passedby_no.Color = Color.White;
            TC6passedby_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 0 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            TC6passedby_no.Name = Math.Round(TC6passedby_no.YValue / denominator * 100, 1).ToString() + "%";
            TC6_passedbyModel.AddElements(TC6passedby_no);

            Element TC7passedby_yes = new Element();
            TC7passedby_yes.Color = Color.Red;
            TC7passedby_yes.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            TC7passedby_yes.Name = Math.Round(TC7passedby_yes.YValue / denominator * 100, 1).ToString() + "%";
            TC7_passedbyModel.AddElements(TC7passedby_yes);

            Element TC7passedby_no = new Element();
            TC7passedby_no.Color = Color.White;
            TC7passedby_no.YValue = breastRisk.Where(mr => ((BreastRiskFrequency.BreastRiskStat)mr).BreastCancer == 0 &&
                                                          ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 0 &&
                                                                   (((BreastRiskFrequency.BreastRiskStat)mr).Claus_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).BRCAPRO_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC6_passed == 1 ||
                                                                     ((BreastRiskFrequency.BreastRiskStat)mr).TC7_passed == 1)).Count();
            TC7passedby_no.Name = Math.Round(TC7passedby_no.YValue / denominator * 100, 1).ToString() + "%";
            TC7_passedbyModel.AddElements(TC7passedby_no);


            passedbyModelCollection.Add(brcapro_passedbyModel);
            passedbyModelCollection.Add(claus_passedbyModel);
            passedbyModelCollection.Add(TC6_passedbyModel);
            passedbyModelCollection.Add(TC7_passedbyModel);

            chart3.SeriesCollection.Add(passedbyModelCollection);
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
                                           "Total Patients with ScoresB",
                                            all.ToString("#,###,###"),
                                            "");

            int percent = (int)Math.Round(100 * (double)non / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "Non Breast Cancer Patients",
                                            non.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)low / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "0-14% Patients",
                                            low.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)mid / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "15-19%",
                                            mid.ToString("#,###,###"),
                                            percent.ToString() + "%");

            percent = (int)Math.Round(100 * (double)high / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                                           "20% or Greater",
                                            high.ToString("#,###,###"),
                                            percent.ToString() + "%");


            return table;
        }

        private HtmlNode GetTable2Node(HtmlNode node)
        {
            HtmlNode table = UIUtils.CreateReportTableNode(node);
            UIUtils.AddColumnRowToTable(table,
                               "Total Patients with Scores From Report Period",
                                all.ToString("#,###,###"),
                                "");
            
            int percent = (int)Math.Round(100 * (double)passed / (double)all, 0);
            UIUtils.AddColumnRowToTable(table,
                               "Total Patients At or Above Threshold",
                                passed.ToString("#,###,###"),
                                percent.ToString() + "%");

            UIUtils.AddColumnRowToTable(table,
                                       "BRCAPRO",
                                        brcaproCount.ToString("#,###,###"),
                                        brcaproPercent);

            UIUtils.AddColumnRowToTable(table,
                                       "Myriad",
                                        clausCount.ToString("#,###,###"),
                                        clausPercent);

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


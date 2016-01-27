using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dotnetCHARTING.WinForms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Risk;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class ColoEndoCancerRiskView : HraView
    {
        private Patient proband;

        Series ColonCaRisk_series = new Series();
        Series ColonCaRiskWithMLH1_series = new Series();
        Series ColonCaRiskWithMSH2_series = new Series();
        Series ColonCaRiskWithMSH6_series = new Series();
        Series ColonCaRiskNoMut_series = new Series();

        Series EndometrialCaRisk_series = new Series();
        Series EndometrialCaRiskWithMLH1_series = new Series();
        Series EndometrialCaRiskWithMSH2_series = new Series();
        Series EndometrialCaRiskWithMSH6_series = new Series();
        Series EndometrialCaRiskNoMut_series = new Series();

        string MLH1PosFiveYearColon = "N/A";
        string MSH2PosFiveYearColon = "N/A";
        string MSH6PosFiveYearColon = "N/A";

        string MLH1PosFiveYearEndo = "N/A";
        string MSH2PosFiveYearEndo = "N/A";
        string MSH6PosFiveYearEndo = "N/A";

        string MLH1PosLifetimeYearColon = "N/A";
        string MSH2PosLifetimeYearColon = "N/A";
        string MSH6PosLifetimeYearColon = "N/A";

        string MLH1PosLifetimeYearEndo = "N/A";
        string MSH2PosLifetimeYearEndo = "N/A";
        string MSH6PosLifetimeYearEndo = "N/A";

        Series CCRAT_series = new Series();



        public ColoEndoCancerRiskView()
        {
            InitializeComponent();

            chart1.DefaultElement.Marker.Type = ElementMarkerType.None;
            chart1.DefaultSeries.Type = SeriesType.Spline;
            chart1.DefaultSeries.Line.Width = 5;
            chart1.ChartArea.LegendBox.Visible = false;
            chart1.ChartArea.XAxis.Label.Text = "Age";
            chart1.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            chart2.DefaultElement.Marker.Type = ElementMarkerType.None;
            chart2.DefaultSeries.Type = SeriesType.Spline;
            chart2.DefaultSeries.Line.Width = 5;
            chart2.ChartArea.LegendBox.Visible = false;
            chart2.ChartArea.XAxis.Label.Text = "Age";
            chart2.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            CCRATchart.DefaultElement.Marker.Type = ElementMarkerType.None;
            CCRATchart.DefaultSeries.Type = SeriesType.Spline;
            CCRATchart.DefaultSeries.Line.Width = 5;
            CCRATchart.ChartArea.LegendBox.Visible = false;
            CCRATchart.ChartArea.XAxis.Label.Text = "Age";
            CCRATchart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";
        }

        private void ColoEndoCancerRiskView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient +=
                        new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
                InitActivePatient();
            }
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitActivePatient();
        }

        /**************************************************************************************************/
        private void InitActivePatient()
        {
            //  get selected relative object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            if (proband.HraState == HraObject.States.Ready)
            {
                int min = 0;
                int.TryParse(proband.age, out min);
                chart1.ChartArea.XAxis.Minimum = min;
                chart2.ChartArea.XAxis.Minimum = min;
                CCRATchart.ChartArea.XAxis.Minimum = min;
            }

            proband.RP.MmrproCancerRiskList.AddHandlersWithLoad(MMRproCancerRiskChanged, MMRproCancerRiskLoaded, MMRproCancerRiskItemChanged);
            proband.RP.CCRATModel.AddHandlersWithLoad(CCRATCancerRiskChanged, CCRATCancerRiskLoaded, CCRATCancerRiskItemChanged);
            proband.RP.AddHandlersWithLoad(null, RpLoaded, null);

        }

                

        delegate void RpLoadedCallback(object sender, RunWorkerCompletedEventArgs e);
        private void RpLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                RpLoadedCallback rmc = new RpLoadedCallback(RpLoaded);
                object[] args = new object[2];
                args[0] = sender;
                args[1] = e;
                this.Invoke(rmc, args);
            }
            else
            {
                label23.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob);
                label63.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob);
            }
        }
        private string NullDoubleToRoundedString(double? val)
        {
            string retval = "";
            if (val != null)
            {
                double x = (double)val;
                retval = Math.Round(x, 1).ToString() + "%";
            }
            return retval;
        }

        /**************************************************************************************************/
        private void MMRproCancerRiskItemChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void MMRproCancerRiskChanged(HraListChangedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void CCRATCancerRiskItemChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void CCRATCancerRiskChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void CCRATCancerRiskLoaded(HraListLoadedEventArgs list_e)
        {

            String colonNA = "";
            if (proband.HasColonCancer())
            {
                colonNA = "N/A because the patient has Colon cancer";
            }

            if (colonNA.Length > 0)
            {
                CCRATchart.Visible = false;
                tableLayoutPanel5.Visible = false;
                CCRATTextBox.Visible = false;
                CCRATNALabel.Visible = true;
                CCRATNALabel.Text = colonNA;
                return;
            }
            else
            {
                CCRATchart.Visible = true;
                tableLayoutPanel5.Visible = true;
                CCRATTextBox.Visible = true;
                CCRATNALabel.Visible = false;
            }


            CCRATTextBox.Text = "";
            CCRATTextBox.Visible = false;

            CCRATchart.SeriesCollection.Clear();
            CCRAT_series.Elements.Clear();
            CCRAT_series.Name = "CCRAT Colon Cancer Risk";

            int proband_age = -1;
            int.TryParse(proband.age, out proband_age);


            Element cur = new Element();
            cur.XValue = proband_age;
            cur.YValue = 0;
            CCRAT_series.Elements.Add(cur);

            foreach (CCRATRiskByAge score in proband.RP.CCRATModel)
            {
                Element e = new Element();
                e.XValue = score.CCRATRiskByAge_age;
                e.YValue = score.CCRATRiskByAge_ColonCaRisk;
                CCRAT_series.Elements.Add(e);
            }

            CCRAT_series.DefaultElement.Color = Color.Black;

            CCRATFiveYearPatientLabel.Text = NullDoubleToRoundedString(proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_FiveYear_CRC);
            CCRATLifetimePatientLabel.Text = NullDoubleToRoundedString(proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_Lifetime_CRC);

            CCRATTextBox.Text += proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_MESSAGES + Environment.NewLine;
            CCRATTextBox.Text += proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_NAERRORS;
            //CCRATERRORS.Text = proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_NAERRORS;
            //CCRATMSGS.Text = proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_MESSAGES;

            if (string.IsNullOrEmpty(proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_MESSAGES) == false)
                CCRATTextBox.Visible = true;

            if (string.IsNullOrEmpty(proband.RP.CCRATModel.Details.CCRATDetails_CCRAT_NAERRORS) == false)
            {
                CCRATTextBox.Visible = true;
                CCRATFiveYearPatientLabel.Visible = false;
                CCRATLifetimePatientLabel.Visible = false;
                CCRATchart.Visible = false;

            }
            else
            {
                ColonCaRisk_series.DefaultElement.Color = Color.Black;
                CCRATchart.SeriesCollection.Add(CCRAT_series);
                CCRATchart.RefreshChart();
            }
        }
        /**************************************************************************************************/
        delegate void MMRproCancerRiskLoadedCallback(HraListLoadedEventArgs list_e);
        private void MMRproCancerRiskLoaded(HraListLoadedEventArgs list_e)
        {
            if (this.InvokeRequired)
            {
                MMRproCancerRiskLoadedCallback rmc = new MMRproCancerRiskLoadedCallback(MMRproCancerRiskLoaded);
                object[] args = new object[1];
                args[0] = list_e; 
                this.Invoke(rmc, args);
            }
            else
            {
                String endoNA = "";
                if (proband.HasUterineCancer())
                {
                    endoNA = "N/A because the patient has Endometrial cancer";
                }
                if (proband.gender == "Male")
                {
                    endoNA = "N/A because the patient is male";
                }


                if (endoNA.Length > 0)
                {
                    chart2.Visible = false;
                    tableLayoutPanel2.Visible = false;
                    label26.Visible = false;
                    comboBox4.Visible = false;
                    endometrialNALabel.Visible = true;
                    endometrialNALabel.Text = endoNA;
                }
                else
                {
                    chart2.Visible = true;
                    tableLayoutPanel2.Visible = true;
                    label26.Visible = true;
                    comboBox4.Visible = true;
                    endometrialNALabel.Visible = false;
                }


                String colonNA = "";
                if (proband.HasColonCancer())
                {
                    colonNA = "N/A because the patient has Colon cancer";
                }

                if (colonNA.Length > 0)
                {
                    chart1.Visible = false;
                    tableLayoutPanel1.Visible = false;
                    label1.Visible = false;
                    comboBox3.Visible = false;
                    colonNALabel.Visible = true;
                    colonNALabel.Text = colonNA;
                }
                else
                {
                    chart1.Visible = true;
                    tableLayoutPanel1.Visible = true;
                    label1.Visible = true;
                    comboBox3.Visible = true;
                    colonNALabel.Visible = false;
                }




                chart1.SeriesCollection.Clear();
                chart2.SeriesCollection.Clear();

                ColonCaRisk_series.Elements.Clear();
                ColonCaRiskWithMLH1_series.Elements.Clear();
                ColonCaRiskWithMSH2_series.Elements.Clear();
                ColonCaRiskWithMSH6_series.Elements.Clear();
                ColonCaRiskNoMut_series.Elements.Clear();

                EndometrialCaRisk_series.Elements.Clear();
                EndometrialCaRiskWithMLH1_series.Elements.Clear();
                EndometrialCaRiskWithMSH2_series.Elements.Clear();
                EndometrialCaRiskWithMSH6_series.Elements.Clear();
                EndometrialCaRiskNoMut_series.Elements.Clear();


                ColonCaRisk_series.Name = "MMRPRO Colon Cancer Risk";
                ColonCaRiskWithMLH1_series.Name = "MMRPRO Colon Cancer Risk w/ MLH1 Mutation";
                ColonCaRiskWithMSH2_series.Name = "MMRPRO Colon Cancer Risk w/ MSH2 Mutation";
                ColonCaRiskWithMSH6_series.Name = "MMRPRO Colon Cancer Risk w/ MSH6 Mutation";
                ColonCaRiskNoMut_series.Name = "Baseline Colon Cancer Risk w/ No Mutation";

                /*****************************************/

                EndometrialCaRisk_series.Name = "MMRPRO Endometrial Cancer Risk";
                EndometrialCaRiskWithMLH1_series.Name = "MMRPRO Endometrial Cancer Risk w/ MLH1 Mutation";
                EndometrialCaRiskWithMSH2_series.Name = "MMRPRO Endometrial Cancer Risk w/ MSH2 Mutation";
                EndometrialCaRiskWithMSH6_series.Name = "MMRPRO Endometrial Cancer Risk w/ MSH6 Mutation";
                EndometrialCaRiskNoMut_series.Name = "Baseline Endometrial Cancer Risk w/ No Mutation";

                int proband_age = -1;
                int.TryParse(proband.age, out proband_age);
                Element origin = new Element();
                origin.XValue = proband_age;
                origin.YValue = 0;

                ColonCaRisk_series.Elements.Add(origin);
                ColonCaRiskWithMLH1_series.Elements.Add(origin);
                ColonCaRiskWithMSH2_series.Elements.Add(origin);
                ColonCaRiskWithMSH6_series.Elements.Add(origin);
                ColonCaRiskNoMut_series.Elements.Add(origin);

                EndometrialCaRisk_series.Elements.Add(origin);
                EndometrialCaRiskWithMLH1_series.Elements.Add(origin);
                EndometrialCaRiskWithMSH2_series.Elements.Add(origin);
                EndometrialCaRiskWithMSH6_series.Elements.Add(origin);
                EndometrialCaRiskNoMut_series.Elements.Add(origin);

                int current_age = 0;
                foreach (MMRproCancerRiskByAge score in proband.RP.MmrproCancerRiskList)
                {
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_ColonCaRisk > 0)
                    {
                        Element e = new Element();
                        e.XValue = score.MMRproCancerRiskByAge_age;
                        e.YValue = score.MMRproCancerRiskByAge_ColonCaRisk;
                        ColonCaRisk_series.Elements.Add(e);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_ColonCaRiskWithMLH1 > 0)
                    {
                        Element e2 = new Element();
                        e2.XValue = score.MMRproCancerRiskByAge_age;
                        e2.YValue = score.MMRproCancerRiskByAge_ColonCaRiskWithMLH1;
                        ColonCaRiskWithMLH1_series.Elements.Add(e2);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_ColonCaRiskWithMSH2 > 0)
                    {
                        Element e3 = new Element();
                        e3.XValue = score.MMRproCancerRiskByAge_age;
                        e3.YValue = score.MMRproCancerRiskByAge_ColonCaRiskWithMSH2;
                        ColonCaRiskWithMSH2_series.Elements.Add(e3);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_ColonCaRiskWithMSH6 > 0)
                    {
                        Element e4 = new Element();
                        e4.XValue = score.MMRproCancerRiskByAge_age;
                        e4.YValue = score.MMRproCancerRiskByAge_ColonCaRiskWithMSH6;
                        ColonCaRiskWithMSH6_series.Elements.Add(e4);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_ColonCaRiskNoMut > 0)
                    {
                        Element e5 = new Element();
                        e5.XValue = score.MMRproCancerRiskByAge_age;
                        e5.YValue = score.MMRproCancerRiskByAge_ColonCaRiskNoMut;
                        ColonCaRiskNoMut_series.Elements.Add(e5);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_EndometrialCaRisk > 0)
                    {
                        Element e7 = new Element();
                        e7.XValue = score.MMRproCancerRiskByAge_age;
                        e7.YValue = score.MMRproCancerRiskByAge_EndometrialCaRisk;
                        EndometrialCaRisk_series.Elements.Add(e7);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_EndometrialCaRiskWithMLH1 > 0)
                    {
                        Element e8 = new Element();
                        e8.XValue = score.MMRproCancerRiskByAge_age;
                        e8.YValue = score.MMRproCancerRiskByAge_EndometrialCaRiskWithMLH1;
                        EndometrialCaRiskWithMLH1_series.Elements.Add(e8);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH2 > 0)
                    {
                        Element e9 = new Element();
                        e9.XValue = score.MMRproCancerRiskByAge_age;
                        e9.YValue = score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH2;
                        EndometrialCaRiskWithMSH2_series.Elements.Add(e9);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH6 > 0)
                    {
                        Element e10 = new Element();
                        e10.XValue = score.MMRproCancerRiskByAge_age;
                        e10.YValue = score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH6;
                        EndometrialCaRiskWithMSH6_series.Elements.Add(e10);
                    }
                    if (score.MMRproCancerRiskByAge_age > proband_age && score.MMRproCancerRiskByAge_EndometrialCaRiskNoMut > 0)
                    {
                        Element e11 = new Element();
                        e11.XValue = score.MMRproCancerRiskByAge_age;
                        e11.YValue = score.MMRproCancerRiskByAge_EndometrialCaRiskNoMut;
                        EndometrialCaRiskNoMut_series.Elements.Add(e11);
                    }
                    if (proband_age > 0)
                    {
                        if (score.MMRproCancerRiskByAge_age == proband_age + 5)
                        {
                            MLH1PosFiveYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMLH1);
                            MSH2PosFiveYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMSH2);
                            MSH6PosFiveYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMSH6);

                            MLH1PosFiveYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMLH1);
                            MSH2PosFiveYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH2);
                            MSH6PosFiveYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH6);

                            label13.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskNoMut);
                            label53.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRisk);

                            label11.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRisk);
                            label56.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskNoMut);
                        }
                    }
                    if (score.MMRproCancerRiskByAge_age > current_age)
                    {
                        MLH1PosLifetimeYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMLH1);
                        MSH2PosLifetimeYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMSH2);
                        MSH6PosLifetimeYearColon = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskWithMSH6);

                        MLH1PosLifetimeYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMLH1);
                        MSH2PosLifetimeYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH2);
                        MSH6PosLifetimeYearEndo = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskWithMSH6);

                        label14.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRisk); ;
                        label57.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRisk); ;

                        label20.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_ColonCaRiskNoMut);
                        label61.Text = NullDoubleToRoundedString(score.MMRproCancerRiskByAge_EndometrialCaRiskNoMut);

                        current_age = score.MMRproCancerRiskByAge_age;
                    }
                }

                label15.Text = MLH1PosLifetimeYearColon;
                label12.Text = MLH1PosFiveYearColon;
                label55.Text = MLH1PosFiveYearEndo;
                label58.Text = MLH1PosLifetimeYearEndo;


                ColonCaRisk_series.DefaultElement.Color = Color.Black;
                EndometrialCaRisk_series.DefaultElement.Color = Color.Black;

                ColonCaRiskNoMut_series.DefaultElement.Color = Color.Green;
                EndometrialCaRiskNoMut_series.DefaultElement.Color = Color.Green;

                ColonCaRiskWithMLH1_series.DefaultElement.Color = Color.Red;
                ColonCaRiskWithMSH2_series.DefaultElement.Color = Color.Red;
                ColonCaRiskWithMSH6_series.DefaultElement.Color = Color.Red;
                EndometrialCaRiskWithMLH1_series.DefaultElement.Color = Color.Red;
                EndometrialCaRiskWithMSH2_series.DefaultElement.Color = Color.Red;
                EndometrialCaRiskWithMSH6_series.DefaultElement.Color = Color.Red;


                chart1.SeriesCollection.Add(ColonCaRiskWithMLH1_series);
                chart1.SeriesCollection.Add(ColonCaRiskNoMut_series);
                chart1.SeriesCollection.Add(ColonCaRisk_series);
                chart1.RefreshChart();

                chart2.SeriesCollection.Add(EndometrialCaRiskWithMLH1_series);
                chart2.SeriesCollection.Add(EndometrialCaRiskNoMut_series);
                chart2.SeriesCollection.Add(EndometrialCaRisk_series);
                chart2.RefreshChart();
            }
        }
        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string gene = ((ComboBox)sender).SelectedItem.ToString(); ;

                if (sender == comboBox3)
                    comboBox4.SelectedItem = gene;
                else
                    comboBox3.SelectedItem = gene;

                if (string.IsNullOrEmpty(gene) == false)
                {
                    switch (gene)
                    {
                        case "MLH1":
                            if (!chart1.SeriesCollection.Contains(ColonCaRiskWithMLH1_series))
                                chart1.SeriesCollection.Add(ColonCaRiskWithMLH1_series);
                            if (!chart2.SeriesCollection.Contains(EndometrialCaRiskWithMLH1_series))
                                chart2.SeriesCollection.Add(EndometrialCaRiskWithMLH1_series);

                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMSH2_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMSH2_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH2_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMSH2_series);

                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMSH6_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMSH6_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH6_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMSH6_series);

                            label4.Text = "MLH1 +";

                            label15.Text = MLH1PosLifetimeYearColon;
                            label12.Text = MLH1PosFiveYearColon;
                            label58.Text = MLH1PosLifetimeYearEndo;
                            label55.Text = MLH1PosFiveYearEndo;

                            break;
                        case "MSH2":
                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMLH1_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMLH1_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMLH1_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMLH1_series);

                            if (!chart1.SeriesCollection.Contains(ColonCaRiskWithMSH2_series))
                                chart1.SeriesCollection.Add(ColonCaRiskWithMSH2_series);
                            if (!chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH2_series))
                                chart2.SeriesCollection.Add(EndometrialCaRiskWithMSH2_series);

                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMSH6_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMSH6_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH6_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMSH6_series);

                            label4.Text = "MSH2 +";

                            label15.Text = MSH2PosLifetimeYearColon;
                            label12.Text = MSH2PosFiveYearColon;
                            label58.Text = MSH2PosLifetimeYearEndo;
                            label55.Text = MSH2PosFiveYearEndo;

                            break;
                        case "MSH6":
                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMLH1_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMLH1_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMLH1_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMLH1_series);

                            if (chart1.SeriesCollection.Contains(ColonCaRiskWithMSH2_series))
                                chart1.SeriesCollection.Remove(ColonCaRiskWithMSH2_series);
                            if (chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH2_series))
                                chart2.SeriesCollection.Remove(EndometrialCaRiskWithMSH2_series);

                            if (!chart1.SeriesCollection.Contains(ColonCaRiskWithMSH6_series))
                                chart1.SeriesCollection.Add(ColonCaRiskWithMSH6_series);
                            if (!chart2.SeriesCollection.Contains(EndometrialCaRiskWithMSH6_series))
                                chart2.SeriesCollection.Add(EndometrialCaRiskWithMSH6_series);

                            label4.Text = "MSH6 +";

                            label15.Text = MSH6PosLifetimeYearColon;
                            label12.Text = MSH6PosFiveYearColon;
                            label58.Text = MSH6PosLifetimeYearEndo;
                            label55.Text = MSH6PosFiveYearEndo;

                            break;
                    }
                    label45.Text = label4.Text;
                    chart1.RefreshChart();
                    chart2.RefreshChart();
                }
            }
        }

        private void ColoEndoCancerRiskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

    }
}

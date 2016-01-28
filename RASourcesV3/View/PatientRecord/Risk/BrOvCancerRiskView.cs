using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Risk;
using dotnetCHARTING.WinForms;
using System.Threading;
using System.Reflection;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class BrOvCancerRiskView : HraView
    {
        private Patient proband;

        Series breast_series = new Series();
        Series breast_series_Brca1 = new Series();
        Series breast_series_Brca2 = new Series();
        Series breast_series_Brca1And2 = new Series();
        Series breast_series_NoMut = new Series();

        Series ovarian_series = new Series();
        Series ovarian_series_Brca1 = new Series();
        Series ovarian_series_Brca2 = new Series();
        Series ovarian_series_Brca1And2 = new Series();
        Series ovarian_series_NoMut = new Series();

        string Brca1PosFiveYearBreast = "N/A";
        string Brca2PosFiveYearBreast = "N/A";
        string Brca1and2PosFiveYearBreast = "N/A";
        string Brca1PosLifetimeBreast = "N/A";
        string Brca2PosLifetimeBreast = "N/A";
        string Brca1and2PosLifetimeBreast = "N/A";

        string Brca1PosFiveYearOvarian = "N/A";
        string Brca2PosFiveYearOvarian = "N/A";
        string Brca1and2PosFiveYearOvarian = "N/A";
        string Brca1PosLifetimeOvarian = "N/A";
        string Brca2PosLifetimeOvarian = "N/A";
        string Brca1and2PosLifetimeOvarian = "N/A";

        //EventWaitHandle _waitHandle = null;
        //DoneLoadingTracker dlt = null;

        public BrOvCancerRiskView()
        {

            InitializeComponent();
            SummaryChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            SummaryChart.DefaultSeries.Type = SeriesType.Spline;
            SummaryChart.DefaultSeries.Line.Width = 5;
            SummaryChart.ChartArea.LegendBox.Visible = false;
            SummaryChart.ChartArea.XAxis.Label.Text = "Age";
            SummaryChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            BrcaProBreastChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            BrcaProBreastChart.DefaultSeries.Type = SeriesType.Spline;
            BrcaProBreastChart.DefaultSeries.Line.Width = 5;
            BrcaProBreastChart.ChartArea.LegendBox.Visible = false;
            BrcaProBreastChart.ChartArea.XAxis.Label.Text = "Age";
            BrcaProBreastChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            BrcaProOvarianChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            BrcaProOvarianChart.DefaultSeries.Type = SeriesType.Spline;
            BrcaProOvarianChart.DefaultSeries.Line.Width = 5;
            BrcaProOvarianChart.ChartArea.LegendBox.Visible = false;
            BrcaProOvarianChart.ChartArea.XAxis.Label.Text = "Age";
            BrcaProOvarianChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            GailChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            GailChart.DefaultSeries.Type = SeriesType.Spline;
            GailChart.DefaultSeries.Line.Width = 5;
            GailChart.ChartArea.LegendBox.Visible = false;
            GailChart.ChartArea.XAxis.Label.Text = "Age";
            GailChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            ClausChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            ClausChart.DefaultSeries.Type = SeriesType.Spline;
            ClausChart.DefaultSeries.Line.Width = 5;
            ClausChart.ChartArea.LegendBox.Visible = false;
            ClausChart.ChartArea.XAxis.Label.Text = "Age";
            ClausChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            TcChart.DefaultElement.Marker.Type = ElementMarkerType.None;
            TcChart.DefaultSeries.Type = SeriesType.Spline;
            TcChart.DefaultSeries.Line.Width = 5;
            TcChart.ChartArea.LegendBox.Visible = false;
            TcChart.ChartArea.XAxis.Label.Text = "Age";
            TcChart.ChartArea.YAxis.Label.Text = "Risk Of Cancer (%)";

            SummaryChart.Visible = true;
            summaryNAlabel.Visible = false;
        }

        //blocking constructor which waits 'till all data is loaded on form
        public BrOvCancerRiskView(Patient patient)
            : this()
        {
            InitPatient(patient);
        }

        private void CancerRiskView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient +=
                        new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
                InitPatient(null);
            }
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitPatient(null);
        }

        /**************************************************************************************************/
        private void InitPatient(Patient patient)
        {
            if (patient == null)
                proband = SessionManager.Instance.GetActivePatient();
            else
                proband = patient;

            if (proband.HraState == HraObject.States.Ready)
            {
                int min = 0;
                int.TryParse(proband.age, out min);
                SummaryChart.ChartArea.XAxis.Minimum = min;
                BrcaProBreastChart.ChartArea.XAxis.Minimum = min;
                BrcaProOvarianChart.ChartArea.XAxis.Minimum = min;
                GailChart.ChartArea.XAxis.Minimum = min;
                ClausChart.ChartArea.XAxis.Minimum = min;
                TcChart.ChartArea.XAxis.Minimum = min;
            }

            proband.RP.BracproCancerRisk.AddHandlersWithLoad(BracproCancerRiskChanged, BracproCancerRiskLoaded, BracproCancerRiskItemChanged);
            proband.RP.GailModel.AddHandlersWithLoad(GailModelChanged, GailModelLoaded, GailModelItemChanged);
            proband.RP.ClausModel.AddHandlersWithLoad(ClausModelChanged, ClausModelLoaded, ClausModelItemChanged);
            proband.RP.AddHandlersWithLoad(null, RpLoaded, null);
            proband.RP.TyrerCuzickModel.AddHandlersWithLoad(TcChanged, TcLoaded, TclItemChanged);
            proband.RP.TyrerCuzickModel_v7.AddHandlersWithLoad(TcChanged_v7, TcLoaded_v7, TclItemChanged_v7);

        }


        /**************************************************************************************************/
        delegate void RpLoadedCallback(object sender, RunWorkerCompletedEventArgs e);
        private void RpLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                RpLoadedCallback rmc = new RpLoadedCallback(RpLoaded);
                object[] args = new object[2];
                args[0] = sender;
                args[1] = e;
                this.Invoke(rmc, args);
            }
            else
            {
                BreastClausFiveYear.Text = "";
                BreastClausLifetime.Text = "";
                BreastTcFiveYear.Text = "";
                BreastTcLifetime.Text = "";
                BreastTcV7FiveYear.Text = "";
                BreastTcV7Lifetime.Text = "";
                OvaryBrcaproFiveYear.Text = "";
                OvaryBrcaproLifetime.Text = "";
                gailFiveYearPatientLabel.Text = "";
                GailLifetimePatientLabel.Text = "";
                BreastBrcaproFiveYear.Text = "";
                BreastBrcaproLifetime.Text = "";

                BreastBrcaproFiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_5Year_Breast);
                label50.Text = BreastBrcaproFiveYear.Text;

                BreastBrcaproLifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_Lifetime_Breast);
                label54.Text = BreastBrcaproLifetime.Text;

                label28.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_1_2_Mut_Prob);
                label35.Text = label28.Text;
                label19.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_5Year_Ovary);
                label22.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_Lifetime_Ovary);



                if (proband.gender == "Male")
                {

                }
                else
                {
                    OvaryBrcaproFiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_5Year_Ovary);
                    OvaryBrcaproLifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_Lifetime_Ovary);

                    BreastGailFiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_Gail_5Year_Breast);
                    BreastGailLifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_Gail_Lifetime_Breast);
                    gailFiveYearPatientLabel.Text = BreastGailFiveYear.Text;
                    GailLifetimePatientLabel.Text = BreastGailLifetime.Text;

                    clausFiveYearPatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_Claus_5Year_Breast);
                    clausLifetimePatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_Claus_Lifetime_Breast);
                    BreastClausFiveYear.Text = clausFiveYearPatientLabel.Text;
                    BreastClausLifetime.Text = clausLifetimePatientLabel.Text;


                    BreastTcFiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_5Year_Breast);
                    BreastTcLifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_Lifetime_Breast);

                    BreastTcV7FiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_v7_5Year_Breast);
                    BreastTcV7Lifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_v7_Lifetime_Breast);

                    OvaryBrcaproFiveYear.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_5Year_Ovary);
                    OvaryBrcaproLifetime.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_BrcaPro_Lifetime_Ovary);

                    tcFiveYearPatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_5Year_Breast);
                    tcLifetimePatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_Lifetime_Breast);
                    labelTCProbMutation.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_Brca_1_2);

                    tc7FiveYearPatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_v7_5Year_Breast);
                    tc7LifetimePatientLabel.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_v7_Lifetime_Breast);
                    labelTC7ProbMutation.Text = NullDoubleToRoundedString(proband.RP.RiskProfile_TyrerCuzick_v7_Brca_1_2);
                }
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
        private void ClausModelItemChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void ClausModelChanged(HraListChangedEventArgs e)
        {

        }

        public void SetOvarianRiskVisibility(bool vis)
        {
            tableLayoutPanel2.Visible = vis;
            label13.Visible = vis;
        }

        /**************************************************************************************************/
        delegate void ClausModelLoadedCallback(HraListLoadedEventArgs list_e);
        private void ClausModelLoaded(HraListLoadedEventArgs list_e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                ClausModelLoadedCallback rmc = new ClausModelLoadedCallback(ClausModelLoaded);
                object[] args = new object[1];
                args[0] = list_e;
                this.Invoke(rmc, args);
            }
            else
            {
                //ClausChart.SeriesCollection.Clear();
                //BreastClausFiveYear.Text = "";
                //BreastClausLifetime.Text = "";

                /*********/
                Series proband_series = new Series();
                proband_series.Name = "Claus Model Breast Cancer Risk";
                Series summary_series = new Series();
                summary_series.Name = "Claus Model Breast Cancer Risk";

                Series doomed = null;
                foreach (Series s in SummaryChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    SummaryChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in ClausChart.SeriesCollection)
                {
                    if (s.Name == proband_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    ClausChart.SeriesCollection.Remove(doomed);

                string clausna = null;

                foreach (ClincalObservation co in proband.PMH.Observations)
                {
                    if (co.ClinicalObservation_disease.ToLower().StartsWith("breast cancer") ||
                        co.ClinicalObservation_disease.ToLower().StartsWith("lcis") ||
                        co.ClinicalObservation_disease.ToLower().Contains("dcis"))
                    {
                        clausna = "Claus is N/A because the patient has " + co.ClinicalObservation_disease;
                    }

                }
                if (proband.gender == "Male")
                    clausna = "Claus is N/A because the patient is male";

                if (string.IsNullOrEmpty(clausna))
                {
                    ClausChart.Visible = true;
                    tableLayoutPanel7.Visible = true;
                    tableLayoutPanel8.Visible = true;
                    Clausna.Visible = false;
                    BreastClausFiveYear.Visible = true;
                    BreastClausLifetime.Visible = true;
                }
                else
                {
                    Clausna.Text = clausna;
                    ClausChart.Visible = false;
                    tableLayoutPanel7.Visible = false;
                    tableLayoutPanel8.Visible = false;
                    Clausna.Visible = true;
                    BreastClausFiveYear.Visible = false;
                    BreastClausLifetime.Visible = false;
                    return;
                }
                summaryChartVisible(clausna);

                int currentAge = 0;
                foreach (ClausRiskByAge score in proband.RP.ClausModel)
                {
                    Element e = new Element();
                    e.XValue = score.ClausRiskByAge_age;
                    e.YValue = score.ClausRiskByAge_BreastCaRisk;
                    proband_series.Elements.Add(e);
                    summary_series.Elements.Add(e);
                    //if ((score.ClausRiskByAge_age - 5).ToString() == proband.age)
                    //{
                    //    clausFiveYearPatientLabel.Text = score.ClausRiskByAge_BreastCaRisk.ToString();
                    //}

                    //if (score.ClausRiskByAge_age > currentAge)
                    //{
                    //    currentAge = score.ClausRiskByAge_age;
                    //    clausLifetimePatientLabel.Text = Math.Round(score.ClausRiskByAge_BreastCaRisk,1).ToString();
                    //}
                }
                proband_series.DefaultElement.Color = Color.Black;
                summary_series.DefaultElement.Color = Color.Orange;


                SummaryChart.SeriesCollection.Add(summary_series);

                ClausChart.SeriesCollection.Add(proband_series);
                label21.Text = proband.RP.ClausModel.RiskFactors.ClausRiskFactors_Claus_Table;
                relOneLabel.Text = proband.RP.ClausModel.RiskFactors.ClausRiskFactors_RelOne;
                relTwoLabel.Text = proband.RP.ClausModel.RiskFactors.ClausRiskFactors_RelTwo;

                if (relOneLabel.Text == "NA")
                    relOneLabel.Text = "";

                if (relTwoLabel.Text == "NA")
                    relTwoLabel.Text = "";

            }
        }
        /**************************************************************************************************/
        private void GailModelItemChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void GailModelChanged(HraListChangedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void TclItemChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void TcChanged(HraListChangedEventArgs e)
        {

        }
        /**************************************************************************************************/
        private void TclItemChanged_v7(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void TcChanged_v7(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        delegate void TcLoaded_v7Callback(HraListLoadedEventArgs list_e);
        private void TcLoaded_v7(HraListLoadedEventArgs list_e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                TcLoaded_v7Callback rmc = new TcLoaded_v7Callback(TcLoaded_v7);
                object[] args = new object[1];
                args[0] = list_e;
                this.Invoke(rmc, args);
            }
            else
            {
                //TcChart.SeriesCollection.Clear();
                //BreastTcFiveYear.Text = "";
                //BreastTcLifetime.Text = "";

                /*********/
                Series proband_series = new Series();
                Series summary_series = new Series();
                Series population_series = new Series();
                summary_series.DefaultElement.Color = Color.CornflowerBlue;
                summary_series.Name = "T-C v7 Risk of Breast Cancer";
                proband_series.Name = "T-C v7 Risk of Breast Cancer";
                population_series.Name = "T-C v7 Population Risk of Breast Cancer";

                Series doomed = null;
                foreach (Series s in SummaryChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    SummaryChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in TcChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    TcChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in TcChart.SeriesCollection)
                {
                    if (s.Name == population_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    TcChart.SeriesCollection.Remove(doomed);

                if (proband.gender == "Male")
                {
                    BreastTcV7FiveYear.Visible = false;
                    BreastTcV7Lifetime.Visible = false;
                    return;
                }
                else
                {
                    BreastTcV7FiveYear.Visible = true;
                    BreastTcV7Lifetime.Visible = true;
                }



                /*********/

                int current_age = 0;

                population_series.DefaultElement.Color = Color.LightGreen;
                proband_series.DefaultElement.Color = Color.DimGray;

                int proband_age = -1;
                if (int.TryParse(proband.age, out proband_age))
                {
                    Element e = new Element();
                    e.XValue = proband_age;
                    e.YValue = 0;
                    proband_series.Elements.Add(e);
                    summary_series.Elements.Add(e);
                    population_series.Elements.Add(e);
                }
                foreach (TcRiskByAge score in proband.RP.TyrerCuzickModel_v7)
                {
                    if (score.age > proband_age && score.BreastCaRisk > 0)
                    {
                        Element e = new Element();
                        e.XValue = score.age;
                        e.YValue = score.BreastCaRisk;
                        proband_series.Elements.Add(e);
                        summary_series.Elements.Add(e);
                    }
                    if (score.age > proband_age && score.PopulationCaRisk > 0)
                    {
                        Element e2 = new Element();
                        e2.XValue = score.age;
                        e2.YValue = score.PopulationCaRisk;
                        population_series.Elements.Add(e2);
                    }
                    if (score.description == "5 Year")
                    {
                        tc7FiveYearPopulationLabel.Text = Math.Round(score.PopulationCaRisk, 1).ToString() + "%";
                    }
                    if (score.age > current_age)
                    {
                        tc7LifetimePopulationLabel.Text = Math.Round(score.PopulationCaRisk, 1).ToString() + "%";
                        current_age = score.age;
                    }
                }

                TcChart.SeriesCollection.Add(population_series);
                TcChart.SeriesCollection.Add(proband_series);
                TcChart.RefreshChart();


                SummaryChart.SeriesCollection.Add(summary_series);
                SummaryChart.RefreshChart();
            }
        }
        /**************************************************************************************************/
        delegate void TcLoadedCallback(HraListLoadedEventArgs list_e);
        private void TcLoaded(HraListLoadedEventArgs list_e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                TcLoadedCallback rmc = new TcLoadedCallback(TcLoaded);
                object[] args = new object[1];
                args[0] = list_e;
                this.Invoke(rmc, args);
            }
            else
            {
                //TcChart.SeriesCollection.Clear();
                //BreastTcFiveYear.Text = "";
                //BreastTcLifetime.Text = "";

                /*********/
                Series proband_series = new Series();
                Series summary_series = new Series();
                Series population_series = new Series();

                summary_series.DefaultElement.Color = Color.Purple;
                summary_series.Name = "T-C Risk of Breast Cancer";
                proband_series.Name = "T-C Risk of Breast Cancer";
                population_series.Name = "T-C Population Risk of Breast Cancer";

                Series doomed = null;
                foreach (Series s in SummaryChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    SummaryChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in TcChart.SeriesCollection)
                {
                    if (s.Name == proband_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    TcChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in TcChart.SeriesCollection)
                {
                    if (s.Name == population_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    TcChart.SeriesCollection.Remove(doomed);




                string tcna = null;

                foreach (ClincalObservation co in proband.PMH.Observations)
                {
                    if (co.ClinicalObservation_disease.ToLower().Contains("breast cancer") ||
                        //co.ClinicalObservation_disease.ToLower().StartsWith("lcis") ||
                        co.ClinicalObservation_disease.ToLower().Contains("dcis"))
                    {
                        tcna = "TC is N/A because the patient has " + co.ClinicalObservation_disease;
                    }

                }
                if (proband.gender == "Male")
                    tcna = "TC is N/A because the patient is male";




                if (proband.gender == "Male" || proband.HasBreastCancer())
                {
                    TcChart.Visible = false;
                    tableLayoutPanel9.Visible = false;
                    tableLayoutPanel13.Visible = false;
                    TcNaLabel.Visible = true;
                    version6Label.Visible = false;
                    version7Label.Visible = false;
                    BreastTcFiveYear.Visible = false;
                    BreastTcLifetime.Visible = false;
                    TcNaLabel.Text = tcna;
                    return;
                }
                else
                {
                    TcChart.Visible = true;
                    tableLayoutPanel9.Visible = true;
                    tableLayoutPanel13.Visible = true;
                    TcNaLabel.Visible = false;
                    version6Label.Visible = true;
                    version7Label.Visible = true;
                    BreastTcFiveYear.Visible = true;
                    BreastTcLifetime.Visible = true;
                }
                summaryChartVisible(tcna);


                /*********/

                int current_age = 0;

                population_series.DefaultElement.Color = Color.Green;
                proband_series.DefaultElement.Color = Color.Black;

                int proband_age = -1;
                if (int.TryParse(proband.age, out proband_age))
                {
                    Element e = new Element();
                    e.XValue = proband_age;
                    e.YValue = 0;
                    proband_series.Elements.Add(e);
                    summary_series.Elements.Add(e);
                    population_series.Elements.Add(e);
                }
                foreach (TcRiskByAge score in proband.RP.TyrerCuzickModel)
                {
                    if (score.age > proband_age && score.BreastCaRisk > 0)
                    {
                        Element e = new Element();
                        e.XValue = score.age;
                        e.YValue = score.BreastCaRisk;
                        proband_series.Elements.Add(e);
                        summary_series.Elements.Add(e);
                    }
                    if (score.age > proband_age && score.PopulationCaRisk > 0)
                    {
                        Element e2 = new Element();
                        e2.XValue = score.age;
                        e2.YValue = score.PopulationCaRisk;
                        population_series.Elements.Add(e2);
                    }
                    if (score.description == "5 Year")
                    {
                        tcFiveYearPopulationLabel.Text = Math.Round(score.PopulationCaRisk, 1).ToString() + "%";
                    }
                    if (score.age > current_age)
                    {
                        tcLifetimePopulationLabel.Text = Math.Round(score.PopulationCaRisk, 1).ToString() + "%";
                        current_age = score.age;
                    }
                }

                TcChart.SeriesCollection.Add(population_series);
                TcChart.SeriesCollection.Add(proband_series);
                TcChart.RefreshChart();


                SummaryChart.SeriesCollection.Add(summary_series);
                SummaryChart.RefreshChart();
            }
        }
        /**************************************************************************************************/
        delegate void GailModelLoadedCallback(HraListLoadedEventArgs list_e);
        private void GailModelLoaded(HraListLoadedEventArgs list_e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                GailModelLoadedCallback rmc = new GailModelLoadedCallback(GailModelLoaded);
                object[] args = new object[1];
                args[0] = list_e;
                this.Invoke(rmc, args);
            }
            else
            {
                GailChart.SeriesCollection.Clear();

                /*********/
                Series proband_series = new Series();
                proband_series.Name = "Gail Model Breast Cancer Risk";
                Series summary_series = new Series();
                summary_series.Name = "Gail Model Breast Cancer Risk";

                /*********/
                Series population_series = new Series();
                population_series.Name = "Population Breast Cancer Risk";


                Series doomed = null;
                foreach (Series s in SummaryChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    SummaryChart.SeriesCollection.Remove(doomed);

                doomed = null;
                foreach (Series s in ClausChart.SeriesCollection)
                {
                    if (s.Name == population_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    ClausChart.SeriesCollection.Remove(doomed);

                string gailna = null;

                foreach (ClincalObservation co in proband.PMH.Observations)
                {
                    if (co.ClinicalObservation_disease.ToLower().StartsWith("breast cancer") ||
                        co.ClinicalObservation_disease.ToLower().StartsWith("lcis") ||
                        co.ClinicalObservation_disease.ToLower().Contains("dcis"))
                    {
                        gailna = "Gail is N/A because the patient has " + co.ClinicalObservation_disease;
                    }

                }
                if (proband.gender == "Male")
                    gailna = "Gail is N/A because the patient is male";

                if (string.IsNullOrEmpty(gailna))
                {
                    GailChart.Visible = true;
                    tableLayoutPanel5.Visible = true;
                    tableLayoutPanel6.Visible = true;
                    GailNaLabel.Visible = false;


                }
                else
                {
                    BreastGailFiveYear.Text = "";
                    BreastGailLifetime.Text = "";
                    GailNaLabel.Text = gailna;
                    GailChart.Visible = false;
                    tableLayoutPanel5.Visible = false;
                    tableLayoutPanel6.Visible = false;
                    GailNaLabel.Visible = true;
                    BreastGailFiveYear.Text = "";
                    BreastGailLifetime.Text = "";
                    return;
                }
                summaryChartVisible(gailna);


                foreach (GailRiskByAge score in proband.RP.GailModel)
                {
                    Element e = new Element();
                    e.XValue = score.GailRiskByAge_age;
                    e.YValue = score.GailRiskByAge_BreastCaRisk;
                    proband_series.Elements.Add(e);
                    summary_series.Elements.Add(e);

                    Element e2 = new Element();
                    e2.XValue = score.GailRiskByAge_age;
                    e2.YValue = score.GailRiskByAge_PopulationCaRisk;
                    population_series.Elements.Add(e2);

                    if (score.GailRiskByAge_description == "Five Year")
                    {
                        //gailFiveYearPatientLabel.Text = score.GailRiskByAge_BreastCaRisk.ToString();
                        GailFiveYearPopulationLabel.Text = NullDoubleToRoundedString(score.GailRiskByAge_PopulationCaRisk);
                        clausFiveYearPopulationLabel.Text = NullDoubleToRoundedString(score.GailRiskByAge_PopulationCaRisk);
                    }
                    if (score.GailRiskByAge_description == "Lifetime")
                    {
                        //GailLifetimePatientLabel.Text = score.GailRiskByAge_BreastCaRisk.ToString();
                        GailLifetimePopulationLabel.Text = NullDoubleToRoundedString(score.GailRiskByAge_PopulationCaRisk);
                        clausLifetimePopulationLabel.Text = NullDoubleToRoundedString(score.GailRiskByAge_PopulationCaRisk);
                    }
                }
                population_series.DefaultElement.Color = Color.Green;
                proband_series.DefaultElement.Color = Color.Black;
                summary_series.DefaultElement.Color = Color.Blue;



                SummaryChart.SeriesCollection.Add(summary_series);

                GailChart.SeriesCollection.Add(population_series);
                GailChart.SeriesCollection.Add(proband_series);

                GailChart.RefreshChart();

                ClausChart.SeriesCollection.Add(population_series);
                ClausChart.RefreshChart();

                flbLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_firstLiveBirthAge;
                menLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_menarcheAge;
                fdrLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_firstDegreeRel;
                hadBiopLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_hadBiopsy;
                nbiopLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_numBiopsy;
                ahLabel.Text = proband.RP.GailModel.RiskFactors.GailRiskFactors_hyperPlasia;

                string raceInt = proband.RP.GailModel.RiskFactors.GailRiskFactors_race;
                if (raceInt == "1")
                    ageAndRaceLabel.Text = "Caucasian";
                else if (raceInt == "2")
                    ageAndRaceLabel.Text = "African American";
                else if (raceInt == "3")
                    ageAndRaceLabel.Text = "Hispanic";
                else
                {
                    ageAndRaceLabel.Text = "";
                }
            }
        }
        /**************************************************************************************************/
        private void BracproCancerRiskItemChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void BracproCancerRiskChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        delegate void BracproCancerRiskLoadedCallback(HraListLoadedEventArgs list_e);
        private void BracproCancerRiskLoaded(HraListLoadedEventArgs list_e)
        {
            if (Thread.CurrentThread.Name != "MainGUI")
            {
                BracproCancerRiskLoadedCallback rmc = new BracproCancerRiskLoadedCallback(BracproCancerRiskLoaded);
                object[] args = new object[1];
                args[0] = list_e;
                this.Invoke(rmc, args);
            }
            else
            {
                String ovarianNA = "";
                if (proband.HasOvarianCancer())
                {
                    ovarianNA = "N/A because the patient has Ovarian cancer";
                }
                if (proband.gender == "Male")
                {
                    ovarianNA = "N/A because the patient is male";
                }


                if (ovarianNA.Length > 0)
                {
                    BrcaProOvarianChart.Visible = false;
                    tableLayoutPanel3.Visible = false;
                    label40.Visible = false;
                    comboBox1.Visible = false;
                    ovarianCancerLabel.Visible = true;
                    ovarianCancerLabel.Text = ovarianNA;
                }
                else
                {
                    BrcaProOvarianChart.Visible = true;
                    tableLayoutPanel3.Visible = true;
                    label40.Visible = true;
                    comboBox1.Visible = true;
                    ovarianCancerLabel.Visible = false;
                }


                String breastNA = "";
                if (proband.HasBreastCancer())
                {
                    breastNA = "Breast cancer Risk is N/A because the patient has breast cancer.";

                }

                if (breastNA.Length > 0)
                {
                    BrcaProBreastChart.Visible = false;
                    tableLayoutPanel4.Visible = false;
                    label41.Visible = false;
                    comboBox2.Visible = false;
                    breastNALabel.Visible = true;
                    breastNALabel.Text = breastNA;
                }
                else
                {
                    BrcaProBreastChart.Visible = true;
                    tableLayoutPanel4.Visible = true;
                    label41.Visible = true;
                    comboBox2.Visible = true;
                    breastNALabel.Visible = false;
                }

                summaryChartVisible(breastNA);





                BrcaProBreastChart.SeriesCollection.Clear();
                BrcaProOvarianChart.SeriesCollection.Clear();

                breast_series.Elements.Clear();
                breast_series_Brca1.Elements.Clear();
                breast_series_Brca2.Elements.Clear();
                breast_series_Brca1And2.Elements.Clear();
                breast_series_NoMut.Elements.Clear();

                ovarian_series.Elements.Clear();
                ovarian_series_Brca1.Elements.Clear();
                ovarian_series_Brca2.Elements.Clear();
                ovarian_series_Brca1And2.Elements.Clear();
                ovarian_series_NoMut.Elements.Clear();

                Series summary_series = new Series();
                summary_series.Name = "BRCAPRO Breast Cancer Risk";

                breast_series.Name = "BRCAPRO Breast Cancer Risk";
                breast_series_Brca1.Name = "BRCAPRO Breast Cancer Risk w/ BRCA1 Mutation";
                breast_series_Brca2.Name = "BRCAPRO Breast Cancer Risk w/ BRCA2 Mutation";
                breast_series_Brca1And2.Name = "BRCAPRO Breast Cancer Risk w/ BRCA1 & BRCA2 Mutation";
                breast_series_NoMut.Name = "Baseline Breast Cancer Risk w/ No Mutation";

                /*****************************************/

                ovarian_series.Name = "BRCAPRO Ovarian Cancer Risk";
                ovarian_series_Brca1.Name = "BRCAPRO Ovarian Cancer Risk w/ BRCA1 Mutation";
                ovarian_series_Brca2.Name = "BRCAPRO Ovarian Cancer Risk w/ BRCA2 Mutation";
                ovarian_series_Brca1And2.Name = "BRCAPRO Ovarian Cancer Risk w/ BRCA1 & BRCA2 Mutation";
                ovarian_series_NoMut.Name = "Baseline Ovarian Cancer Risk w/ No Mutation";

                int proband_age = -1;
                int.TryParse(proband.age, out proband_age);
                int current_age = 0;

                Element origin = new Element();
                origin.XValue = proband_age;
                origin.YValue = 0;
                breast_series.Elements.Add(origin);
                summary_series.Elements.Add(origin);
                breast_series_Brca1.Elements.Add(origin);
                breast_series_Brca2.Elements.Add(origin);
                breast_series_NoMut.Elements.Add(origin);
                breast_series_Brca1And2.Elements.Add(origin);
                ovarian_series.Elements.Add(origin);
                ovarian_series_Brca1.Elements.Add(origin);
                ovarian_series_Brca2.Elements.Add(origin);
                ovarian_series_Brca1And2.Elements.Add(origin);
                ovarian_series_NoMut.Elements.Add(origin);

                foreach (BrcaProCancerRiskByAge score in proband.RP.BracproCancerRisk)
                {
                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_BreastCaRisk > 0)
                    {
                        Element e = new Element();
                        e.XValue = score.BrcaProCancerRiskByAge_age;
                        e.YValue = score.BrcaProCancerRiskByAge_BreastCaRisk;
                        breast_series.Elements.Add(e);
                        summary_series.Elements.Add(e);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1Mutation > 0)
                    {
                        Element e2 = new Element();
                        e2.XValue = score.BrcaProCancerRiskByAge_age;
                        e2.YValue = score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1Mutation;
                        breast_series_Brca1.Elements.Add(e2);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca2Mutation > 0)
                    {
                        Element e3 = new Element();
                        e3.XValue = score.BrcaProCancerRiskByAge_age;
                        e3.YValue = score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca2Mutation;
                        breast_series_Brca2.Elements.Add(e3);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_BreastCaRiskWithNoMutation > 0)
                    {
                        Element e4 = new Element();
                        e4.XValue = score.BrcaProCancerRiskByAge_age;
                        e4.YValue = score.BrcaProCancerRiskByAge_BreastCaRiskWithNoMutation;
                        breast_series_NoMut.Elements.Add(e4);
                    }
                    //Element e5 = new Element();
                    //e5.XValue = score.BrcaProCancerRiskByAge_age;
                    //e5.YValue = score.BrcaProCancerRiskByAge_BreastCaRisk * 0.15;
                    //breast_NegTest_series.Elements.Add(e5);

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1And2Mutation > 0)
                    {
                        Element e6 = new Element();
                        e6.XValue = score.BrcaProCancerRiskByAge_age;
                        e6.YValue = score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1And2Mutation;
                        breast_series_Brca1And2.Elements.Add(e6);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_OvarianCaRisk > 0)
                    {
                        Element e7 = new Element();
                        e7.XValue = score.BrcaProCancerRiskByAge_age;
                        e7.YValue = score.BrcaProCancerRiskByAge_OvarianCaRisk;
                        ovarian_series.Elements.Add(e7);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1Mutation > 0)
                    {
                        Element e8 = new Element();
                        e8.XValue = score.BrcaProCancerRiskByAge_age;
                        e8.YValue = score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1Mutation;
                        ovarian_series_Brca1.Elements.Add(e8);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca2Mutation > 0)
                    {
                        Element e9 = new Element();
                        e9.XValue = score.BrcaProCancerRiskByAge_age;
                        e9.YValue = score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca2Mutation;
                        ovarian_series_Brca2.Elements.Add(e9);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1And2Mutation > 0)
                    {
                        Element e10 = new Element();
                        e10.XValue = score.BrcaProCancerRiskByAge_age;
                        e10.YValue = score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1And2Mutation;
                        ovarian_series_Brca1And2.Elements.Add(e10);
                    }

                    if (score.age > proband_age && score.BrcaProCancerRiskByAge_OvarianCaRiskWithNoMutation > 0)
                    {
                        Element e11 = new Element();
                        e11.XValue = score.BrcaProCancerRiskByAge_age;
                        e11.YValue = score.BrcaProCancerRiskByAge_OvarianCaRiskWithNoMutation;
                        ovarian_series_NoMut.Elements.Add(e11);
                    }

                    if (proband_age > 0)
                    {
                        if (score.BrcaProCancerRiskByAge_age == proband_age + 5)
                        {
                            Brca1PosFiveYearBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1Mutation);
                            Brca2PosFiveYearBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca2Mutation);
                            Brca1and2PosFiveYearBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1And2Mutation);

                            Brca1PosFiveYearOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1Mutation);
                            Brca2PosFiveYearOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca2Mutation);
                            Brca1and2PosFiveYearOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1And2Mutation);

                            BreastBrcaNegFiveyearLabel.Text = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithNoMutation);
                            OvarianBrcaNegFiveyearLabel.Text = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithNoMutation);
                        }
                    }
                    if (score.BrcaProCancerRiskByAge_age > current_age)
                    {
                        Brca1PosLifetimeBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1Mutation);
                        Brca2PosLifetimeBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca2Mutation);
                        Brca1and2PosLifetimeBreast = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithBrca1And2Mutation);

                        Brca1PosLifetimeOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1Mutation);
                        Brca2PosLifetimeOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca2Mutation);
                        Brca1and2PosLifetimeOvarian = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1And2Mutation);

                        BreastBrcaNegLifetimeLabel.Text = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_BreastCaRiskWithNoMutation); ;
                        OvarianBrcaNegLifetimeLabel.Text = NullDoubleToRoundedString(score.BrcaProCancerRiskByAge_OvarianCaRiskWithNoMutation); ;

                        current_age = score.BrcaProCancerRiskByAge_age;
                    }
                }

                BreastBrcaPosLifetimeLabel.Text = Brca1PosLifetimeBreast;
                BreastBrcaPosFiveyearLabel.Text = Brca1PosFiveYearBreast;
                OvarianBrcaPosLifetimeLabel.Text = Brca1PosLifetimeOvarian;
                OvarianBrcaPosFiveyearLabel.Text = Brca1PosFiveYearOvarian;


                breast_series.DefaultElement.Color = Color.Black;
                ovarian_series.DefaultElement.Color = Color.Black;
                breast_series_NoMut.DefaultElement.Color = Color.Green;
                ovarian_series_NoMut.DefaultElement.Color = Color.Green;

                breast_series_Brca1.DefaultElement.Color = Color.Red;
                breast_series_Brca2.DefaultElement.Color = Color.Red;
                breast_series_Brca1And2.DefaultElement.Color = Color.Red;
                ovarian_series_Brca1.DefaultElement.Color = Color.Red;
                ovarian_series_Brca2.DefaultElement.Color = Color.Red;
                ovarian_series_Brca1And2.DefaultElement.Color = Color.Red;

                summary_series.DefaultElement.Color = Color.Lime;

                Series doomed = null;
                foreach (Series s in SummaryChart.SeriesCollection)
                {
                    if (s.Name == summary_series.Name)
                    {
                        doomed = s;
                        break;
                    }
                }
                if (doomed != null)
                    SummaryChart.SeriesCollection.Remove(doomed);


                if (breastNA.Length == 0)
                {
                    SummaryChart.SeriesCollection.Add(summary_series);
                    SummaryChart.RefreshChart();
                }



                BrcaProBreastChart.SeriesCollection.Add(breast_series_Brca1);
                BrcaProBreastChart.SeriesCollection.Add(breast_series_NoMut);
                BrcaProBreastChart.SeriesCollection.Add(breast_series);
                BrcaProBreastChart.RefreshChart();


                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series_Brca1);
                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series_NoMut);
                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series);
                BrcaProOvarianChart.RefreshChart();

            }
        }
        private void CancerRiskView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }


        private void comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string gene = ((ComboBox)sender).SelectedItem.ToString(); ;

                if (sender == comboBox1)
                    comboBox2.SelectedItem = gene;
                else
                    comboBox1.SelectedItem = gene;

                if (string.IsNullOrEmpty(gene) == false)
                {
                    switch (gene)
                    {
                        case "BRCA1":
                            if (!BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1))
                                BrcaProBreastChart.SeriesCollection.Add(breast_series_Brca1);
                            if (!BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1))
                                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series_Brca1);

                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca2))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca2);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca2))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca2);

                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1And2))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca1And2);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1And2))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca1And2);

                            label7.Text = "BRCA1 +";

                            BreastBrcaPosLifetimeLabel.Text = Brca1PosLifetimeBreast;
                            BreastBrcaPosFiveyearLabel.Text = Brca1PosFiveYearBreast;
                            OvarianBrcaPosLifetimeLabel.Text = Brca1PosLifetimeOvarian;
                            OvarianBrcaPosFiveyearLabel.Text = Brca1PosFiveYearOvarian;

                            break;
                        case "BRCA2":
                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca1);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca1);

                            if (!BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca2))
                                BrcaProBreastChart.SeriesCollection.Add(breast_series_Brca2);
                            if (!BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca2))
                                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series_Brca2);

                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1And2))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca1And2);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1And2))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca1And2);

                            label7.Text = "BRCA2 +";

                            BreastBrcaPosLifetimeLabel.Text = Brca2PosLifetimeBreast;
                            BreastBrcaPosFiveyearLabel.Text = Brca2PosFiveYearBreast;
                            OvarianBrcaPosLifetimeLabel.Text = Brca2PosLifetimeOvarian;
                            OvarianBrcaPosFiveyearLabel.Text = Brca2PosFiveYearOvarian;

                            break;
                        case "BRCA1and2":
                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca1);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca1);

                            if (BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca2))
                                BrcaProBreastChart.SeriesCollection.Remove(breast_series_Brca2);
                            if (BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca2))
                                BrcaProOvarianChart.SeriesCollection.Remove(ovarian_series_Brca2);

                            if (!BrcaProBreastChart.SeriesCollection.Contains(breast_series_Brca1And2))
                                BrcaProBreastChart.SeriesCollection.Add(breast_series_Brca1And2);
                            if (!BrcaProOvarianChart.SeriesCollection.Contains(ovarian_series_Brca1And2))
                                BrcaProOvarianChart.SeriesCollection.Add(ovarian_series_Brca1And2);

                            label7.Text = "BRCA1and2 +";

                            BreastBrcaPosLifetimeLabel.Text = Brca1and2PosLifetimeBreast;
                            BreastBrcaPosFiveyearLabel.Text = Brca1and2PosFiveYearBreast;
                            OvarianBrcaPosLifetimeLabel.Text = Brca1and2PosLifetimeOvarian;
                            OvarianBrcaPosFiveyearLabel.Text = Brca1and2PosFiveYearOvarian;

                            break;
                    }
                    label43.Text = label7.Text;
                    BrcaProOvarianChart.RefreshChart();
                    BrcaProBreastChart.RefreshChart();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            TabPage active = tabControl1.SelectedTab;

            Bitmap b = new Bitmap(active.Width, active.Height);
            active.DrawToBitmap(b, new Rectangle(0, 0, active.Width, active.Height));
            Clipboard.SetImage(b);
        }

        private void summaryChartVisible(string summaryNA)
        {
            if (proband.HasBreastCancer())
            {
                label1.Visible = false;
                tableLayoutPanel1.Visible = false;
                summaryNAlabel.Text = "Breast cancer Risk is N/A because the patient has breast cancer.";
                SummaryChart.Visible = false;
                summaryNAlabel.Visible = true;
            }
            else
            {
                label1.Visible = true;
                tableLayoutPanel1.Visible = true;
                //summaryNAlabel.Text = "Breast cancer Risk is N/A because the patient has breast cancer.";
                SummaryChart.Visible = true;
                summaryNAlabel.Visible = false;
            }
        }

        public Bitmap getRiskChartToDisplay()
        {
            SummaryChart.Background.Color = Color.White;
            SummaryChart.ChartArea.Background.Color = Color.White;
            SummaryChart.TitleBox.Background.Color = Color.White;

            Bitmap tableau = new Bitmap(tabPage1.Width, tabPage1.Height);
            Graphics g = Graphics.FromImage(tableau);
            g.Clear(findBackColor(tabPage1));
            DrawAllControls(tabPage1, null, 0, 0, ref g);

            // Clean up
            g.Dispose();

            return tableau;
        }


        private void DrawAllControls(Control c, Control parent, int parentDrawnLeft, int parentDrawnTop, ref Graphics g)
        {
            try
            {
                //create bitmap image of this control
                if ((c.Width == 0) || (c.Height == 0) || (c.Name == "loadingCircle1") || (c.Name == "label13") || (c.Name == "tableLayoutPanel2")) return;

                Bitmap image = new Bitmap(c.Width, c.Height);
                c.DrawToBitmap(image, new Rectangle(0, 0, c.Width, c.Height));
                int absLeft = c.Left + ((parent == null) ? 0 : parentDrawnLeft);
                int absTop = c.Top + ((parent == null) ? 0 : parentDrawnTop);

                //draw it
                g.DrawImage(image, absLeft, absTop);
                //draw immediate children, if any
                if (c.HasChildren && !(c is Chart))
                {
                    foreach (Control child in c.Controls)
                    {
                        DrawAllControls(child, c, absLeft, absTop, ref g);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public Color findBackColor(Control c)
        {
            try
            {
                if (c.BackColor != Color.Transparent)
                {
                    return c.BackColor;
                }

                if (c.Parent != null)
                {
                    findBackColor(c.Parent);
                }

                return Color.White;
            }
            catch (Exception e)
            {
                return Color.White;
            }
        }
    }
}


/*
class DoneLoadingTracker
{
    bool BracproCancerRiskLoadDone = false;
    bool GailModelDone = false;
    bool ClausModelDone = false;
    bool RpLoadDone = false;
    bool TcLoadDone = false;
    bool Tc_v7LoadDone = false;
    BrOvCancerRiskView owningBocr = null;

    public DoneLoadingTracker(BrOvCancerRiskView owningBocr)
    {
        BracproCancerRiskLoadDone = false;
        GailModelDone = false;
        ClausModelDone = false;
        RpLoadDone = false;
        TcLoadDone = false;
        Tc_v7LoadDone = false;
        this.owningBocr = owningBocr;
    }
    public bool BracproCancerRiskLoadComplete()
    {
        BracproCancerRiskLoadDone = true;
        return allLoadsComplete();
    }
    public bool GailModelLoadComplete()
    {
        GailModelDone = true;
        return allLoadsComplete();
    }
    public bool ClausModelLoadComplete()
    {
        ClausModelDone = true;
        return allLoadsComplete();
    }
    public bool RpLoadComplete()
    {
        RpLoadDone = true;
        return allLoadsComplete();
    }
    public bool TcLoadComplete()
    {
        TcLoadDone = true;
        return allLoadsComplete();
    }
    public bool Tc_v7LoadComplete()
    {
        Tc_v7LoadDone = true;
        return allLoadsComplete();
    }

    public bool allLoadsComplete()
    {
        if (BracproCancerRiskLoadDone && GailModelDone && ClausModelDone && RpLoadDone && TcLoadDone && Tc_v7LoadDone) {
            owningBocr._waitHandle.Set();
            return true;
        }
        else
            return false;
    }
         
}
 */
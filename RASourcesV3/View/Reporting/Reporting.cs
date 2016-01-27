using BrightIdeasSoftware;
using EvoPdf;
using Foxit.PDF.Printing;
using HtmlAgilityPack;
using RiskApps3.Controllers;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.MetaData;
using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.View.Reporting
{
    public partial class Reporting : Form
    {
        public DateTime StartTime = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime EndTime = new DateTime(DateTime.Now.Year, 12, 31);
        public int clinicId = -1;
        public DocumentTemplate reportTemplate;
        public string clinicName;
        int divCount = 0;
        int curDiv = 0;

        public Reporting()
        {
            InitializeComponent();
            button1.Enabled = false;
            this.Size = new Size(this.Width,Screen.PrimaryScreen.WorkingArea.Size.Height);
        }

        private void Reporting_Load(object sender, EventArgs e)
        {


            //using (SqlDataReader reader = BCDB2.Instance.ExecuteReader("SELECT clinicID, clinicName FROM lkpclinics"))
            //{
            //    while (reader.Read())
            //    {
            //        Clinic c = new Clinic();
            //        c.clinicID = reader.GetInt32(0);
            //        c.clinicName = reader.GetString(1);

            //        ClinicCombo.Items.Add(c);
            //    }
            //}



            foreach (Clinic c in SessionManager.Instance.ActiveUser.UserClinicList)
            {
                ClinicCombo.Items.Add(c);
            }
            if (ClinicCombo.Items.Count > 0)
            {
                ClinicCombo.SelectedIndex = 0;
                clinicId = ((Clinic)(ClinicCombo.SelectedItem)).clinicID;
            }
            Clinic all = new Clinic();
            all.clinicID = -1;
            all.clinicName = "All Clinics";
            ClinicCombo.Items.Add(all);

            dateTimePicker1.Value = StartTime;
            dateTimePicker2.Value = EndTime;

            backgroundWorker2.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            winFormHtmlEditor1.Focus();
            winFormHtmlEditor1.DocumentHtml = "";
            progressBar1.Enabled = true;
            progressBar1.Visible = true;
            winFormHtmlEditor1.Enabled = false;
            label5.Visible = true;
            reportTemplate = (DocumentTemplate)ReportCombo.SelectedItem;
            clinicId = ((Clinic)(ClinicCombo.SelectedItem)).clinicID;
            clinicName = ((Clinic)(ClinicCombo.SelectedItem)).clinicName;
            StartTime = dateTimePicker1.Value;
            EndTime = dateTimePicker2.Value;


            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            reportTemplate.OpenHTML();
            PreProcessReportTemplate(reportTemplate);
            reportTemplate.ProcessDocument();
            reportTemplate.UpdateDocFromText();
            divCount = GetDivCount(reportTemplate.doc.DocumentNode);
            curDiv = 0;
            ProcessDocumentNode(reportTemplate.doc.DocumentNode);
        }

        private void PreProcessReportTemplate(DocumentTemplate reportTemplate)
        {
            reportTemplate.htmlText = reportTemplate.htmlText.Replace("#reportDate#", DateTime.Now.ToShortDateString());
            reportTemplate.htmlText = reportTemplate.htmlText.Replace("#reportStart#", StartTime.ToShortDateString());
            reportTemplate.htmlText = reportTemplate.htmlText.Replace("#reportEnd#", EndTime.ToShortDateString());
            reportTemplate.htmlText = reportTemplate.htmlText.Replace("#clinicChoice#", clinicName);
            reportTemplate.htmlText = reportTemplate.htmlText.Replace("#clinicId#", clinicId.ToString());
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            curDiv += 1;

            string s = (string)e.UserState;
            label5.Text = curDiv.ToString() + " / " + divCount.ToString() + " : " +  s;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            winFormHtmlEditor1.DocumentHtml = reportTemplate.doc.DocumentNode.OuterHtml;
            button1.Enabled = true;
            progressBar1.Enabled = false;
            progressBar1.Visible = false;
            label5.Visible = false;
            winFormHtmlEditor1.Enabled = true;
        }

        private void ProcessDocumentNode(HtmlNode node)
        {
            if (string.Compare(node.Name,"div",true)==0)
            {
                ProcessNode(node);
            }
            else
            {
                foreach (HtmlNode child in node.ChildNodes)
                {
                    ProcessDocumentNode(child);
                }
            }
        }

        private int GetDivCount(HtmlNode node)
        {
            int retval = 0;
            if (string.Compare(node.Name, "div", true) == 0)
                retval += 1;

            foreach (HtmlNode child in node.ChildNodes)
            {
                retval += GetDivCount(child);
            }

            return retval; 
        }

        private void ProcessNode(HtmlNode node)
        {

            switch(node.Id)
            {
                case "PatientVolume":
                    backgroundWorker1.ReportProgress(0, "Patient Volume by Clinic");
                    ProcessPatientVolumeNode(node); 
                    break;
                case "LifetimeBreastCancerRisk":
                    backgroundWorker1.ReportProgress(0, "Elevated Lifetime Risk of Breast Cancer");
                    ProcessLifetimeBreastCancerRiskNode(node);
                    break;
                case "LifetimeBreastRiskFrequency":
                    backgroundWorker1.ReportProgress(0, "Lifetime Breast Cancer Risk By Model");
                    ProcessLifetimeBreastRiskFrequencykNode(node);
                    break;
                case "BrOvCancerFrequency":
                    backgroundWorker1.ReportProgress(0, "Breast and/or Ovarian Cancer Status");
                    ProcessBrOvCancerFrequencyNode(node);
                    break;
                case "LynchCancerFrequency":
                    backgroundWorker1.ReportProgress(0, "Lynch Syndrome Cancer Status");
                    ProcessLynchCancerFrequencyNode(node);
                    break;
                case "LifetimeColonRiskFrequency":
                    backgroundWorker1.ReportProgress(0, "Elevated Lifetime Risk of Colorectal Cancer");
                    ProcessLifetimeColonRiskFrequencyNode(node);
                    break;
                case "MutationRisk":
                    backgroundWorker1.ReportProgress(0, "Mutation Risk Risk By Model");
                    ProcessMutationRiskNode(node);
                    break;
                case "LynchRiskByModel":
                    backgroundWorker1.ReportProgress(0, "Lynch Syndrome Mutation Risk Risk By Model");
                    ProcessLynchRiskByModelNode(node);
                    break;
                case "LynchHighRisk":
                    backgroundWorker1.ReportProgress(0, "Elevated Risk of Lynch Syndrome");
                    ProcessLynchHighRiskNode(node);
                    break;
                case "BRCAMutationRisk":
                    backgroundWorker1.ReportProgress(0, "High Risk BRCA1/2");
                    ProcessBRCAMutationRiskNode(node);
                    break;
                default:
                    break;
            }
        }

        private void ProcessLynchHighRiskNode(HtmlNode node)
        {
            LynchRiskElement lre = new LynchRiskElement(StartTime, EndTime, clinicId);
            lre.GetData();
            lre.ToHTML(node);
        }

        private void ProcessLynchRiskByModelNode(HtmlNode node)
        {
            LynchRiskFrequencyControl lrfc = new LynchRiskFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            lrfc.GetData();
            lrfc.ToHTML(node);
        }

        private void ProcessBRCAMutationRiskNode(HtmlNode node)
        {
            BrcaRiskElement bre = new BrcaRiskElement(StartTime, EndTime, clinicId);
            bre.GetData();
            bre.ToHTML(node);
        }

        private void ProcessLifetimeColonRiskFrequencyNode(HtmlNode node)
        {
            ColonRiskFrequencyControl crfc = new ColonRiskFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            crfc.GetData();
            crfc.ToHTML(node);
        }

        private void ProcessLynchCancerFrequencyNode(HtmlNode node)
        {
            LynchCancerFrequencyControl lcfc = new LynchCancerFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            lcfc.GetData();
            lcfc.ToHTML(node);
        }

        private void ProcessMutationRiskNode(HtmlNode node)
        {
            MutationRiskFrequencyControl mrfc = new MutationRiskFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            mrfc.GetData();
            mrfc.ToHTML(node);
        }

        private void ProcessLifetimeBreastRiskFrequencykNode(HtmlNode node)
        {
            BreastRiskFrequencyControl brfc = new BreastRiskFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            brfc.GetData();
            brfc.ToHTML(node);
        }

        private void ProcessLifetimeBreastCancerRiskNode(HtmlNode node)
        {
            BreastLifetimeRiskElement blre = new BreastLifetimeRiskElement(StartTime, EndTime, clinicId);
            blre.GetData();
            blre.ToHTML(node);
        }

        private void ProcessBrOvCancerFrequencyNode(HtmlNode node)
        {
            BrOvCancerFrequencyControl bofc = new BrOvCancerFrequencyControl(StartTime, EndTime, clinicId, "Cancer Screening");
            bofc.GetData();
            bofc.ToHTML(node);
        }

        private void ProcessPatientVolumeNode(HtmlNode node)
        {
            ApptHistoryControl ah = new ApptHistoryControl(StartTime,EndTime,clinicId,"Cancer Screening");
            ah.GetData();
            ah.ToHTML(node);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            string sql = "select documentTemplateID from lkpDocumentTemplates where routinename = 'ClinicReport'";
            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0) == false)
                            {
                                DocumentTemplate dt = new DocumentTemplate();
                                dt.documentTemplateID = reader.GetInt32(0);
                                dt.BackgroundLoadWork();
                                backgroundWorker2.ReportProgress(0, dt);
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception exception)
                {
                    Logger.Instance.WriteToLog(exception.ToString());
                }
            }

        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DocumentTemplate dt = (DocumentTemplate)e.UserState;
            ReportCombo.Items.Add(dt);

            if (ReportCombo.SelectedItem == null)
                ReportCombo.SelectedItem = dt;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Today":
                    StartTime = DateTime.Now.Date;
                    EndTime = DateTime.Now.Date;
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    break;
                case "This Week":
                    StartTime = UIUtils.weekstart(DateTime.Now.Date);
                    EndTime = UIUtils.weekend(DateTime.Now.Date);
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    break;
                case "This Month":
                    StartTime = new DateTime(DateTime.Now.Year, 1, 1);
                    EndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    break;
                case "This Year":
                    StartTime = new DateTime(DateTime.Now.Year, 1, 1);
                    EndTime = new DateTime(DateTime.Now.Year, 12, 31);
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    break;
                case "Last Year":
                    StartTime = new DateTime(DateTime.Now.Year - 1, 1, 1);
                    EndTime = new DateTime(DateTime.Now.Year - 1, 12, 31);
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    break;
                default:
                    DateTime.TryParse("1/1/1900", out StartTime);
                    DateTime.TryParse("1/1/3000", out EndTime);
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    break;
            }
            dateTimePicker1.Value = StartTime;
            dateTimePicker2.Value = EndTime;



        }

        private void dateTimePicker_CloseUp(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog MyFiles = new SaveFileDialog();
            MyFiles.Filter = "PDF File (*.pdf)|*.pdf|HTML File (*.html)|*.html";
            MyFiles.Title = "Save As...";
            MyFiles.DefaultExt = "*.pdf";
            MyFiles.FileName = ReportCombo.Text.Replace(" ", "_").Replace("/", ""); ;
            if (MyFiles.ShowDialog() == DialogResult.OK)
            {
                string name = MyFiles.FileName;
                string ext = Path.GetExtension(name);
                if (string.Compare(".pdf",ext,true)==0)
                {
                    HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
                    htmlToPdfConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
                    htmlToPdfConverter.HtmlViewerWidth = 850;
                    htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;
                    htmlToPdfConverter.ConvertHtmlToFile(winFormHtmlEditor1.DocumentHtml, "", name);
                }
                else
                {
                    File.WriteAllText(name, winFormHtmlEditor1.DocumentHtml);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string printername = "";
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                printername = pd.PrinterSettings.PrinterName;
                try
                {
                    HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
                    htmlToPdfConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
                    htmlToPdfConverter.HtmlViewerWidth = 850;
                    htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;

                    byte[] outPdfBuffer = htmlToPdfConverter.ConvertHtml(winFormHtmlEditor1.DocumentHtml, "");


                    InputPdf inputPdf = new InputPdf(outPdfBuffer);
                    //PrinterSettings settings = new PrinterSettings();
                    PrintJob printJob = new PrintJob(printername, inputPdf);
                    printJob.DocumentName = ReportCombo.Text.Replace(" ", "_").Replace("/","");
                    PrintJob.AddLicense("FPM20NXDLB2DHPnggbYuVwkquSU3u2ffoA/Pgph4rjG5wiNCxO8yEfbLf2j90rZw1J3VJQF2tsniVvl5CxYka6SmZX4ak6keSsOg");
                    printJob.PrintOptions.Scaling = new AutoPageScaling();
                    printJob.Print();
                }
                catch (Exception ee)
                {
                    Logger.Instance.WriteToLog(ee.ToString());
                }
            }
        }
    }
}

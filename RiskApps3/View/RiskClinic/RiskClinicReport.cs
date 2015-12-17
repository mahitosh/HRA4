using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BrightIdeasSoftware;
using RiskApps3.Model.Clinic.Reports;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using dotnetCHARTING.WinForms;

namespace RiskApps3.View.RiskClinic
{
    public partial class RiskClinicReport : Form
    {
        private RiskClinicPatientCohort PatientCohort = new RiskClinicPatientCohort();
        private RiskClinicTestingCohort TestingCohort = new RiskClinicTestingCohort();
        private RiskClinicEthnicityCohort EthnicityCohort = new RiskClinicEthnicityCohort();
        private RiskClinicReferringProviders ReferringProviders = new RiskClinicReferringProviders();
        private EmailReport emailReport = new EmailReport();
        private OptInOrOutReport optInOrOutReport = new OptInOrOutReport();

        private DocumentTemplate dt = new DocumentTemplate();

        public RiskClinicReport()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.AddDays(-365);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;

            loadingCircle2.Enabled = true;
            loadingCircle2.Visible = true;

            loadingCircle3.Enabled = true;
            loadingCircle3.Visible = true;

            loadingCircle4.Enabled = true;
            loadingCircle4.Visible = true;

            loadingCircle5.Enabled = true;
            loadingCircle5.Visible = true;

            listView1.Items.Clear();
            listView2.Items.Clear();

            fastDataListView1.ClearObjects();
            fastDataListView2.ClearObjects();
            fastDataListView3.ClearObjects();
            fastDataListView4.ClearObjects();
            fastDataListView5.ClearObjects();

            splitContainer3.Enabled = false;

            chart1.SeriesCollection.Clear();
            listView3.Items.Clear();
            chart2.SeriesCollection.Clear();

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void RiskClinicReport_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void fastDataListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }


        private string GetChartImage(Image image)
        {

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            String imageData = "data:image/png;base64,";

            imageData = imageData + base64Data;
            return
                "<img class=\"autoResizeImage\" src=\"" + imageData + "\">";
        }

        private String InsertChartImage(Image image)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            String imageData = "data:image/png;base64,";

            imageData = imageData + base64Data;
            return
                "<div class=\"break\" id=\"Pedigree\"><img src=\"" + imageData + "\"></div>";
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            PatientCohort.StartTime = dateTimePicker1.Value;
            PatientCohort.EndTime = dateTimePicker2.Value;
            PatientCohort.BackgroundListLoad();
            backgroundWorker1.ReportProgress(10);

            /////////////////////////////////////////////////////////////
            //  Patients
            /////////////////////////////////////////////////////////////
            //SeriesCollection PatientsChartCollection = new SeriesCollection();
            Series PatientSeries = new Series("Patient Visits By Type");
            string patients_html = dt.GetTableDiv("Patients");

            string metric = "";
            //////////////////////////////
            ListViewItem lvi = GetListViewAndUpdateHtml("Number of Scheduled Visits",
                                                        PatientCohort.ToArray(),
                                                        ref patients_html);

            //  Image chartImage = chart1.GetChartBitmap();

//            ListViewItem lvi = GetListViewAndUpdateHtml("Number of Scheduled Visits",
            //                                                      PatientCohort.ToArray(),
            //                                                    ref patients_html, chartImage, 14);


            backgroundWorker1.ReportProgress(11, lvi);

            HraObject[] totalAssessments =
                PatientCohort.Where(p => ((PatientCohortMember) p).RiskAssessment == 1).ToArray();
            if (totalAssessments != null)
            {
                PatientSeries.Name += (" (n=" + totalAssessments.Length.ToString() + ")");
            }
            lvi = GetListViewAndUpdateHtml("Risk Assessments",
                                           totalAssessments,
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);

            //////////////////////////////
            lvi = new ListViewItem("Unique Patients");
            lvi.Tag = null;
            metric =
                PatientCohort.Where(p => ((PatientCohortMember) p).RiskAssessment == 1)
                             .Select(p => ((PatientCohortMember) p).unitnum)
                             .Distinct()
                             .Count()
                             .ToString();
            lvi.SubItems.Add(metric);
            patients_html = dt.InsertTwoColumnLeftRightTableRow(patients_html, "Unique Patients", metric);
            backgroundWorker1.ReportProgress(11, lvi);
            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("New Patient Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).Followup == 0 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1).ToArray(),
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);

            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Followup Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).Followup == 1 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1).ToArray(),
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);

            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Cancer Patient Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 1 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1).ToArray(),
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);
            //////////////////////////////

            lvi = GetListViewAndUpdateHtml("Non Cancer Patient Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 0 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1).ToArray(),
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);
            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Cancer Patient New Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 1 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1 &&
                                               ((PatientCohortMember) p).Followup == 0).ToArray(),
                                           ref patients_html);
            AddToSeries(ref PatientSeries, lvi);
            backgroundWorker1.ReportProgress(11, lvi);
            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Cancer Patient Followup Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 1 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1 &&
                                               ((PatientCohortMember) p).Followup == 1).ToArray(),
                                           ref patients_html);
            AddToSeries(ref PatientSeries, lvi);
            backgroundWorker1.ReportProgress(11, lvi);

            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Non Cancer Patient New Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 0 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1 &&
                                               ((PatientCohortMember) p).Followup == 0).ToArray(),
                                           ref patients_html);
            AddToSeries(ref PatientSeries, lvi);
            backgroundWorker1.ReportProgress(11, lvi);
            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Non Cancer Followup Patient Visits",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).CancerPatient == 0 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1 &&
                                               ((PatientCohortMember) p).Followup == 1).ToArray(),
                                           ref patients_html);
            AddToSeries(ref PatientSeries, lvi);
            backgroundWorker1.ReportProgress(11, lvi);

            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Visits by Relatives of Other Patients",
                                           PatientCohort.Where(
                                               p =>
                                               ((PatientCohortMember) p).RelatedAppts == 1 &&
                                               ((PatientCohortMember) p).RiskAssessment == 1).ToArray(),
                                           ref patients_html);
            backgroundWorker1.ReportProgress(11, lvi);

            backgroundWorker1.ReportProgress(12, PatientSeries);

            AddImageAsThirdColumn(ref patients_html, chart1.GetChartBitmap());

            /////////////////////////////////////////////////////////////
            //  Tests
            /////////////////////////////////////////////////////////////

            TestingCohort.StartTime = dateTimePicker1.Value;
            TestingCohort.EndTime = dateTimePicker2.Value;
            TestingCohort.BackgroundListLoad();
            backgroundWorker1.ReportProgress(20);

            string tests_html = dt.GetTableDiv("Tests");

            metric = "";

            lvi = GetListViewAndUpdateHtml("Total Number of Tests Ordered",
                                           TestingCohort.ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Total Number of Tests Pending",
                                           TestingCohort.Where(p => ((TestingCohorttMember) p).IsCompleted == 0)
                                                        .ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Total Number of Tests Completed",
                                           TestingCohort.Where(p => ((TestingCohorttMember) p).IsCompleted == 1)
                                                        .ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = new ListViewItem("Unique Patients");
            lvi.Tag = null;
            metric =
                TestingCohort.Where(p => ((TestingCohorttMember) p).IsCompleted == 1)
                             .Select(p => ((TestingCohorttMember) p).unitnum)
                             .Distinct()
                             .Count()
                             .ToString();
            lvi.SubItems.Add(metric);
            tests_html = dt.InsertTwoColumnLeftRightTableRow(tests_html, "Unique Patients", metric);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Completed BRCA Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsBrcaPanel == 1).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Completed Lynch Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsLynchPanel == 1).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Completed Other Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsLynchPanel == 0 &&
                                               ((TestingCohorttMember) p).IsBrcaPanel == 0).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Positive BRCA Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsBrcaPanel == 1 &&
                                               ((TestingCohorttMember) p).VariantFound == 1).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Positive Lynch Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsLynchPanel == 1 &&
                                               ((TestingCohorttMember) p).VariantFound == 1).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);

            lvi = GetListViewAndUpdateHtml("Positive Other Tests",
                                           TestingCohort.Where(
                                               p =>
                                               ((TestingCohorttMember) p).IsCompleted == 1 &&
                                               ((TestingCohorttMember) p).IsLynchPanel == 0 &&
                                               ((TestingCohorttMember) p).IsBrcaPanel == 0 &&
                                               ((TestingCohorttMember) p).VariantFound == 1).ToArray(),
                                           ref tests_html);
            backgroundWorker1.ReportProgress(21, lvi);


            /////////////////////////////////////////////////////////////
            //  Ethnicity
            /////////////////////////////////////////////////////////////
            Series EthnicitySeries = new Series("Ethnicity");
            string ethnicity_html = dt.GetTableDiv("Ethnicity");
            EthnicityCohort.StartTime = dateTimePicker1.Value;
            EthnicityCohort.EndTime = dateTimePicker2.Value;
            EthnicityCohort.BackgroundListLoad();
            backgroundWorker1.ReportProgress(60);

            //////////////////////////////
            lvi = GetListViewAndUpdateHtml("Total number of Ethnicity Reports",
                                           EthnicityCohort.ToArray(),
                                           ref ethnicity_html);
            backgroundWorker1.ReportProgress(61, lvi);
            //////////////////////////////
            lvi = new ListViewItem("Unique Patients");
            lvi.Tag = null;
            metric = EthnicityCohort.Select(p => ((EthnicityCohortMember) p).unitnum).Distinct().Count().ToString();
            lvi.SubItems.Add(metric);
            ethnicity_html = dt.InsertTwoColumnLeftRightTableRow(ethnicity_html, "Unique Patients", metric);
            backgroundWorker1.ReportProgress(61, lvi);
            ////////////////////////////// 
            foreach (string o in EthnicityCohort.Select(p => ((EthnicityCohortMember) p).racialBackground).Distinct())
            {
                if (string.IsNullOrEmpty(o))
                {
                    lvi = GetListViewAndUpdateHtml("People Identifying as no ethnicity",
                                                   EthnicityCohort.Where(
                                                       p => ((EthnicityCohortMember) p).racialBackground == "")
                                                                  .ToArray(),
                                                   ref ethnicity_html);
                    AddToSeries(ref EthnicitySeries, lvi);
                    backgroundWorker1.ReportProgress(61, lvi);
                }
                else
                {
                    lvi = GetListViewAndUpdateHtml("People Identifying as " + o,
                                                   EthnicityCohort.Where(
                                                       p => ((EthnicityCohortMember) p).racialBackground == o).ToArray(),
                                                   ref ethnicity_html);
                    AddToSeries(ref EthnicitySeries, lvi);
                    backgroundWorker1.ReportProgress(61, lvi);
                }
            }
            //////////////////////////////
            backgroundWorker1.ReportProgress(62, EthnicitySeries);

            AddImageAsThirdColumn(ref ethnicity_html, chart2.GetChartBitmap());

            /////////////////////////////////////////////////////////////
            //  ReferringProviders
            /////////////////////////////////////////////////////////////
            ReferringProviders.StartTime = dateTimePicker1.Value;
            ReferringProviders.EndTime = dateTimePicker2.Value;
            ReferringProviders.BackgroundListLoad();

            backgroundWorker1.ReportProgress(30);

            /////////////////////////////////////////////////////////////
            //  emailReport
            /////////////////////////////////////////////////////////////
            emailReport.StartTime = dateTimePicker1.Value;
            emailReport.EndTime = dateTimePicker2.Value;
            emailReport.BackgroundListLoad();

            backgroundWorker1.ReportProgress(40);

            /////////////////////////////////////////////////////////////
            //  optInOrOutReport
            /////////////////////////////////////////////////////////////
            optInOrOutReport.StartTime = dateTimePicker1.Value;
            optInOrOutReport.EndTime = dateTimePicker2.Value;
            optInOrOutReport.BackgroundListLoad();

            backgroundWorker1.ReportProgress(50);

            /////////////////////////////////////////////////////////////
            //  Document
            /////////////////////////////////////////////////////////////
            dt.documentTemplateID = 500;
            dt.BackgroundLoadWork();
            dt.OpenHTML();
            dt.UseDocArgs = false;
            dt.ProcessDocument();
            dt.AddDiv(patients_html);
            dt.AddDiv(tests_html);
            dt.AddDiv(ethnicity_html);
        }

        private void AddToSeries(ref Series ChartCollection, ListViewItem lvi)
        {
            if (lvi.Tag != null)
            {
                ReportSeries series = (ReportSeries) lvi.Tag;
                if (series.ChartElement != null)
                    ChartCollection.Elements.Add(series.ChartElement);
            }
        }

        private ListViewItem GetListViewAndUpdateHtml(string p, HraObject[] hraObject, ref string patients_html)
        {
            ReportSeries rs = new ReportSeries();
            rs.CreateChartSeriesFromObjectArray(hraObject, p);

            ListViewItem lvi = new ListViewItem(p);
            lvi.Tag = rs;
            lvi.SubItems.Add(hraObject.Length.ToString());
            patients_html = dt.InsertTwoColumnLeftRightTableRow(patients_html, p, hraObject.Length.ToString());
            return lvi;
        }

        private void AddImageAsThirdColumn(ref string html, Image image)
        {
            int pos = html.IndexOf("</tr>");
            int pos2 = html.IndexOf("</tr>", pos + 1);

            //colspan="2"

            html = html.Replace("colspan=\"2\"", "colspan=\"3\"");

            //Console.WriteLine("=================================================");
            //Console.WriteLine(html);
            //Console.WriteLine("=================================================");

            String imageTag = GetChartImage(image);
            int count = Regex.Matches(html, "</tr>").Count;

            int rowSpan = count - 1;
            String rowSpanTag = "rowspan=\"" + rowSpan + "\"";
            html = html.Insert(pos2, @"
                        <td  " + rowSpanTag + @" align=""right"">
                            " + imageTag + @"
                     </td>
                    ");
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 10:
                    fastDataListView1.SetObjects(PatientCohort);
                    loadingCircle1.Enabled = false;
                    loadingCircle1.Visible = false;
                    break;
                case 11:
                    ListViewItem lvi = (ListViewItem) (e.UserState);
                    listView1.Items.Add(lvi);
                    break;
                case 12:
                    Series sc = (Series) (e.UserState);
                    chart1.Title = sc.Name;
                    sc.Name = "";
                    chart1.SeriesCollection.Add(sc);
                    break;
                case 20:
                    fastDataListView2.SetObjects(TestingCohort);
                    loadingCircle2.Enabled = false;
                    loadingCircle2.Visible = false;
                    break;
                case 21:
                    ListViewItem lvi2 = (ListViewItem) (e.UserState);
                    listView2.Items.Add(lvi2);
                    break;
                case 30:
                    fastDataListView3.SetObjects(ReferringProviders);
                    loadingCircle3.Enabled = false;
                    loadingCircle3.Visible = false;
                    break;
                case 40:
                    fastDataListView4.SetObjects(emailReport);
                    loadingCircle4.Enabled = false;
                    loadingCircle4.Visible = false;
                    break;
                case 50:
                    fastDataListView5.SetObjects(optInOrOutReport);
                    loadingCircle5.Enabled = false;
                    loadingCircle5.Visible = false;
                    break;
                case 60:
                    fastDataListView6.SetObjects(EthnicityCohort);
                    break;
                case 61:
                    ListViewItem lvi3 = (ListViewItem) (e.UserState);
                    listView3.Items.Add(lvi3);
                    break;
                case 62:
                    Series sc2 = (Series) (e.UserState);
                    chart2.Title = sc2.Name;
                    sc2.Name = "";
                    chart2.SeriesCollection.Add(sc2);
                    break;
                default:
                    break;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            webBrowser1.DocumentText = dt.htmlText;

            splitContainer3.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            fastDataListView4.CopyObjectsToClipboard((IList) fastDataListView4.Objects);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            fastDataListView4.CopyObjectsToClipboard((IList) fastDataListView4.Objects);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.DefaultExt = "txt";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter outfile = new StreamWriter(sfd.FileName);
                outfile.Write(Clipboard.GetText());
                outfile.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fastDataListView5.CopyObjectsToClipboard((IList) fastDataListView5.Objects);
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count > 0)
            {
                if (listView2.SelectedItems[0].Tag != null)
                {
                    ReportSeries rs = (ReportSeries) listView2.SelectedItems[0].Tag;
                    if (rs.ObjectArray != null)
                        fastDataListView2.SelectObjects(rs.ObjectArray);
                    else
                        fastDataListView2.SelectObjects(null);
                }
                else
                {
                    fastDataListView2.SelectObjects(null);
                }
            }
            else
            {
                fastDataListView2.SelectObjects(null);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].Tag != null)
                {
                    ReportSeries rs = (ReportSeries) listView1.SelectedItems[0].Tag;
                    if (rs.ObjectArray != null)
                        fastDataListView1.SelectObjects(rs.ObjectArray);
                    else
                        fastDataListView1.SelectObjects(null);
                }
                else
                {
                    fastDataListView1.SelectObjects(null);
                }
            }
            else
            {
                fastDataListView1.SelectObjects(null);
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            dt.SetDivVis((string) checkedListBox1.Items[e.Index], e.NewValue == CheckState.Checked);
            webBrowser1.DocumentText = dt.htmlText;
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listView3.SelectedItems.Count > 0)
            //{
            //    if (listView3.SelectedItems[0].Tag != null)
            //    {
            //        ReportSeries rs = (ReportSeries)listView1.SelectedItems[0].Tag;
            //        if (rs.ObjectArray != null)
            //            fastDataListView6.SelectObjects(rs.ObjectArray);
            //        else
            //            fastDataListView6.SelectObjects(null);
            //    }
            //    else
            //    {
            //        fastDataListView6.SelectObjects(null);
            //    }
            //}
            //else
            //{
            //    fastDataListView6.SelectObjects(null);
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(GetDataAsSring());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            sfd.DefaultExt = "txt";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter outfile = new StreamWriter(sfd.FileName);
                outfile.Write(GetDataAsSring());
                outfile.Close();
            }
        }


        private string GetDataAsSring()
        {
            string output = "";

            for (int i = 0; i < fastDataListView1.Columns.Count; i++)
            {
                String headerName = fastDataListView1.GetColumn(i).ToString();
                int pos = headerName.LastIndexOf(":");

                headerName = headerName.Substring(pos + 1).Trim();

                output += headerName + "\t";
            }
            output += Environment.NewLine;

            for (int i = 0; i < fastDataListView1.GetItemCount(); i++)
            {
                OLVListItem OLVListItem = fastDataListView1.GetItem(0);
                PatientCohortMember patientCohortMember = (PatientCohortMember)OLVListItem.RowObject;

                for (int j = 0; j < fastDataListView1.Columns.Count; j++)
                {
                    OLVColumn OLVColumn = fastDataListView1.GetColumn(j);
                    String aspectName = OLVColumn.AspectName;

                   String value =  patientCohortMember.GetType().GetField(aspectName).GetValue(patientCohortMember).ToString();
                   output += value + "\t";
                }
                output += Environment.NewLine;
            }
            return output;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //if (!e.Url.AbsolutePath.Contains("blank"))
            //{
            //    webBrowser1.DocumentText = "hello world";
            //    e.Cancel = true;
            //}
                
        }
    }

    public class ReportSeries
    {
        public Element ChartElement;
        public HraObject[] ObjectArray;

        public void CreateChartSeriesFromObjectArray(HraObject[] p, string name)
        {
            ObjectArray = p;
            if (p != null)
            {
                ChartElement = new Element();
                ChartElement.Name = name;
                ChartElement.YValue = p.Length;
            }
        }
    }
}
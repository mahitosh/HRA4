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
using RiskApps3.Utilities;
using HtmlAgilityPack;

namespace RiskApps3.View.Reporting
{
    public partial class ApptHistoryControl : UserControl
    {
        public AppointmentHistoryStats history = new AppointmentHistoryStats();
        public AppointmentHistorySummaryStats summary = new AppointmentHistorySummaryStats();

        public ApptHistoryControl(DateTime StartTime, DateTime EndTime, int clinicId, string type)
        {
            InitializeComponent();
            chart1.LegendBox.Visible = false;
            chart1.DefaultElement.SmartLabel.Text = "%YValue";
            chart1.Title = "Monthly Completed Cancer Screening Risk Assessments";
            chart1.TitleBox.Background.Color = Color.White;
            chart1.TitleBox.Position = TitleBoxPosition.FullWithLegend;
            chart1.Palette = new Color[] { Color.FromArgb(240, 163, 255), Color.FromArgb(0, 117, 220), Color.FromArgb(153, 63, 0), Color.FromArgb(76, 0, 92), Color.FromArgb(25, 25, 25), Color.FromArgb(0, 92, 49), Color.FromArgb(43, 206, 72), Color.FromArgb(255, 204, 153), Color.FromArgb(128, 128, 128), Color.FromArgb(148, 255, 181), Color.FromArgb(143, 124, 0), Color.FromArgb(157, 204, 0), Color.FromArgb(194, 0, 136), Color.FromArgb(0, 51, 128), Color.FromArgb(255, 164, 5), Color.FromArgb(255, 168, 187), Color.FromArgb(66, 102, 0), Color.FromArgb(255, 0, 16), Color.FromArgb(94, 241, 242), Color.FromArgb(0, 153, 143), Color.FromArgb(224, 255, 102), Color.FromArgb(116, 10, 255), Color.FromArgb(153, 0, 0), Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 80, 5) };
            label1.Text = "";
            chart1.Width = 600;
            history.StartTime = StartTime;
            history.EndTime = EndTime;
            history.clinicID = clinicId;
            history.type = type;

            summary.StartTime = StartTime;
            summary.EndTime = EndTime;
            summary.clinicID = clinicId;
            summary.type = type;
        }

        private void PatientCohortElement_Load(object sender, EventArgs e)
        {

        } 

        public void GetData()
        {
            history.BackgroundListLoad();
            summary.BackgroundListLoad();
            LoadChart();
        }

        public void ToHTML(HtmlAgilityPack.HtmlNode node)
        {
            HtmlNode table = node.OwnerDocument.CreateElement("table");

            HtmlNode row1 = node.OwnerDocument.CreateElement("tr");
            row1.Attributes.Add(node.OwnerDocument.CreateAttribute("valign", "top"));

            HtmlNode td1 = node.OwnerDocument.CreateElement("td");
            HtmlNode td2 = node.OwnerDocument.CreateElement("td");

            td1.ChildNodes.Add(GetTableNode(node));
            td2.ChildNodes.Add(UIUtils.GetImageHtmlFromChart(chart1, node));
            row1.ChildNodes.Add(td1);
            row1.ChildNodes.Add(td2);
            table.ChildNodes.Add(row1);
            node.ChildNodes.Add(table); 
        }

        private HtmlAgilityPack.HtmlNode GetTableNode(HtmlAgilityPack.HtmlNode node)
        {
            HtmlNode table = node.OwnerDocument.CreateElement("table");

            foreach(RiskApps3.Model.Clinic.Reports.AppointmentHistorySummaryStats.AppointmentSummaryStat stat in summary)
            {
                HtmlNode row0 = node.OwnerDocument.CreateElement("tr");
                HtmlNode td0 = node.OwnerDocument.CreateElement("td"); 

                HtmlNode row1 = node.OwnerDocument.CreateElement("tr"); 
                HtmlNode td1 = node.OwnerDocument.CreateElement("td");
                HtmlNode td2 = node.OwnerDocument.CreateElement("td");

                HtmlNode row2 = node.OwnerDocument.CreateElement("tr");
                HtmlNode td3 = node.OwnerDocument.CreateElement("td");
                HtmlNode td4 = node.OwnerDocument.CreateElement("td");

                HtmlAttribute attr = node.OwnerDocument.CreateAttribute("style", "border:1px solid black;border-collapse:collapse;");

                table.Attributes.Add(attr);
                row0.Attributes.Add(attr);
                td0.Attributes.Add(attr);
                row1.Attributes.Add(attr);
                td1.Attributes.Add(attr);
                td2.Attributes.Add(attr);
                row2.Attributes.Add(attr);
                td3.Attributes.Add(attr);
                td4.Attributes.Add(attr);


                HtmlAttribute colspan = node.OwnerDocument.CreateAttribute("colspan", "2");
                td0.Attributes.Add(colspan);

                HtmlNode bold = node.OwnerDocument.CreateElement("b");

                bold.InnerHtml = stat.clinicName;
                td1.InnerHtml = "Completed Risk Assessments";
                td2.InnerHtml = stat.total_appts.ToString("#,###,###");
                td3.InnerHtml = "Unique Patients";
                td4.InnerHtml = stat.unique_patients.ToString("#,###,###");

                td0.ChildNodes.Add(bold);

                row0.ChildNodes.Add(td0);
                row1.ChildNodes.Add(td1);
                row1.ChildNodes.Add(td2);
                row2.ChildNodes.Add(td3);
                row2.ChildNodes.Add(td4);

                table.ChildNodes.Add(row0);
                table.ChildNodes.Add(row1);
                table.ChildNodes.Add(row2);
            }
            return table;
        }

        private void LoadChart()
        {
            string latest = "";

            history.Sort((x, y) => ((AppointmentHistoryStats.AppointmentStat)x).clinicName.CompareTo(((AppointmentHistoryStats.AppointmentStat)y).clinicName));
            Series s = null;
            foreach (object o in history)
            {
                if (o is AppointmentHistoryStats.AppointmentStat)
                {
                    AppointmentHistoryStats.AppointmentStat stat = (AppointmentHistoryStats.AppointmentStat)o;
                    if (stat.clinicName != latest)
                    {
                        if (s != null)
                        {
                            chart1.SeriesCollection.Add(s); 
                        }
                        s = new Series(stat.clinicName);
                        s.LegendEntry = new LegendEntry(stat.clinicName, "%Value");
                        latest = stat.clinicName;
                    }
                    Element elem = new Element();
                    elem.XDateTime = stat.StartOfMonth;
                    elem.YValue = stat.apptCount;
                    if (s != null)
                        s.AddElements(elem);
                }
            }
            if (s != null)
                chart1.SeriesCollection.Add(s);
        } 
    }
}
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
using RiskApps3.Model.Clinic.Reports;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using dotnetCHARTING.WinForms;

namespace RiskApps3.View.Admin
{
    public partial class AuditReportsForm : Form
    {
        private AuditUserLoginsReport AuditUserLoginsReport = new AuditUserLoginsReport();
        private AuditMrnAccessV2 auditMrnAccessV2 = new AuditMrnAccessV2();
        private AuditMrnAccessV3 auditMrnAccessV3 = new AuditMrnAccessV3();

        private DateTime start;
        private DateTime end;
        private string unitnum;

        public AuditReportsForm()
        {
            InitializeComponent();

            dateTimePicker1.Value = DateTime.Now.AddDays(-365);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void AuditReportsForm_Load(object sender, EventArgs e)
        {

            //fastDataListViewAuditLogins.ClearObjects();
            fastDataListViewAuditUnitnumAccessV2.ClearObjects();
            fastDataListViewAuditUnitnumAccessV3.ClearObjects();

            loadMrnComboBox();
        }

        private void fastDataListView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void loadMrnComboBox()
        {
            //mrnList.BackgroundListLoad();
            //List<string> mrns = new List<string>();
            //foreach (MrnListEntry entry in mrnList)
            //{
            //    mrns.Add(entry.unitnum);
            //}

            //mrnComboBox.DataSource = mrns;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /////////////////////////////////////////////////////////////
                //  AuditUserLoginsReport
                /////////////////////////////////////////////////////////////
                AuditUserLoginsReport.StartTime = start;
                AuditUserLoginsReport.EndTime = end;
                AuditUserLoginsReport.BackgroundListLoad();

                backgroundWorker1.ReportProgress(10);

                /////////////////////////////////////////////////////////////
                // AuditMrnAccessV2
                /////////////////////////////////////////////////////////////
                auditMrnAccessV2.StartTime = start;
                auditMrnAccessV2.EndTime = end;
                auditMrnAccessV2.unitnum = unitnum;
                auditMrnAccessV2.BackgroundListLoad();

                backgroundWorker1.ReportProgress(20);

                /////////////////////////////////////////////////////////////
                // AuditMrnAccessV3
                /////////////////////////////////////////////////////////////
                auditMrnAccessV3.StartTime = start;
                auditMrnAccessV3.EndTime = end;
                auditMrnAccessV3.unitnum = unitnum;
                auditMrnAccessV3.BackgroundListLoad();

                backgroundWorker1.ReportProgress(30);
            }
            catch (Exception ee)
            {
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                switch (e.ProgressPercentage)
                {
                    case 10:
                        //fastDataListViewAuditLogins.SetObjects(AuditUserLoginsReport);
                        //loadingCircleAuditLogins.Enabled = false;
                        //loadingCircleAuditLogins.Visible = false;
                        break;

                    case 20:
                        fastDataListViewAuditUnitnumAccessV2.SetObjects(auditMrnAccessV2);
                        loadingCircleAuditUnitnumAccessV2.Enabled = false;
                        loadingCircleAuditUnitnumAccessV2.Visible = false;
                        break;

                    case 30:
                        fastDataListViewAuditUnitnumAccessV3.SetObjects(auditMrnAccessV3);
                        loadingCircleAuditUnitnumAcessV3.Enabled = false;
                        loadingCircleAuditUnitnumAcessV3.Visible = false;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception eee)
            {
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            start = dateTimePicker1.Value;
            end = dateTimePicker2.Value;
            unitnum = mrnTextBox.Text;

            //loadingCircleAuditLogins.Enabled = true;
            //loadingCircleAuditLogins.Visible = true;
            //fastDataListViewAuditLogins.ClearObjects();

            loadingCircleAuditUnitnumAccessV2.Enabled = true;
            loadingCircleAuditUnitnumAccessV2.Visible = true;
            fastDataListViewAuditUnitnumAccessV2.ClearObjects();

            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void fastDataListViewAuditUnitnumAccessV3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

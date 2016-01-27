using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Utilities;
using System.Xml;
using System.Xml.Serialization;
using RiskApps3.Model.PatientRecord;
using System.Runtime.Serialization;
using System.IO;
using RiskApps3.Model.Clinic;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;

namespace RiskApps3.View.Appointments
{
    public partial class WebAppointments : Form
    {
        //private string PREVIEW = "Preview";
        private string SYNCH_APPT = "Accept - Copy To Existing Appointment";
        private static string DELETE_APPT = "Reject - Delete Web Survey";
        private string CREATE_NEW_APPT = "Accept - Create New Appointment";

        AppointmentList appts;
        private bool htmlInProgress = false;
        string url;
        HraCloudServices.RiskAppsCloudServices cloud;


        public WebAppointments()
        {
            appts = new AppointmentList();

            string url = Configurator.getCloudWebURL();
            cloud = new RiskApps3.HraCloudServices.RiskAppsCloudServices(url);

            InitializeComponent();

            label14.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Enabled = true;
            label14.Text = "Fetching Available Web Surveys...";
            button1.Enabled = false;
            fastDataListView1.Enabled = false;
            backgroundWorker1.RunWorkerAsync();

            foreach (Clinic c in SessionManager.Instance.ActiveUser.UserClinicList)
            {
                comboBox1.Items.Add(c);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocLoadComplete);

        }

        private void fastDataListView1_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column.Text != "Choose Action")
                return;

            ComboBox cb = new ComboBox();
            cb.Bounds = e.CellBounds;
            cb.Font = ((BrightIdeasSoftware.ObjectListView)sender).Font;
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.Items.AddRange(new String[] { "", CREATE_NEW_APPT, SYNCH_APPT, DELETE_APPT });
            cb.Text = ((Appointment)e.RowObject).CloudWebQueueState;
            cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
            cb.Tag = e.RowObject; // remember which appt we are editing
            e.Control = cb;
        }
        private void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            Appointment a = (Appointment)cb.Tag;
            a.CloudWebQueueState = cb.Text;

            if (a.CloudWebQueueState == SYNCH_APPT)
            {
                WebApptSynchToSchedule wsts = new WebApptSynchToSchedule();
                wsts.Setup(appts);
                wsts.ShowDialog();
                a.CloudWebQueueSynchId = wsts.SelectedApptID;
            }
            else if (a.CloudWebQueueState == DELETE_APPT)
            {
                if (MessageBox.Show("Are you sure you want to mark this survey to be deleted?", "Delete Survey", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                }
                else
                {
                    cb.Text = "";
                };
            }

            fastDataListView1.Focus();
        }

        private void fastDataListView1_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column.Text != "Choose Action")
                return;

            // Stop listening for change events
            ((ComboBox)e.Control).SelectedIndexChanged -= new EventHandler(cb_SelectedIndexChanged);

            // Any updating will have been down in the SelectedIndexChanged event handler
            // Here we simply make the list redraw the involved ListViewItem
            ((BrightIdeasSoftware.ObjectListView)sender).RefreshItem(e.ListViewItem);

            // We have updated the model object, so we cancel the auto update
            e.Cancel = true;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SessionManager.Instance.ActiveUser.UserClinicList.Count > 0)
            {
                appts.Date = DateTime.Now.ToShortDateString();
                appts.clinicId = ((Clinic)(SessionManager.Instance.ActiveUser.UserClinicList[0])).clinicID;
                appts.BackgroundListLoad();
            }

           
            XmlNode x = cloud.FetchCompletedSurveys();
            foreach (XmlNode xmlappt in x.ChildNodes)
            {
                Appointment appt = new Appointment();
                int.TryParse(xmlappt.Attributes["apptid"].Value, out appt.apptID);
                appt.PatientName = xmlappt.Attributes["patientname"].Value;
                appt.Unitnum = xmlappt.Attributes["unitnum"].Value;
                appt.SurveyType = xmlappt.Attributes["surveyType"].Value;
                appt.Dob = xmlappt.Attributes["dob"].Value;
                DateTime.TryParse(xmlappt.Attributes["riskdatacompleted"].Value, out appt.riskdatacompleted);
                backgroundWorker1.ReportProgress(0, appt);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                fastDataListView1.AddObject(e.UserState);
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label14.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            fastDataListView1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false && backgroundWorker2.IsBusy == false)
            {
                button1.Enabled = false;
                
                fastDataListView1.ClearObjects();

                label14.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Enabled = true;
                label14.Text = "Fetching Available Web Surveys...";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false && backgroundWorker2.IsBusy == false)
            {
                Clinic c = (Clinic)comboBox1.SelectedItem;

                List<Appointment> appts = new List<Appointment>();
                foreach (object o in fastDataListView1.Objects)
                {
                    Appointment appt = (Appointment)o;
                    appt.Clinic = c;
                    appts.Add(appt);
                }
 
                button1.Enabled = false;
                fastDataListView1.Enabled = false;
                label14.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Enabled = true;
                label14.Text = "Processing Appt List...";
                backgroundWorker2.RunWorkerAsync(appts);
            } 
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Appointment> appts = (List<Appointment>)e.Argument;
            try
            {
                foreach (object o in appts)
                {
                    Appointment appt = (Appointment)o;
                    if (appt.CloudWebQueueState == CREATE_NEW_APPT || appt.CloudWebQueueState == SYNCH_APPT)
                    {
                        backgroundWorker2.ReportProgress(2, "Retrieving Patient Record: " + appt.PatientName + " - " + appt.Dob);
                        XmlNode xml = cloud.FetchPatientRecord(appt.apptID);

                        MemoryStream stm = new MemoryStream();
                        StreamWriter stw = new StreamWriter(stm);
                        stw.Write(xml.OuterXml);
                        stw.Flush();
                        stm.Position = 0;

                        backgroundWorker2.ReportProgress(2, "Saving Data: " + appt.PatientName + " - " + appt.Dob);
                        DataContractSerializer ds = new DataContractSerializer(typeof(Patient));
                        Patient p2 = (Patient)ds.ReadObject(stm);

                        if (appt.CloudWebQueueState == CREATE_NEW_APPT)
                        {

                            SurveyResponse newMRN = (SurveyResponse)p2.SurveyReponses.Find(g => ((SurveyResponse)g).responseTag == "PatientReportedMRN");

                            if (!String.IsNullOrEmpty(newMRN.SurveyResponse_responseValue))
                            {
                                using (CloudMRNReconcile reconcileForm = new CloudMRNReconcile(newMRN.SurveyResponse_responseValue, p2.name, p2.apptid, appt.clinicID))
                                {
                                    reconcileForm.ShowDialog();

                                    if ((reconcileForm.retMRN != "Cancel") && (!String.IsNullOrEmpty(reconcileForm.retMRN)))
                                        p2.unitnum = reconcileForm.retMRN;
                                    else
                                        break;
                                }
                            }

                            Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
                            pc.Add("unitnum", p2.unitnum);
                            pc.Add("patientname", p2.name);
                            pc.Add("dob", p2.dob);
                            pc.Add("clinicId", appt.ClinicId);
                            object newApptId = BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_createMasteryAppointment", SqlDbType.Int, pc);
                            p2.apptid = (int)newApptId;
                        }
                        else
                        {
                            if (appt.CloudWebQueueState == SYNCH_APPT)
                            {
                                p2.apptid = appt.CloudWebQueueSynchId;
                            }
                        }
                        
                        p2.FHx.proband = p2;
                        p2.PersistFullObject(new RiskApps3.Model.HraModelChangedEventArgs(null));
      
                        if (checkBox4.Checked)
                        {
                            backgroundWorker2.ReportProgress(2, "Marking Complete and updating meta data: " + appt.PatientName + " - " + appt.Dob);
                            ParameterCollection pc = new ParameterCollection("apptID", p2.apptid);
                            BCDB2.Instance.RunSPWithParams("sp_markRiskDataCompleted", pc);
                        }

                        
                        if (checkBox1.Checked)
                        {
                            backgroundWorker2.ReportProgress(2, "Running Risk Models: " + appt.PatientName + " - " + appt.Dob);
                            p2.RecalculateRisk();

                            ParameterCollection pc = new ParameterCollection("unitnum", p2.unitnum);
                            BCDB2.Instance.RunSPWithParams("sp_3_populateBigQueue", pc);
                        }
                        
                        if (checkBox2.Checked)
                        {
                            backgroundWorker2.ReportProgress(2, "Printing Automation Documents: " + appt.PatientName + " - " + appt.Dob);

                            p2.RunAutomation();

                            //ParameterCollection printDocArgs = new ParameterCollection();
                            //printDocArgs.Add("apptid", p2.apptid);
                            //SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_AutomationHtmlDocsToPrint", printDocArgs);
                            //while (reader.Read())
                            //{
                            //    int templateID = -1;
                            //    if (reader.IsDBNull(0) == false)
                            //    {
                            //        templateID = reader.GetInt32(0);
                            //    }
                            //    SessionManager.Instance.SetActivePatientNoCallback(p2.unitnum, p2.apptid);
                            //    DocumentTemplate template = new DocumentTemplate();
                            //    template.documentTemplateID = templateID;
                            //    template.BackgroundLoadWork();
                            //    template.OpenHTML();
                            //    template.ProcessDocument();
                            //    htmlInProgress = true;
                            //    backgroundWorker2.ReportProgress(1, template.htmlText);
                            //    while (htmlInProgress)
                            //    {
                            //        Application.DoEvents();
                            //        Thread.Sleep(500);
                            //    }

                            //}
                        }
                        if (checkBox5.Checked)
                        {
                            backgroundWorker2.ReportProgress(2, "Removing Web Survey: " + appt.PatientName + " - " + appt.Dob);
                            backgroundWorker2.ReportProgress(4, appt);
                            cloud.DeleteSurvey(appt.apptID);
                        }
                        backgroundWorker2.ReportProgress(2, "Precessing Queue Documents: " + appt.PatientName + " - " + appt.Dob);
                        if (checkBox3.Checked)
                        {
                            ParameterCollection pc = new ParameterCollection("apptID", p2.apptid);
                            BCDB2.Instance.RunSPWithParams("sp_processQueueDocuments", pc);
                        }                        
                    }
                    else if (appt.CloudWebQueueState == DELETE_APPT)
                    {
                        backgroundWorker2.ReportProgress(2, "Removing Web Survey: " + appt.PatientName + " - " + appt.Dob);
                        backgroundWorker2.ReportProgress(4, appt);
                        cloud.DeleteSurvey(appt.apptID);
                    }
                    backgroundWorker2.ReportProgress(3,o);
                }
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog(exc.ToString());
            }
           
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                webBrowser1.DocumentText = (string)e.UserState;
            }
            else if (e.ProgressPercentage == 2)
            {
                label14.Text = (string)e.UserState;
            }
            else if (e.ProgressPercentage == 3)
            {
                fastDataListView1.SelectedObject = e.UserState;
                //fastDataListView1.GetItem(fastDataListView1.SelectedIndex).ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 4)
            {
                fastDataListView1.RemoveObject(e.UserState);
            }
        }

        private void DocLoadComplete(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Print();
            htmlInProgress = false;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label14.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            fastDataListView1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            Appointment appt = (Appointment)(e.Argument);
            e.Result = cloud.SurveySummary(appt.apptID);
        }
        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string html = (string)e.Result;
            WebSurveyPreview wsp = new WebSurveyPreview(html);
            wsp.ShowDialog();
            button1.Enabled = true;
            button2.Enabled = true;
            button4.Enabled = true;
            fastDataListView1.Enabled = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            fastDataListView1.Enabled = false;
            Appointment appt = (Appointment)(fastDataListView1.SelectedObject);
            if (appt != null)
            {
                backgroundWorker3.RunWorkerAsync(appt);
            }
        }
    }
}

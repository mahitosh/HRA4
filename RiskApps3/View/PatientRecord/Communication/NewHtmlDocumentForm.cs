using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic;
using RiskApps3.Model;
using RiskApps3.Model.Clinic.Letters;
using RiskApps3.Controllers;
using System.IO;
using BrightIdeasSoftware;
using HtmlAgilityPack;
using EvoPdf;
using Foxit.PDF.Printing;
using System.Drawing.Printing;
using RiskApps3.Utilities;
using System.Data.SqlClient;

namespace RiskApps3.View.PatientRecord.Communication
{
    public partial class NewHtmlDocumentForm : Form
    {
        DocumentTemplateList templates = new DocumentTemplateList(DocumentListType.RiskClinic);

        public string routine = "";

        bool docLoaded = false;

        string[] supported = new string[] { "surveySummary", "riskClinic", "LMN", "relativeLetter", "relativeKnownMutationLetter", "Screening" };

        public NewHtmlDocumentForm()
        {
            InitializeComponent();
            
        }

        private void NewHtmlDocumentForm_Load(object sender, EventArgs e)
        {
            templates.AddHandlersWithLoad(null, ListLoaded, null);
        }
        
        /**************************************************************************************************/
        private void ListLoaded(HraListLoadedEventArgs e)
        {
            //objectListView1.SetObjects(templates.Where(g => string.IsNullOrEmpty(((DocumentTemplate)g).htmlPath) == false));

            if (string.IsNullOrEmpty(routine)==false)
            {
                foreach (DocumentTemplate dt in templates)
                {
                    if (supported.Contains(dt.routineName))
                    {
                        if (string.Compare(routine, dt.routineName, true) == 0 || string.Compare(dt.routineName, "surveySummary", true) == 0)
                        {
                            objectListView2.AddObject(dt);
                        }
                        else
                        {
                            objectListView1.AddObject(dt);
                        }
                    }
                }
            }
            else
            {
                foreach (DocumentTemplate dt in templates)
                {
                    if (dt.suggested == 1 || string.Compare(dt.routineName, "surveySummary", true) == 0)
                    {
                        objectListView2.AddObject(dt);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dt.htmlPath) == false)
                        {
                            objectListView1.AddObject(dt);

                        }
                    }
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            CreateDocument(false);
             this.Close();
        }

        private void CreateDocument(bool print)
        {
            bool bInterface = false;
            bool bPacsFound = false;
            bool bPowerscribeFound = false;
            int interfaceId = -1;
            string interfaceType = "";
            string ipAddress = "";
            string port = "";
            string aeTitleRemote = "";
            string aeTitleLocal = "";
            string DCMTKpath = "";
            int pacsSubType = 1;

            ObjectListView olv = null;

            if (objectListView2.SelectedObject != null)
                olv = objectListView2;
            else if (objectListView1.SelectedObject != null)
                olv = objectListView1;

            if (olv != null)
            {
                DocumentTemplate dt = (DocumentTemplate)olv.SelectedObject;

                if (string.IsNullOrEmpty(dt.htmlPath))
                {
                    if (!string.IsNullOrEmpty(dt.documentFileName))
                    {
                        RiskAppCore.Globals.setApptID(SessionManager.Instance.GetActivePatient().apptid);
                        RiskAppCore.Globals.setUnitNum(SessionManager.Instance.GetActivePatient().unitnum);
                        FileInfo fInfo = dt.CalculateFileName(SessionManager.Instance.GetActivePatient().name,
                                       SessionManager.Instance.GetActivePatient().apptdatetime.ToShortDateString().Replace("/", "-"),
                                       SessionManager.Instance.GetActivePatient().apptid,
                                       SessionManager.Instance.GetActivePatient().unitnum,
                                       "doc", "");

                        LetterGenerator.Letter.generateDocFromTemplate(dt.documentFileName, dt.documentName,print);

                        RiskAppCore.Globals.setApptID(-1);
                        RiskAppCore.Globals.setUnitNum("");
                        return;
                    }
                }

                if (SessionManager.Instance.GetActivePatient() != null)
                {
                    FileInfo fInfo = dt.CalculateFileName(SessionManager.Instance.GetActivePatient().name,
                                                           SessionManager.Instance.GetActivePatient().apptdatetime.ToShortDateString().Replace("/", "-"),
                                                           SessionManager.Instance.GetActivePatient().apptid,
                                                           SessionManager.Instance.GetActivePatient().unitnum,
                                                           "html", "");

                    System.IO.File.WriteAllText(fInfo.FullName, dt.htmlText);

                    // Check to see if there's an interface we should send to... ONE PER DOCUMENT TEMPLATE!  If you want multiple interfaces, create multiple templates.
                    ParameterCollection pacsArgs = new ParameterCollection();
                    pacsArgs.Add("documentTemplateID", dt.documentTemplateID);
                    SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_getInterfaceDefinitionFromTemplateID", pacsArgs);
                    while (reader.Read())       // loop thru these rows and see what interfaces there are for this document
                    {
                        if (reader.IsDBNull(0) == false)
                        {
                            bInterface = true;
                            interfaceId = reader.GetInt32(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            interfaceType = reader.GetString(1);        // used in message below, this should be refactored, as multiple rows can be returned
                            if (interfaceType.ToUpper() == "PACS") bPacsFound = true;
                            if (interfaceType.ToUpper() == "POWERSCRIBE") bPowerscribeFound = true;
                        }
                        if (reader.IsDBNull(2) == false)
                        {
                            ipAddress = reader.GetString(2);    // these are mostly deprecated now, except the booleans, as this query now can return multiple rows, we don't need this stuff here.  jdg 8/21/15
                        }
                        if (reader.IsDBNull(3) == false)
                        {
                            port = reader.GetString(3);
                        }
                        if (reader.IsDBNull(4) == false)
                        {
                            aeTitleRemote = reader.GetString(4);
                        }
                        if (reader.IsDBNull(5) == false)
                        {
                            aeTitleLocal = reader.GetString(5);
                        }
                        if (reader.IsDBNull(6) == false)
                        {
                            DCMTKpath = reader.GetString(6);
                        }
                        if (reader.IsDBNull(7) == false)
                        {
                            pacsSubType = reader.GetInt32(7);
                        }

                    }

                    if ((print) || (bInterface))        // not mutually exclusive
                    {
                        try
                        {
                            if (print)
                            {
                                PrinterSettings settings = new PrinterSettings();
                                DocumentTemplate.Print(dt.htmlText,settings.PrinterName);
                            }

                            // Prompt to actually execute interfaces...
                            if (bInterface && (MessageBox.Show("Would you like to send this document to " + interfaceType + "?\n\nThis will take a few moments.", "Send Document to External System?", MessageBoxButtons.YesNo) == DialogResult.Yes))
                            {
                                if(bPacsFound)
                                {
                                    try
                                    {
                                        // jdg 8/21/15 let send2Pacs figure out what interfaces and file formats to send this thing to
                                        DocumentTemplate.ConvertToPdf(dt.htmlText, fInfo.FullName + ".pdf");
                                        RiskApps3.Utilities.InterfaceUtils.send2PACS(dt, SessionManager.Instance.GetActivePatient(), fInfo.FullName + ".pdf", SessionManager.Instance.GetActivePatient().apptid);
                                    }
                                    catch (Exception e)
                                    {
                                        Logger.Instance.WriteToLog("Failed to send PDF file to interface for appointment " + SessionManager.Instance.GetActivePatient().apptid + ", document template: " + dt.documentTemplateID + ".  Underlying error was: " + e.Message);
                                    }
                                }

                                //if (interfaceType.ToUpper() == "POWERSCRIBE")
                                if(bPowerscribeFound)
                                {
                                    InterfaceUtils.sendPowerscribe(dt);
                                }

                                // TODO:  Add future interfaces here
                            }
                            // end jdg 8/5/15


                        }
                        catch (Exception e)
                        {
                            Logger.Instance.WriteToLog(e.ToString());
                        }
                    }

                    ParameterCollection pc = new ParameterCollection();
                    pc.Add("apptid", SessionManager.Instance.GetActivePatient().apptid);
                    pc.Add("templateID", dt.documentTemplateID);
                    pc.Add("dateTime", DateTime.Now);
                    pc.Add("userlogin", SessionManager.Instance.ActiveUser.userLogin);

                    string sqlStr = "INSERT INTO tblDocuments([apptID],[documentTemplateID],[created],[createdBy]) VALUES(@apptid, @templateID, @dateTime, @userlogin);";
                    BCDB2.Instance.ExecuteNonQueryWithParams(sqlStr, pc);

                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            DocumentTemplate dt = (DocumentTemplate)e.Argument;
            DocumentArgs da = new DocumentArgs();

            if (SessionManager.Instance.GetActivePatient() != null)
            {
                dt.ProcessDocument();
            }
            e.Result = dt;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                DocumentTemplate dt = (DocumentTemplate)e.Result;
                
                webBrowser1.DocumentText = dt.htmlText;

                splitContainer2.Enabled = true;
            }
        }
        /**************************************************************************************************/
        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void objectListView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //objectListView1.SelectedObjects.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void objectListView2_CellClick(object sender, CellClickEventArgs e)
        {
            

            if (objectListView2.SelectedObject != null)
            {
                if (objectListView2.Tag == null || objectListView2.SelectedObject != objectListView2.Tag)
                {
                    objectListView1.Tag = null;
                    objectListView1.SelectedObject = null;                                  // jdg 8/12/15
                    DocumentTemplate dt = (DocumentTemplate)objectListView2.SelectedObject;

                    if (string.IsNullOrEmpty(dt.htmlPath))
                    {
                        if (!string.IsNullOrEmpty(dt.documentFileName))
                        {
                            webBrowser1.DocumentText = "Preview is not available for Word documents";
                        }
                    }
                    else
                    {
                        dt.SetPatient(SessionManager.Instance.GetActivePatient());
                        dt.OpenHTML();
                        webBrowser1.DocumentText = dt.htmlText;
                        objectListView2.Tag = objectListView2.SelectedObject;
                        groupBox1.Text = "Preview - " + dt.documentName;
                        splitContainer2.Enabled = false;
                        backgroundWorker1.RunWorkerAsync(dt);
                    }
                }
            }
        }

        private void objectListView1_CellClick(object sender, CellClickEventArgs e)
        {


            if (objectListView1.SelectedObject != null)
            {
                if (objectListView1.Tag == null || objectListView1.SelectedObject != objectListView1.Tag)
                {
                    objectListView2.Tag = null;
                    objectListView2.SelectedObject = null;                                      // jdg 8/12/15
                    DocumentTemplate dt = (DocumentTemplate)objectListView1.SelectedObject;
                    if (string.IsNullOrEmpty(dt.htmlPath))
                    {
                        if (!string.IsNullOrEmpty(dt.documentFileName))
                        {
                            webBrowser1.DocumentText = "Preview is not available for Word documents";
                        }
                    }
                    else
                    {
                        dt.SetPatient(SessionManager.Instance.GetActivePatient());
                        dt.OpenHTML();
                        webBrowser1.DocumentText = dt.htmlText;
                        objectListView1.Tag = objectListView1.SelectedObject;
                        groupBox1.Text = "Preview - " + dt.documentName;
                        while (backgroundWorker1.IsBusy)
                        {
                            Application.DoEvents();
                        }
                        splitContainer2.Enabled = false;
                        backgroundWorker1.RunWorkerAsync(dt);
                    }
                }
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            CreateDocument(true);
            this.Close();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.IO;
using RiskApps3.Model.Clinic.Letters.HraLetterTags;
using RiskApps3.Model.Clinic.Letters;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Specialized;
using HtmlAgilityPack;
using RiskApps3.Model.Clinic.HraLetterTags;
using RiskApps3.Model.PatientRecord;
using EvoPdf;
using Foxit.PDF.Printing;

namespace RiskApps3.Model.Clinic
{
    public enum DocumentListType {All,RiskClinic};

    /**********************************************************************/
    public class DocumentTemplateList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;
        private DocumentListType my_type = DocumentListType.All;

        public DocumentTemplateList(DocumentListType listType)
        {
            constructor_args = new object[] { };
            my_type = listType;
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            
            if (my_type == DocumentListType.RiskClinic)
            {
                Patient proband = RiskApps3.Controllers.SessionManager.Instance.GetActivePatient();
                if (proband != null)
                {
                    pc.Add("unitnum", proband.unitnum);
                }
                LoadListArgs lla = new LoadListArgs("sp_3_LoadRiskClinicDocumentTemplates",
                                                    pc,
                                                    typeof(DocumentTemplate),
                                                    constructor_args);
                DoListLoad(lla);
                foreach(DocumentTemplate dt in this)
                {
                    if (string.IsNullOrEmpty(dt.relativeName)==false)
                    {
                        dt.documentName += (" for " + dt.relativeName);
                    }
                }
            }
            else
            {
                LoadListArgs lla = new LoadListArgs("sp_3_LoadDocumentTemplates",
                                                    pc,
                                                    typeof(DocumentTemplate),
                                                    constructor_args);
                DoListLoad(lla);
            }
        }

        
    }
    /**********************************************************************/
    public class DocumentTemplate : HraObject
    {
        private Patient proband;

        public int documentTemplateID;

        #region HraAttributes
        [HraAttribute]
        public string documentName;
        [HraAttribute]
        public string documentFileName;
        [HraAttribute]
        public string diseaseName;
        [HraAttribute]
        public string providerID;
        [HraAttribute]
        public bool batchPrint;
        [HraAttribute]
        public bool autoPrint;
        [HraAttribute]
        public string conditionSQL;
        [HraAttribute]
        public bool surgeryPrint;
        [HraAttribute]
        public int printCopies;
        [HraAttribute]
        public int forWebSubmission;
        [HraAttribute]
        public int highRisk;
        [HraAttribute]
        public string area;
        [HraAttribute]
        public string emailSubject;
        [HraAttribute]
        public string emailFile;
        [HraAttribute]
        public string routineName;
        [HraAttribute]
        public string saveLocation;
        [HraAttribute]
        public string standAlone;
        [HraAttribute]
        public string defaultBehavior;
        [HraAttribute]
        public bool autoSave;
        [HraAttribute]
        public string htmlPath;
        #endregion

        public string htmlText = "";
        public HtmlDocument doc;
        public List<DocumentSection> Sections;
        public bool UseDocArgs = true;
        public int relativeID = -1;
        public string relativeName = "";
        public int suggested = 0;

        /*********************************************************/
        public void SetPatient(Patient p)
        {
            proband = p;
        }

        /*********************************************************/
        public void OpenHTML()
        {
            if (string.IsNullOrEmpty(htmlPath) == false)
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(htmlPath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }
                htmlText = sb.ToString();
            }
        }
        /*********************************************************/
        public FileInfo CalculateFileName(string patientName, string date, int apptid, string unitnum, string extension, string directory)
        {

            StringBuilder sbFname = new StringBuilder();
            string[] nameParts = patientName.Split(new char[] { ',', '^' });
            string fname;
            string[] filenameParts = new string[] { };
            NameValueCollection values = Configurator.GetConfig("AppSettings");
            string schema = values["DocumentFilenameSchema"];
            if(!String.IsNullOrEmpty(schema)) filenameParts = schema.Split(new char[] {'|'});

            string dirName = directory;
            if (string.IsNullOrEmpty(directory))
            {
                dirName = getLetterDirectory(apptid, unitnum, patientName);
            }
            // jdg 8/25/15
            if (filenameParts.Length > 0)
            {
                foreach (string part in filenameParts)
                {
                    switch (part.ToUpper())
                    {
                        case "LASTNAME":
                            sbFname.Append(String.IsNullOrEmpty(nameParts[0]) ? "" : nameParts[0] + "_");    
                            break;
                        case "FIRSTNAME":
                            sbFname.Append(String.IsNullOrEmpty(nameParts[1]) ? "" : nameParts[1] + "_");
                            break;
                        case "PATIENTNAME":
                            sbFname.Append(patientName + "_");
                            break;
                        case "DATE":
                            sbFname.Append(date + "_");
                            break;
                        case "UNITNUM":
                            sbFname.Append(unitnum + "_");
                            break;
                        case "DOCUMENTNAME":
                            sbFname.Append(documentName + "_");
                            break;
                        default:
                            Logger.Instance.WriteToLog("LETTER GENERATION: your config.xml or cloudConfig file contains the unknown filename part you called: " + part + " in the appSetting DocumentFilenameSchema.");
                            break;
                    }
                }
                // now erase the last underscore, if needed, and append the extension.
                fname = sbFname.ToString();
                fname = fname.TrimEnd(new char[] { '_' }) + "." + extension;
            }
            else
            {
                fname = patientName + "_" + date + "_" + unitnum + "_" + documentName + "." + extension; // default, as always... jdg 8/25/15
            }
            int count = 1;
            string fileName = fname;
            string[] fileNameSplit = fileName.Split(new[] { '.' });
            string ext = "." + fileNameSplit[fileNameSplit.Length - 1];
            string prefix = fileName.Substring(0, fileName.Length - ext.Length);
            while (File.Exists(dirName + fileName))
            {
                fileName = prefix + "[" + count + "]" + ext;
                count++;
            }
            return new FileInfo(dirName + fileName);
        }
        /*********************************************************/
        public static string getLetterDirectory(int apptid, string unitnum, string patientname)
        {
            string letterDirectory = "";

            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand cmdProcedure = new SqlCommand("sp_getProviderDocStorageLocation", connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;
                cmdProcedure.CommandTimeout = 300; //change command timeout from default to 5 minutes
                cmdProcedure.Parameters.Add("@apptID", SqlDbType.Int);
                cmdProcedure.Parameters["@apptID"].Value = apptid;
                try
                {
                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);
                    if (reader.Read())
                    {
                        letterDirectory = (reader.GetValue(0).ToString());
                        letterDirectory = letterDirectory.Trim();
                    }

                    reader.Close();
                }
                catch (Exception exc)
                {
                    Logger.Instance.WriteToLog("[DoLoadWithSpAndParams] Executing Stored Procedure - " + exc.ToString());
                    //Console.WriteLine(exc.StackTrace);
                }
            }

            if (String.IsNullOrEmpty(letterDirectory))
            {
                NameValueCollection values = Configurator.GetConfig("globals");
                letterDirectory = values["DocumentStorage"];
            }

            //if letter directory doesn't exist, we need to create it
            DirectoryInfo di = Directory.CreateDirectory(letterDirectory);

            DirectoryInfo[] targets = di.GetDirectories("*_" + unitnum);
            if (targets.Length > 0)
            {
                foreach (DirectoryInfo sub in targets)
                {
                    letterDirectory = sub.FullName;
                }
            }
            else
            {
                letterDirectory = Path.Combine(letterDirectory, patientname + "_" + unitnum);
                di = Directory.CreateDirectory(letterDirectory);
            }

            if (letterDirectory.EndsWith("\\") == false)
            {
                letterDirectory = letterDirectory + "\\";
            }
            return letterDirectory;
        }
        /*********************************************************/
        public void ProcessDocument()
        {
            doc = new HtmlDocument();
            doc.LoadHtml(htmlText);

            StampAuthorAndTime();

            ParagraphRuleTag prt = new ParagraphRuleTag();
            if (proband != null)
                prt.proband = proband;
            prt.ProcessHtml(ref doc);

            if (UseDocArgs)
            {
                DocumentArgs da = new DocumentArgs();
                da.relativeID = relativeID;
                if (proband != null)
                    da.proband = proband;
                da.ProcessHtml(ref doc);
            }

            //start the tag replacement
            ImageTag it = new ImageTag();
            it.DocumentHtmlPath = htmlPath;
            it.ProcessHtml(ref doc);

            SqlExecTag exec = new SqlExecTag();
            exec.ProcessHtml(ref doc);

            MakeTableTag mt = new MakeTableTag();
            mt.ProcessHtml(ref doc);

            MakeListTag ml = new MakeListTag();
            ml.ProcessHtml(ref doc);

            MakeResponseTableTag mrt = new MakeResponseTableTag();
            mrt.ProcessHtml(ref doc);

            PedigreeTag pt = new PedigreeTag();
            if (proband != null)
                pt.proband = proband;
            pt.ProcessHtml(ref doc);

            RiskChartTag rct = new RiskChartTag();
            if (proband != null)
                rct.proband = proband;
            rct.ProcessHtml(ref doc);

            AreaProvidersTag apt = new AreaProvidersTag();
            if (proband != null)
                apt.proband = proband;
            apt.ProcessHtml(ref doc);

            Sectionizer sz = new Sectionizer();
            Sections = sz.ProcessHtml(ref doc);


            TableColumnTag tc = new TableColumnTag();
            if (proband != null)
                tc.proband = proband;
            tc.ProcessHtml(ref doc);

            InsertTemplateTag itt = new InsertTemplateTag();
            itt.UseDocArgs = UseDocArgs;
            if (proband != null)
                itt.proband = proband;
            itt.ProcessHtml(ref doc);

            htmlText = ProcessFormatingTags(doc.DocumentNode.OuterHtml);

        }



        public static void GetPageParts(string htmlString, out string header, out string footer, out string body, out int header_height, out int footer_height)
        {
            header = "";
            footer = "";
            body = "";
            header_height = 0;
            footer_height = 0;

            if (string.IsNullOrEmpty(htmlString))
                return;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            HtmlNode headerNode = FindNodeByAttribute(doc.DocumentNode, "pageheader", "true");
            HtmlNode footerNode = FindNodeByAttribute(doc.DocumentNode, "pagefooter", "true"); ;
            HtmlNode bodyNode = doc.DocumentNode;

            if (headerNode != null)
            {
                header = headerNode.OuterHtml;
                headerNode.ParentNode.RemoveChild(headerNode);
                HtmlAttribute StyleAttr = headerNode.Attributes["style"];
                if (StyleAttr != null)
                {
                    string[] styleParts = StyleAttr.Value.Split(new char[] { ';' });
                    foreach (string s in styleParts)
                    {
                        if (s.ToLower().Contains("height"))
                        {
                            string h = s.ToLower().Replace("height", "");
                            h = h.Replace(':', ' ');
                            h = h.Replace("px", "");
                            int.TryParse(h, out header_height);
                        }
                    }
                }
            }
            if (footerNode != null)
            {
                footer = footerNode.OuterHtml;
                footerNode.ParentNode.RemoveChild(footerNode);
                HtmlAttribute StyleAttr = footerNode.Attributes["style"];
                if (StyleAttr != null)
                {
                    string[] styleParts = StyleAttr.Value.Split(new char[] { ';' });
                    foreach (string s in styleParts)
                    {
                        if (s.ToLower().Contains("height"))
                        {
                            string h = s.ToLower().Replace("height", "");
                            h = h.Replace(':', ' ');
                            h = h.Replace("px", "");
                            int.TryParse(h, out footer_height);
                        }
                    }
                }
            }
            body = doc.DocumentNode.OuterHtml;
        }

        public static HtmlNode FindNodeByAttribute(HtmlNode node, string attrName, string attrVal)
        {
            HtmlNode retval = null;

            HtmlAttribute attr = node.Attributes[attrName];
            if (attr != null)
            {
                if (string.Compare(attr.Value, attrVal, true) == 0)
                {
                    retval = node;
                }
            }
            if (retval == null)
            {
                foreach (HtmlNode child in node.ChildNodes)
                {
                    retval = FindNodeByAttribute(child, attrName, attrVal);
                    if (retval != null)
                        break;
                }
            }
            return retval;
        }

        private void StampAuthorAndTime()
        {
            //HtmlNode n = doc.DocumentNode.SelectSingleNode("/html/head");
            //HtmlNode author = new HtmlNode(HtmlNodeType.Comment, doc, 0);
            //author.InnerHtml = Environment.UserName;
            //n.ChildNodes.Add(author);
        }

        private string ProcessFormatingTags(string p)
        {
            p = p.Replace("<NL>", "<BR>");
            return p;
        }
        /*********************************************************/
        public void UpdateDocFromText()
        {
            doc = new HtmlDocument();
            doc.LoadHtml(htmlText);

            Sectionizer sz = new Sectionizer();
            Sections = sz.ProcessHtml(ref doc);


        }
        /*********************************************************/
        public void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("documentTemplateID", documentTemplateID);

            DoLoadWithSpAndParams("sp_3_LoadDocumentTemplateByID", pc);
        }

        internal void AddDiv(string patients_html)
        {

            int insert = htmlText.LastIndexOf("</div>");
            if (insert > 0)
            {
                htmlText = htmlText.Insert(insert + 6, patients_html);
            }
            UpdateDocFromText();
        }

        internal string GetTableDiv(string name)
        {
            return @"
                <div class=""avoid-break"" id=""" + name + @""">
                        <table class=""summaryTable"" >
                               <col width=""250"">
                                <col width=""50"">
                                <col width=""500"">
                            <tr>
                                <td align=""center""; class =""header1""; colspan=""2"">
                                    " + name + @"</td>
                            </tr>
                        </table>
                </div>";
        }


        internal string InsertTwoColumnLeftRightTableRow(string table, string metric, string p)
        {
            string retval = table;
            int insert = table.LastIndexOf("</tr>");
            if (insert > 0)
            {
                retval = retval.Insert(insert, @"</tr>
                     <tr class =""regular1"">
                        <td >
                            " + metric + @"</td>
                        <td align=""right"">
                            " + p + @"</td>
                    ");
            }
            return retval;
        }

        internal void SetDivVis(string p, bool p_2)
        {
            HtmlNode node = FindNodeByID(doc.DocumentNode, p);
            node.Attributes.Add("style", "dispaly: none;");
            node.ChildNodes.Clear();
            //htmlText = doc.DocumentNode.OuterHtml;
        }

        public HtmlNode FindNodeByID(HtmlNode node, string id)
        {
            HtmlNode retval = null;

            if (string.Compare(node.Id, id, true) == 0)
            {
                retval = node;
            }
            else
            {
                foreach (HtmlNode child in node.ChildNodes)
                {
                    if (retval == null)
                    {
                        retval = FindNodeByID(child, id);
                    }
                }
            }
            return retval;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(documentName))
                return "";
            else
                return documentName;
        }
        public HtmlNode FindNodeByName(HtmlNode node, string name)
        {
            HtmlNode retval = null;

            if (string.Compare(node.Name, name, true) == 0)
            {
                retval = node;
            }
            else
            {
                foreach (HtmlNode child in node.ChildNodes)
                {
                    if (retval == null)
                    {
                        retval = FindNodeByName(child, name);
                    }
                }
            }
            return retval;
        }
        public static HtmlToPdfConverter GetInitializedHtmlConverter(string html_string, out string html_body)
        {
            
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            htmlToPdfConverter.PdfDocumentOptions.Width = 750;
            htmlToPdfConverter.HtmlViewerWidth = 650;
            htmlToPdfConverter.PdfDocumentOptions.LeftMargin = 50;
            htmlToPdfConverter.PdfDocumentOptions.RightMargin = 50;
            htmlToPdfConverter.PdfDocumentOptions.TopMargin = 25;
            htmlToPdfConverter.PdfDocumentOptions.BottomMargin = 10;
            htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;
            htmlToPdfConverter.PdfDocumentOptions.AvoidTextBreak = true;
            htmlToPdfConverter.PdfDocumentOptions.JpegCompressionLevel = 0;

            // Install a handler where to change the header and footer in first page
            htmlToPdfConverter.PrepareRenderPdfPageEvent += new PrepareRenderPdfPageDelegate(htmlToPdfConverter_PrepareRenderPdfPageEvent);
    
            html_body = "";
            string header = "";
            string footer = "";
            int header_height = 0;
            int footer_height = 0;
            DocumentTemplate.GetPageParts(html_string, out header, out footer, out html_body, out header_height, out footer_height);

            if (!string.IsNullOrEmpty(header))
            {
                htmlToPdfConverter.PdfDocumentOptions.ShowHeader = true;
                htmlToPdfConverter.PdfHeaderOptions.HeaderHeight = header_height;
                HtmlToPdfElement headerElem = new HtmlToPdfElement(0, 0, 0, header, "", 680);
                headerElem.FitHeight = true;
                htmlToPdfConverter.PdfHeaderOptions.AddElement(headerElem);
            }
            if (!string.IsNullOrEmpty(footer))
            {
                htmlToPdfConverter.PdfDocumentOptions.ShowFooter = true;
                HtmlToPdfElement footerElem = new HtmlToPdfElement(footer, "");
                if (footer_height > 0)
                    htmlToPdfConverter.PdfFooterOptions.FooterHeight = footer_height;
                else
                    footerElem.FitHeight = true;
                htmlToPdfConverter.PdfFooterOptions.AddElement(footerElem);
            }

            htmlToPdfConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
            htmlToPdfConverter.PdfDocumentOptions.AvoidImageBreak = true;

            return htmlToPdfConverter;
        }
        public static void Print(string html_string, string printername)
        {
             
            try
            {
                string body = "";
                HtmlToPdfConverter converter = GetInitializedHtmlConverter(html_string, out body);
                byte[] outPdfBuffer = converter.ConvertHtml(body, "");
                
                InputPdf inputPdf = new InputPdf(outPdfBuffer);
                //PrinterSettings settings = new PrinterSettings();
                PrintJob printJob = new PrintJob(printername, inputPdf);
                PrintJob.AddLicense("FPM20NXDLB2DHPnggbYuVwkquSU3u2ffoA/Pgph4rjG5wiNCxO8yEfbLf2j90rZw1J3VJQF2tsniVvl5CxYka6SmZX4ak6keSsOg");
                printJob.PrintOptions.Scaling = new AutoPageScaling();
                printJob.Print();
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog(ee.ToString());
            }
        }

        public static int ConvertToPdf(string html_string, string name)
        {
            int retval = 0;

            try
            {
                string body = "";
                HtmlToPdfConverter converter = GetInitializedHtmlConverter(html_string, out body);
                converter.ConvertHtmlToFile(body, "", name);
            }
            catch (Exception e)
            {
                retval = 1;
                Logger.Instance.WriteToLog(e.ToString());
            }

            return retval;
        }

        public static byte[] ConvertToPdfBuffer(string html_string)
        {
            byte[] retval = null;

            try
            {
                string body = "";
                HtmlToPdfConverter converter = GetInitializedHtmlConverter(html_string, out body);
                retval = converter.ConvertHtml(body, "");
            }
            catch (Exception e)
            {

                Logger.Instance.WriteToLog(e.ToString());
            }

            return retval;
        }


        static void htmlToPdfConverter_PrepareRenderPdfPageEvent(PrepareRenderPdfPageParams eventParams)
        {
            if (eventParams.PageNumber == 1)
            {
                PdfPage page = eventParams.Page;
                page.ShowHeader = false;
            }
        }
    }

    /**********************************************************************/
    public class DocumentSection
    {
        public HtmlNode SectionNode = null;
        public string title = "";
    }
}

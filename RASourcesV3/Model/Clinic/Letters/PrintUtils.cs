using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Drawing;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using LetterGenerator;
using EvoPdf;
using System.IO;
using System.Xml;

namespace RiskApps3.Model.Clinic.Letters
{
    public class PrintUtils
    {
        string printer;
        string defaultPrinter;
        System.Drawing.Printing.PrinterSettings settings;

        public PrintUtils()
        {
            settings = new System.Drawing.Printing.PrinterSettings();
            defaultPrinter = settings.PrinterName;
            printer = defaultPrinter;
        }
        public PrintUtils(string printer)
        {
            settings = new System.Drawing.Printing.PrinterSettings();
            defaultPrinter = settings.PrinterName;

            if (string.IsNullOrEmpty(printer))
            {
                this.printer = string.Empty;
            }
            else if (printer == "*")
            {
                this.printer = defaultPrinter;
            }
            else
            {
                this.printer = printer;
            }
        }
        public string getPrinter()
        {
            return printer;
        }
        public HraHtmlDocument printHtmlDoc(int apptID, string unitnum, int templateID)
        {
            if (string.IsNullOrEmpty(printer) || printer.ToUpper() == "BATCH")
            {
                return null;
            }

            HraHtmlDocument hdoc = null;
            try
            {
                SessionManager.Instance.SetActivePatient(unitnum, apptID);

                hdoc = new HraHtmlDocument(templateID, unitnum, apptID);
                if (hdoc == null)
                {
                    Logger.Instance.WriteToLog("RiskApps3Automation.HtmlDocument(" + templateID.ToString() + ", " + apptID.ToString() + ", \"" + unitnum + "\") returned null");
                    return null;
                }
                hdoc.targetPrinter = printer;

                hdoc.apptid = apptID;
                if (printer.ToLower() != "batch")
                {
                    hdoc.Print();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("printHtmlDoc(" + apptID.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ") " + e.ToString());
                return null;
            }
            return hdoc;
        }
        public bool printWordDoc(int apptID, string unitnum, int templateID)
        {
            if (string.IsNullOrEmpty(printer) || printer.ToUpper() == "BATCH")
            {
                return false;
            }

            try
            {
                RiskAppCore.Globals.setApptID(apptID);
                RiskAppCore.Globals.setUnitNum(unitnum);

                string path = getDocumentFileName(templateID);
                string name = getDocumentName(templateID);

                Letter.printDocFromTemplate(path, name, printer);

                RiskAppCore.Globals.setApptID(-1);
                RiskAppCore.Globals.setUnitNum("");

            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("printWordDoc(" + apptID.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ") " + e.ToString());
                return false;
            }
            return true;
        }
        public bool saveHtmlDoc(int apptID, string unitnum, int templateID, string directory)
        {
            try
            {
                SessionManager.Instance.SetActivePatient(unitnum, apptID);

                HraHtmlDocument hdoc = new HraHtmlDocument(templateID, unitnum, apptID);
                if (hdoc == null)
                {
                    Logger.Instance.WriteToLog("RiskApps3Automation.HtmlDocument(" + templateID.ToString() + ", " + apptID.ToString() + ", \"" + unitnum + "\") returned null");
                    return false;
                }
                hdoc.apptid = apptID;
                hdoc.Save(directory);
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("saveHtmlDoc(" + apptID.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ", \"" + directory + "\") " + e.ToString());
                return false;
            }
            return true;
        }
        public bool savePdfDoc(int apptID, string unitnum, int templateID, string directory)
        {
            try
            {
                SessionManager.Instance.SetActivePatient(unitnum, apptID);
                directory = Configurator.GetDocumentStorage();   // override save location with config.xml
                if (directory.Substring(directory.Length - 1) != "\\") directory = directory + "\\";
                string templateDirectory = Configurator.GetDocumentTemplateStorage();   // override save location with config.xml
                if (templateDirectory.Substring(templateDirectory.Length - 1) != "\\") templateDirectory = templateDirectory + "\\";

                HraHtmlDocument hdoc = new HraHtmlDocument(templateID, unitnum, apptID);
                if (hdoc == null)
                {
                    Logger.Instance.WriteToLog("RiskApps3Automation.HtmlDocument(" + templateID.ToString() + ", " + apptID.ToString() + ", \"" + unitnum + "\") returned null");
                    return false;
                }
                hdoc.apptid = apptID;

                FileInfo fInfo = hdoc.template.CalculateFileName(SessionManager.Instance.GetActivePatient().name,
                                        SessionManager.Instance.GetActivePatient().apptdatetime.ToShortDateString().Replace("/", "-"),
                                        SessionManager.Instance.GetActivePatient().apptid,
                                        SessionManager.Instance.GetActivePatient().unitnum,
                                        "pdf", directory);

                string PdfFileName = fInfo.FullName;

                DocumentTemplate.ConvertToPdf(hdoc.template.htmlText,PdfFileName);

            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("saveHtmlDoc(" + apptID.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ", \"" + directory + "\") " + e.ToString());
                return false;
            }
            return true;
        }
        public  bool saveWordDoc(int apptID, string unitnum, int templateID, string directory)
        {
            try
            {
                RiskAppCore.Globals.setApptID(apptID);
                RiskAppCore.Globals.setUnitNum(unitnum);

                string path = getDocumentFileName(templateID);
                string name = getDocumentName(templateID);
                string dir = string.IsNullOrEmpty(directory) ? getProviderDirectory(apptID) : directory;

                if (string.IsNullOrEmpty(path) == false && string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(dir) == false)
                {
                    Letter.saveDocFromTemplate(path, name, dir);
                }

                RiskAppCore.Globals.setApptID(-1);
                RiskAppCore.Globals.setUnitNum("");
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("saveWordDoc(" + apptID.ToString() + ", \"" + unitnum + "\", " + templateID.ToString() + ", \"" + directory + "\") " + e.ToString());
                return false;
            }
            return true;
        }
        private string getDocumentName(int templateID)
        {
            string documentName = string.Empty;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("templateID", templateID);
            SqlDataReader reader = BCDB2.Instance.ExecuteReaderWithParams("select documentName from lkpDocumentTemplates where documentTemplateID = @templateID", pc);
            if (reader != null)
            {
                if (reader.Read() && reader.IsDBNull(0) == false)
                {
                    documentName = reader.GetString(0);
                }
                reader.Close();
            }

            return documentName;
        }
        private string getDocumentFileName(int templateID)
        {
            string documentFileName = string.Empty;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("templateID", templateID);
            string sqlStr = "select documentFileName from lkpDocumentTemplates where documentTemplateID = @templateID";
            using (SqlDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc))
            {
                if (reader.Read() && reader.IsDBNull(0) == false)
                {
                    documentFileName = reader.GetString(0);
                }
            }

            return documentFileName;
        }
        private string getHtmlPath(int templateID)
        {
            string htmlPath = string.Empty;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("templateID", templateID);
            string sqlStr = "select htmlPath from lkpDocumentTemplates where documentTemplateID = @templateID";
            using (SqlDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc))
            {
                if (reader.Read() && reader.IsDBNull(0) == false)
                {
                    htmlPath = reader.GetString(0);
                }
            }

            return htmlPath;
        }
        private string getProviderDirectory(int apptID)
        {
            string providerDirectory = string.Empty;

            ParameterCollection pc = new ParameterCollection();
            pc.Add("apptID", apptID);
            using (SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_getProviderDocStorageLocation", pc))
            {
                if (reader.Read() && reader.IsDBNull(0) == false)
                {
                    providerDirectory = reader.GetString(0);
                }
            }
            return providerDirectory;
        }
    }
}

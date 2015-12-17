using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.Model.Clinic;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.FHx;
using System.Reflection;
using System.Drawing.Printing;
using EvoPdf;
using HtmlAgilityPack;

using Foxit.PDF.Printing;
using System.Management;

namespace RiskApps3.Model.Clinic.Letters
{
    public class HraHtmlDocument
    {
        public string targetPrinter;
        public int apptid;
        public DocumentTemplate template;

        private List<Image> pages;
        private int pageIndex = 0;

        string preamble = @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""> " + Environment.NewLine +
                          @"<html xmlns=""http://www.w3.org/1999/xhtml"">" + Environment.NewLine;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="unitnum"></param>
        /// <param name="apptid"></param>
        public HraHtmlDocument(int documentID, string unitnum, int apptid)
        {
            CreateHtmlDocument(documentID, unitnum, apptid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentID"></param>
        /// <param name="unitnum"></param>
        /// <param name="apptid"></param>
        /// 
        public void CreateHtmlDocument(int documentID, string unitnum, int apptid)
        {
            template = new DocumentTemplate();
            template.SetPatient(SessionManager.Instance.GetActivePatient());
            template.documentTemplateID = documentID;
            template.BackgroundLoadWork();
            template.OpenHTML();
            template.ProcessDocument();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            Save("");
        }
        public void Save(string directory)
        {
            if (template == null)
                return;

            if (string.IsNullOrEmpty(this.template.htmlText))
                this.template.htmlText = ""; //return; ????

            FileInfo fInfo = template.CalculateFileName(SessionManager.Instance.GetActivePatient().name,
                                                    SessionManager.Instance.GetActivePatient().apptdatetime.ToShortDateString().Replace("/", "-"),
                                                    SessionManager.Instance.GetActivePatient().apptid,
                                                    SessionManager.Instance.GetActivePatient().unitnum,
                                                    "html", directory);


            System.IO.File.WriteAllText(fInfo.FullName, this.template.htmlText);
        }  

        /// <summary>
        /// 
        /// </summary>
        public void Print()
        {
            DocumentTemplate.Print(template.htmlText, targetPrinter);
        }
    }
}

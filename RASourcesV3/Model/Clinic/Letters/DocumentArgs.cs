using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using RiskApps3.Controllers;
using System.Text.RegularExpressions;
using RiskApps3.Model.PatientRecord;
using HtmlAgilityPack;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Letters
{
    public class DocumentArgs
    {
        public int apptid = -1;
        public string unitnum = "";
        public int surgeryID = -1;
        public Patient proband;
        public int relativeID = -1;

        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            if (proband == null)
            {
                if (SessionManager.Instance.GetActivePatient() != null)
                {
                    apptid = SessionManager.Instance.GetActivePatient().apptid;
                    unitnum = SessionManager.Instance.GetActivePatient().unitnum;
                }
            }
            else
            {
                apptid = proband.apptid;
                unitnum = proband.unitnum;
            }

            try
            {
                FindAndReplace(doc.DocumentNode);
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        private void FindAndReplace(HtmlNode htmlNode)
        {
            if (htmlNode.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)htmlNode;
                tn.InnerHtml = Regex.Replace(tn.InnerHtml, "#apptid#", apptid.ToString(), RegexOptions.IgnoreCase);
                tn.InnerHtml = Regex.Replace(tn.InnerHtml, "#unitnum#", unitnum, RegexOptions.IgnoreCase);
                tn.InnerHtml = Regex.Replace(tn.InnerHtml, "#relativeID#", relativeID.ToString(), RegexOptions.IgnoreCase);
            }
            else
            {
                foreach(HtmlAttribute attr in htmlNode.Attributes)
                {
                    attr.Value = Regex.Replace(attr.Value, "#apptid#", apptid.ToString(), RegexOptions.IgnoreCase);
                    attr.Value = Regex.Replace(attr.Value, "#unitnum#", unitnum, RegexOptions.IgnoreCase);
                    attr.Value = Regex.Replace(attr.Value, "#relativeID#", relativeID.ToString(), RegexOptions.IgnoreCase);
                }
            }
            foreach (HtmlNode child in htmlNode.ChildNodes)
            {
                FindAndReplace(child);
            }
        }
    }
}

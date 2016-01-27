using HtmlAgilityPack;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    class InsertTemplateTag : HraLetterTag
    {
        public readonly string HraTagText = "[InsertTemplate(";
        public readonly string HraTagSuffix = ")]";

        public Patient proband;
        public bool UseDocArgs = true;

        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            try
            {
                //find all make table nodes
                List<HtmlTextNode> sqlExecNodes = new List<HtmlTextNode>();
                FindSqlExecNodes(doc.DocumentNode, ref sqlExecNodes);

                foreach (HtmlTextNode sqlNode in sqlExecNodes)
                {
                    try
                    {
                        GetDataAndReplaceTag(sqlNode);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteToLog(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /**********************************************************************/
        private void FindSqlExecNodes(HtmlNode node, ref List<HtmlTextNode> sqlExecNodes)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)node;
                if (tn.Text.Contains(HraTagText))
                {
                    sqlExecNodes.Add(tn);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindSqlExecNodes(child, ref sqlExecNodes);
            }
        }

        /**********************************************************************/
        private void GetDataAndReplaceTag(HtmlTextNode tableNode)
        {

            int pos = 0;
            int suffixPos = -1;

            while (pos < tableNode.Text.Length && pos >= 0)
            {
                pos = tableNode.Text.IndexOf(HraTagText, pos);
                if (pos >= 0)
                {
                    suffixPos = tableNode.Text.IndexOf(HraTagSuffix, pos);
                    if (pos >= 0 && suffixPos > pos)
                    {
                        string tag = tableNode.Text.Substring(pos, suffixPos + HraTagSuffix.Length - pos);
                        string args = tableNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);
                        int templateId = -1;
                        int.TryParse(args, out templateId);

                        string value = "";

                        if (templateId > 0)
                        {
                            DocumentTemplate dt = new DocumentTemplate();
                            dt.documentTemplateID = templateId;
                            dt.SetPatient(proband);
                            dt.BackgroundLoadWork();
                            dt.OpenHTML();
                            dt.UseDocArgs = UseDocArgs;
                            dt.ProcessDocument();
                            value = dt.htmlText;
                            HtmlNode body = dt.FindNodeByName(dt.doc.DocumentNode, "body");
                            StripNonInsertNodes(body);
                            if (body != null)
                            {
                                value = body.InnerHtml;
                            }

                            tableNode.Text = tableNode.Text.Replace(tag, value);
                        }
                    }
                }
            }
        }

        private void StripNonInsertNodes(HtmlNode body)
        {
            List<HtmlNode> toDelete = new List<HtmlNode>();
            GetNonInsertNodes(body, ref toDelete);
            foreach(HtmlNode n in toDelete)
            {
                n.ParentNode.RemoveChild(n);
            }
        }
        private bool IsRemoveOnTemplateInsert(HtmlNode target)
        {
            bool retval = false;
            HtmlAttribute attr = target.Attributes["insertable"];
            if (attr != null)
            {
                if (string.Compare(attr.Value, "false", true) == 0)
                {
                    retval = true;
                }
            }

            return retval;
        }
        private void GetNonInsertNodes(HtmlNode body, ref List<HtmlNode> doomed)
        {
            if (IsRemoveOnTemplateInsert(body))
            {
                doomed.Add(body);
            }
            else
            {
                foreach (HtmlNode child in body.ChildNodes)
                {
                    GetNonInsertNodes(child, ref doomed);
                }
            }
        }
    }
}
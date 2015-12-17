using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Data;
using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    class ParagraphRuleTag : HraLetterTag
    {
        private char[] splitChars = { ',' };
        public readonly string HraTagText = "[ParagraphRule(";
        public readonly string HraTagSuffix = ")]";

        public Patient proband;

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
            if (proband != null)
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
                            string[] parts = args.Split(splitChars);
                            int rule = -1;
                            if (parts.Length > 0)
                            {
                                int.TryParse(parts[0], out rule);
                            }
                            bool bullets = false;
                            if (parts.Length>1)
                            {
                                bool.TryParse(parts[1], out bullets);
                            }
                            string prefix = "<p>";
                            string suffix = "</p>";

                            if (bullets)
                            {
                                prefix = "<li>";
                                suffix = "</li>";
                            }
                            if (rule > 0)
                            {
                                ParameterCollection pc = new ParameterCollection();
                                pc.Add("ruleID", rule);
                                pc.Add("apptID", proband.apptid);

                                string value = "";

                                using (SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_getParagraphsByRule", pc))
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.IsDBNull(0) == false)
                                            value += (prefix + reader.GetValue(0).ToString() + suffix);
                                    }
                                }
                                tableNode.Text = tableNode.Text.Replace(tag, value);
                            }
                        }
                    }
                }
            }
        }
    }
}
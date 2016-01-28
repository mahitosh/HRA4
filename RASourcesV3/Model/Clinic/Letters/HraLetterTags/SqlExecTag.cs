using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Data;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class SqlExecTag : HraLetterTag
    {
        public readonly string HraTagText = "[SQLEXEC(";
        public readonly string HraTagSuffix = ")]";

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
                        string sql = tableNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);

                        DataTable theData = BCDB2.Instance.getDataTable(sql);
                        string dataVal = "";
                        if (theData.Rows.Count > 0 && theData.Columns.Count > 0)
                        {
                            dataVal = theData.Rows[0][0].ToString();
                        }
                        tableNode.Text = tableNode.Text.Replace(tag, dataVal);
                    }
                }
            }
            
        }

        ///**********************************************************************/
        //private void PerformTagReplacement(HtmlTextNode tableNode, DataTable theData)
        //{
        //    string result = "";

        //    if (theData != null)
        //    {
        //        if (theData.Rows.Count > 0 && theData.Columns.Count > 0)
        //        {
        //            result = theData.Rows[0][0].ToString();
        //        }
        //    }

        //    tableNode.Text = result;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using HtmlAgilityPack;
using System.Data;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class MakeListTag : HraLetterTag
    {
        public string HraTagText = "[MakeList(";
        public string HraTagSuffix = ")]";

        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {   
            //find all make table nodes
            List<HtmlTextNode> makeTableNodes = new List<HtmlTextNode>();
            FindMakeTableNodes(doc.DocumentNode, ref makeTableNodes);

            foreach (HtmlTextNode tableNode in makeTableNodes)
            {
                try
                {
                    GetDataAndReplaceTag(tableNode);
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog(e.ToString());
                }
            }
        }

        /**********************************************************************/
        private void FindMakeTableNodes(HtmlNode node, ref List<HtmlTextNode> makeTableNodes)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)node;
                if (tn.Text.Contains(HraTagText))
                {
                    makeTableNodes.Add(tn);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindMakeTableNodes(child, ref makeTableNodes);
            }
        }

        /**********************************************************************/
        private void GetDataAndReplaceTag(HtmlTextNode tableNode)
        {

                int pos = tableNode.Text.IndexOf(HraTagText, 0);
                int suffixPos = tableNode.Text.IndexOf(HraTagSuffix);
                if (suffixPos > pos)
                {
                    string sql = tableNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);

                    DataTable theData = BCDB2.Instance.getDataTable(sql);

                    BuildTableFromResults(tableNode, theData);
                }
        }

        private void BuildTableFromResults(HtmlTextNode makeTableNode, DataTable theData)
        {
            HtmlNode firstRow = FindFirstRow(makeTableNode);
            HtmlNode firstlist = FindFirstList(makeTableNode);

            if (theData == null)
            {
                firstlist.ParentNode.RemoveChild(firstlist);
                return;
            }
            if (theData.Rows.Count==0)
            {
                firstlist.ParentNode.RemoveChild(firstlist);
                return;
            }

            for (int i = 0; i < theData.Rows.Count; i++)
            {
                HtmlNode newRow;
                HtmlTextNode newText = null;
                if (i == 0)
                    newRow = firstRow;
                else
                {
                    newRow = makeTableNode.OwnerDocument.CreateElement("li");
                    foreach (HtmlAttribute ha in firstRow.Attributes)
                    {
                        newRow.Attributes.Add(ha);
                    }
                }
                string acc = "";
                for (int j = 0; j < theData.Columns.Count; j++)
                {
                    acc += (theData.Rows[i][j].ToString()) + " ";
                }
                acc = acc.Trim();


                if (i == 0)
                {
                    newText = makeTableNode;
                    newText.Text = acc;
                }
                else
                {
                    newText = makeTableNode.OwnerDocument.CreateTextNode(acc);
                    newText.Text = acc;
                    newRow.AppendChild(newText);
                    firstRow.ParentNode.AppendChild(newRow);
                }
            }
        }

        /**********************************************************************/
        protected HtmlNode FindFirstList(HtmlNode tableNode)
        {
            HtmlNode retval = null;
            if (tableNode.Name == "ul")
                retval = tableNode;
            else
                if (tableNode.ParentNode != null)
                    retval = FindFirstList(tableNode.ParentNode);
            return retval;
        }


        /**********************************************************************/
        protected HtmlNode FindFirstRow(HtmlNode tableNode)
        {
            HtmlNode retval = null;
            if (tableNode.Name == "li")
                retval = tableNode;
            else
                if (tableNode.ParentNode != null)
                    retval = FindFirstRow(tableNode.ParentNode);
            return retval;
        }

    }
}

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
    public class MakeTableTag : HraLetterTag
    {
        public string HraTagText = "[MakeTable(";
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
            HtmlNode firstColumn = FindFirstColumn(makeTableNode);
            HtmlNode firstRow = FindFirstRow(makeTableNode);
            HtmlNode firsttable = FindFirstTable(makeTableNode);

            if (theData == null)
            {
                firsttable.ParentNode.RemoveChild(firsttable);
                return;
            }
            if (theData.Rows.Count==0)
            {
                firsttable.ParentNode.RemoveChild(firsttable);
                return;
            }

            for (int i = 0; i < theData.Rows.Count; i++)
            {
                HtmlNode newRow;
                if (i == 0)
                    newRow = firstRow;
                else
                {
                    newRow = makeTableNode.OwnerDocument.CreateElement("tr");
                    foreach(HtmlAttribute ha in firstRow.Attributes)
                    {
                        newRow.Attributes.Add(ha);
                    }
                }
                for (int j = 0; j < theData.Columns.Count; j++)
                {
                    HtmlNode newCell = null;
                    HtmlTextNode newText = null;
                    if (!(i == 0 && j == 0))
                    {
                        newCell = makeTableNode.OwnerDocument.CreateElement("td");
                        foreach (HtmlAttribute ha in firstColumn.Attributes)
                        {
                            newCell.Attributes.Add(ha);
                        }
                        newText = makeTableNode.OwnerDocument.CreateTextNode(theData.Rows[i][j].ToString());
                        if (string.IsNullOrEmpty(newText.InnerHtml))
                            newText.InnerHtml = "&nbsp";
                        newCell.AppendChild(newText);
                        newRow.AppendChild(newCell);
                    }
                    else
                    {
                        newCell = firstColumn;
                        newText = makeTableNode;
                        newText.Text = theData.Rows[i][j].ToString();
                    }
                    ProcessTableCellByIndex(i, j, newCell, newText);
                }
                if (i != 0)
                    firstRow.ParentNode.AppendChild(newRow);
            }

        }

        public virtual void ProcessTableCellByIndex(int row, int column, HtmlNode tableCellNode, HtmlTextNode cellTextNode)
        {

        }

        /**********************************************************************/
        protected HtmlNode FindFirstTable(HtmlNode tableNode)
        {
            HtmlNode retval = null;
            if (tableNode.Name == "table")
                retval = tableNode;
            else
                if (tableNode.ParentNode != null)
                    retval = FindFirstTable(tableNode.ParentNode);
            return retval;
        }

        /**********************************************************************/
        protected HtmlNode FindFirstColumn(HtmlNode tableNode)
        {
            HtmlNode retval = null;
            if (tableNode.Name == "td")
                retval = tableNode;
            else
                if (tableNode.ParentNode != null)
                    retval = FindFirstColumn(tableNode.ParentNode);
            return retval;
        }

        /**********************************************************************/
        protected HtmlNode FindFirstRow(HtmlNode tableNode)
        {
            HtmlNode retval = null;
            if (tableNode.Name == "tr")
                retval = tableNode;
            else
                if (tableNode.ParentNode != null)
                    retval = FindFirstRow(tableNode.ParentNode);
            return retval;
        }

    }
}

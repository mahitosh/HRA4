using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HtmlAgilityPack;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.HraLetterTags
{
    public class Sectionizer
    {
        /**********************************************************************/

        public List<DocumentSection> ProcessHtml(ref HtmlDocument doc)
        {
            List<DocumentSection> documentSections = new List<DocumentSection>();
            try
            {
                SectionizeNodes(doc.DocumentNode, ref documentSections);
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }

            List<HtmlNode> divs2Remove = new List<HtmlNode>();
            List<HtmlNode> parents = new List<HtmlNode>();
            foreach (HtmlNode matchingDiv in doc.DocumentNode.Descendants().Where(n => n.Name == "div"))
            {

                foreach (HtmlAttribute hat in matchingDiv.Attributes)
                {
                    if (hat.Name.ToUpper() == "DIVDISPLAY" && !string.IsNullOrEmpty(hat.Value))
                    {
                        //Console.WriteLine(hat.Value);
                        String sectionSQL = hat.Value;
                        SqlDataReader reader = BCDB2.Instance.ExecuteReader(sectionSQL);

                        String value = "";
                        if (reader.Read())
                        {
                            value = reader.GetValue(0).ToString();
                        }
                        else
                        {
                            value = "No";
                        }


                        if (reader.NextResult())
                        {
                            if (reader.Read())
                            {
                                value = reader.GetValue(0).ToString();
                            }
                        }

                        reader.Close();

                        if (value == "No")
                        {
                           //remove the div
                            divs2Remove.Add(matchingDiv);
                            parents.Add(matchingDiv.ParentNode);
                        }

                    }
                }


                Boolean isTableDiv = false;
                foreach (HtmlAttribute hat in matchingDiv.Attributes)
                {
                    if (hat.Name.ToUpper() == "TABLEDIV" && !string.IsNullOrEmpty(hat.Value))
                    {
                        if (hat.Value.ToUpper() == "YES")
                        {
                            isTableDiv = true;
                        }
                    }
                }


                if (isTableDiv)
                {
                    bool hasTable = FindPopulatedTable(matchingDiv);
                    if (!hasTable)
                    {
                        if (divs2Remove.Contains(matchingDiv) == false)
                        {
                            divs2Remove.Add(matchingDiv);
                            parents.Add(matchingDiv.ParentNode);
                        }
                    }
                }
            }
 
            for (int i = 0; i < divs2Remove.Count; i++)
            {
                //try
                //{
                    parents[i].RemoveChild(divs2Remove[i]);
                //}
                //catch { }
            }

            return documentSections;
        }

        private bool FindPopulatedTable(HtmlNode node)
        {
            bool retval = false;

            if (node.Name.ToUpper().Equals("TABLE"))
            {
                if (TableNodeHasRows(node))
                {
                    retval = true;
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                if (FindPopulatedTable(child))
                    retval = true;
            }

            return retval;
        }

        private bool TableNodeHasRows(HtmlNode node)
        {
            bool retval = false;
            string tableContents = "";
            HtmlNodeCollection nodes = node.SelectNodes("tr");
            if (nodes != null)
            {
                foreach (HtmlNode row in nodes)
                {
                    foreach (HtmlNode cell in row.SelectNodes("td"))  //BWD removed th so that headers can be used in templates
                    {
                        tableContents = tableContents + cell.InnerText.Trim();
                    }
                }
            }
            nodes = node.SelectNodes("ul");
            if (nodes != null)
            {
                foreach (HtmlNode row in nodes)
                {
                    foreach (HtmlNode cell in row.SelectNodes("li"))  //BWD removed th so that headers can be used in templates
                    {
                        tableContents = tableContents + cell.InnerText.Trim();
                    }
                }
            }
            if (tableContents.Length != 0)
            {
                retval = true;
            }

            return retval;
        }


        /**********************************************************************/

        private void SectionizeNodes(HtmlNode node, ref List<DocumentSection> documentSections)
        {
            foreach (HtmlAttribute hat in node.Attributes)
            {
                if (hat.Name == "id" && !string.IsNullOrEmpty(hat.Value))
                {
                    DocumentSection ds = new DocumentSection();
                    ds.SectionNode = node.CloneNode(true);
                    ;
                    ds.title = hat.Value;
                    documentSections.Add(ds);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                SectionizeNodes(child, ref documentSections);
            }
        }
    }
}



                //String divName = "";
                //foreach (HtmlAttribute hat in matchingDiv.Attributes)
                //{
                //    if (hat.Name == "id" && !string.IsNullOrEmpty(hat.Value))
                //    {
                //        Console.WriteLine(hat.Value);
                //        divName = hat.Value;
                //    }
                //}





                    //foreach (HtmlNode child in matchingDiv.ChildNodes)
                    //{
                    //    if (child.Name.ToUpper().Equals("TABLE"))
                    //    {
                    //        hasTable = true;
                    //    }
                    //}
                    //if (hasTable == false)
                    //{
                    //    divs2Remove.Add(matchingDiv);
                    //    parents.Add(matchingDiv.ParentNode);
                    //}
                    //else
                    //{
                    //    String tableContents = "";

                    //    //see if table is there, but just empty
                    //    foreach (HtmlNode child in matchingDiv.ChildNodes)
                    //    {
                    //        HtmlNode tableNode = null;
                    //        if (child.Name.ToUpper().Equals("TABLE"))
                    //        {
                    //            tableNode = child;
                    //        }

                    //        if (tableNode != null)
                    //        {
                    //            foreach (HtmlNode row in tableNode.SelectNodes("tr"))
                    //            {
                    //                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                    //                {
                    //                    tableContents = tableContents + cell.InnerText.Trim();
                    //                }
                    //            }
  
                    //            if (tableContents.Length == 0)
                    //            {
                    //                divs2Remove.Add(matchingDiv);
                    //                parents.Add(matchingDiv.ParentNode);
                    //            }
                    //        }
                    //    }
                    //}
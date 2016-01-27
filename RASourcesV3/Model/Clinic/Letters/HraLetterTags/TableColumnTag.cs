using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using RiskApps3.Utilities;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class TableColumnTag : HraLetterTag
    {
        public readonly string HraTagRegEx = "\\[[a-zA-Z0-9_]*\\.[a-zA-Z0-9_]*\\]";

        char[] splitters = { '.' };

        Regex tagRegEx;

        public Patient proband;

        public TableColumnTag()
        {
            tagRegEx = new Regex(HraTagRegEx);
        }

        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            try
            {
                //find all make table nodes
                List<HtmlTextNode> tableColumnNodes = new List<HtmlTextNode>();
                FindTableColumnNodes(doc.DocumentNode, ref tableColumnNodes);

                foreach (HtmlTextNode tableColumnNode in tableColumnNodes)
                {
                    try
                    {
                        GetDataAndReplaceTag(tableColumnNode);
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
        private void FindTableColumnNodes(HtmlNode node, ref List<HtmlTextNode> tableColumnNodes)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)node;
                if (tagRegEx.IsMatch(tn.Text))
                {
                    tableColumnNodes.Add(tn);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindTableColumnNodes(child, ref tableColumnNodes);
            }
        }

        /**********************************************************************/
        private void GetDataAndReplaceTag(HtmlTextNode tableColumnNode)
        {
            string val = "";

            MatchCollection matches = tagRegEx.Matches(tableColumnNode.Text);
            foreach (Match m in matches)
            {
                string formated = m.Value.Replace("[", "").Replace("]", "");
                string[] parts = formated.Split(splitters);

                if (parts.Length == 2)
                {
                    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                    {
                        connection.Open();

                        SqlCommand cmdProcedure = new SqlCommand("sp_getPatientDataValue", connection);
                        cmdProcedure.CommandType = CommandType.StoredProcedure;

                        //SqlCommand command = new SqlCommand(sql, connection);
                        cmdProcedure.Parameters.Add("@apptID", SqlDbType.NVarChar);

                        if (proband == null)
                        {
                            cmdProcedure.Parameters["@apptID"].Value = SessionManager.Instance.GetActivePatient().apptid;//da.apptid;
                        }
                        else
                        {
                            cmdProcedure.Parameters["@apptID"].Value = proband.apptid;//da.apptid;
                        }

                        cmdProcedure.Parameters.Add("@tableName", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@tableName"].Value = parts[0];

                        cmdProcedure.Parameters.Add("@columnName", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@columnName"].Value = parts[1];

                        try
                        {
                            SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0) == false)
                                        val = reader.GetValue(0).ToString();
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Instance.WriteToLog(e.ToString());
                        }
                    }
                   tableColumnNode.Text = tableColumnNode.Text.Replace(m.Value, val);
                }
            }
            //PerformTagReplacement(tableColumnNode, val);
        }

        ///**********************************************************************/
        //private void PerformTagReplacement(HtmlTextNode tableColumn, string val)
        //{
        //    string result = "";

        //    if (string.IsNullOrEmpty(val) ==false)
        //    {
        //        result = val;
        //    }

        //    tableColumn.Text = result;
        //}
    }
}

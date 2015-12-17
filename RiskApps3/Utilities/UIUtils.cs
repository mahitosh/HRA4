using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using dotnetCHARTING.WinForms;
using System.Drawing;
using HtmlAgilityPack;
using System.Globalization;

namespace RiskApps3.Utilities
{
    public class UIUtils
    {
        public static void AddTwoColumnRowToTable(HtmlAgilityPack.HtmlNode table, HtmlAgilityPack.HtmlNode cell1, HtmlAgilityPack.HtmlNode cell2)
        {
            HtmlNode row = table.OwnerDocument.CreateElement("tr");
            row.Attributes.Add(table.OwnerDocument.CreateAttribute("valign", "top"));

            HtmlNode td1 = table.OwnerDocument.CreateElement("td");
            HtmlNode td2 = table.OwnerDocument.CreateElement("td");

            td1.ChildNodes.Add(cell1);
            td2.ChildNodes.Add(cell2);

            row.ChildNodes.Add(td1);
            row.ChildNodes.Add(td2);

            table.ChildNodes.Add(row);
        }

        public static void AddColumnRowToTable(HtmlAgilityPack.HtmlNode table, string s1, string s2, string s3)
        {
            HtmlAttribute attr = table.OwnerDocument.CreateAttribute("style", "border:1px solid black;border-collapse:collapse;");

            HtmlNode row = table.OwnerDocument.CreateElement("tr");
            row.Attributes.Add(table.OwnerDocument.CreateAttribute("valign", "top"));

            HtmlNode td1 = table.OwnerDocument.CreateElement("td");
            HtmlNode td2 = table.OwnerDocument.CreateElement("td");

            HtmlAttribute rightJustify = table.OwnerDocument.CreateAttribute("align", "right");

            row.Attributes.Add(attr);
            td1.Attributes.Add(attr);
            td2.Attributes.Add(attr);
            td1.Attributes.Add(rightJustify);
            td2.Attributes.Add(rightJustify);

            td1.InnerHtml = s1;
            td2.InnerHtml = s2;

            row.ChildNodes.Add(td1);
            row.ChildNodes.Add(td2);

            if (string.IsNullOrEmpty(s3)==false)
            {
                HtmlNode td3 = table.OwnerDocument.CreateElement("td");
                td3.Attributes.Add(attr);
                td3.Attributes.Add(rightJustify);
                td3.InnerHtml = s3;
                row.ChildNodes.Add(td3);
            }
            table.ChildNodes.Add(row);
        }

        public static HtmlAgilityPack.HtmlNode CreateReportTableNode(HtmlAgilityPack.HtmlNode node) 
        {
            HtmlNode table = node.OwnerDocument.CreateElement("table");
            HtmlAttribute attr = table.OwnerDocument.CreateAttribute("style", "border:1px solid black;border-collapse:collapse;");
            table.Attributes.Add(attr);
            return table;
        }
        public static HtmlAgilityPack.HtmlNode GetImageHtmlFromChart(Chart chart, HtmlAgilityPack.HtmlNode node)
        {
            Bitmap bmp = chart.GetChartBitmap();

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            HtmlNode newRCNode = node.OwnerDocument.CreateElement("img");

            newRCNode.Attributes.Add(node.OwnerDocument.CreateAttribute("src", "data:image/gif;base64," + base64Data));
            newRCNode.Attributes.Add(node.OwnerDocument.CreateAttribute("style", "width:" + bmp.Width + "px; height:" + bmp.Height + "px"));
 
            return newRCNode;
        }
        public static Object[] getYearList()
        {
            Object[] yearList = new Object[101];

            int currentYear = DateTime.Today.Year;
            yearList[0] = "";
            for (int i = 1; i < 101; i++)
            {
                yearList[i] = ((Int16) (currentYear - i + 1)).ToString();
            }

            return yearList;
        }

        public static void fillComboBoxFromList(ComboBox comboBox, List<String> list, bool blankOK)
        {
            comboBox.Items.Clear();
            //add a blank if blank is OK
            if (blankOK)
            {
                comboBox.Items.Add("");
            }

            foreach (string item in list)
            {
                if (item != null)
                {
                    comboBox.Items.Add(item);
                }
            }
        }

        public static DateTime weekstart(DateTime today)
        {
            DayOfWeek firstDay = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDayInWeek = today.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime weekend(DateTime today)
        {
            return weekstart(today).AddDays(7);
        }

        public static void fillComboBoxFromLookups(ComboBox comboBox, String table, String field, bool blankOK)
        {
            comboBox.Items.Clear();
            //add a blank if blank is OK
            if (blankOK)
            {
                comboBox.Items.Add("");
            }

            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand cmdProcedure = new SqlCommand("sp_3_GetValuesFromlkpLookups", connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;

                cmdProcedure.Parameters.Add("@table", SqlDbType.NVarChar);
                cmdProcedure.Parameters["@table"].Value = table;

                cmdProcedure.Parameters.Add("@field", SqlDbType.NVarChar);
                cmdProcedure.Parameters["@field"].Value = field;

                try
                {
                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0) == false)
                            {
                                String value = reader.GetValue(0).ToString();
                                comboBox.Items.Add(value);
                            }
                        }
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.WriteToLog(ex.ToString());
                }
            }
        }


/*
        public static void fillCheckedListBoxFromLookups(CheckedListBox checkedListBox, String table, String field)
        {
            fillCheckedListBoxFromLookups(checkedListBox, table, field, "");
        }

        public static void fillCheckedListBoxFromLookups(CheckedListBox checkedListBox, String table, String field,
                                                         String filter)
        {
            String sqlStr =
                "SELECT value1 FROM lkpLookups WHERE [table]='" + table +
                "' AND field='" + field + "' " + filter +
                "ORDER BY ord_ ASC, value1 ASC;";

            fillCheckedListBoxFromQuery(checkedListBox, sqlStr);
        }


     


        public static void fillCheckedListBoxFromQuery(CheckedListBox checkedListBox, String sqlStr)
        {
            DbDataReader reader = DBUtils.ExecuteReader(sqlStr);

            //remove all existing items
            checkedListBox.Items.Clear();

            //populate the combo
            while (reader.Read())
            {
                String value = reader.GetValue(0).ToString();
                checkedListBox.Items.Add(value);
            }
            reader.Close();
        }

        public static void fillComboBoxFromQuery(ComboBox comboBox, String sqlStr, bool blankOK)
        {
            DbDataReader reader = DBUtils.ExecuteReader(sqlStr);

            //remove all existing items
            comboBox.Items.Clear();

            //add a blank if blank is OK
            if (blankOK)
            {
                comboBox.Items.Add("");
            }

            if (reader != null)
            {
                //populate the combo
                while (reader.Read())
                {
                    String value = reader.GetValue(0).ToString();
                    comboBox.Items.Add(value);
                }
                reader.Close();
            }
        }

        public static void fillComboBoxFromQuery(ComboBox comboBox, String sqlStr, bool blankOK, String currentValue)
        {
            fillComboBoxFromQuery(comboBox, sqlStr, blankOK);

            if (comboBox.Items.Contains(currentValue) == false)
            {
                comboBox.Items.Add(currentValue);
            }
            comboBox.Text = currentValue;
        }*/
    }
}
using HtmlAgilityPack;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class AreaProvidersTag : HraLetterTag
    {
        public readonly string HraTagText = "[AreaProviders(";
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
                FindAreaProvidersNodes(doc.DocumentNode, ref sqlExecNodes);

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
        private void FindAreaProvidersNodes(HtmlNode node, ref List<HtmlTextNode> sqlExecNodes)
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
                FindAreaProvidersNodes(child, ref sqlExecNodes);
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
                        string relid = tableNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);
                        int r;
                        if (int.TryParse(relid,out r))
                        {
                            tableNode.Text = GetAreaProvidersByRelId(r);
                        }
                    }
                }
            }

        }

        private string GetAreaProvidersByRelId(int r)
        {
            string retval = "";

            if (proband != null)
            {
                string sqlStr = "SELECT zipcode, city, state FROM tblRelativeDetails WHERE relativeID=@relativeID AND apptID=@apptID";
                ParameterCollection pc = new ParameterCollection();
                pc.Add("relativeID", r);
                pc.Add("apptID", proband.apptid);

                DbDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc);

                bool hasZipCode = false;
                String zipcode = "";
                String city = "";
                String state = "";

                if (reader.Read())
                {
                    zipcode = reader.GetValue(0).ToString();
                    city = reader.GetValue(1).ToString();
                    state = reader.GetValue(2).ToString();
                }
                reader.Close();

                if (zipcode != "")
                {
                    hasZipCode = ZipCodes.isValidZip(zipcode);
                }
                else
                {
                    zipcode = ZipCodes.getZipCode(city, state);
                    if (zipcode != "")
                    {
                        hasZipCode = ZipCodes.isValidZip(zipcode);
                    }
                }


                if (hasZipCode)
                {
                    retval = GetAreaProvidersByZipCode(zipcode);
                }


            }

            return retval;
        }

        private string GetAreaProvidersByZipCode(string zipcode)
        {
            string retval = "";

            zipcode = zipcode.Trim();
            if (zipcode.Length > 5)
            {
                zipcode = zipcode.Substring(0, 5);
            }

            String sqlStr = "SELECT providerID, zipcode FROM lkpProviders WHERE riskClinic=1 AND zipcode IS NOT NULL AND LEN(zipCode)>0;"; 
            DbDataReader reader = BCDB2.Instance.ExecuteReader(sqlStr);
            var providerHash = new Hashtable();
            while (reader.Read())
            {
                String providerID = reader.GetValue(0).ToString();
                String providerZipcode = reader.GetValue(1).ToString();
                providerZipcode = providerZipcode.Trim();
                if (providerZipcode.Length > 5)
                {
                    providerZipcode = providerZipcode.Substring(0, 5);
                }
                providerHash.Add(providerID, providerZipcode);
            }
            reader.Close();

            IDictionaryEnumerator en = providerHash.GetEnumerator();

            var providerDistanceHash = new Hashtable();
            while (en.MoveNext())
            {
                String providerID = en.Key.ToString();
                String providerZipcode = en.Value.ToString();

                if (ZipCodes.isValidZip(providerZipcode))
                {
                    double distance = ZipCodes.getDistance(zipcode, providerZipcode);
                    if (distance < 100.0)
                    {
                        providerDistanceHash.Add(providerID, distance);
                    }
                }
            }

            // sort by reverse distance
            var keys = new string[providerDistanceHash.Count];
            providerDistanceHash.Keys.CopyTo(keys, 0);
            var values = new double[providerDistanceHash.Count];
            providerDistanceHash.Values.CopyTo(values, 0);
            Array.Sort(values, keys);
            int maxIndex = 10;
            if (keys.Length < maxIndex)
                maxIndex = keys.Length;

            for (int i = 0; i < maxIndex; i++) //limit to 10 closest
            {
                retval += GetProviderInfo(keys[i]);
            }


            return retval;
        }

        private string GetProviderInfo(string p)
        {
            string retval = "";

            String sqlStr = "SELECT title, firstName, middleName, lastName, degree, institution, address1, address2, city, state, zipcode, phone, email FROM lkpProviders WHERE providerID=@providerID";
            ParameterCollection pc = new ParameterCollection();
            pc.Add("providerID", p); 

            DbDataReader reader = BCDB2.Instance.ExecuteReaderWithParams(sqlStr, pc);

            if (reader.Read())
            {
                String title = reader.GetValue(0).ToString();
                String firstName = reader.GetValue(1).ToString();
                String middleName = reader.GetValue(2).ToString();
                String lastName = reader.GetValue(3).ToString();
                String degree = reader.GetValue(4).ToString();
                String institution = reader.GetValue(5).ToString();
                String address1 = reader.GetValue(6).ToString();
                String address2 = reader.GetValue(7).ToString();
                String city = reader.GetValue(8).ToString();
                String state = reader.GetValue(9).ToString();
                String zipcode = reader.GetValue(10).ToString();
                String phone = reader.GetValue(11).ToString();
                String email = reader.GetValue(12).ToString();

                String name = "";
                if (title != "")
                {
                    name = name + title + " ";
                }
                name = name + firstName + " ";
                if (middleName != "")
                {
                    name = name + middleName + " ";
                }
                name = name + lastName;

                if (degree != "")
                {
                    name = name + ", " + degree;
                }

                retval += name + @"</br>";

                if (institution != "")
                {
                    retval += institution + @"</br>";
                }

                if (address1 != "")
                {
                    retval += address1 + @"</br>";
                }

                if (address2 != "")
                {
                    retval += address2 + @"</br>";
                }


                //city, state zip
                String cityLine = city + ", " + state + " " + zipcode;

                if (cityLine != "")
                {
                    retval += cityLine + @"</br>";
                }

                if (phone != "")
                {
                    retval += phone + @"</br>";
                }

                if (email != "")
                {
                    retval += email + @"</br>";
                }


            }

            retval += @"</br>";

            return retval;
        }
    }
}

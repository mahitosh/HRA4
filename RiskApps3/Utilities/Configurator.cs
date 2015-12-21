using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using System.Web;

namespace RiskApps3.Utilities
{
    public class Configurator
    {

        //added 2012.02.26 by PRB; this can be set via a riskapp.exe command line param
        private static string databaseNameOverride = "";
        public static string configFilePath = "";

        public static bool? aggregatorActive = null;

        public static NameValueCollection GetConfig(string section)
        {
            XmlDocument configFile = new XmlDocument();
            if (HttpContext.Current != null) // Mahitosh
            {
                if (HttpContext.Current.Session["InstitutionId"] != null)
                {
                    string key = HttpContext.Current.Session["InstitutionId"].ToString();
                    string tempConfiguration = HttpRuntime.Cache[key].ToString();
                    if (string.IsNullOrEmpty(tempConfiguration))
                    {
                        throw new Exception("Could not load or read configuration");
                    }
                    configFile.LoadXml(tempConfiguration);
                }
                
            }
            else if (configFilePath.Length > 0)
                configFile.Load(configFilePath);
            else
                configFile.Load("config.xml");

            for (int i = 0; i < configFile.ChildNodes.Count; i++)
            {
                XmlNode childNode = configFile.ChildNodes[i];

                if (string.Compare(childNode.Name, "configuration", true) == 0)
                {
                    return processConfigNode(childNode, section);
                }
            }

            return null;
        }

        private static NameValueCollection processConfigNode(XmlNode configNode, string section)
        {
            for (int i = 0; i < configNode.ChildNodes.Count; i++)
            {
                XmlNode childNode = configNode.ChildNodes[i];

                if (string.Compare(childNode.Name, section, true) == 0)
                {
                    return processSectionNode(childNode);
                }
            }

            return null;
        }

        private static NameValueCollection processSectionNode(XmlNode sectionNode)
        {
            NameValueCollection retval = new NameValueCollection();
            for (int i = 0; i < sectionNode.ChildNodes.Count; i++)
            {
                XmlNode childNode = sectionNode.ChildNodes[i];
                if (string.Compare(childNode.Name, "Add", true) == 0)
                {
                    string keyStr = getAttributeFromNode(childNode, "key");
                    string valueStr = getAttributeFromNode(childNode, "value");

                    //PRB 2012.02.26
                    //override config.xml database name, when supplied as arg for riskApp.exe
                    if ((databaseNameOverride.Length > 0) && (string.Compare(sectionNode.Name, "DatabaseInfo", true) == 0))
                    {
                        if (string.Compare(keyStr, "Connection", true) == 0)
                            valueStr = Regex.Replace(valueStr, "DATABASE=[^;]+;", "DATABASE=" + databaseNameOverride + ";");
                    }

                    retval.Add(keyStr, valueStr);
                }
            }
            return retval;
        }

        /**********************************************************************/
        //
        //  getAttributeFromNode()
        //
        //
        //
        //
        private static string getAttributeFromNode(XmlNode node, string target)
        {
            string retval = null;

            if (node.Attributes != null)
            {
                for (int i = 0; i < node.Attributes.Count; i++)
                {
                    XmlAttribute attr = node.Attributes[i];
                    if (string.Compare(attr.Name, target, true) == 0)
                    {
                        retval = attr.Value;
                    }
                }
            }

            return retval;
        }

        public static string getAutomationHeartbeatServiceURL()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["AutomationHeartbeatServiceURL"];

            if (!String.IsNullOrEmpty(strValue))
            {
                return strValue;
            }
            else
            {
                return "";  //when config.xml doesn't have a RiskServiceURL entry
            }
        }

        public static string getCloudWebURL()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["CloudWebQueueURL"];

            if (!String.IsNullOrEmpty(strValue))
            {
                return strValue;
            }
            else
            {
                return "";  //when config.xml doesn't have a RiskServiceURL entry
            }
        }


        public static bool useNTAuthentication()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["UseNTAuthentication"].ToUpper();
            if (strValue.ToString().Length > 0)
            {
                if (strValue == "TRUE")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static string GetDocumentStorage()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            return collectionValues["DocumentStorage"];
        }
        public static string GetDocumentTemplateStorage()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            return collectionValues["DocumentTemplateStorage"];
        }
        public static bool getConfigBool(string tag)
        {
            NameValueCollection collectionValues = GetConfig("globals");
            string strValue = collectionValues[tag];
            if (string.IsNullOrEmpty(strValue) == false)
            {
                strValue = strValue.ToUpper();
                if (strValue.ToString().Length > 0)
                {
                    if (strValue == "TRUE")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        public static string getNodeValue(String strSection, String strNode)
        {
            String strValue = "";
            NameValueCollection collectionValues = GetConfig(strSection);
            try
            {
                strValue = collectionValues[strNode];
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
                strValue = "";
            }
            //
            return strValue != null ? strValue : "";
            //
        }

        public static bool useRCOM()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["UseRCOM"];

            if (strValue != null)
            {
                strValue = strValue.ToUpper();
                if (strValue.ToString().Length > 0)
                {
                    if (strValue == "TRUE")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else //default when config.xml doesn't have any UseRCOM entry
            {
                return false;
            }
        }

        public static bool useRiskService()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["UseRiskService"];

            if (strValue != null)
            {
                strValue = strValue.ToUpper();
                if (strValue.ToString().Length > 0)
                {
                    if (strValue == "TRUE")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else //default when config.xml doesn't have any UseRiskService entry
            {
                return false;
            }
        }

        public static int getRiskServiceTimeout()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["RiskServiceTimeout"];
            const int defaultTimeout = 45;  //seconds (when not supplied in Config file)

            if (strValue != null)
            {
                if (strValue.ToString().Length > 0)
                {
                    int timeout;
                    bool result = Int32.TryParse(strValue, out timeout);
                    if (true == result)
                    {
                        return timeout;
                    }
                    else
                    {
                        return defaultTimeout;
                    }
                }
                else
                {
                    return defaultTimeout;
                }
            }
            else //default when config.xml doesn't have any RiskServiceTimeout entry
            {
                return defaultTimeout;
            }
        }

        public static int getRiskServiceRetryInterval()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["RiskServiceRetryInterval"];
            const int defaultRetryInterval = 300;  // =5 minutes (when not supplied in Config file)

            if (strValue != null)
            {
                if (strValue.ToString().Length > 0)
                {
                    int retryInterval;
                    bool result = Int32.TryParse(strValue, out retryInterval);
                    if (true == result)
                    {
                        return retryInterval;
                    }
                    else
                    {
                        return defaultRetryInterval;
                    }
                }
                else
                {
                    return defaultRetryInterval;
                }
            }
            else //default when config.xml doesn't have any RiskServiceRetryInterval entry
            {
                return defaultRetryInterval;
            }
        }

        public static string getHVIDsServiceURL()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["HealthVaultIDsServiceURL"];

            if (!String.IsNullOrEmpty(strValue))
            {
                return strValue;
            }
            else
            {
                return "";  //when config.xml doesn't have any HealthVaultIDsServiceURL entry
            }
        }

        public static bool useAggregatorService()
        {
            //if (aggregatorActive != null)
            //{
            //    if (aggregatorActive == true)
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
                NameValueCollection collectionValues = GetConfig("globals");
                String strValue = collectionValues["UseAggregatorService"];

                if (strValue != null)
                {
                    strValue = strValue.ToUpper();
                    if (strValue.ToString().Length > 0)
                    {
                        if (strValue == "TRUE")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else //default when config.xml doesn't have any UseAggregatorService entry
                {
                    return false;
                }
            //}
        }

        public static string getAggregatorLicenseID()
        {
            NameValueCollection collectionValues = GetConfig("globals");
            String strValue = collectionValues["AggregatorLicenseID"];

            if (!String.IsNullOrEmpty(strValue))
            {
                return strValue;
            }
            return "3b511abe-8342-4947-a05e-b3f9bc322c00";  //licenseID when not supplied in Config file = legacy riskApps ID
        }


        public static void setDatabaseNameOverride(string dbOverrideName)
        {
            databaseNameOverride = dbOverrideName;
        }

        //
        // Determine in what folder the current running/executing software is located
        //
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path) + @"\";
            }
        }
    }
}

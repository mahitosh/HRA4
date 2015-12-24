using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace HRA4.Utilities
{
    public class ConfigurationSettings
    {
        public static string CommonDbConnection
        {
            get
            {
                //Do null check and return empty or default value
                return ConfigurationManager.AppSettings["CommonDbConnection"];
            }
        }
        public static string InstitutionDbConnection
        {
            get
            {
                return ConfigurationManager.AppSettings["InstitutionDbConnection"];
            }
        }

        public static string InstitutionPassword { get { return ConfigurationManager.AppSettings["InstitutionPassword"]; } }
    }
}

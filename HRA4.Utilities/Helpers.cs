using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using log4net;
using System.Web.Mvc;
using System.Web;
using System.Drawing;
using RiskApps3.Utilities;
using System.Globalization;
namespace HRA4.Utilities
{
    public class Helpers
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Helpers));
        public static void CreateInstitutionDb(string conn, string dbscript)
        {            
            Logger.DebugFormat("CreateInstitutionDb: Start");
            string[] result = ParseSqlStatementBatch(dbscript);

            try
            {
                SqlConnection myConn = new SqlConnection(conn);

                foreach (string commandText in result)
                {
                   
                    try
                    {
                        if (myConn == null)
                            myConn = new SqlConnection(conn);
                        if (myConn.State == ConnectionState.Closed)
                        {
                            myConn.Open();
                        }
                        if (!string.IsNullOrWhiteSpace(commandText))
                        {
                            var cmd = new SqlCommand(commandText, myConn);
                            // Logger.DebugFormat("Sql statement:{0}", commandText);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                    }

                myConn.Close();
                myConn.Dispose();
                Logger.DebugFormat("CreateInstitutionDb: End");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public static string ShowPedigreeImage(RiskApps3.Model.PatientRecord.Patient proband)
        {
            int Width = 625;
            int Height = 625;
            PedigreeGenerator pg = new PedigreeGenerator(Width, Height, proband);
            Bitmap bmp;
            if (proband != null)
            {
                bmp = pg.GeneratePedigreeImage(proband);
            }
            else
            {
                bmp = pg.GeneratePedigreeImage();
            }
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var base64Data = Convert.ToBase64String(stream.ToArray());

            return base64Data;
        }

        private static string[] ParseSqlStatementBatch(string sqlStatementBatch)
        {
            // split the sql into seperate batches by dividing on the GO statement
            Regex sqlStatementBatchSplitter = new Regex(@"^\s*GO\s*\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return sqlStatementBatchSplitter.Split(sqlStatementBatch);
        }

        public static string GenerateDbName(string instName)
        {
            string tmp = instName.Split(' ')[0];// takes first string from name
            string tmp1 = Guid.NewGuid().ToString().Split('-')[0];// takes first string from GUID
            string tenantDbName = string.Format("{0}_{1}_{2}", "HRA", tmp.ToUpper(), tmp1);
            return tenantDbName;
        }
       
        public static string GetInstitutionConfiguration(string configTemplate, string tenantDbName)
        {
            string instConnectionString = ConfigurationSettings.InstitutionDbConnection;
            instConnectionString = instConnectionString.Replace("[DBNAME]", tenantDbName);

            string instConfiguration = configTemplate;
            instConfiguration = instConfiguration.Replace("[DBCONNECTION]", instConnectionString);
            instConfiguration = instConfiguration.Replace("[PWD]", ConfigurationSettings.InstitutionPassword);
            return instConfiguration;
        }





       public static bool invalid = false;

        public static bool IsValidEmail(string strIn)
        {
           invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

    }
}

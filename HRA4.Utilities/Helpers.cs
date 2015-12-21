using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
namespace HRA4.Utilities
{
    public class Helpers
    {
        public static void CreateInstitutionDb(string conn, string dbscript)
        {            
             
            string[] result = ParseSqlStatementBatch(dbscript);

            try
            {
                SqlConnection myConn = new SqlConnection(conn);

                foreach (string commandText in result)
                {
                    try
                    {
                        if (myConn.State == ConnectionState.Closed)
                        {
                            myConn.Open();
                        }

                        var cmd = new SqlCommand(commandText, myConn);

                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        //don't log these errors.
                    }


                }
            }
            catch (Exception ex)
            {

            }
        }

        private static string[] ParseSqlStatementBatch(string sqlStatementBatch)
        {
            // split the sql into seperate batches by dividing on the GO statement
            Regex sqlStatementBatchSplitter = new Regex(@"^\s*GO\s*\r?$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return sqlStatementBatchSplitter.Split(sqlStatementBatch);
        }

       

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Specialized;
using System.Data;
using Obviex.CipherLite;
using System.Windows.Forms;
using System.Diagnostics;

using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using System.Web;

namespace RiskApps3.Utilities
{
    public class BCDB2
    {        
        //////////////////////////////////
        //  The singleton instance
        private static BCDB2 instance;

        //////////////////////////////////
        //  The members
        //old way:private SqlConnection dbConnection;
        //no longer save static connection to the DB.
        //instead create connections as needed on the fly; this will
        //allow multiple simultaneous database connections in different threads
        private String connectionString;

        private String connectionConfigKey = "Connection";
        private String connectionPWDConfigKey = "ConnectionPWD";

        /**********************************************************************/
        //
        //  BCDB2()
        //
        //  This is the default constructor
        //
        //
        //
        private BCDB2()
        {
            //we only need to do this once
            storeConnectionString();
        }

        private BCDB2(string connection_string)
        {
            connectionString = connection_string;
        }

        /**********************************************************************/
        //
        //  Instance()
        //
        //
        //
        //
        public static BCDB2 Instance
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    instance = new BCDB2();
                }
                else if (instance == null)// Mahitosh remove this line; Cache the cloud db 
                {
                    instance = new BCDB2();
                }
                return instance;
            }
        }

        /**********************************************************************/
        //
        //  
        //
        //
        //
        //
        public void setConnectionString(string connection_string)
        {
            connectionString = connection_string;
        }

        public static void InitConnectionString(string connection_string)
        {
            instance = new BCDB2(connection_string);
        }
        /**********************************************************************/
        //
        //  Instance()
        //
        //
        //
        //
        private void storeConnectionString()
        {
            try
            {
                NameValueCollection values = Configurator.GetConfig("DatabaseInfo");
                connectionString = values[connectionConfigKey];


                connectionString = connectionString.Replace("DRIVER=SQL SERVER;", "");
                connectionString = connectionString.Replace("APP=RISKAPP;", "APP=RISKAPP3;"); //("APP=MGHTranslator;", "");
                connectionString = connectionString.Replace("WSID=RISKAPPSWSID;", "");
                connectionString = connectionString.Replace("ADDRESS=1433;", "");
                connectionString = connectionString.Replace("SERVER=", "Data Source=");
                connectionString = connectionString.Replace("UID=", "User ID=");
                connectionString = connectionString.Replace("DATABASE=", "Initial Catalog=");
                connectionString += "Persist Security Info=True;PWD=";

                string encryptedPassword = values[connectionPWDConfigKey];
                if (encryptedPassword.Length == 0)
                {
                    connectionString += "dbbc";
                }
                else
                {
                    byte[] cipherText;
                    cipherText = Obviex.CipherLite.Encoder.Base64Decode(encryptedPassword);

                    // Decrypt value using DPAPI.
                    String connectionPWD;

                    connectionPWD = Rijndael.Decrypt(cipherText, "DeCipherriskApps", "", 256, 1, "", "MD5");
                    connectionString += connectionPWD;
                }
            }
            catch (Exception ex)
            {
                if (Environment.UserInteractive == true)
                {
                    MessageBox.Show("Unable to get valid connection string to database", "riskApps™", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                Logger.Instance.WriteToLog("Could not get connection string to database\n\t" + ex);
            }
        }

        //*********************************************************************
        //This routine should always be called as follows:
        //  using(SqlDataReader reader = BCDB2.Instance.ExecuteReader(...query text...))
        //      {
        //            ... do your stuff ...
        //      } // reader and connection are "automatically" closed here.
        //**********************************************************************
        public SqlDataReader ExecuteReader(string sqlStr)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                command = new SqlCommand(sqlStr, connection);
                command.CommandTimeout = 180; //change command timeout from default to 3 minutes
                //automatically close connection when disposing SqlDataReader
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                // Close connection and return a null reader
                connection.Close();
                Logger.Instance.WriteToLog("Could not execute SQL query: " + sqlStr + "\n\t" + e);
                return (SqlDataReader)null;
            }
        }

        /**********************************************************************/
        public int ExecuteNonQuery(String sqlStr)
        {
            int affectedRecords = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    command = new SqlCommand(sqlStr, connection);
                    command.CommandTimeout = 180; //change command timeout from default to 3 minutes
                    affectedRecords = command.ExecuteNonQuery();
                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
            }

            return affectedRecords;
        }
        public int ExecuteNonQueryWithParams(String sqlStr, ParameterCollection pc)
        {
            int affectedRecords = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    command = new SqlCommand(sqlStr, connection);
                    command.CommandTimeout = 180; //change command timeout from default to 3 minutes

                    if (pc != null)
                    {
                        foreach (string param in pc.getKeys())
                        {
                            command.Parameters.Add("@" + param, pc[param].sqlType);
                            command.Parameters["@" + param].Value = pc[param].obj;
                        }
                    }
                    affectedRecords = command.ExecuteNonQuery();
                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
            }

            return affectedRecords;
        }


        /**********************************************************************
         * return the first value in the first column of the result set,
         * that is, return a single scalar value
         * 
         * caller must cast resulting object to correct datatype
         *********************************************************************/
        public object ExecuteScalarQuery(String sqlStr)
        {
            object result = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    command = new SqlCommand(sqlStr, connection);
                    result = command.ExecuteScalar();
                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
            }

            return result;
        }

        /**********************************************************************
         * parameterized query
         *********************************************************************/
        public string GetClinicAndInstitutionNames(int apptid)
        {
            string sqlStr = "";
            string result = "Clinic: ; Institution: ";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    sqlStr = @"select 'Clinic: ' + clinicName + '; Institution: ' + institutionName
                                        from tblAppointments A
                                          join lkpClinics C
                                            on A.clinicID = C.clinicID
                                          join lkpInstitutions I
                                            on C.institutionID = I.institutionID
                                        where A.apptid = @apptid";

                    command = new SqlCommand(sqlStr, connection);
                    // Add new SqlParameter to the command.
                    command.Parameters.Add(new SqlParameter("apptid", apptid));

                    Object resultObject = command.ExecuteScalar();
                    if (resultObject != null)
                    {
                        string resultTemp = (string)resultObject;
                        if (resultTemp.Length > 0)
                        {
                            result = resultTemp;
                        }
                    }

                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
            }

            return result;
        }


        /**********************************************************************
 * parameterized query
 *********************************************************************/
        public void GetClinicAndInstitutionNames(int apptid, out string clinic, out string institution)
        {
            string sqlStr = "";
            clinic = "";
            institution = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    sqlStr = @"select clinicName, institutionName
                                        from tblAppointments A
                                          join lkpClinics C
                                            on A.clinicID = C.clinicID
                                          join lkpInstitutions I
                                            on C.institutionID = I.institutionID
                                        where A.apptid = @apptid";

                    command = new SqlCommand(sqlStr, connection);
                    // Add new SqlParameter to the command.
                    command.Parameters.Add(new SqlParameter("apptid", apptid));

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0) == false)
                        {
                            clinic = reader.GetString(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            institution = reader.GetString(1);
                        }
                    }

                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
            }
        }

        

        /**********************************************************************
         * return value
         *********************************************************************/
        public object ExecuteSpWithRetVal(String stored_procedure, SqlDbType return_type)
        {
            object result = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    command = new SqlCommand(stored_procedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter retval = command.Parameters.Add("@retval", return_type);
                    retval.Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();
                    result = command.Parameters["@retval"].Value;

                    connection.Close();
                } //connection will close here
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SP: " + stored_procedure + "\n\t" + e);
            }

            return result;
        }

        /**********************************************************************/
        public object ExecuteSpWithRetValAndParams(String stored_procedure, SqlDbType return_type, ParameterCollection pc)
        {
            object result = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command;
                    command = new SqlCommand(stored_procedure, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    if (pc != null)
                    {
                        foreach (string param in pc.getKeys())
                        {
                            command.Parameters.Add("@" + param, pc[param].sqlType);
                            command.Parameters["@" + param].Value = pc[param].obj;
                        }
                    }
                    SqlParameter retval = command.Parameters.Add("@retval", return_type);
                    retval.Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();
                    result = command.Parameters["@retval"].Value;

                    connection.Close();
                } //connection will close here
            }
            catch (Exception ee)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SP: " + stored_procedure + "\n\t" + ee);
            }

            return result;
        }

        /**********************************************************************/
        public SqlDbType GetSqlTypeFromModel(System.Type theType)
        {
            SqlParameter p1 = new SqlParameter();
            System.ComponentModel.TypeConverter tc =System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
            {
                p1.DbType = (DbType) tc.ConvertFrom(theType.Name);
            }
            else if (theType.Name.Equals("Byte[]"))  //byte array (sql varbinary) needs special handling
            {
                return SqlDbType.VarBinary;
            }
            else if (theType.FullName.Contains("DateTime"))  //needed for nullable DateTime field
            {
                return SqlDbType.DateTime;
            }
            else
            {
                try
                {
                    //allow to work with nullable types
                    string effectiveTypeName = theType.Name.StartsWith("Nullable") ? Nullable.GetUnderlyingType(theType).Name : theType.Name;
                    p1.DbType = (DbType)tc.ConvertFrom(effectiveTypeName);
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog(e.ToString());
                }
            }
            return p1.SqlDbType;
        }

        /**********************************************************************/
        public String getConnectionString()
        {
            return connectionString;
        }

        /**********************************************************************/
        public DataTable getDataTable(String sqlStmt)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DataTable dtGet = null;
                try
                {
                    connection.Open();
                    // create a new data adapter based on the specified query.
                    SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(sqlStmt, connection));
                    // create a new DataTable
                    dtGet = new DataTable();
                    //fill the DataTable
                    da.Fill(dtGet);
                    //return the DataTable
                    return dtGet;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Unable to get data table from database", "riskApps™", ex.Message);
                    Logger.Instance.WriteToLog("Unable to get data table from database" + ex.Message);
                }
                return dtGet;
            }
        }

        public string GetDataValue(string table, string column, int apptID)
        {
            string retval = "";
            
            try
            {
                //////////////////////
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = new SqlCommand("sp_getLatestDataValue", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;

                    cmdProcedure.Parameters.Add("@apptID", SqlDbType.Int);
                    cmdProcedure.Parameters["@apptID"].Value = apptID;
                    cmdProcedure.Parameters.Add("@tableName", SqlDbType.NVarChar);
                    cmdProcedure.Parameters["@tableName"].Value = table;
                    cmdProcedure.Parameters.Add("@columnName", SqlDbType.NVarChar);
                    cmdProcedure.Parameters["@columnName"].Value = column;

                    SqlDataReader reader = cmdProcedure.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(0) == false)
                            {
                                retval = reader.GetString(0);
                            }
                        }
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("Could not get data value " + table + " | " + column + "\n\t" + e.ToString());
            }

            return retval;

        }


        /**********************************************************************/
        public static void AddUnitnumToCommand(Person p, SqlCommand c)
        {
            c.Parameters.Add("@patientUnitnum", SqlDbType.NVarChar);
            if (p.relativeID != 1)
                c.Parameters["@patientUnitnum"].Value = p.owningFHx.proband.unitnum;
            else
            {
                Patient patient = (Patient)p;
                c.Parameters["@patientUnitnum"].Value = patient.unitnum;
            }
        }

        /// <summary>
        /// Use this when there are no records returned whose fields need capturing
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sPparams"></param>
        /// <returns></returns>
        public int RunSPWithParams(String spName, ParameterCollection sPparams)
        {
            int affectedRecords = 0;

            try
            {
                //////////////////////
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = new SqlCommand(spName, connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    cmdProcedure.CommandTimeout = 180; //change command timeout from default to 3 minutes

                    if (sPparams != null)
                    {
                        foreach (string param in sPparams.getKeys())
                        {
                            cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                            cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                        }
                    }
                    affectedRecords = cmdProcedure.ExecuteNonQuery();
                } //end of using connection

            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + spName + "\n\t" + e);
            }

            return affectedRecords;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //

        /// <summary>
        /// Use this when there are records returned whose fields need capturing from a string
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sPparams"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderWithParams(String sqlStr, ParameterCollection sPparams)
        {
            try
            {
                //////////////////////

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmdProcedure = new SqlCommand(sqlStr, connection);
                //cmdProcedure.CommandType = CommandType.StoredProcedure;
                cmdProcedure.CommandTimeout = 180; //change command timeout from default to 3 minutes

                if (sPparams != null)
                {
                    foreach (string param in sPparams.getKeys())
                    {
                        cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                        cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                    }
                }
                return (cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + sqlStr + "\n\t" + e);
                return (SqlDataReader)null;
            }
        }
        /// <summary>
        /// Use this when there are records returned whose fields need capturing from a stored procedure
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sPparams"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderSPWithParams(String spName, ParameterCollection sPparams)
        {
            try
            {
                //////////////////////

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmdProcedure = new SqlCommand(spName, connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;
                cmdProcedure.CommandTimeout = 180; //change command timeout from default to 3 minutes

                if (sPparams != null)
                {
                    foreach (string param in sPparams.getKeys())
                    {
                        cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                        cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                    }
                }
                return (cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + spName + "\n\t" + e);
                return (SqlDataReader)null;
            }
        }

        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //

        /// <summary>
        /// Use this when there are records returned whose fields need capturing
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="sPparams"></param>
        /// <returns></returns>
        public System.Xml.XmlReader ExecuteXmlReaderSPWithParams(String spName, ParameterCollection sPparams)
        {
            try
            {
                //////////////////////

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmdProcedure = new SqlCommand(spName, connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;
                cmdProcedure.CommandTimeout = 180; //change command timeout from default to 3 minutes

                if (sPparams != null)
                {
                    foreach (string param in sPparams.getKeys())
                    {
                        cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                        cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                    }
                }
                return (cmdProcedure.ExecuteXmlReader());
            }
            catch (Exception e)
            {
                // get call stack
                StackTrace stackTrace = new StackTrace();

                // get calling method name
                String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
                Logger.Instance.WriteToLog("[BCDB2] from [" + callingRoutine + "] Could not execute SQL query: " + spName + "\n\t" + e);
                return (System.Xml.XmlReader)null;
            }
        }

        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}

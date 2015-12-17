using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.Clinic;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Data;

namespace RiskApps3.Model.PatientRecord
{
    public class Appointment : HraObject
    {
        public int apptID;
        public string type;
        public string unitnum;
        public string patientname;
        public string diseases;
        public string dob;
        public string apptdate;
        public string appttime;
        public string apptphysname;
        public int familyNumber;
        public string accession;
        public int clinicID;
        public DateTime GoldenApptTime;
        public DateTime apptdatetime;
        public int GoldenAppt;
        public string clinicName;

        public string CloudWebQueueState = "";
        public int CloudWebQueueSynchId = -1;

        public string surveyType = "";
        public DateTime riskdatacompleted;

        public int? formattedFamilyNumber
        {
            get
            {
                if (familyNumber > 0)
                    return familyNumber;
                else
                    return null;
            }
            set
            {

            }
        }
        public static void UpdateAppointmentUnitnum(int apptid, string unitnum)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_updateApptUnitnum", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                        cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                        cmdProcedure.Parameters["@unitnum"].Value = unitnum;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public static int CreateAppointment()
        {
            int newApptId = -1; 
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_createMasteryAppointment", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    cmdProcedure.Parameters.Add("@Return_Value", SqlDbType.Int);
                    cmdProcedure.Parameters["@Return_Value"].Direction = ParameterDirection.ReturnValue;

                    cmdProcedure.ExecuteNonQuery();
                    newApptId = (int)cmdProcedure.Parameters["@Return_Value"].Value;
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            } 
            return newApptId;
        }
        public static void DeleteApptData(int apptid, bool KeepEmptyAppt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_deleteWebAppointment", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                        cmdProcedure.Parameters.Add("@keepApptRecord", SqlDbType.Bit);
                        cmdProcedure.Parameters["@keepApptRecord"].Value = KeepEmptyAppt;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public static void MarkIncomplete(int apptid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptIncomplete", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        public static void MarkComplete(int apptid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptComplete", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
        public static void MarkPrinted(int apptid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptPrinted", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        public static void MarkForAutomation(int apptid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();
                    SqlCommand cmdProcedure = new SqlCommand("sp_3_MarkApptForAutomation", connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    if (apptid > 0)
                    {
                        cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                        cmdProcedure.Parameters["@apptid"].Value = apptid;
                    }
                    cmdProcedure.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }
    }
}

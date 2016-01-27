
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using RiskApps3.Utilities;
using RiskApps3.View;
using System.Diagnostics;
using System.Data;

namespace RiskApps3.Model.Clinic.Queues
{
    public class FollowupCohort : HraObject
    {
        /**************************************************************************************************/
        public List<FollowupCohortEntry> FollowupCohortPeople = new List<FollowupCohortEntry>();


        /**************************************************************************************************/
        public FollowupCohort()
        {

        }

        /**************************************************************************************************/
        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadFollowupCohort", connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;

                try
                {
                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader != null)
                    {
                        while (reader.Read())
                        {

                            FollowupCohortEntry hrbqe = new FollowupCohortEntry();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    foreach (FieldInfo fi in hrbqe.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                                    {
                                        string name = fi.Name;
                                        if (name == reader.GetName(i))
                                        {
                                            fi.SetValue(hrbqe, reader.GetValue(i));
                                        }
                                    }
                                }
                            }
                            FollowupCohortPeople.Add(hrbqe);

                        }
                        reader.Close();
                    }
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog(e.ToString());
                }
            }
        }
            

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }
    /**************************************************************************************************/
    public class FollowupCohortEntry
    {
        public string unitnum;
        public string patientname;
        public string dob;
        public string MammoRec;
        public string TVSRec;
        public string CA125Rec;
        public string MRIRec;

    }
}
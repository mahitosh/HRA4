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
    public class HighRiskColonQueue : HraObject
    {
        /**************************************************************************************************/
        public List<HighRiskColonQueueEntry> HighRiskColonPeople = new List<HighRiskColonQueueEntry>();


        /**************************************************************************************************/

        public HighRiskColonQueue()
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
            string sql = "select * from v_3_HighRiskColonQueue";
            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader != null)
                    {
                        while (reader.Read())
                        {

                            HighRiskColonQueueEntry hrcqe = new HighRiskColonQueueEntry();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    foreach (FieldInfo fi in hrcqe.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                                    {
                                        string name = fi.Name;
                                        if (name == reader.GetName(i))
                                        {
                                            fi.SetValue(hrcqe, reader.GetValue(i));
                                            break;
                                        }
                                    }
                                }
                            }
                            HighRiskColonPeople.Add(hrcqe);

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
    public class HighRiskColonQueueEntry
    {
        public string unitnum;
        public DateTime apptdatetime;
        public double BRCAProScore;
        public double MyriadScore;
        public double MaxBRCAProMyriadScore;
        public int isRCPt;
        public int omit;
        public int isNWHTestPt;
        public string patientname;
        public string dob;
        public string gender;
        public string Diseases;
        public double MMRProScore;
        public double PREMMScore;
        public double MaxMMRProPREMMScore;
        public double BreastCancerAge;
        public int OvarianCancer;
        public double ColoRectalEndoUterCancerAge;
        public double ClausLifetimeScore;
        public double GailLifetimeScore;
        public double GailFiveYearScore;
        public double BRCAProLifetimeScore;
        public double MaxLifetimeScore;
        public int AH;
        public int LCIS;
        public int AbnormalBx;
        public int ChestRadiation;
        public int genTested;
        public string SBHCReason;
        public int age;
        public int genTestedPt;
        public double TCMutProb;
        public double TCLifetime;
        public string apptphysname;
        public string geneNames;
        public int NumTestedFamilyMembers;
        public int NumTestableFamilyMembers;
        public int NumRelativesWithAgeAndVitalStatus;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;
using System.Reflection;
using System.Data;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class MedicationHx : HraObject
    {
        
        /**************************************************************************************************/
        public Patient theProband;

        [DataMember] public List<Medication> Medications;
        [DataMember] public Chemoprevention chemoprevention;

        /**************************************************************************************************/
        public override void LoadFullObject()
        {
            base.LoadFullObject();
            chemoprevention.LoadFullObject();

        }
        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
 	         base.PersistFullObject(e);
             chemoprevention.patientOwning = theProband;
             chemoprevention.PersistFullObject(e);
        }
        /**************************************************************************************************/

        public MedicationHx() { } // Default constructor for serialization

        public MedicationHx(Patient owner)
        {
            theProband = owner;

            Medications = new List<Medication>();
            chemoprevention= new Chemoprevention(owner);
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            foreach (Medication med in Medications)
            {
                med.ReleaseListeners(view);
            }

            chemoprevention.ReleaseListeners(view);
            base.ReleaseListeners(view);
        }
        
        /**************************************************************************************************/
        public string GerSummaryText()
        {
            string retval = "";
            foreach (Medication med in Medications)
            {
                
            }
            string trimChars = " ;";
            return retval.Trim(trimChars.ToCharArray());
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //string sql = "Select * from v_3_MedicationHx where unitnum = '" + theProband.unitnum + "'";

            //SqlDataReader reader = BCDB2.Instance.ExecuteReader(sql);
            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();
                SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadMedicationHx", connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;

                //SqlCommand command = new SqlCommand(sql, connection);
                cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                cmdProcedure.Parameters["@unitnum"].Value = theProband.unitnum;

                cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                cmdProcedure.Parameters["@apptid"].Value = theProband.apptid;

                try
                {
                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader != null)
                    {
                        Medications.Clear();
                        while (reader.Read())
                        {
                            Medication med = new Medication(this);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    foreach (FieldInfo fi in med.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                                    {
                                        string name = fi.Name;
                                        if (name == reader.GetName(i))
                                        {
                                            fi.SetValue(med, reader.GetValue(i));
                                            break;
                                        }
                                    }
                                }
                            }
                            med.HraState = States.Ready;

                            Medications.Add(med);
                        }
                        reader.Close();
                    }
                }
                             catch (Exception ex)
                {
                    Logger.Instance.WriteToLog(ex.ToString());
                }
            }
            base.BackgroundLoadWork();
        }
    }
}

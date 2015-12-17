using System;
using System.Data.SqlClient;
using RiskApps3.Model.Security;
using RiskApps3.Utilities;
using System.Reflection;
using RiskApps3.View;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract(IsReference=true)]
    public class PastMedicalHistory : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Person RelativeOwningPMH;

        [DataMember] public ClinicalObservationList Observations;
        [DataMember] public GeneticTestList GeneticTests;

        /**************************************************************************************************/

        public PastMedicalHistory() { } // Default constructor for serialization

        public PastMedicalHistory(Person owner)
        {
            RelativeOwningPMH = owner;

            Observations = new ClinicalObservationList(this);

            GeneticTests = new GeneticTestList(this);
        }

        /**************************************************************************************************/

        public override void ReleaseListeners(object view)
        {
            Observations.ReleaseListeners(view);
            GeneticTests.ReleaseListeners(view);
            //foreach (ClincalObservation co in Observations)
            //{
            //    co.ReleaseListeners(view);
            //}
            //foreach (GeneticTest gt in GeneticTests)
            //{
            //    gt.ReleaseListeners(view);
            //}

            base.ReleaseListeners(view);
        }
                
        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            Observations.OwningPMH = this;
            GeneticTests.OwningPMH = this;
            Observations.PersistFullList(e);
            GeneticTests.PersistFullList(e);
        }
        /**************************************************************************************************/

        public string GerSummaryText()
        {
            string retval = "";
            foreach (ClincalObservation co in Observations)
            {
                retval += ("; " + co.GetSummaryText());
            }
            string trimChars = " ;";
            return retval.Trim(trimChars.ToCharArray());
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {

            LoadClinicalObservations(RelativeOwningPMH.owningFHx.proband.unitnum);
            LoadGeneticTests(RelativeOwningPMH.owningFHx.proband.unitnum);

            base.BackgroundLoadWork();
        }

        /**************************************************************************************************/
        
        private void LoadClinicalObservations(string patient_unitnum)
        {
            //Observations.LoadList();
            Observations.BackgroundListLoad();
        }

        /**************************************************************************************************/

        private void LoadGeneticTests(string patient_unitnum)
        {
            //GeneticTests.LoadList();
            GeneticTests.BackgroundListLoad();
        }

        private void saveObservations(HraView sendingView)
        {
            int i = 1;
            foreach (ClincalObservation co in Observations)
            {
                co.instanceID = i;
                i++;

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(sendingView);
                co.BackgroundPersistWork(args);
               //co.SignalModelChanged(args);
               // BackgroundPersistWork(null, "sp_3_Save_ClinicalObservation", RelativeOwningPMH.ownningFHx.proband,
               //                       RelativeOwningPMH, co.instanceID);
                 

            }
        }

        private void saveGeneticTests(HraView sendingView)
        {
            int i = 1;
            foreach (GeneticTest gt in GeneticTests)
            {
                gt.instanceID = i;
                i++;

                HraModelChangedEventArgs args = new HraModelChangedEventArgs(sendingView);

                gt.BackgroundPersistWork(args);

            }
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            int relativeId = RelativeOwningPMH.relativeID;
            //Console.WriteLine("relativeId=" + relativeId);
  
            String unitnum = RelativeOwningPMH.owningFHx.proband.unitnum;
            //Console.WriteLine("unitnum=" + unitnum);


            if (e.updatedMembers.Count > 0)
            {
                foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                {
                    string name = fi.Name;
                    if (name.Equals("Observations"))
                    {
                        saveObservations(e.sendingView);
                    }
                    else if (name.Equals("GeneticTests"))
                    {
                        saveGeneticTests(e.sendingView);
                    }
                }
            }
            else
            {
                saveObservations(e.sendingView);
                saveGeneticTests(e.sendingView);
            }
        }
    }
}



//public void RemoveObservation(string dx)
//{
//    if (Observations.IsLoaded == false)
//    {
//        Observations.BackgroundListLoad();
//    }
//    ClincalObservation doomed = null;
//    foreach(ClincalObservation co in Observations)
//    {
//        if (string.Compare(dx, co.disease, true) == 0)
//        {
//            doomed = co;
//            break;
//        }
//    }
//    if (doomed != null)
//    {
//        Observations.RemoveFromList(doomed, null);
//    }
//}


//public List<GeneticTest> GeneticTests;
//GeneticTests = new List<GeneticTest>();


// if (RelativeOwningPMH != null)
//    RelativeOwningPMH.ReleaseListeners(view);




//string sql = "Select * from v_3_geneticTest where unitnum = '" + patient_unitnum + "' AND relativeID = " +
//             RelativeOwningPMH.relativeID;
//SqlDataReader reader = BCDB2.Instance.ExecuteReader(sql);

/*
using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
{
    connection.Open();

    SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadgeneticTest", connection);
    cmdProcedure.CommandType = CommandType.StoredProcedure;


    cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
    cmdProcedure.Parameters["@unitnum"].Value = patient_unitnum;

    cmdProcedure.Parameters.Add("@relId", SqlDbType.NVarChar);
    cmdProcedure.Parameters["@relId"].Value = RelativeOwningPMH.relativeID;

    try
    {
        SqlDataReader reader = cmdProcedure.ExecuteReader();

        if (reader != null)
        {
            while (reader.Read())
            {

                GeneticTest gt = new GeneticTest(this);

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.IsDBNull(i) == false)
                    {
                        foreach (FieldInfo fi in gt.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                        {
                            string name = fi.Name;
                            if (name == reader.GetName(i))
                            {
                               // fi.SetValue(gt, reader.GetValue(i));
                                SetFieldInfoValue(fi, reader.GetValue(i));
                                break;
                            }
                        }
                    }
                }
                gt.hra_state = States.Ready;
                gt.LoadObject();
                GeneticTests.Add(gt);
            }
            reader.Close();
        }
    }
    catch (Exception ex)
    {
        Logger.Instance.WriteToLog(ex.ToString());
    }
}
 */

/*
private void LoadClinicalObservations(string patient_unitnum)
{

    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
    {
        connection.Open();

        SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadPastMedicalHistory", connection);
        cmdProcedure.CommandType = CommandType.StoredProcedure;


        cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
        cmdProcedure.Parameters["@unitnum"].Value = patient_unitnum;

        cmdProcedure.Parameters.Add("@relId", SqlDbType.NVarChar);
        cmdProcedure.Parameters["@relId"].Value = RelativeOwningPMH.relativeID;

        try
        {
            SqlDataReader reader = cmdProcedure.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    ClincalObservation co = new ClincalObservation(this);

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader.IsDBNull(i) == false)
                        {
                            foreach (FieldInfo fi in co.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                            {
                                string name = fi.Name;
                                if (name == reader.GetName(i))
                                {
                                    fi.SetValue(co, reader.GetValue(i));
                                    break;
                                }
                            }
                        }
                    }
                    co.hra_state = States.Ready;

                    Observations.Add(co);
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Logger.Instance.WriteToLog(ex.ToString());
        }
    }
            
}
*/

/**************************************************************************************************/

//public override bool LoadObject()
//{
//    //Console.Out.WriteLine(DateTime.Now.ToLongTimeString() + " PMH Load for Rel " + RelativeOwningPMH.relativeID);

//    Observations.Clear();
//    GeneticTests.Clear();

//    return base.LoadObject();
//}

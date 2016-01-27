using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Controllers;
using RiskApps3.Model.Security;
using RiskApps3.Utilities;
using System.Data;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class GeneticTest : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public PastMedicalHistory owningPMH;

        /**************************************************************************************************/
        public int instanceID = 0;
        [DataMember] [HraAttribute] public string testMonth;
        [DataMember] [HraAttribute] public string testDay;
        [DataMember] [HraAttribute] public string testYear;
        [DataMember] [HraAttribute] public string comments;
        [DataMember] [HraAttribute] public int panelID;
        [DataMember] [HraAttribute] public string status;
        [DataMember] [HraAttribute] public string accession;
        [DataMember] [HraAttribute] public string test_lab;
        [DataMember] [HraAttribute] public string test_code;
        [DataMember] [HraAttribute] public int isPtAware = -1;
        
        [DataMember] public string panelShortName;
        [DataMember] public string panelName;

        [DataMember] public List<GeneticTestResult> GeneticTestResults;

        public bool IsASOTest
        {
            get
            {
                if (string.Compare(this.GetPanelName(),"Obligate Familial Mutation",true)==0 || 
                    string.Compare(this.GetPanelName(),"Familial Known Mutation Test",true)==0)
                    return true;
                else
                    return false;
            }
        }


        #region getters_setters
        /*****************************************************/
        
        public string GeneticTest_testMonth
        {
            get
            {
                return testMonth;
            }
            set
            {
                if (value != testMonth)
                {
                    testMonth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("testMonth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_testDay
        {
            get
            {
                return testDay;
            }
            set
            {
                if (value != testDay)
                {
                    testDay = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("testDay"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_testYear
        {
            get
            {
                return testYear;
            }
            set
            {
                if (value != testYear)
                {
                    testYear = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("testYear"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_comments
        {
            get
            {
                return comments;
            }
            set
            {
                if (value != comments)
                {
                    comments = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("comments"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public int GeneticTest_panelID
        {
            get
            {
                return panelID;
            }
            set
            {
                if (value != panelID)
                {
                    panelID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("panelID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("status"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_accession
        {
            get
            {
                return accession;
            }
            set
            {
                if (value != accession)
                {
                    accession = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("accession"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_test_lab
        {
            get
            {
                return test_lab;
            }
            set
            {
                if (value != test_lab)
                {
                    test_lab = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("test_lab"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string GeneticTest_test_code
        {
            get
            {
                return test_code;
            }
            set
            {
                if (value != test_code)
                {
                    test_code = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("test_code"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        
        public int GeneticTest_isPtAware
        {
            get
            {
                return isPtAware;
            }
            set
            {
                if (value != isPtAware)
                {
                    isPtAware = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("isPtAware"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion


        /**************************************************************************************************/

        public GeneticTest() { } // Default constructor for serialization

        public GeneticTest(PastMedicalHistory pmh)
        {
            owningPMH = pmh;
            GeneticTestResults = new List<GeneticTestResult>();
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            base.PersistFullObject(e);
            foreach (GeneticTestResult gtr in GeneticTestResults)
            {
                gtr.PersistFullObject(e);
            }
        }

        /**************************************************************************************************/
        public override bool LoadObject()
        {
            GeneticTestResults.Clear();

            return base.LoadObject();
        }


        /**************************************************************************************************/
        public string GetPanelName()
        {
            return SessionManager.Instance.MetaData.GeneticTests.GetPanelNameFromID(panelID);
        }

        /**************************************************************************************************/
        public string GetResultSummaryText()
        {
            string retval = "";

            foreach (GeneticTestResult geneticTestResult in GeneticTestResults)
            {
                string val = geneticTestResult.getNonNegativeResult();
                if (val.Length>0)
                {
                    if (retval.Length>0)
                    {
                        retval = retval + "; ";
                    }
                    retval = retval + val;
                }
            }
            return retval;
        }

        public void LoadResultsOnly()
        {
            LoadGeneticTestResults(this.owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum,
                                   this.owningPMH.RelativeOwningPMH.relativeID,
                                   this.instanceID);
        }
        /**************************************************************************************************/
        private void LoadGeneticTestResults(string patient_unitnum, int relativeID, int instanceId)
        {
            using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            {
                connection.Open();

                SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadgeneticTestResults", connection);
                cmdProcedure.CommandType = CommandType.StoredProcedure;

                cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                cmdProcedure.Parameters["@unitnum"].Value = patient_unitnum;
                cmdProcedure.Parameters.Add("@apptid", SqlDbType.Int);
                cmdProcedure.Parameters["@apptid"].Value = owningPMH.RelativeOwningPMH.owningFHx.proband.apptid;

                cmdProcedure.Parameters.Add("@relId", SqlDbType.NVarChar);
                cmdProcedure.Parameters["@relId"].Value = relativeID;

                cmdProcedure.Parameters.Add("@instanceId", SqlDbType.Int);
                cmdProcedure.Parameters["@instanceId"].Value = instanceID;

                try
                {
                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            GeneticTestResult geneticTestResult = new GeneticTestResult(this);

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    foreach (FieldInfo fi in geneticTestResult.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
                                    {
                                        string name = fi.Name;
                                        if (name == reader.GetName(i))
                                        {
                                            SetFieldInfoValue(fi, reader.GetValue(i), geneticTestResult);
                                            break;
                                        }
                                    }
                                }
                            }
                            geneticTestResult.HraState = States.Ready;
                            GeneticTestResults.Add(geneticTestResult);
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

        public override void BackgroundLoadWork()
        {
            string patient_unitnum = "";
            int relativeID = -1;

            // if this is a relative, get the unitnum form the familyhistory object
            if (owningPMH.RelativeOwningPMH.owningFHx != null)
            {
                patient_unitnum = owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum;
                relativeID = owningPMH.RelativeOwningPMH.relativeID;
            }
            else
            {
                Patient p = (Patient) (owningPMH.RelativeOwningPMH);
                patient_unitnum = p.unitnum;
                relativeID = 1;
            }

            LoadGeneticTestResults(patient_unitnum, relativeID, instanceID);
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            lock (this)
            {

                ParameterCollection pc = new ParameterCollection();
                pc.Add("patientUnitnum", this.owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
                pc.Add("apptid", this.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);
                pc.Add("relativeID", this.owningPMH.RelativeOwningPMH.relativeID);
                pc.Add("instanceID", instanceID, true);

                DoPersistWithSpAndParams(e,
                                          "sp_3_Save_GeneticTest",
                                          ref pc);

                this.instanceID = (int)pc["instanceID"].obj;

            }
        }
        public List<GeneticTestResult> GetNonNegativeResults()
        {
            List<GeneticTestResult> results=new List<GeneticTestResult>();

            foreach (GeneticTestResult geneticTestResult in GeneticTestResults)
            {
                if (geneticTestResult.isNonNegativeResult())
                {
                    results.Add(geneticTestResult);
                }
            }

            return results;
        }

        public bool HasNonNegativeResults(List<GeneticTestResult> toExclude)
        {
            return GeneticTestResults.Any(tr => tr.isNonNegativeResult() && !toExclude.Contains(tr));
        }

        internal void SetNegativeResults()
        {
            foreach (GeneticTestResult geneticTestResult in GeneticTestResults)
            {
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                if (this.IsASOTest)
                {
                    geneticTestResult.ASOResult = "Not Found";
                }
                else
                {
                    if (string.IsNullOrEmpty(geneticTestResult.resultSignificance))
                    {
                        geneticTestResult.resultSignificance = "Negative";
                    }
                }


                geneticTestResult.SignalModelChanged(args);
            }
        }
    }
}


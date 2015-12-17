using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Runtime.Serialization;

using System.Reflection;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord.Labs
{
    /**************************************************************************************************/
    [CollectionDataContract]
    [KnownType(typeof(LabResult))]
    public class LabsHx : HRAList
    {
        /**************************************************************************************************/
        private ParameterCollection pc = new ParameterCollection();
        
        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public LabsHx() { }  // Default constructor for serialization

        public LabsHx(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] {};
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadLabsHx",
                                                pc,
                                                typeof(LabResult),
                                                constructor_args);
            DoListLoad(lla);
        }
    }
    /**************************************************************************************************/
    [DataContract]
    public class LabResult : Diagnostic
    {
        //public string unitnum;
        [DataMember] public int instanceID = 0;
        [DataMember] [HraAttribute]  public string MID;
        [DataMember] [HraAttribute]  public string SITE ;
        [DataMember] [HraAttribute]  public string Datatype ;
        [DataMember] [HraAttribute]  public string Res ;
        [DataMember] [HraAttribute]  public string TestCode ;
        [DataMember] [HraAttribute]  public string TestDesc ;
        [DataMember] [HraAttribute]  public string CDRTestClass ;
        [DataMember] [HraAttribute]  public string TestShort ;
        [DataMember] [HraAttribute]  public string Loinc ;
        [DataMember] [HraAttribute]  public string SpecMID ;
        [DataMember] [HraAttribute]  public string Sts ;
        [DataMember] [HraAttribute]  public string RRR ;
        [DataMember] [HraAttribute]  public string RU ;
        [DataMember] [HraAttribute]  public string Com ;
        [DataMember] [HraAttribute]  public string AbnFlg ;
        [DataMember] [HraAttribute]  public string TOXRNG;
        [DataMember] [HraAttribute]  public string CorrFlag;

        public LabResult() { }  // Default constructor for serialization

        /*****************************************************/
        public string LabResult_MID
        {
            get
            {
                return MID;
            }
            set
            {
                if (value != MID)
                {
                    MID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_SITE
        {
            get
            {
                return SITE;
            }
            set
            {
                if (value != SITE)
                {
                    SITE = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("SITE"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_Datatype
        {
            get
            {
                return Datatype;
            }
            set
            {
                if (value != Datatype)
                {
                    Datatype = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Datatype"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_Res
        {
            get
            {
                return Res;
            }
            set
            {
                if (value != Res)
                {
                    Res = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Res"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_TestCode
        {
            get
            {
                return TestCode;
            }
            set
            {
                if (value != TestCode)
                {
                    TestCode = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TestCode"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_TestDesc
        {
            get
            {
                return TestDesc;
            }
            set
            {
                if (value != TestDesc)
                {
                    TestDesc = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TestDesc"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_CDRTestClass
        {
            get
            {
                return CDRTestClass;
            }
            set
            {
                if (value != CDRTestClass)
                {
                    CDRTestClass = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CDRTestClass"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_TestShort
        {
            get
            {
                return TestShort;
            }
            set
            {
                if (value != TestShort)
                {
                    TestShort = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TestShort"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_Loinc
        {
            get
            {
                return Loinc;
            }
            set
            {
                if (value != Loinc)
                {
                    Loinc = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Loinc"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_SpecMID
        {
            get
            {
                return SpecMID;
            }
            set
            {
                if (value != SpecMID)
                {
                    SpecMID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("SpecMID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_Sts
        {
            get
            {
                return Sts;
            }
            set
            {
                if (value != Sts)
                {
                    Sts = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Sts"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_RRR
        {
            get
            {
                return RRR;
            }
            set
            {
                if (value != RRR)
                {
                    RRR = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RRR"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_RU
        {
            get
            {
                return RU;
            }
            set
            {
                if (value != RU)
                {
                    RU = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RU"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_Com
        {
            get
            {
                return Com;
            }
            set
            {
                if (value != Com)
                {
                    Com = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Com"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_AbnFlg
        {
            get
            {
                return AbnFlg;
            }
            set
            {
                if (value != AbnFlg)
                {
                    AbnFlg = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("AbnFlg"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_TOXRNG
        {
            get
            {
                return TOXRNG;
            }
            set
            {
                if (value != TOXRNG)
                {
                    TOXRNG = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TOXRNG"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string LabResult_CorrFlag
        {
            get
            {
                return CorrFlag;
            }
            set
            {
                if (value != CorrFlag)
                {
                    CorrFlag = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CorrFlag"));
                    SignalModelChanged(args);
                }
            }
        } 


        public override string GetDiagnosticType()
        {
            return TestDesc;
        }
        public override void SetDiagnosticType(string text)
        {
            TestDesc = text;
        }

        public override string GetValue()
        {
            return (Res + " " + RU).Trim();
        }
        public override void SetValue(string text)
        {
            LabResult_Res = text;
        }

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("instanceID", instanceID, true);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_LabResult",
                                      ref pc);

            this.instanceID = (int)pc["instanceID"].obj;  
        }
    }
}




        
        //    try
        //    {
        //        //////////////////////
        //        using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
        //        {
        //            connection.Open();

        //            SqlCommand cmdProcedure = new SqlCommand("sp_3_Save_LabResult", connection);
        //            cmdProcedure.CommandType = CommandType.StoredProcedure;

        //            cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
        //            cmdProcedure.Parameters["@unitnum"].Value = unitnum;

        //            cmdProcedure.Parameters.Add("@instanceID", SqlDbType.Int);
        //            cmdProcedure.Parameters["@instanceID"].Value = instanceID;
        //            cmdProcedure.Parameters["@instanceID"].Direction = ParameterDirection.InputOutput;

        //            /////////////////////////
        //            if (e.updatedMembers.Count > 0)
        //            {
        //                foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        //                {
        //                    string name = fi.Name;

        //                    //if there is a specified member(s) then just add those, otherwise do all

        //                    foreach (MemberInfo mi in e.updatedMembers)
        //                    {
        //                        if (name == mi.Name)
        //                        {
        //                            SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
        //                            cmdProcedure.Parameters.Add("@" + name, sType);
        //                            cmdProcedure.Parameters["@" + name].Value = fi.GetValue(this);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        //                {
        //                    string name = fi.Name;

        //                    foreach (MemberInfo mi in GetType().GetMembers())
        //                    {
        //                        //todo: optomize this double loop
        //                        if (name == mi.Name && IsPersistable(mi))
        //                        {
        //                            SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
        //                            cmdProcedure.Parameters.Add("@" + name, sType);
        //                            cmdProcedure.Parameters["@" + name].Value = fi.GetValue(this);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }

        //            cmdProcedure.ExecuteNonQuery();
        //            instanceID = (int)cmdProcedure.Parameters["@instanceID"].Value;
        //            connection.Close();
        //        } //end of using connection
        //    }
        //    catch (Exception exc)
        //    {
        //        Logger.Instance.WriteToLog("Persisting object - " + exc.ToString());
        //    }

        //}


    

    //    /**************************************************************************************************/
    //    public LabsHx(string mrn)
    //    {
    //        unitnum = mrn;
    //    }

    //    /**************************************************************************************************/
    //    public override void BackgroundLoadWork()
    //    {
    //        using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
    //        {
    //            connection.Open();
    //            SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadLabsHx", connection);
    //            cmdProcedure.CommandType = CommandType.StoredProcedure;

    //            //SqlCommand command = new SqlCommand(sql, connection);
    //            cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
    //            cmdProcedure.Parameters["@unitnum"].Value = unitnum;

    //            try
    //            {
    //                SqlDataReader reader = cmdProcedure.ExecuteReader();

    //                if (reader != null)
    //                {
    //                    while (reader.Read())
    //                    {
    //                        LabResult result = new LabResult();
    //                        for (int i = 0; i < reader.FieldCount; i++)
    //                        {
    //                            if (reader.IsDBNull(i) == false)
    //                            {
    //                                foreach (FieldInfo fi in result.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
    //                                {
    //                                    string name = fi.Name;
    //                                    if (name == reader.GetName(i))
    //                                    {
    //                                        fi.SetValue(result, reader.GetValue(i));
    //                                        break;
    //                                    }
    //                                }
    //                            }
    //                        }
    //                        labs.Add(result);
    //                    }
    //                    reader.Close();
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                Logger.Instance.WriteToLog(ex.ToString());
    //            }
    //        }
    //        base.BackgroundLoadWork();

    //    }

    //    /**************************************************************************************************/
    //    public override void ReleaseListeners(object view)
    //    {
    //        foreach (LabResult result in labs)
    //        {
    //            result.ReleaseListeners(view);
    //        }

    //        base.ReleaseListeners(view);
    //    }
    //}
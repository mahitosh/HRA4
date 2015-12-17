
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;

using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    /**************************************************************************************************/
    [CollectionDataContract]
    [KnownType(typeof(TransvaginalImagingStudy))]
    public class TransvaginalImagingHx : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();

        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public TransvaginalImagingHx() { }  // Default constructor for serialization

        public TransvaginalImagingHx(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] {  };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadTransvaginalImagingStudy",
                                                pc,
                                                typeof(TransvaginalImagingStudy),
                                                constructor_args);
            DoListLoad(lla);
        }
    }

    /**************************************************************************************************/
    [DataContract]
    public class TransvaginalImagingStudy : Diagnostic
    {
        //[DataMember] public string unitnum;
        [DataMember] public int instanceID;
        
        [DataMember] [HraAttribute] public string imagingType;
        [DataMember] [HraAttribute] public string impression;
        [DataMember] [HraAttribute] public string leftOvary;
        [DataMember] [HraAttribute] public string rightOvary;
        [DataMember] [HraAttribute] public string uterus;
        [DataMember] [HraAttribute] public string leftTube;
        [DataMember] [HraAttribute] public string rightTube;

        public TransvaginalImagingStudy() { } // Default constructor for serialization

        /*****************************************************/
        public string Tvs_imagingType
        {
            get
            {
                return imagingType;
            }
            set
            {
                if (value != imagingType)
                {
                    imagingType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("imagingType"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_impression
        {
            get
            {
                return impression;
            }
            set
            {
                if (value != impression)
                {
                    impression = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("impression"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_leftOvary
        {
            get
            {
                return leftOvary;
            }
            set
            {
                if (value != leftOvary)
                {
                    leftOvary = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("leftOvary"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_rightOvary
        {
            get
            {
                return rightOvary;
            }
            set
            {
                if (value != rightOvary)
                {
                    rightOvary = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("rightOvary"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_uterus
        {
            get
            {
                return uterus;
            }
            set
            {
                if (value != uterus)
                {
                    uterus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("uterus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_leftTube
        {
            get
            {
                return leftTube;
            }
            set
            {
                if (value != leftTube)
                {
                    leftTube = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("leftTube"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Tvs_rightTube
        {
            get
            {
                return rightTube;
            }
            set
            {
                if (value != rightTube)
                {
                    rightTube = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("rightTube"));
                    SignalModelChanged(args);
                }
            }
        } 

        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override string GetDiagnosticType()
        {
            return Tvs_imagingType;
        }
        public override void SetDiagnosticType(string text)
        {
            Tvs_imagingType = text;
        }

        public override string GetValue()
        {
            return Tvs_impression;
        }
        public override void SetValue(string text)
        {
            Tvs_impression = text;
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("instanceID", instanceID, true);
            pc.Add("reportId", reportId, true);
            pc.Add("reportIdType", reportIdType, true,1024);

            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_TransvaginalImagingStudy",
                                     ref pc);

            this.instanceID = (int)pc["instanceID"].obj;
            this.reportId = (string)pc["reportId"].obj;
            this.reportIdType = (string)pc["reportIdType"].obj;
        }
    }
}


/**************************************************************************************************/
//public List<TransvaginalImagingStudy> imagingStudies = new List<TransvaginalImagingStudy>();
////public string unitnum;

///**************************************************************************************************/
//public TransvaginalImagingHx(string mrn)
//{
//    unitnum = mrn;
//}

///**************************************************************************************************/
//public override void BackgroundLoadWork()
//{
//    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
//    {
//        connection.Open();
//        SqlCommand cmdProcedure = new SqlCommand("sp_3_LoadTransvaginalImagingStudy", connection);
//        cmdProcedure.CommandType = CommandType.StoredProcedure;

//        //SqlCommand command = new SqlCommand(sql, connection);
//        cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
//        cmdProcedure.Parameters["@unitnum"].Value = unitnum;

//        try
//        {
//            SqlDataReader reader = cmdProcedure.ExecuteReader();

//            if (reader != null)
//            {
//                while (reader.Read())
//                {
//                    TransvaginalImagingStudy study = new TransvaginalImagingStudy();
//                    for (int i = 0; i < reader.FieldCount; i++)
//                    {
//                        if (reader.IsDBNull(i) == false)
//                        {
//                            foreach (FieldInfo fi in study.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
//                            {
//                                string name = fi.Name;
//                                if (name == reader.GetName(i))
//                                {
//                                    fi.SetValue(study, reader.GetValue(i));
//                                    break;
//                                }
//                            }

//                        }
//                    }
//                    imagingStudies.Add(study);
//                }
//                reader.Close();
//            }
//        }
//        catch (Exception ex)
//        {
//            Logger.Instance.WriteToLog(ex.ToString());
//        }
//    }
//    base.BackgroundLoadWork();
//}

//    /**************************************************************************************************/
//    public override void ReleaseListeners(object view)
//    {
//        foreach (TransvaginalImagingStudy bis in imagingStudies)
//        {
//            bis.ReleaseListeners(view);
//        }

//        base.ReleaseListeners(view);
//    }
//}






//            try
//            {
//                //////////////////////
//                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
//                {
//                    connection.Open();

//                    SqlCommand cmdProcedure = new SqlCommand("sp_3_Save_TransvaginalImagingStudy", connection);
//                    cmdProcedure.CommandType = CommandType.StoredProcedure;

//                    cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
//                    cmdProcedure.Parameters["@unitnum"].Value = unitnum;

//                    cmdProcedure.Parameters.Add("@instanceID", SqlDbType.Int);
//                    cmdProcedure.Parameters["@instanceID"].Value = instanceID;
//                    cmdProcedure.Parameters["@instanceID"].Direction = ParameterDirection.InputOutput;
                    
//                    /////////////////////////
//                    if (e.updatedMembers.Count > 0)
//                    {
//                        foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
//                        {
//                            string name = fi.Name;

//                            //if there is a specified member(s) then just add those, otherwise do all

//                            foreach (MemberInfo mi in e.updatedMembers)
//                            {
//                                if (name == mi.Name)
//                                {
//                                    SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
//                                    cmdProcedure.Parameters.Add("@" + name, sType);
//                                    cmdProcedure.Parameters["@" + name].Value = fi.GetValue(this);
//                                    break;
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
//                        {
//                            string name = fi.Name;

//                            foreach (MemberInfo mi in GetType().GetMembers())
//                            {
//                                //todo: optomize this double loop
//                                if (name == mi.Name && IsPersistable(mi))
//                                {
//                                    SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
//                                    cmdProcedure.Parameters.Add("@" + name, sType);
//                                    cmdProcedure.Parameters["@" + name].Value = fi.GetValue(this);
//                                    break;
//                                }
//                            }
//                        }
//                    }
 
//                    cmdProcedure.ExecuteNonQuery();
//                    instanceID = (int)cmdProcedure.Parameters["@instanceID"].Value;
//                    connection.Close();
//                } //end of using connection
//            }
//            catch (Exception exc)
//            {
//                Logger.Instance.WriteToLog("Persisting object - " + exc.ToString());
//            }
            
//        }
//    }
//}


using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Communication
{
    [DataContract]
    public class PtFollowup : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Task owningTask;

        /**************************************************************************************************/
        public int FollowupID;
        [DataMember] [HraAttribute]  protected DateTime FollowupDate=DateTime.Now;
        [DataMember] [HraAttribute]  protected string FollowupWho;
        [DataMember] [HraAttribute]  protected string FollowupType;
        [DataMember] [HraAttribute]  protected string FollowupDisposition;
        [DataMember] [HraAttribute]  protected string noApptReason;
        [DataMember] [HraAttribute]  protected string followupComment;
        [DataMember] [HraAttribute]  protected int taskID;

        /**************************************************************************************************/
        public DateTime Date
        {
            get
            {
                return FollowupDate;
            }
            set
            {
                if (value != FollowupDate)
                {
                    FollowupDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("FollowupDate"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Who
        {
            get
            {
                return FollowupWho;
            }
            set
            {
                if (value != FollowupWho)
                {
                    FollowupWho = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("FollowupWho"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string TypeOfFollowup
        {
            get
            {
                return FollowupType;
            }
            set
            {
                if (value != FollowupType)
                {
                    FollowupType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("FollowupType"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Disposition
        {
            get
            {
                return FollowupDisposition;
            }
            set
            {
                if (value != FollowupDisposition)
                {
                    FollowupDisposition = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("FollowupDisposition"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Reason
        {
            get
            {
                return noApptReason;
            }
            set
            {
                if (value != noApptReason)
                {
                    noApptReason = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("noApptReason"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Comment
        {
            get
            {
                return followupComment;
            }
            set
            {
                if (value != followupComment)
                {
                    followupComment = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("followupComment"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public int TaskID
        {
            get
            {
                return taskID;
            }
            set
            {
                if (value != taskID)
                {
                    taskID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("taskID"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        public PtFollowup() { }  // Default constructor for serialization

        public PtFollowup(Task parent)
        {
            owningTask = parent;
            FollowupWho = parent.Task_AssignedBy; 
        }

        /**************************************************************************************************/

        public string GetSummaryText()
        {
            return "";
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", owningTask.owningPatient.unitnum);
            pc.Add("apptid", owningTask.owningPatient.apptid);
            pc.Add("FollowupID", FollowupID, true);
            if (owningTask != null)
            {
                pc.Add("taskID", owningTask.taskID);
            }
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_PtFollowup",
                                      ref pc);

            this.FollowupID = (int)pc["FollowupID"].obj;
        }

    }
}



            //String unitnum =owningCommunicationHx.theProband.unitnum;
            //Console.WriteLine("unitnum=" + unitnum);


            //try
            //{
            //    //////////////////////
            //    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
            //    {
            //        connection.Open();

            //        SqlCommand cmdProcedure = new SqlCommand("sp_3_Save_PtFollowup", connection);
            //        cmdProcedure.CommandType = CommandType.StoredProcedure;


            //        BCDB2.AddUnitnumToCommand(owningCommunicationHx.theProband, cmdProcedure);

            //        cmdProcedure.Parameters.Add("@delete", SqlDbType.Bit);
            //        cmdProcedure.Parameters["@delete"].Value = delete;



            //        cmdProcedure.Parameters.Add("@FollowupID", SqlDbType.Int);
            //        cmdProcedure.Parameters["@FollowupID"].Value = FollowupID;
            //        cmdProcedure.Parameters["@FollowupID"].Direction = ParameterDirection.InputOutput;
   
            //        cmdProcedure.Parameters.Add("@FollowupDate", SqlDbType.DateTime);
            //        cmdProcedure.Parameters["@FollowupDate"].Value = FollowupDate;

            //        cmdProcedure.Parameters.Add("@FollowupWho", SqlDbType.NVarChar);
            //        cmdProcedure.Parameters["@FollowupWho"].Value = FollowupWho;

            //        cmdProcedure.Parameters.Add("@FollowupType", SqlDbType.NVarChar);
            //        cmdProcedure.Parameters["@FollowupType"].Value = FollowupType;

            //        cmdProcedure.Parameters.Add("@FollowupDisposition", SqlDbType.NVarChar);
            //        cmdProcedure.Parameters["@FollowupDisposition"].Value = FollowupDisposition;


            //        if (String.IsNullOrEmpty(noApptReason) == false)
            //        {
            //            cmdProcedure.Parameters.Add("@noApptReason", SqlDbType.NVarChar);
            //            cmdProcedure.Parameters["@noApptReason"].Value = noApptReason;
            //        }


            //        if (String.IsNullOrEmpty(followupComment) == false)
            //        {
            //            cmdProcedure.Parameters.Add("@followupComment", SqlDbType.NVarChar);
            //            cmdProcedure.Parameters["@followupComment"].Value = followupComment;
            //        }

                 


            //        //Console.WriteLine("********");
            //        //Console.WriteLine(cmdProcedure.CommandText);
            //        foreach (IDataParameter i in cmdProcedure.Parameters)
            //        {
            //            //Console.WriteLine(i.ParameterName + ": " + i.Value);
            //        }

            //        //Console.WriteLine("********");
            //        cmdProcedure.ExecuteNonQuery();


            //        FollowupID = Int32.Parse(cmdProcedure.Parameters["@FollowupID"].Value.ToString());
            //        //Console.WriteLine("FollowupID=" + FollowupID);
            //        connection.Close();
            //    } //end of using connection
            //}
            //catch (Exception exc)
            //{
            //    Logger.Instance.WriteToLog("Persisting object - " + exc.ToString());
            //    //Console.WriteLine("Persisting object - " + exc.ToString() + exc.StackTrace);
            //}

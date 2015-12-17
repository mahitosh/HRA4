using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    [CollectionDataContract]
    [KnownType(typeof(PediatricRule))]
    public class PediatricConsiderations : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();

        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public PediatricConsiderations() { }  // Default constructor for serialization

        public PediatricConsiderations(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadPediatricConsiderations",
                                                pc,
                                                typeof(PediatricRule),
                                                constructor_args);
            
           //Azure was timing out on this for no obvious reason 
            //DoListLoad(lla);

        }
    }

    [DataContract]
    public class PediatricRule : HraObject
    {
        [DataMember] [HraAttribute] private string  RuleID;
        [DataMember] [HraAttribute] private string  Syndrome;
        [DataMember] [HraAttribute] private string  Consideration;
        [DataMember] [HraAttribute] private string  Action;
        [DataMember] [HraAttribute] private string  Reason;
        [DataMember] [HraAttribute] private double  Rank;
        [DataMember] [HraAttribute] private DateTime  created;

        public PediatricRule() { }  // Default constructor for serialization

        /*****************************************************/
        public string PediatricRule_RuleID
        {
            get
            {
                return RuleID;
            }
            set
            {
                if (value != RuleID)
                {
                    RuleID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RuleID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PediatricRule_Syndrome
        {
            get
            {
                return Syndrome;
            }
            set
            {
                if (value != Syndrome)
                {
                    Syndrome = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Syndrome"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PediatricRule_Consideration
        {
            get
            {
                return Consideration;
            }
            set
            {
                if (value != Consideration)
                {
                    Consideration = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Consideration"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PediatricRule_Action
        {
            get
            {
                return Action;
            }
            set
            {
                if (value != Action)
                {
                    Action = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Action"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PediatricRule_Reason
        {
            get
            {
                return Reason;
            }
            set
            {
                if (value != Reason)
                {
                    Reason = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Reason"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double PediatricRule_Rank
        {
            get
            {
                return Rank;
            }
            set
            {
                if (value != Rank)
                {
                    Rank = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Rank"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public DateTime PediatricRule_created
        {
            get
            {
                return created;
            }
            set
            {
                if (value != created)
                {
                    created = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("created"));
                    SignalModelChanged(args);
                }
            }
        } 


    }
}

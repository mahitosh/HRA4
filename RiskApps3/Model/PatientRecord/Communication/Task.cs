using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord.Communication
{
    [DataContract]
    public class Task : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Patient owningPatient;

        /**************************************************************************************************/
        [DataMember] public int taskID = 0;

        [DataMember] public PtFollowupList FollowUps;

        #region Hra Attributes
        [DataMember] [HraAttribute] 
        protected string type = "New Task";
        [DataMember] [HraAttribute] 
        protected string action;
        [DataMember] [HraAttribute] 
        protected string assignedBy;
        [DataMember] [HraAttribute] 
        protected string assignedTo;
        [DataMember] [HraAttribute] 
        protected DateTime duedate;
        [DataMember] [HraAttribute] 
        protected string status;
        [DataMember] [HraAttribute] 
        protected string text;
        [DataMember] [HraAttribute] 
        protected DateTime taskDate;
        #endregion

        #region gets/sets
        /**************************************************************************************************/
        public string Task_Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("text"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        /**************************************************************************************************/
        public string Task_Status
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
        /**************************************************************************************************/
        public DateTime Task_Duedate
        {
            get
            {
                return duedate;
            }
            set
            {
                if (value != duedate)
                {
                    duedate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("duedate"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Task_AssignedTo
        {
            get
            {
                return assignedTo;
            }
            set
            {
                if (value != assignedTo)
                {
                    assignedTo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("assignedTo"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Task_AssignedBy
        {
            get
            {
                return assignedBy;
            }
            set
            {
                if (value != assignedBy)
                {
                    assignedBy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("assignedBy"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Task_Action
        {
            get
            {
                return action;
            }
            set
            {
                if (value != action)
                {
                    action = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("action"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public string Task_Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("type"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public DateTime Task_Date
        {
            get
            {
                return taskDate;
            }
            set
            {
                if (value != taskDate)
                {
                    taskDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("taskDate"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

        /**************************************************************************************************/
        public Task() { }  // Default constructor for serialization

        public Task(Patient proband)
        {
            owningPatient = proband;
            FollowUps = new PtFollowupList(this);
        }
        /**************************************************************************************************/
        public Task(Patient proband, string task_type, string task_status, string task_assignedBy, DateTime date)
            : this(proband)
        {
            type = task_type;
            status = task_status;
            assignedBy = task_assignedBy;
            taskDate = date;
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", owningPatient.unitnum);
            pc.Add("taskID", taskID, true);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Task",
                                      ref pc);

            this.taskID = (int)pc["taskID"].obj;
        }


        public static int CompareTasksByReverseDate(HraObject x, HraObject y)
        {
            if (x is Task && y is Task)
            {
                return DateTime.Compare(((Task)y).Task_Date, ((Task)x).Task_Date);
            }
            else return 0;
        }

        public override string ToString()
        {
            return this.action + " (" + this.taskDate.ToShortDateString() + ")";
        }
    }
}


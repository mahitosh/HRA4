using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord
{
    public class Order : HraObject
    {
        /**************************************************************************************************/
        public string unitnum;
        public int instanceID;
        [HraAttribute] public int orderApptID;
        [HraAttribute] public int orderID;
        [HraAttribute] public string orderDesc;
        [HraAttribute] public DateTime? orderDate;
        [HraAttribute] public string status;
        [HraAttribute] public string orderGrouping;
        [HraAttribute] public string why;
        [HraAttribute] public string apptphysname;
        [HraAttribute] public int finalized;

        /*****************************************************/
        public string Order_unitnum
        {
            get
            {
                return unitnum;
            }
            set
            {
                if (value != unitnum)
                {
                    unitnum = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("unitnum"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Order_instanceID
        {
            get
            {
                return instanceID;
            }
            set
            {
                if (value != instanceID)
                {
                    instanceID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("instanceID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Order_orderApptID
        {
            get
            {
                return orderApptID;
            }
            set
            {
                if (value != orderApptID)
                {
                    orderApptID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("orderApptID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int Order_orderID
        {
            get
            {
                return orderID;
            }
            set
            {
                if (value != orderID)
                {
                    orderID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("orderID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Order_orderDesc
        {
            get
            {
                return orderDesc;
            }
            set
            {
                if (value != orderDesc)
                {
                    orderDesc = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("orderDesc"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public DateTime Order_orderDate
        {
            get
            {
                return (DateTime)orderDate;
            }
            set
            {
                if (value != orderDate)
                {
                    orderDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("orderDate"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Order_status
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
        public string Order_orderGrouping
        {
            get
            {
                return orderGrouping;
            }
            set
            {
                if (value != orderGrouping)
                {
                    orderGrouping = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("orderGrouping"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Order_why
        {
            get
            {
                return why;
            }
            set
            {
                if (value != why)
                {
                    why = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("why"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string Order_apptphysname
        {
            get
            {
                return apptphysname;
            }
            set
            {
                if (value != apptphysname)
                {
                    apptphysname = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("apptphysname"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public int Order_finalized
        {
            get
            {
                return finalized;
            }
            set
            {
                if (value != finalized)
                {
                    finalized = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("finalized"));
                    SignalModelChanged(args);
                }
            }
        }

        /**************************************************************************************************/
        public Order()
        {
            unitnum = SessionManager.Instance.GetActivePatient().unitnum;
            orderDate = DateTime.Today;
            instanceID = 0;
            status = "Pending";
        }

        /**************************************************************************************************/
        public Order(string unitnum, int instanceID, int orderApptID, int orderID, string orderDesc,
                        DateTime? orderDate,string status, string orderGrouping, string why, int finalized)
        {
            this.unitnum = unitnum;
            this.instanceID = instanceID;
            this.orderApptID = orderApptID;
            this.orderID = orderID;
            this.orderDesc = orderDesc;
            this.orderDate = orderDate;
            this.status = status;
            this.orderGrouping = orderGrouping;
            this.why = why;
            this.finalized = finalized;
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("instanceID", instanceID, true);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Order",
                                      ref pc);

            this.instanceID = (int)pc["instanceID"].obj;
        }
    }
}

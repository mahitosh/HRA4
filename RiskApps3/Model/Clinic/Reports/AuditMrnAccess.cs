using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class AuditMrnAccessV2 : HRAList  //Silicus:Marked class as public
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string unitnum;
    
        private object[] construction_args;
        private ParameterCollection pc;

        public AuditMrnAccessV2()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void  BackgroundListLoad()
        {
            pc.Clear();
            
            pc.Add("startDate", StartTime.ToShortDateString());
            pc.Add("endDate", EndTime.ToShortDateString());
            pc.Add("unitnum", unitnum);

            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_GetAuditUnitnumAccessV2",
                this.pc,
                typeof(AuditMrnAccessV2Entry),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }
    public class AuditMrnAccessV2Entry : HraObject
    {
            [HraAttribute] public DateTime? startTime;
            [HraAttribute] public DateTime? endTime;
            [HraAttribute] public string createdBy;
            [HraAttribute] public string machineName; 
            [HraAttribute] public string unitnum;
            [HraAttribute] public int apptid;
            [HraAttribute] public string message;
            [HraAttribute] public string application;
            [HraAttribute] public string table;
            [HraAttribute] public string field;
            [HraAttribute] public string fieldMeaning;
            [HraAttribute] public string oldValue;
            [HraAttribute] public string newValue;
    }

    public class AuditMrnAccessV3 : HRAList  //Silicus:Marked class as public
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public string unitnum;
    
        private object[] construction_args;
        private ParameterCollection pc;

        public AuditMrnAccessV3()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            try
            {
                pc.Clear();

                pc.Add("startDate", StartTime.ToShortDateString());
                pc.Add("endDate", EndTime.ToShortDateString());
                pc.Add("unitnum", unitnum);

                LoadListArgs lla = new LoadListArgs
                (
                    "sp_3_GetAuditUnitnumAccessV3",
                    this.pc,
                    typeof(AuditMrnAccessV3Entry),
                    this.construction_args
                );

                DoListLoad(lla);
            }
            catch (Exception e)
            {
            }
        }
    }

    public class AuditMrnAccessV3Entry : HraObject
    {
            [HraAttribute] public DateTime? timestamp;
            [HraAttribute] public string userName;
            [HraAttribute] public string unitnum;
            [HraAttribute] public int apptID;
            [HraAttribute] public string storedProcedure;
            [HraAttribute] public string hraAttributeValueList;
            [HraAttribute] public int rowsAffected;
            [HraAttribute] public int relativeID;
            [HraAttribute] public string hraObject;
    }

    class MrnList : HRAList
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public MrnList()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_GetUnitnumList",
                this.pc,
                typeof(MrnListEntry),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class MrnListEntry : HraObject
    {
        [HraAttribute] public string unitnum;
    }
}

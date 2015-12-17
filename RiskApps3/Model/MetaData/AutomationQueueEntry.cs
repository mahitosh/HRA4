using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.SqlClient;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.MetaData
{
    [DataContract]
    class AutomationQueueEntry  : HraObject
    {
        [DataMember] [HraAttribute] public int apptid;
        [DataMember] [HraAttribute] public string patientName;
        [DataMember] [HraAttribute] public string unitnum;
        [DataMember] [HraAttribute] public string dob;
        [DataMember] [HraAttribute] public DateTime apptdatetime;
        [DataMember] [HraAttribute] public DateTime riskdatacompleted;

        public AutomationQueueEntry()
        {
        }
        public AutomationQueueEntry(int apptid, string patientname, string unitnum, string dob, DateTime apptdatetime, DateTime riskdatacompleted)
        {
            this.apptid = apptid;
            this.patientName = patientname;
            this.unitnum = unitnum;
            this.dob = dob;
            this.apptdatetime = apptdatetime;
            this.riskdatacompleted = riskdatacompleted;
        }
    }
}

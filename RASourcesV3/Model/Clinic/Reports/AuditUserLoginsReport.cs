using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    class AuditUserLoginsReport : HRAList<AuditUserLoginsReportEntry>
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public AuditUserLoginsReport()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("startDate", StartTime.ToShortDateString());
            pc.Add("endDate", EndTime.ToShortDateString());
            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_GetAuditUserLoginsReport",
                this.pc,
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class AuditUserLoginsReportEntry : HraObject
    {
        [HraAttribute]
        public DateTime timestamp;
        [HraAttribute]
        public string userLogin;
        [HraAttribute]
        public string application;
    }
}

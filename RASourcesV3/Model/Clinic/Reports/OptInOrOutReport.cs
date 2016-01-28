using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class OptInOrOutReport : HRAList<OptInOrOutReportEntry>
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public OptInOrOutReport()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("start", StartTime.ToShortDateString());
            pc.Add("end", EndTime.ToShortDateString());
            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_GetOptInOrOutReport",
                this.pc,
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class OptInOrOutReportEntry : HraObject
    {
        [HraAttribute] public int 	apptid;
        [HraAttribute] public DateTime 	apptdatetime;
        [HraAttribute] public string 	unitnum;
        [HraAttribute] public string 	patientname;
        [HraAttribute] public string 	dob;
        [HraAttribute] public string optInOrOut;

    }
}

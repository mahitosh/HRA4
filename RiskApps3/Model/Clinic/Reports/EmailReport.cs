using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class EmailReport : HRAList
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public EmailReport()
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
                "sp_3_GetEmailReport",
                this.pc,
                typeof(EmailReportEntry),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class EmailReportEntry : HraObject
    {
        [HraAttribute] public int 	apptid;
        [HraAttribute] public DateTime 	apptdatetime;
        [HraAttribute] public string 	unitnum;
        [HraAttribute] public string 	patientname;
        [HraAttribute] public string 	dob;
        [HraAttribute] public string emailAddress;

    }
}

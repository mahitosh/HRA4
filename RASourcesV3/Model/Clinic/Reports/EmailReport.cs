using System;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class EmailReport : HRAList<EmailReportEntry>
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
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class EmailReportEntry : HraObject
    {
        [Hra] public int 	apptid;
        [Hra] public DateTime 	apptdatetime;
        [Hra] public string 	unitnum;
        [Hra] public string 	patientname;
        [Hra] public string 	dob;
        [Hra] public string emailAddress;

    }
}

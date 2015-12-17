using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    class RiskClinicEthnicityCohort : HRAList
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public RiskClinicEthnicityCohort()
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
                "sp_3_GetRiskClinicEthnicity",
                this.pc,
                typeof(EthnicityCohortMember),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class EthnicityCohortMember : HraObject
    {
        [HraAttribute] public int 	apptid;
        [HraAttribute] public DateTime 	apptdatetime;
        [HraAttribute] public string 	unitnum = "";
        [HraAttribute] public string 	patientname = "";
        [HraAttribute] public string 	racialBackground = "";
        [HraAttribute] public string isHispanic = "";
        [HraAttribute] public string isAshkenazi = "";

    }
}

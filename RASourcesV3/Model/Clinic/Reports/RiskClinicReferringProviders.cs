
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class RiskClinicReferringProviders : HRAList<ReferringProvider>
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public RiskClinicReferringProviders()
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
                "sp_3_GetRiskClinicReferringProviders",
                this.pc,
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class ReferringProvider : HraObject
    {
        [HraAttribute] public string 	fullName;
        [HraAttribute] public string 	institution;
        [HraAttribute] public int 	freq;
    }
}

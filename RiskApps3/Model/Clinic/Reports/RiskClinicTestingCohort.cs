using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class RiskClinicTestingCohort : HRAList
    {        
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;

        public RiskClinicTestingCohort()
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
                "sp_3_GetRiskClinicTestingCohort",
                this.pc,
                typeof(TestingCohorttMember),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class TestingCohorttMember : HraObject
    {
        [HraAttribute] public string 	unitnum;
        [HraAttribute] public string 	panelName;
        [HraAttribute] public DateTime 	VisitDate;
        [HraAttribute] public int 	    VariantFound;
        [HraAttribute] public int 	    IsBrcaPanel;
        [HraAttribute] public int       IsLynchPanel;
        [HraAttribute] public int    	IsCompleted;
    }
}

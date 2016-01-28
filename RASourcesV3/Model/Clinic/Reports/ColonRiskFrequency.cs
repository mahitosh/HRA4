using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class ColonRiskFrequency : HRAList<ColonRiskFrequency.ColonRiskStat>
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public ColonRiskFrequency()
        {
            construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("clinicId", clinicID);
            pc.Add("start", StartTime.ToShortDateString());
            pc.Add("end", EndTime.ToShortDateString());
            pc.Add("type", type);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadLifetimeLynchCancerRiskStats",
                                    pc,
                                    construction_args);
            DoListLoad(lla);
        }

        public class ColonRiskStat : HraObject
        {
            [HraAttribute] public int apptid;

            [HraAttribute] public int MMRPRO_Lifetime_Endo;
            [HraAttribute] public int MMRPRO_Lifetime_Colon;
            [HraAttribute] public int CCRAT;
        }
    }
}

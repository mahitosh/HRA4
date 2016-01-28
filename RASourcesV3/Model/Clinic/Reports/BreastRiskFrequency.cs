using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class BreastRiskFrequency : HRAList<BreastRiskFrequency.BreastRiskStat>
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public BreastRiskFrequency()
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

            LoadListArgs lla = new LoadListArgs("sp_3_LoadLifetimeBreastRiskStats",
                                    pc,
                                    construction_args);
            DoListLoad(lla);
        }

        public class BreastRiskStat : HraObject
        {
            [HraAttribute] public int BreastCancer;

            [HraAttribute] public int BRCAPRO;
            [HraAttribute] public int Claus;
            [HraAttribute] public int TC6;
            [HraAttribute] public int TC7;

            [HraAttribute] public int BRCAPRO_passed;
            [HraAttribute] public int Claus_passed;
            [HraAttribute] public int TC6_passed;
            [HraAttribute] public int TC7_passed;
        }
    }
}
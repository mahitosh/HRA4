using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class MutationRiskFrequency : HRAList
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public MutationRiskFrequency()
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

            LoadListArgs lla = new LoadListArgs("sp_3_LoadMutationRiskStats",
                                    pc,
                                    typeof(MutationRisk),
                                    construction_args);
            DoListLoad(lla);
        }

        public class MutationRisk : HraObject
        {

            [HraAttribute] public int BRCAPRO;
            [HraAttribute] public int Myriad;
            [HraAttribute] public int TC6;
            [HraAttribute] public int TC7;

            [HraAttribute] public int BRCAPRO_passed;
            [HraAttribute] public int  Myriad_passed;
            [HraAttribute] public int TC6_passed;
            [HraAttribute] public int TC7_passed;
        }
    }
}
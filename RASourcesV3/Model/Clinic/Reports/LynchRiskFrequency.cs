using System;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class LynchRiskFrequency : HRAList<LynchRiskFrequency.LynchMutationRisk>
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public LynchRiskFrequency()
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

            LoadListArgs lla = new LoadListArgs("sp_3_LoadLynchMutationRiskStats",
                                    pc,
                                    construction_args);
            DoListLoad(lla);
        }

        public class LynchMutationRisk : HraObject
        {

            [Hra] public int MMRPro;
            [Hra] public int PREMM;
            [Hra] public int MMRPro_passed;
            [Hra] public int PREMM_passed;

        }
    }
}
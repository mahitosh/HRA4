using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class EthnicityFrequency : HRAList<EthnicityFrequency.EthnicityStat>
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public EthnicityFrequency()
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

            LoadListArgs lla = new LoadListArgs("sp_3_LoadEthnicityStats",
                                    pc,
                                    construction_args);
            DoListLoad(lla);
        }

        public class EthnicityStat : HraObject
        {
            [HraAttribute] public string racialBackground;
            [HraAttribute] public int frequency;
        }
    }
}

using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class AppointmentHistoryStats : HRAList
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public AppointmentHistoryStats()
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
            pc.Add("monthly", 1);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadAppointmentHistoryStats",
                                    pc,
                                    typeof(AppointmentStat),
                                    construction_args);
            DoListLoad(lla);
        }

        public class AppointmentStat : HraObject
        {
            [HraAttribute] public int apptCount;
            [HraAttribute] public DateTime StartOfMonth;
            [HraAttribute] public string clinicName;
        } 
    }
        
    public class AppointmentHistorySummaryStats : HRAList
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;
        public string type = "";

        public AppointmentHistorySummaryStats()
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
            pc.Add("monthly", 0);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadAppointmentHistoryStats",
                                    pc,
                                    typeof(AppointmentSummaryStat),
                                    construction_args);
            DoListLoad(lla);
        }

        public class AppointmentSummaryStat : HraObject
        {
            [HraAttribute] public int unique_patients;
            [HraAttribute] public int total_appts;
            [HraAttribute] public string clinicName;
        }
    } 
}


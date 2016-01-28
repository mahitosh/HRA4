using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;

namespace RiskApps3.Utilities
{
    public class LegacyCoreAPI
    {
        public static void StartWorkingWithAppointment(int apptid, string config_path)
        {
            StartWorkinigWithAppointment(apptid, config_path);
        }


        public static void StartWorkinigWithAppointment(int apptid, string config_path)
        {
            if (!string.IsNullOrEmpty(config_path))
            {
                RiskApps3.Utilities.Configurator.configFilePath = config_path;
            }
            //RiskApps3.Utilities.Logger.Instance.SetFilePath(Path.Combine(appRoot, "log.txt"));
            Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
            pc.Add("apptID", apptid);
            object returnCode = BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_3_Start_Working_With_Appt", SqlDbType.Int, pc);
            //Logger.Instance.WriteToLog("NEW: Started working with appointment!!  Appt id = " + apptid + ", return value from new SP = " + (int)returnCode);
        }



        public static void stopWorkingWithAppointment(int apptid)
        {
            Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
            pc.Add("apptID", apptid);
            object returnCode = BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_markRiskDataCompleted", SqlDbType.Int, pc);
        }


        public static bool isMDY()
        {
            String datePattern = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            datePattern = datePattern.ToUpper();
            if (datePattern.IndexOf("D") < datePattern.IndexOf("M"))
            {
                return false;
            }

            return true;
        }


        public static String getShortDateFormatString()
        {
            if (isMDY())
            {
                return "MM/dd/yyyy";
            }
            return "dd/MM/yyyy";
        }
    }
}

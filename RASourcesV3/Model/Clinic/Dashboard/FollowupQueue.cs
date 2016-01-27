using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class FollowupQueue : Queue
    {
        public DateTime startTime;
        public DateTime endTime;
        public int clinicId = -1;
        public string type = "";
        public RiskApps3.View.Reporting.RiskElementControl.Mode mode = RiskApps3.View.Reporting.RiskElementControl.Mode.Incidence;
        
        public override void BackgroundLoadWork()
        {
            int output;
            if (mode == RiskApps3.View.Reporting.RiskElementControl.Mode.Incidence)
                output = 2;
            else
                output = 3;


            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            pc.Add("startTime", startTime);
            pc.Add("endTime", endTime);
            pc.Add("type", type);
            pc.Add("output", output);

            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadBreastImagingDashboardElement", pc);
            dt = new DataTable(); 
            dt.Load(dr);
        }
    }
}

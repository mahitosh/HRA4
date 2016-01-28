using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class BreastImagingModelElement : HraObject
    {

        public DateTime startTime;
        public DateTime endTime;
        public int clinicId = -1;
        public string type = "";
        public int output = 1;

        [HraAttribute] public int incidence;
        [HraAttribute] public int prevelance;
        [HraAttribute] public int denominator;
        [HraAttribute] public int seenInRC;
        [HraAttribute] public string info = "";

        public BreastImagingModelElement()
        {

        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            pc.Add("startTime", startTime);
            pc.Add("endTime", endTime);
            pc.Add("type", type);
            pc.Add("output", output);
            DoLoadWithSpAndParams("sp_3_LoadBreastImagingDashboardElement", pc);
            pc.Clear();
            pc.Add("type", type);
            DoLoadWithSpAndParams("sp_3_LoadFilterDescription", pc);
        }
    }
}

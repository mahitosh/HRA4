using RiskApps3.View.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Reports
{
    public class ReportElement : HraObject
    {
        public DateTime StartTime = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime EndTime = new DateTime(DateTime.Now.Year, 12, 31);
        public int clinicId = -1;
        public string DisplayName = "";

        public ReportElement()
        {

        }

        public virtual string ToHTML()
        {
            return "";
        }
    }
}

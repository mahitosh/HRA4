using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord
{
    public class GoldenAppointment
    {
        private static string query = "select dbo.fn_3_GetGoldenAppt ('{0}')";

        public string MRN { get; set; }

        public long? apptid;

        public GoldenAppointment() 
        {
            this.MRN = "";
        }

        public void Load()
        {
            string query = string.Format(GoldenAppointment.query, this.MRN);
            object result = BCDB2.Instance.ExecuteScalarQuery(query);
            if(DBNull.Value == result)
            {
                this.apptid = null;
            } 
            else 
            {
                this.apptid = Convert.ToInt64(result);
            }
        }
    }
}

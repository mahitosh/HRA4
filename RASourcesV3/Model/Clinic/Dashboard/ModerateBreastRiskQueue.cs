using RiskApps3.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class ModerateBreastRiskQueue : Queue
    {
        public int clinicId = -1;

        public ModerateBreastRiskQueue()
        {
            QueueText = "High Risk BRCA Patients";
            QueueName = "High Risk BRCA";
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue where BreastHRQ = 1");
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadModerateBreastRiskQueue", pc);
            dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

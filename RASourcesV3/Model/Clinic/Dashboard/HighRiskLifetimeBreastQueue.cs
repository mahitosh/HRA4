using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RiskApps3.Utilities;
using System.Data.SqlClient;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class HighRiskLifetimeBreastQueue : Queue
    {
        public int clinicId = -1;

        public HighRiskLifetimeBreastQueue()
        {
            QueueText = "Patients at an Elevated Lifetime Risk of Breast Cancer";
            QueueName = "Lifetime Breast Risk = 20%";
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //fill the local datatable object
            //dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue where MaxLifetimeScore > 20.0");
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadHighRiskLifetimeBreastQueue", pc);
            dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

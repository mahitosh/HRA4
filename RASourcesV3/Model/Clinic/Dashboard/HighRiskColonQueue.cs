using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.Data;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class HighRiskColonQueue : Queue
    {
        public int clinicId = -1;

        public HighRiskColonQueue()
        {
            QueueText = "Patients at High Risk for Lynch Syndrome";
            QueueName = "High Risk Lynch";
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //fill the local datatable object
            //dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue where ColonHRQ = 1");
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadHighRiskLynchQueue", pc);
            if (dr != null)
                dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

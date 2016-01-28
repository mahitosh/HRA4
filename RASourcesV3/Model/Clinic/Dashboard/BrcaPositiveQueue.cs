using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RiskApps3.Utilities;
using System.Data.SqlClient;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class BrcaPositiveQueue : Queue
    {
        public int clinicId = -1;

        public BrcaPositiveQueue()
        {
            QueueText = "Families with BRCA Mutations";
            QueueName = "Brca Pos Families";
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //fill the local datatable object
            //dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue where NumTestedFamilyMembers > 0");
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadBrcaPositiveQueue", pc);
            dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

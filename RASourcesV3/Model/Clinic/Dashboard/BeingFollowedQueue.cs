using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RiskApps3.Utilities;
using System.Data.SqlClient;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class BeingFollowedQueue : Queue
    {
        public int clinicId = -1;

        public BeingFollowedQueue()
        {
            QueueText = "Patients Being Followed";
            QueueName = "Patients Followed";
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //fill the local datatable object
            //dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue where BeingFollowedInRiskClinic = 1");
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadBeingFollowedQueue", pc);
            dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

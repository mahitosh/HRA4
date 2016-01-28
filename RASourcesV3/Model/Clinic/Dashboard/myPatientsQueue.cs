using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RiskApps3.Utilities;
using System.Data.SqlClient;

namespace RiskApps3.Model.Clinic.Dashboard
{
    class myPatientsQueue : Queue
    {
        public int clinicId = -1;

        public myPatientsQueue()
        {
            QueueText = "My Patients";
            QueueName = "My Patients";
        }
        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            dt = new DataTable();
            SqlDataReader dr = BCDB2.Instance.ExecuteReaderSPWithParams("sp_3_LoadMyPatientsQueue", pc);
            dt.Load(dr);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

    }
}

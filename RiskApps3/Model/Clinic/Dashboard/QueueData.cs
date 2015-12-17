using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.View;
using RiskApps3.Utilities;
using System.Data;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class QueueData : HraObject
    {
        public DateTime LastQueueUpdate;
        public int? forceUpdate;
        public DataTable dt = null;  //only one of these needed for all Qs

        /**************************************************************************************************/
        public QueueData()
        {
            //forceUpdate = false;
            //DoLoadWithViewAndPatient("v_3_LastQueueUpdate", null);
        }
        public static void UpdateBigQueueByMrn(string mrn)
        {
            if (string.IsNullOrEmpty(mrn) == false)
            {
                ParameterCollection pc = new ParameterCollection("unitnum", mrn);
                BCDB2.Instance.RunSPWithParams("sp_3_populateBigQueue", pc);
            }
        }
        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //(optionally) update the big queue table in the database
            ParameterCollection pc = new ParameterCollection();
            if (forceUpdate != null)
            {
                pc.Add("forceUpdate", forceUpdate);
            }
            DoLoadWithSpAndParams("sp_3_UpdateQueueDataWhenOld", pc);
            forceUpdate = null;

            //fill the local datatable object
            dt = BCDB2.Instance.getDataTable(@"SELECT * from tblBigQueue");
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }
    }
}

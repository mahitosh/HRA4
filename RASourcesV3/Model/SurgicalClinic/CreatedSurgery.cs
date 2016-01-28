using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Controllers;
using System.Data.SqlClient;
using RiskApps3.Utilities;

namespace RiskApps3.Model.SurgicalClinic
{
    public class CreatedSurgery : HraObject
    {
        public long createdSurgeryID;

        //public override void BackgroundLoadWork()
        //{
        //    //probably not worth a dedicated stored procedure?
        //    string sql = "SELECT createdSurgeryID FROM tblSurgicalClinic WHERE apptID = {0}";
        //    sql = string.Format(sql, SessionManager.Instance.GetActivePatient().apptid);

        //    using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(sql))
        //    {
        //        if (reader != null)
        //        {
        //            if (reader.Read())
        //            {
        //                this.createdSurgeryID = reader.IsDBNull(0) ? -1 : reader.GetInt64(0);
        //            } 
        //            else 
        //            {
        //                this.createdSurgeryID = -1;
        //            }
        //        }
        //    }
        //    base.BackgroundLoadWork();
        //}
    }
}

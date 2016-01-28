using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Reflection;
using RiskApps3.Utilities;
using RiskApps3.View;
using System.Data;
using RiskApps3.Model.PatientRecord;
using System.Threading;

namespace RiskApps3.Model.Clinic.Followup
{
    public class FollowupStatus : HraObject
    {/**************************************************************************************************/

        /**************************************************************************************************/

        public FollowupStatus(string mrn)
        {
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
           
        }
            

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }
}
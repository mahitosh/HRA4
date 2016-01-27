using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.View;

namespace RiskApps3.Model.Clinic.Dashboard
{
    class PendingTasks : HraObject
    {
        public int NumPendingTasks;
        public int NumAssignedToYou;
        public int clinicId = -1;

        /**************************************************************************************************/

        public void ReleaseListeners(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("user",SessionManager.Instance.ActiveUser.ToString());
            pc.Add("clinicId", clinicId);
            DoLoadWithSpAndParams("sp_3_GetPendingTaskCounts", pc);

        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }
}
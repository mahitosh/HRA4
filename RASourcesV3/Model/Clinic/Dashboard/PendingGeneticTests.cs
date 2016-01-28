using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using RiskApps3.View;


namespace RiskApps3.Model.Clinic.Dashboard
{
    public class PendingGeneticTests : HraObject
    {
        public int NumPendingGeneticTests;
        //public int assignedToYou;

        public int clinicId = -1;

        /**************************************************************************************************/

        public void ReleaseListeners(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("user", SessionManager.Instance.ActiveUser.ToString());
            pc.Add("clinicId", clinicId);
            DoLoadWithSpAndParams("sp_3_GetPendingGeneticTestCounts", pc);

        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }

    }
}

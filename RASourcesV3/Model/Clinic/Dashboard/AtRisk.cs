using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.View;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class AtRisk : HraObject
    {
        public int clinicId = -1;
        /**************************************************************************************************/
        [HraAttribute]
        public int NumAtRiskBreast;
        [HraAttribute]
        public int NumAtRiskColon;
        [HraAttribute]
        public int NumMaxLifetimeBreastGE20;
        [HraAttribute]
        public int NumBrcaPosFamilies;
        [HraAttribute]
        public int NumBrcaPosRelativesTestable;
        [HraAttribute]
        public int NumPatientsFollowed;
        [HraAttribute]
        public int PrintQueueCount;
        [HraAttribute]
        public int NumPatients;

        public string ProviderName = "";

        /**************************************************************************************************/

        public AtRisk()
        {
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            DoLoadWithSpAndParams("sp_3_LoadAtRisk", pc);
            DoLoadWithSpAndParams("sp_3_LoadDashboardCounts", pc);

        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }
}
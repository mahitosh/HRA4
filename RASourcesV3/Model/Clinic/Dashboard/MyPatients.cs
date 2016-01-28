using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.View;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.Clinic.Dashboard
{
    public class MyPatients : HraObject
    {
        public int clinicId = -1;
        /**************************************************************************************************/
        [HraAttribute]
        public int NumPatientsToday;
        [HraAttribute]
        public int NumNewPatientsToday;
        [HraAttribute]
        public int NumFollowUpPtsToday;
        [HraAttribute]
        public int NumPatientsTotal;

        public string groupName = "Entire Clinic";
        public string date;

        /**************************************************************************************************/

        public MyPatients()
        {
        }

        /**************************************************************************************************/

        public void ReleaseListeners(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("clinicId", clinicId);
            pc.Add("when", date);
            
            //pc.Add("groupName", groupName);
            //pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);
            

            DoLoadWithSpAndParams("sp_3_GetMyPatientCounts", pc);

        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {

        }
    }
}
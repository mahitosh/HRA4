using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Reports
{
    public class RiskClinicPatientCohort : HRAList
    { 
        private object[] construction_args;
        private ParameterCollection pc;

        public DateTime StartTime;
        public DateTime EndTime;
        public int clinicID = -1;

        public RiskClinicPatientCohort()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("clinicId", clinicID);
            pc.Add("start", StartTime.ToShortDateString());
            pc.Add("end", EndTime.ToShortDateString());
            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_GetRiskClinicPatientCohort",
                this.pc,
                typeof(PatientCohortMember),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }

    public class PatientCohortMember : HraObject
    {
        [HraAttribute] public int 	apptid;
        [HraAttribute] public DateTime 	apptdatetime;
        [HraAttribute] public string 	unitnum;
        [HraAttribute] public string 	patientname;
        [HraAttribute] public int 	RiskAssessment;
        [HraAttribute] public int Followup;
        [HraAttribute] public int CancerPatient;
        [HraAttribute] public int 	RelatedAppts;

    }
}

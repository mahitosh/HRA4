using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Utilities;
using RiskApps3.Controllers;
using RAM = RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
namespace HRA4.Utilities
{
    public class HraSessionManager
    {
        public HraSessionManager(string InstitutionId,string config)
        {            
            Cache.SetCache<string>(InstitutionId, config);         
        }

        public HraSessionManager(string InstitutionId, string config,string mrn,int apptId): this(InstitutionId,config)
        {
            SetActivePatient(mrn, apptId);
        }

        public HraSessionManager(string InstitutionId, string config, string user, string mrn, int apptId)
            : this(InstitutionId, config,mrn,apptId)
        {
            SetRaActiveUser(user);
        }

        public void SetRaActiveUser(string user)
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            var users = SessionManager.Instance.MetaData.Users;// We can cache users.
            SessionManager.Instance.ActiveUser = users.GetUser(user);
        }

        public void SetActivePatient(string mrn,int apptId)
        {
            SessionManager.Instance.SetActivePatient(mrn, apptId);
        }
        public Patient GetActivePatient()
        {
            return SessionManager.Instance.GetActivePatient();
        }



    }
}

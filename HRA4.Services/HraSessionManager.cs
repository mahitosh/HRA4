using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Utilities;
using RiskApps3.Controllers;
using RAM = RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord;
using System.Web;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
namespace HRA4.Services
{
    public class HraSessionManager : Interfaces.IHraSessionManager
    {
        IRepositoryFactory _repositoryFactory;
        public string Username { get; private set; }
        public string InstitutionId
        {
            get;
            private set;
        }
        public HraSessionManager(IRepositoryFactory repositoryFactory, string user)
        {
            _repositoryFactory = repositoryFactory;
            Username = user;
            SetUserSession(user);

        }
        public HraSessionManager(string InstitutionId, string config)
        {
            this.InstitutionId = InstitutionId;
            Cache.SetCache<string>(InstitutionId, config);
        }

        public HraSessionManager(string InstitutionId, string config, string mrn, int apptId)
            : this(InstitutionId, config)
        {
            SetActivePatient(mrn, apptId);
        }

        public HraSessionManager(string InstitutionId, string config, string user, string mrn, int apptId)
            : this(InstitutionId, config, mrn, apptId)
        {
            SetRaActiveUser(user);
        }

        public void SetRaActiveUser(string user)
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            var users = SessionManager.Instance.MetaData.Users;// We can cache users.
            SessionManager.Instance.ActiveUser = users.GetUser(user);
            RiskApps3.Model.HraObject.AuditUserLogin(user);
        }

        public void SetConfig(string InstitutionId, string config)
        {
            this.InstitutionId = InstitutionId;
            Cache.SetCache<string>(InstitutionId, config);
        }

        public void SetActivePatient(string mrn, int apptId)
        {
            SessionManager.Instance.SetActivePatient(mrn, apptId);
        }
        public Patient GetActivePatient()
        {
            return SessionManager.Instance.GetActivePatient();
        }

        /// <summary>
        /// Initialize session as per selected institution.
        /// </summary>
        private void SetUserSession(string user)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["InstitutionId"] != null)
            {
                this.InstitutionId = HttpContext.Current.Session["InstitutionId"].ToString();
                int _institutionId = Convert.ToInt32(  this.InstitutionId);                
                string rootPath = HttpContext.Current.Server.MapPath(Constants.RootPath);
                string configTemplate = System.IO.File.ReadAllText(System.IO.Path.Combine(rootPath, "config.xml"));
                Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);
                string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);
                SetConfig(_institutionId.ToString(), configuration);
                if (!string.IsNullOrWhiteSpace(user))
                    SetRaActiveUser(user);
            }
           
        }

        private void Audit()
        {
            //ParameterCollection pc = new ParameterCollection();
            //pc.Add("application", "RiskApps3");
            //pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);
            //pc.Add("machineName", System.Environment.MachineName);
            //pc.Add("message", "Accessed appointment");
            //pc.Add("apptID", apptid);

            //BCDB2.Instance.RunSPWithParams("sp_3_AuditUserActivity", pc);
        }



        
    }
}

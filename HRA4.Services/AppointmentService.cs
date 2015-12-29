using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RAM = RiskApps3.Model.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = HRA4.ViewModels;
using HRA4.Mapper;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
using HRA4.Utilities;
using System.Web;
using log4net;
namespace HRA4.Services
{
    public class AppointmentService : IAppointmentService
    {
        string _username;
        RAM.User _user;
        int _institutionId;
        IRepositoryFactory _repositoryFactory;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AppointmentService));
        public AppointmentService(IRepositoryFactory repositoryFactory, string user)
        {
            _username = user;
            _repositoryFactory = repositoryFactory;
        }

        // SessionManager.Instance.ActiveUser.userClinicList

        public List<VM.Appointment> GetAppointments(int InstitutionId)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            if(InstitutionId != null)
            {
                _institutionId = InstitutionId;
                SetUserSession();
                var list = new AppointmentList();
                list.Date = null;
                list.clinicId = 1;
                list.BackgroundListLoad();
                return list.FromRAppointmentList();
            }
            return new List<VM.Appointment>();
        }

        /// <summary>
        /// Initialize session as per selected institution.
        /// </summary>
        private void SetUserSession()
        {
            Logger.DebugFormat("Institution Id: {0}", _institutionId);
            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);
            Logger.DebugFormat("Get Instituion");
            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;
            Logger.DebugFormat("Get Configtemplate");
            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);

            HttpRuntime.Cache[_institutionId.ToString()] = configuration;
            
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            Logger.DebugFormat("Load Users");
            var users = SessionManager.Instance.MetaData.Users;// may cache user list.
            Logger.DebugFormat("User count :{0}",users.Count());
          
           // _user = users.FirstOrDefault(u => _username == u.GetMemberByName(_username).Name) as RAM.User;
            SessionManager.Instance.ActiveUser = users[0] as RAM.User;// need to change this.
        }



         
    }
}

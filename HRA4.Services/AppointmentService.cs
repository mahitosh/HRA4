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
using HRACACHE = HRA4.Utilities.Cache;
using RiskApps3.Model.PatientRecord;
namespace HRA4.Services
{
    public class AppointmentService : IAppointmentService
    {
        string _username;
        RAM.User _user;
        int _institutionId;
        IRepositoryFactory _repositoryFactory;
        HraSessionManager _hraSessionManager;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AppointmentService));
        public AppointmentService(IRepositoryFactory repositoryFactory, string user)
        {
            _username = user;
            _repositoryFactory = repositoryFactory;
            SetUserSession();
        }

        // SessionManager.Instance.ActiveUser.userClinicList

        public List<VM.Appointment> GetAppointments(int InstitutionId)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            List<VM.Appointment> appointments = new List<VM.Appointment>();

            if(InstitutionId != null)
            {

                _institutionId = InstitutionId;
               // SetUserSession();
                //appointments = HRACACHE.GetCache<List<VM.Appointment>>(InstitutionId);
                var list = new AppointmentList();
                 
                list.clinicId = 1;
                list.Date = DateTime.Now.ToString("MM/dd/yyyy");
                list.BackgroundListLoad();
               
                 appointments = list.FromRAppointmentList();
                return appointments;
            }
            return new List<VM.Appointment>();
        }

        private bool GetDNCFlag(string p)
        {
            return true;
        }

        /// <summary>
        /// Initialize session as per selected institution.
        /// </summary>
        private void SetUserSession()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["InstitutionId"] != null)
            {
                _institutionId = Convert.ToInt32(HttpContext.Current.Session["InstitutionId"]);

                Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);

                string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;

                string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);
                _hraSessionManager = new HraSessionManager(_institutionId.ToString(), configuration);
                _hraSessionManager.SetRaActiveUser(_username);
            }

           
            
        }

        private List<VM.Appointment> SearchOnAppointment(List<VM.Appointment> appts, string searchField, string searchParam)
        {
            List<VM.Appointment> newlist = new List<VM.Appointment>();
            switch(searchField)
            {
                case Constants.MRN:
                    newlist = appts.Where(list => list.MRN.Contains(searchParam)).ToList();
                    break;
                case Constants.AppointmentDate:
                    newlist = appts.Where(list => list.AppointmentDate == Convert.ToDateTime(searchParam)).ToList();
                    break;
                case Constants.DateCompleted:
                    newlist = appts.Where(list => list.DateCompleted == Convert.ToDateTime(searchParam)).ToList();
                    break;
                case Constants.DateOfBirth:
                    newlist = appts.Where(list => list.DateOfBirth == Convert.ToDateTime(searchParam)).ToList();
                    break;
                case Constants.DiseaseHx:
                    newlist = appts.Where(list => list.DiseaseHx == searchParam).ToList();
                    break;
                case Constants.DoNotCall:
                    newlist = appts.Where(list => list.DoNotCall == Convert.ToBoolean(searchParam)).ToList();
                    break;
                case Constants.Id:
                    newlist = appts.Where(list => list.Id == Convert.ToInt32(searchParam)).ToList();
                    break;
                case Constants.PatientName:
                    newlist = appts.Where(list => list.PatientName == searchParam).ToList();
                    break;
                case Constants.Provider:
                    newlist = appts.Where(list => list.Provider == searchParam).ToList();
                    break;
                case Constants.Survey:
                    newlist = appts.Where(list => list.Survey == searchParam).ToList();
                    break;
            }
            return newlist;
        }

        public void SaveAppointments(VM.Appointment Appt, int InstitutionId)
        {

            UpdateMarkAsComplete(Appt, InstitutionId);

        }
         
        private void UpdateMarkAsComplete(VM.Appointment Appt, int InstitutionId)
        {
            List<VM.Appointment> apptlist = GetAppointments(InstitutionId);
            List<VM.Appointment> filteredlist = SearchOnAppointment(apptlist, Constants.MRN, Appt.MRN);

            foreach (var item in filteredlist)
            {
                Appointment appt = ((Appointment)(item.ToRAppointment()));

                if (Appt.SetMarkAsComplete)
                {
                    Appointment.MarkComplete(appt.apptID);
                }
                else
                {
                    Appointment.MarkIncomplete(appt.apptID);
                }
            }
        }
    }
}

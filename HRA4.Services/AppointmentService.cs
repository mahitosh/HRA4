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
using System.Collections.Specialized;
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
            List<VM.Appointment> appointments = new List<VM.Appointment>();

            if(InstitutionId != null)
            {

                _institutionId = InstitutionId;
                SetUserSession();
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

        public List<VM.Appointment> GetAppointments(int InstitutionId, NameValueCollection searchfilter)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            List<VM.Appointment> appointments = new List<VM.Appointment>();

            if (InstitutionId != null)
            {
                _institutionId = InstitutionId;
                SetUserSession();
                var list = new AppointmentList();
                list.clinicId = 1;
                if (Convert.ToString(searchfilter["appdt"]) != null && Convert.ToString(searchfilter["appdt"]) != "")
                    list.Date = Convert.ToString(searchfilter["appdt"]);
                if (Convert.ToString(searchfilter["name"]) != null && Convert.ToString(searchfilter["name"]) !="")
                list.NameOrMrn = Convert.ToString(searchfilter["name"]);
                list.BackgroundListLoad();
                appointments = list.FromRAppointmentList();
                return appointments;
            }
           
            return new List<VM.Appointment>();
        }

        /// <summary>
        /// Initialize session as per selected institution.
        /// </summary>
        private void SetUserSession()
        {
            
            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);
           
            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;
            
            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);

            HttpRuntime.Cache[_institutionId.ToString()] = configuration;
            
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            
            var users = SessionManager.Instance.MetaData.Users;// may cache user list.
            
          
           // _user = users.FirstOrDefault(u => _username == u.GetMemberByName(_username).Name) as RAM.User;
            SessionManager.Instance.ActiveUser = users[0] as RAM.User;// need to change this.


            //string assignedBy = SessionManager.Instance.ActiveUser.ToString();
            //SessionManager.Instance.SetActivePatient("99911041501", 1);
            //Patient p = SessionManager.Instance.GetActivePatient();    // TODO:  Check this!!
            //var t = new RiskApps3.Model.PatientRecord.Communication.Task(p, "Task", null, assignedBy, DateTime.Now);
            //HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            //args.Persist = true;
            
            //p.Tasks.AddToList(t, args);
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
            NameValueCollection searchfilter = new NameValueCollection();
            searchfilter.Add("name",Appt.MRN);
            searchfilter.Add("appdt",null);
            List<VM.Appointment> filteredlist = GetAppointments(InstitutionId, searchfilter);
          //  List<VM.Appointment> filteredlist = SearchOnAppointment(apptlist, Constants.MRN, Appt.MRN);

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

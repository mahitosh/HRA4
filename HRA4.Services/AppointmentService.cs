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
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Model.PatientRecord;
using RA = RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
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


        public List<VM.Tasks> GetTasks(int InstitutionId, string unitnum)
        {
            if (InstitutionId != null)
            {
                _institutionId = InstitutionId;
                SetUserSession();
                Patient p = new Patient();
                p.unitnum = unitnum;
                var list = new TaskList(p);
                list.BackgroundListLoad();
                return list.FromRATaskList();
            }

            return new List<VM.Tasks>();

        }


        public bool GetDNCStatus(int InstitutionId, string unitnum)
        {

            bool _DNCStatus = false;


            if (GetTasks(InstitutionId,unitnum).Count > 0)
            {
                _DNCStatus = true;
            }

            return _DNCStatus;

        }


        public List<VM.Appointment> GetAppointments(int InstitutionId)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            List<VM.Appointment> appointments = new List<VM.Appointment>();
            if(InstitutionId != null)
            {
                _institutionId = InstitutionId;
                SetUserSession();
                var list = new AppointmentList();
                list.Date = null;
                list.clinicId = 1;
                list.BackgroundListLoad();

                foreach (RA.Appointment app in list)
                {
                    
                    bool _DNCStatus = GetDNCStatus(InstitutionId,app.unitnum);
                    appointments.Add(app.FromRAppointment(_DNCStatus));

                }

                return appointments;

                //return list.FromRAppointmentList();
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
        public void AssignCommonSettings(int _institutionId)
        {
            
            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);

            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;

            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);

            HttpRuntime.Cache[_institutionId.ToString()] = configuration;
        }


        public void DeleteTasks(int _institutionId, string unitnum, int apptid)
        {
            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);

            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;

            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);

            HttpRuntime.Cache[_institutionId.ToString()] = configuration;

        
            string assignedBy = "";
            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();
                }
            }
            SessionManager.Instance.SetActivePatient(unitnum, apptid);
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();
            //Creating task object
            RiskApps3.Model.PatientRecord.Communication.Task t = new RiskApps3.Model.PatientRecord.Communication.Task(p, "Task", null, assignedBy, DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            TaskList tList = new TaskList(p);
            tList.LoadFullList();

            AppointmentList apt = new AppointmentList();
            apt.LoadFullList();



            foreach (RiskApps3.Model.PatientRecord.Communication.Task task in tList)
            {
                PtFollowupList fList = new PtFollowupList(task);
                fList.LoadFullList();
                HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                args1.Delete = true;
                foreach(PtFollowup followup in fList)
                {
                    followup.BackgroundPersistWork(args1);
                }
                task.BackgroundPersistWork(args1);
            }
            //Creating folowup object



        }

      
        public void AddTasks(int _institutionId, string unitnum, int apptid)
        {
            

            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);

            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;

            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);

            HttpRuntime.Cache[_institutionId.ToString()] = configuration;

            /* code written by nilesh  */
            string assignedBy = "";
            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();
                }
            }
            SessionManager.Instance.SetActivePatient(unitnum ,apptid);
            RiskApps3.Model.PatientRecord.Patient p = SessionManager.Instance.GetActivePatient();    // TODO:  Check this!!
            RiskApps3.Model.PatientRecord.Communication.Task t = new RiskApps3.Model.PatientRecord.Communication.Task(p, "Task", null, assignedBy, DateTime.Now);
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
          
           

            t.BackgroundPersistWork(args);
           
            RiskApps3.Model.PatientRecord.Communication.PtFollowup newFollowup = new RiskApps3.Model.PatientRecord.Communication.PtFollowup(t);
            newFollowup.FollowupDisposition = "Omit From List";
            newFollowup.TypeOfFollowup = "Phone Call";
            newFollowup.Comment = "Do Not Call";
            newFollowup.Who = assignedBy;
            newFollowup.Date = DateTime.Now;
            args = new HraModelChangedEventArgs(null);
            t.FollowUps.Add(newFollowup);
            newFollowup.BackgroundPersistWork(args);


            /* End code written by nilesh  */


        }

    

         
    }
}

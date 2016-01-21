using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.Clinic.Reports;
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
using HRA4.Services;
using System.Web;
using log4net;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.Model.PatientRecord;
using RA = RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Communication;
using System.Collections.Specialized;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Utilities;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Drawing;
using System.Data.SqlClient;
 
namespace HRA4.Services
{
    public class AppointmentService : IAppointmentService
    {
        string _username;
        RAM.User _user;
        int _institutionId;
        IRepositoryFactory _repositoryFactory;
        IHraSessionManager _hraSessionManager;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AppointmentService));
        public AppointmentService(IRepositoryFactory repositoryFactory, IHraSessionManager hraSessionManger)
        {
            _repositoryFactory = repositoryFactory;
            _hraSessionManager = hraSessionManger;
            _username = _hraSessionManager.Username;

        }
        public AppointmentService(IRepositoryFactory repositoryFactory, string user)
        {
            _username = user;
            _repositoryFactory = repositoryFactory;
            //SetUserSession();
        }


        public List<VM.Tasks> GetTasks(int InstitutionId, string unitnum)
        {
            if (InstitutionId != null)
            {
                _institutionId = InstitutionId;
                //SetUserSession();
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


            if (GetTasks(InstitutionId, unitnum).Count > 0)
            {
                _DNCStatus = true;
            }

            return _DNCStatus;

        }


        public List<VM.Clinic> GetClinics(int InstitutionId)
        {

            List<VM.Clinic> clinics = new List<VM.Clinic>();
            if (InstitutionId > 0)
            {
                //SetUserSession();
                
                _institutionId = InstitutionId;

                clinics = GetClinicList();

                return clinics;
            }
            return new List<VM.Clinic>();


        }

        private static List<VM.Clinic> GetClinicList()
        {
            List<VM.Clinic> clinics = new List<VM.Clinic>();
            var list = new RAM.ClinicList();
            list.user_login = SessionManager.Instance.ActiveUser.userLogin;
            list.BackgroundListLoad();

            clinics = list.FromRClinicList();
            return clinics;
        }


        public List<VM.Appointment> GetAppointments(int InstitutionId)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            List<VM.Appointment> appointments = new List<VM.Appointment>();
            if (InstitutionId != null)
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

        /// <summary>
        /// To Show Appointments based on seacrh filter entered by user
        /// </summary>
        /// <param name="InstitutionId">Institution Id</param>
        /// <param name="searchfilter">Namevalue collection of seacrh parameter like ClinicId,Appointment Date, Name Or MRN</param>
        /// <returns>List of Appointments based on Search criteria</returns>
        public List<VM.Appointment> GetAppointments(int InstitutionId, NameValueCollection searchfilter)
        {
            Logger.DebugFormat("Institution Id: {0}", InstitutionId);
            List<VM.Appointment> appointments = new List<VM.Appointment>();

            if (InstitutionId != null)
            {
                _institutionId = InstitutionId;
                // SetUserSession();
                var list = new AppointmentList();
                if (Convert.ToString(searchfilter["clinicId"]) != null && Convert.ToString(searchfilter["clinicId"]) != "")
                    list.clinicId = Convert.ToInt32(searchfilter["clinicId"]);
                if (Convert.ToString(searchfilter["appdt"]) != null && Convert.ToString(searchfilter["appdt"]) != "")
                    list.Date = Convert.ToString(searchfilter["appdt"]);
                if (Convert.ToString(searchfilter["name"]) != null && Convert.ToString(searchfilter["name"]) != "")
                    list.NameOrMrn = Convert.ToString(searchfilter["name"]);
                list.BackgroundListLoad();
                foreach (RA.Appointment app in list)
                {

                    bool _DNCStatus = GetDNCStatus(InstitutionId, app.unitnum);
                    appointments.Add(app.FromRAppointment(_DNCStatus));

                }

                return appointments;

                //return list.FromRAppointmentList();
            }
            return new List<VM.Appointment>();
        }



        /// <summary>
        /// It will do searching on passed Appointment list based on below parameters
        /// </summary>
        /// <param name="appts">Appointment list on which searching will be performed</param>
        /// <param name="searchField">Column Name</param>
        /// <param name="searchParam">Value to Search</param>
        /// <returns>List of searched appointments</returns>
        private List<VM.Appointment> SearchOnAppointment(List<VM.Appointment> appts, string searchField, string searchParam)
        {
            List<VM.Appointment> newlist = new List<VM.Appointment>();
            switch (searchField)
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

        /// <summary>
        /// To Save Appointments
        /// </summary>
        /// <param name="Appt">Appointment Object to Save</param>
        /// <param name="InstitutionId">Institution Id</param>
        public void SaveAppointments(VM.Appointment Appt, int InstitutionId)
        {

            UpdateMarkAsComplete(Appt, InstitutionId);


        }
        /// <summary>
        /// To do process of Run Automation Documents
        /// </summary>
        /// <param name="InstitutionId">Institution Id</param>
        /// <param name="apptid">Appointment Id</param>
        /// <param name="MRN">MRN Number</param>
        /// <returns></returns>
        public FileInfo RunAutomationDocuments(int InstitutionId, int apptid, string MRN)
        {
            Patient proband = CalculateRiskAndRunAutomation(apptid, MRN);
            return proband.file;
        }

        /// <summary>
        /// Method which actually calculates Risk and Run Automation
        /// </summary>
        /// <param name="apptid">Appointment Id</param>
        /// <param name="MRN">MRN Number</param>
        /// <returns>Patient Model</returns>
        private Patient CalculateRiskAndRunAutomation(int apptid, string MRN)
        {
            Appointment.MarkComplete(apptid);
            SessionManager.Instance.SetActivePatient(MRN, apptid);
            Patient proband = SessionManager.Instance.GetActivePatient();
            string toolsPath = HttpContext.Current.Server.MapPath(Constants.RAFilePath);
            proband.RecalculateRisk(false, toolsPath, "");
            proband.RunAutomation();
            return proband;
        }

        /// <summary>
        ///To show Risk Score on Click of Risk Calculation  
        /// </summary>
        /// <param name="apptid">Appointment Id</param>
        /// <param name="MRN">MRN Number</param>
        /// <returns>Risk Score Model</returns>
        public VM.RiskScore RiskScore(int apptid, string MRN)
        {
            SessionManager.Instance.SetActivePatient(MRN, apptid);
            Patient proband = SessionManager.Instance.GetActivePatient();
            proband.RP.LoadFullObject();
            VM.RiskScore RS = RiskScoreMapper.ToRiskScore(proband.RP);
            RS.ApptId = apptid;
            RS.MRN = MRN;
            return RS;
        }

        /// <summary>
        /// To Show Risk Score on Click of Run Risk Models Button. It calculates Risk Score
        /// </summary>
        /// <param name="apptid">Appointment Id</param>
        /// <param name="MRN">MRN Number</param>
        /// <returns></returns>
        public VM.RiskScore RiskCalculateAndRunAutomation(int apptid, string MRN)
        {
            Patient proband = CalculateRiskAndRunAutomation(apptid, MRN);
            proband.RP.LoadFullObject();
            VM.RiskScore RS = RiskScoreMapper.ToRiskScore(proband.RP);
            RS.ApptId = apptid;
            RS.MRN = MRN;
            return RS;

        }
        /// <summary>
        /// To Mark appointment as complete and incomplete
        /// </summary>
        /// <param name="Appt">Appointment Id</param>
        /// <param name="InstitutionId">Institution Id</param>
        private void UpdateMarkAsComplete(VM.Appointment Appt, int InstitutionId)
        {

            NameValueCollection searchfilter = new NameValueCollection();
            searchfilter.Add("name", Appt.MRN);
            searchfilter.Add("appdt", null);
            searchfilter.Add("clinicId", Appt.clinicID.ToString());
            List<VM.Appointment> filteredlist = GetAppointments(InstitutionId, searchfilter);
            //List<VM.Appointment> filteredlist = SearchOnAppointment(apptlist, Constants.MRN, Appt.MRN);

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


        public void DeleteTasks(int _institutionId, string unitnum, int apptid)
        {
            //SetUserSession();
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
                foreach (PtFollowup followup in fList)
                {
                    followup.BackgroundPersistWork(args1);
                }
                task.BackgroundPersistWork(args1);
            }
            //Creating folowup object



        }



        public void DeleteAppointment(int InstitutionId, int apptid)
        {

            Appointment.DeleteApptData(apptid, false);
        }

        public string ShowPedigreeImage(int _institutionId, string unitnum, int apptid, string PedigreeImageSavePath)
        {
            int Width = 625;
            int Height = 625;
            string _ImagePath = string.Empty;
            string assignedBy = "";

            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();

                }
            }
            string _userlogin = SessionManager.Instance.ActiveUser.userLogin;
            SessionManager.Instance.SetActivePatient(unitnum, apptid);

            RiskApps3.Model.PatientRecord.Patient proband = SessionManager.Instance.GetActivePatient();    // TODO:  Check this!!

            PedigreeGenerator pg = new PedigreeGenerator(Width, Height, proband);
            Bitmap bmp;
            if (proband != null)
            {
                bmp = pg.GeneratePedigreeImage(proband);
            }
            else
            {
                bmp = pg.GeneratePedigreeImage();
            }
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            var base64Data = Convert.ToBase64String(stream.ToArray());
            /*
            _ImagePath = SaveImage(base64Data, _institutionId, _userlogin, apptid, PedigreeImageSavePath);
           return _ImagePath;
            */
            return base64Data;

        }

        public string SaveImage(string base64, int _institutionId, string _userlogin, int apptid, string PedigreeImageSavePath)
        {
            string _ImagePath = string.Empty;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Convert.FromBase64String(base64)))
            {
                using (Bitmap bm2 = new Bitmap(ms))
                {


                    //  PedigreeImagePath = System.Web.Serv
                    string _ImageName = _institutionId.ToString() + "_" + _userlogin + "_" + apptid.ToString() + ".png";

                    bm2.Save(PedigreeImageSavePath + _ImageName);
                    _ImagePath = _ImageName;
                }
            }

            return _ImagePath;

        }


        public void AddTasks(int _institutionId, string unitnum, int apptid)
        {

            //SetUserSession();

            /* code written by nilesh  */
            string assignedBy = "";
            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();
                }
            }
            SessionManager.Instance.SetActivePatient(unitnum, apptid);
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

        public VM.AuditReports GetAuditReports(string MRN, string startdate, string enddate)
        {

            //For getting list of AuditMrnAccessV2
            AuditMrnAccessV2 auditMrnAccessV2 = new AuditMrnAccessV2();
            auditMrnAccessV2.StartTime =Convert.ToDateTime(startdate);
            auditMrnAccessV2.EndTime =Convert.ToDateTime(enddate);
            auditMrnAccessV2.unitnum = MRN;
            auditMrnAccessV2.BackgroundListLoad();
          
            List<VM.AuditMrnAccessV2Entry> auditMrnAccessV2Entry = new List<VM.AuditMrnAccessV2Entry>();
            foreach (AuditMrnAccessV2Entry item in auditMrnAccessV2)
            {
                //you have to convert item to VM.V2Access model
                VM.AuditMrnAccessV2Entry a = AuditReportsMapper.ToAuditMrnAccessV2Entry(item);
                auditMrnAccessV2Entry.Add(a);

            }

            //For getting list of AuditMrnAccessV3
            AuditMrnAccessV3 auditMrnAccessV3 = new AuditMrnAccessV3();
            auditMrnAccessV3.StartTime = Convert.ToDateTime(startdate);
            auditMrnAccessV3.EndTime = Convert.ToDateTime(enddate);
            auditMrnAccessV3.unitnum = MRN;
            auditMrnAccessV3.BackgroundListLoad();

            List<VM.AuditMrnAccessV3Entry> auditMrnAccessV3Entry = new List<VM.AuditMrnAccessV3Entry>();
            foreach (AuditMrnAccessV3Entry item in auditMrnAccessV3)
            {
                //you have to convert item to VM.V2Access model
                VM.AuditMrnAccessV3Entry a = AuditReportsMapper.ToAuditMrnAccessV3Entry(item);
                auditMrnAccessV3Entry.Add(a);

            }

            VM.AuditReports reports = new VM.AuditReports();
            reports.RSAuditMrnAccessV2Entry = auditMrnAccessV2Entry;
            reports.RSAuditMrnAccessV3Entry = auditMrnAccessV3Entry;

            return reports;


        }


        #region TestPatients

        public VM.TestPatient LoadCreateTestPatients()
        {
            VM.TestPatient tp = new VM.TestPatient();
            TestPatientManager Tpm=new TestPatientManager();
            tp.Surveys = Tpm.GetSurveys();
            tp.Clinics = GetClinicList();
            tp.InitateTestPatients = Tpm.InitiateTestPatients();

            return tp;
                         
        }

        public void CreateTestPatients(int NoOfPatients, string dtAppointmentDate, int surveyID,string SurveyName,int clinicID)
        {
            TestPatientManager Pm = new TestPatientManager();
            Pm.CreateTestPatients(NoOfPatients, dtAppointmentDate, surveyID, SurveyName, clinicID);

        }

        #endregion
    }
}

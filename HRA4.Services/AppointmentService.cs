﻿using HRA4.Services.Interfaces;
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
using RiskApps3.Model.MetaData;
namespace HRA4.Services
{
    public class AppointmentService : IAppointmentService
    {
        private Person relative = null;

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

        public AppointmentService(Person person)
        {

            relative = person;
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

        

        public VM.Appointment GetAppointment(int InstitutionId, NameValueCollection searchfilter, string apptid)
        {
            // I want a appointment using AppointmentID


            List<VM.Appointment> ap = GetAppointments(InstitutionId, searchfilter);
            VM.Appointment Filteredlist = SearchOnAppointment(ap, Constants.Id, apptid).FirstOrDefault();
            if (Filteredlist == null)
                return new VM.Appointment();

            SessionManager.Instance.SetActivePatient(Filteredlist.MRN, Filteredlist.Id);
            SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
            //SessionManager.Instance.MetaData.AllProviders.BackgroundListLoad();

            Patient p = SessionManager.Instance.GetActivePatient();
            p.Providers.BackgroundListLoad();

            Filteredlist.clinics = GetClinicList();
            Filteredlist.FromRAppointment(p);
            Filteredlist.IsCopyAppointment = "No";
            
           // Filteredlist.Providers = SessionManager.Instance.MetaData.AllProviders.ToProviderList();
            return Filteredlist;
        }

        public VM.Appointment GetAppointmentForAdd(string MRN, int clinicId)
        {
            GoldenAppointment _appointment = new GoldenAppointment();
            _appointment.MRN = MRN;
            _appointment.Load();
            Patient _patient;
            VM.Appointment app = new VM.Appointment();
            if (_appointment.apptid.HasValue)
            {
                AppointmentList appts = new AppointmentList();
                appts.BackgroundListLoad();

             

              ////Appointment goldenAppointment = appts.First(appt => appt.id == _appointment.apptid);
                
              // // SessionManager.Instance.SetActivePatient(goldenAppointment.unitnum, goldenAppointment.apptID);
              //  SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
              ////  SessionManager.Instance.MetaData.AllProviders.BackgroundListLoad();
              //  _patient = SessionManager.Instance.GetActivePatient();
              //  _patient.Providers.BackgroundListLoad();
              // // VM.Appointment app = goldenAppointment.FromRAppointment();
              //  app.clinics= GetClinicList();
              ////  app.Providers = SessionManager.Instance.MetaData.AllProviders.ToProviderList();
              //  app.FromRAppointment(_patient);
              //  app.IsGoldenAppointment = "Yes";
              //  app.IsCopyAppointment = "No";
                return app;

            }
            else
            {
                _patient = new Patient(MRN);
                _patient.Providers.LoadFullList();

                SessionManager.Instance.SetActivePatient(_patient.unitnum, _patient.apptid);
                Appointment appointment = new Appointment();
              //  appointment = new Appointment(clinicId, MRN) { };
              //  VM.Appointment app = appointment.FromRAppointment();
              //  app.clinics=GetClinicList();
              // // SessionManager.Instance.MetaData.AllProviders.BackgroundListLoad();
              ////  app.Providers = SessionManager.Instance.MetaData.AllProviders.ToProviderList();
              //  app.FromRAppointment(_patient);
              //  app.IsGoldenAppointment = "No";
              //  app.IsCopyAppointment = "No";
                return app;

            }

        }

        public VM.Appointment GetAppointmentForCopy(string ApptId, int InstitutionId, NameValueCollection searchfilter)
        {
            Patient _patient;
            List<VM.Appointment> ap = GetAppointments(InstitutionId, searchfilter);
            VM.Appointment copyAppt = SearchOnAppointment(ap, Constants.Id, ApptId).FirstOrDefault();
            if (copyAppt == null)
                return new VM.Appointment();
            Appointment toCopy = copyAppt.ToRAppointment();
            VM.Appointment app = new VM.Appointment();
            //Appointment copiedFromExisting = new Appointment(toCopy, copyAppt.clinicID);
            //copiedFromExisting.BackgroundPersistWork(new HraModelChangedEventArgs(null));

            //SessionManager.Instance.SetActivePatient(copiedFromExisting.unitnum, copiedFromExisting.apptID);
            //SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
            //SessionManager.Instance.MetaData.AllProviders.BackgroundListLoad();
            //_patient = SessionManager.Instance.GetActivePatient();
            //_patient.Providers.BackgroundListLoad();
            //VM.Appointment app = copiedFromExisting.FromRAppointment();
            //app.clinics = GetClinicList();
            //app.Providers = SessionManager.Instance.MetaData.AllProviders.ToProviderList();
            //app.FromRAppointment(_patient);
            //app.AppointmentDate = DateTime.Now;
            //app.appttime = null;
            //app.IsGoldenAppointment = "No";
            //app.IsCopyAppointment = "Yes";
           // SavePatient(app);
            return app;
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
            SaveAppointments(Appt);
            UpdateMarkAsComplete(Appt, InstitutionId);
            
        }
       
        
        private void SaveAppointments(VM.Appointment Appt)
        {
            var raAppt = Appt.ToRAppointment();
           // raAppt.CreateAppointmentRecordsIfNeeded();
            raAppt.BackgroundPersistWork(new HraModelChangedEventArgs(null));


            SavePatient(Appt);
        }

        private void SavePatient(VM.Appointment Appt)
        {
            var raPatient = Appt.ToRAPatient();
           // raPatient.AddStockRelatives();
            raPatient.Providers.AddRange(SaveProvider(Appt));
            raPatient.BackgroundPersistWork(new HraModelChangedEventArgs(null));
            raPatient.Providers.PersistFullList(new HraModelChangedEventArgs(null));
        }

        private ProviderList SaveProvider(VM.Appointment Appt)
        {
            ProviderList pl = new ProviderList();
            //SessionManager.Instance.MetaData.AllProviders.BackgroundListLoad();
            //AllProviders allproviders= SessionManager.Instance.MetaData.AllProviders;
            //Provider providerRef = allproviders.Where(p => p.providerID == Appt.RefPhysician).FirstOrDefault();
            //providerRef.refPhys = true;
            //providerRef.PCP = false;
            //providerRef.apptid = Appt.Id;
            //Provider providerPCP = allproviders.Where(p => p.providerID == Appt.PCP).FirstOrDefault();
            //providerPCP.refPhys = false;
            //providerPCP.PCP = true;
            //providerPCP.apptid = Appt.Id;
            //ProviderList pl = new ProviderList();
            //pl.Add(providerPCP);
            //pl.Add(providerRef);
            
            return pl;


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
        public Patient CalculateRiskAndRunAutomation(int apptid, string MRN)
        {
            // Appointment.MarkComplete(apptid);// Commented this code as the new library is giving error.
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

                //Commented below line as the new library is giving error.
                if (Appt.SetMarkAsComplete)
                {
                   // appt.MarkComplete();
                    Appointment.MarkComplete(appt.apptID);
                }
                else
                {
                    //appt.MarkIncomplete();
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



        public void DeleteAppointment(int InstitutionId, int apptid,bool flag)
        {

            Appointment.DeleteApptData(apptid,flag);
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
            auditMrnAccessV2.StartTime = Convert.ToDateTime(startdate);
            auditMrnAccessV2.EndTime = Convert.ToDateTime(enddate);
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

        public bool SetDeleteButtonFlag(Person relative)
        {


            bool retval = true;

            if (relative == null)
                return false ;

            if (relative.relativeID < 8)
            {
                retval = false;
            }
            else
            {
                foreach (Person p in relative.owningFHx)
                {
                    if (p.motherID == relative.relativeID || p.fatherID == relative.relativeID)
                    {
                        retval = false;
                        break;
                    }
                }
            }

            return retval;
        
        }

        public void AddRelative(RiskApps3.Model.PatientRecord.Patient proband, ViewModels.FamilyHistoryRelative obj)
        {
            /*==============================*/

            RiskApps3.Model.PatientRecord.FHx.FamilyHistory FHX = new RiskApps3.Model.PatientRecord.FHx.FamilyHistory(proband);

            proband.FHx.BackgroundListLoad();

            string relationship = "";
            string bloodline = "";

            int count = 1;

            switch (obj.Relationship)
            {
                case "Son":
                case "Daughter":
                case "Brother":
                case "Sister":
                case "Cousin":
                case "Cousin (Male)":
                case "Cousin (Female)":
                case "Niece":
                case "Nephew":
                case "Other":
                case "Other (Male)":
                case "Other (Female)":
                    relationship = obj.Relationship;
                    break;
                case "Aunt - Maternal":
                    relationship = "Aunt";
                    bloodline = "Maternal";
                    break;
                case "Aunt - Paternal":
                    relationship = "Aunt";
                    bloodline = "Paternal";
                    break;
                case "Uncle - Maternal":
                    relationship = "Uncle";
                    bloodline = "Maternal";
                    break;
                case "Uncle - Paternal":
                    relationship = "Uncle";
                    bloodline = "Paternal";
                    break;

            }

            if (relationship.Length > 0)
            {
                string new_rel_type;

                List<Person> retval = new List<Person>();

                RelationshipEnum re = Relationship.Parse(relationship);
                GenderEnum ge = Relationship.getGenderFromRelationshipType(re);
                if (Relationship.isOffspring(re))
                {
                    Person newSpouse = null;
                    foreach (Person q in proband.FHx)
                    {
                        if (q.relationshipOther == "Spouse of Self")
                        {
                            newSpouse = q;
                            break;
                        }
                    }
                    if (newSpouse == null)
                    {
                        newSpouse = proband.FHx.CreateSpouse(proband);
                      
                        
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        args.updatedMembers.Add(newSpouse.GetMemberByName("relativeID")); // Edit And save
                        args.updatedMembers.Add(newSpouse.GetMemberByName("relationship"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("relationshipOther"));
                        //args.updatedMembers.Add(newSpouse.GetMemberByName("bloodline"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("motherID"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("fatherID"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("gender"));
                        //args.updatedMembers.Add(newSpouse.GetMemberByName("vitalStatus"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("twinID"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("x_position"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("y_position"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("x_norm"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("y_norm"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("pedigreeGroup"));
                        args.updatedMembers.Add(newSpouse.GetMemberByName("consanguineousSpouseID"));
                        newSpouse.BackgroundPersistWork(args);

                       // newSpouse.BackgroundPersistWork(); 
                        //this.AddToList(newSpouse, new HraModelChangedEventArgs(null));
                    }

                    for (int i = 0; i < count; i++)
                    {
                        proband.FHx.BackgroundListLoad();

                        int nextID = proband.FHx.GetNewRelativeID();
                        Person newRel = new Person(proband.FHx);
                        newRel.HraState = RiskApps3.Model.HraObject.States.Ready;
                        newRel.relativeID = nextID;
                        newRel.motherID = 0;
                        newRel.fatherID = 0;

                        newRel.owningFHx = proband.FHx;
                        newRel.vitalStatus = "Alive";
                        newRel.gender = Gender.toString(ge);
                        newRel.relationship = relationship;
                        newRel.bloodline = bloodline;

                        if (proband.gender == "Female")
                        {
                            newRel.motherID = proband.relativeID;
                            newRel.fatherID = newSpouse.relativeID;
                        }
                        else
                        {
                            newRel.motherID = newSpouse.relativeID;
                            newRel.fatherID = proband.relativeID;
                        }


                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        args.updatedMembers.Add(newRel.GetMemberByName("relativeID")); // Edit And save
                        args.updatedMembers.Add(newRel.GetMemberByName("relationship"));
                        args.updatedMembers.Add(newRel.GetMemberByName("bloodline"));
                        args.updatedMembers.Add(newRel.GetMemberByName("motherID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("fatherID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("gender"));
                        args.updatedMembers.Add(newRel.GetMemberByName("vitalStatus"));
                        args.updatedMembers.Add(newRel.GetMemberByName("twinID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("x_position"));
                        args.updatedMembers.Add(newRel.GetMemberByName("y_position"));
                        args.updatedMembers.Add(newRel.GetMemberByName("x_norm"));
                        args.updatedMembers.Add(newRel.GetMemberByName("y_norm"));
                        args.updatedMembers.Add(newRel.GetMemberByName("pedigreeGroup"));
                        args.updatedMembers.Add(newRel.GetMemberByName("consanguineousSpouseID"));
                        newRel.BackgroundPersistWork(args);
                        //this.AddToList(newRel, new HraModelChangedEventArgs(null)); 
                        retval.Add(newRel);
                    }
                    //AddChild(proband, false, count, ge);
                    //List<Person> newFolks = AddChild(proband, false, count, ge);
                    //foreach (Person np in newFolks)
                    //{
                    //    retval.Add(np);
                    //    //np.SignalModelChanged(new HraModelChangedEventArgs(null));
                    //}
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        int nextID = proband.FHx.GetNewRelativeID();
                        Person newRel = new Person(proband.FHx);
                        newRel.HraState = RiskApps3.Model.HraObject.States.Ready;
                        newRel.relativeID = nextID;
                        newRel.motherID = 0;
                        newRel.fatherID = 0;

                        newRel.owningFHx = proband.FHx;
                        newRel.vitalStatus = "Alive";
                        newRel.gender = Gender.toString(ge);
                        newRel.relationship = relationship;
                        newRel.bloodline = bloodline;
                        proband.FHx.SetIDsFromRelationship(ref newRel);

                       
                        
                        HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                        args.updatedMembers.Add(newRel.GetMemberByName("relativeID")); // Edit And save
                        args.updatedMembers.Add(newRel.GetMemberByName("relationship"));
                        args.updatedMembers.Add(newRel.GetMemberByName("bloodline"));
                        args.updatedMembers.Add(newRel.GetMemberByName("motherID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("fatherID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("gender"));
                        args.updatedMembers.Add(newRel.GetMemberByName("vitalStatus"));
                        args.updatedMembers.Add(newRel.GetMemberByName("twinID"));
                        args.updatedMembers.Add(newRel.GetMemberByName("x_position"));
                        args.updatedMembers.Add(newRel.GetMemberByName("y_position"));
                        args.updatedMembers.Add(newRel.GetMemberByName("x_norm"));
                        args.updatedMembers.Add(newRel.GetMemberByName("y_norm"));
                        args.updatedMembers.Add(newRel.GetMemberByName("pedigreeGroup"));
                        args.updatedMembers.Add(newRel.GetMemberByName("consanguineousSpouseID"));
                        newRel.BackgroundPersistWork(args);
                        
                        /*
                        proband.FHx.AddToList(newRel, new HraModelChangedEventArgs(null));
                         */ 
                        retval.Add(newRel);
                    }
                }
               


            }


        
        }

        public void DeleteSurvey(RiskApps3.Model.PatientRecord.Patient proband, ViewModels.FamilyHistoryRelative obj)
        {

            RiskApps3.Model.PatientRecord.PastMedicalHistory pmh = new RiskApps3.Model.PatientRecord.PastMedicalHistory(proband);
            Person per = proband.FHx.Relatives.Where(p => p.relativeID == obj.RelativeId).FirstOrDefault();

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Delete = true;
            args.updatedMembers.Add(per.GetMemberByName("relativeID")); // Edit And save
            args.updatedMembers.Add(per.GetMemberByName("relationship"));
            args.updatedMembers.Add(per.GetMemberByName("bloodline"));
            args.updatedMembers.Add(per.GetMemberByName("motherID"));
            args.updatedMembers.Add(per.GetMemberByName("fatherID"));
            args.updatedMembers.Add(per.GetMemberByName("gender"));
            args.updatedMembers.Add(per.GetMemberByName("vitalStatus"));
            args.updatedMembers.Add(per.GetMemberByName("twinID"));
            args.updatedMembers.Add(per.GetMemberByName("x_position"));
            args.updatedMembers.Add(per.GetMemberByName("y_position"));
            args.updatedMembers.Add(per.GetMemberByName("x_norm"));
            args.updatedMembers.Add(per.GetMemberByName("y_norm"));
            args.updatedMembers.Add(per.GetMemberByName("pedigreeGroup"));
            args.updatedMembers.Add(per.GetMemberByName("consanguineousSpouseID"));
            per.BackgroundPersistWork(args);
        
        }

        public void EditSurvey(RiskApps3.Model.PatientRecord.Patient proband, ViewModels.FamilyHistoryRelative obj)
        {
            RiskApps3.Model.PatientRecord.PastMedicalHistory pmh = new RiskApps3.Model.PatientRecord.PastMedicalHistory(proband);
            Person per = proband.FHx.Relatives.Where(p => p.relativeID == obj.RelativeId).FirstOrDefault();
            per.vitalStatus = obj.VitalStatus;
            per.relationship = obj.Relationship;
            per.age = obj.RelativeAge;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.updatedMembers.Add(per.GetMemberByName("vitalStatus"));
            //args.updatedMembers.Add(per.GetMemberByName("relationship"));
            args.updatedMembers.Add(per.GetMemberByName("age"));
            per.BackgroundPersistWork(args);


            relative = per;


            relative.PMH.BackgroundLoadWork();
            pmh = relative.PMH;



            SessionManager.Instance.MetaData.Diseases.BackgroundListLoad();


            relative.PMH.BackgroundLoadWork();

            for (int i = 0; i < 3; i++)
            {

                ClincalObservation co = new ClincalObservation();

                if (i < pmh.Observations.Count)
                {

                    co = (ClincalObservation)pmh.Observations[i];


                }

                switch (i)
                {
                    case 0:
                        if (co.instanceID != 0)
                        {
                            co.disease = obj.FirstDx;
                            co.ageDiagnosis = obj.FirstAgeOnset;
                            co.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co.GetMemberByName("disease"));
                            args1.updatedMembers.Add(co.GetMemberByName("ageDiagnosis"));
                            co.BackgroundPersistWork(args1);
                        }
                        else
                        {


                            ClincalObservation co1 = new ClincalObservation(pmh);
                            co1.disease = obj.FirstDx;
                            co1.ageDiagnosis = obj.FirstAgeOnset;
                            co1.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co1.GetMemberByName("disease"));

                            args1.updatedMembers.Add(co1.GetMemberByName("ageDiagnosis"));

                            co1.BackgroundPersistWork(args1);

                            pmh.Observations.AddToList(co1, args1);


                        }

                        break;
                    case 1:
                        if (co.instanceID != 0)
                        {
                            co.disease = obj.SecondDx;
                            co.ageDiagnosis = obj.SecondAgeOnset;
                            co.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co.GetMemberByName("disease"));
                            args1.updatedMembers.Add(co.GetMemberByName("ageDiagnosis"));
                            co.BackgroundPersistWork(args1);
                        }
                        else
                        {


                            ClincalObservation co1 = new ClincalObservation(pmh);
                            co1.disease = obj.SecondDx;
                            co1.ageDiagnosis = obj.SecondAgeOnset;
                            co1.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co1.GetMemberByName("disease"));

                            args1.updatedMembers.Add(co1.GetMemberByName("ageDiagnosis"));

                            co1.BackgroundPersistWork(args1);

                            pmh.Observations.AddToList(co1, args1);


                        }
                        break;
                    case 2:
                        if (co.instanceID != 0)
                        {
                            co.disease = obj.ThirdDx;
                            co.ageDiagnosis = obj.ThirdAgeOnset;
                            co.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co.GetMemberByName("disease"));
                            args1.updatedMembers.Add(co.GetMemberByName("ageDiagnosis"));
                            co.BackgroundPersistWork(args1);
                        }
                        else
                        {


                            ClincalObservation co1 = new ClincalObservation(pmh);
                            co1.disease = obj.ThirdDx;
                            co1.ageDiagnosis = obj.ThirdAgeOnset;
                            co1.SetDiseaseDetails();
                            HraModelChangedEventArgs args1 = new HraModelChangedEventArgs(null);
                            args1.updatedMembers.Add(co1.GetMemberByName("disease"));

                            args1.updatedMembers.Add(co1.GetMemberByName("ageDiagnosis"));

                            co1.BackgroundPersistWork(args1);

                            pmh.Observations.AddToList(co1, args1);


                        }
                        break;
                    default:
                        break;
                }

            }

        
        
        }

        public void SaveSurvey(string unitnum, int apptid, ViewModels.FamilyHistoryRelative obj , int type )
        {

       
            SessionManager.Instance.SetActivePatient(unitnum, apptid);
            SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
            RiskApps3.Model.PatientRecord.Patient proband = SessionManager.Instance.GetActivePatient();
            proband.FHx.BackgroundListLoad();

            

            if (type == 0)
            {

                AddRelative(proband, obj);
            }
            else if (type == 1)
            {
                EditSurvey(proband, obj);

            }
            else
            {
                DeleteSurvey(proband, obj);
            }

            
         
        
        }

       

               

        public List<ViewModels.FamilyHistoryRelative> GetFamilyHistoryRelative(string unitnum, int apptid)
        {
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
            SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
            RiskApps3.Model.PatientRecord.Patient proband = SessionManager.Instance.GetActivePatient();

            RiskApps3.Model.PatientRecord.PastMedicalHistory pmh = new RiskApps3.Model.PatientRecord.PastMedicalHistory(proband);
            RiskApps3.Model.PatientRecord.FHx.FamilyHistory FHX = new RiskApps3.Model.PatientRecord.FHx.FamilyHistory(proband)  ;
            
            proband.FHx.BackgroundListLoad();
           
            /*
            RiskApps3.Model.PatientRecord.Person relative1 = new RiskApps3.Model.PatientRecord.Person(proband.FHx);
            relative1.BackgroundLoadWork();
             */
 
            //RiskApps3.Model.PatientRecord.FHx.FamilyHistory proband = SessionManager.Instance.GetActivePatient().FHx;


            List<ViewModels.FamilyHistoryRelative> lst = new List<VM.FamilyHistoryRelative>();

            
            //foreach (Person p in proband.FHx.Relatives)
            //foreach (Person p in FHX.Relatives)
            foreach (Person p in proband.FHx.Relatives)
            {
              

                ViewModels.FamilyHistoryRelative obj = new ViewModels.FamilyHistoryRelative();
                

                //Person relative = p;
                relative = p;

                obj.Relationship = "";

               // pmh = relative.PMH;

                if (string.IsNullOrEmpty(relative.bloodline) || relative.relationship.ToLower() == "mother" || relative.relationship.ToLower() == "father")
                {
                    if (relative.relationship.ToLower() != "other")
                        obj.Relationship = relative.relationship;
                }
                else
                {
                    if (relative.bloodline.ToLower() != "unknown" && relative.bloodline.ToLower() != "both")
                        obj.Relationship = relative.bloodline + " " + relative.relationship;
                    else
                        obj.Relationship = relative.relationship;
                }

                if (obj.Relationship.Length == 0)
                {
                    if (!string.IsNullOrEmpty(relative.relationshipOther))
                    {
                        obj.Relationship = relative.relationshipOther;
                    }
                }

                obj.RelativeAge = relative.age;

                obj.VitalStatus = relative.vitalStatus;

                obj.RelativeId = relative.relativeID;
                relative.PMH.BackgroundLoadWork();
                pmh = relative.PMH;



                obj.DeleteFlag = SetDeleteButtonFlag(relative);


                for (int i = 0; i < pmh.Observations.Count; i++)
                {
                    ClincalObservation co = (ClincalObservation)pmh.Observations[i];
                    switch (i)
                    {
                        case 0:

                            obj.FirstDx = co.disease;
                            obj.FirstAgeOnset= co.ageDiagnosis;
                            
                            break;
                        case 1:
                           obj.SecondDx = co.disease;
                            obj.SecondAgeOnset= co.ageDiagnosis;
                            break;
                        case 2:
                           obj.ThirdDx = co.disease;
                            obj.ThirdAgeOnset= co.ageDiagnosis;
                            break;
                        default:
                            break;
                    }
                }

                /**/
                lst.Add(obj);


            }



            return lst;
        
        }





        #region TestPatients

        /// <summary>
        /// for loading Test patient page
        /// </summary>
        /// <returns></returns>
        public VM.TestPatient LoadCreateTestPatients()
        {
            VM.TestPatient tp = new VM.TestPatient();
            TestPatientManager Tpm = new TestPatientManager();
            tp.Surveys = Tpm.GetSurveys();
            tp.Clinics = GetClinicList();
            tp.InitateTestPatients = Tpm.InitiateTestPatients();

            return tp;

        }

        /// <summary>
        /// FOr creating test patients 
        /// </summary>
        /// <param name="NoOfPatients"></param>
        /// <param name="dtAppointmentDate"></param>
        /// <param name="surveyID"></param>
        /// <param name="SurveyName"></param>
        /// <param name="clinicID"></param>
        public void CreateTestPatients(int NoOfPatients, string dtAppointmentDate, int surveyID, string SurveyName, int clinicID)
        {
            TestPatientManager Pm = new TestPatientManager();
            Pm.CreateTestPatients(NoOfPatients, dtAppointmentDate, surveyID, SurveyName, clinicID);

        }

        /// <summary>
        /// Delete test Patients permanently
        /// </summary>
        /// <param name="apptids"></param>
        public void DeleteTestPatientsByapptids(int[] apptids)
        {
            TestPatientManager Pm = new TestPatientManager();
            Pm.DeleteTestPatients(apptids);
        }
        /// <summary>
        /// To exclude patients as test patient
        /// </summary>
        /// <param name="apptids"></param>
        public void ExcludeTestPatientsByapptids(int[] apptids)
        {
            TestPatientManager Pm = new TestPatientManager();
            Pm.ExcludeTestPatients(apptids);

        }
        #endregion


    }
}

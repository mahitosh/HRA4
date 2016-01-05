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
using System.Collections.Specialized;
using RiskApps3.Model.PatientRecord.FHx;
using RiskApps3.Utilities;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using System.Runtime.Serialization;
using System.Xml.Linq;
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
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["InstitutionId"] != null)
            {
                _institutionId = Convert.ToInt32(HttpContext.Current.Session["InstitutionId"]);
            
            Institution inst = _repositoryFactory.TenantRepository.GetTenantById(_institutionId);
           
            string configTemplate = _repositoryFactory.SuperAdminRepository.GetAdminUser().ConfigurationTemplate;
            
            string configuration = Helpers.GetInstitutionConfiguration(configTemplate, inst.DbName);
                _hraSessionManager = new HraSessionManager(_institutionId.ToString(), configuration);
                _hraSessionManager.SetRaActiveUser(_username);
            }
            

            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            Logger.DebugFormat("Load Users");
            var users = SessionManager.Instance.MetaData.Users;// may cache user list.
            Logger.DebugFormat("User count :{0}",users.Count());
           // _user = users.FirstOrDefault(u => _username == u.GetMemberByName(_username).Name) as RAM.User;
            SessionManager.Instance.ActiveUser = users[0] as RAM.User;// need to change this.
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


        #region HL7
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mrn">MRN for a patient</param>
        /// <param name="apptId">Appointment Id of the selected appointment</param>
        /// <param name="Identified">True for DeIdentified and false for Identitify</param>
        /// <returns>Returns xml file for download.</returns>
        public VM.HraXmlFile ExportAsHL7(string mrn, int apptId, bool Identified)
        {

            string rootPath = HttpContext.Current.Server.MapPath(@"~/App_Data/RAFiles/");

            _hraSessionManager.SetActivePatient(mrn, apptId);
            Patient _patient = _hraSessionManager.GetActivePatient();
            _patient.LoadFullObject();
            FamilyHistory theFH = _patient.owningFHx;
            string fhAsString = TransformUtils.DataContractSerializeObject<FamilyHistory>(theFH);
            XmlDocument inDOM = new XmlDocument();
            inDOM.LoadXml(fhAsString);


            XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, rootPath, @"hra_to_ccd_remove_namespaces.xsl");
            XmlDocument hl7FHData = TransformUtils.performTransformWithParam(resultXmlDoc, rootPath, @"hra_serialized_to_hl7.xsl", "deIdentify", Identified ? "1" : "0");


            string filename = SessionManager.Instance.GetActivePatient().name + " " +
                SessionManager.Instance.GetActivePatient().unitnum +
                " HL7 " +
                DateTime.Now.ToString("yyyy-MM-dd-HHmm");
            string filePath = System.IO.Path.Combine(rootPath, "Download", filename);
            hl7FHData.Save(filePath);

            VM.HraXmlFile xmlFile = new VM.HraXmlFile();
            xmlFile.FileName = filename;
            xmlFile.FilePath = filePath;
            xmlFile.Estension = ".xml";
            return xmlFile;
        }
                
        public void ImportHL7(VM.HraXmlFile xmlFile, string mrn, int apptId)
        {
            Appointment.DeleteApptData(apptId, true);
            string rootPath = HttpContext.Current.Server.MapPath(@"~/App_Data/RAFiles/");
            string riskMeanings = File.ReadAllText(Path.Combine(rootPath, "riskMeanings.xml"));
            string HL7Relationships = File.ReadAllText(Path.Combine(rootPath, "HL7Relationships.xml"));
            string hl7 = File.ReadAllText(xmlFile.FilePath);
            bool is_SG_XML = testSGDoc(xmlFile.FilePath);

            if (is_SG_XML)
            {
                //transform it
                XmlDocument inDOM = new XmlDocument();
                inDOM.LoadXml(hl7);               
                XmlDocument result_SG_XmlDoc = TransformUtils.performTransform(inDOM, rootPath, @"sg_to_hl7.xsl");
                hl7 = result_SG_XmlDoc.InnerXml;
            }
            Patient p = Patient.processHL7Import(apptId, hl7, riskMeanings, HL7Relationships);
            
            if (string.IsNullOrEmpty(p.name))
            {
                SessionManager.Instance.SetActivePatient(mrn, apptId);
                Patient patient = SessionManager.Instance.GetActivePatient();
                if (string.IsNullOrEmpty(patient.name) == false)
                {
                    p.name = patient.name;
                }
            }

            p.PersistFullObject(new HraModelChangedEventArgs(null));
        }

        private bool testSGDoc(string fileName) // Silicus: Created a copy of this method in AppointmentService class.
        {
            //test if is Surgeon General XML file
            XmlDocument suspectSGDoc = new XmlDocument();
            suspectSGDoc.Load(fileName);
            XPathNavigator sgNav = suspectSGDoc.CreateNavigator();
            string str = sgNav.Evaluate("string(FamilyHistory/methodCode[1]/@displayName)").ToString();

            return (!String.IsNullOrEmpty(str) && str.StartsWith("Surgeon General")); //presumed Surgeon General XML file
        }

        #endregion

        #region XML       

        public VM.HraXmlFile ExportAsXml(string mrn, int apptId, bool Identified)
        {
            string rootPath = HttpContext.Current.Server.MapPath(@"~/App_Data/RAFiles/");
            _hraSessionManager.SetActivePatient(mrn, apptId);
            Patient _patient = _hraSessionManager.GetActivePatient();
            _patient.LoadFullObject();
            FamilyHistory theFH = _patient.owningFHx;
            string fileName = SessionManager.Instance.GetActivePatient().name + " " +
                SessionManager.Instance.GetActivePatient().unitnum +
                " Serialization " +
                DateTime.Now.ToString("yyyy-MM-dd-HHmm");
            string filePath = System.IO.Path.Combine(rootPath, "Download", fileName);
            if (!Identified)
            {
                //legacy code chunk; written before caring about de-identifying
                DataContractSerializer ds = new DataContractSerializer(typeof(FamilyHistory));
                FileStream stm = new FileStream(filePath, FileMode.Create);
                ds.WriteObject(stm, theFH);
                stm.Flush();
                stm.Position = 0;
                stm.Close();
            }

            string fhAsString = TransformUtils.DataContractSerializeObject<FamilyHistory>(theFH);

            //transform it
            XmlDocument inDOM = new XmlDocument();
            inDOM.LoadXml(fhAsString);


            XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, rootPath, @"hraDeIdentifySerialized.xsl");

            //following actually removes all indentation and extra whitespace; prefer to save the file with indentations, so leave this commented
            //hl7FHData.PreserveWhitespace = true;
            resultXmlDoc.Save(fileName);

            VM.HraXmlFile xmlFile = new VM.HraXmlFile()
            {
                FileName = fileName,
                FilePath = filePath,
                Estension = ".xml"
            };
            return xmlFile;
        }

        public void ImportXml(VM.HraXmlFile xmlFIle, string mrn, int apptId)
        {

            DataContractSerializer ds = new DataContractSerializer(typeof(FamilyHistory));
            string filePath = xmlFIle.FilePath;
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            FamilyHistory fhx;
            try
            {
                fhx = (FamilyHistory)ds.ReadObject(fs);
            }
            catch (Exception e)  //catch exception where cdsBreastOvary data is older version
            {
                fs.Flush();
                fs.Position = 0;
                XDocument doc;
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    doc = XDocument.Load(reader);
                }

                doc.XPathSelectElement("//*[local-name() = 'cdsBreastOvary']").Remove();

                var xmlDocument = new XmlDocument();
                using (var xmlReader = doc.CreateReader())
                {
                    xmlDocument.Load(xmlReader);
                }

                MemoryStream ms = new MemoryStream();
                xmlDocument.Save(ms);
                ms.Flush();
                ms.Position = 0;
                fhx = (FamilyHistory)ds.ReadObject(ms);
            }

            foreach (Person p in fhx)
            {
                if (p is Patient)
                {
                    fhx.proband = (Patient)p;
                }
            }
            fhx.proband.apptid = apptId;
            Appointment.DeleteApptData(apptId, true);
            if (fhx.proband.unitnum == null)  //no unitnum happens when importing from de-identified XML
            {
                fhx.proband.unitnum = mrn;  //just continue to use the existing unitnum for the appt we're overwriting
            }
            Appointment.UpdateAppointmentUnitnum(apptId, fhx.proband.unitnum);
            fhx.proband.PersistFullObject(new HraModelChangedEventArgs(null));

            fs.Close();

           
        }
        #endregion

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
                foreach (PtFollowup followup in fList)
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


    }
}

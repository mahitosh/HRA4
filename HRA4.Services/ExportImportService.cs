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
    public class ExportImportService:Interfaces.IExportImportService
    {
         IRepositoryFactory _repositoryFactory;
        IHraSessionManager _hraSessionManager;

        public ExportImportService(IRepositoryFactory repositoryFactory, IHraSessionManager hraSessionManger)
        {
            _repositoryFactory = repositoryFactory;
            _hraSessionManager = hraSessionManger;  
        }

        #region HL7
        /// <summary>
        /// Export patient details as xml.
        /// </summary>
        /// <param name="mrn">MRN for a patient</param>
        /// <param name="apptId">Appointment Id of the selected appointment</param>
        /// <param name="Identified">True for DeIdentified and false for Identitify</param>
        /// <returns>Returns xml file for download.</returns>
        public VM.HraXmlFile ExportAsHL7(string mrn, int apptId, bool Identified)
        {            
            string rootPath = HttpContext.Current.Server.MapPath(Constants.RAFilePath);
             
            _hraSessionManager.SetActivePatient(mrn, apptId);
            Patient _patient = SessionManager.Instance.GetActivePatient();
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
            if(!Directory.Exists(System.IO.Path.Combine(rootPath, "Download")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(rootPath, "Download"));
            }

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
            string rootPath = HttpContext.Current.Server.MapPath(Constants.RAFilePath);
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
            string rootPath = HttpContext.Current.Server.MapPath(Constants.RAFilePath);
            _hraSessionManager.SetActivePatient(mrn, apptId);
            Patient _patient = SessionManager.Instance.GetActivePatient();
            _patient.LoadFullObject();
            FamilyHistory theFH = _patient.owningFHx;
            string fileName = SessionManager.Instance.GetActivePatient().name + " " +
                SessionManager.Instance.GetActivePatient().unitnum +
                " Serialization " +
                DateTime.Now.ToString("yyyy-MM-dd-HHmm");

            if (!Directory.Exists(System.IO.Path.Combine(rootPath, "Download")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(rootPath, "Download"));
            }
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
            else
            {
                string fhAsString = TransformUtils.DataContractSerializeObject<FamilyHistory>(theFH);

                //transform it
                XmlDocument inDOM = new XmlDocument();
                inDOM.LoadXml(fhAsString);

                XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, rootPath, @"hraDeIdentifySerialized.xsl");

                //following actually removes all indentation and extra whitespace; prefer to save the file with indentations, so leave this commented
                //hl7FHData.PreserveWhitespace = true;
                resultXmlDoc.Save(filePath);
            }
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
            _hraSessionManager.SetActivePatient(mrn, apptId);
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
            //The below line is commented out as the new library doesn't have any such method.
           // Appointment.UpdateAppointmentUnitnum(apptId, fhx.proband.unitnum); 
            fhx.proband.PersistFullObject(new HraModelChangedEventArgs(null));

            fs.Close();


        }
        #endregion
    }
}

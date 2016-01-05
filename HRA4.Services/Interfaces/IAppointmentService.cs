using RiskApps3.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = HRA4.ViewModels;

namespace HRA4.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<VM.Appointment> GetAppointments(int InstitutionId);
        List<VM.Appointment> GetAppointments(int InstitutionId,NameValueCollection Collection);
        void SaveAppointments(VM.Appointment Appt, int InstitutionId);

        VM.HraXmlFile ExportAsXml(string mrn, int apptId, bool Identified);
       
        VM.HraXmlFile ExportAsHL7(string mrn, int apptId, bool Identified);       

        void ImportXml(VM.HraXmlFile xmlFIle, string mrn,int apptId);
        void ImportHL7(VM.HraXmlFile xmlFIle, string mrn, int apptId);

        void AddTasks(int _institutionId, string unitnum, int apptid);
       
        void DeleteTasks(int _institutionId, string unitnum, int apptid);
    }
}

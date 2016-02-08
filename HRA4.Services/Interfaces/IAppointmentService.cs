using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
        VM.Appointment GetAppointment(int InstitutionId, NameValueCollection searchfilter, string apptid);
        void SaveAppointments(VM.Appointment Appt, int InstitutionId);
        void DeleteAppointment(int InstitutionId, int apptid, bool flag);
        FileInfo RunAutomationDocuments(int InstitutionId, int apptid, string MRN);

        void AddTasks(int _institutionId, string unitnum, int apptid);
       
        void DeleteTasks(int _institutionId, string unitnum, int apptid);
        List<VM.Clinic> GetClinics(int InstitutionId);
        string ShowPedigreeImage(int _institutionId, string unitnum, int apptid, string PedigreeImagePath);
        VM.RiskScore RiskScore(int apptid, string MRN);
        VM.RiskScore RiskCalculateAndRunAutomation(int apptid, string MRN);
        VM.AuditReports GetAuditReports(string MRN, string startdate, string enddate);
        void CreateTestPatients(int NoOfPatients, string dtAppointmentDate, int surveyID, string SurveyName, int clinicID);
        VM.TestPatient LoadCreateTestPatients();
        void DeleteTestPatientsByapptids(int[] apptids);
        void ExcludeTestPatientsByapptids(int[] apptids);
        VM.Appointment GetAppointmentForAdd(string MRN, int clinicId);
        VM.Appointment GetAppointmentForCopy(string ApptId, int InstitutionId, NameValueCollection searchfilter);
        List<ViewModels.FamilyHistoryRelative> GetFamilyHistoryRelative(string unitnum, int apptid);
        void SaveSurvey(string unitnum, int apptid, ViewModels.FamilyHistoryRelative obj,  int type);
        Patient CalculateRiskAndRunAutomation(int apptid, string MRN);
    }
}

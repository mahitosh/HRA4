using HRA4.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services.Interfaces
{
    public interface ITestPatientManager
    {
        void CreateTestPatients(int NoOfPatients, string dtAppointmentDate, int surveyID, string SurveyName, int clinicID);
        string createTestUnitnum();
        List<Survey> GetSurveys();
        List<Survey> getSurveyList(string commandString);
        List<TestPatientAppointment> InitiateTestPatients();
        void DeleteTestPatients(int[] apptids);
        void ExcludeTestPatients(int[] apptids);
    }
}

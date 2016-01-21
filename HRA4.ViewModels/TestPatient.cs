using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class TestPatient
    {
        public TestPatient()
        {
            Clinics = new List<Clinic>();
            Surveys = new List<Survey>();
            InitateTestPatients = new List<TestPatientAppointment>();

        }
        public List<Clinic> Clinics { get; set; }
        public List<Survey> Surveys { get; set; }
        public List<TestPatientAppointment> InitateTestPatients { get; set; }
    }
    public class Survey
    {
        public int Id { get; set; }
        public string  SurveyName { get; set; }
    }
    public class TestPatientAppointment 
    {
        public int apptID;
        public string MRN;
        public string patientName;
        public string DOB;
        public DateTime apptdatetime;
    }

}

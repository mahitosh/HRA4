using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class Appointment
    {
        public Appointment()
        {
           Providers = new List<Provider>();
        }
        DateTime appointmentDate;
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string MRN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }

        public string Provider { get; set; }
     

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCompleted { get; set; }

        public string DiseaseHx { get; set; }
        public string Survey { get; set; }
        public bool DoNotCall { get; set; }
        public bool MarkAsComplete
        {
            get
            {
                if (DateCompleted == DateTime.MinValue || DateCompleted == null)
                {
                    return false;

                }
                else return true;
            }
            set { }

        }
        public bool SetMarkAsComplete { get; set; }
        public List<string> appttimes { get { return new List<string>() { "7:00 AM", "7:15 AM" }; } set { } }
        public string appttime { get; set; }
        public List<Clinic> clinics { get; set; }
        public int clinicID { get; set; }

        public string Patient_Comment { get; set; }
        public string AssessmentName { get; set; }
        public List<string> Languages { get { return new List<string>() { "English", "Hindi","French" }; } set { } }
        public string Language { get { return "French"; } set { } }
        public List<string> Races { get { return new List<string>() { "Asian", "Black" }; } set { } }
        public string Race { get; set; }
        public List<string> Nationalities { get { return new List<string>() { "Indian", "American" }; } set { } }
        public string Nationality { get; set; }
        public string Occupation { get; set; }
        public string Education { get; set; }
        public string Maritalstatus { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public List<string> States { get { return new List<string>() { "AK", "AR" }; } set { } }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<string> Countries { get { return new List<string>() { "India", "US" }; } set { } }
        public string Country { get; set; }
        public string Homephone { get; set; }
        public string Workphone { get; set; }
        public string Cellphone { get; set; }
        public string EmailAddress { get; set; }
        public List<string> Genders { get { return new List<string>() { "M", "F", "U" }; } set { } }
        public string Gender { get; set; }
        public List<string> AppointmentPhysicians { get { return new List<string>() { "A", "B", "U" }; } set { } }
        public string AppointmentPhysician { get; set; }
        public List<Provider> Providers { get; set; }
        //public List<ProviderType> ProviderTypes { get; set; }
        public int RefPhysician { get; set; }
        public int PCP { get; set; }
        public string DisplayHeaderMenus { get; set; }
        public string IsGoldenAppointment { get; set; }
        public string IsCopyAppointment { get; set; }
    }

    public class ListOfProvider
    {
        public string providerIDString {get;set;}
        public string role{get;set;}
        public bool refPhys{get;set;}
        public bool PCP { get; set; }
    }
    public class Provider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
    }
    public class ProviderType
    {
        public int Id { get; set; }
        public string ProviderTypeName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
using RA = RiskApps3.Model.PatientRecord;
using RAM = RiskApps3.Model.MetaData;
using RAT = RiskApps3.Model.PatientRecord.Communication;
using HRA4.Utilities;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
namespace HRA4.Mapper
{
    public static class AppointmentMapper
    {
        public static RA.Patient  ToRAPatient(this Appointment appt)
        {
            return new RA.Patient(appt.MRN)
            {               
                address1 = appt.Address1 ,
                address2=appt.Address2,
                cellphone=appt.Cellphone,
                city=appt.City,
                educationLevel=appt.Education,
                emailAddress=appt.EmailAddress,
                gender=appt.Gender,
                homephone=appt.Homephone,
                maritalstatus=appt.Maritalstatus,
                //Nationality=appt.Nationality,
                occupation=appt.Occupation,
                comment = appt.Patient_Comment,                  
                state=appt.State,                
                workphone=appt.Workphone,
                zip=appt.Zip,
                relativeID = 1,
               
                //unitnum = appt.MRN,
               // apptid = appt.Id,
                owningFHx = new RA.FHx.FamilyHistory(){
                    proband = new RA.Patient()
                    {
                        unitnum = appt.MRN,
                        apptid = appt.Id,
                        relativeID = 1
                    }
                }
               

            };
        }
        public static RA.Appointment ToRAppointment(this Appointment appointment)
        {
            return new RA.Appointment()
            {
                apptID = appointment.Id,
                apptdate = appointment.AppointmentDate.ToString(),
                unitnum = appointment.MRN,
                dob = appointment.DateOfBirth.ToString(),
                appttime=appointment.appttime,
                Apptphysname=appointment.AppointmentPhysician,
                AssessmentName=appointment.AssessmentName,
                clinicID=appointment.clinicID,
                clinicName="",
                gender=appointment.Gender,
                language=appointment.Language,
                nationality=appointment.Nationality,
                race=appointment.Race,
                surveyType=appointment.Survey,
                Unitnum=appointment.MRN,
                apptphysname = appointment.Provider,
                diseases = appointment.DiseaseHx,
                patientname = appointment.PatientName,
               

            };
        }
        public static Appointment FromRAppointment(this RA.Appointment app, bool dnc = false)
        {
            return new Appointment()
            {
                Id = app.apptID,
                MRN = app.unitnum,
                AppointmentDate = app.apptdatetime,
                DateOfBirth = app.dob.ToDateTime(),
                DiseaseHx = app.diseases,
                Provider = app.apptphysname,
                PatientName = app.patientname,
                DateCompleted = app.riskdatacompleted,
                Survey = app.surveyType,
                DoNotCall = dnc,
                appttime = app.appttime,
                clinicID = app.clinicID,
               
            };
        }

        public static TestPatientAppointment FromRATestAppointment(this TestAppointment app)
        {
            return new TestPatientAppointment()
            {
                apptID = app.apptID,
                MRN = app.unitnum,
                patientName = app.patientName,
                DOB = app.DOB,
                apptdatetime = app.apptdatetime

            };
        }

        public static RAT.Task ToRATTasks(this Tasks tasks)
        {
            return new RAT.Task()
            {
                taskID = tasks.taskID
            };
        }



        public static RAM.Clinic ToRAMClinic(RAM.Clinic clinics)
        {
            return new RAM.Clinic()
            {
                clinicID = clinics.clinicID,
                clinicName = clinics.clinicName
            };
        }

        public static Appointment FromRAppointment(this Appointment appt, RA.Patient patient)
        {
            appt.Address1 = patient.address1;
            appt.Address2 = patient.address2;
            appt.Cellphone = patient.cellphone;
            appt.City = patient.city;
            appt.Country = patient.country;
            appt.Education = patient.educationLevel;
            appt.EmailAddress = patient.emailAddress;
            appt.Gender = patient.gender;
            appt.Homephone = patient.homephone;
            appt.Maritalstatus = patient.maritalstatus;
            appt.Occupation = patient.occupation;
            appt.Patient_Comment = patient.comment;//mapping needs to verify

            appt.RefPhysician = GetrefPhysProviderId(patient);
            appt.PCP = GetrefPCPProviderId(patient);
            appt.State = patient.state;
            appt.Workphone = patient.workphone;
            appt.Zip = patient.zip;

            return appt;

        }

        private static int GetrefPhysProviderId(RA.Patient patient)
        {
            if (patient.Providers.Count > 0 && patient.Providers.FirstOrDefault(x => x.refPhys == true) != null)
                return patient.Providers.FirstOrDefault(x => x.refPhys == true).providerID;
            else return 0;
        }
        private static int GetrefPCPProviderId(RA.Patient patient)
        {
            if (patient.Providers.Count > 0 && patient.Providers.FirstOrDefault(p => p.PCP == true) != null)
                return patient.Providers.FirstOrDefault(p => p.PCP == true).providerID;
            else return 0;
        }
        private static List<ViewModels.Provider> GetProviders(ProviderList providerList)
        {
            List<ViewModels.Provider> provider = new List<ViewModels.Provider>();
            foreach (var p in providerList)
            {
               
                provider.Add(new ViewModels.Provider()
                            {
                                //PCP = p.PCP,
                                //providerIDString=p.providerID.ToString(),
                                //refPhys=p.refPhys,
                                //role=p.defaultRole
                            });
            }
            return provider;
        }


        public static Appointment FromRAppointment(this RA.Appointment app)
        {
            return new Appointment()
            {
                Id = app.apptID,
                MRN = app.unitnum,
                AppointmentDate = app.apptdatetime,
                DateOfBirth = app.dob.ToDateTime(),
                DiseaseHx = app.diseases,
                AppointmentPhysician = app.apptphysname,
                PatientName = app.patientname,
                DateCompleted = app.riskdatacompleted,
                Survey = app.surveyType,
                appttime = app.appttime,
                AssessmentName = app.AssessmentName,
                clinicID = app.clinicID,
                Language = app.language,
                Race = app.race,
                Nationality = app.nationality
            };
        }

        public static Clinic FromRClinics(this RAM.Clinic app)
        {
            return new Clinic()
            {
                clinicID = app.clinicID,
                clinicName = app.clinicName

            };
        }


        public static Tasks FromRATTasks(this RAT.Task app)
        {

            return new Tasks()
            {
                taskID = app.taskID

            };
        }

        private static DateTime? CheckForNullAndMin(DateTime dtDate)
        {
            if (dtDate == null || dtDate == DateTime.MinValue)
                return null;
            else
                return dtDate;
        }

        public static List<Appointment> FromRAppointmentList(this List<RA.Appointment> apps)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (var app in apps)
            {
                appointments.Add(app.FromRAppointment());
            }
            return appointments;
        }

        public static List<Appointment> FromRAppointmentList(this RiskApps3.Model.Clinic.AppointmentList apps)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (RA.Appointment app in apps)
            {
                appointments.Add(app.FromRAppointment());
            }
            return appointments;
        }

        public static List<Clinic> FromRClinicList(this RiskApps3.Model.MetaData.ClinicList apps)
        {
            List<Clinic> clinics = new List<Clinic>();
            foreach (RAM.Clinic app in apps)
            {
                clinics.Add(app.FromRClinics());
            }
            return clinics;
        }


        public static List<HRA4.ViewModels.Tasks> FromRATaskList(this RiskApps3.Model.PatientRecord.Communication.TaskList apps)
        {
            List<HRA4.ViewModels.Tasks> tasks = new List<HRA4.ViewModels.Tasks>();

            foreach (RAT.Task app in apps)
            {
                tasks.Add(app.FromRATTasks());
            }
            return tasks;
        }



    }
}

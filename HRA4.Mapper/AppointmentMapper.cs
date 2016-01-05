using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
using RA=RiskApps3.Model.PatientRecord;
using RAM = RiskApps3.Model.MetaData;
using RAT=RiskApps3.Model.PatientRecord.Communication;
using HRA4.Utilities;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
namespace HRA4.Mapper
{
    public static class AppointmentMapper
    {
        public static RA.Appointment ToRAppointment(this Appointment appointment)
        {
            return new RA.Appointment()
            {
                apptID = appointment.Id,
                apptdate = appointment.AppointmentDate.ToString(),
                unitnum = appointment.MRN,
                dob = appointment.DateOfBirth.ToString(),
               // apptdatetime = appointment.AppointmentDate?appointment.AppointmentDate:DateTime.MinValue,
                apptphysname = appointment.Provider,
                diseases= appointment.DiseaseHx,
                patientname = appointment.PatientName,
              
            };
        }
        public static Appointment FromRAppointment(this RA.Appointment app, bool dnc=false)
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
                DoNotCall = dnc
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
                clinicName=clinics.clinicName
            };
        }

        public static Appointment FromRAppointment(this RA.Appointment app)
        {
            return new Appointment()
            {
                Id = app.apptID,
                MRN = app.unitnum,
                AppointmentDate = app.apptdatetime,
                DateOfBirth = app.dob.ToDateTime(),
                DiseaseHx= app.diseases,
                Provider = app.apptphysname,
                PatientName = app.patientname,
                DateCompleted = app.riskdatacompleted,
               Survey = app.surveyType,               
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
            foreach(var app in apps)
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

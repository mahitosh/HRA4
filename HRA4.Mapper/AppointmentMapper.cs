using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
using RA=RiskApps3.Model.PatientRecord;
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
                apptdatetime = appointment.AppointmentDate,
                apptphysname = appointment.Provider,
                diseases= appointment.DiseaseHx,
                patientname = appointment.PatientName
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
                DateCompleted = app.riskdatacompleted
            };
        }

        //private DateTime CheckForNullAndMin(DateTime dtDate)
        //{
        //    if (dtDate == null || dtDate == DateTime.MinValue)
        //        return null;
        //}

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

       
    }
}

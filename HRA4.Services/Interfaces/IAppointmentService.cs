﻿using RiskApps3.Model;
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
        void SaveAppointments(VM.Appointment Appt, int InstitutionId);
        void DeleteAppointment(int InstitutionId,int apptid);
        FileInfo RunAutomationDocuments(int InstitutionId, int apptid, string MRN);

        void AddTasks(int _institutionId, string unitnum, int apptid);
       
        void DeleteTasks(int _institutionId, string unitnum, int apptid);
        List<VM.Clinic> GetClinics(int InstitutionId);
        string ShowPedigreeImage(int _institutionId, string unitnum, int apptid, string PedigreeImagePath);
    }
}

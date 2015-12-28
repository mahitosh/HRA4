using RiskApps3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM=HRA4.ViewModels;

namespace HRA4.Services.Interfaces
{
    public interface IAppointmentService
    {
        List<VM.Appointment> GetAppointments(int InstitutionId);

    }
}

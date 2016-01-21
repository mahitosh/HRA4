using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace HRA4.Services.Interfaces
{
    public interface IRiskClinicServices
    {
        DataTable GetPatients(int ClinicID);
        List<ViewModels.HighRiskLifetimeBreast> GetPatients();
        Entities.PatientDetails GetPatientDetails(string unitnum, int apptid);

    }
}

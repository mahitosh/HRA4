using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services.Interfaces
{
    public interface IServiceContext
    {
        IAppointmentService AppointmentService { get; }
        IUserService UserService { get; }
        IAdminService AdminService { get; }
        IExportImportService ExportImportService { get; }

    }
}

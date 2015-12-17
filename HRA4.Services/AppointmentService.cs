using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services
{
   public class AppointmentService:IAppointmentService
    {
       User _user;
       public AppointmentService(User user)
       {
           _user = user;
           SessionManager.Instance.ActiveUser = user;
       }

        public List<HraObject> GetAppointments()
        {
            var list = new AppointmentList();
            list.Date = null;
            list.clinicId = 1;
            list.BackgroundListLoad();
            return list;
        }
    }
}

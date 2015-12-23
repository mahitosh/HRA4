using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RAM = RiskApps3.Model.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = HRA4.ViewModels;
using HRA4.Mapper;
namespace HRA4.Services
{
    public class AppointmentService : IAppointmentService
    {
        string _username;
        RAM.User _user;
        public AppointmentService(string user)
        {
            _username = user;
        }

        public List<VM.Appointment> GetAppointments()
        {
            SetUserSession();
            var list = new AppointmentList();
            list.Date = null;
            list.clinicId = 1;
            list.BackgroundListLoad();             
            return list.FromRAppointmentList();
        }

        /// <summary>
        /// Initialize session as per selected institution.
        /// </summary>
        private void SetUserSession()
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            var users = SessionManager.Instance.MetaData.Users;// may cache user list.

            _user = users.FirstOrDefault(u => _username == u.GetMemberByName(_username).Name) as RAM.User;
            SessionManager.Instance.ActiveUser = users[0] as RAM.User;// need to change this.
        }


    }
}

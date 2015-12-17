using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services
{
    public class UserService:IUserService
    {

        public List<RiskApps3.Model.HraObject> GetUsers()
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            return SessionManager.Instance.MetaData.Users;            
        }
    }
}

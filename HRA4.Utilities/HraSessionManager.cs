using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Utilities;
using RiskApps3.Controllers;

namespace HRA4.Utilities
{
    public class HraSessionManager
    {
        public HraSessionManager(string InstitutionId,string config)
        {
            Cache.SetCache<string>(InstitutionId, config);     
      
        }
        public void SetRaConfiguration(string config)
        {
            Cache.SetCache<string>("", config);
        }

        public void SetRaActiveUser(string user)
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            var users = SessionManager.Instance.MetaData.Users;
           // SessionManager.Instance.ActiveUser = users[0] as RAM.User;

        }
    }
}

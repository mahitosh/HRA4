using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.Security
{
    public class SecurityContext
    {
        public string user;
        public SecurityContext(string p_user)
        {
            user = p_user;
        }
    }
}

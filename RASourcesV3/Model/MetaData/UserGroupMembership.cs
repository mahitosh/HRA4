using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.MetaData
{
    public class UserGroupMembership : HraObject
    {
        [HraAttribute] public string userLogin;        
        [HraAttribute] public string userGroup;
    }
}

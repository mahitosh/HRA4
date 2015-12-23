using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
 
using HRA4.Utilities;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RA=RiskApps3.Model.MetaData;
namespace HRA4.Mapper
{
    public static class UserMapper
    {
        public static RA.User ToRAppointment(this User user)
        {
            return new RA.User()
            {
                userLogin = user.Username,
                userFirstName = user.FirstName,
                userLastName= user.LastName,
                
            };
        }

    }
}

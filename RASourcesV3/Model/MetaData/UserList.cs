using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class UserList : HRAList <User>
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public UserList()
        {

            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs("sp_3_LoadUserList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

            foreach (User u in this)
            {
                if (string.IsNullOrEmpty(u.userFullName))
                {
                    u.User_userFullName = (u.userFirstName + " " + u.userLastName).Trim();
                }
            }

        }

        public bool IsUserInList(string login)
        {
            bool retval = false;
            foreach(User u in this)
            {
                if (u.userLogin.ToLower() == login.ToLower())
                {
                    retval = true;
                    break;
                } 
            }
            return retval;
        }

        public User GetUser(string login) // Silicus: Changed the method from internal to public.
        {
            User retval = null;
            foreach (User u in this)
            {
                if (u.userLogin.ToLower() == login.ToLower())
                {
                    retval = u;
                    break;
                }
            }
            return retval;
        }
    }
}

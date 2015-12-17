using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class ClinicList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public string user_login = "";

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("user_login", user_login);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadUserClinicList",
                                                pc,
                                                typeof(Clinic),
                                                constructor_args);
            DoListLoad(lla);

        }
    }

    public class Clinic : HraObject
    {
        [HraAttribute]
        public int clinicID;
        [HraAttribute]
        public string clinicName;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(clinicName))
                return "";
            else
                return clinicName;
        }
    }
}

/*
public class UserGroupList : HRAList
{


    public UserGroupList()
    {

        constructor_args = new object[] { };
    }

    public override void BackgroundListLoad()
    {
        pc.Clear();

        LoadListArgs lla = new LoadListArgs("sp_3_LoadUserGroupList",
                                            pc,
                                            typeof(UserGroupMembership),
                                            constructor_args);
        DoListLoad(lla);

    }

}

*/
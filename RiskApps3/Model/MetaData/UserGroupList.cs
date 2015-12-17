using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class UserGroupList: HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

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
}

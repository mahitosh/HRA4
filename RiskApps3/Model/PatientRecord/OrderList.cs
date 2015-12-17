using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord
{
    public class PtOrderList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public String userLogin;
        public String groupName;
        public DateTime? orderDate;

        public PtOrderList()
        {
            constructor_args = new object[] {};
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);
            if (!String.IsNullOrEmpty(groupName))
                pc.Add("groupName", groupName);
            if (orderDate != null)
                pc.Add("orderDate", orderDate);
            //if (orderDate != null)
                pc.Add("unitnum", SessionManager.Instance.GetActivePatient().unitnum);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadPtOrders",
                                                pc,
                                                typeof(Order),
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

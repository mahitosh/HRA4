using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    public class PatientsByGroup : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        string userLogin = null;
        string groupName = null;

        public PatientsByGroup(string userLogin, string groupName)
        {
            this.constructor_args = new object[] { };
            this.pc = new ParameterCollection();

            this.userLogin = userLogin;
            this.groupName = groupName;
        }

        public override void BackgroundListLoad()
        {
            //pc.Clear();
            //if (!String.IsNullOrEmpty(this.userLogin))
            //{
            //    pc.Add("userLogin", this.userLogin);
            //}
            //if (!String.IsNullOrEmpty(this.groupName))
            //{
            //    pc.Add("groupName", this.groupName);
            //}

            //LoadListArgs lla = new LoadListArgs(
            //    "sp_3_LoadPatientsByGroup",
            //    this.pc,
            //    typeof(string),
            //    this.constructor_args);

            //DoListLoad(lla);
        }

        internal bool Contains(string mrn)
        {
            return this.Contains(mrn);
        }
    }
}

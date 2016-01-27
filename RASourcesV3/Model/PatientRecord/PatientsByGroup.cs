using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    public class PatientsByGroup : HRAList<HraObject>   //TODO is this really used?
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

        internal bool Contains(string mrn)
        {
            return this.Contains(mrn);
        }
    }
}

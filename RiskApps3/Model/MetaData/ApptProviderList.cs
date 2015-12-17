using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Model.Clinic;

namespace RiskApps3.Model.MetaData
{
    public class ApptProviderList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public ApptProviderList()
        {

            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs("sp_3_LoadApptProviders",
                                                pc,
                                                typeof(Provider),
                                                constructor_args);
            DoListLoad(lla);

        }

        public bool IsProviderInList(int providerID)
        {
            bool retval = false;
            foreach(Provider u in this)
            {
                if (u.providerID == providerID)
                {
                    retval = true;
                    break;
                } 
            }
            return retval;
        }

        internal Provider GetProvider(int providerID)
        {
            Provider retval = null;
            foreach (Provider u in this)
            {
                if (u.providerID == providerID)
                {
                    retval = u;
                    break;
                }
            }
            return retval;
        }
    }
}

using System.Linq;
using RiskApps3.Model.Clinic;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    /// <summary>
    /// A collection of all <code>Provider</code> records who are flagged as 'appointment providers'.
    /// </summary>
    public class ApptProviderList : HRAList<Provider>
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

            //gets lkpProviders
            LoadListArgs lla = new LoadListArgs("sp_3_LoadApptProviders",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }

        public bool IsProviderInList(int providerID)
        {
            return this.Any(u => u.providerID == providerID);
        }

        internal Provider GetProvider(int providerId)
        {
            return this.FirstOrDefault(u => u.providerID == providerId);
        }
    }
}

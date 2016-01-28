using System.Runtime.Serialization;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic
{
    /// <summary>
    /// A collection of all <code>Provider</code> objects associcated with the provided <code>Patient</code>.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(Provider))]
    public class ProviderList : HRAList<Provider>
    {
        private ParameterCollection pc = new ParameterCollection();
        public Patient proband;
        private object[] constructor_args;

        public ProviderList() { } // Default constructor for serialization

        public ProviderList(Patient patient)
        {
            proband = patient;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", proband.unitnum);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadProviderList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }

        /// <summary>
        /// This override causes the stored proc to map these Providers 
        /// to the <code>Patient</code> associated with this List.
        /// </summary>
        /// <param name="e"></param>
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            if (proband!= null)
            {
                if (proband.apptid > 0)
                {
                    foreach (Provider ho in this)
                    {
                        ho.apptid = proband.apptid;
                    }
                }
            }

            base.PersistFullList(e);
        }
    }
}

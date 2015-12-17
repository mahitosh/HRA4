using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using System.Runtime.Serialization;

using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.Clinic
{
    [CollectionDataContract]
    [KnownType(typeof(Provider))]
    public class ProviderList : HRAList
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
                                                typeof(Provider),
                                                constructor_args);
            DoListLoad(lla);
        }
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            if (proband!= null)
            {
                if (proband.apptid > 0)
                {
                    foreach (HraObject ho in this)
                    {
                        Provider p = (Provider)ho;
                        p.apptid = proband.apptid;
                    }
                }
            }

            base.PersistFullList(e);
        }
    }
}

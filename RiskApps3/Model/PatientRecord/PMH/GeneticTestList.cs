using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [CollectionDataContract]
    [KnownType(typeof(GeneticTest))]
    public class GeneticTestList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        
        [DataMember] public PastMedicalHistory OwningPMH;

        private object[] constructor_args;

        /*****************************************************/
        public GeneticTestList() { } // Default constructor for serialization

        public GeneticTestList(PastMedicalHistory pmh)
        {
            OwningPMH = pmh;
            constructor_args = new object[] { OwningPMH };
        }

        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                ((GeneticTest)o).owningPMH = OwningPMH;
            }
            base.PersistFullList(e);
        }

        /*****************************************************/
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
            pc.Add("apptid", OwningPMH.RelativeOwningPMH.owningFHx.proband.apptid);
            pc.Add("relId", OwningPMH.RelativeOwningPMH.relativeID);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadgeneticTest",
                                                pc,
                                                typeof(GeneticTest),
                                                constructor_args);
            DoListLoad(lla);

            foreach (HraObject ho in this)
            {
                GeneticTest gt = (GeneticTest)ho;
                gt.LoadResultsOnly();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [CollectionDataContract]
    [KnownType(typeof(ClincalObservation))]
    [KnownType(typeof(BreastCancerDetails))]
    [KnownType(typeof(ColonCancerDetails))]
    public class ClinicalObservationList : HRAList<ClincalObservation>
    {
        private ParameterCollection pc = new ParameterCollection();
        
        [DataMember] public PastMedicalHistory OwningPMH;

        private object[] constructor_args;

        /*****************************************************/

        public ClinicalObservationList() { }  // Default constructor for serialization.

        public ClinicalObservationList(PastMedicalHistory pmh)
        {
            OwningPMH = pmh;
            constructor_args = new object[] { OwningPMH };
        }

        /**************************************************************************************************/
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                ((ClincalObservation)o).owningPMH = OwningPMH;
            }
            base.PersistFullList(e);
        }

        /*****************************************************/

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
            pc.Add("relId", OwningPMH.RelativeOwningPMH.relativeID);
            pc.Add("apptid", OwningPMH.RelativeOwningPMH.owningFHx.proband.apptid);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadPastMedicalHistory",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
            foreach (ClincalObservation co in this)
            {
                if (co.disease.ToLower().Contains("breast") && co.disease.ToLower().Contains("cancer"))
                {
                    co.Details = new BreastCancerDetails();
                    co.Details.owningClincalObservation = co;
                    co.Details.BackgroundLoadWork();
                }
                if ((co.disease.ToLower().Contains("colon") || 
                    co.disease.ToLower().Contains("rectal") || 
                    co.disease.ToLower().Contains("uterine")) && co.disease.ToLower().Contains("cancer"))
                {
                    co.Details = new ColonCancerDetails();
                    co.Details.owningClincalObservation = co;
                    co.Details.BackgroundLoadWork();
                }
            }
        }
    }
}

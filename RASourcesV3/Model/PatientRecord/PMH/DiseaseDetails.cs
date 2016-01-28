using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract(IsReference=true)]
    public abstract class DiseaseDetails : HraObject
    {
        [DataMember]
        public ClincalObservation owningClincalObservation;


    }
}

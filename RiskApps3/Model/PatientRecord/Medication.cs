using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class Medication : HraObject
    {
        [DataMember] public string medication;
        [DataMember] public string dose;
        [DataMember] public string howObtained;
        [DataMember] public string reason;
        [DataMember] public string otherInfo;
        [DataMember] public string comments;
        [DataMember] public int medicationID;
        [DataMember] public string internalComment;
        [DataMember] public string startDate;
        [DataMember] public string stopDate;
        [DataMember] public string currentStatus;
        [DataMember] public string dataSource;
        [DataMember] public string frequency;
        [DataMember] public string route;
        [DataMember] public string prn;
        [DataMember] public bool currentMed;
        [DataMember] public string medicationGroup;
        [DataMember] public bool patientAsked;

        public Medication() { }  // Default constructor for serialization

        public Medication(MedicationHx owningHx)
        {

        }
    }
}

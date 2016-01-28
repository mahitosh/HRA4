using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class ColonCancerDetails : DiseaseDetails
    {
        /*****************************************************/
        public ColonCancerDetails() { } // Default constructor for serialization.
        /*****************************************************/

        [DataMember] [HraAttribute] public int diagnosisMonth=-1;
        [DataMember] [HraAttribute] public int diagnosisYear=-1;
        [DataMember] [HraAttribute] public string immunohistochemistry;
        [DataMember] [HraAttribute] public string location;
        [DataMember] [HraAttribute] public string msiResults;
        //
        [DataMember] [HraAttribute] public string BRAFV600E;
        [DataMember] [HraAttribute] public string MLH1PromoterMethylation;
        [DataMember] [HraAttribute] public bool PMS2Absent;
        [DataMember] [HraAttribute] public bool MSH2Absent;
        [DataMember] [HraAttribute] public bool MSH6Absent;
        [DataMember] [HraAttribute] public bool MLH1Absent;

        #region customSetters
        /*****************************************************/
        
        public int ColonCancerDetails_diagnosisMonth
        {
            get
            {
                return diagnosisMonth;
            }
            set
            {
                if (value != diagnosisMonth)
                {
                    diagnosisMonth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diagnosisMonth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public int ColonCancerDetails_diagnosisYear
        {
            get
            {
                return diagnosisYear;
            }
            set
            {
                if (value != diagnosisYear)
                {
                    diagnosisYear = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diagnosisYear"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string ColonCancerDetails_immunohistochemistry
        {
            get
            {
                return immunohistochemistry;
            }
            set
            {
                if (value != immunohistochemistry)
                {
                    immunohistochemistry = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("immunohistochemistry"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string ColonCancerDetails_location
        {
            get
            {
                return location;
            }
            set
            {
                if (value != location)
                {
                    location = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("location"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        
        public string ColonCancerDetails_msiResults
        {
            get
            {
                return msiResults;
            }
            set
            {
                if (value != msiResults)
                {
                    msiResults = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("msiResults"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string ColonCancerDetails_BRAFV600E
        {
            get
            {
                return BRAFV600E;
            }
            set
            {
                if (value != BRAFV600E)
                {
                    BRAFV600E = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BRAFV600E"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ColonCancerDetails_MLH1PromoterMethylation
        {
            get
            {
                return MLH1PromoterMethylation;
            }
            set
            {
                if (value != MLH1PromoterMethylation)
                {
                    MLH1PromoterMethylation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MLH1PromoterMethylation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool ColonCancerDetails_PMS2Absent
        {
            get
            {
                return PMS2Absent;
            }
            set
            {
                if (value != PMS2Absent)
                {
                    PMS2Absent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PMS2Absent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool ColonCancerDetails_MSH2Absent
        {
            get
            {
                return MSH2Absent;
            }
            set
            {
                if (value != MSH2Absent)
                {
                    MSH2Absent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MSH2Absent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool ColonCancerDetails_MSH6Absent
        {
            get
            {
                return MSH6Absent;
            }
            set
            {
                if (value != MSH6Absent)
                {
                    MSH6Absent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MSH6Absent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public bool ColonCancerDetails_MLH1Absent
        {
            get
            {
                return MLH1Absent;
            }
            set
            {
                if (value != MLH1Absent)
                {
                    MLH1Absent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MLH1Absent"));
                    SignalModelChanged(args);
                }
            }
        } 


        #endregion

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum",
                                                             owningClincalObservation.owningPMH.RelativeOwningPMH
                                                                                     .owningFHx.proband.unitnum);
            pc.Add("relativeID", owningClincalObservation.owningPMH.RelativeOwningPMH.relativeID);
            pc.Add("instanceID", owningClincalObservation.instanceID);
            pc.Add("apptid", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);
            DoLoadWithSpAndParams("sp_3_LoadColonCancerDetails", pc);
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection("unitnum",
                                                             owningClincalObservation.owningPMH.RelativeOwningPMH
                                                                                     .owningFHx.proband.unitnum);
            pc.Add("relativeID", owningClincalObservation.owningPMH.RelativeOwningPMH.relativeID);
            pc.Add("instanceID", owningClincalObservation.instanceID);
            pc.Add("apptid", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);

            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_ColonCancerDetails",
                                     ref pc);
        }
    }
}
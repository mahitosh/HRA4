using System.Runtime.Serialization;

using RiskApps3.View;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class SocialHistory : HraObject
    {
        /**************************************************************************************************/
        [DataMember]
        public Patient patientOwning;

        [DataMember] [HraAttribute] private string hasSmoked;
        [DataMember] [HraAttribute] private string hasAlcohol;
        [DataMember] [HraAttribute] private string smokingPacksPerDay;
        [DataMember] [HraAttribute] private int packYears;
        [DataMember] [HraAttribute] private string smokingWhenQuit;
        [DataMember] [HraAttribute] private string currentlySmoke;
        [DataMember] [HraAttribute] private string numCigarettesPerDay;
        [DataMember] [HraAttribute] private string numYearsSmokedCigarettes;
        [DataMember] [HraAttribute] public string vegetableServingsPerDay;
        [DataMember] [HraAttribute] public string RegularAspirinUser;
        [DataMember] [HraAttribute] public string RegularIbuprofenUser;
        [DataMember] [HraAttribute] public string vigorousPhysicalActivityHoursPerWeek;
        [DataMember] [HraAttribute] public string colonoscopyLast10Years;
        [DataMember] [HraAttribute] public string colonPolypLast10Years;

        /*****************************************************/
        public string SocialHistory_hasSmoked
        {
            get
            {
                return hasSmoked;
            }
            set
            {
                if (value != hasSmoked)
                {
                    hasSmoked = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hasSmoked"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_hasAlcohol
        {
            get
            {
                return hasAlcohol;
            }
            set
            {
                if (value != hasAlcohol)
                {
                    hasAlcohol = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hasAlcohol"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_smokingPacksPerDay
        {
            get
            {
                return smokingPacksPerDay;
            }
            set
            {
                if (value != smokingPacksPerDay)
                {
                    smokingPacksPerDay = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("smokingPacksPerDay"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int SocialHistory_packYears
        {
            get
            {
                return packYears;
            }
            set
            {
                if (value != packYears)
                {
                    packYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("packYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_smokingWhenQuit
        {
            get
            {
                return smokingWhenQuit;
            }
            set
            {
                if (value != smokingWhenQuit)
                {
                    smokingWhenQuit = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("smokingWhenQuit"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_currentlySmoke
        {
            get
            {
                return currentlySmoke;
            }
            set
            {
                if (value != currentlySmoke)
                {
                    currentlySmoke = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("currentlySmoke"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_numCigarettesPerDay
        {
            get
            {
                return numCigarettesPerDay;
            }
            set
            {
                if (value != numCigarettesPerDay)
                {
                    numCigarettesPerDay = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("numCigarettesPerDay"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_numYearsSmokedCigarettes
        {
            get
            {
                return numYearsSmokedCigarettes;
            }
            set
            {
                if (value != numYearsSmokedCigarettes)
                {
                    numYearsSmokedCigarettes = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("numYearsSmokedCigarettes"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_vegetableServingsPerDay
        {
            get
            {
                return vegetableServingsPerDay;
            }
            set
            {
                if (value != vegetableServingsPerDay)
                {
                    vegetableServingsPerDay = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("vegetableServingsPerDay"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_RegularAspirinUser
        {
            get
            {
                return RegularAspirinUser;
            }
            set
            {
                if (value != RegularAspirinUser)
                {
                    RegularAspirinUser = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RegularAspirinUser"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_RegularIbuprofenUser
        {
            get
            {
                return RegularIbuprofenUser;
            }
            set
            {
                if (value != RegularIbuprofenUser)
                {
                    RegularIbuprofenUser = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RegularIbuprofenUser"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_vigorousPhysicalActivityHoursPerWeek
        {
            get
            {
                return vigorousPhysicalActivityHoursPerWeek;
            }
            set
            {
                if (value != vigorousPhysicalActivityHoursPerWeek)
                {
                    vigorousPhysicalActivityHoursPerWeek = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("vigorousPhysicalActivityHoursPerWeek"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_colonoscopyLast10Years
        {
            get
            {
                return colonoscopyLast10Years;
            }
            set
            {
                if (value != colonoscopyLast10Years)
                {
                    colonoscopyLast10Years = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("colonoscopyLast10Years"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SocialHistory_colonPolypLast10Years
        {
            get
            {
                return colonPolypLast10Years;
            }
            set
            {
                if (value != colonPolypLast10Years)
                {
                    colonPolypLast10Years = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("colonPolypLast10Years"));
                    SignalModelChanged(args);
                }
            }
        } 


        /**************************************************************************************************/
        public SocialHistory() { } // Default constructor for serialization

        public SocialHistory(Patient owner)
        {
            patientOwning = owner;
        }

        /**************************************************************************************************/

        //public void RemoveViewHandlers(HraView view)
        //{
        //    base.ReleaseListeners(view);
        //}

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoLoadWithSpAndParams("sp_3_LoadSocialHx", pc);
        }

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_SocialHistory",
                                      ref pc);

        }
    }
}
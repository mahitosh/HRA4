using System;
using System.Data;
using System.Runtime.Serialization;

using RiskApps3.View;
using System.Data.SqlClient;
using RiskApps3.Utilities;
using System.Reflection;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class BreastBx : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Patient patientOwningSHx;

        [DataMember] [HraAttribute] private string biopsyAtypical;
        [DataMember] [HraAttribute] private string biopsyLCIS;
        [DataMember] [HraAttribute] private string hadBreastBiopsy;
        [DataMember] [HraAttribute] private string breastBiopsies;
        [DataMember] [HraAttribute] private string breastBiopsiesSide;
        [DataMember] [HraAttribute] private string breastBiopsiesLeft1;
        [DataMember] [HraAttribute] private string breastBiopsiesLeftDate1;
        [DataMember] [HraAttribute] private string breastBiopsiesLeft2;
        [DataMember] [HraAttribute] private string breastBiopsiesLeftDate2;
        [DataMember] [HraAttribute] private string breastBiopsiesLeft3;
        [DataMember] [HraAttribute] private string breastBiopsiesLeftDate3;
        [DataMember] [HraAttribute] private string breastBiopsiesRight1;
        [DataMember] [HraAttribute] private string breastBiopsiesRightDate1;
        [DataMember] [HraAttribute] private string breastBiopsiesRight2;
        [DataMember] [HraAttribute] private string breastBiopsiesRightDate2;
        [DataMember] [HraAttribute] private string breastBiopsiesRight3;
        [DataMember] [HraAttribute] private string breastBiopsiesRightDate3;


        /**************************************************************************************************/
        public BreastBx() { } // Default constructor for serialization

        public BreastBx(Patient owner)
        {
            patientOwningSHx = owner;
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", patientOwningSHx.unitnum);
            pc.Add("apptid", patientOwningSHx.apptid);
            DoLoadWithSpAndParams("sp_3_LoadBreastBx", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", patientOwningSHx.unitnum);
            pc.Add("apptid", patientOwningSHx.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_BreastBx",
                                      ref pc);

        }
        /*****************************************************/
        public string BreastBx_biopsyAtypical
        {
            get
            {
                return biopsyAtypical;
            }
            set
            {
                if (value != biopsyAtypical)
                {
                    biopsyAtypical = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("biopsyAtypical"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string BreastBx_biopsyLCIS
        {
            get
            {
                return biopsyLCIS;
            }
            set
            {
                if (value != biopsyLCIS)
                {
                    biopsyLCIS = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("biopsyLCIS"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string BreastBx_hadBreastBiopsy
        {
            get
            {
                return hadBreastBiopsy;
            }
            set
            {
                if (value != hadBreastBiopsy)
                {
                    hadBreastBiopsy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hadBreastBiopsy"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsies
        {
            get
            {
                return breastBiopsies;
            }
            set
            {
                if (value != breastBiopsies)
                {
                    breastBiopsies = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsies"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesSide
        {
            get
            {
                return breastBiopsiesSide;
            }
            set
            {
                if (value != breastBiopsiesSide)
                {
                    breastBiopsiesSide = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesSide"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeft1
        {
            get
            {
                return breastBiopsiesLeft1;
            }
            set
            {
                if (value != breastBiopsiesLeft1)
                {
                    breastBiopsiesLeft1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeft1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeftDate1
        {
            get
            {
                return breastBiopsiesLeftDate1;
            }
            set
            {
                if (value != breastBiopsiesLeftDate1)
                {
                    breastBiopsiesLeftDate1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeftDate1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeft2
        {
            get
            {
                return breastBiopsiesLeft2;
            }
            set
            {
                if (value != breastBiopsiesLeft2)
                {
                    breastBiopsiesLeft2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeft2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeftDate2
        {
            get
            {
                return breastBiopsiesLeftDate2;
            }
            set
            {
                if (value != breastBiopsiesLeftDate2)
                {
                    breastBiopsiesLeftDate2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeftDate2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeft3
        {
            get
            {
                return breastBiopsiesLeft3;
            }
            set
            {
                if (value != breastBiopsiesLeft3)
                {
                    breastBiopsiesLeft3 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeft3"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesLeftDate3
        {
            get
            {
                return breastBiopsiesLeftDate3;
            }
            set
            {
                if (value != breastBiopsiesLeftDate3)
                {
                    breastBiopsiesLeftDate3 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesLeftDate3"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRight1
        {
            get
            {
                return breastBiopsiesRight1;
            }
            set
            {
                if (value != breastBiopsiesRight1)
                {
                    breastBiopsiesRight1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRight1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRightDate1
        {
            get
            {
                return breastBiopsiesRightDate1;
            }
            set
            {
                if (value != breastBiopsiesRightDate1)
                {
                    breastBiopsiesRightDate1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRightDate1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRight2
        {
            get
            {
                return breastBiopsiesRight2;
            }
            set
            {
                if (value != breastBiopsiesRight2)
                {
                    breastBiopsiesRight2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRight2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRightDate2
        {
            get
            {
                return breastBiopsiesRightDate2;
            }
            set
            {
                if (value != breastBiopsiesRightDate2)
                {
                    breastBiopsiesRightDate2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRightDate2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRight3
        {
            get
            {
                return breastBiopsiesRight3;
            }
            set
            {
                if (value != breastBiopsiesRight3)
                {
                    breastBiopsiesRight3 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRight3"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastBx_breastBiopsiesRightDate3
        {
            get
            {
                return breastBiopsiesRightDate3;
            }
            set
            {
                if (value != breastBiopsiesRightDate3)
                {
                    breastBiopsiesRightDate3 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastBiopsiesRightDate3"));
                    SignalModelChanged(args);
                }
            }
        } 

    }
}
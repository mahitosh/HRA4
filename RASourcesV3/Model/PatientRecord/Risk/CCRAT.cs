using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class CCRAT : HRAList<CCRATRiskByAge>
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public CCRATDetails Details;
        public CCRAT(Patient proband)
        {
            Details = new CCRATDetails();
            OwningPatient = proband;
            constructor_args = new object[] { };
        }
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadCCRATLifetimeRisk",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

            Details.unitnum = OwningPatient.unitnum;
            Details.apptid = OwningPatient.apptid;
            Details.BackgroundLoadWork();
        }
    }
    public class CCRATRiskByAge : HraObject
    {
        [HraAttribute] public int age;
        [HraAttribute] public double ColonCaRisk;

        /*****************************************************/
        public int CCRATRiskByAge_age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != age)
                {
                    age = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("age"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double CCRATRiskByAge_ColonCaRisk
        {
            get
            {
                return ColonCaRisk;
            }
            set
            {
                if (value != ColonCaRisk)
                {
                    ColonCaRisk = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ColonCaRisk"));
                    SignalModelChanged(args);
                }
            }
        }

    }
    public class CCRATDetails : HraObject
    {
        public string unitnum;
        public int apptid;

        [HraAttribute (auditable=false)] public double CCRAT_FiveYear_CRC;
        [HraAttribute (auditable=false)] public double CCRAT_Lifetime_CRC;
        [HraAttribute (auditable=false)] public string CCRAT_MESSAGES;
        [HraAttribute (auditable=false)] public string CCRAT_NAERRORS;
        [HraAttribute (auditable=false)] public string CCRAT_ERRORS;
        [HraAttribute (auditable=false)] public string CCRAT_VERSION;

        /*****************************************************/
        public double CCRATDetails_CCRAT_FiveYear_CRC
        {
            get
            {
                return CCRAT_FiveYear_CRC;
            }
            set
            {
                if (value != CCRAT_FiveYear_CRC)
                {
                    CCRAT_FiveYear_CRC = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CCRAT_FiveYear_CRC"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double CCRATDetails_CCRAT_Lifetime_CRC
        {
            get
            {
                return CCRAT_Lifetime_CRC;
            }
            set
            {
                if (value != CCRAT_Lifetime_CRC)
                {
                    CCRAT_Lifetime_CRC = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CCRAT_Lifetime_CRC"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string CCRATDetails_CCRAT_MESSAGES
        {
            get
            {
                return CCRAT_MESSAGES;
            }
            set
            {
                if (value != CCRAT_MESSAGES)
                {
                    CCRAT_MESSAGES = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CCRAT_MESSAGES"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string CCRATDetails_CCRAT_NAERRORS
        {
            get
            {
                return CCRAT_NAERRORS;
            }
            set
            {
                if (value != CCRAT_NAERRORS)
                {
                    CCRAT_NAERRORS = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("CCRAT_NAERRORS"));
                    SignalModelChanged(args);
                }
            }
        } 



        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadCCRATDetails", pc);
        }

    }
}

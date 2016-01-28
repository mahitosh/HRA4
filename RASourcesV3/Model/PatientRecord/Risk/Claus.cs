using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class Claus : HRAList<ClausRiskByAge>
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public ClausRiskFactors RiskFactors;

        public Claus(Patient proband)
        {
            RiskFactors = new ClausRiskFactors();
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadClausLifetimeRisk",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

            RiskFactors.unitnum = OwningPatient.unitnum;
            RiskFactors.apptid = OwningPatient.apptid;
            RiskFactors.BackgroundLoadWork();
        }

        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            RiskFactors.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid);
            int proband_age;
            if (int.TryParse(OwningPatient.age, out proband_age))
            {
                int lifetime_age = proband_age;
                foreach (ClausRiskByAge o in this)
                {
                    if (o.age > lifetime_age)
                    {
                        lifetime_age = o.age;
                    }
                }
                foreach (ClausRiskByAge o in this)
                {
                    if (o.age - proband_age == 5)
                    {
                        o.description = "Five Year";
                    }
                    if (lifetime_age == o.age)
                    {
                        o.description = "Lifetime";
                    }
                    o.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid);
                }
            }
        }

    }
    
    public class ClausRiskByAge : HraObject
    {
        [HraAttribute (auditable=false)] public int age;
        [HraAttribute (auditable=false)] public double BreastCaRisk;
        [HraAttribute (auditable=false)] public string description;

        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_ClausRiskByAge",
                                        ref pc);
        }


        #region getters_setters
        /*****************************************************/
        public int ClausRiskByAge_age
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
        public double ClausRiskByAge_BreastCaRisk
        {
            get
            {
                return BreastCaRisk;
            }
            set
            {
                if (value != BreastCaRisk)
                {
                    BreastCaRisk = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BreastCaRisk"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion
    }

    public class ClausRiskFactors : HraObject
    {
        public string unitnum;
        public int apptid;

        [HraAttribute (auditable=false)] public string Claus_Table;
        [HraAttribute (auditable=false)] public string RelOne;
        [HraAttribute (auditable=false)] public string RelTwo;

        [HraAttribute (auditable=false)] public string effectiveTime;

        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_ClausParameters",
                                        ref pc);
        }


        #region getters_setters
        /*****************************************************/
        public string ClausRiskFactors_Claus_Table
        {
            get
            {
                return Claus_Table;
            }
            set
            {
                if (value != Claus_Table)
                {
                    Claus_Table = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Claus_Table"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ClausRiskFactors_RelOne
        {
            get
            {
                return RelOne;
            }
            set
            {
                if (value != RelOne)
                {
                    RelOne = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RelOne"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ClausRiskFactors_RelTwo
        {
            get
            {
                return RelTwo;
            }
            set
            {
                if (value != RelTwo)
                {
                    RelTwo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("RelTwo"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

        public override void BackgroundLoadWork()
        {
            Claus_Table = "";
            RelOne = "";
            RelTwo = "";
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadClausRiskFactors", pc);
        }

    }
}

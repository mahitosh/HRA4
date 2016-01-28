using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class Gail : HRAList<GailRiskByAge>
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public GailRiskFactors RiskFactors;

        public Gail(Patient proband)
        {
            RiskFactors = new GailRiskFactors();
            OwningPatient = proband;
            constructor_args = new object[] { };
        }
                
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadGailLifetimeRisk",
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
                foreach (GailRiskByAge o in this)
                {
                    if (o.age > lifetime_age)
                    {
                        lifetime_age = o.age;
                    }
                }
                foreach (GailRiskByAge o in this)
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

    public class GailRiskByAge : HraObject
    {
        [HraAttribute (auditable=false)] public int age;
        [HraAttribute (auditable=false)] public double BreastCaRisk;
        [HraAttribute (auditable=false)] public double PopulationCaRisk;
        [HraAttribute (auditable=false)] public string description;

        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_GailRiskByAge",
                                        ref pc);
        }

        #region getters_setters
        /*****************************************************/
        public int GailRiskByAge_age
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
        public double GailRiskByAge_BreastCaRisk
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
        /*****************************************************/
        public double GailRiskByAge_PopulationCaRisk
        {
            get
            {
                return PopulationCaRisk;
            }
            set
            {
                if (value != PopulationCaRisk)
                {
                    PopulationCaRisk = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PopulationCaRisk"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskByAge_description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("description"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

    }
    public class GailRiskFactors : HraObject
    {
        public string unitnum;
        public int apptid;

        [HraAttribute] public string currentAge;
        [HraAttribute] public string menarcheAge;
        [HraAttribute] public string firstLiveBirthAge;
        [HraAttribute] public string hadBiopsy;
        [HraAttribute] public string numBiopsy;
        [HraAttribute] public string hyperPlasia;
        [HraAttribute] public string firstDegreeRel;
        [HraAttribute] public string race;

        [HraAttribute] public string effectiveTime;

        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_GailParameters",
                                        ref pc);
        }

        #region getters_setters
        /*****************************************************/
        public string GailRiskFactors_menarcheAge
        {
            get
            {
                return menarcheAge;
            }
            set
            {
                if (value != menarcheAge)
                {
                    menarcheAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("menarcheAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_firstLiveBirthAge
        {
            get
            {
                return firstLiveBirthAge;
            }
            set
            {
                if (value != firstLiveBirthAge)
                {
                    firstLiveBirthAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("firstLiveBirthAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_hadBiopsy
        {
            get
            {
                return hadBiopsy;
            }
            set
            {
                if (value != hadBiopsy)
                {
                    hadBiopsy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hadBiopsy"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_numBiopsy
        {
            get
            {
                return numBiopsy;
            }
            set
            {
                if (value != numBiopsy)
                {
                    numBiopsy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("numBiopsy"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_hyperPlasia
        {
            get
            {
                return hyperPlasia;
            }
            set
            {
                if (value != hyperPlasia)
                {
                    hyperPlasia = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hyperPlasia"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_firstDegreeRel
        {
            get
            {
                return firstDegreeRel;
            }
            set
            {
                if (value != firstDegreeRel)
                {
                    firstDegreeRel = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("firstDegreeRel"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string GailRiskFactors_race
        {
            get
            {
                return race;
            }
            set
            {
                if (value != race)
                {
                    race = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("race"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoLoadWithSpAndParams("sp_3_LoadGailRiskFactors", pc);
        }

    }
}



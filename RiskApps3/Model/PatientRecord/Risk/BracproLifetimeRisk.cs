using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class BracproCancerRiskList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public BracproCancerRiskList(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadBrcaproCancerRisk",
                                                pc,
                                                typeof(BrcaProCancerRiskByAge),
                                                constructor_args);
            DoListLoad(lla);

        }

        //this override passes in the unitnum for the persist of each BrcaProCancerRiskByAge
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (BrcaProCancerRiskByAge o in this)
            {
                o.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid);
            }
        }


    }

    public class BrcaProCancerRiskByAge : HraObject
    {
        [HraAttribute (auditable=false)] public int age;

        [HraAttribute (auditable=false)] public double BreastCaRisk;
        [HraAttribute (auditable=false)] public double BreastCaRiskWithNoMutation;
        [HraAttribute (auditable=false)] public double BreastCaRiskWithBrca1Mutation;
        [HraAttribute (auditable=false)] public double BreastCaRiskWithBrca2Mutation;
        [HraAttribute (auditable=false)] public double BreastCaRiskWithBrca1And2Mutation;
        [HraAttribute (auditable=false)] public double OvarianCaRisk;
        [HraAttribute (auditable=false)] public double OvarianCaRiskWithNoMutation;
        [HraAttribute (auditable=false)] public double OvarianCaRiskWithBrca1Mutation;
        [HraAttribute (auditable=false)] public double OvarianCaRiskWithBrca2Mutation;
        [HraAttribute (auditable=false)] public double OvarianCaRiskWithBrca1And2Mutation;

        //hazard rates
        [HraAttribute (auditable=false)] public double hFX0;
        [HraAttribute (auditable=false)] public double hFY0;
        [HraAttribute (auditable=false)] public double hFX1;
        [HraAttribute (auditable=false)] public double hFY1;
        [HraAttribute (auditable=false)] public double hFX2;
        [HraAttribute (auditable=false)] public double hFY2;
        [HraAttribute (auditable=false)] public double hFX12;
        [HraAttribute (auditable=false)] public double hFY12;

        /*****************************************************/
        public int BrcaProCancerRiskByAge_age
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
        public double BrcaProCancerRiskByAge_BreastCaRisk
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
        public double BrcaProCancerRiskByAge_BreastCaRiskWithNoMutation
        {
            get
            {
                return BreastCaRiskWithNoMutation;
            }
            set
            {
                if (value != BreastCaRiskWithNoMutation)
                {
                    BreastCaRiskWithNoMutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BreastCaRiskWithNoMutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_BreastCaRiskWithBrca1Mutation
        {
            get
            {
                return BreastCaRiskWithBrca1Mutation;
            }
            set
            {
                if (value != BreastCaRiskWithBrca1Mutation)
                {
                    BreastCaRiskWithBrca1Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BreastCaRiskWithBrca1Mutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_BreastCaRiskWithBrca2Mutation
        {
            get
            {
                return BreastCaRiskWithBrca2Mutation;
            }
            set
            {
                if (value != BreastCaRiskWithBrca2Mutation)
                {
                    BreastCaRiskWithBrca2Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BreastCaRiskWithBrca2Mutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_BreastCaRiskWithBrca1And2Mutation
        {
            get
            {
                return BreastCaRiskWithBrca1And2Mutation;
            }
            set
            {
                if (value != BreastCaRiskWithBrca1And2Mutation)
                {
                    BreastCaRiskWithBrca1And2Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BreastCaRiskWithBrca1And2Mutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_OvarianCaRisk
        {
            get
            {
                return OvarianCaRisk;
            }
            set
            {
                if (value != OvarianCaRisk)
                {
                    OvarianCaRisk = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("OvarianCaRisk"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_OvarianCaRiskWithNoMutation
        {
            get
            {
                return OvarianCaRiskWithNoMutation;
            }
            set
            {
                if (value != OvarianCaRiskWithNoMutation)
                {
                    OvarianCaRiskWithNoMutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("OvarianCaRiskWithNoMutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1Mutation
        {
            get
            {
                return OvarianCaRiskWithBrca1Mutation;
            }
            set
            {
                if (value != OvarianCaRiskWithBrca1Mutation)
                {
                    OvarianCaRiskWithBrca1Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("OvarianCaRiskWithBrca1Mutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_OvarianCaRiskWithBrca2Mutation
        {
            get
            {
                return OvarianCaRiskWithBrca2Mutation;
            }
            set
            {
                if (value != OvarianCaRiskWithBrca2Mutation)
                {
                    OvarianCaRiskWithBrca2Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("OvarianCaRiskWithBrca2Mutation"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double BrcaProCancerRiskByAge_OvarianCaRiskWithBrca1And2Mutation
        {
            get
            {
                return OvarianCaRiskWithBrca1And2Mutation;
            }
            set
            {
                if (value != OvarianCaRiskWithBrca1And2Mutation)
                {
                    OvarianCaRiskWithBrca1And2Mutation = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("OvarianCaRiskWithBrca1And2Mutation"));
                    SignalModelChanged(args);
                }
            }
        }

        //a customized persistence method for BrcaProCancerRiskByAge items
        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_BrcaProCancerRiskByAge",
                                        ref pc);
        }
    }

}

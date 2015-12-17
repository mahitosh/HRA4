using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class MmrproCancerRiskList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public MmrproCancerRiskList(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadMMRproCancerRisk",
                                                pc,
                                                typeof(MMRproCancerRiskByAge),
                                                constructor_args);
            DoListLoad(lla);

        }

        //this override passes in the unitnum for the persist of each BrcaProCancerRiskByAge
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (MMRproCancerRiskByAge o in this)
            {
                o.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid);
            }
        }

    }

    public class MMRproCancerRiskByAge : HraObject
    {
        [HraAttribute (auditable=false)] public int age;

        [HraAttribute (auditable=false)] public double ColonCaRisk;
        [HraAttribute (auditable=false)] public double ColonCaRiskWithMLH1;
        [HraAttribute (auditable=false)] public double ColonCaRiskWithMSH2;
        [HraAttribute (auditable=false)] public double ColonCaRiskWithMSH6;
        [HraAttribute (auditable=false)] public double ColonCaRiskNoMut;
        [HraAttribute (auditable=false)] public double EndometrialCaRisk;
        [HraAttribute (auditable=false)] public double EndometrialCaRiskWithMLH1;
        [HraAttribute (auditable=false)] public double EndometrialCaRiskWithMSH2;
        [HraAttribute (auditable=false)] public double EndometrialCaRiskWithMSH6;
        [HraAttribute (auditable=false)] public double EndometrialCaRiskNoMut;

        //hazard rates
        [HraAttribute (auditable=false)] public double hFX0;
        [HraAttribute (auditable=false)] public double hFX1;
        [HraAttribute (auditable=false)] public double hFX2;
        [HraAttribute (auditable=false)] public double hFX6;
        [HraAttribute (auditable=false)] public double hFY0;
        [HraAttribute (auditable=false)] public double hFY1;
        [HraAttribute (auditable=false)] public double hFY2;
        [HraAttribute (auditable=false)] public double hFY6;


        #region custom setters
        /*****************************************************/
        public int MMRproCancerRiskByAge_age
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
        public double MMRproCancerRiskByAge_ColonCaRisk
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
        /*****************************************************/
        public double MMRproCancerRiskByAge_ColonCaRiskWithMLH1
        {
            get
            {
                return ColonCaRiskWithMLH1;
            }
            set
            {
                if (value != ColonCaRiskWithMLH1)
                {
                    ColonCaRiskWithMLH1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ColonCaRiskWithMLH1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_ColonCaRiskWithMSH2
        {
            get
            {
                return ColonCaRiskWithMSH2;
            }
            set
            {
                if (value != ColonCaRiskWithMSH2)
                {
                    ColonCaRiskWithMSH2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ColonCaRiskWithMSH2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_ColonCaRiskWithMSH6
        {
            get
            {
                return ColonCaRiskWithMSH6;
            }
            set
            {
                if (value != ColonCaRiskWithMSH6)
                {
                    ColonCaRiskWithMSH6 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ColonCaRiskWithMSH6"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_ColonCaRiskNoMut
        {
            get
            {
                return ColonCaRiskNoMut;
            }
            set
            {
                if (value != ColonCaRiskNoMut)
                {
                    ColonCaRiskNoMut = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ColonCaRiskNoMut"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_EndometrialCaRisk
        {
            get
            {
                return EndometrialCaRisk;
            }
            set
            {
                if (value != EndometrialCaRisk)
                {
                    EndometrialCaRisk = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("EndometrialCaRisk"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_EndometrialCaRiskWithMLH1
        {
            get
            {
                return EndometrialCaRiskWithMLH1;
            }
            set
            {
                if (value != EndometrialCaRiskWithMLH1)
                {
                    EndometrialCaRiskWithMLH1 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("EndometrialCaRiskWithMLH1"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_EndometrialCaRiskWithMSH2
        {
            get
            {
                return EndometrialCaRiskWithMSH2;
            }
            set
            {
                if (value != EndometrialCaRiskWithMSH2)
                {
                    EndometrialCaRiskWithMSH2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("EndometrialCaRiskWithMSH2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_EndometrialCaRiskWithMSH6
        {
            get
            {
                return EndometrialCaRiskWithMSH6;
            }
            set
            {
                if (value != EndometrialCaRiskWithMSH6)
                {
                    EndometrialCaRiskWithMSH6 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("EndometrialCaRiskWithMSH6"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double MMRproCancerRiskByAge_EndometrialCaRiskNoMut
        {
            get
            {
                return EndometrialCaRiskNoMut;
            }
            set
            {
                if (value != EndometrialCaRiskNoMut)
                {
                    EndometrialCaRiskNoMut = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("EndometrialCaRiskNoMut"));
                    SignalModelChanged(args);
                }
            }
        } 
        #endregion

        //a customized persistence method for BrcaProCancerRiskByAge items
        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_MMRProCancerRiskByAge",
                                        ref pc);
        }

    }
}

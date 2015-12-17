using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class TyrerCuzick : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public TcRiskFactors RiskFactors;

        public TyrerCuzick(Patient proband)
        {
            RiskFactors = new TcRiskFactors();
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadTyrerCuzickLifetimeRisk",
                                                pc,
                                                typeof(TcRiskByAge),
                                                constructor_args);
            DoListLoad(lla);

            RiskFactors.unitnum = OwningPatient.unitnum;
            RiskFactors.apptid = OwningPatient.apptid;
            RiskFactors.BackgroundLoadWork();
        }

        //this override passes in the unitnum for the persist of each TcRiskByAge
        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (TcRiskByAge o in this)
            {
                o.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid, RiskFactors.TYRER_CUZICK_VERSION, OwningPatient.RP.BMRS_EffectiveTime, OwningPatient.RP.BMRS_RequestId);
            }
        }

    }
    
    public class TcRiskByAge : HraObject
    {
        [HraAttribute (auditable=false)] public int age;
        [HraAttribute (auditable=false)] public double BreastCaRisk;
        [HraAttribute (auditable=false)] public double PopulationCaRisk;
        [HraAttribute (auditable=false)] public string description;

        //a customized persistence method for TcRiskByAge items
        public void PersistFullObject(HraModelChangedEventArgs e, string unitnum, int apptid, string version, DateTime? BMRS_EffectiveTime, Int64? BMRS_RequestId)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("apptid", apptid);
            pc.Add("version", version);
            pc.Add("BMRS_EffectiveTime", BMRS_EffectiveTime);
            pc.Add("BMRS_RequestId", BMRS_RequestId);

            DoPersistWithSpAndParams(e,
                                        "sp_3_Save_TcRiskByAge",
                                        ref pc);
        }

    }

    public class TcRiskFactors : HraObject
    {
        public string unitnum;
        public int apptid;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK_VERSION;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK_MESSAGES;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK_1_2;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK_1;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK_2;

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadTyrerCuzickRiskFactors", pc);
        }

    }
}

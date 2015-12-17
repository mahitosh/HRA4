using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class TyrerCuzick_v7 : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public TcRiskFactors_v7 RiskFactors;

        public TyrerCuzick_v7(Patient proband)
        {
            RiskFactors = new TcRiskFactors_v7();
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadTyrerCuzickLifetimeRisk_v7",
                                                pc,
                                                typeof(TcRiskByAge),
                                                constructor_args);
            DoListLoad(lla);

            RiskFactors.unitnum = OwningPatient.unitnum;
            RiskFactors.apptid = OwningPatient.apptid;
            RiskFactors.BackgroundLoadWork();
        }

        public override void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (TcRiskByAge o in this)
            {
                o.PersistFullObject(e, OwningPatient.unitnum, OwningPatient.apptid, RiskFactors.TYRER_CUZICK7_VERSION, OwningPatient.RP.BMRS_EffectiveTime, OwningPatient.RP.BMRS_RequestId);
            }
        }
    }

    public class TcRiskFactors_v7 : HraObject
    {
        public string unitnum;
        public int apptid;

        [HraAttribute (auditable=false)] public string TYRER_CUZICK7_VERSION;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK7_MESSAGES;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK7_1_2;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK7_1;
        [HraAttribute (auditable=false)] public string TYRER_CUZICK7_2;


        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            pc.Add("apptid", apptid);
            DoLoadWithSpAndParams("sp_3_LoadTyrerCuzickRiskFactors_v7", pc);
        }

    }
}

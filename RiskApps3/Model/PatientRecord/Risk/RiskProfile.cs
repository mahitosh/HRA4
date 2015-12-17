using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;
using RiskApps3.Model.Security;
using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord.Risk;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class RiskProfile : HraObject
    {
        [DataMember] public Person OwningPerson;

        /**************************************************************************************************/
        [DataMember] [HraAttribute] public Int64? BMRS_RequestId;  //BMRS stand for BayesMendel Risk Service
        [DataMember] [HraAttribute] public DateTime? BMRS_EffectiveTime;
        [DataMember] [HraAttribute] public string BMRS_Messages;
        [DataMember] [HraAttribute] public string BrcaPro_Version;
        [DataMember] [HraAttribute] public string BrcaPro_Messages;
        [DataMember] [HraAttribute] public string MmrPro_Version;
        [DataMember] [HraAttribute] public string MmrPro_Messages;

        [DataMember] [HraAttribute] public double? BrcaPro_1_2_Mut_Prob;
        [DataMember] [HraAttribute] public double? BrcaPro_1_Mut_Prob;
        [DataMember] [HraAttribute] public double? BrcaPro_2_Mut_Prob;

        [DataMember] [HraAttribute] public double? BrcaPro_5Year_Breast;
        [DataMember] [HraAttribute] public double? BrcaPro_5Year_Ovary;
        [DataMember] [HraAttribute] public double? BrcaPro_Lifetime_Breast;
        [DataMember] [HraAttribute] public double? BrcaPro_Lifetime_Ovary;

        [DataMember] [HraAttribute] public Int32? BM_5Year_Age;
        [DataMember] [HraAttribute] public Int32? BM_Lifetime_Age;

        [DataMember] [HraAttribute] public double? MmrPro_1_2_6_Mut_Prob;
        [DataMember] [HraAttribute] public double? MmrPro_MLH1_Mut_Prob;
        [DataMember] [HraAttribute] public double? MmrPro_MSH2_Mut_Prob;
        [DataMember] [HraAttribute] public double? MmrPro_MSH6_Mut_Prob;

        [DataMember] [HraAttribute] public double? PREMM;  //legacy internal local code runs PREMM, not PREMM2
        [DataMember] [HraAttribute] public double? PREMM2; //DFCI Service always returns PREMM2 score
        [DataMember] [HraAttribute] public string PREMM_Version;
        [DataMember] [HraAttribute] public string PREMM_Messages;
        [DataMember] [HraAttribute] public string PREMM_Errors;

        [DataMember] [HraAttribute] public double? MmrPro_5Year_Colon;
        [DataMember] [HraAttribute] public double? MmrPro_5Year_Endometrial; 
        [DataMember] [HraAttribute] public double? MmrPro_Lifetime_Colon;
        [DataMember] [HraAttribute] public double? MmrPro_Lifetime_Endometrial;

        [DataMember] [HraAttribute] public double? Myriad_Brca_1_2;

        [DataMember] [HraAttribute] public double? TyrerCuzick_Brca_1_2;
        [DataMember] [HraAttribute] public double? TyrerCuzick_5Year_Breast;
        [DataMember] [HraAttribute] public double? TyrerCuzick_Lifetime_Breast;

        [DataMember] [HraAttribute] public double? TyrerCuzick_v7_Brca_1_2;
        [DataMember] [HraAttribute] public double? TyrerCuzick_v7_5Year_Breast;
        [DataMember] [HraAttribute] public double? TyrerCuzick_v7_Lifetime_Breast;

        [DataMember] [HraAttribute] public double? Claus_5Year_Breast;
        [DataMember] [HraAttribute] public double? Claus_Lifetime_Breast;

        [DataMember] [HraAttribute] public double? Gail_5Year_Breast;
        [DataMember] [HraAttribute] public double? Gail_Lifetime_Breast;

        #region getter_setters
        /*****************************************************/
        public double? RiskProfile_BrcaPro_1_2_Mut_Prob
        {
            get
            {
                return BrcaPro_1_2_Mut_Prob;
            }
            set
            {
                if (value != BrcaPro_1_2_Mut_Prob)
                {
                    BrcaPro_1_2_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_1_2_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_1_Mut_Prob
        {
            get
            {
                return BrcaPro_1_Mut_Prob;
            }
            set
            {
                if (value != BrcaPro_1_Mut_Prob)
                {
                    BrcaPro_1_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_1_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_2_Mut_Prob
        {
            get
            {
                return BrcaPro_2_Mut_Prob;
            }
            set
            {
                if (value != BrcaPro_2_Mut_Prob)
                {
                    BrcaPro_2_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_2_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_5Year_Breast
        {
            get
            {
                return BrcaPro_5Year_Breast;
            }
            set
            {
                if (value != BrcaPro_5Year_Breast)
                {
                    BrcaPro_5Year_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_5Year_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_5Year_Ovary
        {
            get
            {
                return BrcaPro_5Year_Ovary;
            }
            set
            {
                if (value != BrcaPro_5Year_Ovary)
                {
                    BrcaPro_5Year_Ovary = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_5Year_Ovary"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_Lifetime_Breast
        {
            get
            {
                return BrcaPro_Lifetime_Breast;
            }
            set
            {
                if (value != BrcaPro_Lifetime_Breast)
                {
                    BrcaPro_Lifetime_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_Lifetime_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_BrcaPro_Lifetime_Ovary
        {
            get
            {
                return BrcaPro_Lifetime_Ovary;
            }
            set
            {
                if (value != BrcaPro_Lifetime_Ovary)
                {
                    BrcaPro_Lifetime_Ovary = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BrcaPro_Lifetime_Ovary"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_1_2_6_Mut_Prob
        {
            get
            {
                return MmrPro_1_2_6_Mut_Prob;
            }
            set
            {
                if (value != MmrPro_1_2_6_Mut_Prob)
                {
                    MmrPro_1_2_6_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_1_2_6_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_MLH1_Mut_Prob
        {
            get
            {
                return MmrPro_MLH1_Mut_Prob;
            }
            set
            {
                if (value != MmrPro_MLH1_Mut_Prob)
                {
                    MmrPro_MLH1_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_MLH1_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_MSH2_Mut_Prob
        {
            get
            {
                return MmrPro_MSH2_Mut_Prob;
            }
            set
            {
                if (value != MmrPro_MSH2_Mut_Prob)
                {
                    MmrPro_MSH2_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_MSH2_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_MSH6_Mut_Prob
        {
            get
            {
                return MmrPro_MSH6_Mut_Prob;
            }
            set
            {
                if (value != MmrPro_MSH6_Mut_Prob)
                {
                    MmrPro_MSH6_Mut_Prob = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_MSH6_Mut_Prob"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_5Year_Colon
        {
            get
            {
                return MmrPro_5Year_Colon;
            }
            set
            {
                if (value != MmrPro_5Year_Colon)
                {
                    MmrPro_5Year_Colon = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_5Year_Colon"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_5Year_Endometrial
        {
            get
            {
                return MmrPro_5Year_Endometrial;
            }
            set
            {
                if (value != MmrPro_5Year_Endometrial)
                {
                    MmrPro_5Year_Endometrial = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_5Year_Endometrial"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_Lifetime_Colon
        {
            get
            {
                return MmrPro_Lifetime_Colon;
            }
            set
            {
                if (value != MmrPro_Lifetime_Colon)
                {
                    MmrPro_Lifetime_Colon = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_Lifetime_Colon"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_MmrPro_Lifetime_Endometrial
        {
            get
            {
                return MmrPro_Lifetime_Endometrial;
            }
            set
            {
                if (value != MmrPro_Lifetime_Endometrial)
                {
                    MmrPro_Lifetime_Endometrial = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("MmrPro_Lifetime_Endometrial"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_Myriad_Brca_1_2
        {
            get
            {
                return Myriad_Brca_1_2;
            }
            set
            {
                if (value != Myriad_Brca_1_2)
                {
                    Myriad_Brca_1_2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Myriad_Brca_1_2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_Brca_1_2
        {
            get
            {
                return TyrerCuzick_Brca_1_2;
            }
            set
            {
                if (value != TyrerCuzick_Brca_1_2)
                {
                    TyrerCuzick_Brca_1_2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_Brca_1_2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_5Year_Breast
        {
            get
            {
                return TyrerCuzick_5Year_Breast;
            }
            set
            {
                if (value != TyrerCuzick_5Year_Breast)
                {
                    TyrerCuzick_5Year_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_5Year_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_Lifetime_Breast
        {
            get
            {
                return TyrerCuzick_Lifetime_Breast;
            }
            set
            {
                if (value != TyrerCuzick_Lifetime_Breast)
                {
                    TyrerCuzick_Lifetime_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_Lifetime_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_Claus_5Year_Breast
        {
            get
            {
                return Claus_5Year_Breast;
            }
            set
            {
                if (value != Claus_5Year_Breast)
                {
                    Claus_5Year_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Claus_5Year_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_Claus_Lifetime_Breast
        {
            get
            {
                return Claus_Lifetime_Breast;
            }
            set
            {
                if (value != Claus_Lifetime_Breast)
                {
                    Claus_Lifetime_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Claus_Lifetime_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_Gail_5Year_Breast
        {
            get
            {
                return Gail_5Year_Breast;
            }
            set
            {
                if (value != Gail_5Year_Breast)
                {
                    Gail_5Year_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Gail_5Year_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_Gail_Lifetime_Breast
        {
            get
            {
                return Gail_Lifetime_Breast;
            }
            set
            {
                if (value != Gail_Lifetime_Breast)
                {
                    Gail_Lifetime_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Gail_Lifetime_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_PREMM
        {
            get
            {
                return PREMM;
            }
            set
            {
                if (value != PREMM)
                {
                    PREMM = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PREMM"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public double? RiskProfile_PREMM2
        {
            get
            {
                return PREMM2;
            }
            set
            {
                if (value != PREMM2)
                {
                    PREMM2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PREMM2"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_v7_Brca_1_2
        {
            get
            {
                return TyrerCuzick_v7_Brca_1_2;
            }
            set
            {
                if (value != TyrerCuzick_v7_Brca_1_2)
                {
                    TyrerCuzick_v7_Brca_1_2 = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_v7_Brca_1_2"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_v7_5Year_Breast
        {
            get
            {
                return TyrerCuzick_v7_5Year_Breast;
            }
            set
            {
                if (value != TyrerCuzick_v7_5Year_Breast)
                {
                    TyrerCuzick_v7_5Year_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_v7_5Year_Breast"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public double? RiskProfile_TyrerCuzick_v7_Lifetime_Breast
        {
            get
            {
                return TyrerCuzick_v7_Lifetime_Breast;
            }
            set
            {
                if (value != TyrerCuzick_v7_Lifetime_Breast)
                {
                    TyrerCuzick_v7_Lifetime_Breast = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("TyrerCuzick_v7_Lifetime_Breast"));
                    SignalModelChanged(args);
                }
            }
        } 

        #endregion

        public BracproCancerRiskList BracproCancerRisk;
        public MmrproCancerRiskList MmrproCancerRiskList;

        public Gail GailModel;
        public Claus ClausModel;
        public TyrerCuzick TyrerCuzickModel;
        public TyrerCuzick_v7 TyrerCuzickModel_v7;

        public CCRAT CCRATModel;

        public NCCN NCCNGuideline;

        /**************************************************************************************************/

        public RiskProfile() { } // Default constructor for serialization

        public RiskProfile(Person owner)
        {
            OwningPerson = owner;
            if (owner is Patient)
            {
                BracproCancerRisk = new BracproCancerRiskList((Patient)owner);
                MmrproCancerRiskList = new MmrproCancerRiskList((Patient)owner);
                GailModel = new Gail((Patient)owner);
                ClausModel = new Claus((Patient)owner);
                TyrerCuzickModel = new TyrerCuzick((Patient)owner);
                TyrerCuzickModel_v7 = new TyrerCuzick_v7((Patient)owner);
                CCRATModel = new CCRAT((Patient)owner);
                NCCNGuideline = new NCCN((Patient)owner);
            }
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            BrcaPro_1_2_Mut_Prob = null;
            BrcaPro_1_Mut_Prob = null;
            BrcaPro_2_Mut_Prob = null;
            BrcaPro_5Year_Breast = null;
            BrcaPro_5Year_Ovary = null;
            BrcaPro_Lifetime_Breast = null;
            BrcaPro_Lifetime_Ovary = null;
            MmrPro_1_2_6_Mut_Prob = null;
            MmrPro_MLH1_Mut_Prob = null;
            MmrPro_MSH2_Mut_Prob = null;
            MmrPro_MSH6_Mut_Prob = null;
            PREMM = null;
            PREMM2 = null;
            MmrPro_5Year_Colon = null;
            MmrPro_5Year_Endometrial = null;
            MmrPro_Lifetime_Colon = null;
            MmrPro_Lifetime_Endometrial = null;
            Myriad_Brca_1_2 = null;
            TyrerCuzick_Brca_1_2 = null;
            TyrerCuzick_5Year_Breast = null;
            TyrerCuzick_Lifetime_Breast = null;
            TyrerCuzick_v7_Brca_1_2 = null;
            TyrerCuzick_v7_5Year_Breast = null;
            TyrerCuzick_v7_Lifetime_Breast = null;
            Claus_5Year_Breast = null;
            Claus_Lifetime_Breast = null;
            Gail_5Year_Breast = null;
            Gail_Lifetime_Breast = null;

            ParameterCollection pc = new ParameterCollection("unitnum", OwningPerson.owningFHx.proband.unitnum);
            pc.Add("relativeId", OwningPerson.relativeID);
            pc.Add("apptid", OwningPerson.owningFHx.proband.apptid);
            DoLoadWithSpAndParams("sp_3_LoadRiskProfile", pc);
        }


        public override void LoadFullObject()
        {
            if (HraState != States.Ready)
                base.LoadFullObject();
            
            if (OwningPerson is Patient)
            {
                if (BracproCancerRisk == null)
                {
                    BracproCancerRisk = new BracproCancerRiskList((Patient)OwningPerson);
                }
                if (MmrproCancerRiskList == null)
                {
                    MmrproCancerRiskList = new MmrproCancerRiskList((Patient)OwningPerson);
                }
                if (GailModel == null)
                {
                    GailModel = new Gail((Patient)OwningPerson);
                }
                if (ClausModel == null)
                {
                    ClausModel = new Claus((Patient)OwningPerson);
                }
                if (TyrerCuzickModel == null)
                {
                    TyrerCuzickModel = new TyrerCuzick((Patient)OwningPerson);
                }
                if (TyrerCuzickModel_v7 == null)
                {
                    TyrerCuzickModel_v7 = new TyrerCuzick_v7((Patient)OwningPerson);
                }
                if (CCRATModel == null)
                {
                    CCRATModel = new CCRAT((Patient)OwningPerson);
                }
                if (NCCNGuideline == null)
                {
                    NCCNGuideline = new NCCN((Patient)OwningPerson);
                }

                if (!BracproCancerRisk.IsLoaded)
                    BracproCancerRisk.BackgroundListLoad();

                if (!MmrproCancerRiskList.IsLoaded)
                    MmrproCancerRiskList.BackgroundListLoad();

                if (!GailModel.IsLoaded)
                    GailModel.BackgroundListLoad();

                if (!ClausModel.IsLoaded)
                    ClausModel.BackgroundListLoad();

                if (!TyrerCuzickModel.IsLoaded)
                    TyrerCuzickModel.BackgroundListLoad();

                if (!TyrerCuzickModel_v7.IsLoaded)
                    TyrerCuzickModel_v7.BackgroundListLoad();

                if (!CCRATModel.IsLoaded)
                    CCRATModel.BackgroundListLoad();

                if (!NCCNGuideline.IsLoaded)
                    NCCNGuideline.BackgroundListLoad();
            }
        }


        public override void ReleaseListeners(object view)
        {
            if (BracproCancerRisk != null)
                BracproCancerRisk.ReleaseListeners(view);

            if (MmrproCancerRiskList != null)
                MmrproCancerRiskList.ReleaseListeners(view);

            if (GailModel != null)
                GailModel.ReleaseListeners(view);

            if (ClausModel != null)
                ClausModel.ReleaseListeners(view);

            if (TyrerCuzickModel != null)
                TyrerCuzickModel.ReleaseListeners(view);

            if (TyrerCuzickModel_v7 != null)
                TyrerCuzickModel_v7.ReleaseListeners(view);

            if (CCRATModel != null)
                CCRATModel.ReleaseListeners(view);

            if (NCCNGuideline != null)
                NCCNGuideline.ReleaseListeners(view);

            base.ReleaseListeners(view);
        }
        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

        public override void PersistFullObject(HraModelChangedEventArgs e)
        {
            //Azure barked on configurator
            //if (Configurator.useAggregatorService())
            //{
                //base.PersistFullObject(e);
                string ptUnitnum = OwningPerson.owningFHx.proband.unitnum;

                ParameterCollection pc = new ParameterCollection("unitnum", ptUnitnum);
                pc.Add("relId", ((Person)OwningPerson).relativeID);
                pc.Add("apptid", OwningPerson.owningFHx.proband.apptid);

                if (OwningPerson is Patient)
                {
                    if (OwningPerson.owningFHx.proband.RP.TyrerCuzickModel != null && OwningPerson.owningFHx.proband.RP.TyrerCuzickModel_v7 != null)
                    {
                        string tcVersion = OwningPerson.owningFHx.proband.RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_VERSION;
                        string tcMessages = OwningPerson.owningFHx.proband.RP.TyrerCuzickModel.RiskFactors.TYRER_CUZICK_MESSAGES;
                        string tc7Version = OwningPerson.owningFHx.proband.RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_VERSION;
                        string tc7Messages = OwningPerson.owningFHx.proband.RP.TyrerCuzickModel_v7.RiskFactors.TYRER_CUZICK7_MESSAGES;
                        pc.Add("TyrerCuzick_Version", tcVersion);
                        pc.Add("TyrerCuzick_Messages", tcMessages);
                        pc.Add("TyrerCuzick_v7_Version", tc7Version);
                        pc.Add("TyrerCuzick_v7_Messages", tc7Messages);
                    }
                }

                DoPersistWithSpAndParams(e,
                                            "sp_3_Save_RiskProfile",
                                            ref pc);
                if (OwningPerson is Patient)
                {
                    if (BracproCancerRisk != null)
                        BracproCancerRisk.PersistFullList(e);

                    if (MmrproCancerRiskList != null)
                        MmrproCancerRiskList.PersistFullList(e);

                    if (TyrerCuzickModel != null)
                        TyrerCuzickModel.PersistFullList(e);

                    if (TyrerCuzickModel_v7 != null)
                        TyrerCuzickModel_v7.PersistFullList(e);

                    if (GailModel != null)
                       GailModel.PersistFullList(e);

                    if (ClausModel != null)
                        ClausModel.PersistFullList(e);
                    
                    AuditFullObject();
                }

                
            //}

        }


        public  void ClearRP()
        {
            BrcaPro_1_2_Mut_Prob = null;
            BrcaPro_1_Mut_Prob = null;
            BrcaPro_2_Mut_Prob = null;
            BrcaPro_5Year_Breast = null;
            BrcaPro_5Year_Ovary = null;
            BrcaPro_Lifetime_Breast = null;
            BrcaPro_Lifetime_Ovary = null;
            MmrPro_1_2_6_Mut_Prob = null;
            MmrPro_MLH1_Mut_Prob = null;
            MmrPro_MSH2_Mut_Prob = null;
            MmrPro_MSH6_Mut_Prob = null;
            PREMM = null;
            PREMM2 = null;
            MmrPro_5Year_Colon = null;
            MmrPro_5Year_Endometrial = null;
            MmrPro_Lifetime_Colon = null;
            MmrPro_Lifetime_Endometrial = null;
            Myriad_Brca_1_2 = null;
            TyrerCuzick_Brca_1_2 = null;
            TyrerCuzick_5Year_Breast = null;
            TyrerCuzick_Lifetime_Breast = null;
            TyrerCuzick_v7_Brca_1_2 = null;
            TyrerCuzick_v7_5Year_Breast = null;
            TyrerCuzick_v7_Lifetime_Breast = null;
            Claus_5Year_Breast = null;
            Claus_Lifetime_Breast = null;
            Gail_5Year_Breast = null;
            Gail_Lifetime_Breast = null;

            TyrerCuzickModel.Clear();
            TyrerCuzickModel_v7.Clear();
            BracproCancerRisk.Clear();
            ClausModel.Clear();
            GailModel.Clear();
            MmrproCancerRiskList.Clear();
        }

    }
}

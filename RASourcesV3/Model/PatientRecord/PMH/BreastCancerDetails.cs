using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class BreastCancerDetails : DiseaseDetails
    {
        /*****************************************************/
        // Default constructor for serialization.
        public BreastCancerDetails() { }
        /*****************************************************/


        [DataMember] [HraAttribute] public string side;
        [DataMember] [HraAttribute] public string diagnosisMonth;
        [DataMember] [HraAttribute] public string diagnosisYear;
        [DataMember] [HraAttribute] public string surgeryMonth;
        [DataMember] [HraAttribute] public string surgeryYear;
        [DataMember] [HraAttribute] public string breastSurgery;
        [DataMember] [HraAttribute] public string axillarySurgery;
        [DataMember] [HraAttribute] public string sizeCM;
        [DataMember] [HraAttribute] public string ERStatus;
        [DataMember] [HraAttribute] public string PRStatus;
        [DataMember] [HraAttribute] public string Her2NeuIHC;
        [DataMember] [HraAttribute] public string Her2NeuFISH;
        [DataMember] [HraAttribute] public string invasiveHistology;
        [DataMember] [HraAttribute] public string invasiveHistologyGrade;
        [DataMember] [HraAttribute] public string DCISHistology;
        [DataMember] [HraAttribute] public string DCISNucGrade;
        [DataMember] [HraAttribute] public string DCISERStatus;
        [DataMember] [HraAttribute] public string DCISPRStatus;
        [DataMember] [HraAttribute] public string DCISSize;
        [DataMember] [HraAttribute] public string ERPercent;
        [DataMember] [HraAttribute] public string ERIntensity;
        [DataMember] [HraAttribute] public string PRPercent;
        [DataMember] [HraAttribute] public string PRIntensity;
        [DataMember] [HraAttribute] public string HER2Percent;
        [DataMember] [HraAttribute] public string HER2Intensity;
        [DataMember] [HraAttribute] public string DCISERPercent;
        [DataMember] [HraAttribute] public string DCISERIntensity;
        [DataMember] [HraAttribute] public string DCISPRPercent;
        [DataMember] [HraAttribute] public string DCISPRIntensity;
        [DataMember] [HraAttribute] public string reconstruction;

        #region unsused attributes
        //[HraAttribute] public int apptID;
        //[HraAttribute] public int relativeID;
        //[HraAttribute] public int instanceID;
        //[HraAttribute] public DateTime created;
        //[HraAttribute] public string createdby;
        //[HraAttribute] public DateTime modified;
        //[HraAttribute] public string modifiedby;
        //[HraAttribute] public string clinicalT;
        //[HraAttribute] public string clinicalN;
        //[HraAttribute] public string clinicalM;
        //[HraAttribute] public string clinicalStage;
        //[HraAttribute] public string pathT;
        //[HraAttribute] public string pathN;
        //[HraAttribute] public string pathM;
        //[HraAttribute] public string pathStage;
        //[HraAttribute] public string PNTclinicalT;
        //[HraAttribute] public string PNTclinicalN;
        //[HraAttribute] public string PNTclinicalM;
        //[HraAttribute] public string PNTclinicalStage;
        //[HraAttribute] public string PNTpathT;
        //[HraAttribute] public string PNTpathN;
        //[HraAttribute] public string PNTpathM;
        //[HraAttribute] public string PNTpathStage;
        //[HraAttribute] public string PNTDate;
        //[HraAttribute] public string LVI;
        //[HraAttribute] public int isolatedCellsNodes;
        //[HraAttribute] public int posNodes;
        //[HraAttribute] public int totalNodes;
        //[HraAttribute] public string extranodalExtension;
        //[HraAttribute] public string EIC;
        //[HraAttribute] public string preopTherapy;
        //[HraAttribute] public string chemotherapy;
        //[HraAttribute] public string XRT;
        //[HraAttribute] public string hormonalTherapy;
        //[HraAttribute] public string recurrenceBreast;
        //[HraAttribute] public string recurrenceBreastDate;
        //[HraAttribute] public string recurrenceNodal;
        //[HraAttribute] public string recurrenceNodalDate;
        //[HraAttribute] public string recurrenceDistant;
        //[HraAttribute] public string recurrenceDistantDate;
        //[HraAttribute] public string oncotypeDx;
        //[HraAttribute] public string mammaPrint;
        //[HraAttribute] public string DCISBlocks;
        //[HraAttribute] public string oClock;
        //[HraAttribute] public string quadrant;
        //[HraAttribute] public string BRS;
        //[HraAttribute] public string Elston;
        //[HraAttribute] public string VNPI;
        //[HraAttribute] public string nodesMicro;
        //[HraAttribute] public string nodesMacro;
        //[HraAttribute] public string ki67;
        //[HraAttribute] public string ki67Percent;
        //[HraAttribute] public string ki67Intensity;
        //[HraAttribute] public string occurrence;
        //[HraAttribute] public string sizeCM2;
        //[HraAttribute] public string sizeCM3;
        //[HraAttribute] public string DCISSize2;
        //[HraAttribute] public string DCISSize3;
        //[HraAttribute] public string SPhase;
        //[HraAttribute] public string DNAPloidy;
        //[HraAttribute] public string skinInvolvement;
        //[HraAttribute] public string finalized;
        //[HraAttribute] public string presentedAs;
        #endregion

        #region customSetters
        /*****************************************************/
        public string BreastCancerDetails_side
        {
            get
            {
                return side;
            }
            set
            {
                if (value != side)
                {
                    side = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("side"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_diagnosisMonth
        {
            get
            {
                return diagnosisMonth;
            }
            set
            {
                if (value != diagnosisMonth)
                {
                    diagnosisMonth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diagnosisMonth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_diagnosisYear
        {
            get
            {
                return diagnosisYear;
            }
            set
            {
                if (value != diagnosisYear)
                {
                    diagnosisYear = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diagnosisYear"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_surgeryMonth
        {
            get
            {
                return surgeryMonth;
            }
            set
            {
                if (value != surgeryMonth)
                {
                    surgeryMonth = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("surgeryMonth"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_surgeryYear
        {
            get
            {
                return surgeryYear;
            }
            set
            {
                if (value != surgeryYear)
                {
                    surgeryYear = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("surgeryYear"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_breastSurgery
        {
            get
            {
                return breastSurgery;
            }
            set
            {
                if (value != breastSurgery)
                {
                    breastSurgery = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("breastSurgery"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_axillarySurgery
        {
            get
            {
                return axillarySurgery;
            }
            set
            {
                if (value != axillarySurgery)
                {
                    axillarySurgery = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("axillarySurgery"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_sizeCM
        {
            get
            {
                return sizeCM;
            }
            set
            {
                if (value != sizeCM)
                {
                    sizeCM = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("sizeCM"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_ERStatus
        {
            get
            {
                return ERStatus;
            }
            set
            {
                if (value != ERStatus)
                {
                    ERStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ERStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_PRStatus
        {
            get
            {
                return PRStatus;
            }
            set
            {
                if (value != PRStatus)
                {
                    PRStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PRStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_Her2NeuIHC
        {
            get
            {
                return Her2NeuIHC;
            }
            set
            {
                if (value != Her2NeuIHC)
                {
                    Her2NeuIHC = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Her2NeuIHC"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_Her2NeuFISH
        {
            get
            {
                return Her2NeuFISH;
            }
            set
            {
                if (value != Her2NeuFISH)
                {
                    Her2NeuFISH = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("Her2NeuFISH"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_invasiveHistology
        {
            get
            {
                return invasiveHistology;
            }
            set
            {
                if (value != invasiveHistology)
                {
                    invasiveHistology = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("invasiveHistology"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_invasiveHistologyGrade
        {
            get
            {
                return invasiveHistologyGrade;
            }
            set
            {
                if (value != invasiveHistologyGrade)
                {
                    invasiveHistologyGrade = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("invasiveHistologyGrade"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISHistology
        {
            get
            {
                return DCISHistology;
            }
            set
            {
                if (value != DCISHistology)
                {
                    DCISHistology = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISHistology"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISNucGrade
        {
            get
            {
                return DCISNucGrade;
            }
            set
            {
                if (value != DCISNucGrade)
                {
                    DCISNucGrade = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISNucGrade"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISERStatus
        {
            get
            {
                return DCISERStatus;
            }
            set
            {
                if (value != DCISERStatus)
                {
                    DCISERStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISERStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISPRStatus
        {
            get
            {
                return DCISPRStatus;
            }
            set
            {
                if (value != DCISPRStatus)
                {
                    DCISPRStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISPRStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISSize
        {
            get
            {
                return DCISSize;
            }
            set
            {
                if (value != DCISSize)
                {
                    DCISSize = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISSize"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_ERPercent
        {
            get
            {
                return ERPercent;
            }
            set
            {
                if (value != ERPercent)
                {
                    ERPercent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ERPercent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_ERIntensity
        {
            get
            {
                return ERIntensity;
            }
            set
            {
                if (value != ERIntensity)
                {
                    ERIntensity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ERIntensity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_PRPercent
        {
            get
            {
                return PRPercent;
            }
            set
            {
                if (value != PRPercent)
                {
                    PRPercent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PRPercent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_PRIntensity
        {
            get
            {
                return PRIntensity;
            }
            set
            {
                if (value != PRIntensity)
                {
                    PRIntensity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("PRIntensity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_HER2Percent
        {
            get
            {
                return HER2Percent;
            }
            set
            {
                if (value != HER2Percent)
                {
                    HER2Percent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("HER2Percent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_HER2Intensity
        {
            get
            {
                return HER2Intensity;
            }
            set
            {
                if (value != HER2Intensity)
                {
                    HER2Intensity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("HER2Intensity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISERPercent
        {
            get
            {
                return DCISERPercent;
            }
            set
            {
                if (value != DCISERPercent)
                {
                    DCISERPercent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISERPercent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISERIntensity
        {
            get
            {
                return DCISERIntensity;
            }
            set
            {
                if (value != DCISERIntensity)
                {
                    DCISERIntensity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISERIntensity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISPRPercent
        {
            get
            {
                return DCISPRPercent;
            }
            set
            {
                if (value != DCISPRPercent)
                {
                    DCISPRPercent = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISPRPercent"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_DCISPRIntensity
        {
            get
            {
                return DCISPRIntensity;
            }
            set
            {
                if (value != DCISPRIntensity)
                {
                    DCISPRIntensity = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("DCISPRIntensity"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastCancerDetails_reconstruction
        {
            get
            {
                return reconstruction;
            }
            set
            {
                if (value != reconstruction)
                {
                    reconstruction = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("reconstruction"));
                    SignalModelChanged(args);
                }
            }
        } 

        #endregion

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
            pc.Add("relativeID", owningClincalObservation.owningPMH.RelativeOwningPMH.relativeID);
            pc.Add("instanceID", owningClincalObservation.instanceID);
            pc.Add("apptid", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);
            DoLoadWithSpAndParams("sp_3_LoadBreastCancerDetails", pc);
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection("unitnum", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.unitnum);
            pc.Add("relativeID", owningClincalObservation.owningPMH.RelativeOwningPMH.relativeID);
            pc.Add("instanceID", owningClincalObservation.instanceID);
            pc.Add("apptid", owningClincalObservation.owningPMH.RelativeOwningPMH.owningFHx.proband.apptid);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_BreastCancerDetails",
                                      ref pc);

        }
    
    }
}

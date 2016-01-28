using System.Runtime.Serialization;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class MammographyHx : HraObject
    {
        [DataMember]
        public Patient PatientOwning;
        [DataMember]
        public string PatientUnitnum;

        // ReSharper disable InconsistentNaming - refelction is used to map these members to sp param names and to HraView controls so names must be preserved
        [DataMember]
        [Hra]
        private string breastImplants;
        [DataMember]
        [Hra]
        private string breastImplantsDate;
        [DataMember]
        [Hra]
        private string breastImplantsSide;
        [DataMember]
        [Hra]
        private string breastReduction;
        [DataMember]
        [Hra]
        private string breastReductionDate;
        [DataMember]
        [Hra]
        private string breastReductionSide;
        [DataMember]
        [Hra]
        private string createdby;
        [DataMember]
        [Hra]
        private string filmsOutsideReleaseSigned;
        [DataMember]
        [Hra]
        private string filmsOutsideRequested;
        [DataMember]
        [Hra]
        private string lumpectomyLeft1;
        [DataMember]
        [Hra]
        private string lumpectomyLeftDate1;
        [DataMember]
        [Hra]
        private string lumpectomyRight1;
        [DataMember]
        [Hra]
        private string lumpectomyRightDate1;
        [DataMember]
        [Hra]
        private string mastectomyLeft;
        [DataMember]
        [Hra]
        private string mastectomyLeftDate;
        [DataMember]
        [Hra]
        private string mastectomyRight;
        [DataMember]
        [Hra]
        private string mastectomyRightDate;
        [DataMember]
        [Hra]
        private string otherBreastSurgery;
        [DataMember]
        [Hra]
        private string priorMammogramFilmsAtHosp;
        [DataMember]
        [Hra]
        private string priorMammogramWhere;
        [DataMember]
        [Hra]
        private string priorMammogramYearsDate;
        [DataMember]
        [Hra]
        private string radiationLeft1;
        [DataMember]
        [Hra]
        private string radiationLeftDate1;
        [DataMember]
        [Hra]
        private string radiationRight1;
        [DataMember]
        [Hra]
        private string radiationRightDate1;
        [DataMember]
        [Hra]
        private string reconstructiveSurgery;
        [DataMember]
        [Hra]
        private string reconstructionSide;
        [DataMember]
        [Hra]
        private string reconstructionDate;
        [DataMember]
        [Hra]
        private string otherBreastSurgeryYesNo;
        [DataMember]
        [Hra]
        private string otherBreastSurgerySide;
        [DataMember]
        [Hra]
        private string otherBreastSurgeryDate;
        [DataMember]
        [Hra]
        private string radiationTherapy;
        [DataMember]
        [Hra]
        private string hadMastectomy;
        [DataMember]
        [Hra]
        private string mastectomyDate;
        [DataMember]
        [Hra]
        private string lumpectomyYesNo;
        [DataMember]
        [Hra]
        private string lumpectomyDate;

        [DataMember]
        [Hra]
        private string mammogramRoutine;
        [DataMember]
        [Hra]
        private string visitReason_Lump;
        [DataMember]
        [Hra]
        private string visitReason_Discharge;
        [DataMember]
        [Hra]
        private string visitReason_Retraction;
        [DataMember]
        [Hra]
        private string visitReason_Thickening;
        [DataMember]
        [Hra]
        private string visitReason_Pain;
        [DataMember]
        [Hra]
        private string visitReason_Other;
        [DataMember]
        [Hra]
        private string visitReason_Other_Explain;

        // ReSharper restore InconsistentNaming


        #region accessors
        public string BreastReductionDate
        {
            get { return breastReductionDate; }
            set
            {
                breastReductionDate = value;
                SignalMemberChanged(GetMemberByName("breastReductionDate"));
            }
        }

        public string BreastImplantsDate
        {
            get { return breastImplantsDate; }
            set
            {
                breastImplantsDate = value;
                SignalMemberChanged(GetMemberByName("breastImplantsDate"));
            }
        }

        public string OtherBreastSurgery
        {
            get { return otherBreastSurgery; }
            set
            {
                otherBreastSurgery = value;
                SignalMemberChanged(GetMemberByName("otherBreastSurgery"));
            }
        }

        public string RadiationLeftDate1
        {
            get { return radiationLeftDate1; }
            set
            {
                radiationLeftDate1 = value;
                SignalMemberChanged(GetMemberByName("radiationLeftDate1"));
            }
        }

        public string LumpectomyRightDate1
        {
            get { return lumpectomyRightDate1; }
            set
            {
                lumpectomyRightDate1 = value;
                SignalMemberChanged(GetMemberByName("lumpectomyRightDate1"));
            }
        }

        public string MastectomyRightDate
        {
            get { return mastectomyRightDate; }
            set
            {
                mastectomyRightDate = value;
                SignalMemberChanged(GetMemberByName("mastectomyRightDate"));
            }
        }

        public string MastectomyLeftDate
        {
            get { return mastectomyLeftDate; }
            set
            {
                mastectomyLeftDate = value;
                SignalMemberChanged(GetMemberByName("mastectomyLeftDate"));
            }
        }

        public string LumpectomyLeftDate1
        {
            get { return lumpectomyLeftDate1; }
            set
            {
                lumpectomyLeftDate1 = value;
                SignalMemberChanged(GetMemberByName("lumpectomyLeftDate1"));
            }
        }

        public string RadiationRightDate1
        {
            get { return radiationRightDate1; }
            set
            {
                radiationRightDate1 = value;
                SignalMemberChanged(GetMemberByName("radiationRightDate1"));
            }
        }

        public string BreastReductionSide
        {
            get { return breastReductionSide; }
            set
            {
                breastReductionSide = value;
                SignalMemberChanged(GetMemberByName("breastReductionSide"));
            }
        }

        public string BreastImplantsSide
        {
            get { return breastImplantsSide; }
            set
            {
                breastImplantsSide = value;
                SignalMemberChanged(GetMemberByName("breastImplantsSide"));
            }
        }

        public string BreastReduction
        {
            get { return breastReduction; }
            set
            {
                breastReduction = value;
                SignalMemberChanged(GetMemberByName("breastReduction"));
            }
        }

        public string BreastImplants
        {
            get { return breastImplants; }
            set
            {
                breastImplants = value;
                SignalMemberChanged(GetMemberByName("breastImplants"));
            }
        }

        public string RadiationLeft1
        {
            get { return radiationLeft1; }
            set
            {
                radiationLeft1 = value;
                SignalMemberChanged(GetMemberByName("radiationLeft1"));
            }
        }

        public string RadiationRight1
        {
            get { return radiationRight1; }
            set
            {
                radiationRight1 = value;
                SignalMemberChanged(GetMemberByName("radiationRight1"));
            }
        }

        public string LumpectomyLeft1
        {
            get { return lumpectomyLeft1; }
            set
            {
                lumpectomyLeft1 = value;
                SignalMemberChanged(GetMemberByName("lumpectomyLeft1"));
            }
        }

        public string LumpectomyRight1
        {
            get { return lumpectomyRight1; }
            set
            {
                lumpectomyRight1 = value;
                SignalMemberChanged(GetMemberByName("lumpectomyRight1"));
            }
        }

        public string MastectomyLeft
        {
            get { return mastectomyLeft; }
            set
            {
                mastectomyLeft = value;
                SignalMemberChanged(GetMemberByName("mastectomyLeft"));
            }
        }

        public string MastectomyRight
        {
            get { return mastectomyRight; }
            set
            {
                mastectomyRight = value;
                SignalMemberChanged(GetMemberByName("mastectomyRight"));
            }
        }

        public string PriorMammogramWhere
        {
            get { return priorMammogramWhere; }
            set
            {
                priorMammogramWhere = value;
                SignalMemberChanged(GetMemberByName("priorMammogramWhere"));
            }
        }

        public string PriorMammogramYearsDate
        {
            get { return priorMammogramYearsDate; }
            set
            {
                priorMammogramYearsDate = value;
                SignalMemberChanged(GetMemberByName("priorMammogramYearsDate"));
            }
        }

        public string FilmsOutsideReleaseSigned
        {
            get { return filmsOutsideReleaseSigned; }
            set
            {
                filmsOutsideReleaseSigned = value;
                SignalMemberChanged(GetMemberByName("filmsOutsideReleaseSigned"));
            }
        }

        public string FilmsOutsideRequested
        {
            get { return filmsOutsideRequested; }
            set
            {
                filmsOutsideRequested = value;
                SignalMemberChanged(GetMemberByName("filmsOutsideRequested"));
            }
        }

        public string PriorMammogramFilmsAtHosp
        {
            get { return priorMammogramFilmsAtHosp; }
            set
            {
                priorMammogramFilmsAtHosp = value;
                SignalMemberChanged(GetMemberByName("priorMammogramFilmsAtHosp"));
            }
        }


        public string ReconstructiveSurgery
        {
            get { return reconstructiveSurgery; }
            set
            {
                reconstructiveSurgery = value;
                SignalMemberChanged(GetMemberByName("reconstructiveSurgery"));
            }
        }

        public string ReconstructionSide
        {
            get { return reconstructionSide; }
            set
            {
                reconstructionSide = value;
                SignalMemberChanged(GetMemberByName("reconstructionSide"));
            }
        }

        public string ReconstructionDate
        {
            get { return reconstructionDate; }
            set
            {
                reconstructionDate = value;
                SignalMemberChanged(GetMemberByName("reconstructionDate"));
            }
        }

        public string OtherBreastSurgeryYesNo
        {
            get { return otherBreastSurgeryYesNo; }
            set
            {
                otherBreastSurgeryYesNo = value;
                SignalMemberChanged(GetMemberByName("otherBreastSurgeryYesNo"));
            }
        }

        public string OtherBreastSurgerySide
        {
            get { return otherBreastSurgerySide; }
            set
            {
                otherBreastSurgerySide = value;
                SignalMemberChanged(GetMemberByName("otherBreastSurgerySide"));
            }
        }

        public string OtherBreastSurgeryDate
        {
            get { return otherBreastSurgeryDate; }
            set
            {
                otherBreastSurgeryDate = value;
                SignalMemberChanged(GetMemberByName("otherBreastSurgeryDate"));
            }
        }

        public string RadiationTherapy
        {
            get { return radiationTherapy; }
            set
            {
                radiationTherapy = value;
                SignalMemberChanged(GetMemberByName("radiationTherapy"));
            }
        }

        public string HadMastectomy
        {
            get { return hadMastectomy; }
            set
            {
                hadMastectomy = value;
                SignalMemberChanged(GetMemberByName("hadMastectomy"));
            }
        }

        public string MastectomyDate
        {
            get { return mastectomyDate; }
            set
            {
                mastectomyDate = value;
                SignalMemberChanged(GetMemberByName("mastectomyDate"));
            }
        }

        public string LumpectomyYesNo
        {
            get { return lumpectomyYesNo; }
            set
            {
                lumpectomyYesNo = value;
                SignalMemberChanged(GetMemberByName("lumpectomyYesNo"));
            }
        }

        public string LumpectomyDate
        {
            get { return lumpectomyDate; }
            set
            {
                lumpectomyDate = value;
                SignalMemberChanged(GetMemberByName("lumpectomyDate"));
            }
        }

        public string MammogramRoutine
        {
            get { return mammogramRoutine; }
            set
            {
                mammogramRoutine = value;
                SignalMemberChanged(GetMemberByName("mammogramRoutine"));
            }
        }


        public string VisitReason_Lump
        {
            get { return visitReason_Lump; }
            set
            {
                visitReason_Lump = value;
                SignalMemberChanged(GetMemberByName("visitReason_Lump"));
            }
        }
        public string VisitReason_Discharge
        {
            get { return visitReason_Discharge; }
            set
            {
                visitReason_Discharge = value;
                SignalMemberChanged(GetMemberByName("visitReason_Discharge"));
            }
        }
        public string VisitReason_Retraction
        {
            get { return visitReason_Retraction; }
            set
            {
                visitReason_Retraction = value;
                SignalMemberChanged(GetMemberByName("visitReason_Retraction"));
            }
        }
        public string VisitReason_Thickening
        {
            get { return visitReason_Thickening; }
            set
            {
                visitReason_Thickening = value;
                SignalMemberChanged(GetMemberByName("visitReason_Thickening"));
            }
        }
        public string VisitReason_Pain
        {
            get { return visitReason_Pain; }
            set
            {
                visitReason_Pain = value;
                SignalMemberChanged(GetMemberByName("visitReason_Pain"));
            }
        }
        public string VisitReason_Other
        {
            get { return visitReason_Other; }
            set
            {
                visitReason_Other = value;
                SignalMemberChanged(GetMemberByName("visitReason_Other"));
            }
        }
        public string VisitReason_Other_Explain
        {
            get { return visitReason_Other_Explain; }
            set
            {
                visitReason_Other_Explain = value;
                SignalMemberChanged(GetMemberByName("visitReason_Other_Explain"));
            }
        }





        public string Createdby
        {
            get { return createdby; }
            set
            {
                createdby = value;
                SignalMemberChanged(GetMemberByName("createdby"));
            }
        }

        #endregion

        /// <summary>
        /// Creates a MammographyHxView model.
        /// </summary>
        /// <remarks>For use with serialization</remarks>
        public MammographyHx()
        {
        }

        /// <summary>
        /// Create Mammo Hx with a given patient
        /// </summary>
        /// <param name="owner">who does this Hx apply to?</param>
        public MammographyHx(Patient owner)
        {
            this.PatientOwning = owner;
        }

        public override void BackgroundLoadWork()
        {
            ParameterCollection pc = new ParameterCollection("unitnum", PatientOwning.unitnum);
            pc.Add("currentapptid", PatientOwning.apptid);

            DoLoadWithSpAndParams("sp_3_LoadMammoHx", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", PatientOwning.unitnum);
            pc.Add("apptid", PatientOwning.apptid);

            DoPersistWithSpAndParams(e,
                                      "sp_3_SaveMammoHx",
                                      ref pc);
        }
    }
}

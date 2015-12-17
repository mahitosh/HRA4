using System.Runtime.Serialization;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class MammographyHx : HraObject
    {
        [DataMember] public Patient PatientOwning;
        [DataMember] public string PatientUnitnum;

        // ReSharper disable InconsistentNaming - refelction is used to map these members to sp param names and to HraView controls so names must be preserved
        [DataMember] [Hra] private string breastImplants;
        [DataMember] [Hra] private string breastImplantsDate;
        [DataMember] [Hra] private string breastImplantsSide;
        [DataMember] [Hra] private string breastReduction;
        [DataMember] [Hra] private string breastReductionDate;
        [DataMember] [Hra] private string breastReductionSide;
        [DataMember] [Hra] private string createdby;
        [DataMember] [Hra] private string filmsOutsideReleaseSigned;
        [DataMember] [Hra] private string filmsOutsideRequested;
        [DataMember] [Hra] private string lumpectomyLeft1;
        [DataMember] [Hra] private string lumpectomyLeftDate1;
        [DataMember] [Hra] private string lumpectomyRight1;
        [DataMember] [Hra] private string lumpectomyRightDate1;
        [DataMember] [Hra] private string mastectomyLeft;
        [DataMember] [Hra] private string mastectomyLeftDate;
        [DataMember] [Hra] private string mastectomyRight;
        [DataMember] [Hra] private string mastectomyRightDate;
        [DataMember] [Hra] private string otherBreastSurgery;
        [DataMember] [Hra] private string priorMammogramFilmsAtHosp;
        [DataMember] [Hra] private string priorMammogramWhere;
        [DataMember] [Hra] private string priorMammogramYearsDate;
        [DataMember] [Hra] private string radiationLeft1;
        [DataMember] [Hra] private string radiationLeftDate1;
        [DataMember] [Hra] private string radiationRight1;
        [DataMember] [Hra] private string radiationRightDate1;
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

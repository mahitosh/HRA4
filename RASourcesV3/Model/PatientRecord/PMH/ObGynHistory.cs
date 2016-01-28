using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using System.Reflection;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord.PMH
{
    [DataContract]
    public class ObGynHistory : HraObject
    {
        /**************************************************************************************************/
        [DataMember]
        public Patient patientOwning;
        [DataMember]
        public string patientUnitnum;
        [DataMember] [HraAttribute] private string startedMenstruating;
        [DataMember] [HraAttribute] private string currentlyMenstruating;
        [DataMember] [HraAttribute] private string stoppedMenstruating;
        [DataMember] [HraAttribute] private string lastPeriodDate;
        [DataMember] [HraAttribute] private string timesPregnant;
        [DataMember] [HraAttribute] private string numChildren;
        [DataMember] [HraAttribute] private string ageFirstChildBorn;
        [DataMember] [HraAttribute] private string currentlyPregnant;
        [DataMember] [HraAttribute] private string currentlyNursing;
        [DataMember] [HraAttribute] private string takenFertilityDrugs;
        [DataMember] [HraAttribute] private string takenHerbalSups;
        [DataMember] [HraAttribute] private string takenAlternateMeds;
        [DataMember] [HraAttribute] private string doneChild;
        [DataMember] [HraAttribute] private string menopausalStatus;
        [DataMember] [HraAttribute] private string hadHysterectomy;
        [DataMember] [HraAttribute] private string hysterectomyAge;
        [DataMember] [HraAttribute] private string bothOvariesRemoved;
        [DataMember] [HraAttribute] private string birthControlUse;
        [DataMember] [HraAttribute] private string birthControlAge;
        [DataMember] [HraAttribute] private string birthControlYears;
        [DataMember] [HraAttribute] private string birthControlContinuously;
        [DataMember] [HraAttribute] private string hormoneUse;
        [DataMember] [HraAttribute] private string hormoneAge;
        [DataMember] [HraAttribute] private string hormoneUseYears;
        [DataMember] [HraAttribute] private string hormoneUseContinuously;
        [DataMember] [HraAttribute] private string hormoneYearsSinceLastUse;
        [DataMember] [HraAttribute] private string hormoneCombined;
        [DataMember] [HraAttribute] private string hormoneIntendedLength;
        [DataMember] [HraAttribute] private string ageLastPregnancy;
        [DataMember] [HraAttribute] private string hormonesComments;
        [DataMember] [HraAttribute] private string LMP_Confidence;
        [DataMember] [HraAttribute] private string nursingPast3Months;
        [DataMember] [HraAttribute] private string estrogenYesNo;
        [DataMember] [HraAttribute] private string estrogenYears;
        [DataMember] [HraAttribute] private string estrogenStillUsing;
        [DataMember] [HraAttribute] private string premarinYesNo;
        [DataMember] [HraAttribute] private string premarinYears;
        [DataMember] [HraAttribute] private string premarinStillUsing;
        [DataMember] [HraAttribute] private string proveraYesNo;
        [DataMember] [HraAttribute] private string proveraYears;
        [DataMember] [HraAttribute] private string proveraStillUsing;
        [DataMember] [HraAttribute] private string tamoxifenYesNo;
        [DataMember] [HraAttribute] private string tamoxifenYears;
        [DataMember] [HraAttribute] private string tamoxifenStillUsing;
        [DataMember] [HraAttribute] private string otherHormonesYesNo;
        [DataMember] [HraAttribute] private string otherHormonesYears;
        [DataMember] [HraAttribute] private string otherHormonesStillUsing;
        [DataMember] [HraAttribute] private string otherHormonesExplain;
        #region Getters_Setters
        /*****************************************************/
        public string ObGynHistory_startedMenstruating
        {
            get
            {
                return startedMenstruating;
            }
            set
            {
                if (value != startedMenstruating)
                {
                    startedMenstruating = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("startedMenstruating"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_currentlyMenstruating
        {
            get
            {
                return currentlyMenstruating;
            }
            set
            {
                if (value != currentlyMenstruating)
                {
                    currentlyMenstruating = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("currentlyMenstruating"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_stoppedMenstruating
        {
            get
            {
                return stoppedMenstruating;
            }
            set
            {
                if (value != stoppedMenstruating)
                {
                    stoppedMenstruating = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("stoppedMenstruating"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_lastPeriodDate
        {
            get
            {
                return lastPeriodDate;
            }
            set
            {
                if (value != lastPeriodDate)
                {
                    lastPeriodDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("lastPeriodDate"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_timesPregnant
        {
            get
            {
                return timesPregnant;
            }
            set
            {
                if (value != timesPregnant)
                {
                    timesPregnant = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("timesPregnant"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_numChildren
        {
            get
            {
                return numChildren;
            }
            set
            {
                if (value != numChildren)
                {
                    numChildren = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("numChildren"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_ageFirstChildBorn
        {
            get
            {
                return ageFirstChildBorn;
            }
            set
            {
                if (value != ageFirstChildBorn)
                {
                    ageFirstChildBorn = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ageFirstChildBorn"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_currentlyPregnant
        {
            get
            {
                return currentlyPregnant;
            }
            set
            {
                if (value != currentlyPregnant)
                {
                    currentlyPregnant = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("currentlyPregnant"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_currentlyNursing
        {
            get
            {
                return currentlyNursing;
            }
            set
            {
                if (value != currentlyNursing)
                {
                    currentlyNursing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("currentlyNursing"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string ObGynHistory_nursingPast3Months
        {
            get
            {
                return nursingPast3Months;
            }
            set
            {
                if (value != nursingPast3Months)
                {
                    nursingPast3Months = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("nursingPast3Months"));
                    SignalModelChanged(args);
                }
            }
        }

        /*****************************************************/
        public string ObGynHistory_estrogenYesNo
        {
            get
            {
                return estrogenYesNo;
            }
            set
            {
                if (value != estrogenYesNo)
                {
                    estrogenYesNo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("estrogenYesNo"));
                    SignalModelChanged(args);
                }
            }
        }  
        /*****************************************************/
        public string ObGynHistory_estrogenYears
        {
            get
            {
                return estrogenYears;
            }
            set
            {
                if (value != estrogenYears)
                {
                    estrogenYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("estrogenYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_estrogenStillUsing
        {
            get
            {
                return estrogenStillUsing;
            }
            set
            {
                if (value != estrogenStillUsing)
                {
                    estrogenStillUsing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("estrogenStillUsing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_premarinYesNo
        {
            get
            {
                return premarinYesNo;
            }
            set
            {
                if (value != premarinYesNo)
                {
                    premarinYesNo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("premarinYesNo"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_premarinYears
        {
            get
            {
                return premarinYears;
            }
            set
            {
                if (value != premarinYears)
                {
                    premarinYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("premarinYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_premarinStillUsing
        {
            get
            {
                return premarinStillUsing;
            }
            set
            {
                if (value != premarinStillUsing)
                {
                    premarinStillUsing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("premarinStillUsing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_proveraYesNo
        {
            get
            {
                return proveraYesNo;
            }
            set
            {
                if (value != proveraYesNo)
                {
                    proveraYesNo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("proveraYesNo"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_proveraYears
        {
            get
            {
                return proveraYears;
            }
            set
            {
                if (value != proveraYears)
                {
                    proveraYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("proveraYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_proveraStillUsing
        {
            get
            {
                return proveraStillUsing;
            }
            set
            {
                if (value != proveraStillUsing)
                {
                    proveraStillUsing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("proveraStillUsing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_tamoxifenYesNo
        {
            get
            {
                return tamoxifenYesNo;
            }
            set
            {
                if (value != tamoxifenYesNo)
                {
                    tamoxifenYesNo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("tamoxifenYesNo"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_tamoxifenYears
        {
            get
            {
                return tamoxifenYears;
            }
            set
            {
                if (value != tamoxifenYears)
                {
                    tamoxifenYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("tamoxifenYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_tamoxifenStillUsing
        {
            get
            {
                return tamoxifenStillUsing;
            }
            set
            {
                if (value != tamoxifenStillUsing)
                {
                    tamoxifenStillUsing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("tamoxifenStillUsing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_otherHormonesYesNo
        {
            get
            {
                return otherHormonesYesNo;
            }
            set
            {
                if (value != otherHormonesYesNo)
                {
                    otherHormonesYesNo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("otherHormonesYesNo"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_otherHormonesYears
        {
            get
            {
                return otherHormonesYears;
            }
            set
            {
                if (value != otherHormonesYears)
                {
                    otherHormonesYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("otherHormonesYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_otherHormonesStillUsing
        {
            get
            {
                return otherHormonesStillUsing;
            }
            set
            {
                if (value != otherHormonesStillUsing)
                {
                    otherHormonesStillUsing = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("otherHormonesStillUsing"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_otherHormonesExplain
        {
            get
            {
                return otherHormonesExplain;
            }
            set
            {
                if (value != otherHormonesExplain)
                {
                    otherHormonesExplain = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("otherHormonesExplain"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_takenFertilityDrugs
        {
            get
            {
                return takenFertilityDrugs;
            }
            set
            {
                if (value != takenFertilityDrugs)
                {
                    takenFertilityDrugs = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("takenFertilityDrugs"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_takenHerbalSups
        {
            get
            {
                return takenHerbalSups;
            }
            set
            {
                if (value != takenHerbalSups)
                {
                    takenHerbalSups = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("takenHerbalSups"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_takenAlternateMeds
        {
            get
            {
                return takenAlternateMeds;
            }
            set
            {
                if (value != takenAlternateMeds)
                {
                    takenAlternateMeds = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("takenAlternateMeds"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_doneChild
        {
            get
            {
                return doneChild;
            }
            set
            {
                if (value != doneChild)
                {
                    doneChild = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("doneChild"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_menopausalStatus
        {
            get
            {
                return menopausalStatus;
            }
            set
            {
                if (value != menopausalStatus)
                {
                    menopausalStatus = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("menopausalStatus"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hadHysterectomy
        {
            get
            {
                return hadHysterectomy;
            }
            set
            {
                if (value != hadHysterectomy)
                {
                    hadHysterectomy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hadHysterectomy"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hysterectomyAge
        {
            get
            {
                return hysterectomyAge;
            }
            set
            {
                if (value != hysterectomyAge)
                {
                    hysterectomyAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hysterectomyAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_bothOvariesRemoved
        {
            get
            {
                return bothOvariesRemoved;
            }
            set
            {
                if (value != bothOvariesRemoved)
                {
                    bothOvariesRemoved = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("bothOvariesRemoved"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_birthControlUse
        {
            get
            {
                return birthControlUse;
            }
            set
            {
                if (value != birthControlUse)
                {
                    birthControlUse = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("birthControlUse"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_birthControlAge
        {
            get
            {
                return birthControlAge;
            }
            set
            {
                if (value != birthControlAge)
                {
                    birthControlAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("birthControlAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_birthControlYears
        {
            get
            {
                return birthControlYears;
            }
            set
            {
                if (value != birthControlYears)
                {
                    birthControlYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("birthControlYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_birthControlContinuously
        {
            get
            {
                return birthControlContinuously;
            }
            set
            {
                if (value != birthControlContinuously)
                {
                    birthControlContinuously = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("birthControlContinuously"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneUse
        {
            get
            {
                return hormoneUse;
            }
            set
            {
                if (value != hormoneUse)
                {
                    hormoneUse = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneUse"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneAge
        {
            get
            {
                return hormoneAge;
            }
            set
            {
                if (value != hormoneAge)
                {
                    hormoneAge = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneAge"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneUseYears
        {
            get
            {
                return hormoneUseYears;
            }
            set
            {
                if (value != hormoneUseYears)
                {
                    hormoneUseYears = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneUseYears"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneUseContinuously
        {
            get
            {
                return hormoneUseContinuously;
            }
            set
            {
                if (value != hormoneUseContinuously)
                {
                    hormoneUseContinuously = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneUseContinuously"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneYearsSinceLastUse
        {
            get
            {
                return hormoneYearsSinceLastUse;
            }
            set
            {
                if (value != hormoneYearsSinceLastUse)
                {
                    hormoneYearsSinceLastUse = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneYearsSinceLastUse"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneCombined
        {
            get
            {
                return hormoneCombined;
            }
            set
            {
                if (value != hormoneCombined)
                {
                    hormoneCombined = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneCombined"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormoneIntendedLength
        {
            get
            {
                return hormoneIntendedLength;
            }
            set
            {
                if (value != hormoneIntendedLength)
                {
                    hormoneIntendedLength = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormoneIntendedLength"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_ageLastPregnancy
        {
            get
            {
                return ageLastPregnancy;
            }
            set
            {
                if (value != ageLastPregnancy)
                {
                    ageLastPregnancy = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ageLastPregnancy"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_hormonesComments
        {
            get
            {
                return hormonesComments;
            }
            set
            {
                if (value != hormonesComments)
                {
                    hormonesComments = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hormonesComments"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string ObGynHistory_LMP_Confidence
        {
            get
            {
                return LMP_Confidence;
            }
            set
            {
                if (value != LMP_Confidence)
                {
                    LMP_Confidence = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("LMP_Confidence"));
                    SignalModelChanged(args);
                }
            }
        }
        #endregion

        /**************************************************************************************************/
        public ObGynHistory() { } // Default constructor for serialization
        public ObGynHistory(Patient owner)
        {
            patientOwning = owner;
        }

        /**************************************************************************************************/

        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/

        public override void BackgroundLoadWork()
        {
            //DoLoadWithViewAndPatient("v_3_ObGynHistory", patientOwning);
            ParameterCollection pc = new ParameterCollection("unitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);

            DoLoadWithSpAndParams("sp_3_LoadObGynHistory", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_ObGynHistory",
                                      ref pc);
        }
    }
}
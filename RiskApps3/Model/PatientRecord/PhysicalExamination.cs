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

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class PhysicalExamination : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public Patient patientOwning;

        [DataMember] [HraAttribute] private string patientUnitnum;
        [DataMember] [HraAttribute] private string heightFeetInches;
        [DataMember] [HraAttribute] private string heightInches;
        [DataMember] [HraAttribute] private string weightPounds;
        [DataMember] [HraAttribute] private string weightChangeLastMammo;
        [DataMember] [HraAttribute] private string lastBreastExamDate;
        [DataMember] [HraAttribute] private string braSize;
        [DataMember] [HraAttribute] private string braCupSize;
        [DataMember] [HraAttribute] private string selfExam;
        [DataMember] [HraAttribute] private string BMI;
        [DataMember] [HraAttribute] private string BMICategory;

        /*****************************************************/
        public string PhysicalExamination_patientUnitnum
        {
            get
            {
                return patientUnitnum;
            }
            set
            {
                if (value != patientUnitnum)
                {
                    patientUnitnum = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("patientUnitnum"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_heightFeetInches
        {
            get
            {
                return heightFeetInches;
            }
            set
            {
                if (value != heightFeetInches)
                {
                    heightFeetInches = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("heightFeetInches"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_heightInches
        {
            get
            {
                return heightInches;
            }
            set
            {
                if (value != heightInches)
                {
                    heightInches = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("heightInches"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_weightPounds
        {
            get
            {
                return weightPounds;
            }
            set
            {
                if (value != weightPounds)
                {
                    weightPounds = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("weightPounds"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_weightChangeLastMammo
        {
            get
            {
                return weightChangeLastMammo;
            }
            set
            {
                if (value != weightChangeLastMammo)
                {
                    weightChangeLastMammo = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("weightChangeLastMammo"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_lastBreastExamDate
        {
            get
            {
                return lastBreastExamDate;
            }
            set
            {
                if (value != lastBreastExamDate)
                {
                    lastBreastExamDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("lastBreastExamDate"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_braSize
        {
            get
            {
                return braSize;
            }
            set
            {
                if (value != braSize)
                {
                    braSize = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("braSize"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_braCupSize
        {
            get
            {
                return braCupSize;
            }
            set
            {
                if (value != braCupSize)
                {
                    braCupSize = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("braCupSize"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_selfExam
        {
            get
            {
                return selfExam;
            }
            set
            {
                if (value != selfExam)
                {
                    selfExam = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("selfExam"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_BMI
        {
            get
            {
                return BMI;
            }
            set
            {
                if (value != BMI)
                {
                    BMI = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BMI"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string PhysicalExamination_BMICategory
        {
            get
            {
                return BMICategory;
            }
            set
            {
                if (value != BMICategory)
                {
                    BMICategory = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BMICategory"));
                    SignalModelChanged(args);
                }
            }
        } 


        /**************************************************************************************************/
        public PhysicalExamination() { } // Default constructor for serialization

        public PhysicalExamination(Patient owner)
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
            //DoLoadWithViewAndPatient("v_3_PhysicalExmination", patientOwning);
            ParameterCollection pc = new ParameterCollection("unitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoLoadWithSpAndParams("sp_3_LoadPhysicalExmination", pc);
        }

        /**************************************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("patientUnitnum", patientOwning.unitnum);
            pc.Add("apptid", patientOwning.apptid);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_PhysicalExmination",
                                      ref pc);
        }

    }
}
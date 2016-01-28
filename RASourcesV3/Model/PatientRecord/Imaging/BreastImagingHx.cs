using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using RiskApps3.View;

namespace RiskApps3.Model.PatientRecord
{
    /**************************************************************************************************/
    [CollectionDataContract]
    [KnownType(typeof(BreastImagingStudy))]
    public class BreastImagingHx : HRAList<BreastImagingStudy>
    {
        private ParameterCollection pc = new ParameterCollection();
        
        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public BreastImagingHx() { } // Default constructor for serialization

        public BreastImagingHx(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadBreastImaging",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }
    }


    /**************************************************************************************************/
    [DataContract]
    public class BreastImagingStudy : Diagnostic
    {
        
        public int instanceID;

        [DataMember] [HraAttribute] public string imagingType;
        [DataMember] [HraAttribute] public string rightReportConclusions;
        [DataMember] [HraAttribute] public string rightBirads;
        [DataMember] [HraAttribute] public string leftReportConclusions;
        [DataMember] [HraAttribute] public string leftBirads;
        [DataMember] [HraAttribute] public string BIRADS;
        [DataMember] [HraAttribute] public string side;

        Dictionary<string, int> BiradsTrumping = new Dictionary<string, int>();

        public BreastImagingStudy()
        {
            BiradsTrumping.Add("",1);
            BiradsTrumping.Add("Not Done", 2);
            BiradsTrumping.Add("1 BIRADS", 3);
            BiradsTrumping.Add("2 BIRADS", 4);
            BiradsTrumping.Add("1,2 BIRADS", 5);
            BiradsTrumping.Add("0 BIRADS", 6);
            BiradsTrumping.Add("3 BIRADS", 7);
            BiradsTrumping.Add("4 BIRADS stereo core for calcs",8);
            BiradsTrumping.Add("4 BIRADS stereo core for density/mass", 9);
            BiradsTrumping.Add("4 BIRADS ultrasound core for density/mass", 10);
            BiradsTrumping.Add("4 BIRADS aspiration", 11);
            BiradsTrumping.Add("4 BIRADS open biopsy", 12);
            BiradsTrumping.Add("4 BIRADS Biopsy", 13);
            BiradsTrumping.Add("4 BIRADS MRI Guided Biopsy", 14);
            BiradsTrumping.Add("5 BIRADS", 15);
            BiradsTrumping.Add("6 BIRADS", 16);
        }

        /*****************************************************/
        public string BreastImaging_imagingType
        {
            get
            {
                return imagingType;
            }
            set
            {
                if (value != imagingType)
                {
                    imagingType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("imagingType"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_rightReportConclusions
        {
            get
            {
                return rightReportConclusions;
            }
            set
            {
                if (value != rightReportConclusions)
                {
                    rightReportConclusions = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("rightReportConclusions"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_rightBirads
        {
            get
            {
                return rightBirads;
            }
            set
            {
                if (value != rightBirads)
                {
                    rightBirads = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("rightBirads"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_leftReportConclusions
        {
            get
            {
                return leftReportConclusions;
            }
            set
            {
                if (value != leftReportConclusions)
                {
                    leftReportConclusions = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("leftReportConclusions"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_leftBirads
        {
            get
            {
                return leftBirads;
            }
            set
            {
                if (value != leftBirads)
                {
                    leftBirads = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("leftBirads"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_BIRADS
        {
            get
            {
                return BIRADS;
            }
            set
            {
                if (value != BIRADS)
                {
                    BIRADS = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("BIRADS"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_reportId
        {
            get
            {
                return reportId;
            }
            set
            {
                if (value != reportId)
                {
                    reportId = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("reportId"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_reportIdType
        {
            get
            {
                return reportIdType;
            }
            set
            {
                if (value != reportIdType)
                {
                    reportIdType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("reportIdType"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string BreastImaging_side
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


        /**************************************************************************************************/
        public override void ReleaseListeners(object view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override string GetDiagnosticType()
        {
            return BreastImaging_imagingType;
        }
        public override void SetDiagnosticType(string text)
        {
            BreastImaging_imagingType = text;
        }

        public override string GetValue()
        {
            int leftVal = -1;
            int rightVal = -1;
            try
            {
                if (BreastImaging_leftBirads != null)
                    leftVal = BiradsTrumping[BreastImaging_leftBirads];
                if (BreastImaging_rightBirads != null)
                    rightVal = BiradsTrumping[BreastImaging_rightBirads];
            }
            catch (Exception e)
            {
           
            }

            if (leftVal > rightVal)
                return BreastImaging_leftBirads;
            else if (rightVal > leftVal)
                return BreastImaging_rightBirads;
            else if (rightVal == leftVal)
                return BreastImaging_leftBirads;
            else
                return "";

        }
        public override void SetValue(string text)
        {
            BreastImaging_BIRADS = text;
        }

        public override string GetSide()
        {
            return BreastImaging_side;
        }
        public override void SetSide(string text)
        {
            BreastImaging_side = text;
        }

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("unitnum", unitnum);
            pc.Add("instanceID", instanceID, true);
            pc.Add("reportId", reportId, true);
            pc.Add("reportIdType", reportIdType, true, 30);

            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_BreastImagingStudy",
                                     ref pc);

            this.instanceID = (int)pc["instanceID"].obj;
            this.reportId = (string)pc["reportId"].obj;
            this.reportIdType = (string)pc["reportIdType"].obj;
        }
    }
}

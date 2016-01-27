using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace RiskApps3.Model.MetaData
{
    public class DiseaseObject : HraObject
    {
        [HraAttribute] public int diseaseID;
        [HraAttribute] public string diseaseName;
        [HraAttribute] public string diseaseShortName;
        [HraAttribute] public string diseaseSyndrome;
        [HraAttribute] public string diseaseIconType;
        [HraAttribute] public string diseaseIconArea;
        [HraAttribute] public string diseaseIconColor;
        [HraAttribute] public string diseaseDisplayName;
        [HraAttribute] public string diseaseGender;
        [HraAttribute] public string diseaseOrder;
        [HraAttribute] public string SNOMED;
        [HraAttribute] public string riskMeaning;
        [HraAttribute] public int groupingID;
        [HraAttribute] public string groupingName;

        public override string ToString()
        {
            if (diseaseDisplayName != null)
            {
                return this.diseaseDisplayName;
            }
            else if(diseaseShortName!=null)
            {
                return this.diseaseShortName;
            }
            else if (diseaseName != null)
            {
                return this.diseaseName;
            }
            else
            {
                return "Unknown disease";
            }
        }

        /*****************************************************/
        public string DiseaseObject_diseaseIconType
        {
            get
            {
                return diseaseIconType;
            }
            set
            {
                if (value != diseaseIconType)
                {
                    diseaseIconType = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconType"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string DiseaseObject_diseaseIconArea
        {
            get
            {
                return diseaseIconArea;
            }
            set
            {
                if (value != diseaseIconArea)
                {
                    diseaseIconArea = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconArea"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string DiseaseObject_diseaseIconColor
        {
            get
            {
                return diseaseIconColor;
            }
            set
            {
                if (value != diseaseIconColor)
                {
                    diseaseIconColor = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseIconColor"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string DiseaseObject_diseaseShortName
        {
            get
            {
                return diseaseShortName;
            }
            set
            {
                if (value != diseaseShortName)
                {
                    diseaseShortName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("diseaseShortName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/

        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("diseaseID", diseaseID);
            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_DiseaseObject",
                                      ref pc);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.View;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class FollowupStatus : HraObject
    {
        /**************************************************************************************************/
        [DataMember] public string unitnum;
        [DataMember] public int apptid;

        [DataMember] public double? MaxBRCAProMyriadScore;  //nullable
        [DataMember] public double? MaxLifetimeScore;  //nullable
        [DataMember] public string Diseases;
        [DataMember] public string MammoRec;
        [DataMember] public string MRIRec;
        [DataMember] public string TvsRec;
        [DataMember] public string Ca125Rec;
        [DataMember] public string BreastFamilyGene;
        [DataMember] public string BreastFamilyMutationSignificance;
        [DataMember] public string BreastProbandResult;
        [DataMember] public string AtypiaLCIS;

        //sp_3_GetGeneticRiskGroup populates these:
        //[DataMember] public string GenRiskGroup;
        [DataMember] public string ActionReTesting;
        [DataMember] public string BreastRiskGroup;
        //[DataMember] public string GenRiskImpactOnBreastRisk;
        //[DataMember] public string GenRiskImpactOnOvaryRisk;
        [DataMember] public string BreastScreening;
        [DataMember] public string BreastChemoprevent;

        /**************************************************************************************************/
        public FollowupStatus() { }  // Default constructor for serialization

        public FollowupStatus(string owner_unitnum, int owner_apptid)
        {
            unitnum = owner_unitnum;
            apptid = owner_apptid;
        }

        /**************************************************************************************************/
        public void RemoveViewHandlers(HraView view)
        {
            base.ReleaseListeners(view);
        }

        /**************************************************************************************************/
        public override void BackgroundLoadWork()
        {
            //  This uses the select INTO syntax not supported by azure

            //ParameterCollection pc = new ParameterCollection("unitnum", unitnum);
            //DoLoadWithSpAndParams("sp_3_GetGeneticRiskGroup", pc);

            //pc.Add("apptid", apptid);
            //DoLoadWithSpAndParams("sp_3_LoadFollowupStatus", pc);

        }
    }
}
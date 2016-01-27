

namespace RiskApps3.Model.MetaData
{
    public class MasterParagraph : HraObject
    {
        /**************************************************************************************************/
        [HraAttribute] public int paragraphID;
        [HraAttribute] public string tableName; //lkpSurgicalClinic
        [HraAttribute] public string fieldName; //lkpSurgicalClinic
        [HraAttribute] public string clinicianText; //value1 in lkpSurgicalClinic
        [HraAttribute] public string patientText; //value2 in lkpSurgicalClinic
        [HraAttribute] public string impression; //lkpSurgicalClinicImpressions
        [HraAttribute] public string patientImpression; //lkpSurgicalClinicImpressions
        [HraAttribute] public string proc_desc; //lkpProcedures
        [HraAttribute] public string patientProcDesc; //lkpProcedures
        [HraAttribute] public string Risk; //lkp_3_RiskClinicRecommendations
        [HraAttribute] public string RiskBullet; //lkp_3_RiskClinicRecommendations
        [HraAttribute] public string RuleComment; //lkpParagraphRules
        [HraAttribute] public int displayOrder = -1; //displayOrder

        public string DefaultPatientParagraph;
        public string DefaultProviderParagraph;

        public ProviderParagraph providerSpecific;

        /**************************************************************************************************/
        public string Type
        {
            get
            {
                if (!string.IsNullOrEmpty(fieldName))
                {
                    return fieldName;
                }
                //if (!string.IsNullOrEmpty(impression))
                //{
                //    return "Impression";
                //}
                //if (!string.IsNullOrEmpty(proc_desc))
                //{
                //    return "Procedure";
                //}
                if (!string.IsNullOrEmpty(Risk))
                {
                    return "Risk";
                }
                //if (!string.IsNullOrEmpty(RiskBullet))
                //{
                //    return "Risk";
                //}
                if (!string.IsNullOrEmpty(RuleComment))
                {
                    return "Screening";
                } 

                //if (!string.IsNullOrEmpty(tableName))
                //{
                //    return "Clinic";
                //}
                return "";    
            }
            set
            {
                //
            }
        }

        /**************************************************************************************************/
        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(clinicianText))
                {
                    return clinicianText;
                }
                if (!string.IsNullOrEmpty(impression))
                {
                    return impression;
                }
                if (!string.IsNullOrEmpty(proc_desc))
                {
                    return proc_desc;
                }
                if (!string.IsNullOrEmpty(Risk))
                {
                    return Risk;
                }
                if (!string.IsNullOrEmpty(RuleComment))
                {
                    return RuleComment;
                }
                //if (!string.IsNullOrEmpty(tableName))
                //{
                //    return tableName + " - " + fieldName;
                //}

                return "";
            }
            set
            {
                //
            }
        }
        /**************************************************************************************************/
    }
}
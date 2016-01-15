using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApps3.Model.PatientRecord;
using HRA4.ViewModels;
namespace HRA4.Mapper
{
    public static class RiskScoreMapper
    {
        public static RiskScore ToRiskScore(this RiskProfile RP)
        {
            return new RiskScore()
            {
                RSRiskOfBRCA1andBRCA2 = new RiskOfBRCA1andBRCA2()
                {
                    riskOfBRCA1andBRCA2 = RP.RiskProfile_BrcaPro_1_2_Mut_Prob,
                    MyRaid = RP.RiskProfile_Myriad_Brca_1_2,
                    Tyrer_Cuzick = RP.RiskProfile_TyrerCuzick_Brca_1_2,
                    Tyrer_Cuzick_v7 = RP.RiskProfile_TyrerCuzick_v7_Brca_1_2
                },
                RSBreastCancerRisk = new BreastCancerRisk()
                {

                    BRCAPro_5yearsRisk = RP.RiskProfile_BrcaPro_5Year_Breast,
                    BRCAPro_LifetimeRisk = RP.RiskProfile_BrcaPro_Lifetime_Breast,
                    Claus_5yearsRisk = RP.RiskProfile_Claus_5Year_Breast,
                    Claus_LifetimeRisk = RP.RiskProfile_Claus_Lifetime_Breast,
                    Gail_5yearsRisk = RP.RiskProfile_Gail_5Year_Breast,
                    Gail_LifetimeRisk = RP.RiskProfile_Gail_Lifetime_Breast,
                    Tyrer_Cuzick_5yearsRisk = RP.RiskProfile_TyrerCuzick_5Year_Breast,
                    Tyrer_Cuzick_LifetimeRisk = RP.RiskProfile_TyrerCuzick_Lifetime_Breast,
                    Tyrer_Cuzick_v7_5yearsRisk = RP.RiskProfile_TyrerCuzick_v7_5Year_Breast,
                    Tyrer_Cuzick_v7_LifetimeRisk = RP.RiskProfile_TyrerCuzick_v7_Lifetime_Breast
                },
                RSOvarianCancerRisk = new OvarianCancerRisk()
                {
                    BRCAPro_5yearsRisk = RP.RiskProfile_BrcaPro_5Year_Ovary,
                    BRCAPro_LifetimeRisk = RP.RiskProfile_BrcaPro_Lifetime_Ovary
                },
                RSRiskofLynchSyndromeMutation = new RiskofLynchSyndromeMutation()
                {
                    MMRPRO = RP.MmrPro_1_2_6_Mut_Prob,
                    PREMM2 = (RP.PREMM2 != null) ? RP.RiskProfile_PREMM2 : RP.RiskProfile_PREMM
                },
                RSColonCancerRisk = new ColonCancerRisk()
                {
                    MMRPRO_5yearsRisk = RP.MmrPro_5Year_Colon,
                    MMRPRO_LifetimeRisk=RP.MmrPro_Lifetime_Colon,
                    CCRAT_5yearsRisk=RP.CCRATModel.Details.CCRAT_FiveYear_CRC,
                    CCRAT_LifetimeRisk = RP.CCRATModel.Details.CCRAT_Lifetime_CRC
               
                },
                RSEndometrialRisk = new EndometrialRisk() 
                {
                    MMRPRO_5yearsRisk = RP.MmrPro_5Year_Colon,
                    MMRPRO_LifetimeRisk = RP.MmrPro_Lifetime_Colon
                }
            };
        }
    }
}

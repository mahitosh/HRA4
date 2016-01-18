using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRA4.ViewModels
{
    public class RiskScore
    {
        public RiskOfBRCA1andBRCA2 RSRiskOfBRCA1andBRCA2 { get; set; }
        public BreastCancerRisk RSBreastCancerRisk { get; set; }
        public OvarianCancerRisk RSOvarianCancerRisk { get; set; }
        public RiskofLynchSyndromeMutation RSRiskofLynchSyndromeMutation { get; set; }
        public ColonCancerRisk RSColonCancerRisk { get; set; }
        public EndometrialRisk RSEndometrialRisk { get; set; }
    }



    public class RiskOfBRCA1andBRCA2
    {
        public double? riskOfBRCA1andBRCA2;
        public string S_riskOfBRCA1andBRCA2
        {
            get
            {
                if (riskOfBRCA1andBRCA2 != null)
                    return String.Format("{0:#0.0}", riskOfBRCA1andBRCA2)+"%";
                else
                    return string.Empty;
            }
        }

        public double? MyRaid;
        public string S_MyRaid
        {
            get
            {
                if (MyRaid != null)
                    return String.Format("{0:#0.0}", MyRaid) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick;
        public string S_Tyrer_Cuzick
        {
            get
            {
                if (Tyrer_Cuzick != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick_v7;
        public string S_Tyrer_Cuzick_v7
        {
            get
            {
                if (Tyrer_Cuzick_v7 != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick_v7) + "%";
                else
                    return string.Empty;
            }
        }
    }
    public class BreastCancerRisk
    {
        public double? BRCAPro_5yearsRisk;
        public string S_BRCAPro_5yearsRisk
        {
            get
            {
                if (BRCAPro_5yearsRisk != null)
                    return String.Format("{0:#0.0}", BRCAPro_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? BRCAPro_LifetimeRisk;
        public string S_BRCAPro_LifetimeRisk
        {
            get
            {
                if (BRCAPro_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", BRCAPro_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Claus_5yearsRisk;
        public string S_Claus_5yearsRisk
        {
            get
            {
                if (Claus_5yearsRisk != null)
                    return String.Format("{0:#0.0}", Claus_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Claus_LifetimeRisk;
        public string S_Claus_LifetimeRisk
        {
            get
            {
                if (Claus_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", Claus_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Gail_5yearsRisk;
        public string S_Gail_5yearsRisk
        {
            get
            {
                if (Gail_5yearsRisk != null)
                    return String.Format("{0:#0.0}", Gail_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Gail_LifetimeRisk;
        public string S_Gail_LifetimeRisk
        {
            get
            {
                if (Gail_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", Gail_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick_5yearsRisk;
        public string S_Tyrer_Cuzick_5yearsRisk
        {
            get
            {
                if (Tyrer_Cuzick_5yearsRisk != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick_LifetimeRisk;
        public string S_Tyrer_Cuzick_LifetimeRisk
        {
            get
            {
                if (Tyrer_Cuzick_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick_v7_5yearsRisk;
        public string S_Tyrer_Cuzick_v7_5yearsRisk
        {
            get
            {
                if (Tyrer_Cuzick_v7_5yearsRisk != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick_v7_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? Tyrer_Cuzick_v7_LifetimeRisk;
        public string S_Tyrer_Cuzick_v7_LifetimeRisk
        {
            get
            {
                if (Tyrer_Cuzick_v7_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", Tyrer_Cuzick_v7_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }

    }
    public class OvarianCancerRisk
    {
        public double? BRCAPro_5yearsRisk;
        public string S_BRCAPro_5yearsRisk
        {
            get
            {
                if (BRCAPro_5yearsRisk != null)
                    return String.Format("{0:#0.0}", BRCAPro_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? BRCAPro_LifetimeRisk;
        public string S_BRCAPro_LifetimeRisk
        {
            get
            {
                if (BRCAPro_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", BRCAPro_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }

    }
    public class RiskofLynchSyndromeMutation
    {
        public double? MMRPRO;
        public string S_MMRPRO
        {
            get
            {
                if (MMRPRO != null)
                    return String.Format("{0:#0.0}", MMRPRO) + "%";
                else
                    return string.Empty;
            }
        }
        public double? PREMM2;
        public string S_PREMM2
        {
            get
            {
                if (PREMM2 != null)
                    return String.Format("{0:#0.0}", PREMM2) + "%";
                else
                    return string.Empty;
            }
        }

    }
    public class ColonCancerRisk
    {
        public double? MMRPRO_5yearsRisk;
        public string S_MMRPRO_5yearsRisk
        {
            get
            {
                if (MMRPRO_5yearsRisk != null)
                    return String.Format("{0:#0.0}", MMRPRO_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? MMRPRO_LifetimeRisk;
        public string S_MMRPRO_LifetimeRisk
        {
            get
            {
                if (MMRPRO_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", MMRPRO_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? CCRAT_5yearsRisk;
        public string S_CCRAT_5yearsRisk
        {
            get
            {
                if (CCRAT_5yearsRisk != null)
                    return String.Format("{0:#0.0}", CCRAT_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? CCRAT_LifetimeRisk;
        public string S_CCRAT_LifetimeRisk
        {
            get
            {
                if (CCRAT_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", CCRAT_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }

    }
    public class EndometrialRisk
    {
        public double? MMRPRO_5yearsRisk;
        public string S_MMRPRO_5yearsRisk
        {
            get
            {
                if (MMRPRO_5yearsRisk != null)
                    return String.Format("{0:#0.0}", MMRPRO_5yearsRisk) + "%";
                else
                    return string.Empty;
            }
        }
        public double? MMRPRO_LifetimeRisk;
        public string S_MMRPRO_LifetimeRisk
        {
            get
            {
                if (MMRPRO_LifetimeRisk != null)
                    return String.Format("{0:#0.0}", MMRPRO_LifetimeRisk) + "%";
                else
                    return string.Empty;
            }
        }

    }
}

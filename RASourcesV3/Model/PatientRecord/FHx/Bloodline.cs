using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord.FHx
{
    public class Bloodline
    {
        public enum BloodlineEnum
        {
            Maternal,
            Paternal,
            Both,
            Unknown,
            NULL
        } ;


        public static BloodlineEnum Parse(String bloodlineString)
        {
            if (string.IsNullOrEmpty(bloodlineString))
                return BloodlineEnum.Unknown;


            if (bloodlineString.ToUpper().Equals("MATERNAL"))
            {
                return BloodlineEnum.Maternal;
            }
            else if (bloodlineString.ToUpper().Equals("PATERNAL"))
            {
                return BloodlineEnum.Paternal;
            }
            else
            {
                return BloodlineEnum.Unknown;
            }
        }

        public static String toString(BloodlineEnum bloodline)
        {
            switch (bloodline)
            {
                case BloodlineEnum.Maternal:
                    return "Maternal";
                case BloodlineEnum.Paternal:
                    return "Paternal";
                case BloodlineEnum.Both:
                    return "Both";
                case BloodlineEnum.Unknown:
                    return "Unknown";
                default:
                    return "";
            }
        }
    }
}


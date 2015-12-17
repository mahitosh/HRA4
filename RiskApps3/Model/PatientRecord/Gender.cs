using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.PatientRecord
{
    public enum GenderEnum
    {
        Female,
        Male,
        Unknown
    } ;


    public class Gender
    {
        public static GenderEnum Parse(String genderString)
        {
            if (genderString==null)
            {
                return GenderEnum.Unknown;
            }
            if (genderString.ToUpper().StartsWith("M"))
            {
                return GenderEnum.Male;
            }
            else if (genderString.ToUpper().StartsWith("F"))
            {
                return GenderEnum.Female;
            }
            else
            {
                return GenderEnum.Unknown;
            }
        }

        public static String toString(GenderEnum gender)
        {
            switch (gender)
            {
                case GenderEnum.Female:
                    return "Female";
                case GenderEnum.Male:
                    return "Male";
                default:
                    return "";
            }
        }
    }
}
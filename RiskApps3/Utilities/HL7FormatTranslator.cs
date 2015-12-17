using RiskApps3.Model.PatientRecord.PMH;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RiskApps3.Utilities
{
    public class HL7FormatTranslator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hl7"></param>
        /// <returns></returns>
        public static string GenderFromHL7(string hl7)
        {
            string FEMALE_SNOMED ="248152002";
            string MALE_SNOMED ="248153007";

            string retval = "Unknown";
            if (string.IsNullOrEmpty(hl7)==false)
            {
                if (hl7.ToLower().StartsWith("f") || (hl7 == FEMALE_SNOMED))
                    return "Female";
                else if (hl7.ToLower().StartsWith("m") || (hl7 == MALE_SNOMED))
                    return "Male";
            }
            return retval;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hl7"></param>
        /// <returns></returns>
        public static string DateFromHL7(string hl7)
        {
            string retval = "";

            DateTime target;

            if (DateTime.TryParseExact(hl7, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out target))
            {
                //old
                //retval = target.ToString("dd/MM/yyyy");
                retval = target.ToString("MM/dd/yyyy");
            }

            return retval;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deceasedInd"></param>
        /// <returns></returns>
        public static string VitalStatusFromHL7(string deceasedInd)
        {
            string retval = "Alive";

            if (string.Compare(deceasedInd.ToLower(), "true", true)==0)
                retval = "Dead";

            return retval;
        }


        public static string RaceFromHL7(string code, string displayName)
        {
            string retval = "";
            switch(code)
            {
                case "2054-5":
                    retval = "African American or Black";
                    break;
                case "1002-5":
                    retval = "American Indian/Aleutian/Eskimo";
                    break;
                case "2028-9":
                    retval = "Asian or Pacific Islander";
                    break;
                case "2075-0":
                    retval = "Caribbean/West Indian";
                    break;
                case "2106-3":
                    retval = "Caucasian or White";
                    break;
                default:
                    retval = displayName;
                    break;
            }

            return retval;
        }

        public static string EthnicityFromHL7(string code, string displayName)
        {
            string retval = "";
            switch (code)
            {
                case "81706006":
                    retval = "Ashkenazi";
                    break;
                case "2135-2":
                    retval = "Hispanic or Latino";
                    break;
                case "2186-5":
                    retval = "not Hispanic or Latino";
                    break;
                default:
                    retval = displayName;
                    break;
            }

            return retval;
        }

        public static string GetIntFromHL7HighLow(string low, string high)
        {
            int retval = int.MinValue;
            int x;
            int y;

            bool bLow = int.TryParse(low, out x);
            bool bHigh = int.TryParse(high, out y);

            if (bLow && bHigh)
                retval = (x + y) / 2;

            else if (bLow && !bHigh)
                retval = x;

            else if (!bLow && bHigh)
                retval = y;


            if (retval > int.MinValue)
                return retval.ToString();
            else
                return "";
        }

        internal static void DecodeMenopausalStatus(string statusCode, ref ObGynHistory obGynHx)
        {
            if (obGynHx == null)
                return;

            switch(statusCode)
            {
                case "0":
                    obGynHx.ObGynHistory_menopausalStatus = "Pre";
                    obGynHx.ObGynHistory_currentlyMenstruating = "Yes";
                    break;
                case "1":
                    obGynHx.ObGynHistory_menopausalStatus = "Peri";
                    obGynHx.ObGynHistory_currentlyMenstruating = "Yes";
                    break;
                case "2":
                    obGynHx.ObGynHistory_menopausalStatus = "Post";
                    obGynHx.ObGynHistory_currentlyMenstruating = "No";
                    break;

            }
        }

        internal static string GetInchesFromMeters(string coValue)
        {
            string retval = "";

            double meters;
            if (double.TryParse(coValue,out meters))
            {
                retval = Math.Round((meters * 39.37)).ToString();
            }

            return retval;
        }

        internal static string GetFeetInchesFromMeters(string coValue)
        {
            string retval = "";

            double meters;
            if (double.TryParse(coValue, out meters))
            {
               int inches = (int)(Math.Round((meters * 39.37)));
               retval = (inches / 12).ToString() + "'" + (inches % 12) + "\"";
            }

            return retval;
        }

        internal static string GetPoundsFromKg(string coValue)
        {
            string retval = "";

            double meters;
            if (double.TryParse(coValue, out meters))
            {
                retval = Math.Round((meters * 2.20462)).ToString();
            }

            return retval;
        }

        internal static string GetHRTUse(string statusCode)
        {
            string retval = "";
            switch (statusCode)
            {
                case "0":
                    //retval = "No, never";
                    break;
                case "1": 
                case "2":
                    retval = "Yes, in the past";
                    break;
                case "3":
                    retval = "Yes, currently";
                    break; 
            } 
            return retval;
        }

        internal static string GetHRTType(string statusCode)
        {
            string retval = "";
            switch (statusCode)
            {
                case "0": 
                    break;
                case "1": 
                    retval = "No";
                    break;
                case "2":
                    retval = "Yes";
                    break;

            }
            return retval;
        }

        internal static string GetColonPolypType(string statusCode)
        {
            string retval = "";
            switch (statusCode)
            {
                case "NotDuringPast10Years":
                    retval = "No";
                    break;
                case "DuringPast10Years":
                    retval = "Yes";
                    break;

            }
            return retval;
        }

        internal static string GetColonoscopyType(string statusCode)
        {
            string retval = "";
            switch (statusCode)
            {
                case "NotDuringPast10Years":
                    retval = "No";
                    break;
                case "DuringPast10Years":
                    retval = "Yes";
                    break;

            }
            return retval;
        }

        internal static string GetExcersizeType(string statusCode)
        {
            string retval = "";
            int x = -1;

            if (int.TryParse(statusCode, out x))
            {
                if (x >= 0)
                    retval = x.ToString();
            }
            else
            {
                switch (statusCode)
                {
                    case "Zero":
                        retval = "None";
                        break;
                    case "GTZeroLE2":
                        retval = "Up to 2 hours per week";
                        break;
                    case "GT2LE4":
                        retval = "Between 2-4 hours per week";
                        break;
                    case "GT4":
                        retval = "More than 4 hours per week";
                        break;

                }
            }
            return retval;
        }

        internal static string GetVegetablesType(string statusCode)
        {
            string retval = "";
            int x = -1;

            if (int.TryParse(statusCode, out x))
            {
                if (x >= 0)
                    retval = x.ToString();
            }
            else
            {
                switch (statusCode)
                {
                    case "LT5":
                        retval = "Less than 5 servings per day";
                        break;
                    case "GE5":
                        retval = "5 or more servings per day";
                        break;
                }
            }
            return retval;
        }
    }
}


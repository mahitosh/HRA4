using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class CancerRiskFactors
    {
        public PhysicalDataFactors PhysicalData { get; set; }
        public MensturationFactors MensturationHistory { get; set; }
        public Origin OriginData { get; set; }

    }

    public class Origin
    {
        
        public string AshkenaziJewish { get; set; }
        public List<string> lstAshkenaziJewish { get { return new List<string>() { "Don't Know", "Prefer Not to Answer", "No", "Yes" }; } set { } }
        public string Hispanic { get; set; }
        public List<string> lstHispanic { get { return new List<string>() { "Don't Know", "Prefer Not to Answer", "No", "Yes" }; } set { } }
        public string Race { get; set; }
        public List<string> lstRace { get { return new List<string>() { "African American or Black", "American Indian/Aleutian/Eskimo", "Asian or Pacific Islander", "Caribbean/West Indian", "Caucasian or White", "Other", "Unknown" }; } set { } }

    }


    public class PhysicalData:PhysicalDataFactors
    {
        public string BMI { get; set; }
        public string WeightChange { get; set; }
    }

    public class PhysicalDataFactors
    {
        public string Weight { get; set; }
        public string Feet { get; set; }
        public string Inches { get; set; }
    }    

    public class MensturationFactors
    {
        public string StillHavingPeriods { get; set; }
        public string AgePeriodStopped { get; set; }
    }

    public class MensturationHistory:MensturationFactors
    {
        public string AgeOfFirstPeriod { get; set; }
      
        public virtual string LMP { get; set; }
        public virtual string Confident { get; set; }
       

    }

    

}

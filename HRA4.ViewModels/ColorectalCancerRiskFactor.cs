using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class ColorectalCancer: CancerRiskFactors
    {

        public LifeStyle LifeStyleData { get; set; }

         
    }
    public class LifeStyle 
    {

      public  string RiskDataCustom_numYearsSmokedCigarettes { get; set; }
      public string RiskDataCustom_numCigarettesPerDay { get; set; }
      public string Vegetables { get; set; }
      public List<string> lstddlVegetables { get { return new List<string>() { "Unknown", "None", "Less than 1 serving per day", "1-2 servings per day", "3-4 servings per day", "5-6 servings per day", "7-10 servings per day", "More than 10 servings per day" }; } set { } }
      public string vigorous {get;set;}
      public List<string> lstddlvigorous { get { return new List<string>() { "Unknown", "None", "Up to 1 hour per week", "Between 1-2 hours per week", "Between 2-3 hours per week", "Between 3-4 hours per week", "More than 4 hours per week" }; } set { } }
           
    }



}

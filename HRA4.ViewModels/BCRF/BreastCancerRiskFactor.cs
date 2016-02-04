using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels.BCRF
{
    public class BreastCancerRiskFactor
    {

    }

    public class MensturationHistory
    {
        public string AgeOfFirstPeriod { get; set; }
        public string StillHavingPeriods { get; set; }
        public string LMP { get; set; }
        public string Confident { get; set; }
        public string AgePeriodStopped { get; set; }
         
    }

    public class SocialHistory
    {
        public string Smoking { get; set; }
        public string Alcohol { get; set; }

    }



    public class PhysicalData
    {
        public string Weight { get; set; }
        public string Feet { get; set; }
        public string Inches { get; set; }
    }
}

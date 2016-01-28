using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model
{
    [System.AttributeUsage(System.AttributeTargets.Field)
]
    public class HraAttribute : System.Attribute
    {
        public Boolean persistable=true;
        public Boolean affectsTestingDecision = false;
        public Boolean affectsRiskProfile = false;
        public Boolean auditable = true;

        public HraAttribute()
        {
       
        }
    }
}

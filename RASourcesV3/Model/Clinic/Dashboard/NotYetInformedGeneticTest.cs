using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.Clinic.Dashboard
{
    class NotYetInformedGeneticTest : HraObject
    {
        public String name;
        public String unitnum;
        public String testPanel;
        public DateTime datePanelOrdered;
        public String result;
    }
}

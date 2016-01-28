using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Dashboard
{
    class NotYetInformedGeneticTestList : HRAList<NotYetInformedGeneticTest>
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public NotYetInformedGeneticTestList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            LoadListArgs lla = new LoadListArgs("sp_3_LoadNotYetInformedGeneticTestList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

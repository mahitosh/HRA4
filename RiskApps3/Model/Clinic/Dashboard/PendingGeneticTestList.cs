using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic.Dashboard
{
    class PendingGeneticTestList : HRAList
    {
        public int clinicId = -1;

        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public PendingGeneticTestList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("clinicId", clinicId);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadPendingGeneticTestList",
                                                pc,
                                                typeof(PendingGeneticTest),
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class MutationList : HRAList
    {
        private object[] construction_args;
        private ParameterCollection pc;

        public MutationList()
        {
            this.construction_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs
            (
                "sp_3_LoadMutations",
                this.pc,
                typeof(MutationObject),
                this.construction_args
            );

            DoListLoad(lla);
        }
    }
}

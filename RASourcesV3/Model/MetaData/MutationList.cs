using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class MutationList : HRAList<MutationObject>
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
                this.construction_args
            );

            DoListLoad(lla);
        }
    }
}

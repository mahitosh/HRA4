using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class DefaultParagraphList : HRAList<ProviderParagraph>
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public DefaultParagraphList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs("sp_3_LoadDefaultParagraphs",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

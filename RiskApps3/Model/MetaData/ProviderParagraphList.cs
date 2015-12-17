using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class ProviderParagraphList : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public int providerID;

        public ProviderParagraphList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("providerID", providerID);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadProviderParagraphs",
                                                pc,
                                                typeof(ProviderParagraph),
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}

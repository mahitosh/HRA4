using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.MetaData
{
    [CollectionDataContract]
    [KnownType(typeof(AutomationQueueEntry))]
    class AutomationQueueEntryList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;
        private int includePrinted = 0;
        
        public AutomationQueueEntryList()
        {
            constructor_args = new object[]{};
            this.pc = new ParameterCollection();
        }
        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_Automation",
                this.pc,
                typeof(AutomationQueueEntry),
                this.constructor_args);
            
            DoListLoad(lla);
        }
    }
}

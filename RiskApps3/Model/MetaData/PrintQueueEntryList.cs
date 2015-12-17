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
    [KnownType(typeof(PrintQueueEntry))]
    public class PrintQueueEntryList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;
        private int includePrinted = 0;
        
        public PrintQueueEntryList()
        {
            constructor_args = null;
            this.pc = new ParameterCollection();
            pc.Add("includePrinted", includePrinted);
        }
        public PrintQueueEntryList(bool include)
        {
            includePrinted = include ? 1 : 0;
            constructor_args = null;
            this.pc = new ParameterCollection();
            pc.Add("includePrinted", includePrinted);
        }
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("includePrinted", includePrinted);

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadPrintQueueEntries",
                this.pc,
                typeof(PrintQueueEntry),
                this.constructor_args);
            
            DoListLoad(lla);
        }
    }
}
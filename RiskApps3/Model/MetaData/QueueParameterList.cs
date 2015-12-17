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
    [KnownType(typeof(QueueParameter))]
    public class QueueParameterList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        public QueueParameterList()
        {
            constructor_args = null;
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadQueueParameterList",
                this.pc,
                typeof(QueueParameter),
                this.constructor_args);
            
            DoListLoad(lla);
        }
    }
}

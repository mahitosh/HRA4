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
    [KnownType(typeof(ActiveSurvey))]
    class ActiveSurveyList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        public ActiveSurveyList()
        {
            constructor_args = null; //  new object[] { OwningPatient };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadActiveSurveyList",
                this.pc,
                typeof(ActiveSurvey),
                this.constructor_args);
            
            DoListLoad(lla);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.MetaData
{
    public class DiseaseList : HRAList<DiseaseObject>
    {        
        private object[] constructor_args;
        private ParameterCollection pc;

        public DiseaseList()
        {
            this.constructor_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadDiseases",
                this.pc,
                this.constructor_args);

            DoListLoad(lla);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Communication
{
    class FollowupTimeline : HRAList<PtFollowup>
    {
        private ParameterCollection pc = new ParameterCollection();
        private string patient_unitnum;
        private object[] constructor_args;

        public FollowupTimeline(string unitnum)
        {
            patient_unitnum = unitnum;
            constructor_args = new object[] { null };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", patient_unitnum);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadPtFollowupCommunication",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

        }
    }
}

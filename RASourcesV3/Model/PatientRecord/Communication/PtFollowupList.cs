using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Communication
{
    [CollectionDataContract]
    [KnownType(typeof(PtFollowup))]
    public class PtFollowupList : HRAList<PtFollowup>
    {
        private ParameterCollection pc = new ParameterCollection();
        [DataMember] private Task OwningTask;
        private object[] constructor_args;

        public PtFollowupList() { }  // Default constructor for serialization

        public PtFollowupList(Task parent)
        {
            OwningTask = parent;
            constructor_args = new object[] { OwningTask };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningTask.owningPatient.unitnum);
            pc.Add("taskID", OwningTask.taskID);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadPtFollowupCommunication",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

        }
    }
}

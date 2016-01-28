using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Communication
{
    [CollectionDataContract]
    [KnownType(typeof(Task))]
    public class TaskList : HRAList<Task>
    {
        private ParameterCollection pc = new ParameterCollection();
        
        [DataMember] public Patient OwningPatient;

        private object[] constructor_args;

        public TaskList() { }  // Default constructor for serialization

        public TaskList(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { OwningPatient };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadTaskList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);

        }

    }
}

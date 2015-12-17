using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3.Model.PatientRecord
{
    [CollectionDataContract]
    [KnownType(typeof(SurveyResponse))]
    public class SurveyResponseList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        [DataMember] public Patient OwningPatient;

        public SurveyResponseList()
        {
            this.pc = new ParameterCollection();
        }
        public SurveyResponseList(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { OwningPatient };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum",  OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadSurveyResponses",
                this.pc,
                typeof(SurveyResponse),
                this.constructor_args);
            
            DoListLoad(lla);
        }
    }
}

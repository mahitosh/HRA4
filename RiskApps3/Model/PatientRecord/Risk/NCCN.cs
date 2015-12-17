using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.PatientRecord.Risk
{
    public class NCCN : HRAList
    {
        private ParameterCollection pc = new ParameterCollection();
        private Patient OwningPatient;
        private object[] constructor_args;

        public NCCN(Patient proband)
        {
            OwningPatient = proband;
            constructor_args = new object[] { };
        }
                
        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("unitnum", OwningPatient.unitnum);
            pc.Add("apptid", OwningPatient.apptid);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadNCCN",
                                                pc,
                                                typeof(NCCNFactors),
                                                constructor_args);
            DoListLoad(lla);
        }
    }

    public class NCCNFactors : HraObject
    {
        [HraAttribute (auditable=false)] private string guideline;
        [HraAttribute (auditable=false)] private string satisfied;

        /*****************************************************/
        public string NCCNGuideline_guideline
        {
            get
            {
                return guideline;
            }
            set
            {
                if (value != guideline)
                {
                    guideline = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("guideline"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string NCCNGuideline_satisfied
        {
            get
            {
                return satisfied;
            }
            set
            {
                if (value != satisfied)
                {
                    satisfied = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("satisfied"));
                    SignalModelChanged(args);
                }
            }
        } 

    }
}

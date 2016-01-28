using System;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class AssessmentList : HRAList<Assessment>
    {
        private readonly ParameterCollection pc = new ParameterCollection();

        public override void BackgroundListLoad()
        {
            throw new NotImplementedException("must implement sp!!!");
            pc.Clear(); //TODO create this SP if needed
            LoadListArgs lla = new LoadListArgs("sp3_LoadAssessments", this.pc, null);

            DoListLoad(lla);
        }
    }

    public class Assessment : HraObject
    {
        [Hra] private int _assessmentId;
        [Hra] private string _assessmentName;

        public int AssessmentId { get { return this._assessmentId; } }

        public string AssessmentName { get{return this._assessmentName;} }

        public override string ToString()
        {
            return string.IsNullOrEmpty(this._assessmentName) ? "" : this._assessmentName;
        }
    }
}

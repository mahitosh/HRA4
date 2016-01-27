using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class ClinicList : HRAList <Clinic>
    {
        private ParameterCollection pc = new ParameterCollection();
        private object[] constructor_args;

        public string user_login = "";

        public override void BackgroundListLoad()
        {
            pc.Clear();
            pc.Add("user_login", user_login);
            LoadListArgs lla = new LoadListArgs("sp_3_LoadUserClinicList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }
    }

    public class Clinic : HraObject
    {
        [Hra] public int clinicID;
        [Hra] public string clinicName;
        [Hra] public string defaultAssessmentType;

        public override string ToString()
        {
            return string.IsNullOrEmpty(clinicName) ? "" : clinicName;
        }
    }
}
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic
{
    public class AppointmentList : HRAList<Appointment>
    {
        private readonly ParameterCollection pc = new ParameterCollection();
        private readonly object[] constructor_args;
        public string Date;
        public string NameOrMrn;
        public string groupName;
        public int clinicId;

        public AppointmentList()
        {
            constructor_args = new object[] { };
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            if (string.IsNullOrEmpty(Date) == false)
                pc.Add("Date", Date);
            if (string.IsNullOrEmpty(NameOrMrn) == false)
                pc.Add("NameOrMrn", NameOrMrn);
            if (string.IsNullOrEmpty(groupName) == false)
                pc.Add("groupName", groupName);

            if (clinicId != 0)  //dont check for null - int's init to 0 if not assigned
                pc.Add("clinicId", clinicId);

            pc.Add("userLogin", SessionManager.Instance.ActiveUser.userLogin);

            LoadListArgs lla = new LoadListArgs("sp_3_LoadAppointmentList",
                                                pc,
                                                constructor_args);
            DoListLoad(lla);
        }
    }
}
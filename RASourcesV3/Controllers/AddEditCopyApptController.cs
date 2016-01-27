using System.Windows.Forms;
using RiskAppCore;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.Appointments;
using RiskApps3.View.RiskClinic;
using RiskAppUtils;

namespace RiskApps3.Controllers
{
    public class AddEditCopyApptController
    {
        private const string NoAppointmentSelected = "No appointment selected.";
        private const string Riskapps = "RiskApps";
        private readonly IWin32Window _owner;

        public AddEditCopyApptController(IWin32Window owner)
        {
            _owner = owner;
        }

        public void AddAppt(int clinicId)
        {
            User.setClinicID(clinicId);

            NewAppointmentWizard wizard = new NewAppointmentWizard(clinicId);
            wizard.ShowDialog(_owner);

            SessionManager.Instance.ClearActivePatient();
        }

        public void EditAppt(int clinicId, Appointment appointment)
        {
            MarkStartedAndPullForwardForm mark = new MarkStartedAndPullForwardForm(appointment.apptID, appointment.unitnum);
            mark.ShowDialog();
            User.setClinicID(clinicId);
            AddAppointmentView view = new AddAppointmentView(appointment, clinicId, AddAppointmentView.Mode.Edit);
            view.ShowDialog();

            SessionManager.Instance.ClearActivePatient();
        }

        public void CopyAppt(int clinicId, Appointment toCopy)
        {
            User.setClinicID(clinicId);

            Appointment copiedFromExisting = new Appointment(toCopy, clinicId);

            MarkStartedAndPullForwardForm mark = new MarkStartedAndPullForwardForm(copiedFromExisting.apptID, copiedFromExisting.unitnum);
            mark.ShowDialog();
            
            AddAppointmentView view = new AddAppointmentView(
                copiedFromExisting, 
                clinicId, 
                AddAppointmentView.Mode.Copy);
            view.ShowDialog();
        }

        public void ShowNoAppointmentSelectedErrorMessage()
        {
            MessageBox.Show(NoAppointmentSelected, Riskapps, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

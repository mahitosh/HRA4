using System;
using System.Linq;
using System.Windows.Forms;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.Appointments;

namespace RiskApps3.View.RiskClinic
{
    public partial class NewAppointmentWizard : Form
    {
        private const string PleaseEnterAMedicalRecordNumberToAddAnAppointment = "Please enter a medical record number to add an appointment";
        private readonly GoldenAppointment _appointment;
        private readonly int _clinicId;
        private bool _init;

        public NewAppointmentWizard(int clinicId)
        {
            InitializeComponent();
            this._appointment = new GoldenAppointment();
            this._clinicId = clinicId;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (textBoxMrn.Text.Length == 0)
            {
                MessageBox.Show(PleaseEnterAMedicalRecordNumberToAddAnAppointment);
                return;
            }
            if (!_init)
            {
                _init = true;

                this.Enabled = false;

                this._appointment.MRN = this.textBoxMrn.Text;
                this._appointment.Load();

                AddAppointmentView view;
                if (this._appointment.apptid.HasValue)
                {
                    AppointmentList appts = new AppointmentList();
                    appts.LoadFullList();
                    appts.LoadList();

                    Appointment goldenAppointment = appts.First(appt => appt.apptID == this._appointment.apptid);

                    view = new AddAppointmentView(goldenAppointment, this._clinicId, AddAppointmentView.Mode.Copy);
                }
                else
                {
                    view = new AddAppointmentView(this._appointment.MRN, this._clinicId);
                }
                view.ShowDialog();
                this.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

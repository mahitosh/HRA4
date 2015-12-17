using System;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskAppUtils;

namespace RiskApps3.View.RiskClinic
{
    public partial class NewAppointmentWizard : Form
    {
        private readonly GoldenAppointment _appointment;
        private readonly int _clinicId;
        private bool init = false;

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
                MessageBox.Show("Please enter a medical record number to add an appointment");
                return;
            }
            if (!init)
            {
                init = true;

                this.Enabled = false;

                this._appointment.MRN = this.textBoxMrn.Text;
                this._appointment.Load();

                frmAddEditAppointment addAppt;
                if (this._appointment.apptid.HasValue)
                {
                    addAppt =
                        new frmAddEditAppointment(frmAddEditAppointment.COPY, this._appointment.apptid.Value);
                    addAppt.setClinic(this._clinicId);
                }
                else
                {
                    addAppt =
                        new frmAddEditAppointment(frmAddEditAppointment.ADD);
                    addAppt.setUnitNum(this.textBoxMrn.Text);
                }
                addAppt.ShowDialog();
                this.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

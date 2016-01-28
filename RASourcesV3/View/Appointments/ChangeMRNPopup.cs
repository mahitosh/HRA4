using System;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.View.Appointments
{
    public partial class ChangeMrnPopup : Form
    {
        private readonly Appointment _appointment;
        

        public ChangeMrnPopup(Appointment appointment)
        {
            InitializeComponent();

            this._appointment = appointment;

            unitnumTextBox.Text = this._appointment.unitnum;
        }


        private void Save()
        {
            this._appointment.unitnum = this.newUnitnumTextBox.Text;
            this._appointment.UpdateMrn(this.newUnitnumTextBox.Text);
        }

        private bool OKtoUpdate()
        {
            MrnValidator validator = new MrnValidator(this.newUnitnumTextBox.Text);
            validator.Valdidate();
            return validator.IsValid;
        }


        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {
            if (OKtoUpdate() == false)
            {
                return;
            }
            Save();
            Close();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
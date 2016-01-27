using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.Utilities
{
    public class MrnValidator
    {
        private readonly string _mrn;

        private readonly string _mrnValidationQuery;

        private static string mrnValidationQueryFormat = 
            "SELECT * FROM tblAppointments INNER JOIN tblRiskData ON tblAppointments.apptid=tblRiskData.apptid WHERE unitnum='{0}'";
        
        private static string errorMrnRequired = "New Unit Number is required.";
        private static string mrnProblemCaption = "Problem with MRN";
        private static string MrnInUseMessage
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("Unit Number is already associated with an appointment that has been used in HRA.  ");
                builder.Append("You can change this appointment to use the given MRN, but appointments with this MRN will be permanently associated with one another.  ");
                builder.Append("Choose OK only if you understand the consequences.  To enter a different MRN, choose Cancel.");
                return builder.ToString();
            }
        }

        public MrnValidator(string mrn)
        {
            this._mrn = mrn;
            this._mrnValidationQuery = string.Format(
                mrnValidationQueryFormat, 
                this._mrn);
        }

        public void Valdidate()
        {
            if (this.MrnNullOrEmpty())
            {
                HandleMrnEmpty();
                return;
            }

            if (this.MrnIsInUse())
            {
                HandleMrnInUse();
                return;
            }

            //everything passes
            this.IsValid = true;
        }

        private void HandleMrnInUse()
        {
            DialogResult challenge = ChallengeUser();
            if (challenge.Equals(DialogResult.OK))
            {
                this.IsValid = true;
            }
            else
            {
                this.IsValid = false;
            }
        }

        private void HandleMrnEmpty()
        {
            this.IsValid = false;

            MessageBox.Show(
                errorMrnRequired,
                mrnProblemCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static DialogResult ChallengeUser()
        {
            DialogResult challenge = MessageBox.Show(
                MrnInUseMessage,
                mrnProblemCaption,
                MessageBoxButtons.OKCancel);
            return challenge;
        }

        public bool IsValid { get; private set; }

        private bool MrnNullOrEmpty()
        {
            return string.IsNullOrEmpty(this._mrn);
        }

        private bool MrnIsInUse()
        {
            using (SqlDataReader result = BCDB2.Instance.ExecuteReader(this._mrnValidationQuery))
            {
                if(result == null) return false;
                if (result.Read()) return true;
            }
            return false;
        }
    }
}

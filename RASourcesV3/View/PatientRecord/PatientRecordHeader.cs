using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using System.ComponentModel;
using System.Linq;
using RiskApps3.Model.Clinic;
using RiskApps3.Model;

namespace RiskApps3.View.PatientRecord
{
    public partial class PatientRecordHeader : UserControl
    {
        Patient patient;
        ProviderList Providers;
        private int original_height;

        private bool b_Collapsed = false;

        public bool Collapsed
        {
            get
            {
                return b_Collapsed;
            }
            set
            {
                b_Collapsed = value;
                if (b_Collapsed)
                    this.Height = original_height;
                else
                    this.Height = original_height/2;
            }
        }

        public PatientRecordHeader()
        {
            InitializeComponent();
            original_height = this.Height;
            PatientName.Text = "";
            HomePhone.Text = "";
            age.Text = "";
            MRN.Text = "";
            CellPhone.Text = "";
            DOB.Text = "";
            WorkPhone.Text = "";
            PCP.Text = "";
        }

        public void setPatient(Patient proband)
        {
            if (proband != null)
            {
                PCP.Text = "";

                if (patient != null)
                {
                    patient.ReleaseListeners(this);
                    patient.Providers.ReleaseListeners(this);
                }
                patient = proband;
                Providers = patient.Providers;
                if (patient != null)
                {
                    patient.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
                    patient.Providers.AddHandlersWithLoad(null, ProvidersLoaded, null);
                }
            }
            else
            {
                PatientName.Text = "";
                HomePhone.Text = "";
                age.Text = "";
                MRN.Text = "";
                CellPhone.Text = "";
                DOB.Text = "";
                WorkPhone.Text = "";
                PCP.Text = "";
 
                if (patient != null)
                {
                    patient.ReleaseListeners(this);
                    patient.Providers.ReleaseListeners(this);
                }
                patient = proband;
            }
        }

        /**************************************************************************************************/
        delegate void activePatientChangedCallback(object sender, HraModelChangedEventArgs e);
        private void activePatientChanged(object sender, HraModelChangedEventArgs e)  // todo Examine eventargs and only update those items
        {
            if (PatientName.InvokeRequired)
            {
                activePatientChangedCallback apcc = new activePatientChangedCallback(activePatientChanged);
                object[] args = new object[2];
                args[0] = sender;
                args[1] = e;
                this.Invoke(apcc, args);
            }
            else
            {
                if (patient.name != null)
                {
                    PatientName.Text = patient.name;
                }
                if (patient.age != null)
                {
                    age.Text = patient.age;
                }
            }
        }
        delegate void ProvidersLoadedCallback(HraListLoadedEventArgs e);
        private void ProvidersLoaded(HraListLoadedEventArgs e)
        {
            if (PCP.InvokeRequired)
            {
                ProvidersLoadedCallback apcc = new ProvidersLoadedCallback(ProvidersLoaded);
                object[] args = new object[1];
                args[0] = e;
                this.Invoke(apcc, args);
            }
            else
            {

                foreach (Provider o in Providers.Where(o => o.PCP))
                {
                    PCP.Text = o.fullName;
                    SetPcpVisibility();
                }
            }
        }
        delegate void activePatientLoadedCallback(object sender, RunWorkerCompletedEventArgs e);
        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (PatientName.InvokeRequired)
            {
                activePatientLoadedCallback apcc = new activePatientLoadedCallback(activePatientLoaded);
                object[] args = new object[2];
                args[0] = sender;
                args[1] = e;
                this.Invoke(apcc, args);
            }
            else
            {

                if (patient == null)
                {
                    PatientName.Text = "";
                    age.Text = "";
                    MRN.Text = "";
                    DOB.Text = "";
                    return;
                }

                if (patient.name != null)
                    PatientName.Text = patient.name;

                if (patient.age != null)
                    age.Text = patient.age;

                if (patient.unitnum != null)
                    MRN.Text = patient.unitnum;

                if (patient.dob != null)
                    DOB.Text = patient.dob;

                if (patient.homephone != null)
                    HomePhone.Text = patient.homephone;
                else
                    HomePhone.Text = "";

                if (patient.cellphone != null)
                    CellPhone.Text = patient.cellphone;
                else
                    CellPhone.Text = "";

                if (patient.workphone != null)
                    WorkPhone.Text = patient.workphone;
                else
                    WorkPhone.Text = "";

                SetPcpVisibility();
            }
        }
        private void SetPcpVisibility()
        {
            if (label12.Location.X + label12.Width + TextRenderer.MeasureText(PCP.Text, PCP.Font).Width > this.Width)
            {
                label12.Visible = false;
                PCP.Visible = false;
            }
            else
            {
                label12.Visible = true;
                PCP.Visible = true;
            }
        }

        private void PatientRecordHeader_Resize(object sender, System.EventArgs e)
        {
            SetPcpVisibility();
        }
        public void ReleaseListeners()
        {
            if (patient != null)
            {
                patient.ReleaseListeners(this);
                patient.Providers.ReleaseListeners(this);
            }
        }

    }
}
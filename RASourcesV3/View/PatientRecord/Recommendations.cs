using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;
using System.Data;
using System.Collections.Generic;
using RiskApps3.Controllers;
using System.Diagnostics;
using System.Data.SqlClient;
using RiskApps3.View.PatientRecord.Risk;

namespace RiskApps3.View.PatientRecord
{
    public partial class Recommendations : HraView
    {
        private Patient proband;
        private CDSBreastOvary cdsbo;

        public Recommendations()
        {
            InitializeComponent();
            SetupControls();
        }

        private void SetupControls()
        {

            Recommendation blank = new Recommendation();
            blank.recID = -1;
            blank.recName = "";
            blank.recValue = "";


            cbeRecClinComboBox.Items.Add(blank);
            chemoRecClinComboBox.Items.Add(blank);
            mammoRecClinComboBox.Items.Add(blank);
            prophMastRecClinComboBox.Items.Add(blank);
            mriRecClinComboBox.Items.Add(blank);
            tvsRecClinComboBox.Items.Add(blank);
            ca125RecClinComboBox.Items.Add(blank);
            prophOophRecClinComboBox.Items.Add(blank);
            ocRecClinComboBox.Items.Add(blank);


            List<Recommendation> AllPossibleRecommendationList = new List<Recommendation>();        

            AllPossibleRecommendationList = new List<Recommendation>(
                       (from dRow in SessionManager.Instance.MetaData.BrOvCdsRecs.Recs.AsEnumerable()
                        select (GetRecDataTableRow(dRow)))
                       );
        

            String controlName;
            foreach (Recommendation r in AllPossibleRecommendationList)
            {
                controlName = r.recName + "ClinComboBox";

                Control c = GetControlByNameIgnoreCase(controlName, this);
                if (c != null)
                {
                    ComboBox comboBox = (ComboBox)c;
                    if (comboBox.Items.Contains(r) == false)
                    {
                        comboBox.Items.Add(r);
                    }
                }
            }

        }

        private void Recommendations_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += NewActivePatient;

                InitNewPatient();
            }
        }
        /**************************************************************************************************/

        private void InitNewPatient()
        {
            //  get active patient object from session manager
            proband = SessionManager.Instance.GetActivePatient();

            ClearControls();

            if (proband != null)
            {
                proband.AddHandlersWithLoad(activePatientChanged, activePatientLoaded, null);
            }
        }

        /**************************************************************************************************/

        private void ClearControls()
        {
            foreach (Control c in Controls)
            {
                String className = c.GetType().ToString();

                if (className.EndsWith("TextBox") || className.EndsWith("ComboBox"))
                {
                    c.Text = "";
                }
                if (className.EndsWith("ChangedAlertLabel"))
                {
                    c.Visible = false;
                }
            }
        }

        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }


        /**************************************************************************************************/

        private void activePatientChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/

        private void activePatientLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadOrGetCDSRecs();
        }

        /**************************************************************************************************/

        private void LoadOrGetCDSRecs()
        {
            //  get active patient object from session manager
            cdsbo = SessionManager.Instance.GetActivePatient().cdsBreastOvary;

            if (cdsbo != null)
            {
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;

                cdsbo.AddHandlersWithLoad(CDSBreastOvaryChanged, CDSBreastOvaryLoaded, null);
            }
        }


        /**************************************************************************************************/

        private void CDSBreastOvaryChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillCDSBreastOvaryControls();
            }
        }

        /**************************************************************************************************/

        private void CDSBreastOvaryLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillCDSBreastOvaryControls();
        }


        /**************************************************************************************************/
        delegate void FillCDSBreastOvaryControlsCallback();
        private void FillCDSBreastOvaryControls()
        {
            if (this.InvokeRequired)
            {
                FillCDSBreastOvaryControlsCallback rmc = new FillCDSBreastOvaryControlsCallback(FillCDSBreastOvaryControls);
                this.Invoke(rmc, null);
            }
            else
            {
                //breast
                cbeRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.CBERec);
                chemoRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.ChemoRec);
                mammoRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.MammoRec);
                prophMastRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.ProphMastRec);
                mriRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.MRIRec);

                //ovary
                tvsRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.TVSRec);
                ca125RecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.CA125Rec);
                prophOophRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.ProphOophRec);
                ocRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.OCRec);

                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
        }


        private void Recommendations_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cdsbo != null)
                cdsbo.ReleaseListeners(this);
            if (proband != null)
                proband.ReleaseListeners(this);
            
            SessionManager.Instance.RemoveHraView(this);
        }

        private Recommendation GetRecDataTableRow(DataRow dr)
        {
            Recommendation oRec = new Recommendation();
            oRec.recID = (int)dr["recID"];
            oRec.recName = dr["recName"].ToString();
            oRec.recValue = dr["recValue"].ToString();
            oRec.paragraph = dr["paragraph"].ToString();
            if (dr["bullet"] != null)
            {
                oRec.bullet = dr["bullet"].ToString();
            }
            oRec.patientParagraph = dr["patientParagraph"].ToString();
            return oRec;
        }

        public class Recommendation
        {
            private string strRecName = "";
            private string strRecValue = "";

            public int recID = 0;
            public string paragraph = "";
            public string patientParagraph = "";
            public string bullet = "";

            public string recName
            {
                get { return strRecName; }
                set { strRecName = value; }

            }
            public string recValue
            {
                get { return strRecValue; }
                set { strRecValue = value; }

            }
            public string recValueAndText
            {
                get { return strRecValue;}
                set {  }

            }
            public override string ToString()
            {
                return strRecValue;
            }
        }

        private Control GetControlByNameIgnoreCase(string Name, Control parent)
        {
            if (string.Equals(parent.Name, Name, StringComparison.CurrentCultureIgnoreCase))
                return parent;
            else
            {
                foreach (Control child in parent.Controls)
                {
                    Control retval = GetControlByNameIgnoreCase(Name, child);
                    if (retval != null)
                        return retval;
                }
            }
            
            return null;
        }

        private void cbeRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)cbeRecClinComboBox.SelectedItem;
            cdsbo.CBERec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("CBERec"));
            cdsbo.SignalModelChanged(args);

        }

        private void chemoRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)chemoRecClinComboBox.SelectedItem;
            cdsbo.ChemoRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("ChemoRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void mammoRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)mammoRecClinComboBox.SelectedItem;
            cdsbo.MammoRec = r.recID;

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("MammoRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void prophMastRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)prophMastRecClinComboBox.SelectedItem;
            cdsbo.ProphMastRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("ProphMastRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void mriRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)mriRecClinComboBox.SelectedItem;
            cdsbo.MRIRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("MRIRec"));
            cdsbo.SignalModelChanged(args);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cdsbo.LoadObject();
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }


        private void tvsRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)tvsRecClinComboBox.SelectedItem;
            cdsbo.TVSRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("TVSRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void ca125RecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ca125RecClinComboBox.SelectedItem;
            cdsbo.CA125Rec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("CA125Rec"));
            cdsbo.SignalModelChanged(args);
        }

        private void prophOophRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)prophOophRecClinComboBox.SelectedItem;
            cdsbo.ProphOophRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("ProphOophRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void ocRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ocRecClinComboBox.SelectedItem;
            cdsbo.OCRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("OCRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void RecClinComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DisplayMember = "recValueAndText";
        }
        private void RecClinComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.DisplayMember = "recValue";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)cbeRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet,r.paragraph);
            rdp.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)chemoRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)mammoRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)prophMastRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)mriRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)tvsRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ca125RecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)prophOophRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ocRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

    }
}
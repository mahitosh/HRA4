using System;
using System.ComponentModel;
using System.Drawing;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Windows.Forms;
using System.Reflection;
using RiskApps3.Controllers;
using System.Data;
using RiskApps3.Utilities;
using System.Collections.Generic;
using RiskApps3.View.PatientRecord.Risk;
using System.Linq;
using RiskApps3.Model.PatientRecord.Risk;


namespace RiskApps3.View.PatientRecord
{
    public partial class GenTestRecommendationsView : HraView
    {
        /**************************************************************************************************/
        private Patient proband;
        private CDSBreastOvary cdsbo;

        private RiskApps3.View.Risk.SimpleRiskModelView.ChangeSyndromeCallbackType ChangeSyndromeCallback;

        /**************************************************************************************************/
        public GenTestRecommendationsView()
        {
            InitializeComponent();
            //backgroundWorker1.RunWorkerAsync();
            SetupControls();
        }

        private void SetupControls()
        {

            if (SessionManager.Instance.MetaData.Globals.NCCN == false)
            {
                label9.Visible = false;
                label7.Visible = false;
                bitmapButton2.Visible = false;
            }
            
        }


        /**************************************************************************************************/
        private void GenTestRecommendationsView_Load(object sender, EventArgs e)
        {
            if (ViewClosing == false)
            {
                SessionManager.Instance.NewActivePatient +=
                    new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
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
                proband.FHx.AddHandlersWithLoad(FHChanged, FHLoaded, FHItemChanged);
            }
        }
        private void FHItemChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                foreach (MemberInfo mi in e.updatedMembers)
                {
                    if (HraObject.DoesAffectTestingDecision(mi))
                    {
                        reCreateListOfRelativesToConsider();
                        break;
                    }
                }
            }
        }
        /**************************************************************************************************/
        private string decorateNullableScore(double? nullableScore)
        {
            double score = nullableScore ?? -1;
            if (score >= 0)
                return String.Format("{0:#0.0}", Math.Round(score, 1)) + "%";
            else
                return "";
        }
        private void RelativeRPLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            bool completed = true;
            foreach (Person fm in SessionManager.Instance.GetActivePatient().FHx.Relatives)
            {
                if (!(fm.RP.HraState == HraObject.States.Ready))
                {
                    completed = false;
                    break;
                }
                
            }

            if (completed)
                reCreateListOfRelativesToConsider();

        }

        //check age string if greater than or equal to 18 (or if not present)
        private bool AgeGE18(string s)
        {
            double number;

            if (String.IsNullOrEmpty(s))
                return true;

            if (Double.TryParse(s, out number))
                return (number >= 18);

            return true;
        }

        /**************************************************************************************************/
        delegate void reCreateListOfRelativesToConsiderCallback();
        private void reCreateListOfRelativesToConsider()
        {
            if (this.InvokeRequired)
            {
                reCreateListOfRelativesToConsiderCallback rmc = new reCreateListOfRelativesToConsiderCallback(reCreateListOfRelativesToConsider);
                this.Invoke(rmc, null);
            }
            else
            {
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();

                mutBRCA.Text = decorateNullableScore(proband.RP.RiskProfile_BrcaPro_1_2_Mut_Prob);
                mutMyriad.Text = decorateNullableScore(proband.RP.RiskProfile_Myriad_Brca_1_2);
                mutTC.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_Brca_1_2);
                mutTC7.Text = decorateNullableScore(proband.RP.RiskProfile_TyrerCuzick_v7_Brca_1_2);
                MMRProLabel.Text = decorateNullableScore(proband.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob);
                label10.Text = (proband.RP.PREMM == null) ? "PREMM2" : "PREMM";
                PremmLabel.Text = decorateNullableScore((proband.RP.PREMM2 != null) ? proband.RP.RiskProfile_PREMM2 : proband.RP.RiskProfile_PREMM);

                //For V3, we'll list all alive relatives >=18 y.o. (or age unavailable) w/BrcaPro Score >= 10% and no existing gen tests in descending order
                List<Person> familyMembers = SessionManager.Instance.GetActivePatient().FHx.Relatives;

                //Is this the coolest statement or what?
                List<Person> filteredList = familyMembers
                    .Where(x => (x.vitalStatus != "Dead") && (x.RP.RiskProfile_BrcaPro_1_2_Mut_Prob >= 10.0) && (x.PMH.GeneticTests.Count == 0) && AgeGE18(x.age))
                    .OrderByDescending(x => x.RP.RiskProfile_BrcaPro_1_2_Mut_Prob)
                    .ToList();

                //Add the control rows.
                foreach (Person p in filteredList)
                {
                    RelativeToConsiderRow r = new RelativeToConsiderRow(this);
                    r.Width = flowLayoutPanel1.Width - 6;
                    //r.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    //        | System.Windows.Forms.AnchorStyles.Right)));
                    r.SetLabels(p.relativeID, String.Format("{0:#0.0}", Math.Round(p.RP.RiskProfile_BrcaPro_1_2_Mut_Prob ?? -1, 1)), p.name, p.relationship);
                    flowLayoutPanel1.Controls.Add(r);
                }

                //Is this the coolest statement or what?
                List<Person> filteredMmrList = familyMembers
                    .Where(x => (x.vitalStatus != "Dead") && (x.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob >= 10.0 || x.RP.RiskProfile_PREMM >= 10.0) && (x.PMH.GeneticTests.Count == 0) && AgeGE18(x.age))
                    .OrderByDescending(x => x.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob)
                    .ToList();

                //Add the control rows.
                foreach (Person p in filteredMmrList)
                {
                    RelativeToConsiderRow r = new RelativeToConsiderRow(this);
                    r.Width = flowLayoutPanel1.Width - 6;
                    //        r.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    //| System.Windows.Forms.AnchorStyles.Right)));
                    r.ShowDisposition = false;
                    r.SetLabels(p.relativeID, String.Format("{0:#0.0}", Math.Round(p.RP.RiskProfile_MmrPro_1_2_6_Mut_Prob ?? -1, 1)), p.name, p.relationship);
                    flowLayoutPanel2.Controls.Add(r);
                }

                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
        }
        private void FHChanged(HraListChangedEventArgs e)
        {
            //TODO not sure
        }

        private void FHLoaded(HraListLoadedEventArgs e)
        {
            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;

            foreach (Person fm in SessionManager.Instance.GetActivePatient().FHx.Relatives)
            {
                fm.RP.AddHandlersWithLoad(null, RelativeRPLoaded, null);
            }




            if (SessionManager.Instance.MetaData.Globals.NCCN == true)
            {
                loadingCircle2.Active = true;
                loadingCircle2.Visible = true;
                proband.RP.NCCNGuideline.AddHandlersWithLoad(NCCNChanged, NCCNLoaded, NCCNItemChanged);
            }

        
        }
        /**************************************************************************************************/
        private void NCCNItemChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void NCCNChanged(HraListChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void NCCNLoaded(HraListLoadedEventArgs list_e)
        {
            if (list_e != null)
            {
                ///listView1.Items.Clear();
                NCCN guideline = (NCCN)list_e.sender;
                string overall = "";
                foreach (NCCNFactors factor in guideline)
                {
                    if (factor.NCCNGuideline_satisfied == "Yes")
                        overall = "Yes";
                    else
                    {
                        if (overall.Length == 0)
                        {
                            overall = "No";
                        }
                    }
                }
                label7.Text = overall;
                bitmapButton2.Visible = true;
                //bitmapButton1.Visible = true;
                label7.Visible = true;

                loadingCircle2.Active = false;
                loadingCircle2.Visible = false;
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
            SetupRecDropDowns();
            LoadOrGetCDSRecs();
        }

        private void SetupRecDropDowns()
        {
            Recommendation blank = new Recommendation();
            blank.recID = -1;
            blank.recName = "";
            blank.recValue = "";

            Recommendation na = new Recommendation();
            na.recID = -1;
            na.recName = "";
            na.recValue = "N/A";


            genTestRecClinComboBox.Items.Add(blank);
            MmrTestRecComboBox.Items.Add(blank);
            MmrTestResultComboBox.Items.Add(blank);
            ScreeningRecComboBox.Items.Add(blank);
            GenTestresultComboBox.Items.Add(blank);

            genTestRecClinComboBox.Items.Add(na);
            MmrTestRecComboBox.Items.Add(na);
            MmrTestResultComboBox.Items.Add(na);
            ScreeningRecComboBox.Items.Add(na);
            GenTestresultComboBox.Items.Add(na);

            List<Recommendation> AllPossibleRecommendationList = new List<Recommendation>();
            AllPossibleRecommendationList = new List<Recommendation>(
           (from dRow in SessionManager.Instance.MetaData.BrOvCdsRecs.Recs.AsEnumerable()
            select (GetRecDataTableRow(dRow)))
           );

            foreach (Recommendation r in AllPossibleRecommendationList)
            {
                if (proband.gender.ToLower().StartsWith("f"))
                {
                    if (r.recValue.Contains(" Male"))
                    {
                        continue;
                    }
                    else
                    {
                        r.recValue = r.recValue.Replace(" Female", "");
                    }
                }
                else
                {
                    if (r.recValue.Contains(" Female"))
                    {
                        continue;
                    }
                    else
                    {
                        r.recValue = r.recValue.Replace(" Male", "");
                    }
                }

                if (string.Compare(r.recName, "genTestRec", true) == 0)
                {
                    genTestRecClinComboBox.Items.Add(r);
                }
                else if (string.Compare(r.recName, "Breast Density", true) == 0)
                {
                    ScreeningRecComboBox.Items.Add(r);
                }
                else if (string.Compare(r.recName, "GenTestResult", true) == 0)
                {
                    GenTestresultComboBox.Items.Add(r);
                }
                else if (string.Compare(r.recName, "ColonGenTestRec", true) == 0)
                {
                    MmrTestRecComboBox.Items.Add(r);
                }
                else if (string.Compare(r.recName, "ColonGenTestResult", true) == 0)
                {
                    MmrTestResultComboBox.Items.Add(r);
                }
            }
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
                genTestRecClinComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.GenTestRec);
                ScreeningRecComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.ScreeningRec);
                GenTestresultComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.GenTestResult);
                MmrTestRecComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.MmrGenTestRec);
                MmrTestResultComboBox.Text = SessionManager.Instance.MetaData.BrOvCdsRecs.GetRecTextFromID(cdsbo.MmrGenTestResult);

                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

            }
        }
        /**************************************************************************************************/
        private void GenTestRecommendationsView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
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
                get { return strRecValue + "  -  " + paragraph; }
                set { }

            }
            public override string ToString()
            {
                return strRecValue;
            }
        }

        private Control GetControlByNameIgnoreCase(string Name)
        {
            foreach (Control c in this.Controls)
                if (string.Equals(c.Name, Name, StringComparison.CurrentCultureIgnoreCase))
                    return c;

            return null;
        }

        private void genTestRecClinComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)genTestRecClinComboBox.SelectedItem;
            cdsbo.GenTestRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("GenTestRec"));
            cdsbo.SignalModelChanged(args);

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)MmrTestRecComboBox.SelectedItem;
            cdsbo.MmrGenTestRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("MmrGenTestRec"));
            cdsbo.SignalModelChanged(args);
        }

        internal void Register(RiskApps3.View.Risk.SimpleRiskModelView.ChangeSyndromeCallbackType ChangeSyndromeDelegate)
        {
            ChangeSyndromeCallback = ChangeSyndromeDelegate;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (ChangeSyndromeCallback != null)
            {
                ChangeSyndromeCallback.Invoke(e.TabPage.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NCCNView nccnv = new NCCNView();
                nccnv.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadingCircle2.Active = true;
            loadingCircle2.Visible = true;
            proband.RP.NCCNGuideline.LoadList();
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ScreeningRecComboBox.SelectedItem;
            cdsbo.ScreeningRec = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("ScreeningRec"));
            cdsbo.SignalModelChanged(args);
        }

        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)GenTestresultComboBox.SelectedItem;
            cdsbo.GenTestResult = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("GenTestResult"));
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

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)genTestRecClinComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)ScreeningRecComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)GenTestresultComboBox.SelectedItem;
            RecommendationDetailsPopup rdp = new RecommendationDetailsPopup(r.bullet, r.paragraph);
            rdp.ShowDialog();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Recommendation r = (Recommendation)MmrTestResultComboBox.SelectedItem;
            cdsbo.MmrGenTestResult = r.recID;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            args.updatedMembers.Add(cdsbo.GetMemberByName("MmrGenTestResult"));
            cdsbo.SignalModelChanged(args);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord.Labs;
using RiskApps3.View.PatientRecord.Imaging;
using RiskApps3.View.PatientRecord.Labs;

namespace RiskApps3.View.PatientRecord
{
    public partial class TestsView : HraView
    {
        Patient proband;
        
        public Diagnostic InitialDx;

        public TestsView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void TestsView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
                dateTimePicker1.Text = "";
                InitNewPatient();
            }
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            //  get active patient object from session manager
            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                proband.breastImagingHx.AddHandlersWithLoad(theBreastImagingHxChanged, theBreastImagingHxLoaded, DiagnosticChanged);
                proband.transvaginalImagingHx.AddHandlersWithLoad(theTransvaginalImagingHxChanged, theTransvaginalImagingHxLoaded, DiagnosticChanged);
                proband.labsHx.AddHandlersWithLoad(theLabsChanged, theLabsLoaded, DiagnosticChanged);
                //patientSummaryHeader1.InitNewPatient();
            }
        }
        private void DiagnosticChanged(object sender, HraModelChangedEventArgs e)
        {
            fastDataListView1.RefreshObject(sender);
            if (fastDataListView1.SelectedObject == sender)
            {
                Diagnostic dx = (Diagnostic)sender;
                comboBox1.Text = dx.normal;
                ValueLabel.Text = " - " + dx.value;
                ValueLabel.Location = new Point(label5.Location.X + label5.Width , label5.Location.Y);
                comboBox3.Text = dx.status;
                richTextBox1.Text = dx.report;
            }
        }

        /**************************************************************************************************/
        private void theBreastImagingHxLoaded(HraListLoadedEventArgs e)
        {
            fastDataListView1.AddObjects(proband.breastImagingHx);
            if (InitialDx != null)
            {
                if (proband.breastImagingHx.Contains(InitialDx))
                {
                    fastDataListView1.SelectObject(InitialDx);
                }
            }
            else
            {
                if (fastDataListView1.SelectedObject == null)
                {
                    if (fastDataListView1.Items.Count > 0)
                        fastDataListView1.SelectedIndex = 0;
                }
            }
        }
        /**************************************************************************************************/
        private void theBreastImagingHxChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                BreastImagingStudy theStudy = (BreastImagingStudy)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        fastDataListView1.AddObject(theStudy);
                        fastDataListView1.SelectedObject = theStudy;
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        RemoveDxFromList(theStudy);
                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void RemoveDxFromList(Diagnostic theStudy)
        {
            int i = fastDataListView1.SelectedIndex;
            fastDataListView1.RemoveObject(theStudy);
            if (fastDataListView1.Items.Count > i)
            {
                fastDataListView1.SelectedIndex = i;
            }
            else
            {
                comboBox1.Text = "";
                ValueLabel.Text = "";
                ValueLabel.Location = new Point(label5.Location.X + label5.Width, label5.Location.Y);
                comboBox3.Text = "";
                dateTimePicker1.Text = "";
                richTextBox1.Text = "";
                panel3.Controls.Clear();
            }
        }
        /**************************************************************************************************/
        private void theTransvaginalImagingHxLoaded(HraListLoadedEventArgs e)
        {
            fastDataListView1.AddObjects(proband.transvaginalImagingHx);
            if (InitialDx != null)
            {
                if (proband.transvaginalImagingHx.Contains(InitialDx))
                {
                    fastDataListView1.SelectObject(InitialDx);
                }
            }
            else
            {
                if (fastDataListView1.SelectedObject == null)
                {
                    if (fastDataListView1.Items.Count > 0)
                        fastDataListView1.SelectedIndex = 0;
                }
            }
        }
        /**************************************************************************************************/
        private void theTransvaginalImagingHxChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                TransvaginalImagingStudy theStudy = (TransvaginalImagingStudy)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        fastDataListView1.AddObject(theStudy);
                        fastDataListView1.SelectedObject = theStudy;
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        RemoveDxFromList(theStudy);
                        break;
                }
            }
        }

        /**************************************************************************************************/
        private void theLabsLoaded(HraListLoadedEventArgs e)
        {
            fastDataListView1.AddObjects(proband.labsHx);
            if (InitialDx != null)
            {
                if (proband.labsHx.Contains(InitialDx))
                {
                    fastDataListView1.SelectObject(InitialDx);
                }
            }
            else
            {
                if (fastDataListView1.SelectedObject == null)
                {
                    if (fastDataListView1.Items.Count > 0)
                        fastDataListView1.SelectedIndex = 0;
                }
            }
        }
        /**************************************************************************************************/
        private void theLabsChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                RiskApps3.Model.PatientRecord.Labs.LabResult theStudy = (RiskApps3.Model.PatientRecord.Labs.LabResult)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                        fastDataListView1.AddObject(theStudy);
                        fastDataListView1.SelectedObject = theStudy;
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        RemoveDxFromList(theStudy);
                        break;
                }
            }
        }

        private void fastDataListView1_SelectionChanged(object sender, EventArgs e)
        {
            this.label5.Visible = true;
            this.ValueLabel.Visible = true;

            object o = fastDataListView1.SelectedObject;
            Diagnostic dx = (Diagnostic)o;
            if (o != null)
            {
                panel2.Visible = true;

                label5.Text = dx.type;
                if (dx.date > DateTime.MinValue && dx.date < DateTime.MaxValue)
                {
                    dateTimePicker1.Value = dx.date;
                }
                comboBox1.Text = dx.normal;
                ValueLabel.Text = " - " + dx.value;
                ValueLabel.Location = new Point(label5.Location.X + label5.Width , label5.Location.Y);

                comboBox3.Text = dx.status;
                richTextBox1.Text = dx.report;
                panel3.Controls.Clear();
                if (dx is BreastImagingStudy)
                {
                    MammographySummary ms = new MammographySummary((BreastImagingStudy)dx);
                    panel3.Controls.Add(ms);
                }
                else if (dx is TransvaginalImagingStudy)
                {
                    TvsSummary tvs = new TvsSummary((TransvaginalImagingStudy)dx);
                    panel3.Controls.Add(tvs);
                }
                else if (dx is LabResult)
                {
                    LabSummary ls = new LabSummary((LabResult)dx);
                    panel3.Controls.Add(ls);
                }

            }
        }

        private void dateTimePicker1_Validated(object sender, EventArgs e)
        {
            object o = fastDataListView1.SelectedObject;
            if (o != null)
            {
                Diagnostic dx = (Diagnostic)o;
                dx.Dx_date = dateTimePicker1.Value;
                //fastDataListView1.RefreshObject(o);
            }
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            object o = fastDataListView1.SelectedObject;
            if (o != null)
            {
                Diagnostic dx = (Diagnostic)o;
                dx.Dx_normal = comboBox1.Text;
                //fastDataListView1.RefreshObject(o);
            }
        }

        private void comboBox2_Validated(object sender, EventArgs e)
        {
            //object o = fastDataListView1.SelectedObject;
            //if (o != null)
            //{
            //    Diagnostic dx = (Diagnostic)o;
            //    dx.SetValue(comboBox2.Text);
            //    fastDataListView1.RefreshObject(o);
            //}
        }

        private void comboBox3_Validated(object sender, EventArgs e)
        {
            object o = fastDataListView1.SelectedObject;
            if (o != null)
            {
                Diagnostic dx = (Diagnostic)o;
                dx.Dx_status = comboBox3.Text;
                //fastDataListView1.RefreshObject(o);
            }
        }

        private void richTextBox1_Validated(object sender, EventArgs e)
        {
            object o = fastDataListView1.SelectedObject;
            if (o != null)
            {
                Diagnostic dx = (Diagnostic)o;
                dx.Dx_report = richTextBox1.Text;
                //fastDataListView1.RefreshObject(o);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object o = fastDataListView1.SelectedObject;
            Diagnostic dx = (Diagnostic)o;
            if (o != null)
            {
                if (proband.labsHx.Contains(dx))
                {
                    proband.labsHx.RemoveFromList(dx, SessionManager.Instance.securityContext);
                }
                else if (proband.breastImagingHx.Contains(dx))
                {
                    proband.breastImagingHx.RemoveFromList(dx, SessionManager.Instance.securityContext);
                }
                else if (proband.transvaginalImagingHx.Contains(dx))
                {
                    proband.transvaginalImagingHx.RemoveFromList(dx, SessionManager.Instance.securityContext);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button2.Location.X + button2.Width,button2.Location.Y+button2.Height);
        }

        private void TestsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ValidateChildren();
            //patientSummaryHeader1.ReleaseListeners();
            SessionManager.Instance.RemoveHraView(this);
        }

        private void mammographyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BreastImagingStudy bis = new BreastImagingStudy();
            bis.unitnum = proband.unitnum;
            bis.type = "MammographyHxView";
            bis.date = DateTime.Today;
            bis.imagingType = "MammographyHxView";
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            proband.breastImagingHx.AddToList(bis, args);

        }

        private void mRIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BreastImagingStudy bis = new BreastImagingStudy();
            bis.unitnum = proband.unitnum;
            bis.type = "MRI";
            bis.date = DateTime.Today;
            bis.imagingType = "MRI";
            bis.side = "Bilateral";
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            proband.breastImagingHx.AddToList(bis, args);
        }

        private void tVSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TransvaginalImagingStudy tvs = new TransvaginalImagingStudy();
            tvs.unitnum = proband.unitnum;
            tvs.type = "TVS";
            tvs.date = DateTime.Today;
            tvs.imagingType = "TVS";
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            proband.transvaginalImagingHx.AddToList(tvs, args);
        }

        private void cA125ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LabResult lr = new LabResult();
            lr.unitnum = proband.unitnum;
            lr.date = DateTime.Today;
            lr.TestDesc = "CA125";
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(this);
            proband.labsHx.AddToList(lr, args);

        }

    }
}

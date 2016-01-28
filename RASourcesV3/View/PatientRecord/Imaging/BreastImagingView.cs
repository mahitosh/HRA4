using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;

namespace RiskApps3.View.PatientRecord.Imaging
{
    public partial class BreastImagingView : HraView
    {
        /**************************************************************************************************/
        BreastImagingHx theBreastImagingHx;

        /**************************************************************************************************/
        public BreastImagingView()
        {
            InitializeComponent();

            chart1.Application = "SZ7Xdm7cflDtBo4+WqVIPuWTiHM5kccphrHW2GjvTk4bKM4ygUUoF+CkZOtrPEEtVQERA/1RWBUebtVBt4PLmQCFIDsJrzpm1pCPKOH1Ad8=";
            chart1.TempDirectory = "temp";
            chart1.Debug = true;
            chart1.Palette = new Color[] { Color.Gray, Color.FromArgb(255, 255, 0), Color.FromArgb(255, 99, 49), Color.FromArgb(0, 156, 255) };
            chart1.Type = dotnetCHARTING.WinForms.ChartType.Combo;
            chart1.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time;

            chart1.LegendBox.Visible = false;
            chart1.YAxis.Clear();
            chart1.ChartArea.ClearColors();
            chart1.ChartArea.Background.Color = Color.White;
            chart1.ChartArea.Line.Color = Color.Black;
            chart1.ChartArea.Label.Text = "";
            chart1.XAxis.Line.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            chart1.XAxis.Line.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            chart1.XAxis.Line.AnchorCapScale = 4;
            chart1.XAxis.StaticColumnWidth = 1;
            chart1.XAxis.TimeScaleLabels.Mode = dotnetCHARTING.WinForms.TimeScaleLabelMode.Smart;

            chart1.DefaultElement.Annotation = new dotnetCHARTING.WinForms.Annotation("%Name");
            chart1.DefaultElement.Annotation.HeaderLabel.Text = " <%XValue,d>";
            chart1.DefaultElement.Annotation.HeaderBackground.Color = Color.DarkOliveGreen;
            chart1.DefaultElement.Annotation.HeaderLabel.Font = new Font("Arial", 10);
            chart1.DefaultElement.Annotation.HeaderBackground.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.Four;
            chart1.DefaultElement.Annotation.Padding = 4;
            chart1.DefaultElement.Annotation.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Round;
            chart1.DefaultElement.Annotation.Background.Color = Color.White;
        }

        /**************************************************************************************************/
        private void DemographicsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);

            InitNewPatient();
        }

        /**************************************************************************************************/
        private void InitNewPatient()
        {
            ////  get active patinet object from session manager
            //Patient proband = SessionManager.Instance.GetActivePatient();
            //if (proband != null)
            //{
            //    theBreastImagingHx = proband.breastImagingHx;

            //    if (theBreastImagingHx != null)
            //    {

            //        theBreastImagingHx.Changed += new HraObject.ChangedEventHandler(theBreastImagingHxChanged);
            //        theBreastImagingHx.Loaded += new HraObject.LoadFinishedEventHandler(theBreastImagingHxLoaded);

            //        switch (theBreastImagingHx.hra_state)
            //        {
            //            case HraObject.States.NULL:
            //                loadingCircle1.Active = true;
            //                loadingCircle1.Visible = true;

            //                theBreastImagingHx.LoadObject();
            //                break;

            //            case HraObject.States.Loading:
            //                break;

            //            case HraObject.States.Ready:
            //                FillControls();
            //                break;

            //        }
            //    }
            //}
        }
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitNewPatient();
        }

        /**************************************************************************************************/
        private void theBreastImagingHxChanged(object sender, HraModelChangedEventArgs e)
        {
            if (e.sendingView != this)
            {
                FillControls();
            }
        }
        /**************************************************************************************************/
        private void theBreastImagingHxLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void FillControls()
        {
            chart1.SeriesCollection.Clear();
            flowLayoutPanel1.Controls.Clear();

            foreach (BreastImagingStudy bis in theBreastImagingHx)
            {
                BreastImagingRow bisRow = new BreastImagingRow(bis);
                bisRow.owningView = this;
                bisRow.SetScrollState(false);
                flowLayoutPanel1.Controls.Add(bisRow);

                Application.DoEvents();

                string name = bis.imagingType;
                if (string.IsNullOrEmpty(bis.imagingType))
                    name = "n/a";

                chart1.Series.Element = new dotnetCHARTING.WinForms.Element(name, bis.date, 2, 2);
                chart1.Series.Elements.Add();
            }
            chart1.SeriesCollection.Add();
            chart1.Refresh();

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }

        private void BreastImagingView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //BreastImagingStudy bis = new BreastImagingStudy();
            //bis.date = DateTime.Now;
            //theBreastImagingHx.imagingStudies.Add(bis);

            //BreastImagingRow bisRow = new BreastImagingRow(bis);
            //bisRow.owningView = this;
            //bisRow.SetScrollState(false);
            //flowLayoutPanel1.Controls.Add(bisRow);
            //flowLayoutPanel1.Controls.SetChildIndex(bisRow, 0);

            //theBreastImagingHx.SignalModelChanged(new HraModelChangedEventArgs(this));
        }

    }
}

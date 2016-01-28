using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.Clinic.Queues;
using RiskApps3.Model.Clinic.Followup;
using RiskApps3.Model.PatientRecord;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.PatientRecord.Imaging;
using RiskApps3.View.PatientRecord.Labs;
using RiskApps3.Controllers;

namespace RiskApps3.View.RiskClinic
{
    public partial class HighRiskFollowupStatusView : HraView
    {
        /**************************************************************************************************/
        private RiskApps3.Model.Clinic.Queues.FollowupCohort p_FollowupCohort;

        PedigreeImageView pf = new PedigreeImageView();
        SummaryFollowupView sfv = new SummaryFollowupView();
        //BreastImagingView biv = new BreastImagingView();
        //LabsGridView lgv = new LabsGridView();


        /**************************************************************************************************/
        public HighRiskFollowupStatusView()
        {
            p_FollowupCohort = new RiskApps3.Model.Clinic.Queues.FollowupCohort();

            InitializeComponent();
        }
                
        /**************************************************************************************************/
        private void HighRiskFollowupStatus_Load(object sender, EventArgs e)
        {
            theDockPanel.AllowEndUserDocking = SessionManager.Instance.AllowDockDragAndDrop;

            p_FollowupCohort.AddHandlersWithLoad(HighRiskBrcaQueueChanged, HighRiskBrcaQueueLoaded, null);

            sfv.Show(theDockPanel);
            sfv.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;


            pf.Show(theDockPanel);
            //pf.SetMode("MANUAL");
            pf.DockState = WeifenLuo.WinFormsUI.Docking.DockState.Document;

            pf.Enabled = false;

            sfv.Show();

        }
        /**************************************************************************************************/
        private void FillControls()
        {
            objectListView1.SetObjects(p_FollowupCohort.FollowupCohortPeople);
        }

        /**************************************************************************************************/
        private void HighRiskBrcaQueueChanged(object sender, HraModelChangedEventArgs e)
        {
        }

        /**************************************************************************************************/
        private void HighRiskBrcaQueueLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillQueueList();
        }

        /**************************************************************************************************/
        private void FillQueueList()
        {
            objectListView1.SetObjects(p_FollowupCohort.FollowupCohortPeople);
        }

        /**************************************************************************************************/
        private void HighRiskFollowupStatus_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItem != null)
            {
                FollowupCohortEntry item = (FollowupCohortEntry)objectListView1.SelectedItem.RowObject;
                SessionManager.Instance.SetActivePatient(item.unitnum,-1);

                patientRecordHeader1.setPatient(SessionManager.Instance.GetActivePatient());
            }
        }


    }
}







//progressBar1.Visible = true;

/*
HighRiskBrcaQueueEntry item = (HighRiskBrcaQueueEntry) objectListView1.SelectedItem.RowObject;
if (fs != null)
{
    fs.RemoveViewHandlers(this);
}
fs = new FollowupStatus(item.unitnum);

fs.Changed += new HraObject.ChangedEventHandler(FollowupStatusChanged);
fs.Loaded += new HraObject.LoadFinishedEventHandler(FollowupStatusLoaded);

switch (fs.hra_state)
{
    case HraObject.States.NULL:
        fs.LoadObject();
        break;
    case HraObject.States.Loading:
        break;
    case HraObject.States.Ready:
        FillFollowupStatus();
        break;
}
*/




///**************************************************************************************************/
//private void FollowupStatusChanged(object sender, HraModelChangedEventArgs e)
//{
//}

///**************************************************************************************************/
//private void FollowupStatusLoaded(object sender, RunWorkerCompletedEventArgs e)
//{
//    FillFollowupStatus();
//    progressBar1.Visible = false;
//}

///**************************************************************************************************/
//private void FillFollowupStatus()
//{
//    //chart1.SeriesCollection.Clear();
//    //LatestMammoDateLabel.Text ="";
//    //LatestMammoLeftBirads.Text = "";
//    //LatestMammoRightBirads.Text = "";

//    //DateTime dt = new DateTime(0);

//    //foreach (BreastImagingStudy bis in fs.breastImaging.imagingStudies)
//    //{
//    //    if (bis.leftBirads.Length > 0 && bis.rightBirads.Length > 0 && bis.imagingType == "MammographyHxView")
//    //    {
//    //        if (bis.date > dt)
//    //        {
//    //            dt = bis.date;
//    //            LatestMammoDateLabel.Text = bis.date.ToShortDateString();
//    //            LatestMammoLeftBirads.Text = bis.leftBirads;
//    //            LatestMammoRightBirads.Text = bis.rightBirads;
//    //        }
//    //    }
//    //    string name = bis.imagingType;
//    //    if (string.IsNullOrEmpty(bis.imagingType))
//    //        name = "n/a";

//    //    chart1.Series.Element = new dotnetCHARTING.WinForms.Element(name, bis.date,2,2);
//    //    chart1.Series.Elements.Add();
//    //}
//    //chart1.SeriesCollection.Add();
//    //chart1.Refresh();
//}

//private void AddImagingButton_Click(object sender, EventArgs e)
//{
//    //AddImagingForm aif = new AddImagingForm();
//    //aif.ShowDialog();
//    //if (aif.DialogResult == DialogResult.OK)
//    //{
//    //    BreastImagingStudy bsi = new BreastImagingStudy();
//    //    bsi.imagingType = aif.ImagingType;
//    //    bsi.date = aif.StudyDate;
//    //    bsi.leftBirads = aif.LeftBirads;
//    //    bsi.rightBirads = aif.RightBirads;
//    //    fs.breastImaging.imagingStudies.Add(bsi);

//    //    FillFollowupStatus();
//    //}
//}
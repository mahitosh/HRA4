using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.PatientRecord;
using System.Threading;
using RiskApps3.View.Appointments;
using RiskApps3.Utilities;
using RiskApps3.View.Common;

namespace RiskApps3.View
{
    public partial class AppointmentsView : HraView
    {

        ///**************************************************************************************************/
        //private AppointmentList theApptList;
        //private bool TryingToClose = false;
        //private bool TryingToUpdate = false;

        //private ListViewColumnSorter lvwColumnSorter;

        ///**************************************************************************************************/
        //public AppointmentsView()
        //{
        //    InitializeComponent();

        //    dateTimePicker1.Value = DateTime.Now;
        //    lvwColumnSorter = new ListViewColumnSorter();
        //    AppointmentsListView.ListViewItemSorter = lvwColumnSorter;
        //    label4.Text = "";
        //    comboBox1.Items.Add("");

        //}

        ///**************************************************************************************************/
        //private void AppointmentsView_Load(object sender, EventArgs e)
        //{
        //    // set local pointer for convienance
        //    theApptList = new AppointmentList();

        //    theApptList.Changed += new HraObject.ChangedEventHandler(AppointmentListChanged);
        //    theApptList.Loaded += new HraObject.LoadFinishedEventHandler(AppointmentListLoaded);

        //    loadingCircle1.Active = true;

        //    switch (theApptList.hra_state)
        //    {
        //        case HraObject.States.NULL:
        //            SetApptListWhereClaus();
        //            theApptList.LoadObject();
        //            break;
        //        case HraObject.States.Loading:
        //            break;
        //        case HraObject.States.Ready:
        //                backgroundWorker1.RunWorkerAsync();
        //            break;

        //    }
         
        //}

        ///**************************************************************************************************/
        //private void SetApptListWhereClaus()
        //{
        //    theApptList.WhereClaus = "";
        //    if (checkBox1.Checked)
        //    {
        //        theApptList.WhereClaus += ("CONVERT(datetime,apptdate) = CONVERT(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "')");
        //    }
        //    if (comboBox1.Text.Length > 0)
        //    {
        //        if (theApptList.WhereClaus.Length > 0)
        //        {
        //            theApptList.WhereClaus += " AND ";
        //        }

        //        theApptList.WhereClaus += ("apptphysname ='" + comboBox1.Text + "'");
        //    }

        //}

        ///**************************************************************************************************/
        //private void AppointmentListChanged(object sender, HraModelChangedEventArgs e)
        //{
        //    AppointmentsListView.Items.Clear();

        //    if (backgroundWorker1.IsBusy == false)
        //    {
        //        backgroundWorker1.RunWorkerAsync();
        //    }
        //    else
        //    {
        //        TryingToUpdate = true;
        //        backgroundWorker1.CancelAsync();
        //    }
        //}

        ///**************************************************************************************************/
        //private void AppointmentListLoaded(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    backgroundWorker1.RunWorkerAsync();
        //}

        ///**************************************************************************************************/
        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        foreach (Appointment a in theApptList.appointments)
        //        {
        //            if (backgroundWorker1.CancellationPending)
        //            {
        //                e.Cancel = true;
        //                return;
        //            }

        //            ListViewItem lvi = new ListViewItem(a.unitnum);
        //            lvi.SubItems.Add(a.patientName);
        //            lvi.SubItems.Add(a.apptDateTime.ToShortDateString());
        //            lvi.SubItems.Add(a.apptDateTime.ToShortTimeString());
        //            lvi.SubItems.Add(a.apptPhysName);
        //            lvi.Tag = a;
        //            backgroundWorker1.ReportProgress(0, lvi);
        //            Thread.Sleep(15);
        //        }
        //    }
        //    catch (Exception exe)
        //    {
        //        Logger.Instance.WriteToLog(exe.ToString());
        //    }
        //}

        ///**************************************************************************************************/
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    if (e.UserState != null)
        //    {
        //        ListViewItem lvi = (ListViewItem)(e.UserState);
        //        AppointmentsListView.Items.Add(lvi);

        //        label4.Text = AppointmentsListView.Items.Count.ToString() + " Appointments";

        //        if (lvi.Tag!=null)
        //        {
        //            Appointment a = (Appointment)(lvi.Tag);

        //            string doc = a.apptPhysName;

        //            if (comboBox1.Items.Contains(doc) == false)
        //            {
        //                comboBox1.Items.Add(doc);
        //            }
        //        }
        //    }
        //}

        ///**************************************************************************************************/
        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    foreach (ColumnHeader ch in AppointmentsListView.Columns)
        //    {
        //        ch.Width = -2;
        //    }

        //    if (TryingToUpdate)
        //    {
        //        TryingToUpdate = false;
        //        backgroundWorker1.RunWorkerAsync();
                
        //    }
        //    else
        //    {
        //        loadingCircle1.Active = false;
        //        loadingCircle1.Visible = false;
        //    }
        //    if (TryingToClose)
        //        this.Close();
        //}

        ///**************************************************************************************************/
        //private void AppointmentsView_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    TryingToClose = true;
        //    if (backgroundWorker1.IsBusy)
        //    {
        //        backgroundWorker1.CancelAsync();
        //        e.Cancel = true;
        //    }
            
        //    theApptList.ReleaseListeners(this);

        //    SessionManager.Instance.RemoveHraView(this);
        //}

        ///**************************************************************************************************/
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    AddAppointmentView aa = new AddAppointmentView();
        //    SessionManager.Instance.AddViewToSession(aa);
        //    aa.Show();
        //}

        ///**************************************************************************************************/
        //private void AppointmentsListView_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (AppointmentsListView.SelectedItems.Count > 0)
        //    {
        //        string unitnum = AppointmentsListView.SelectedItems[0].Text;
        //        if (string.IsNullOrEmpty(unitnum) == false)
        //        {
        //            SessionManager.Instance.SetActivePatient(unitnum);
        //        }
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    AppointmentsListView.SelectedItems.Clear();
        //    SessionManager.Instance.ClearActivePatient();
        //}

        //private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        //{
        //    if (theApptList != null)
        //    {
        //        loadingCircle1.Active = true;
        //        loadingCircle1.Visible = true;

        //        AppointmentsListView.Items.Clear();
        //        SetApptListWhereClaus();
        //        theApptList.LoadObject();
        //    }
        //}

        //private void AppointmentsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    // Determine if clicked column is already the column that is being sorted.
        //    if (e.Column == lvwColumnSorter.SortColumn)
        //    {
        //        // Reverse the current sort direction for this column.
        //        if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
        //        {
        //            lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
        //        }
        //        else
        //        {
        //            lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
        //        }
        //    }
        //    else
        //    {
        //        // Set the column number that is to be sorted; default to ascending.
        //        lvwColumnSorter.SortColumn = e.Column;
        //        lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
        //    }

        //    // Perform the sort with these new sort options.
        //    AppointmentsListView.Sort();
        //}

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (theApptList != null)
        //    {
        //        loadingCircle1.Active = true;
        //        loadingCircle1.Visible = true;

        //        AppointmentsListView.Items.Clear();
        //        SetApptListWhereClaus();
        //        theApptList.LoadObject();
        //    }
        //}

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (theApptList != null)
        //    {
        //        loadingCircle1.Active = true;
        //        loadingCircle1.Visible = true;

        //        AppointmentsListView.Items.Clear();
        //        SetApptListWhereClaus();
        //        theApptList.LoadObject();
        //    }
        //}


    }
}

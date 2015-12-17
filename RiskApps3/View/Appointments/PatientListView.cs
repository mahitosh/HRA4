using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic;
using RiskApps3.View.Common;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Threading;
using RiskApps3.Utilities;
using BrightIdeasSoftware;
using RiskApps3.Controllers;

namespace RiskApps3.View.Appointments
{
    public partial class PatientListView : HraView
    {
        private PatientList thePatientList = new PatientList();
            private HeaderFormatStyle s = new HeaderFormatStyle();

        
        /**************************************************************************************************/
        public PatientListView()
        {
            InitializeComponent();

            dateTimePicker1.Value = DateTime.Now;

            s.Normal.FrameColor = Color.Black;
            s.Normal.BackColor = Color.LightGray;
            s.Normal.ForeColor = Color.Black;
            s.Normal.Font = new Font(objectListView1.Font, FontStyle.Bold);

            s.Pressed.FrameColor = Color.Black;
            s.Pressed.BackColor = Color.LightGray;
            s.Pressed.ForeColor = Color.Black;
            s.Pressed.Font = new Font(objectListView1.Font, FontStyle.Bold);

            //s.SetBackColor(System.Drawing.Color.LightGray);
            objectListView1.HeaderFormatStyle = s;
        }

        /**************************************************************************************************/
        private void PatientListView_Load(object sender, EventArgs e)
        {
            thePatientList.AddHandlersWithLoad(PatientListChanged, PatientListLoaded, null);
   
        }

        /**************************************************************************************************/
        private void SetPatientListWhereClaus()
        {
            thePatientList.WhereClaus = "";
            if (checkBox1.Checked)
            {
                thePatientList.WhereClaus += ("CONVERT(datetime,apptdate) = CONVERT(datetime,'" + dateTimePicker1.Value.ToShortDateString() + "')");
            }
        }

        /**************************************************************************************************/
        private void PatientListChanged(object sender, HraModelChangedEventArgs e)
        {

        }

        /**************************************************************************************************/
        private void PatientListLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();

        }

        /**************************************************************************************************/
        private void FillControls()
        {
            if (thePatientList.patients.Count > 1000)
            {
                objectListView1.Visible = false;
                fastObjectListView1.Visible = true;
                fastObjectListView1.SetObjects(thePatientList.patients);
                label4.Text = fastObjectListView1.Items.Count.ToString();
            }
            else
            {
                objectListView1.Visible = true;
                fastObjectListView1.Visible = false;
                objectListView1.SetObjects(thePatientList.patients);
                label4.Text = objectListView1.Items.Count.ToString();
            }
            
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;

        }

        
        /**************************************************************************************************/
        private void AppointmentsView_FormClosing(object sender, FormClosingEventArgs e)
        {


            SessionManager.Instance.RemoveHraView(this);
        }

        /**************************************************************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            AddAppointmentView aa = new AddAppointmentView();
            SessionManager.Instance.AddViewToSession(aa);
            aa.Show();
        }



        /**************************************************************************************************/
        private void button2_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.ClearActivePatient();
        }

        /**************************************************************************************************/
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (thePatientList != null)
            {
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;

                SetPatientListWhereClaus();
                thePatientList.LoadObject();
            }
        }

        /**************************************************************************************************/
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (thePatientList != null)
            {
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;

                SetPatientListWhereClaus();
                thePatientList.LoadObject();
            }
        }

        /**************************************************************************************************/
        private void PatientListView_FormClosing(object sender, FormClosingEventArgs e)
        {
            thePatientList.ReleaseListeners(this);
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void objectListView1_SelectionChanged(object sender, EventArgs e)
        {
            PatientListEntry ple = (PatientListEntry)objectListView1.SelectedObject;
            if (ple != null)
            {
                SessionManager.Instance.SetActivePatient(ple.unitnum,-1);
            }
        }

        private void fastObjectListView1_SelectionChanged(object sender, EventArgs e)
        {
            PatientListEntry ple = (PatientListEntry)fastObjectListView1.SelectedObject;
            if (ple != null)
            {
                SessionManager.Instance.SetActivePatient(ple.unitnum,-1);
            }
        }
    }
}



/*
 * 
 * 
 *             //backgroundWorker1.RunWorkerAsync();
 *             
 * 
 * 
 *         private bool TryingToClose = false;
        private bool TryingToUpdate = false;

        private ListViewColumnSorter lvwColumnSorter;
 * 
 * 

 *             lvwColumnSorter = new ListViewColumnSorter();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ListViewItem> chunk = new List<ListViewItem>();
            int count = 0;
            try
            {
                foreach (PatientListEntry ple in thePatientList.patients)
                {
                    count++;
                    if (backgroundWorker1.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    ListViewItem lvi = new ListViewItem(ple.unitnum);
                    lvi.SubItems.Add(ple.name);
                    lvi.SubItems.Add(ple.dob.ToShortDateString());
                    lvi.Tag = ple;

                    chunk.Add(lvi);

                    if (count % 10 == 0)
                    {
                        backgroundWorker1.ReportProgress(0, chunk);
                        chunk = new List<ListViewItem>();
                    }
                   // Thread.Sleep(5);
                }
                backgroundWorker1.ReportProgress(0, chunk);
            }
            catch (Exception exe)
            {
                Logger.Instance.WriteToLog(exe.ToString());
            }
        }

       
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                List<ListViewItem> chunk = (List<ListViewItem>)(e.UserState);
                foreach (ListViewItem lvi in chunk)
                {
                    PatientListControl.Items.Add(lvi);

                    label4.Text = PatientListControl.Items.Count.ToString() + " Patients";
                }
                chunk.Clear();
            }
        }

       
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            PatientListControl.ListViewItemSorter = lvwColumnSorter;

            foreach (ColumnHeader ch in PatientListControl.Columns)
            {
                ch.Width = -2;
            }

            if (TryingToUpdate)
            {
                TryingToUpdate = false;
                backgroundWorker1.RunWorkerAsync();
                
            }
            else
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;
            }
            if (TryingToClose)
                this.Close();

            
        }


        private void PatientListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = System.Windows.Forms.SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            PatientListControl.Sort();
        }



        private void PatientListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PatientListControl.SelectedItems.Count > 0)
            {
                string unitnum = PatientListControl.SelectedItems[0].Text;
                if (string.IsNullOrEmpty(unitnum) == false)
                {
                    SessionManager.Instance.SetActivePatient(unitnum);
                }
            }
        }



            //if (backgroundWorker1.IsBusy == false)
            //{
            //    backgroundWorker1.RunWorkerAsync();
            //}
            //else
            //{
            //    TryingToUpdate = true;
            //    backgroundWorker1.CancelAsync();
            //}


            //TryingToClose = true;
            //if (backgroundWorker1.IsBusy)
            //{
            //    backgroundWorker1.CancelAsync();
            //    e.Cancel = true;
            //}
 * 
 * 
 * 
**/
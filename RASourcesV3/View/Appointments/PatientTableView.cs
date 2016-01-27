using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.Clinic;
using RiskApps3.Model;
using BrightIdeasSoftware;

namespace RiskApps3.View.Appointments
{
    public partial class PatientTableView : HraView
    {
        private PatientTable Patients = new PatientTable();

        public PatientTableView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void PatientListView_Load(object sender, EventArgs e)
        {
            
            loadingCircle1.Active = true;
            Patients.AddHandlersWithLoad(PatientTableChanged, PatientTableLoaded, null);
        }
        /**************************************************************************************************/
        private void PatientTableChanged(object sender, HraModelChangedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void PatientTableLoaded(object sender, RunWorkerCompletedEventArgs e)
        {
            FillControls();
        }

        /**************************************************************************************************/
        private void FillControls()
        {
            /**/
            
            foreach (DataColumn c in Patients.PatientsTable.Columns)
            {
                string colName = c.ColumnName;
                BrightIdeasSoftware.OLVColumn olvc = new BrightIdeasSoftware.OLVColumn();
                fastDataListView1.AllColumns.Add(olvc);
                System.Windows.Forms.ColumnHeader[] colHeaderArray = new System.Windows.Forms.ColumnHeader[] { olvc };
                fastDataListView1.Columns.AddRange(colHeaderArray);
                olvc.AspectName = colName;
                olvc.Text = colName;
            }

            fastDataListView1.DataSource = new BindingSource(Patients.PatientsTable, "");

            for (int i = 0; i < fastDataListView1.Columns.Count; i++)
            {
                //fastDataListView1.AllColumns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                fastDataListView1.AllColumns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                //int cw1 = fastDataListView1.AllColumns[i].Width;
                //int cw2 = fastDataListView1.AllColumns[i].Width;
                //fastDataListView1.AllColumns[i].Width = 2 + ((cw1 >= cw2) ? cw1 : cw2); //two pixels added for when filtered
            }

            

            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
        }

        /**************************************************************************************************/
        private void PatientTableView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Patients.ReleaseListeners(this);
        }

        private void fastDataListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

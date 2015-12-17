using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.Risk;

namespace RiskApps3.View.PatientRecord.Risk
{
    public partial class NCCNView : HraView
    {
        /**************************************************************************************************/
        //private Person selectedRelative;
        private Patient proband;

        /**************************************************************************************************/
        public NCCNView()
        {
            InitializeComponent();
        }

        /**************************************************************************************************/
        private void NCCNView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            InitActivePatient();
        }

        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitActivePatient();
        }

        /**************************************************************************************************/
        private void InitActivePatient()
        {
            //  get active patinet object from session manager
            proband = SessionManager.Instance.GetActivePatient();
           fastDataListView1.Enabled = false;
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            proband.RP.NCCNGuideline.AddHandlersWithLoad(NCCNChanged, NCCNLoaded, NCCNItemChanged);
 
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
            fastDataListView1.Enabled = true;
            loadingCircle1.Enabled = false;
            loadingCircle1.Visible = false;
            //Application.DoEvents();
            fastDataListView1.ClearObjects();
            fastDataListView1.SetObjects(proband.RP.NCCNGuideline);
            fastDataListView1.Columns[1].Width = -2;
           // fastDataListView1.Sort(olvColumn7, SortOrder.Descending);

            //for (int i=0; i< fastDataListView1.Items.Count; i++)
            //{
            //    //fastDataListView1.Items[i].BackColor = Color.Red;
            //    //lvi.BackColor = Color.Red;
            //    BrightIdeasSoftware.OLVListItem olv =  (BrightIdeasSoftware.OLVListItem)fastDataListView1.GetItem(i);
            //    olv.BackColor = Color.Red;
            //}
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fastDataListView1.Enabled = false;
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            Application.DoEvents();
            proband.RP.NCCNGuideline.LoadList();

        }

        private void NCCNView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
            proband.RP.NCCNGuideline.ReleaseListeners(this);
            proband.ReleaseListeners(this);
        }


    }
}
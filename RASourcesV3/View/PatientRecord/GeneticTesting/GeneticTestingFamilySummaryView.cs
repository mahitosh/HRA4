using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Controllers;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;

namespace RiskApps3.View.PatientRecord.GeneticTesting
{
    public partial class GeneticTestingFamilySummaryView : HraView
    {
        Patient proband;
        char[] trimChars = new char[] { ' ', '/' };

        public GeneticTestingFamilySummaryView()
        {
            InitializeComponent();
        }

        private void GeneticTestingFamilySummaryView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient += NewActivePatient;
            InitSelectedRelative();
        }
        /**************************************************************************************************/

        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            InitSelectedRelative();
        }

        private void InitSelectedRelative()
        {
            Reset();

            if (proband != null)
            {
                proband.ReleaseListeners(this);
            }
            proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                proband.FHx.AddHandlersWithLoad(null, FhxLoaded, null);
            }
        }

        private void FhxLoaded(HraListLoadedEventArgs e)
        {
            lock (proband.FHx)
            {
                foreach (Person p in proband.FHx)
                {
                    p.PMH.GeneticTests.AddHandlersWithLoad(null, GtLoaded, null);
                }
            }
        }

        private void GtLoaded(HraListLoadedEventArgs e)
        {
            lock (e.sender)
            {
                foreach (GeneticTest gt in (GeneticTestList)e.sender)
                {
                    GeniticTestStatusContainer gtsc = new GeniticTestStatusContainer();
                    gtsc.Relative = gt.owningPMH.RelativeOwningPMH.relationship.Replace("Self", "Patient");
                    gtsc.Panel = gt.panelName;
                    gtsc.Status = gt.status;
                    gtsc.owner = gt.owningPMH.RelativeOwningPMH;
                    if (string.IsNullOrEmpty(gt.GeneticTest_testDay))
                    {
                        gtsc.Date = (gt.GeneticTest_testMonth + "/" + gt.GeneticTest_testYear).Trim(trimChars);
                    }
                    else
                    {
                        gtsc.Date = (gt.GeneticTest_testMonth + "/" + gt.GeneticTest_testDay + "/" + gt.GeneticTest_testYear).Trim(trimChars);
                    }
                    AddGtObject(gtsc);

                }
            }
        }

        delegate void SetSelectedObjectCallback(GeniticTestStatusContainer gtsc);
        void AddGtObject(GeniticTestStatusContainer gtsc)
        {
            if (objectListView1.InvokeRequired)
            {
                SetSelectedObjectCallback aoc = new SetSelectedObjectCallback(AddGtObject);
                object[] args = new object[1];
                args[0] = gtsc;
                this.Invoke(aoc, args);
            }
            else
            {
                bool found = false;
                foreach (object o in objectListView1.Objects)
                {
                    if (((GeniticTestStatusContainer)o).owner == gtsc.owner &&
                       ((GeniticTestStatusContainer)o).Panel == gtsc.Panel)
                    {
                        found = true;
                    }
                }
                if (found ==false)
                {
                    objectListView1.AddObject(gtsc);
                    if (string.Compare("Pending", gtsc.Status, true) == 0)
                        objectListView1.SelectObject(gtsc);
                }
            }
        }
        private void GeneticTestingFamilySummaryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
        }

        internal void Reset()
        {
            objectListView1.ClearObjects();
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeniticTestStatusContainer gtsc = (GeniticTestStatusContainer)(objectListView1.SelectedObject);
            if (gtsc != null)
            {
                if (gtsc.owner != null)
                {
                    SessionManager.Instance.SetActiveRelative(this, gtsc.owner);
                }
            }
        }

    }
    class GeniticTestStatusContainer
    {
        public string Relative = "";
        public string Panel = "";
        public string Status = "";
        public string Date = "";
        public Person owner = null;
    }
}

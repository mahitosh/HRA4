using System;
using System.ComponentModel;
using System.Drawing;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using System.Windows.Forms;
using System.Reflection;
using RiskApps3.Controllers;
using RiskApps3.View.PatientRecord.Pedigree;
using System.Collections.Generic;
using System.Linq;
using RiskApps3.View.PatientRecord.Risk;

namespace RiskApps3.View.Risk
{
    public partial class RelativesToConsiderView : HraView
    {
        /**************************************************************************************************/

        public RelativesToConsiderView()
        {
            InitializeComponent();

            loadingCircle1.Active = true;
            loadingCircle1.Visible = true;
        }

        /**************************************************************************************************/

        private void RelativeDetailsView_Load(object sender, EventArgs e)
        {
            SessionManager.Instance.NewActivePatient +=
                new RiskApps3.Controllers.SessionManager.NewActivePatientEventHandler(NewActivePatient);
            //SessionManager.Instance.RelativeSelected +=
            //    new RiskApps3.Controllers.SessionManager.RelativeSelectedEventHandler(RelativeSelected);

            SessionManager.Instance.GetActivePatient().FHx.AddHandlersWithLoad(FHChanged, FHLoaded, FHItemChanged);

            //Nevermind this:
            //use equivalent of V2 method to determine relatives to consider
            //List<int> relativeIDList = SessionManager.Instance.GetActivePatient().FHx.getSortedOrder();

            //reCreateListOfRelativesToConsider();

            //InitSelectedRelative();
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


        
        /**************************************************************************************************/
        private void reCreateListOfRelativesToConsider()
        {
            flowLayoutPanel1.Controls.Clear();

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
                r.SetLabels(p.relativeID, String.Format("{0:#0.0}", Math.Round(p.RP.RiskProfile_BrcaPro_1_2_Mut_Prob ?? -1, 1)), p.name, p.relationship);
                flowLayoutPanel1.Controls.Add(r);
            }

            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;
        }
        
        /**************************************************************************************************/
        private void NewActivePatient(object sender, NewActivePatientEventArgs e)
        {
            e.newActivePatient.FHx.AddHandlersWithLoad(FHChanged, FHLoaded, null);
        }

        ///**************************************************************************************************/

        //private void RelativeSelected(RelativeSelectedEventArgs e)
        //{
        //    InitSelectedRelative();
        //}

        /**************************************************************************************************/
        //private void InitSelectedRelative()
        //{
        //    //  get selected relative object from session manager
        //    selectedRelative = SessionManager.Instance.GetSelectedRelative();

        //    if (selectedRelative != null)
        //    {
        //        selectedRelative.Changed += new HraObject.ChangedEventHandler(selectedRelativeChanged);
        //        selectedRelative.Loaded += new HraObject.LoadFinishedEventHandler(selectedrelativeLoaded);

        //        switch (selectedRelative.hra_state)
        //        {
        //            case HraObject.States.NULL:
        //                loadingCircle1.Active = true;
        //                loadingCircle1.Visible = true;

        //                selectedRelative.LoadObject();
        //                break;

        //            case HraObject.States.Loading:
        //                break;

        //            case HraObject.States.Ready:
        //                FillControls();
        //                break;
        //        }
        //    }
        //}

        /**************************************************************************************************/

        //private void FillControls()
        //{
        //    loadingCircle1.Active = false;
        //    loadingCircle1.Visible = false;

        //}

        ///**************************************************************************************************/

        //private void selectedRelativeChanged(object sender, HraModelChangedEventArgs e)
        //{
        //    FillControls();
        //}

        ///**************************************************************************************************/

        //private void selectedrelativeLoaded(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    FillControls();

        //}

        ///**************************************************************************************************/



        /**************************************************************************************************/
        //private void Control_Leave(object sender, EventArgs e)
        //{
        //    Control t = (Control)sender;

        //    foreach (FieldInfo fi in selectedRelative.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public))
        //    {
        //        string name = fi.Name;
        //        if (name == t.Name)
        //        {
        //            if (t.Text != fi.GetValue(selectedRelative).ToString())
        //            {

        //                fi.SetValue(selectedRelative, t.Text);
        //                HraModelChangedEventArgs signalArgs = new HraModelChangedEventArgs(this);
        //                signalArgs.updatedMembers.Add(fi);

        //                //if (t == firstName || t == middleName || t == lastName || t == suffix || t == title)
        //                //{
        //                //    selectedRelative.name = selectedRelative.title + " " +
        //                //                            selectedRelative.firstName + " " +
        //                //                            selectedRelative.middleName + " " +
        //                //                            selectedRelative.lastName + " " +
        //                //                            selectedRelative.suffix;

        //                //    foreach (MemberInfo mi in selectedRelative.GetType().GetMember("name"))
        //                //    {
        //                //        signalArgs.updatedMembers.Add(mi);
        //                //    }
        //                //}

        //                selectedRelative.SignalModelChanged(signalArgs);
        //                //selectedRelative.SignalModelChanged(new HraModelChangedEventArgs(this));
        //            }
        //            break;
        //        }
        //    }
        //}

        /**************************************************************************************************/
        private void RelativeDetailsView_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            SessionManager.Instance.RemoveHraView(this);
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

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using RiskApps3.View.PatientRecord.Pedigree;
using RiskApps3.View.Appointments;
using RiskApps3.View.RiskClinic;
using RiskApps3.View.PatientRecord;

namespace RiskApps3.View
{
    public class ViewConfiguration
    {

        /**************************************************************************************************/
        public Dictionary<HraView, WeifenLuo.WinFormsUI.Docking.DockState> Views;

        /**************************************************************************************************/

        /**************************************************************************************************/
        //  the default constructor for a view configuration
        public ViewConfiguration()
        {

            //create the list ofviews in this configuration
            Views = new Dictionary<HraView, DockState>();


            RiskClinicDashboard rcd = new RiskClinicDashboard();
            Views.Add(rcd, WeifenLuo.WinFormsUI.Docking.DockState.Document);

            //MyRiskClinicPatients myrcp = new MyRiskClinicPatients();
            //Views.Add(myrcp, WeifenLuo.WinFormsUI.Docking.DockState.Document);


            //BreastCancerRiskFactors bcrf = new BreastCancerRiskFactors();
            //Views.Add(bcrf, WeifenLuo.WinFormsUI.Docking.DockState.Document);


            //RiskClinicMainForm rcmf = new RiskClinicMainForm();
            //Views.Add(rcmf, WeifenLuo.WinFormsUI.Docking.DockState.Document);

            //PatientTableView ptv = new PatientTableView();
            //Views.Add(ptv, WeifenLuo.WinFormsUI.Docking.DockState.Document);


            /*
            //for now, just add an appointmentsView and call it a day
            PatientListView plv = new PatientListView();
            Views.Add(plv, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

            PedigreeForm pf = new PedigreeForm();
            Views.Add(pf,WeifenLuo.WinFormsUI.Docking.DockState.Document);
            */
             
        }

        /**************************************************************************************************/
        public ViewConfiguration(string user)
        {

        }


        /**************************************************************************************************/
        public void AddViewToConfiguration(HraView view)
        {
            Views.Add(view, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
    }
}

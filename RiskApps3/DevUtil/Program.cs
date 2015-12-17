using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RiskApps3.Model.MetaData;
using RiskApps3.View.RiskClinic;

namespace DevUtil
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                Application.Run(new CodeUtilsForm());
            }
            else
            {
                switch (args[0])
                {
                    case "v3Notes":
                        //notes interface expects a unitnum as a param
                        if (args.Length > 1 && string.IsNullOrEmpty(args[1]) == false)
                            LaunchNotesInterface(args[1]);
                        break;
                    case "v3ParagraphEditor":
                        //paragraph editor expects a user login as a param
                        if (args.Length > 1 && string.IsNullOrEmpty(args[1]) == false)
                        {
                            UtilityLoginForm ulf = new UtilityLoginForm();
                            ulf.userLogin = args[1];
                            Application.Run(ulf);
                        }
                        break;
                    case "v3Report":
                        RiskClinicReport rcp = new RiskClinicReport();
                        rcp.ShowDialog();
                        break;
                    case "v3Pedigree":
                        ////paragraph editor expects a user login as a param
                        //if (args.Length > 1 && string.IsNullOrEmpty(args[1]) == false)
                        //{
                        //    UtilityLoginForm ulf = new UtilityLoginForm();
                        //    ulf.userLogin = args[1];
                        //    Application.Run(ulf);
                        //}
                        break;
                    default:
                        Application.Run(new CodeUtilsForm());
                        break;
                }
            }
        }

        private static void LaunchNotesInterface(string unitnum)
        {
            //UserList users;
            //users.AddHandlersWithLoad(null, UsersListLoaded, null);
            //RiskApps3.Controllers.SessionManager.Instance.SetActivePatient(unitnum);
            //RiskApps3.View.RiskClinic.RiskClinicNotesView rcnv = new RiskApps3.View.RiskClinic.RiskClinicNotesView();
            //rcnv.PatientHeaderVisible = true;
            //Application.Run(rcnv);
        }
    }
}

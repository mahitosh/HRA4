using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.Security;
using RiskApps3.View;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.Session;
using RiskApps3.Utilities;
using RiskApps3.Model.MetaData;
using BrightIdeasSoftware;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace RiskApps3.Controllers
{
    public class SessionManager
    {
        /**************************************************************************************************/
        public delegate void NewActivePatientEventHandler(object sender, NewActivePatientEventArgs e);
        public delegate void RelativeSelectedEventHandler(RelativeSelectedEventArgs e);
        public delegate void AppointmentSelectedEventHandler(object sender, AppointmentSelectedEventArgs e);

        /**************************************************************************************************/
        public event NewActivePatientEventHandler NewActivePatient;
        public event RelativeSelectedEventHandler RelativeSelected;
        public event AppointmentSelectedEventHandler AppointmentSelected;

        /**************************************************************************************************/
        static readonly SessionManager _instance = new SessionManager();
        public SecurityContext securityContext;
        
        /**************************************************************************************************/
        private Patient activePatient;
        private Person selectedRelative;

        /**************************************************************************************************/
        public User ActiveUser;
        public string UserGroup = "Entire Clinic";

        /**************************************************************************************************/
        public MetaData MetaData;
        MainFormLoading mfl;

        /**************************************************************************************************/
        public bool SaveLayoutOnClose = false;
        public bool AllowDockDragAndDrop = false;

        /**************************************************************************************************/
        public SessionManager()
        {
            MetaData = new MetaData();
            mfl = new MainFormLoading();
            //mfl.Register(this);
        }

        public static SessionManager Instance
        {
            get
            {
                if (HttpContext.Current != null) // Silicus
                {
                    if (HttpContext.Current.Session["SessionManager"] == null)
                        HttpContext.Current.Session["SessionManager"] = new SessionManager();

                    return (SessionManager)HttpContext.Current.Session["SessionManager"];
                }
                return _instance;
            }
        }

        /**************************************************************************************************/

        public static string SelectDockConfig(string FormName)
        {
            string path = Path.Combine("", "dockConfig");

            if (Instance.ActiveUser != null)
            {
                if (Instance.ActiveUser.ModuleName != null)
                {
                    string subDir = Instance.ActiveUser.ModuleName.Replace("/", "").Replace(" ","");
                    string test = Path.Combine(path, subDir);
                    if (Directory.Exists(test))
                    {
                        path = test;
                    }
                }
            }

            path = Path.Combine(path, FormName);
            return path;
        }

        /**************************************************************************************************/
        public Patient GetActivePatient() { return activePatient; }


        /**************************************************************************************************/
        public Person GetSelectedRelative() { return selectedRelative; }
        /**************************************************************************************************/
        public void SetActivePatient(string unitnum, int apptid)
        {


            if (activePatient != null)
            {
                activePatient.ReleaseListeners(this);
                activePatient.Tasks.ReleaseListeners(this);
            }

            activePatient = new Patient(unitnum);
            activePatient.apptid = apptid;

            selectedRelative = (Person)activePatient;

            if (HttpContext.Current == null) // Silicus: LoadObject has Async calls.
            activePatient.LoadObject();

            if (NewActivePatient != null)
                NewActivePatient(this, new NewActivePatientEventArgs(activePatient, securityContext));


            RelativeSelectedEventArgs args = new RelativeSelectedEventArgs(selectedRelative, securityContext);
            if (RelativeSelected != null)
                RelativeSelected(args);
        }

        /**************************************************************************************************/
        public void SetActivePatientNoCallback(string unitnum, int apptid)
        {
            activePatient = new Patient(unitnum);
            activePatient.apptid = apptid;

            selectedRelative = (Person)activePatient;

            if (activePatient.HraState != HraObject.States.Ready)
            {
                activePatient.BackgroundLoadWork();
                activePatient.HraState = HraObject.States.Ready;
            }
        }

        /**************************************************************************************************/
        public void SetActiveRelative(HraView sender, Person person)
        {
            //Console.Out.WriteLine(DateTime.Now.ToLongTimeString() + "\tNew Active Relative " + person.relativeID.ToString() + " set by " + sender.ToString());

            selectedRelative = person;

            if (RelativeSelected != null)
            {
                RelativeSelectedEventArgs args = new RelativeSelectedEventArgs(selectedRelative, securityContext);
                args.sendingView = sender;
                RelativeSelected(args);
            }
        }

        /**************************************************************************************************/
        public void AddViewToSession(HraView av)
        {
            //av.Register(this);
            //theViewConfiguration.AddViewToConfiguration(av);
        }

        /**************************************************************************************************/
        public void RemoveHraView(HraView view)
        {
            ReleaseListenersForTarget(view);
        }

        public void RemoveHraView(UserControl userControl)
        {
            ReleaseListenersForTarget(userControl);
        }

        private void ReleaseListenersForTarget(object target)
        {
            // remove local listerners
            if (NewActivePatient != null)
            {
                foreach (Delegate d in NewActivePatient.GetInvocationList())
                {
                    if (d.Target == target)
                        NewActivePatient -= (NewActivePatientEventHandler)d;
                }
            }
            if (RelativeSelected != null)
            {
                foreach (Delegate d in RelativeSelected.GetInvocationList())
                {
                    if (d.Target == target)
                        RelativeSelected -= (RelativeSelectedEventHandler)d;
                }
            }
            if (AppointmentSelected != null)
            {
                foreach (Delegate d in AppointmentSelected.GetInvocationList())
                {
                    if (d.Target == target)
                        AppointmentSelected -= (AppointmentSelectedEventHandler)d;
                }
            }

            // remove patient model listeners
            if (activePatient != null)
            {
                activePatient.ReleaseProband(target);
            }

            this.MetaData.ReleaseListeners(target);
        }

        /**************************************************************************************************/
        internal void ClearActivePatient()
        {
            activePatient = null;
            selectedRelative = null;
            if (NewActivePatient != null)
                NewActivePatient(this, new NewActivePatientEventArgs(activePatient,securityContext));
        }



        public void Shutdown()
        {

            if (NewActivePatient != null)
            {
                foreach (Delegate d in NewActivePatient.GetInvocationList())
                {
                        NewActivePatient -= (NewActivePatientEventHandler)d;
                }
            }
            if (RelativeSelected != null)
            {
                foreach (Delegate d in RelativeSelected.GetInvocationList())
                {
                        RelativeSelected -= (RelativeSelectedEventHandler)d;
                }
            }
            if (AppointmentSelected != null)
            {
                foreach (Delegate d in AppointmentSelected.GetInvocationList())
                {
                    AppointmentSelected -= (AppointmentSelectedEventHandler)d;
                }
            }


            // remove patient model listeners
            if (activePatient != null)
            {
                activePatient.ReleaseProband(null);
            }

            this.MetaData.ReleaseListeners(null);
        }

        public void SetCoreConfigPath(string configPath)
        {
            RiskAppCore.Globals.ConfigPath = configPath;
        }
    }
}


///**************************************************************************************************/
//private Type activeQContext;
//public Type ActiveQ
//{
//    get
//    {
//        return activeQContext;
//    }
//    set
//    {
//        activeQContext = value;
//    }
//}

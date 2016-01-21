using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Services.Interfaces;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
//using System.Data.DataSetExtensions;
using HRA4.Repositories.Interfaces;
using log4net;
using RiskApps3.Model.Clinic.Dashboard;
using RiskApps3.Controllers;
using System.Drawing;
using HRA4.Utilities;
namespace HRA4.Services
{
    public class RiskClinicServices : IRiskClinicServices
    {
        IRepositoryFactory _repositoryFactory;
        IHraSessionManager _hraSessionManager;
        
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RiskClinicServices));
        public RiskClinicServices(IRepositoryFactory repositoryFactory, IHraSessionManager hraSessionManger)
        {
            this._repositoryFactory = repositoryFactory;
            _hraSessionManager = hraSessionManger;
        }

        public Entities.PatientDetails  GetPatientDetails(string unitnum , int apptid  )
        {
            Entities.PatientDetails objPatient = new Entities.PatientDetails();

             unitnum = "12312041502";
             apptid = -1;
            string assignedBy = "";
            string Base64 = string.Empty;

            if (SessionManager.Instance.ActiveUser != null)
            {
                if (string.IsNullOrEmpty(SessionManager.Instance.ActiveUser.ToString()) == false)
                {
                    assignedBy = SessionManager.Instance.ActiveUser.ToString();

                }
            }
            string _userlogin = SessionManager.Instance.ActiveUser.userLogin;
            SessionManager.Instance.SetActivePatient(unitnum, apptid);
            //SessionManager.Instance.GetActivePatient().LoadFullObject();
            SessionManager.Instance.GetActivePatient().BackgroundLoadWork();
            RiskApps3.Model.PatientRecord.Patient proband = SessionManager.Instance.GetActivePatient();

            Base64=Helpers.ShowPedigreeImage(proband);

            objPatient.name = proband.name;
            objPatient.age = proband.age;
            objPatient.unitnum = proband.unitnum;
            objPatient.dob = proband.dob;
            objPatient.homephone = proband.homephone;
            objPatient.cellphone = proband.cellphone;
            objPatient.workphone = proband.workphone;
            objPatient.PedigreeImage = Base64;


            /*
            var obj = new { 
                PatientName=proband.name,
                age = proband.age,
                MRN = proband.unitnum,
                DOB = proband.dob,
                HomePhone = proband.homephone,
                CellPhone = proband.cellphone,
                WorkPhone = proband.workphone,
                PedigreeImage = Base64
            };*/


            return objPatient;
            
        
        }


        public System.Data.DataTable GetPatients(int ClinicID)
        {

            
           
            
            
            




            System.Data.DataTable dt = new System.Data.DataTable();
            HighRiskLifetimeBreastQueue obj = new HighRiskLifetimeBreastQueue();
            obj.BackgroundLoadWork();
            dt= obj.dt;

            return dt;
        }


        public List<ViewModels.HighRiskLifetimeBreast> GetPatients()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = GetPatients(-1);

            string type = "";
             type= dt.Columns["patientName"].DataType.ToString();
             type = dt.Columns["unitnum"].DataType.ToString();
             type = dt.Columns["MaxLifetimeScore"].DataType.ToString();
             type = dt.Columns["LastCompApptDate"].DataType.ToString();
             type = dt.Columns["LastMRI"].DataType.ToString();
             type = dt.Columns["Diseases"].DataType.ToString();
             type = dt.Columns["dob"].DataType.ToString();

            List<ViewModels.HighRiskLifetimeBreast> items = dt.AsEnumerable().Select(row =>
                 new ViewModels.HighRiskLifetimeBreast
                 {
                     patientName = row.Field<string>("patientName"),
                     unitnum = row.Field<string>("unitnum"),
                     MaxLifetimeScore = row.Field<double>("MaxLifetimeScore"),
                     LastCompApptDate = row.Field<DateTime?>("LastCompApptDate"),
                     LastMRI = row.Field<DateTime?>("LastMRI"),
                     Diseases = row.Field<string>("Diseases"),
                     dob = row.Field<string>("dob")

                 }).ToList();

            return items;
        }




    }
}

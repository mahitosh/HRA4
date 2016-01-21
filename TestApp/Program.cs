using HRA4.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;
using HRA4.Entities;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.MetaData;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDefaultTemplates();
            
            //IApplicationContext app = new ApplicationContext();
            //var tmp = app.ServiceContext.AdminService.GetTenants();

            //DRIVER=SQL SERVER;SERVER=.\SQLEXPRESS;DATABASE=db2008;APP=RISKAPP;WSID=RISKAPPSWSID;UID=bddb_User;MultipleActiveResultSets=True;
            //dZUoEraPpGiOwzBShnz/ug==

            //string tmp1 = Guid.NewGuid().ToString();

            //AddSuperAdmin();
            User user = new User()
            {                
                userLogin = "admin"
            };
            string assignedBy = "admin";
            Patient p = SessionManager.Instance.GetActivePatient();    // TODO:  Check this!!
            var t = new RiskApps3.Model.PatientRecord.Communication.Task(p, "Task", null, assignedBy, DateTime.Now);
           
        }

        public static void AddSuperAdmin()
        {
            string config = File.ReadAllText(@"D:\Projects\Official\HRA\Sprints\Common\config.xml");
            string connectionString = System.Configuration.ConfigurationSettings.AppSettings["InstitutionDbConnection"].ToString();
            string password = System.Configuration.ConfigurationSettings.AppSettings["InstitutionPassword"].ToString();
            config = config.Replace("[DBCONNECTION]", connectionString);
            config = config.Replace("[PWD]", password);

            string dbscript = File.ReadAllText(@"C:\Users\mkumar\Desktop\Script2008.sql");


            string conn1 = "Server=.\\SQLEXPRESS;Database=RiskappCommon;User Id=sa;Password=mk#12345;";
            //We cannot run/use Simple.Data when support for legacy framework is allowed.
            dynamic commonDbContext = Simple.Data.Database.OpenConnection(conn1);

            SuperAdmin admin = new SuperAdmin()
            {
                FirstName = "Super",
                LastName = "Admin",
                UserName = "sadmin",
                Password = "sadmin",
                ForceResetPassword = true,
                DatabaseSchema = dbscript,
                ConfigurationTemplate = config

            };
           
        }

        private static void CreateDefaultTemplates()
        {
            string conn1 = "Server=.\\SQLEXPRESS;Database=RiskappCommon;User Id=sa;Password=mk#12345;";
            //We cannot run/use Simple.Data when support for legacy framework is allowed.
            dynamic commonDbContext = Simple.Data.Database.OpenConnection(conn1);
            List<HtmlTemplate> templates = commonDbContext.HtmlTemplate.FindAllByInstitutionId(0);
            int count = templates.Count();
            try
            {
                if (count == 0)
                {


                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\ScreeningMammoMriLmn.html", 1001, "MRI LMN", "Screening");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\RiskClinicResultsLetter.html", 105, "Risk Clinic Test Results PCP Letter", "riskClinic");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\LMN.html", 120, "Letter of Medical Necessity", "LMN");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\TestRelativeMutationKnown.html", 140, "Known Mutation Relative Letter", "relativeKnownMutationLetter");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\TestRelative.html", 130, "Consider Testing Relative Letter", "relativeLetter");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\SurveySummary.html", 1, "Survey Summary", "surveySummary");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\ScreeningMammoPcpLetter.html", 1000, "Screening Mammogrpahy PCP Letter", "Screening");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\ScreeningMammoPatientLetter.html", 1002, "Screening Mammogrpahy Patient Letter", "Screening");
                    CreateTemplate(commonDbContext, @"C:\Program Files\RiskAppsV2\templates\html\RiskClinicLetter.html", 100, "Risk Clinic New Patient PCP Letter", "riskClinic");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private static void CreateTemplate(dynamic commonDbContext,string path,int raTemplateId,string templateName,string routineName)
        {
            try
            {
                var templateHtml = File.ReadAllBytes(path);
                HtmlTemplate template = new HtmlTemplate()
                {
                    InstitutionId = 0,
                    RATemplateId = raTemplateId,
                    Template = templateHtml,
                    TemplateName = templateName,
                    RoutineName = routineName

                };

                var newTemp = commonDbContext.HtmlTemplate.Insert(template);
                 
            }
            catch (Exception ex)
            {
                
                 
            }
          
        }
    }
}

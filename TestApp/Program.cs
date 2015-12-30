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
            commonDbContext.SuperAdmin.Insert(admin);

        }
    }
}

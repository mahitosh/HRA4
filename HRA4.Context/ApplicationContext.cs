using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApps3.Model.MetaData;
using HRA4.Services.Interfaces;
using HRA4.Services;
using RiskApps3.Controllers;
using System.Xml;
using System.Web;
using Simple.Data;
using HRA4.Utilities;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
namespace HRA4.Context
{
    public class ApplicationContext:IApplicationContext
    {
        User _user;
        dynamic commonDbContext;
        IServiceContext _service;
        IRepositoryFactory _repository;
        /// <summary>
        /// Constructor to initialize application context.
        /// </summary>
        public ApplicationContext()
        {            
            InitializeCommonDbContext();            
            InitializeTenantContext();
            InitializeServices();
            InitRiskAppContext();
            
        }

        private void InitRiskAppContext()
        {
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
           // HttpContext.Current.User.Identity.Name;
           
        }

        public IServiceContext ServiceContext
        {
            get
            {
                if (_service == null)
                {
                    InitializeServices();
                    
                }
                return _service;

            }
        }

        private void InitializeCommonDbContext()
        {
            string connectionString = ConfigurationSettings.CommonDbConnection;
            commonDbContext = Database.OpenConnection(connectionString);                        
            _repository = new Repositories.RepositoryFactory(commonDbContext);
            
        }



        private void InitializeTenantContext()
        {
            // Add code to initiate connection to tenant db and pass to service layer.
        }

        private void InitializeServices()
        {
            Tenant tenant = _repository.TenantRepository.GetTenantById(3);
            
            if(tenant !=null)
            {
                HttpContext.Current.Session["TenantId"] = tenant.Id.ToString();
                HttpRuntime.Cache[tenant.Id.ToString()] = tenant.Configuration;
            }
            _service = new ServiceContext(_repository);
            
            //XmlDocument xDoc = new XmlDocument();
            //xDoc.Load(@"C:\Program Files\RiskAppsV2\tools\config.xml");
            //HttpContext.Current.Session["Configuration"] = xDoc.OuterXml;
            //SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            //_user = new User();
        }

        //public IServiceContext Services
        //{
        //    get
        //    {
        //        InitializeServices();
        //        return new Service.ServiceContext(_user);
        //    }
        //}

        
    }
}

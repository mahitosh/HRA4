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
        string _username;
        dynamic commonDbContext;
        IServiceContext _service;
        IRepositoryFactory _repository;
        /// <summary>
        /// Constructor to initialize application context.
        /// </summary>
        public ApplicationContext()
        {            
            InitializeCommonDbContext();
            InitRiskAppContext();
            InitializeServices();        
            
        }

        private void InitRiskAppContext()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["InstitutionId"] != null)
            {
                int id = Convert.ToInt32(HttpContext.Current.Session["InstitutionId"]);
                Institution tenant = _repository.TenantRepository.GetTenantById(id);
                if (tenant != null)
                {
                    HttpContext.Current.Session["InstitutionId"] = tenant.Id.ToString();
                    HttpRuntime.Cache[tenant.Id.ToString()] = "";// change the way configuration file gets loaded. This should be per instituion.                   
                }
            }

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
            if(_repository == null)
            {
                string connectionString = ConfigurationSettings.CommonDbConnection;
                commonDbContext = Database.OpenConnection(connectionString);
                _repository = new Repositories.RepositoryFactory(commonDbContext);
            }         
            
        }


        private void InitializeServices()
        {
            //_username = HttpContext.Current.User.Identity.Name;
            _username = "sadmin";
            _service = new ServiceContext(_repository,_username);            
          
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
using HRA4.Utilities;
using System.Web;
using log4net;
namespace HRA4.Services
{
     class AdminService:Interfaces.IAdminService
    {
        IRepositoryFactory _repositoryFactory;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AdminService));
        public AdminService(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }

        public string GetConfiguration()
        {
            return "";
        }


        public List<Entities.Institution> GetTenants()
        {            
            return _repositoryFactory.TenantRepository.GetAll();
        }

        public bool Login(string username, string password)
        {
            SuperAdmin admin =_repositoryFactory.SuperAdminRepository.GetAdminUser();
            return (admin.UserName == username && admin.Password == password);            
        }


        public void UpdatePassword(string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
                

        public Entities.Institution AddUpdateTenant(Entities.Institution tenant)
        {
            tenant.DbName = Helpers.GenerateDbName(tenant.InstitutionName);
            return _repositoryFactory.TenantRepository.AddUpdateTenant(tenant);
        }

        public void UpdateTenantById(int Id )
        {
            Institution _Institution = _repositoryFactory.TenantRepository.GetTenantById(Id);
            _Institution.IsActive = false;
             int r = _repositoryFactory.TenantRepository.UpdateTenant(_Institution);           

        }

         /// <summary>
         /// Insert template records in database for an Institution.
         /// </summary>
         /// <param name="institutionId">Institution Id for which template has to be created.</param>
        private void CreateTemplateRecords(int institutionId)
        {
            Logger.Debug("CreateTemplateRecords: Start");
            try
            {
                List<HtmlTemplate> htmlTemplates = _repositoryFactory.HtmlTemplateRepository.GetAllTemplates(0);
                foreach (var template in htmlTemplates)
                {
                    template.InstitutionId = institutionId;
                    _repositoryFactory.HtmlTemplateRepository.Insert(template);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            Logger.Debug("CreateTemplateRecords: End");

        }
        
        public bool CreateTenantDb(Institution tenant,string scriptPath)
        {
           
            Task.Factory.StartNew(() => CreateTemplateRecords(tenant.Id));

            string dbscript = System.IO.File.ReadAllText(scriptPath);

            string tenantDbName = tenant.DbName;
            
            dbscript = dbscript.Replace("db2008", tenantDbName); // contruct db name using institution name
            
            //End By Aditya
            string connectionString = ConfigurationSettings.CommonDbConnection;
          
            Helpers.CreateInstitutionDb(connectionString, dbscript);
            
            return true;
        }


        public string GetUserName()
        {
            SuperAdmin admin = _repositoryFactory.SuperAdminRepository.GetAdminUser() as SuperAdmin;
            return string.Format("{0} {1}", admin.FirstName, admin.LastName);
        }

        public string GetInstitutionName(int institutionId)
        {
            Institution _Institution = _repositoryFactory.TenantRepository.GetTenantById(institutionId);
            return _Institution.InstitutionName;
        }
    }
}

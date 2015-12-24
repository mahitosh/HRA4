using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
using HRA4.Utilities;
using System.Web;
namespace HRA4.Services
{
     class AdminService:Interfaces.IAdminService
    {
        IRepositoryFactory _repositoryFactory;
        
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


        public bool CreateTenantDb(Institution tenant,string scriptPath)
        {
           // SuperAdmin admin = _repositoryFactory.SuperAdminRepository.GetAdminUser();
            //Added by Aditya on 21-12-2015

            

            string dbscript = System.IO.File.ReadAllText(scriptPath);

            string tenantDbName = tenant.DbName;

            dbscript = dbscript.Replace("db2008", tenantDbName); // contruct db name using institution name
            
            //End By Aditya
            string connectionString = ConfigurationSettings.CommonDbConnection;

            Helpers.CreateInstitutionDb(connectionString, dbscript);
            
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
using HRA4.Utilities;
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
            return _repositoryFactory.TenantRepository.AddUpdateTenant(tenant);
        }


        public bool CreateTenantDb(Institution tenant)
        {
            SuperAdmin admin = _repositoryFactory.SuperAdminRepository.GetAdminUser();
            //Added by Aditya on 21-12-2015
            string dbscript = admin.DatabaseSchema;
            dbscript=dbscript.Replace("db2008",tenant.DbName);
            //End By Aditya
            string connectionString = "Server=.\\SQLEXPRESS;Database=RiskappCommon;User Id=sa;Password=mk#12345;";

            Helpers.CreateInstitutionDb(connectionString, dbscript);


            return true;
        }
    }
}

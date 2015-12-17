using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Repositories.Interfaces;
using HRA4.Entities;
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


        public List<Entities.Tenant> GetTenants()
        {
            throw new NotImplementedException();
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


        public Entities.Tenant AddUpdateTenant(Entities.Tenant tenant)
        {
            return _repositoryFactory.TenantRepository.AddUpdateTenant(tenant);
        }
    }
}

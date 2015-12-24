using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
namespace HRA4.Services.Interfaces
{
    public interface IAdminService
    {
        string GetConfiguration();
        List<Institution> GetTenants();
        bool Login(string username, string password);
        void UpdatePassword(string oldPassword, string newPassword);
        Institution AddUpdateTenant(Institution tenant);
        bool CreateTenantDb(Institution tenant,string scriptPath);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
namespace HRA4.Repositories.Interfaces
{
    public interface ITenantMasterRepository
    {
        Tenant AddUpdateTenant(Tenant tenant);
        Tenant GetTenantById(int id);
        List<Tenant> GetAll();


    }
}

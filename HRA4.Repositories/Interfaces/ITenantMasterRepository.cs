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
        Institution AddUpdateTenant(Institution tenant);
        int UpdateTenant(Institution tenant);
        Institution GetTenantById(int id);
        List<Institution> GetAll();


    }
}

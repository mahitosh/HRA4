using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;

namespace HRA4.Repositories
{
    public class TenantRepository : Interfaces.ITenantMasterRepository
    {
        dynamic dbContext;

        public TenantRepository(dynamic database)
        {
            dbContext = database;
        }

        public Tenant AddUpdateTenant(Entities.Tenant tenant)
        {
            return (Tenant)dbContext.TenantMaster.Insert(tenant);
        }

        public Tenant GetTenantById(int id)
        {
            return (Tenant)dbContext.TenantMaster.FindById(id);
        }

        public List<Entities.Tenant> GetAll()
        {
            return dbContext.TenantMaster.All();
        }
    }
}

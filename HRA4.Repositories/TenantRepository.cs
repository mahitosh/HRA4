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

        public Institution AddUpdateTenant(Entities.Institution tenant)
        {
            return (Institution)dbContext.TenantMaster.Insert(tenant);
        }

        public Institution GetTenantById(int id)
        {
            return (Institution)dbContext.TenantMaster.FindById(id);
        }

        public List<Entities.Institution> GetAll()
        {
            return dbContext.TenantMaster.All();
        }
    }
}

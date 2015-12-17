using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;
namespace HRA4.Repositories
{
    public class RepositoryFactory: Interfaces.IRepositoryFactory
    {
        dynamic dbContext;

        public RepositoryFactory(dynamic database)
        {
            this.dbContext = database;
        }
        public Interfaces.ISuperAdminRepository SuperAdminRepository
        {
            get { return new SuperAdminRepository(this.dbContext); }
        }

        public Interfaces.ITenantMasterRepository TenantRepository
        {
            get { return new TenantRepository(this.dbContext); }
        }
    }
}

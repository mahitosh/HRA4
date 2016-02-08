using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;
namespace HRA4.Repositories
{
    public class SuperAdminRepository:Interfaces.ISuperAdminRepository
    {

        dynamic dbContext;

        public SuperAdminRepository(dynamic database)
        {
            dbContext = database;             
        }

        public SuperAdminRepository(string connection)
        {
            this.dbContext = Database.OpenConnection(connection);
        }



        public Entities.SuperAdmin GetAdminUser()
        {
            List<Entities.SuperAdmin> SAdmins = dbContext.SuperAdmin.All();
            return SAdmins.Where(s=> s.Id == 1).FirstOrDefault();
        }

        public void UpdateAdminUser(Entities.SuperAdmin superAdmin)
        {
            dbContext.SuperAdmin.Update(superAdmin);
        }
    }
}

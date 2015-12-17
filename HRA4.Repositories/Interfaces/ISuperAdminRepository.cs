using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
namespace HRA4.Repositories.Interfaces
{
    public interface ISuperAdminRepository
    {        
        SuperAdmin GetAdminUser();
        void UpdateAdminUser(SuperAdmin sadmin);

    }
}

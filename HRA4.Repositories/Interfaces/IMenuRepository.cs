using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> GetMenu();
        string GetMenuRights(int roleId);
        string GetExcludeControlIds(int roleId);
    }
}

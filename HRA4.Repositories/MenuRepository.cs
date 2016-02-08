using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities.UserManagement;
namespace HRA4.Repositories
{
    public class MenuRepository : Interfaces.IMenuRepository
    {
        dynamic dbContext;

        public MenuRepository(dynamic database)
        {
            dbContext = database;
        }

        public List<Entities.Menu> GetMenu()
        {
            return dbContext.Menu.All();
        }

        public string GetMenuRights(int roleId)
        {
            List<MenuRights> menuRights = dbContext.MenuRights.All();
            string menuIds = menuRights.FirstOrDefault(m => m.RoleId == roleId).MenuIds;
            return menuIds;
        }


        public string GetExcludeControlIds(int roleId)
        {
            List<MenuRights> menuRights = dbContext.MenuRights.All();
            string excludeIds = menuRights.FirstOrDefault(m => m.RoleId == roleId).ExcludeControlIds;
            return excludeIds??string.Empty;
        }
    }
}

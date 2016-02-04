using RiskApps3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
using HRA4.ViewModels;
namespace HRA4.Services.Interfaces
{
    public interface IUserService
    {
        List<ViewModels.User> GetUsers();
        int GetRoleId(string username);
        string GetRoles(string username);
        List<Menu> GetMenus(int RoleId);
        string[] GetExcludeControlIds(int RoleId);
        bool AuthenticateUser(string username, string password, out string msg, out string fullName);
        List<UserProfile> GetUserProfile(string username);
        UserProfile AddNewProfile(string username);
    }
}

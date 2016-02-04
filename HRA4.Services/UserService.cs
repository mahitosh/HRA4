using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities.UserManagement;
using HRA4.Entities;
using HRA4.Repositories.Interfaces;
using RA = RiskApps3.Model.MetaData;
using RiskApps3.Utilities;
using RiskAppCore;
using System.Data.SqlClient;
using RiskApps3.Model;
using HRA4.Mapper;
using HRA4.ViewModels;
using RiskApps3.Model.MetaData;
namespace HRA4.Services
{
    public class UserService : IUserService
    {
        SampleData sample;
        private IRepositoryFactory _repositoryFactory;
        IHraSessionManager _hraSessionManager;
        public UserService(IRepositoryFactory _repositoryFactory, IHraSessionManager hraSessionManger)
        {
            this._repositoryFactory = _repositoryFactory;
            _hraSessionManager = hraSessionManger;

            sample = new SampleData();
        }

        public List<ViewModels.User> GetUsers()
        {

            SessionManager.Instance.MetaData.Users.BackgroundListLoad();

            var raUsers = SessionManager.Instance.MetaData.Users.GroupBy(u => u.userLogin).Select(g => g.First());
            List<ViewModels.User> users = new List<ViewModels.User>();
            foreach(RiskApps3.Model.MetaData.User u in raUsers)
            {
                users.Add(u.FromRUser());
            }
            return users;
        }

        /// <summary>
        /// Gets the pipe separated string of roles for the given username.
        /// </summary>
        /// <param name="username">User name whose role is needed.</param>
        /// <returns>Returns the pipe separated string of roles for the given username.</returns>
        public string GetRoles(string username)
        {
            var userId = sample.SUsers.FirstOrDefault(u => u.Username == username).Id;
            var tmproles = sample.RolesMatrix.GroupBy(r => r.UserId).Select(grp => grp.First());
            var roles = tmproles.Where(r => r.UserId == userId);
            string roleString = "";
            foreach (var ids in roles)
            {
                var _role = sample.Roles.Where(d => d.Id == ids.RoleId).FirstOrDefault();
                roleString += _role.RoleName + "|";
            }
            return roleString;
        }


        public List<Menu> GetMenus(int RoleId)
        {
            List<Menu> menus = new List<Menu>();
            var menuIds = _repositoryFactory.MenuRepository.GetMenuRights(RoleId);
            var menuList = _repositoryFactory.MenuRepository.GetMenu();

            if (menuIds != null)
                foreach (var menuId in menuIds.Split('|'))
                {
                    var tmp = menuList.Where(m => m.Id == Convert.ToInt32(menuId)).FirstOrDefault();
                    if (tmp != null)
                        menus.Add(tmp);
                }
            return menus;
        }


        public int GetRoleId(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                var userId = sample.SUsers.FirstOrDefault(u => u.Username == username).Id;
                var tmproles = sample.RolesMatrix.GroupBy(r => r.UserId).Select(grp => grp.First());
                var roles = tmproles.Where(r => r.UserId == userId).FirstOrDefault();
                return roles.RoleId;
            }
            return 0;
        }


        public string[] GetExcludeControlIds(int RoleId)
        {
            var excludeIds = _repositoryFactory.MenuRepository.GetExcludeControlIds(RoleId);

            return excludeIds.Split('|');
        }

        public bool AuthenticateUser(string username, string password, out string msg, out string fullName)
        {
            RA.User u = null;
            bool result = false;
            SessionManager.Instance.MetaData.Users.BackgroundListLoad();
            fullName = string.Empty;

            var users = SessionManager.Instance.MetaData.Users;

            bool validUser = users.Any(x => ((RA.User)x).userLogin.ToLower() == username.ToLower());

            if (validUser) u = (RA.User)(users.First(x => ((RA.User)x).userLogin.ToLower() == username.ToLower()));

            String encryptedPassword = RiskAppCore.User.encryptPassword(password);

            bool authenticated = false;
            int numFailedAttempts = 0;
            int numMaxFailures = 5;
            int lockoutPeriod = 0;
            string lockoutMsg = "";

            RiskApps3.Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();
            pc.Add("ntLoginName", "");
            pc.Add("userLogin", (u != null) ? u.userLogin : username);

            pc.Add("userPassword", encryptedPassword);



            SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_Authenticate_User", pc);

            if (reader != null)
            {
                if (reader.Read())
                {
                    authenticated = (bool)reader.GetSqlBoolean(0);
                    numFailedAttempts = (int)reader.GetInt32(1);
                    numMaxFailures = (int)reader.GetInt32(2);
                    lockoutPeriod = (int)reader.GetInt32(3);
                }
                reader.Close();
            }
            if ((!validUser) || (!authenticated))  //note that if they're not a valid user they won't be authenticated, but we've updated the failed count and timeout values
            {
                if (lockoutPeriod > 0)
                    lockoutMsg = "\r\nLogin attempts will be blocked for " + lockoutPeriod.ToString() + " minute" + ((lockoutPeriod > 1) ? "s." : ".");
                else
                    lockoutMsg = "\r\nYou have made " + numFailedAttempts.ToString() + " failed Login attempt" + ((numFailedAttempts > 1) ? "s" : "") + " of a maximum " + numMaxFailures.ToString() + " allowed.";
            }

            msg = lockoutMsg;

            if (numFailedAttempts == 1)
            {
                msg = "You have provided an incorrect username or password.\r\nPlease correct your password or try a different user." + lockoutMsg;
            }



            result = (validUser && authenticated);

            if (result)
            {
                SessionManager.Instance.ActiveUser = u;
                HraObject.AuditUserLogin(u.userLogin);
                fullName = u.User_userFullName;
                _hraSessionManager.SetRaActiveUser(username);
            }

            return result;
        }
        
        public UserProfile AddNewProfile(string username)
        {
            // Add a new profile entry and return
            UserProfile profile = new UserProfile()
            {
                SelectedClinicId = 2,
                SelectedRoleId = "Administrator",
                SelectedModule = "Breast Imaging",
                User = GetUsers().Where(u => u.Username == username).FirstOrDefault(),
                Clinics = GetClinics(username),
                Roles = new List<string>()
                        {
                            "Administrator",
                            "Clinician"
                        },
            };
            return profile;
        }

        public List<UserProfile>  GetUserProfile(string username)
        {
            List<UserProfile> profile = new List<UserProfile>();
            //Get all the profiles for the users and create list.
            profile.Add(new UserProfile()
            {
                SelectedClinicId = 2,
                SelectedRoleId="Administrator",
                SelectedModule = "Breast Imaging",
                User = GetUsers().Where(u => u.Username == username).FirstOrDefault(),
                Clinics = GetClinics(username),
                Roles = new List<string>()
                        {
                            "Administrator",
                            "Clinician"
                        },
            });
           
            return profile;
        }

        private List<ViewModels.Clinic> GetClinics(string userid)
        {
            var list = new ClinicList();
            list.user_login = _hraSessionManager.Username;
            list.BackgroundListLoad();
            return list.FromRClinicList();

        }



    }
}

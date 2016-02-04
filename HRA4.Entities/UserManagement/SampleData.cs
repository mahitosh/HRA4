using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HRA4.Entities.UserManagement
{
    public class SampleData
    {
        public List<Role> Roles { get; set; }
        public List<Features> Features { get; set; }
        public List<RoleMatrix> RolesMatrix { get; set; }
        public List<SUser> SUsers { get; set; }
        public List<Menu> Menus { get; set; }
        public List<RoleMenus> RoleMenus { get; set; }
        public SampleData()
        {
            Menus = CreateMenu();
            LoadRoles();
            LoadRolesMatrix();
            LoadUsers();
            LoadMenusByRole();
        }

        private void LoadMenusByRole()
        {
            RoleMenus = new List<RoleMenus>();
            RoleMenus.Add(new RoleMenus()
            {
                Id=1,
                RoleId = 1,
                MenuIds="1|2|3|4|5|6|7|8"
            });

            RoleMenus.Add(new RoleMenus()
            {
                Id = 2,
                RoleId = 2,
                MenuIds = "1|3|4|5|6|7|8"
            });
            RoleMenus.Add(new RoleMenus()
            {
                Id = 3,
                RoleId = 3,
                MenuIds = "4|5|6|7|8"
            });
        }

        private void LoadUsers()
        {
            SUsers = new List<SUser>();
            SUsers.Add(new SUser()
            {
                Id=1,
                Username = "sadmin",
                Role = "SuperAdmin|Administrator|Clinician",
                RoleId = 1
            });
            SUsers.Add(new SUser()
            {
                Id = 2,
                Username = "admin",
                Role = "Administrator|Clinician",
                RoleId = 2
            });
            SUsers.Add(new SUser()
            {
                Id = 3,
                Username = "mohit",
                Role = "Clinician",
                RoleId = 3
            });
        }

        private void LoadRolesMatrix()
        {
            RolesMatrix = new List<RoleMatrix>();
            //super admin
            #region SuperAdmin

            
            RolesMatrix.Add(new RoleMatrix()
            {
                Id= 1,
                UserId=1,
                RoleId = 1,
                MenuId  = 1
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 2,
                RoleId = 1,
                UserId = 1,
                MenuId  = 2
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 3,
                RoleId = 1,
                UserId = 1,
                MenuId = 3
            });

            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 4,
                RoleId = 1,
                UserId = 1,
                MenuId = 4
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 5,
                RoleId = 1,
                UserId = 1,
                MenuId = 5
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 6,
                RoleId = 1,
                UserId = 1,
                MenuId = 6
            });
            #endregion

            //clinic aadmin
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 7,
                RoleId = 2,
                UserId = 2,
                MenuId = 1
            });
            //RolesMatrix.Add(new RoleMatrix()
            //{
            //    Id = 8,
            //    RoleId = 1,
            //    MenuId = 2
            //});
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 8,
                RoleId = 2,
                UserId = 2,
                MenuId = 3
            });

            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 9,
                RoleId = 2,
                UserId = 2,
                MenuId = 4
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 10,
                RoleId = 2,
                UserId = 2,
                MenuId = 5
            });
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 11,
                RoleId = 2,
                UserId = 2,
                MenuId = 6
            });
            
            //Clinician
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 12,
                RoleId = 3,
                UserId = 3,
                MenuId = 2
            });
            
            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 13,
                RoleId = 3,
                UserId = 3,
                MenuId = 3
            });

            RolesMatrix.Add(new RoleMatrix()
            {
                Id = 14,
                RoleId = 3,
                UserId = 3,
                MenuId = 5
            });
            
         

        }

        private void LoadRoles()
        {
            Roles = new List<Role>();
            Roles.Add(new Role()
            {
                Id = 1,
                RoleName="SuperAdmin"
            });
            Roles.Add(new Role()
            {
                Id = 2,
                RoleName = "Administrator"
            });
            Roles.Add(new Role()
            {
                Id = 3,
                RoleName = "Clinician"
            });
        }

        private List<Menu> CreateMenu()
        {
            List<Menu> menulist = new List<Menu>();

            Menu menu = new Menu()
            {
                Id=1,
                MenuName = "Manage Users",
                Roles = "SuperAdmin|Administrator",
                Controller = "User",
                Action = "Index"
            };
            menulist.Add(menu);

            menu = new Menu()
            {
                Id = 2,
                MenuName = "Manage Institution",
                Roles = "SuperAdmin",
                Controller = "Admin",
                Action = "ManageInstitution"
            };
            menulist.Add(menu);

            menu = new Menu()
            {
                Id = 2,
                MenuName = "Manage Providers",
                Roles = "SuperAdmin|Administrator",
                Controller = "Providers",
                Action = "Index"
            };
            menulist.Add(menu);

            menu = new Menu()
            {
                Id = 3,
                MenuName = "Manage Documents",
                Roles = "SuperAdmin|Administrator",
                Controller = "DocumentEditor",
                Action = "Index"
            };
            menulist.Add(menu);

            menu = new Menu()
            {
                Id = 4,
                MenuName = "Audit Reports",
                Roles = "SuperAdmin|Administrator",
                Controller = "Reports",
                Action = "AuditReports"
            };
            menulist.Add(menu);


            menu = new Menu()
            {
                Id = 5,
                MenuName = "TestPatient",
                Roles = "SuperAdmin|Administrator",
                Controller = "TestPatient",
                Action = "TestPatients"
            };
            menulist.Add(menu);

            menu = new Menu()
            {
                Id = 6,
                MenuName = "Manage Clinics",
                Roles = "SuperAdmin|Administrator",
                Controller = "Clinics",
                Action = "Index"
            };
            menulist.Add(menu);




            return menulist;
        }
    }
}

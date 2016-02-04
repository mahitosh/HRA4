using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities.UserManagement
{
    public class SUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public List<Role> Roles { get; set; }
    }
}

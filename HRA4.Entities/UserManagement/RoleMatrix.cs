using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities.UserManagement
{
    public class RoleMatrix
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClinicId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }
}

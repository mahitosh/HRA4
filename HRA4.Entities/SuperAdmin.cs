using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities
{
    public class SuperAdmin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool ForceResetPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DatabaseSchema { get; set; }

    }
}

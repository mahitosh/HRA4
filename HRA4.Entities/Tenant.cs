using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string TenantName { get; set; }
        public string Description { get; set; }
        public string DbName { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public string Configuration { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities 
{
    public class Menu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public string Roles { get; set; }
        public int MenuOrder { get; set; }
        public string Url { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string cssClass { get; set; }

    }
}

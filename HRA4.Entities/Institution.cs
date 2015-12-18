using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HRA4.Entities
{
    public class Institution
    {
        public int Id { get; set; }
        public string InstitutionName { get; set; }
        public string Description { get; set; }
        public string DbName { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        [AllowHtml]        
        public string Configuration { get; set; }

    }
}

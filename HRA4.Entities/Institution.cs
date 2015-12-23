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
        public string DbName { get; set; }
        public string RaInstitutionId { get; set; }
        public DateTime DateCreated { get; set; }


    }
}

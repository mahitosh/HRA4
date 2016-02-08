using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
   public class UserProfile
    {
        public User User { get; set; }
        public List<Clinic> Clinics { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Modules { get; set; }
        public int SelectedClinicId { get; set; }
        public string SelectedRoleId { get; set; }
        public string SelectedModule { get; set; }
       public UserProfile()
        {
            Clinics = new List<Clinic>();
            Roles = new List<string>();
            Modules = new List<string>()
            {
                "Breast Imaging"
            };
        }
    }
}

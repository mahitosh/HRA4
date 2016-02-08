using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class FamilyHistoryRelative
    {
        public string Relationship { get; set; }
        public string RelativeAge { get; set; }
        public string VitalStatus { get; set; }
        public string FirstDx { get; set; }
        public string FirstAgeOnset { get; set; }
        public string SecondDx { get; set; }
        public string SecondAgeOnset { get; set; }
        public string ThirdDx { get; set; }
        public string ThirdAgeOnset { get; set; }
        public bool DeleteFlag { get; set; }
        public int RelativeId { get; set; }
    }
}

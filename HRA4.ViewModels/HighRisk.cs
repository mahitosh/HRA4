using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HRA4.ViewModels
{
    public class HighRisk
    {

        public string patientName { get; set; }
        public string unitnum { get; set; }
        public double MaxLifetimeScore { get; set; }
        public DateTime? LastCompApptDate { get; set; }
        public DateTime? LastMRI { get; set; }
        public string Diseases { get; set; }
        public string dob { get; set; }
        public int? isRCPt { get; set; }  // ExcludeCancerGeneticsPatients
        public int? genTested { get; set; }  // excludePatientsWithGeneticTesting
        public int? DoNotContact { get; set; } //ExcludeDoNotContactPatients 
        public double MaxBRCAScore { get; set; }
        public string geneNames { get; set; }

        


    }
  
    

    

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class MRI
    {
        public List<string> ListOfrightBirads
        {
            get
            {
                return new List<string>() { 
                "Not Done",
                "0 BIRADS",
                "1 BIRADS",
                "2 BIRADS",
                "1,2 BIRADS",
                "3 BIRADS",
                "4 BIRADS stereo core for calcs",
                "4 BIRADS stereo core for density/mass",
                "4 BIRADS ultrasound core for density/mass",
                "4 BIRADS aspiration",
                "4 BIRADS open biopsy",
                "4 BIRADS Biopsy",
                "4 BIRADS MRI Guided Biopsy",
                "5 BIRADS",
                "6 BIRADS"
            };
            }
            set { }
        }
        public List<string> ListOfleftBirads
        {
            get
            {
                return new List<string>() { 
                "Not Done",
                "0 BIRADS",
                "1 BIRADS",
                "2 BIRADS",
                "1,2 BIRADS",
                "3 BIRADS",
                "4 BIRADS stereo core for calcs",
                "4 BIRADS stereo core for density/mass",
                "4 BIRADS ultrasound core for density/mass",
                "4 BIRADS aspiration",
                "4 BIRADS open biopsy",
                "4 BIRADS Biopsy",
                "4 BIRADS MRI Guided Biopsy",
                "5 BIRADS",
                "6 BIRADS"
            };
            }
            set { }
        }
        public List<string> ListOfside { get { return new List<string>() { "Left", "Right", "Bilateral"}; } set { } }
        public List<string> ListOfstatus { get { return new List<string>() { "Ordered", "Pending", "Complete","Awaiting Authorization" }; } set { } }
        public List<string> ListOfresult { get { return new List<string>() { "Normal", "Abnormal", "Inconclusive" }; } set { } }
        public string rightBirads { get; set; }
        public string leftBirads { get; set; }
        public string unitnum { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public string report { get; set; }
        public DateTime Date { get; set; }
    }
}

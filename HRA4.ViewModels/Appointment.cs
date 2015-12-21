using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string MRN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Provider { get; set; }
        public DateTime DateCompleted { get; set; }
        public string DiseaseHx { get; set; }
        public string Survey { get; set; }
        public bool DoNotCall { get; set; }

        
    }
}

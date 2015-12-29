using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class Appointment
    {
        DateTime appointmentDate;
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string MRN { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }

        public string Provider { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateCompleted { get; set; }

        public string DiseaseHx { get; set; }
        public string Survey { get; set; }
        public bool DoNotCall { get; set; }
        

        
    }
}

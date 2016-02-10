using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RA=RiskApps3.Model.PatientRecord;
using VM=HRA4.ViewModels;
namespace HRA4.Mapper
{
    public static class RiskFactorMapper
    {
        public static RA.PhysicalExamination ToRAPhysicalExamination(this VM.PhysicalData physical,RA.Patient patient)
        {
            return new RA.PhysicalExamination(patient)
            {
                weightPounds = physical.Weight,
                heightFeetInches = physical.Feet,
                heightInches = physical.Inches
            };
        }

        public static VM.PhysicalData FromRAPhysicalExamination(this RA.PhysicalExamination physical)
        {
            return new VM.PhysicalData()
            {
                Weight = physical.weightPounds,
                Inches = GetInches(physical.heightInches),
                Feet = GetFeet(physical.heightInches)
            };
        }

        private static string GetFeet(string heightInches)
        {
            int totalInches = Int32.Parse(heightInches);

            return "" + (totalInches / 12);
             
        }

        private static string GetInches(string heightInches)
        {
            int totalInches = Int32.Parse(heightInches);

            return "" + (totalInches % 12);            
        }
    }
}

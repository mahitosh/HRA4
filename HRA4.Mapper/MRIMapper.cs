using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
using RiskApps3.Model.PatientRecord;
namespace HRA4.Mapper
{
    public static class MRIMapper
    {
        public static MRI FromRABreastImagingStudy(BreastImagingStudy mri)
        {
            return new MRI() { 
            
                rightBirads=mri.rightBirads,
                leftBirads=mri.leftBirads,
                status = mri.status,                
                report = mri.report,
                Date=mri.date,
                result=mri.normal                           
            
            };
        }
    }
}

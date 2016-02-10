using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
namespace HRA4.Services.Interfaces
{
    public interface ISurveyRiskFactors
    {
        void SaveBreastRiskFactors(BreastCancer breastCancer);
        void SaveColorectalRiskFactors(ColorectalCancer colorectal);
    }
}

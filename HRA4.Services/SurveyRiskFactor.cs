using HRA4.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services
{
    public class SurveyRiskFactor:Interfaces.ISurveyRiskFactors
    {
        IHraSessionManager _hraSessionManager;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SurveyRiskFactor));

        public SurveyRiskFactor(IHraSessionManager hraSessionManager)
        {
            this._hraSessionManager = hraSessionManager;
        }

        public void SaveBreastRiskFactors(ViewModels.BreastCancer breastCancer)
        {
            throw new NotImplementedException();
        }

        public void SaveColorectalRiskFactors(ViewModels.ColorectalCancer colorectal)
        {
            throw new NotImplementedException();
        }
    }
}

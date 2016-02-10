using HRA4.Services.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApps3.Model.PatientRecord;
using HRA4.Mapper;
using HRA4.ViewModels;
using RiskApps3.Model;
namespace HRA4.Services
{
    public class SurveyRiskFactor:Interfaces.ISurveyRiskFactors
    {
        IHraSessionManager _hraSessionManager;
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SurveyRiskFactor));
        
        public SurveyRiskFactor(IHraSessionManager hraSessionManager)
        {
            this._hraSessionManager = hraSessionManager;
            //_patient = _hraSessionManager.
        }

        /// <summary>
        /// Intantiate SurveyRiskFactor class.
        /// </summary>
        /// <param name="hraSessionManager">Hra4 Session manager</param>
        /// <param name="mrn">MRN number of patient</param>
        /// <param name="apptId">Appointment Id of the appointment.</param>
        public SurveyRiskFactor(IHraSessionManager hraSessionManager, string mrn,int apptId)
        {
            this._hraSessionManager = hraSessionManager;
            
        }

        public void SaveBreastRiskFactors(ViewModels.BreastCancer breastCancer, string mrn, int apptId)
        {
            _hraSessionManager.SetActivePatient(mrn, apptId);
            var patient = _hraSessionManager.GetActivePatient();
            SavePhysicalData(breastCancer.PhysicalData,patient);
        }

        private void SavePhysicalData(ViewModels.PhysicalDataFactors physicalDataFactors,Patient patient )
        {           

            var physical = physicalDataFactors as PhysicalData;

            PhysicalExamination pe = physical.ToRAPhysicalExamination(patient);
            pe.BackgroundPersistWork(new HraModelChangedEventArgs(null));

        }

        public void SaveColorectalRiskFactors(ViewModels.ColorectalCancer colorectal)
        {
            throw new NotImplementedException();
        }


        public BreastCancer LoadBreastCancerRiskFactors(string mrn,int apptId)
        {
            _hraSessionManager.SetActivePatient(mrn, apptId);
            var patient = _hraSessionManager.GetActivePatient();

            patient.PhysicalExam.BackgroundLoadWork();
            BreastCancer breastCancer = new BreastCancer();
           
            
            breastCancer.PhysicalData = patient.PhysicalExam.FromRAPhysicalExamination();
            return breastCancer;
        }

        public ColorectalCancer LoadColorectalRiskFactors()
        {
            throw new NotImplementedException();
        }

        public void SaveBreastRiskFactors(BreastCancer breastCancer)
        {
            throw new NotImplementedException();
        }

         

        public ColorectalCancer LoadColorectalRiskFactors(string mrn, int apptId)
        {
            throw new NotImplementedException();
        }
    }
}

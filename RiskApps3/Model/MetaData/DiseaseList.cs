using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.MetaData
{
    public class DiseaseList : HRAList
    {        
        private object[] constructor_args;
        private ParameterCollection pc;

        public DiseaseList()
        {
            this.constructor_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadDiseases",
                this.pc,
                typeof(DiseaseObject),
                this.constructor_args);

            DoListLoad(lla);
        }
        /*
        internal void SetDataFromDiseaseName(ref ClincalObservation co)
        {
            if (string.IsNullOrEmpty(co.disease))
                return;

            foreach (DiseaseObject dx in this)
            {
                if (dx.diseaseName.Equals(co.disease))
                {
                    co.ClinicalObservation_diseaseDisplayName = dx.diseaseDisplayName;
                    co.ClinicalObservation_diseaseGender = dx.diseaseGender;
                    co.ClinicalObservation_diseaseIconArea = dx.diseaseIconArea;
                    co.ClinicalObservation_diseaseIconColor = dx.diseaseIconColor;
                    co.ClinicalObservation_diseaseIconType = dx.diseaseIconType;
                    co.ClinicalObservation_diseaseOrder = dx.diseaseOrder;
                    co.ClinicalObservation_diseaseShortName = dx.diseaseShortName;
                    co.ClinicalObservation_diseaseSyndrome = dx.diseaseSyndrome;
                    break;
                }
               
            }
            if (co.disease.Contains("Breast Cancer"))
            {
                co.Details = new RiskApps3.Model.PatientRecord.PMH.BreastCancerDetails();
                co.Details.owningClincalObservation = co;
                co.Details.BackgroundLoadWork();
            }
            else
            {
                co.Details = null;
            }
        }
         */
    }
}

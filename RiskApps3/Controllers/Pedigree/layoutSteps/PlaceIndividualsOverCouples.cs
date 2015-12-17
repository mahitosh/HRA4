using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Responsible for placing individuals based on the couples locations while the couples graph is not planar
    /// </summary>
    class PlaceIndividualsOverCouples
    {
        public readonly LayoutStep step;
        public PlaceIndividualsOverCouples(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                if(!model.couplesGraphIsPlanar)
                    foreach (PedigreeCouple couple in model.couples)
                    {
                        couple.mother.point.Set(couple.point);
                        couple.father.point.Set(couple.point);
                        foreach(PedigreeIndividual child in couple.children)
                            child.point.Set(couple.point);

                    }
            };
        }
    }
}

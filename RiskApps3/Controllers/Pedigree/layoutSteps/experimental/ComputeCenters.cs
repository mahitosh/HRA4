using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Computes the midpoints between the centers of couples and
    /// their sibships, storing the result in couple.point.x.
    /// </summary>
    class ComputeCenters
    {
        public readonly LayoutStep step;
        public ComputeCenters(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                if (model.couplesGraphIsPlanar)
                {
                    foreach (PedigreeCouple couple in model.couples)
                    {
                        if (!model.pointsBeingDragged.Contains(couple.point))
                        {
                            //compute the center for the couple
                            if (couple.mother != null && couple.father != null)
                            {
                                double motherX = couple.mother.point.x;
                                double motherY = couple.mother.point.y + model.parameters.individualSize / 2;
                                double fatherX = couple.father.point.x;
                                double fatherY = couple.father.point.y + model.parameters.individualSize / 2;
                                double coupleCenterX = (motherX + fatherX) / 2;

                                //compute the center for the couple's sibship
                                double sibshipMinX = double.MaxValue;
                                double sibshipMaxX = -double.MaxValue;
                                foreach (PedigreeIndividual child in couple.children)
                                {
                                    if (child.point.x < sibshipMinX)
                                        sibshipMinX = (int)child.point.x;
                                    if (child.point.x > sibshipMaxX)
                                        sibshipMaxX = (int)child.point.x;
                                }

                                double sibshipY = couple.point.y +
                                    model.parameters.verticalSpacing - model.parameters.individualSize / 2;
                                double sibshipCenterX = (sibshipMinX + sibshipMaxX) / 2;

                                //this is the middle point between parents and children from which
                                //ideal positions of both are derived
                                double middleX = (sibshipCenterX + coupleCenterX) / 2;
                                couple.point.x = middleX;
                            }
                        }
                    }
                }
            };
        }
    }
}

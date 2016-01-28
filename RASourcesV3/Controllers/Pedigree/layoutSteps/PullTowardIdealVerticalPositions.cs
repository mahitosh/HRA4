using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// This layout step pulls all couples and individuals toward their ideal computed
    /// locations such that the pedigree is centered vertically
    /// </summary>
    class PullTowardIdealVerticalPositions
    {
        public readonly LayoutStep step;
        public PullTowardIdealVerticalPositions(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                double spacing = model.parameters.verticalSpacing;
                double centerY = (model.displayYMin + model.displayYMax) / 2;
                double offset = centerY - (model.maxGenerationalLevel + 1) * spacing / 2;
                foreach (PedigreeCouple couple in model.couples)
                {
                    
                    //pull the couples toward their optimal positions
                    //based on their generational level
                    int level = couple.GenerationalLevel;
                    if (level >= 0)
                    {
                        double goalY = level * spacing + offset;
                        double strength = model.parameters.verticalPositioningForceStrength;
                        PedigreeUtils.PullTowardY(goalY, couple.point, strength, model);

                        //position the individuals vertically based on the 
                        //positions of the couples:
                        goalY = couple.point.y;
                        strength = model.parameters.verticalPositioningForceStrength;
                        if (couple.mother != null)
                            if (model.pointsBeingDragged.Contains(couple.mother.point) == false)
                            {
                                PedigreeUtils.PullTowardY(goalY, couple.mother.point, strength, model);
                                //foreach (PedigreeCouple pc in couple.mother.spouseCouples)
                                //{
                                //    PedigreeUtils.PullTowardY(goalY, pc.father.point, strength, model);
                                //}
                            }
                        if (couple.father != null)
                            if (model.pointsBeingDragged.Contains(couple.father.point) == false)
                            {
                                PedigreeUtils.PullTowardY(goalY, couple.father.point, strength, model);
                                //foreach (PedigreeCouple pc in couple.father.spouseCouples)
                                //{
                                //    PedigreeUtils.PullTowardY(goalY, pc.mother.point, strength, model);
                                //}
                            }
                        foreach (PedigreeIndividual pi in couple.children)
                        {
                            if (model.pointsBeingDragged.Contains(pi.point) == false)
                            {
                                if ((couple.mother != null) && (couple.father != null))
                                {
                                    double target = ((couple.mother.point.y + couple.father.point.y) / 2) + (model.parameters.verticalSpacing);
                                    pi.point.y = target;
                                    PedigreeUtils.PullTowardY(target, pi.point, model.parameters.verticalPositioningForceStrength, model);
                                }
                            }

                        }
                    }
                }
            };
        }
    }
}




//foreach (PedigreeCouple pc in pi.spouseCouples)
                            //{
                            //    if (model.pointsBeingDragged.Count == 0)
                            //    {
                            //        if (pc.mother == pi)
                            //        {
                            //            pc.father.point.y = pi.point.y;
                            //        }
                            //        else
                            //        {
                            //            pc.mother.point.y = pi.point.y;
                            //        }
                            //    }
                            //}
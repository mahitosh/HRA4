using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Responsible for pulling participants in couples toward their optimal positions.
    /// </summary>
    class LayoutCouples
    {
        public readonly LayoutStep step;
        public LayoutCouples(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                double hSpacing = model.parameters.horizontalSpacing;
                double halfHSpacing = hSpacing / 2;

                //for each couple, layout the mothers and fathers with no parents
                foreach (PedigreeCouple couple in model.couples)
                {
                    if (model.parameters.hideNonBloodRelatives && couple.mother.bloodRelative && !couple.father.bloodRelative)
                    {
                        couple.father.point.x = couple.mother.point.x;
                        couple.point.x = couple.mother.point.x;
                    }
                    else if (model.parameters.hideNonBloodRelatives && couple.father.bloodRelative && !couple.mother.bloodRelative)
                    {
                        couple.mother.point.x = couple.father.point.x;
                        couple.point.x = couple.father.point.x;
                    }
                    else
                    {

                        if (couple.mother != null && couple.father != null)
                        {
                            bool motherHasParents = couple.mother.Parents != null;
                            bool fatherHasParents = couple.father.Parents != null;

                            //if mother and father both have parents,
                            if (motherHasParents && fatherHasParents)
                            {
                                bool fatherToLeft = couple.father.Parents.point.x < couple.mother.Parents.point.x;
                                pullParents(model, couple, fatherToLeft);

                            }
                            //if the mother has parents but the father doesn't,
                            else if (motherHasParents && !fatherHasParents)
                            {
                                //then the mother will be pulled into position by
                                //the layout step for her sibship, so position the 
                                //father depending on where the mother is:

                                //pull the father to the left of the mother
                                double goalX = couple.mother.point.x - hSpacing;
                                PointWithVelocity point = couple.father.point;
                                double strength = model.parameters.layoutCouplesStrength;
                                PedigreeUtils.PullTowardX(goalX, point, strength, model);
                            }
                            //if the father has parents but the mother diesn't,
                            else if (!motherHasParents && fatherHasParents)
                            {
                                //then the father will be pulled into position by
                                //the layout step for his sibship, so position the 
                                //mother depending on where the father is:

                                //pull the mother to the right of the father
                                double goalX = couple.father.point.x + hSpacing;
                                PointWithVelocity point = couple.mother.point;
                                double strength = model.parameters.layoutCouplesStrength;
                                PedigreeUtils.PullTowardX(goalX, point, strength, model);

                            }
                            //if neither the father nor the mother has parents,
                            else if (!motherHasParents && !fatherHasParents)
                            {
                                //then position them according to their spouse position,
                                //placing the father on the left:
                                {
                                    //handle the case of half siblings:
                                    bool coupleIsReversed = false;
                                    if (couple.mother.spouseCouples.Count == 2)
                                    {
                                        PedigreeCouple oppositeCouple;
                                        if (couple.mother.spouseCouples[0].Equals(couple))
                                            oppositeCouple = couple.mother.spouseCouples[1];
                                        else
                                            oppositeCouple = couple.mother.spouseCouples[0];

                                        double oppositeX = oppositeCouple.point.x;
                                        double motherX = couple.point.x;
                                        bool oppositeCoupleIsLeftOfMother = oppositeX < motherX;

                                        coupleIsReversed = oppositeCoupleIsLeftOfMother;
                                    }

                                    bool fatherToLeft = !coupleIsReversed;
                                    pullParents(model, couple, fatherToLeft);

                                }
                            }
                        }
                    }
                }
            };
        }

        private static void pullParents(PedigreeModel model, PedigreeCouple couple, bool fatherToLeft)
        {
            double halfHSpacing = model.parameters.horizontalSpacing / 2;
            double reversal = fatherToLeft ? 1 : -1;
            //position the father to the left
            {
                double goalX = couple.point.x - reversal * halfHSpacing;
                PointWithVelocity point = couple.father.point;
                double strength = model.parameters.layoutCouplesStrength;
                PedigreeUtils.PullTowardX(goalX, point, strength, model);
            }

            //position the mother to the right
            {
                double goalX = couple.point.x + reversal * halfHSpacing;
                PointWithVelocity point = couple.mother.point;
                double strength = model.parameters.layoutCouplesStrength;
                PedigreeUtils.PullTowardX(goalX, point, strength, model);
            }
        }
    }
}







//PedigreeCouple otherCouple = null;
//foreach (PedigreeCouple otherparents in model.couples)
//{
//    if (couple != otherparents && (couple.mother == otherparents.mother || couple.father == otherparents.father))
//    {
//        if (otherparents.alreadyHandledHalfSibship == false)
//        {
//            otherCouple = otherparents;
//            couple.alreadyHandledHalfSibship = true;
//           // otherparents.alreadyHandledHalfSibship = true;
//        }
//    }
//}
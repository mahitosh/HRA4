using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    public class LayoutSipshipsTwo
    {
        /*************************************************************************************/
        public readonly LayoutStep step;

        /*************************************************************************************/
        public LayoutSipshipsTwo(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                //if (model.cycles > 150)
                //    return;
                //for each couple, layout their children
                foreach (PedigreeCouple parents in model.couples)
                {
                    parents.children.Sort(delegate(PedigreeIndividual a, PedigreeIndividual b)
                    {
                        double ax = EffectivePosition(a);
                        double bx = EffectivePosition(b);

                        return ax.CompareTo(bx);
                    });

                    double sibWidth = 0;
                    double leftEdge = 0;
                    double rightEdge = 0; ;

                    foreach (PedigreeIndividual child in parents.children)
                    {
                        leftEdge = GetLeftEdge(child, double.MaxValue, model);
                        rightEdge = GetRightEdge(child, double.MinValue, model);

                        sibWidth += (rightEdge - leftEdge);

                    }
                    double spacing = sibWidth / ((double)(parents.children.Count));

                    double i = 0;
                    foreach (PedigreeIndividual child in parents.children)
                    {
                        //position the individuals vertically based on the 
                        //positions of the parents:
                        double goalY = parents.point.y + model.parameters.verticalSpacing;
                        double strength = model.parameters.verticalPositioningForceStrength;
                        PedigreeUtils.PullTowardY(goalY, child.point, strength, model);

                        double goalX = leftEdge + (spacing * i);
                        PedigreeUtils.PullTowardX(goalX, child.point, strength, model);

                        i++;
                    }
                }
            };
        }

        /*************************************************************************************/
        private double GetLeftEdge(PedigreeIndividual pi, double current, PedigreeModel model)
        {
            double retval = current;

            if (pi.point.x < current)
                retval = pi.point.x;

            foreach (PedigreeCouple pc in model.couples)
            {
                if (pc.mother.HraPerson.relativeID == pi.HraPerson.relativeID || pc.father.HraPerson.relativeID == pi.HraPerson.relativeID)
                {
                    foreach (PedigreeIndividual kid in pc.children)
                    {
                        retval = GetLeftEdge(kid, retval, model);
                    }
                }
            }

            return retval;
        }

        /*************************************************************************************/
        private double GetRightEdge(PedigreeIndividual pi, double current, PedigreeModel model)
        {
            double retval = current;

            if (pi.point.x > current)
                retval = pi.point.x;

            foreach (PedigreeCouple pc in model.couples)
            {
                if (pc.mother.HraPerson.relativeID == pi.HraPerson.relativeID || pc.father.HraPerson.relativeID == pi.HraPerson.relativeID)
                {
                    foreach (PedigreeIndividual kid in pc.children)
                    {
                        retval = GetRightEdge(kid, retval, model);
                    }
                }
            }

            return retval;
        }

        /*************************************************************************************/
        /// <summary>
        /// Gets the effective position of an individual in a sibship, used
        /// for horizontal sorting. If an individual has a spouse, then the 
        /// x position of that couple is used as the "effective position"
        /// of the individual.
        /// </summary>
        private double EffectivePosition(PedigreeIndividual child)
        {

            foreach (PedigreeCouple pc in child.spouseCouples)
            {
                if (pc.mother != null && pc.father != null)
                {
                    PedigreeIndividual spouse;
                    if (child.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                        spouse = pc.father;
                    else
                        spouse = pc.mother;
                    //if the spouse's parents are to the right of 
                    //this child's parents,
                    if (spouse.Parents != null)
                    {
                        if (spouse.Parents.point.x > child.Parents.point.x)
                            //place the child at the rightmost position
                            //in the sibship
                            return 99999;
                        else
                            //otherwise to the leftmost
                            return -99999;
                    }
                }
            }

            return child.point.x;

            /*************************************************************************************/
        }
    }
}


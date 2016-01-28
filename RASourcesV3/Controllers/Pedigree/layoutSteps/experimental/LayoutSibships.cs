using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class LayoutSibships
    {
        public readonly LayoutStep step;
        public LayoutSibships(PedigreeModel model)
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
                    PedigreeCouple otherCouple = null;
                    foreach (PedigreeCouple otherparents in model.couples)
                    {
                        if (parents != otherparents && (parents.mother == otherparents.mother || parents.father == otherparents.father))
                        {
                            if (otherparents.alreadyHandledHalfSibship == false)
                            {
                                otherCouple = otherparents;
                                parents.alreadyHandledHalfSibship = true;
                                double oneleft = double.MaxValue;
                                double oneright = double.MinValue;
                                double twoleft = double.MaxValue;
                                double tworight = double.MinValue;
                                foreach (PedigreeIndividual one in parents.children)
                                {
                                    if (one.point.x < oneleft)
                                        oneleft = one.point.x;
                                    if (one.point.x > oneright)
                                        oneright = one.point.x;
                                }
                                foreach (PedigreeIndividual two in otherparents.children)
                                {
                                    if (two.point.x < twoleft)
                                        twoleft = two.point.x;
                                    if (two.point.x > tworight)
                                        tworight = two.point.x;
                                }
                                foreach (PedigreeIndividual two in otherparents.children)
                                {
                                    if (two.point.x < oneright && two.point.x > oneleft)
                                    {

                                        if (parents.point.x < otherparents.point.x)
                                            PedigreeUtils.PullTowardX(tworight, two.point, 5, model);
                                        else
                                            PedigreeUtils.PullTowardX(oneleft, two.point, 5, model);
                                    }
                                }
                            }
                        }
                    }
                    //Pull children toward their computed ideal locations as follows:
                    //Step 1 of 2: Sort sibship by x position
                    parents.children.Sort(delegate(PedigreeIndividual a, PedigreeIndividual b)
                    {
                        double ax = EffectivePosition(a);
                        double bx = EffectivePosition(b);

                            return ax.CompareTo(bx);
                    });

                    //Step 2 of 2: Pull sibship members toward their ideal positions,
                    //spaced ideally and centered around thir parent couple, leaving
                    //open spaces for spouses (as they will be positioned by the 
                    //layout step addressing couples).

                    double spacing;

                    //Compute the offset from the couple center for positioning individuals
                    double offset = 0;
                
                    int numIndividualsInSibshipSpan = 0;

                    //compute the center for the couple's sibship
                    double sibshipMinX = double.MaxValue;
                    double sibshipMaxX = -double.MaxValue;
                    for (int j = 0; j < parents.children.Count; j++)
                    {
                        PedigreeIndividual child = parents.children[j];

                        if (child.point.x < sibshipMinX)
                            sibshipMinX = (int)child.point.x;
                        if (child.point.x > sibshipMaxX)
                            sibshipMaxX = (int)child.point.x;

                        //double leftEdge = GetLeftEdge(child, double.MaxValue, model);
                        //double rightEdge = GetRightEdge(child, double.MinValue, model);

                        //if (leftEdge < sibshipMinX)
                        //    sibshipMinX = (int)leftEdge;
                        //if (rightEdge > sibshipMaxX)
                        //    sibshipMaxX = (int)rightEdge;


                        //count the child
                        numIndividualsInSibshipSpan++;

                        foreach (PedigreeCouple pc in child.spouseCouples)
                        {
                            //overlap += pc.childOverlap;
                            if (pc.mother != null && pc.father != null)
                            {
                                PedigreeIndividual spouse;
                                if (child.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                                    spouse = pc.father;
                                else
                                    spouse = pc.mother;

                                bool thisSpouseCounts = true;
                                bool spouseIsToRight = spouse.point.x > child.point.x;

                                //don't count the spouse to the right of
                                //the sibship's rightmost child
                                if (j == parents.children.Count - 1 && spouseIsToRight)
                                    thisSpouseCounts = false;

                                //don't count the spouse to the left of
                                //the sibship's leftmost child
                                if (j == 0 && !spouseIsToRight)
                                    thisSpouseCounts = false;

                                if (model.parameters.hideNonBloodRelatives && !spouse.bloodRelative)
                                    thisSpouseCounts = false;

                                if (thisSpouseCounts)
                                    numIndividualsInSibshipSpan++;
                            }
                        }
                    }

                    double sibshipSpanSize = (sibshipMaxX - sibshipMinX)*model.parameters.sibshipShrinkingFacor;
                    
                    if (numIndividualsInSibshipSpan == 1)
                        spacing = 0;
                    else
                        spacing = (sibshipSpanSize / (numIndividualsInSibshipSpan-1));

                    if (spacing < model.parameters.horizontalSpacing)
                    {
                        spacing = model.parameters.horizontalSpacing;
                        sibshipSpanSize = ((numIndividualsInSibshipSpan - 1) * spacing);
                    }

                    offset = parents.point.x - sibshipSpanSize / 2;

                    //position individuals ideally by applying a force to them
                    int i = 0;
                    foreach (PedigreeIndividual child in parents.children)
                    {
                        //position the individuals vertically based on the 
                        //positions of the parents:
                        {
                            double goalY = parents.point.y + model.parameters.verticalSpacing;
                            double strength = model.parameters.verticalPositioningForceStrength;
                            PedigreeUtils.PullTowardY(goalY, child.point, strength, model);
                        }

                        //position the individuals horizontally based on 
                        //computation of their ideal horizontal positions
                        {
                            double goalX = offset;
                            
                            //if the child has no spouse
                            if (child.spouseCouples.Count == 0)
                            {
                                //compute the position directly
                                //without incrementing i
                                goalX += i * spacing;

                            }
                            
                            //if the child has a spouse, insert appropriate spacing
                            foreach (PedigreeCouple pc in child.spouseCouples)
                            {
                                if (pc.mother != null && pc.father != null)
                                {
                                    PedigreeIndividual spouse;
                                    if (child.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                                        spouse = pc.father;
                                    else
                                        spouse = pc.mother;

                                    if (!model.parameters.hideNonBloodRelatives || spouse.bloodRelative)
                                    {
                                        bool spouseIsToRight = spouse.point.x > child.point.x;
                                        int index = parents.children.IndexOf(child);
                                        //if the spouse is to the right...
                                        if (spouseIsToRight)
                                            //...but not for the rightmost child
                                            if (index != parents.children.Count - 1)
                                                //leave a space to the right.
                                                goalX = offset + (i++) * spacing;
                                            else
                                                goalX = offset + i * spacing;
                                        else//if the spouse is to the left...
                                            //...but not for the leftmost child
                                            if (index != 0)
                                                //leave a space to the left.
                                                goalX = offset + (++i) * spacing;
                                    }
                                    else
                                    {
                                        goalX += i * (spacing);
                                    }
                                }
                            }

                            double strength = model.parameters.layoutSibshipsStrength;
                            PedigreeUtils.PullTowardX(goalX, child.point, strength,model);
                            //increment i to account for enumeration this child
                            i++;
                        }
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


        }
    }
}



//if (otherCouple != null)
//{

//   for (int j = 0; j < otherCouple.children.Count; j++)
//   {
//PedigreeIndividual child = otherCouple.children[j];

//if (child.point.x < sibshipMinX)
//    sibshipMinX = (int)child.point.x;
//if (child.point.x > sibshipMaxX)
//    sibshipMaxX = (int)child.point.x;

////count the child
//numIndividualsInSibshipSpan++;

//foreach (PedigreeCouple pc in child.spouseCouples)
//{
//    PedigreeIndividual spouse;
//    if (child.relativeID == pc.mother.relativeID)
//        spouse = pc.father;
//    else
//        spouse = pc.mother;

//    bool thisSpouseCounts = true;
//    bool spouseIsToRight = spouse.point.x > child.point.x;

//    //don't count the spouse to the right of
//    //the sibship's rightmost child
//    if (j == parents.children.Count - 1 && spouseIsToRight)
//        thisSpouseCounts = false;

//    //don't count the spouse to the left of
//    //the sibship's leftmost child
//    if (j == 0 && !spouseIsToRight)
//        thisSpouseCounts = false;

//    if (thisSpouseCounts)
//        numIndividualsInSibshipSpan++;
//}

//    }
//}



//if (GetLeftEdge(child, double.MaxValue, model) < sibshipMinX)
//    sibshipMinX = (int)child.point.x;
//if (GetRightEdge(child, double.MinValue, model) > sibshipMaxX)
//    sibshipMaxX = (int)child.point.x;



//int aCount = 0;
//int bCount = 0;
//foreach (PedigreeCouple pc in a.spouseCouples)
//{
//    foreach (PedigreeIndividual kid in pc.children)
//    {
//        aCount++;
//    }
//}
//foreach (PedigreeCouple pc in b.spouseCouples)
//{
//    foreach (PedigreeIndividual kid in pc.children)
//    {
//        bCount++;
//    }
//}

//if (bCount > aCount)
//    return 1;
//else if (aCount > bCount)
//    return -1;
//else



//if (parents.point.x < otherparents.point.x)
//    PedigreeUtils.PullTowardX(tworight, otherparents.point, 5, model);
//else
//    PedigreeUtils.PullTowardX(oneleft, otherparents.point, 5, model);






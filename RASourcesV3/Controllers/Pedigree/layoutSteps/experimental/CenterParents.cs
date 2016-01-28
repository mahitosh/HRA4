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
    class CenterParents
    {
        public readonly LayoutStep step;
        public CenterParents(PedigreeModel model)
        {
            step = delegate()
            {
                ////initialize the per-level ordering lists for each generation
                //for (int generationIndex = 0; generationIndex <= model.maxGenerationalLevel + 1; generationIndex++)
                //{

                //    List<PedigreeIndividual> generation = PedigreeUtils.GetIndividualsInGeneration(model, generationIndex);
                //    for (int i = 0; i < generation.Count; i++)
                //    {

                //    }
                //}
            };
                //if (model.couplesGraphIsPlanar)
                //{
                //    foreach (PedigreeCouple couple in model.couples)
                //    {
                      
                //        if (!model.pointsBeingDragged.Contains(couple.point))
                //        {
                //            //compute the center for the couple
                //            double motherX = couple.mother.point.x;
                //            double motherY = couple.mother.point.y + model.parameters.individualSize / 2;
                //            double fatherX = couple.father.point.x;
                //            double fatherY = couple.father.point.y + model.parameters.individualSize / 2;
                //            double coupleCenterX = (motherX + fatherX) / 2;

                            
                //            //compute the center for the couple's sibship
                //            double sibshipMinX = double.MaxValue;
                //            double sibshipMaxX = -double.MaxValue;
                //            foreach (PedigreeIndividual child in couple.children)
                //            {
                //                if (child.point.x < sibshipMinX)
                //                    sibshipMinX = (int)child.point.x;
                //                if (child.point.x > sibshipMaxX)
                //                    sibshipMaxX = (int)child.point.x;
                //            }

                //            double sibshipY = couple.point.y +
                //                model.parameters.verticalSpacing - model.parameters.individualSize / 2;
                //            double sibshipCenterX = (sibshipMinX + sibshipMaxX) / 2;

                //            //this is the middle point between parents and children from which
                //            //ideal positions of both are derived
                //            //double middleX = (sibshipCenterX + coupleCenterX) / 2;
                //            if (couple.mother.point.x < couple.father.point.x)
                //            {
                //                PedigreeUtils.PullTowardX(sibshipCenterX + (model.parameters.horizontalSpacing / 2), couple.mother.point, 2, model);
                //                PedigreeUtils.PullTowardX(sibshipCenterX - (model.parameters.horizontalSpacing / 2), couple.father.point, 2, model);
                //            }
                //            else
                //            {
                //                PedigreeUtils.PullTowardX(sibshipCenterX + (model.parameters.horizontalSpacing / 2), couple.mother.point, 2, model);
                //                PedigreeUtils.PullTowardX(sibshipCenterX - (model.parameters.horizontalSpacing / 2), couple.father.point, 2, model);
                //            }
                //             /**/
                //        }
                         

                //        //if (Math.Abs(couple.mother.point.x - couple.father.point.x) > 120)
                //        //{
                //        //    if (couple.mother.point.x < couple.father.point.x)
                //        //    {
                //        //        PedigreeUtils.PullTowardX(couple.father.point.x + 60, couple.mother.point, 0.25, model);
                //        //        PedigreeUtils.PullTowardX(couple.mother.point.x - 60, couple.father.point, 0.25, model);
                //        //    }
                //        //    else
                //        //    {
                //        //        PedigreeUtils.PullTowardX(couple.father.point.x - 60, couple.mother.point, 0.25, model);
                //        //        PedigreeUtils.PullTowardX(couple.mother.point.x + 60, couple.father.point, 0.25, model);
                //        //    }
                //        //}
                //    }
                //}
                /* */

                /*
                //initialize the per-level ordering lists for each generation
                for (int generationIndex = 0; generationIndex <= model.maxGenerationalLevel + 1; generationIndex++)
                {

                    List<PedigreeIndividual> generation = PedigreeUtils.GetIndividualsInGeneration(model, generationIndex);
                    for (int i = 0; i < generation.Count; i++)
                    {
                        for (int j = i; j < generation.Count; j++)
                        {
                            PedigreeIndividual one = generation[i];
                            PedigreeIndividual two = generation[j];
                            double delta = Math.Abs(one.point.x - two.point.x);
                            if ( delta < (model.parameters.horizontalSpacing))
                            {
                                if (one.Parents.point.x < two.Parents.point.x)
                                {
                                    PedigreeUtils.PullTowardX(one.point.x - (model.parameters.horizontalSpacing - delta), one.point, 1, model);
                                    PedigreeUtils.PullTowardX(two.point.x + (model.parameters.horizontalSpacing - delta), two.point, 1, model);
                                }
                                else
                                {
                                    PedigreeUtils.PullTowardX(one.point.x + (model.parameters.horizontalSpacing - delta), one.point, 1, model);
                                    PedigreeUtils.PullTowardX(two.point.x - (model.parameters.horizontalSpacing - delta), two.point, 1, model);
                                }
                            }

                        }
                    }
                }
                 
                if (model.couplesGraphIsPlanar)
                {
                    foreach (PedigreeCouple couple in model.couples)
                    {

                        if (!model.pointsBeingDragged.Contains(couple.point))
                        {
                            if (Math.Abs(couple.mother.point.x - couple.father.point.x) > (2 * model.parameters.horizontalSpacing))
                            {

                                //compute the center for the couple
                                double motherX = couple.mother.point.x;
                                double motherY = couple.mother.point.y + model.parameters.individualSize / 2;
                                double fatherX = couple.father.point.x;
                                double fatherY = couple.father.point.y + model.parameters.individualSize / 2;
                                double coupleCenterX = (motherX + fatherX) / 2;

                                if (couple.mother.point.x < couple.father.point.x)
                                {
                                    PedigreeUtils.PullTowardX(coupleCenterX + model.parameters.horizontalSpacing, couple.mother.point, 1, model);
                                    PedigreeUtils.PullTowardX(coupleCenterX - model.parameters.horizontalSpacing, couple.father.point, 1, model);
                                }
                                else
                                {
                                    PedigreeUtils.PullTowardX(coupleCenterX - model.parameters.horizontalSpacing, couple.mother.point, 1, model);
                                    PedigreeUtils.PullTowardX(coupleCenterX + model.parameters.horizontalSpacing, couple.father.point, 1, model);
                                }
                            }
                        }
                    }
                }
                 */
            }

        private bool LayoutIsValid(PedigreeModel model)
        {
            List<PedigreeIndividual> temp = new List<PedigreeIndividual>();
            for (int generation = 0; generation <= model.maxGenerationalLevel + 1; generation++)
            {
                //test for crossed edges
                foreach (PedigreeCouple coupleA in model.couples)
                    if (coupleA.GenerationalLevel == generation)
                        foreach (PedigreeIndividual childA in coupleA.children)
                            foreach (PedigreeCouple coupleB in model.couples)
                                if (coupleB.GenerationalLevel == generation)
                                    foreach (PedigreeIndividual childB in coupleB.children)
                                        if (coupleA != coupleB && childA != childB)
                                        {
                                            double x1 = coupleA.point.x;
                                            double y1 = coupleA.point.y;
                                            double x2 = childA.point.x;
                                            double y2 = childA.point.y;
                                            double x3 = coupleB.point.x;
                                            double y3 = coupleB.point.y;
                                            double x4 = childB.point.x;
                                            double y4 = childB.point.y;
                                            //does the edge from coupleA to childA
                                            //cross the edge from coupleB to childB?
                                            if (PedigreeUtils.LinesIntersect(x1, y1, x2, y2, x3, y3, x4, y4))
                                                //if yes then the layout is not valid
                                                return false;
                                        }
                //test for individuals inbetween fathers and mothers:
                //put all individuals in the current generation in a list
                PedigreeUtils.GetIndividualsInGeneration(model, generation, temp);


                for (int i = 0; i < temp.Count; i++)
                {
                    for (int j = 0; j < temp.Count; j++)
                    {
                        if (i != j)
                        {
                            if (Math.Abs(temp[i].point.x - temp[j].point.x) < (model.parameters.horizontalSpacing/2))
                            {
                                return false;
                            }
                        }
                    }
                }
                //for each person who has a spouse, make sure the spouse is next to that person,
                //otherwise report an invalid layout
                for (int i = 0; i < temp.Count; i++)
                {
                    PedigreeIndividual individual = temp[i];
                    foreach (PedigreeCouple pc in individual.spouseCouples)
                    {

                        PedigreeIndividual spouse;
                        if (individual.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                            spouse = pc.father;
                        else
                            spouse = pc.mother;

                        bool coupleIsSplit = true;
                        if (i > 0)
                            if (temp[i - 1].HraPerson.relativeID == spouse.HraPerson.relativeID)
                                coupleIsSplit = false;
                        if (i < temp.Count - 1)
                            if (temp[i + 1].HraPerson.relativeID == spouse.HraPerson.relativeID)
                                coupleIsSplit = false;
                        if (coupleIsSplit)
                            return false;

                    }
                    //if (individual.spouseCouples.Count == 1)
                    //{
                    //    PedigreeIndividual spouse = individual.Spouse;
                    //    bool coupleIsSplit = true;
                    //    if (i > 0)
                    //        if (temp[i - 1].relativeID == spouse.relativeID)
                    //            coupleIsSplit = false;
                    //    if (i < temp.Count - 1)
                    //        if (temp[i + 1].relativeID == spouse.relativeID)
                    //            coupleIsSplit = false;
                    //    if (coupleIsSplit)
                    //        return false;
                    //}
                }
            }
            return true;
        }

        private PedigreeIndividual GetLeftmostIndividualOfSet(PedigreeIndividualSet individualSet)
        {
            PedigreeIndividual leftmostIndividual = individualSet[0];
            double minX = leftmostIndividual.point.x;
            foreach (PedigreeIndividual individual in individualSet)
            {
                if (individual.point.x < minX)
                {
                    minX = individual.point.x;
                    leftmostIndividual = individual;
                }
            }
            return leftmostIndividual;
        }

        private void ApplyForceToParents(PedigreeIndividualSet iSet, double force)
        {

            foreach (PedigreeIndividual individual in iSet)
            {
                if (individual.Parents != null)
                {
                    PedigreeCouple parents = individual.Parents;
                    //only apply the force to the parent which has a parent
                    if (parents.mother.Parents != null)
                        parents.mother.point.dx += force;
                    if (parents.father.Parents != null)
                        parents.father.point.dx += force;
                }
            }
        }
        private void AddRelativeToMoveList(PedigreeIndividual rightIndividual, List<PedigreeIndividual> toMove)
        {
            if (toMove.Contains(rightIndividual) == false)
                toMove.Add(rightIndividual);

            //if (rightIndividual.motherID > 0)
            //    AddRelativeToMoveList(rightIndividual.Mother, toMove);

            //if (rightIndividual.fatherID > 0)
            //    AddRelativeToMoveList(rightIndividual.Father, toMove);

            //foreach (PedigreeCouple pc in rightIndividual.spouseCouples)
            //{
            //    foreach (PedigreeIndividual pi in pc.children)
            //    {
            //        if (pi != rightIndividual)
            //            if (pi.point.x >= rightIndividual.point.x)
            //                AddRelativeToMoveList(pi, toMove);

            //    }
            //}




            //rightIndividual.point.x += (h - interSetDistance);
            //if (rightIndividual.motherID > 0)

            //if (rightIndividual.fatherID > 0)
            //    if (toMove.Contains(rightIndividual.Father) == false)
            //        toMove.Add(rightIndividual.Father);
        }
        private PedigreeIndividual GetRightmostIndividualOfSet(PedigreeIndividualSet individualSet)
        {
            PedigreeIndividual rightmostIndividual = individualSet[0];
            double maxX = rightmostIndividual.point.x;
            foreach (PedigreeIndividual individual in individualSet)
            {
                if (individual.point.x > maxX)
                {
                    maxX = individual.point.x;
                    rightmostIndividual = individual;
                }
            }
            return rightmostIndividual;
        ;
        }
    }
}

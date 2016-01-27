using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class RepelIndividualSets
    {
        public readonly LayoutStep step;
        public RepelIndividualSets(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                //foreach (PedigreeCouple pc in model.couples)
                //{
                //    pc.childOverlap = 0;
                //}

                int setId = 0;
                //for each generation,
                for (int generationalLevel = 0;
                    generationalLevel < model.individualSets.Count;
                    generationalLevel++)
                {
                    //sort the individual sets of the current generation
                    if (model.individualSets.ContainsKey(generationalLevel))
                    {
                        List<PedigreeIndividualSet> levelIndividualSets = model.individualSets[generationalLevel];
                        levelIndividualSets.Sort(delegate(PedigreeIndividualSet a, PedigreeIndividualSet b)
                        {
                            double ax = a.associatedCouple.point.x;
                            double bx = b.associatedCouple.point.x;
                            return ax.CompareTo(bx);
                        });
                            
                        //then repel adjacent individual sets if necessary
                        for (int i = 0; i < levelIndividualSets.Count - 1; i++)
                        {
                            setId++;

                            PedigreeIndividualSet leftSet = levelIndividualSets[i];
                            PedigreeIndividualSet rightSet = levelIndividualSets[i + 1];

                            PedigreeIndividual rightmostOfLeftSet = GetRightmostIndividualOfSet(leftSet, rightSet);
                            PedigreeIndividual leftmostOfRightSet = GetLeftmostIndividualOfSet(rightSet, leftSet);
                            PedigreeIndividual rightmostOfRightSet = GetRightmostIndividualOfSet(rightSet, leftSet);
                            PedigreeIndividual leftmostOfLefttSet = GetLeftmostIndividualOfSet(leftSet, rightSet);

                            if (rightmostOfLeftSet != null && leftmostOfRightSet != null && rightmostOfRightSet != null && leftmostOfLefttSet != null)
                            {
                                //double interSetDistance = Math.Abs(leftmostOfRightSet.point.x - rightmostOfLeftSet.point.x);
                                double interSetDistance = leftmostOfRightSet.point.x - rightmostOfLeftSet.point.x;

                                double h = model.parameters.horizontalSpacing;

                                //if (leftmostOfRightSet.point.x < leftmostOfLefttSet.point.x && rightmostOfRightSet.point.x > rightmostOfLeftSet.point.x)
                                //{
                                //    interSetDistance = 0;
                                //}
                                
                                double x = -1 * (interSetDistance - h);
                                //double x =  (interSetDistance - h);
                                //double force = 1 / (1 + Math.Pow(2, x));

                                double force = Math.Pow(2, x);
                                if (force > 5)
                                    force = 5;

                                force *= model.parameters.repelIndividualSetsStrength;


                                foreach (PedigreeIndividual leftIndividual in leftSet)
                                    leftIndividual.point.dx -= force;
                                foreach (PedigreeIndividual rightIndividual in rightSet)
                                    rightIndividual.point.dx += force;

                                ApplyForceToParents(leftSet, -force, x);
                                ApplyForceToParents(rightSet, force, x);

                            }
                        }
                    }        
                }
            };

        }

        private void ApplyForceToParents(PedigreeIndividualSet iSet, double force, double overlap)
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

        private PedigreeIndividual GetLeftmostIndividualOfSet(PedigreeIndividualSet individualSet, PedigreeIndividualSet exclusionSet)
        {
            PedigreeIndividual retval = null;

            foreach (PedigreeIndividual individual in individualSet)
            {
                if (exclusionSet.Contains(individual) == false)
                {
                    if (retval == null)
                    {
                        retval = individual;
                    }
                    else
                    {
                        if (individual.point.x < retval.point.x)
                        {
                            retval = individual;
                        }
                    }
                }
            }

            return retval;



            //PedigreeIndividual leftmostIndividual = individualSet[0];
            //double minX = leftmostIndividual.point.x;
            //foreach (PedigreeIndividual individual in individualSet)
            //{
            //    if (individual.point.x < minX)
            //    {
            //        minX = individual.point.x;
            //        leftmostIndividual = individual;
            //    }
            //    foreach (PedigreeCouple pc in individual.spouseCouples)
            //    {
            //        if (pc.mother != null && pc.father != null)
            //        {
            //            PedigreeIndividual spouse;
            //            if (individual.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
            //                spouse = pc.father;
            //            else
            //                spouse = pc.mother;

            //            if (exclusionSet.Contains(spouse) == false)
            //            {
            //                if (spouse.point.x < minX)
            //                {
            //                    minX = spouse.point.x;
            //                    leftmostIndividual = spouse;
            //                }
            //            }
            //        }
            //    }
            //}
            //return leftmostIndividual;
        }

        private PedigreeIndividual GetRightmostIndividualOfSet(PedigreeIndividualSet individualSet, PedigreeIndividualSet exclusionSet)
        {

            PedigreeIndividual retval = null;

            foreach (PedigreeIndividual individual in individualSet)
            {
                if (exclusionSet.Contains(individual) == false)
                {
                    if (retval == null)
                    {
                        retval = individual;
                    }
                    else
                    {
                        if (individual.point.x > retval.point.x)
                        {
                            retval = individual;
                        }
                    }
                }
            }

            return retval;


            //PedigreeIndividual rightmostIndividual = individualSet[0];
            //double maxX = rightmostIndividual.point.x;
            //foreach (PedigreeIndividual individual in individualSet)
            //{
            //    if (individual.point.x > maxX)
            //    {
            //        maxX = individual.point.x;
            //        rightmostIndividual = individual;
            //    }
            //    foreach (PedigreeCouple pc in individual.spouseCouples)
            //    {
            //        if (pc.mother != null && pc.father != null)
            //        {
            //            PedigreeIndividual spouse;
            //            if (individual.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
            //                spouse = pc.father;
            //            else
            //                spouse = pc.mother;

            //            if (exclusionSet.Contains(spouse) == false)
            //            {
            //                if (spouse.point.x > maxX)
            //                {
            //                    maxX = spouse.point.x;
            //                    rightmostIndividual = spouse;
            //                }
            //            }
            //        }
            //    }
            //}
            //return rightmostIndividual;
        }
    }
}




//if (model.parameters.drawVisualDebugging)
//{
//    foreach (PedigreeIndividual pi in levelIndividualSets[i])
//    {
//        //Console.Out.Write("set " + setId.ToString() + ": " + pi.relativeID);
//    }
//    //Console.Out.WriteLine("");
//    foreach (PedigreeIndividual pi in levelIndividualSets[i + 1])
//    {
//        //Console.Out.Write("set " + setId.ToString() + ": " + pi.relativeID);
//    }
//    //Console.Out.WriteLine("");
//}


//foreach (PedigreeIndividual pi in leftSet)
//{
//    if (rightSet.Contains(pi))
//    {
//        break;
//    }
//}
//string LeftMembers = "";
//foreach (PedigreeIndividual pi in leftSet)
//{
//    LeftMembers += ", " + pi.relativeID;
//}
//string RightMembers = "";
//foreach (PedigreeIndividual pi in rightSet)
//{
//    RightMembers += ", " + pi.relativeID;
//}





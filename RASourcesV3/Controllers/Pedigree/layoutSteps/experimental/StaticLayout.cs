using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class StaticLayout
    {
        public readonly LayoutStep transitionToStaticLayout;
        public readonly LayoutStep step;
        private bool runOnce = true;
        List<List<PedigreeIndividual>> generations;
        int failedAttempts = 0;
        //Random autoRand = new Random();
        //a flag so the static layout computes only once after the 
        //transition rather than every frame
        bool readyToRunStaticLayout = false;
        

        public StaticLayout(PedigreeModel model, PedigreeController layoutEngine)
        {
            
            transitionToStaticLayout = delegate()
            {
                if (model.cycles > 1000 || (model.cycles>800 && model.couplesGraphIsPlanar && model.layoutIsValid==false))// && model.forcesHaveConverged)
                {
                    //switch modes from self organizing to static
                    if (layoutEngine.GetMode() == PedigreeController.SELF_ORGANIZING)
                    {
                        model.TargetPositions.Clear();
                        foreach (PedigreeIndividual pi in model.individuals)
                        {
                            PointWithVelocity pos = new PointWithVelocity();
                            pos.x = pi.point.x;
                            pos.y = pi.point.y;
                            model.TargetPositions.Add(pi.HraPerson.relativeID, pos);
                        }

                        layoutEngine.SetMode(PedigreeController.STATIC_LAYOUT);
                        generations = new List<List<PedigreeIndividual>>();
                        //initialize the per-level ordering lists for each generation
                        for (int generation = 0; generation <= model.maxGenerationalLevel+1; generation++)
                            generations.Add(PedigreeUtils.GetIndividualsInGeneration(model, generation));

                        readyToRunStaticLayout = true;

                    }
                }
            };
            step = delegate()
            {
                if (runOnce && readyToRunStaticLayout)
                {
                    failedAttempts++;

                    readyToRunStaticLayout = false;
                    double spacing = model.parameters.verticalSpacing;
                    double centerY = (model.displayYMin + model.displayYMax) / 2;
                    double offset = centerY - (model.maxGenerationalLevel + 1) * spacing / 2;

                    double initialX = 30;
                    double initialY = centerY;
                    //for each generation, bottom to top...
                    for (int g = generations.Count - 1; g >= 0; g--)
                    //for(int i = 0;i<generations.Count;i++)
                    {
                        List<PedigreeIndividual> generation = generations[g];

                        //compute the y positions
                        foreach (PedigreeIndividual individual in generation)
                            individual.point.y = g * spacing + offset;

                        //for the bottom level, just position with even spaces
                        if (g == generations.Count - 1)
                        {
                            double x = initialX;
                            foreach (PedigreeIndividual individual in generation)
                            {
                                individual.point.x = x += model.parameters.horizontalSpacing;
                                individual.wasMoved = true;
                            }
                        }
                        //for all the other levels...
                        else
                        {
                            //First compute locations of parents centered around their children
                            //and place the parent couples exactly at those centers:

                            //for each individual in the current generation...
                            for (int j = 0; j < generation.Count; j++)
                            {
                                PedigreeIndividual individual = generation[j];

                                //if the current individual has a spouse...
                                foreach (PedigreeCouple pc in individual.spouseCouples)
                                {
                                    //so we compute the center of this couple's sibship,
                                    double sibshipCenterX = pc.GetSibshipCenterX(model);

                                    //and position the couple such that its center is the same as the sibship center

                                    if (pc.father.point.x < pc.mother.point.x)
                                    {
                                        pc.father.point.x = sibshipCenterX - model.parameters.horizontalSpacing / 2;
                                        pc.mother.point.x = sibshipCenterX + model.parameters.horizontalSpacing / 2;
                                    }
                                    else
                                    {

                                        pc.mother.point.x = sibshipCenterX - model.parameters.horizontalSpacing / 2;
                                        pc.father.point.x = sibshipCenterX + model.parameters.horizontalSpacing / 2;
                                    }
                                    pc.mother.wasMoved = pc.father.wasMoved = true;

                                }
                            }
                            /**/

                            //Second, position the outer children:
                            //find the leftmost individual that was placed
                            for (int inc = 1; inc >= -1; inc -= 2)
                            {
                                for (int j = inc == 1 ? 0 : generation.Count - 1;
                                             inc == 1 ? j < generation.Count : j >= 0;
                                             j += inc)
                                {
                                    if (generation[j].wasMoved)
                                    {

                                        for (int i = j - inc;
                                             inc == 1 ? i >= 0 : i < generation.Count; i -= inc)
                                        {
                                            generation[i].point.x = generation[i + inc].point.x - inc * model.parameters.horizontalSpacing;
                                            generation[i].wasMoved = true;
                                        }
                                        break;
                                    }
                                }
                            }

                            
                            
                            //Third, detect "too close"ness and move individuals to be perfectly spaced,
                            //propagating left to right and downward through children
                            for (int j = 0; j < generation.Count - 1; j++)
                            {
                                PedigreeIndividual left = generation[j];
                                PedigreeIndividual right = generation[j + 1];
                                //if folks are too close...
                                if (right.point.x - left.point.x < model.parameters.horizontalSpacing)
                                {
                                    //move the individual on the right such that spacing is perfect,
                                    //based on the current position of the individual on the left
                                    double dx = left.point.x + model.parameters.horizontalSpacing - right.point.x;
                                    right.point.x += dx;
                                    right.wasMoved = true;

                                    for (int z = j + 2; z < generation.Count - 1; z++)
                                    {
                                        PedigreeIndividual farRight = generation[z];
                                        farRight.point.x += dx;
                                        farRight.wasMoved = true;

                                    }
                                    //and through their children
                                    foreach (PedigreeCouple pc in left.spouseCouples)
                                    {

                                        PedigreeIndividual spouse;
                                        if (left.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                                            spouse = pc.father;
                                        else
                                            spouse = pc.mother;

                                        if (right.HraPerson.relativeID == spouse.HraPerson.relativeID)
                                        {
                                            //PedigreeCouple couple = GetCoupleFromLR(model, left, right);
                                            //if (couple != null)
                                                MoveChildren(dx, pc, model);
                                        }
                                    }
    
                                }
                            }

                        }
                    }


                    for (int g = 0; g < generations.Count - 1; g++)
                    {
                        List<PedigreeIndividual> generation = generations[g];
                        //for (int j = 0; j <  1; j++)
                        for (int j = generation.Count - 1; j >= 0; j--)
                        {
                            PedigreeIndividual left = generation[j];
                            if (left.HasSpouse())
                            {
                                //PedigreeCouple couple = GetCoupleFromLR(model, left, right);
                                foreach (PedigreeCouple pc in model.couples)
                                {
                                    double parentsX = (pc.mother.point.x + pc.father.point.x) / 2;
                                    if (pc.mother == left || pc.father == left)
                                    {
                                        double kidsX = 0;
                                        foreach (PedigreeIndividual kid in pc.children)
                                        {
                                            kidsX += kid.point.x;
                                        }
                                        kidsX = kidsX / ((double)(pc.children.Count));
                                        foreach (PedigreeIndividual kid in pc.children)
                                        {
                                            kid.point.x += (parentsX - kidsX);

                                            foreach (PedigreeCouple kidsPC in kid.spouseCouples)
                                            {

                                                PedigreeIndividual spouse;
                                                if (left.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                                                    spouse = pc.father;
                                                else
                                                    spouse = pc.mother;

                                                spouse.point.x += (parentsX - kidsX);
                                                spouse.wasMoved = true;

                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                

                    //after positioning everyone, center the pedigree on the screen:
                    double minX = double.MaxValue;
                    double maxX = -minX;

                    foreach (PointWithVelocity point in model.points)
                        if (point.x < minX)
                            minX = point.x;
                        else if (point.x > maxX)
                            maxX = point.x;

                    double centerX = (minX + maxX) / 2;
                    double idealCenterX = (model.displayXMin + model.displayYMax) / 2;
                    double off = idealCenterX - centerX;
                    foreach (PointWithVelocity point in model.points)
                        point.x = point.x + off;
                }

                //if (LayoutIsValid(model))
                if (true)
                {
                    //model.parameters.repelIndividualSetsStrength = 1;

                    layoutEngine.SetMode(PedigreeController.MANUAL);
                }
                //else
                //{
                    
                    //layoutEngine.SetMode(PedigreeController.SELF_ORGANIZING);

                    //if (failedAttempts < 5)
                    //{

                        //model.parameters.repelIndividualSetsStrength += 0.5;
                    //}
                    //else if (failedAttempts < 10)
                    //{
                    //    model.parameters.repelIndividualSetsStrength = 1;
                    //    foreach (PointWithVelocity p in model.points)
                    //    {
                    //        double newX = autoRand.NextDouble() * (model.displayXMax - 1);
                    //        double newY = autoRand.NextDouble() * (model.displayYMax - 1);
                    //        p.x = newX;
                    //        p.dx = 0;
                    //        p.y = newY;
                    //        p.dy = 0;

                    //    }
                    //}
                    //else
                    //{
                    //    layoutEngine.SetMode(PedigreeController.MANUAL);
                    //}
                     /**/
                //}
                
            };
        }



        private static void MoveChildren(double dx, PedigreeCouple couple, PedigreeModel model)
        {
            foreach (PedigreeIndividual child in couple.children)
            {
                child.point.x += dx;
                child.wasMoved = true;
                foreach (PedigreeCouple pc in child.spouseCouples)
                {

                    PedigreeIndividual spouse;
                    if (child.HraPerson.relativeID == pc.mother.HraPerson.relativeID)
                        spouse = pc.father;
                    else
                        spouse = pc.mother;

                    if (spouse.Parents != null)
                    {
                        PedigreeCouple nestedCouple = GetCoupleFromLR(model, child, spouse);
                        if (nestedCouple != null)
                            MoveChildren(dx / 2, nestedCouple, model);
                    }
                    else
                    {
                        spouse.point.x += dx;
                        spouse.wasMoved = true;
                        PedigreeCouple nestedCouple = GetCoupleFromLR(model, child, spouse);
                        if (nestedCouple != null)
                            MoveChildren(dx / 2, nestedCouple, model);

                    }
                    
                }
            }
        }

        private static PedigreeCouple GetCoupleFromLR(PedigreeModel model, PedigreeIndividual left, PedigreeIndividual right)
        {
            CoupleID coupleId = left.HraPerson.gender == PedigreeIndividual.GENDER_MALE ?
                 new CoupleID(left.HraPerson.relativeID, right.HraPerson.relativeID) :
                 new CoupleID(right.HraPerson.relativeID, left.HraPerson.relativeID);
            if (model.couplesDictionary.ContainsKey(coupleId))
                return model.couplesDictionary[coupleId];
            else
                return null;
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
                            if (Math.Abs(temp[i].point.x - temp[j].point.x) < (model.parameters.horizontalSpacing / 2))
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

                }
            }
            return true;
        }

    }
}





//private void MoveParents(PedigreeIndividualSet iSet, PedigreeModel model)
//{
//    double leftEdge = GetLeftmostIndividualOfSet(iSet).point.x;
//    double rightEdge = GetRightmostIndividualOfSet(iSet).point.x;

//    double center = (leftEdge + rightEdge) / 2;
//    foreach (PedigreeIndividual individual in iSet)
//    {
//        foreach (PedigreeCouple pc in model.couples)
//        {
//            if (pc.children.Contains(individual))
//            {
//                if (pc.mother.point.x < pc.father.point.x)
//                {

//                }
//                else
//                {

//                }
//            }
//        }
//    }
//}
//private void MoveBloodline(PedigreeIndividual other, double p, PedigreeModel model)
//{
//    other.point.x += p;
//    foreach (PedigreeCouple pc in other.spouseCouples)
//    {
//        PedigreeIndividual spouse;
//        if (pc.mother.relativeID == other.relativeID)
//            spouse = pc.father;
//        else
//            spouse = pc.mother;

//        spouse.point.x += p;

//        foreach (PedigreeIndividual kid in pc.children)
//        {
//            MoveBloodline(kid, p, model);
//        }
//    }
//}

//private PedigreeIndividual GetLeftmostIndividualOfSet(PedigreeIndividualSet individualSet)
//{
//    PedigreeIndividual leftmostIndividual = individualSet[0];
//    double minX = leftmostIndividual.point.x;
//    foreach (PedigreeIndividual individual in individualSet)
//    {
//        if (individual.point.x < minX)
//        {
//            minX = individual.point.x;
//            leftmostIndividual = individual;
//        }
//    }
//    return leftmostIndividual;
//}

//private PedigreeIndividual GetRightmostIndividualOfSet(PedigreeIndividualSet individualSet)
//{
//    PedigreeIndividual rightmostIndividual = individualSet[0];
//    double maxX = rightmostIndividual.point.x;
//    foreach (PedigreeIndividual individual in individualSet)
//    {
//        if (individual.point.x > maxX)
//        {
//            maxX = individual.point.x;
//            rightmostIndividual = individual;
//        }
//    }
//    return rightmostIndividual;
//}
//private double DetermineRightEdge(PedigreeIndividual pi, PedigreeModel model)
//{

//    double retval = pi.point.x;

//    foreach (PedigreeCouple pc in pi.spouseCouples)
//    {

//        foreach (PedigreeIndividual kid in pc.children)
//        {
//            double kidsRightEdge = DetermineRightEdge(kid, model);
//            if (kidsRightEdge > retval)
//                retval = kidsRightEdge;
//        }
//    }

//    return retval;
//}

//private double DetermineLeftEdge(PedigreeIndividual pi, PedigreeModel model)
//{
//    double retval = pi.point.x;

//    foreach (PedigreeCouple pc in pi.spouseCouples)
//    {

//        foreach (PedigreeIndividual kid in pc.children)
//        {
//            double kidsLeftEdge = DetermineLeftEdge(kid, model);
//            if (kidsLeftEdge < retval)
//                retval = kidsLeftEdge;
//        }
//    }

//    return retval;
//}
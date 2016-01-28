using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class DetectValidLayout
    {
        List<PedigreeIndividual> temp = new List<PedigreeIndividual>();

        public readonly LayoutStep step;
        public DetectValidLayout(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                model.layoutIsValid = LayoutIsValid(model);
            };
        }

        private bool LayoutIsValid(PedigreeModel model)
        {
            for (int generation = 0; generation < model.maxGenerationalLevel + 1; generation++)
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
                                            {

                                                //if yes then the layout is not valid
                                                return false;
                                            }
                                        }
                //test for individuals inbetween fathers and mothers:
                //put all individuals in the current generation in a list
                PedigreeUtils.GetIndividualsInGeneration(model, generation , temp);

           


                if (model.couplesGraphIsPlanar)
                {
                    for (int i = 0; i < temp.Count; i++)
                    {
                        for (int j = 0; j < temp.Count; j++)
                        {
                            if (i != j)
                            {
                                if (Math.Abs(temp[i].point.x - temp[j].point.x) < (model.parameters.horizontalSpacing / 2))
                                {
                                    //if (model.couplesGraphIsPlanar && model.layoutIsValid == false)
                                    //{
                                        //if (model.parameters.repelIndividualSetsStrength < 10)
                                        //{
                                        //    model.parameters.repelIndividualSetsStrength += 0.5;
                                        //}
                                        //if (model.parameters.couplesAttractionStrength > 0.1)
                                        //{
                                        //    model.parameters.couplesAttractionStrength -= 0.1;
                                        //}
                                        //if (model.parameters.layoutCouplesStrength > 0.01)
                                        //{
                                        //    model.parameters.layoutCouplesStrength -= 0.01;
                                        //}
                                        //if (model.parameters.centeringForceStrength > 0.01)
                                        //{
                                        //    model.parameters.centeringForceStrength -= 0.01;
                                        //}
                                        //if (model.parameters.sibshipShrinkingFacor > 0.01)
                                        //{
                                        //    model.parameters.sibshipShrinkingFacor -= 0.01;
                                        //}
                                    //}
                                    return false;
                                }
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
                        if (!model.parameters.hideNonBloodRelatives || (pc.mother.bloodRelative && pc.father.bloodRelative))
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

        
    }
}

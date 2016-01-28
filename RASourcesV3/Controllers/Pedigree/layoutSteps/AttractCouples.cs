using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class AttractCouples
    {
        public readonly LayoutStep step;
        public AttractCouples(PedigreeModel model)
        {
            step = delegate()
            {
                foreach (PedigreeCouple pc in model.couples)
                {
                    if ((pc.mother != null) && (pc.father != null))
                    {
                        if (pc.mother.Mother != null &&
                            pc.mother.Father != null &&
                            pc.father.Mother != null &&
                            pc.father.Father != null)
                        {
                            double motherAvgX = (pc.mother.Mother.point.x + pc.mother.Father.point.x) / 2;
                            double fatherAvgX = (pc.father.Mother.point.x + pc.father.Father.point.x) / 2;

                            if (pc.mother.point.x > pc.father.point.x && motherAvgX < fatherAvgX)
                            {
                                double temp = pc.mother.point.x;
                                pc.mother.point.x = pc.father.point.x;
                                pc.father.point.x = temp;
                            }
                            else if (pc.father.point.x > pc.mother.point.x && fatherAvgX < motherAvgX)
                            {
                                double temp = pc.mother.point.x;
                                pc.mother.point.x = pc.father.point.x;
                                pc.father.point.x = temp;
                            }
                        }

                        if (pc.children.Count > 0)
                        {
                            double xAcc = 0;
                            double yAcc = 0;
                            double sibMin = double.MaxValue;
                            double sibMax = double.MinValue;
                            foreach (PedigreeIndividual kid in pc.children)
                            {
                                xAcc += kid.point.x;
                                yAcc += kid.point.y;
                                if (sibMin > kid.point.x)
                                    sibMin = kid.point.x;
                                if (sibMax < kid.point.x)
                                    sibMax = kid.point.x;
                            }
                            xAcc = (sibMin + sibMax) / 2;
                            yAcc /= (double)pc.children.Count;

                            double distance = Math.Abs(pc.mother.point.x - pc.father.point.x);
                            double coupleAvg = (pc.mother.point.x + pc.father.point.x) / (2.0);

                            if (!model.parameters.hideNonBloodRelatives || (pc.mother.bloodRelative && pc.father.bloodRelative))
                            {
                                if (pc.mother.point.x < pc.father.point.x)
                                {
                                    PedigreeUtils.PullTowardX(xAcc - (distance / 2), pc.mother.point, 0.1, model);
                                    PedigreeUtils.PullTowardX(xAcc + (distance / 2), pc.father.point, 0.1, model);
                                }
                                else
                                {
                                    PedigreeUtils.PullTowardX(xAcc + (distance / 2), pc.mother.point, 0.1, model);
                                    PedigreeUtils.PullTowardX(xAcc - (distance / 2), pc.father.point, 0.1, model);
                                }


                                List<PedigreeIndividual> kidsPulledToParents = new List<PedigreeIndividual>();
                                if (Math.Abs(pc.mother.point.x - pc.father.point.x) < (0.90 * model.parameters.horizontalSpacing))
                                {
                                    SeperateCouple(pc, model, xAcc, ref kidsPulledToParents);
                                }
                            }
                            else
                            {
                                if (model.parameters.hideNonBloodRelatives)
                                {
                                    if (pc.mother.bloodRelative && !pc.father.bloodRelative)
                                    {
                                        PedigreeUtils.PullTowardX(pc.mother.point.x, pc.father.point, 0.1, model);
                                    }
                                    else if (!pc.mother.bloodRelative && pc.father.bloodRelative)
                                    {
                                        PedigreeUtils.PullTowardX(pc.father.point.x, pc.mother.point, 0.1, model);
                                    }
                                }

                                pc.mother.point.x = coupleAvg;
                                pc.father.point.x = coupleAvg;
                            }
                        }
                    }
                }
            };
        }

        private void SeperateCouple(PedigreeCouple pc, PedigreeModel model, double sibMedian, ref List<PedigreeIndividual> kidsPulledToParents)
        {
            if (!model.parameters.hideNonBloodRelatives || (pc.mother.bloodRelative && pc.father.bloodRelative))
            {
                if (pc.mother.point.x < pc.father.point.x)
                {
                    PedigreeUtils.PullTowardX(pc.mother.point.x - 1, pc.mother.point, 0.1, model);
                    PedigreeUtils.PullTowardX(pc.father.point.x + 1, pc.father.point, 0.1, model);
                }
                else
                {
                    PedigreeUtils.PullTowardX(pc.mother.point.x + 1, pc.mother.point, 0.1, model);
                    PedigreeUtils.PullTowardX(pc.father.point.x - 1, pc.father.point, 0.1, model);
                }
            }
            else
            {
                double coupleAvg = (pc.mother.point.x + pc.father.point.x) / (2.0);
                pc.mother.point.x = coupleAvg;
                pc.father.point.x = coupleAvg;
            }

            double target = ((pc.mother.point.x + pc.father.point.x)/2) - sibMedian;
            foreach(PedigreeIndividual pi in pc.children)
            {
                if (kidsPulledToParents.Contains(pi) == false)
                {
                    PedigreeUtils.PullTowardX((pc.mother.point.x + pc.father.point.x) / 2, pi.point, 0.075, model);
                    kidsPulledToParents.Add(pi);
                }
                //PedigreeUtils.PullTowardX(pi.point.x + target, pi.point, 0.1, model);
            }

            foreach (PedigreeCouple nested_pc in model.couples)
            {
                if (nested_pc != pc)
                {
                    if (pc.children.Contains(nested_pc.mother) || pc.children.Contains(nested_pc.father))
                    {
                        SeperateCouple(nested_pc, model, sibMedian, ref kidsPulledToParents);
                    }
                }
            }
        }
    }
}








//foreach (PedigreeIndividual neighbor in model.individuals)
//{
//    if (neighbor != pc.mother && neighbor != pc.father)
//    {
//        if (Math.Abs(neighbor.point.y - ((pc.mother.point.y + pc.father.point.y) / 2)) < (model.parameters.verticalSpacing / 2))
//        {
//            if (neighbor.point.x > Math.Min(pc.mother.point.x, pc.father.point.x) &&
//                neighbor.point.x < Math.Max(pc.mother.point.x, pc.father.point.x))
//            {
//                if (pc.mother.point.x > pc.father.point.x)
//                {
//                    PedigreeUtils.PullTowardX(((pc.mother.point.y + pc.father.point.y) / 2) + 10, pc.mother.point, 1, model);
//                    PedigreeUtils.PullTowardX(((pc.mother.point.y + pc.father.point.y) / 2) - 10, pc.father.point, 1, model);
//                }
//                else
//                {
//                    PedigreeUtils.PullTowardX(((pc.mother.point.y + pc.father.point.y) / 2) - 10, pc.mother.point, 1, model);
//                    PedigreeUtils.PullTowardX(((pc.mother.point.y + pc.father.point.y) / 2) + 10, pc.father.point, 1, model);
//                }
//            }
//        }
//    }
//}





//foreach (PedigreeIndividual kid in pc.children)
//{
//    PedigreeUtils.PullTowardX(coupleAvg, kid.point, 0.05, model);
//}





//bool insider = false;
//foreach (PedigreeIndividual neighbor in model.individuals)
//{
//    if (pc.children.Contains(neighbor) == false)
//    {
//        if (Math.Abs(neighbor.point.y - yAcc) < (model.parameters.verticalSpacing / 2))
//        {
//            if (neighbor.point.x > sibMin && neighbor.point.x < sibMax)
//            {
//                insider = true;
//            }
//        }
//    }
//}
//if (insider)
//{
//    foreach (PedigreeIndividual kid in pc.children)
//    {
//        PedigreeUtils.PullTowardX(xAcc, kid.point, 1, model);
//    }
//}


/*
foreach (PedigreeCouple posParents in model.couples)
{
    if (pc != posParents)
    {
        if (pc.mother.HraPerson.motherID == posParents.mother.HraPerson.relativeID ||
            pc.mother.HraPerson.fatherID == posParents.father.HraPerson.relativeID ||
            pc.father.HraPerson.motherID == posParents.mother.HraPerson.relativeID ||
            pc.father.HraPerson.fatherID == posParents.father.HraPerson.relativeID)
        {

        }
    }
}

*/
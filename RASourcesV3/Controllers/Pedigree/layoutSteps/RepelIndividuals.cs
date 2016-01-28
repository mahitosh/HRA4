using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class RepelIndividuals
    {
        public readonly LayoutStep step;
        public RepelIndividuals(PedigreeModel model)
        {
            step = delegate()
            {
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    foreach (PedigreeIndividual neighbor in model.individuals)
                    {
                        if (pi != neighbor)
                        {
                            if (Math.Abs(neighbor.point.y - pi.point.y) < (model.parameters.verticalSpacing / 2))
                            {
                                if (Math.Abs(neighbor.point.x - pi.point.x) < model.parameters.horizontalSpacing)
                                {
                                    if (!model.parameters.hideNonBloodRelatives || (neighbor.bloodRelative && pi.bloodRelative))
                                    {
                                        if (pi.Group == null || pi.Group.Contains(neighbor)==false)
                                        {
                                            if (neighbor.point.x < pi.point.x)
                                            {
                                                PedigreeUtils.PullTowardX(neighbor.point.x + model.parameters.horizontalSpacing, pi.point, model.parameters.repelIndividualSetsStrength, model);
                                            }
                                            else
                                            {
                                                PedigreeUtils.PullTowardX(neighbor.point.x - model.parameters.horizontalSpacing, pi.point, model.parameters.repelIndividualSetsStrength, model);
                                            }
                                        }
                                    }
                                }
                                
                                //check for neighnor placed between person and spouse
                                if (neighbor.HraPerson.motherID == pi.HraPerson.motherID || neighbor.HraPerson.fatherID == pi.HraPerson.fatherID)
                                {
                                    foreach (PedigreeCouple pc in pi.spouseCouples)
                                    {
                                        PedigreeIndividual spouse;
                                        if (pc.mother == pi)
                                            spouse = pc.father;
                                        else
                                            spouse = pc.mother;

                                        if (spouse != null)
                                        {
                                            if (neighbor.point.x > Math.Min(pi.point.x, spouse.point.x) && neighbor.point.x < Math.Max(pi.point.x, spouse.point.x))
                                            {
                                                double x = neighbor.point.x;
                                                neighbor.point.x = pi.point.x;
                                                pi.point.x = x;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
        public void Repel(PedigreeIndividual pi, int delta, PedigreeModel model)
        {
            PedigreeUtils.PullTowardX(pi.point.x + delta, pi.point, model.parameters.repelIndividualSetsStrength, model); 
        }
    }
}











//foreach (PedigreeCouple pc in pi.spouseCouples)
//{
//    /*
//    if (pc.mother != null)
//    {
//        if (pc.mother != pi)
//        {
//            if (plus)
//                PedigreeUtils.PullTowardX(pc.mother.point.x + delta, pc.mother.point, model.parameters.repelIndividualSetsStrength, model);
//            else
//                PedigreeUtils.PullTowardX(pc.mother.point.x - delta, pc.mother.point, model.parameters.repelIndividualSetsStrength, model);
//        }
//    }
//    if (pc.father != null)
//    {
//        if (pc.father != pi)
//        {
//            if (plus)
//                PedigreeUtils.PullTowardX(pc.father.point.x + delta, pc.father.point, model.parameters.repelIndividualSetsStrength, model);
//            else
//                PedigreeUtils.PullTowardX(pc.father.point.x - delta, pc.father.point, model.parameters.repelIndividualSetsStrength, model);
//        }
//    }
//*/

//    //foreach (PedigreeIndividual kid in pc.children)
//    //{
//    //    if (kid.spouseCouples.Count == 0)
//    //        recursiveRepel(kid, delta, model);
//    //}
//}
               




/*
                                if (Math.Abs(neighbor.point.x - pi.point.x) < (2 * model.parameters.horizontalSpacing))
                                {

                                    if (pi.point.x < neighbor.point.x)
                                    {
                                        if (neighbor.Mother != null && pi.Mother != null)
                                        {
                                            if (pi.Mother.point.x > neighbor.Mother.point.x)
                                            {
                                                double temp = pi.Mother.point.x;
                                                pi.Mother.point.x = neighbor.Mother.point.x;
                                                neighbor.Mother.point.x = temp;
                                            }
                                        }
                                        if (neighbor.Father != null && pi.Father != null)
                                        {
                                            if (pi.Father.point.x > neighbor.Father.point.x)
                                            {
                                                double temp = pi.Father.point.x;
                                                pi.Father.point.x = neighbor.Father.point.x;
                                                neighbor.Father.point.x = temp;
                                            }
                                        }
                                    } 
                                    else if (pi.point.x > neighbor.point.x)
                                    {
                                        if (neighbor.Mother != null && pi.Mother != null)
                                        {
                                            if (pi.Mother.point.x < neighbor.Mother.point.x)
                                            {
                                                double temp = pi.Mother.point.x;
                                                pi.Mother.point.x = neighbor.Mother.point.x;
                                                neighbor.Mother.point.x = temp;
                                            }
                                        }
                                        if (neighbor.Father != null && pi.Father != null)
                                        {
                                            if (pi.Father.point.x < neighbor.Father.point.x)
                                            {
                                                double temp = pi.Father.point.x;
                                                pi.Father.point.x = neighbor.Father.point.x;
                                                neighbor.Father.point.x = temp;
                                            }
                                        }
                                    }
                                }
                                 */
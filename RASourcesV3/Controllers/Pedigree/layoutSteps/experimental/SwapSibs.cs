using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class SwapSibs
    {
        public readonly LayoutStep step;
        public SwapSibs(PedigreeModel model)
        {
            step = delegate()
            {
                foreach (PedigreeCouple pc1 in model.couples)
                {
                    pc1.point.x = (pc1.mother.point.x + pc1.father.point.x) / 2;
                    pc1.point.y = (pc1.mother.point.y + pc1.father.point.y) / 2;

                    foreach (PedigreeCouple pc2 in model.couples)
                    {
                        pc2.point.x = (pc2.mother.point.x + pc2.father.point.x) / 2;
                        pc2.point.y = (pc2.mother.point.y + pc2.father.point.y) / 2;

                        if (pc1 != pc2)
                        {
                            if (Math.Abs(pc1.point.y - pc2.point.y) < (model.parameters.verticalSpacing / 2))
                            {
                                bool FirstIsLeft = false;
                                if (pc1.point.x < pc2.point.x)
                                {
                                    FirstIsLeft = true;
                                }

                                foreach (PedigreeIndividual pi1 in pc1.children)
                                {
                                    foreach (PedigreeIndividual pi2 in pc2.children)
                                    {
                                        if (FirstIsLeft)
                                        {
                                            if (pi2.point.x < pi1.point.x)
                                            {
                                                double temp = pi2.point.x;
                                                pi2.point.x = pi1.point.x;
                                                pi1.point.x = temp;
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (pi1.point.x < pi2.point.x)
                                            {
                                                double temp = pi2.point.x;
                                                pi2.point.x = pi1.point.x;
                                                pi1.point.x = temp;
                                                return;
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


    }
}

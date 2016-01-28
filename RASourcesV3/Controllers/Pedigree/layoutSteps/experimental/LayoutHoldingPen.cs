using System;
using System.Collections.Generic;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class LayoutHoldingPen
   {
        public readonly LayoutStep step;
        public LayoutHoldingPen(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                //pull everything gently toward the center horizontally
                int minX = int.MaxValue;
                int maxX = -minX;

                foreach (PedigreeIndividual pi in model.individuals)
                {
                    if (model.HoldingPen.ContainsKey(pi.HraPerson.relativeID) == false)
                    {
                        if (pi.point.x < minX)
                            minX = (int)pi.point.x;
                    }
                }
                model.parameters.HoldingPenOrigin.X = minX;
                int count = 0;
                foreach (PedigreeIndividual pi in model.HoldingPen.Values)
                {
                        pi.point.x = model.parameters.HoldingPenOrigin.X + (model.parameters.horizontalSpacing * count);
                        pi.point.y = model.parameters.HoldingPenOrigin.Y + (model.parameters.verticalSpacing / 2);
                        count++;
                }
            };
        }
    }
}

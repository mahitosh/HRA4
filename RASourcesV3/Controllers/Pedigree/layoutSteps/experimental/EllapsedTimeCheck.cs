using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class EllapsedTimeCheck
    {
        Random autoRand = new Random();
        public readonly LayoutStep step;
        public EllapsedTimeCheck(PedigreeModel model)
        {
            step = delegate()
            {
                model.cycles++;
                if (model.cycles % 200 == 0 && model.cycles <= 800 && model.layoutIsValid == false)
                {
                    foreach (PointWithVelocity p in model.points)
                    {
                        double newX = autoRand.NextDouble() * (model.displayXMax - 1);
                        double newY = autoRand.NextDouble() * (model.displayYMax - 1);
                        p.x = newX;
                        p.dx = 0;
                        p.y = newY;
                        p.dy = 0;

                    }

                }

            };
        }


    }
}

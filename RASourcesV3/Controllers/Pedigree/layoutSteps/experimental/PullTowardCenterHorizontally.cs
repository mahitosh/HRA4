using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// This layout step pulls all couples and individuals toward
    /// the center of the display.
    /// </summary>
    class PullTowardCenterHorizontally
    {
        public readonly LayoutStep step;
        public PullTowardCenterHorizontally(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                //pull everything gently toward the center horizontally
                //foreach (PointWithVelocity point in model.points)
                //{
                //    double goalX = (model.displayXMin + model.displayXMax) / 2;
                //    double strength = model.parameters.centeringForceStrength;
                //    //strength = 0.06;
                //    PedigreeUtils.PullTowardX(goalX, point,strength);
                //}

                //pull everything gently toward the center horizontally
                double minX = double.MaxValue;
                double maxX = -minX;

                foreach (PointWithVelocity point in model.points)
                    if (point.x < minX)
                        minX = point.x;
                    else if (point.x > maxX)
                        maxX = point.x;

                double centerX = (minX + maxX) / 2;
                double idealCenterX = (model.displayXMin + model.displayXMax) / 2;
                double offset = idealCenterX - centerX;
                foreach (PointWithVelocity point in model.points)
                {
                    double goalX = point.x + offset;
                    double strength = model.parameters.centeringForceStrength;
                    PedigreeUtils.PullTowardX(goalX, point, strength, model);
                }
            };
        }
    }
}

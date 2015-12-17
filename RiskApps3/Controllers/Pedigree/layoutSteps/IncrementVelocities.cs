using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// This class increments all positions by their velocities, and 
    /// multiplies velocities by a dampening factor.
    /// </summary>
    class IncrementVelocities
    {
        public readonly LayoutStep step;
        public IncrementVelocities(PedigreeModel model){
            step = delegate()
            {
                foreach (PointWithVelocity point in model.points)
                {
                    if (!model.pointsBeingDragged.Contains(point))
                    {
                        point.x += point.dx;
                        point.y += point.dy;
                        point.dx *= model.parameters.dampeningFactor;
                        point.dy *= model.parameters.dampeningFactor;
                    }
                    else
                        point.dx = point.dy = 0;
                }
            };
        }
    }
}

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
    class PullToTargetPositions
    {
        public readonly LayoutStep step;
        public PullToTargetPositions(PedigreeModel model)
        {
            step = delegate()
            {
                foreach (PedigreeIndividual pi in model.individuals)
                {
                    PedigreeUtils.PullTowardX(model.TargetPositions[pi.HraPerson.relativeID].x, pi.point, 0.1, model);
                    PedigreeUtils.PullTowardY(model.TargetPositions[pi.HraPerson.relativeID].y, pi.point, 0.1, model);
                }
            };
        }
    }
}


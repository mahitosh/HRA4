
using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Responsible for incrementing members of model.io.
    /// </summary>
    class IncrementControllerVariables
    {
        public readonly LayoutStep step;
        public IncrementControllerVariables(PedigreeModel model, PedigreeController layoutEngine)
        {
            step = delegate()
            {
                model.parameters.ControllerMode = layoutEngine.GetMode();
            };
        }
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Responsible for incrementing members of model.io.
    /// </summary>
    class IncrementIOVariables
    {
        public readonly LayoutStep step;
        public IncrementIOVariables(PedigreeModel model)
        {
            step = delegate()
            {
                model.io.mousePressed = !model.io.pMouseDown && model.io.mouseDown;
                model.io.mouseReleased = model.io.pMouseDown && !model.io.mouseDown;

                model.io.pMouseDown = model.io.mouseDown;
                model.io.pMouseX = model.io.mouseX;
                model.io.pMouseY = model.io.mouseY;
            };
        }
    }
}

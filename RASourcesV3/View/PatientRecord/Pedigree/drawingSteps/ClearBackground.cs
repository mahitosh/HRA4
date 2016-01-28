using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Controllers.Pedigree;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Clears the background and turns on anti-aliasing.
    /// </summary>
    class ClearBackground
    {
        Font idLabelFont = new Font("Tahoma", 12);
        Point labelorigin = new Point(50, 50);
        Pen LassoPen = new Pen(Color.DarkRed, 3F);
        Point linkingOrigin = new Point(200, 50);

        public readonly DrawingStep step;
        internal ClearBackground(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                //draw the background
                g.FillRectangle(model.parameters.BackgroundBrush,
                    (int)model.displayXMin,
                    (int)model.displayYMin,
                    (int)(model.displayXMax - model.displayXMin),
                    (int)(model.displayYMax - model.displayYMin)
                );

                //legendText = "";

                //if (model.parameters.ShowUnitnum)
                //    legendText += ", " + model.familyHistory.proband.unitnum;

                //if (model.parameters.ShowName)
                //    legendText += ", " + model.familyHistory.proband.name;

                //if (model.parameters.ShowDob)
                //    legendText += ", " + model.familyHistory.proband.dob;

                //g.DrawString(legendText.Trim(trimChars), idLabelFont, Brushes.Black, labelorigin);


                if (model.RelativesToLink.Count > 0)
                {
                    linkingOrigin.X = (int)(model.controlWidth / 2);
                    g.DrawString("Please select the parent", idLabelFont, Brushes.Red, linkingOrigin);
                }

                if (model.converged == false && model.parameters.ControllerMode == PedigreeController.SELF_ORGANIZING)
                {
                    linkingOrigin.X = (int)(model.controlWidth / 2);
                    linkingOrigin.Y += 20;
                    g.DrawString("Working...", idLabelFont, Brushes.Gray, linkingOrigin);
                    linkingOrigin.Y -= 20;
                }
                //if (
            };
        }
    }
}

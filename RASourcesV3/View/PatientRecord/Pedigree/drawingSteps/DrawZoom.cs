using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;


namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Applies scaling and zooming.
    /// </summary>
    class DrawZoom
    {
        public readonly DrawingStep step;

        public DrawZoom(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                g.ScaleTransform(model.parameters.scale, model.parameters.scale);
                g.TranslateTransform(model.parameters.hOffset, model.parameters.vOffset);

                //g.FillEllipse(Brushes.Thistle, new Rectangle(new Point((int)(model.displayXMax / 2)-10, (int)(model.displayYMax / 2)-10), new Size(20, 20)));
                //g.DrawRectangle(Pens.Black, new Rectangle(
                //    (int)model.displayXMin,
                //    (int)model.displayYMin,
                //    (int)(model.displayXMax - model.displayXMin),
                //    (int)(model.displayYMax - model.displayYMin)));
            };
        }
    }
}
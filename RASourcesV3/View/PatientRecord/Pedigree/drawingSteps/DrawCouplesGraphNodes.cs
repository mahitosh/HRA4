using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Draws nodes of the couples graph.
    /// </summary>
    class DrawCouplesGraphNodes
    {
        public readonly DrawingStep step;
        private Rectangle tempRect = new Rectangle();
        private Brush b = new SolidBrush(Color.Black);

        public DrawCouplesGraphNodes(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                if (model.parameters.drawCouplesGraph)
                {
                    tempRect.Width = tempRect.Height = model.parameters.coupleNodeRadius * 2;
                    foreach (PedigreeCouple couple in model.couples)
                    {
                        tempRect.X = (int)couple.point.x - model.parameters.coupleNodeRadius;
                        tempRect.Y = (int)couple.point.y - model.parameters.coupleNodeRadius;
                        g.FillEllipse(b, tempRect);
                    }
                }
            };
        }
    }
}

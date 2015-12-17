using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    class DrawLasso
    {
        Pen LassoPen = new Pen(Color.DarkRed, 3F);
        public readonly DrawingStep step;
        internal DrawLasso(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                //turn on anti-aliasing (smooth lines)
                g.SmoothingMode = SmoothingMode.AntiAlias;


                LassoPen.DashPattern = new float[] { 2, 3 };
                if (model.SelectionLasso.Count > 2)
                    g.DrawPolygon(LassoPen, model.SelectionLasso.ToArray());
            };
        }
    }
}

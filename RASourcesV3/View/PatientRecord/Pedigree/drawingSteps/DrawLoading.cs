using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.View.PatientRecord.Pedigree.drawingSteps;
using RiskApps3.Controllers.Pedigree;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    class DrawLoading
    {
        private Pen thePen = new Pen(Brushes.WhiteSmoke);
        Font theFont = new Font("Tahoma", 24);

        private Pen theOrganizingPen = new Pen(Brushes.Black);
        Font theOrganizingFont = new Font("Tahoma", 14);

        public readonly DrawingStep step;

        public DrawLoading(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                //moved to clear background
                //if (model.parameters.allRelativesAccountedFor == false)
                //{
                //    float x = (float)(model.displayXMax / 2) - 100;
                //    float y = (float)(model.displayYMax / 2);
                //    g.FillRectangle(new SolidBrush(Color.FromArgb(64, Color.Gray)), new Rectangle(0, 0, (int)model.displayXMax, (int)model.displayYMax));
                //    g.DrawString("Loading...", theFont, Brushes.WhiteSmoke, new PointF(x, y));
                //}
                
                //if (model.parameters.ControllerMode == PedigreeController.SELF_ORGANIZING)
                //{
                //    float x = (float)(model.displayXMax / 2) - 100;
                //    float y = 30;
                //    g.DrawString("Organizing...", theOrganizingFont, Brushes.Red, new PointF(x, y));
                //}

            };
        }
    }
}
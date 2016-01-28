using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps.interaction
{
    class DragPoints
    {
        public readonly LayoutStep step;


        public DragPoints(PedigreeModel model, PedigreeController layoutEngine)
        {
            step = delegate()
            {
                //if the mouse was pressed and there were no couples being dragged,
                //drag the one under the mouse point.
                if (model.io.mousePressed)
                {
                    double x = model.io.mouseX;// -model.parameters.hOffset;
                    double y = model.io.mouseY;// -model.parameters.vOffset;
                    PointWithVelocity pointUnderCursor = GetPointUnderCursor(model, x, y);

                    if (model.Selected.Count < 2)
                    {
                        if (pointUnderCursor != null)
                            model.pointsBeingDragged.Add(pointUnderCursor);
                    }
                    else
                    {
                        bool found = false;
                        foreach (PedigreeIndividual pi in model.Selected)
                        {
                            model.pointsBeingDragged.Add(pi.point);
                            if (pi.point == pointUnderCursor)
                            {
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            model.pointsBeingDragged.Clear();
                        }
                    }
                }
                if (model.io.mouseReleased)
                {
                    model.pointsBeingDragged.Clear();
                }

                foreach (PointWithVelocity point in model.pointsBeingDragged)
                {
                    point.x += ((model.io.mouseX - model.io.pMouseX) / model.parameters.scale);
                    point.y += ((model.io.mouseY - model.io.pMouseY) / model.parameters.scale);
                }
            };
        }
                
        private PointWithVelocity GetPointUnderCursor(PedigreeModel model, double x, double y)
        {

            PedigreeIndividual individual = PedigreeUtils.GetIndividualUnderPoint(model,x,y);
            if (individual != null)
                return individual.point;
            else
                return null;
        }
        //private PointWithVelocity GetPointUnderCursor(PedigreeModel model, double x, double y)
        //{

        //    //TODO revive couples dragging
        //    foreach (PedigreeCouple couple in model.couples)
        //    {
        //        double dx = x - couple.point.x;
        //        double dy = y - couple.point.y;
        //        if (Math.Sqrt(dx * dx + dy * dy) < model.parameters.coupleNodeRadius)
        //            return couple.point;
        //    }
        //    PedigreeIndividual individual = PedigreeUtils.GetIndividualUnderPoint(model,x,y);
        //    if (individual != null)
        //        return individual.point;
        //    else
        //        return null;
        //}
    }
}

using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Utilities;

namespace RiskApps3.Controllers.Pedigree.layoutSteps.interaction
{
    class Pan
    {
        public readonly LayoutStep step;

        private bool panInProgress = false;
        private double x1, y1,hOffset1,vOffset1;
        public Pan(PedigreeModel model)
        {
            step = delegate()
            {

                //if the mouse was pressed and there were no couples being dragged,
                //drag the one under the mouse point.
                if (model.io.mousePressed)
                {
                    double x = model.io.mouseX;
                    double y = model.io.mouseY;
                    PointWithVelocity pointUnderCursor = GetPointUnderCursor(model, x, y);
                    if (pointUnderCursor == null)
                    {
                        x1 = x; y1 = y;
                        hOffset1 = model.parameters.hOffset;
                        vOffset1 = model.parameters.vOffset;
                        panInProgress = true;
                        //model.parameters.multiSelect = true;
                    } 
                }
                else if (model.io.mouseReleased)
                {
                    if (model.SelectionLasso.Count > 3)
                    {
                        model.Selected.Clear();
                        Polygon p = new Polygon(model.SelectionLasso.ToArray());
                        foreach (PedigreeIndividual pi in model.individuals)
                        {
                            System.Drawing.Point scaled = new System.Drawing.Point((int)pi.point.x, (int)pi.point.y);

                            scaled.X += model.parameters.hOffset;
                            scaled.Y += model.parameters.vOffset;

                            scaled.X = (int)(model.parameters.scale * (double)scaled.X);
                            scaled.Y = (int)(model.parameters.scale * (double)scaled.Y);



                            pi.Selected = false;
                            if (p.Contains(scaled))
                            {
                                pi.Selected = true;
                                if(model.Selected.Contains(pi) == false)
                                    model.Selected.Add(pi);
                            }
                        }
                    }
                    else
                    {
                        //PedigreeIndividual s = PedigreeUtils.GetIndividualUnderPoint(model, x1, y1);
                        //if (s != null)
                        //{
                        //    s.Selected = true;
                        //    if(model.Selected.Contains(s) == false)
                        //            model.Selected.Add(s);
                        //}
                    }
                    model.parameters.multiSelect = false;
                    panInProgress = false;
                    model.SelectionLasso.Clear();
                   
                }
                else if (panInProgress)
                {
                    double x2 = model.io.mouseX;
                    double y2 = model.io.mouseY;

                    if (model.parameters.multiSelect)
                    {
                        model.SelectionLasso.Add(new System.Drawing.Point((int)x2, (int)y2));
                    }
                    else
                    {
                        model.parameters.hOffset = (int)(hOffset1 + x2 - x1);
                        model.parameters.vOffset = (int)(vOffset1 + y2 - y1);
                    }
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
    }
}

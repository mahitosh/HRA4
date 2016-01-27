using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Controllers.Pedigree.layoutSteps;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Draws edges of the couples graph.
    /// </summary>
    class DrawCouplesGraphEdges
    {
        public readonly DrawingStep step;

        private Pen couplesEdgePen = new Pen(Brushes.Black, 2);
        //private Pen crossedEdgePen = new Pen(Brushes.Black, 2);
        List<PedigreeCoupleEdge> crossedEdges = new List<PedigreeCoupleEdge>();

        public DrawCouplesGraphEdges(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                if (model.parameters.drawCouplesGraph)
                {
                    int n = model.coupleEdges.Count;
                    crossedEdges.Clear();
                    for (int i = 0; i < n; i++)
                        for (int j = i + 1; j < n; j++)
                        {
                            PedigreeCoupleEdge edgeA = model.coupleEdges[i];
                            PedigreeCoupleEdge edgeB = model.coupleEdges[j];

                            if (DetectPlanarity.EdgesCross(edgeA, edgeB, model.parameters.hideNonBloodRelatives))
                            {
                                crossedEdges.Add(edgeA);
                                crossedEdges.Add(edgeB);
                            }
                        }

                    foreach (PedigreeCoupleEdge edge in model.coupleEdges)
                    {
                        int x1 = (int)edge.u.point.x;
                        int y1 = (int)edge.u.point.y;
                        int x2 = (int)edge.v.point.x;
                        int y2 = (int)edge.v.point.y;

                       // Pen pen = model.couplesGraphIsPlanar ? couplesEdgePen : crossedEdgePen;
                        if (edge.intergenerational)
                            g.DrawLine(couplesEdgePen, x1, y1, x2, y2);
                        else
                        {
                            //if (x1 != x2 && y1 != y2)
                            //    g.DrawArc(couplesEdgePen, x1, y1,x2-x1, 50, 0, (float)Math.PI);
                            int height = model.parameters.halfSibsArcHeight;
                            int y = (int)((y1 + y2) / 2) - height;
                            if (x2 > x1)
                                g.DrawArc(couplesEdgePen, x1, y, x2 - x1, height * 2, 0, -180);
                            else if (x1 > x2)
                                g.DrawArc(couplesEdgePen, x2, y, x1 - x2, height * 2, 0, -180);
                        }

                    }
                    
                }
            };
        }
    }
}

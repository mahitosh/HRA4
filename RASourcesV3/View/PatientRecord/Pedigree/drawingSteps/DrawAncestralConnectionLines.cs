using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

using System.Drawing;
using RiskApps3.View.PatientRecord.Pedigree.drawingSteps;
using RiskApps3.Controllers.Pedigree;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    class DrawAncestralConnectionLines
    {
        public readonly DrawingStep step;
        public DrawAncestralConnectionLines(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                //g.FillEllipse(Brushes.Plum, new Rectangle((int)(model.displayXMax / 2) - 50, (int)(model.displayYMax / 2) - 50, 100, 100));
                //g.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)model.displayXMax, (int)model.displayYMax));

                int halfIndividualSize = model.parameters.individualSize / 2;
                Pen pen = Pens.Black;

                //if (model.parameters.ControllerMode == PedigreeController.SELF_ORGANIZING)
                //{
                //    //pen = model.layoutIsValid ? Pens.Black : Pens.Red;
                //    pen = model.couplesGraphIsPlanar ? Pens.Black : Pens.Red;
                //}
                
                foreach (PedigreeCouple parents in model.couples)
                {
                    // draw line between parents
                    if (parents.mother != null && parents.father != null)
                        if (!model.parameters.hideNonBloodRelatives || (parents.father.bloodRelative && parents.mother.bloodRelative))
                        {
                            if (parents.mother.HraPerson.consanguineousSpouseID == parents.father.HraPerson.relativeID)
                            {
                                g.DrawLine(pen,
                                    (int)parents.mother.point.x,
                                    (int)parents.mother.point.y + 2,
                                    (int)parents.father.point.x,
                                    (int)parents.father.point.y + 2
                                );
                                g.DrawLine(pen,
                                    (int)parents.mother.point.x,
                                    (int)parents.mother.point.y - 2,
                                    (int)parents.father.point.x,
                                    (int)parents.father.point.y - 2
                                );
                            }
                            else
                            {
                                g.DrawLine(pen,
                                    (int)parents.mother.point.x,
                                    (int)parents.mother.point.y,
                                    (int)parents.father.point.x,
                                    (int)parents.father.point.y
                                );
                            }
                        }
                    try
                    {
                        //draw lines to children
                        List<PedigreeIndividual> children = new List<PedigreeIndividual>(); //= parents.children;
                        foreach (PedigreeIndividual pi in parents.children)
                        {
                            if (!model.parameters.hideNonBloodRelatives || pi.bloodRelative)
                            {
                                children.Add(pi);
                            }
                        }
                        if (children.Count > 1)
                        {
                            if (parents.mother != null && parents.father != null)
                            {
                                // draw horizontal line connecting parents with multiple children
                                double minX = (parents.father.point.x + parents.mother.point.x) / 2;
                                double maxX = minX;
                                Dictionary<int, List<PedigreeIndividual>> twins = new Dictionary<int, List<PedigreeIndividual>>();
                                foreach (PedigreeIndividual child in children)
                                {
                                    if (child.HraPerson.twinID < 1)
                                    {
                                        minX = child.point.x < minX ? child.point.x : minX;
                                        maxX = child.point.x > maxX ? child.point.x : maxX;
                                    }
                                    else
                                    {
                                        if (twins.ContainsKey(child.HraPerson.twinID))
                                        {
                                            twins[child.HraPerson.twinID].Add(child);
                                        }
                                        else
                                        {
                                            twins.Add(child.HraPerson.twinID, new List<PedigreeIndividual>());
                                            twins[child.HraPerson.twinID].Add(child);
                                        }
                                    }
                                }
                                int midY = (int)(parents.mother.point.y + model.parameters.verticalSpacing / 2);
                                g.DrawLine(pen, (int)minX, midY, (int)maxX, midY);

                                // draw line from parents down
                                int x = (int)((parents.father.point.x + parents.mother.point.x) / 2);
                                int topY = (int)((parents.father.point.y + parents.mother.point.y) / 2);
                                g.DrawLine(pen, x, topY, x, midY);

                                // draw lines from children up
                                foreach (PedigreeIndividual child in children)
                                {
                                    if (child.HraPerson.twinID < 1)
                                    {
                                        if (child.HraPerson.motherID > 0 || child.HraPerson.fatherID > 0)
                                        {
                                            g.DrawLine(pen,
                                                 (int)child.point.x,
                                                 (int)child.point.y - model.parameters.individualSize / 2,
                                                 (int)child.point.x, midY
                                            );
                                        }
                                    }

                                }
                                foreach (List<PedigreeIndividual> twinKids in twins.Values)
                                {
                                    double twinMin = double.MaxValue;
                                    double twinMax = double.MinValue;
                                    double twinCenter = 0;
                                    foreach (PedigreeIndividual t in twinKids)
                                    {
                                        twinCenter += t.point.x;
                                        if (t.point.x > twinMax)
                                            twinMax = t.point.x;
                                        if (t.point.x < twinMin)
                                            twinMin = t.point.x;
                                    }
                                    twinCenter /= twinKids.Count;

                                    foreach (PedigreeIndividual t in twinKids)
                                    {
                                        double x1 = t.point.x;
                                        double y1 = t.point.y - model.parameters.individualSize / 2;
                                        double x2 = twinCenter;
                                        double y2 = midY;
                                        g.DrawLine(pen, (int)x1, (int)y1, (int)x2, (int)y2);

                                        if (t.HraPerson.twinType == "Identical")
                                        {
                                            double alpha = Math.Atan((y2 - y1) / (x2 - x1));

                                            double deltaY = (y2 - y1) / 2.0;
                                            double dx = deltaY * Math.Tan((Math.PI / 2) - alpha);

                                            //g.DrawLine(pen, (int)(t.point.x + dx), (int)(t.point.y + deltaY), (int)(twinCenter), (int)(t.point.y + deltaY));
                                            g.DrawLine(pen, (int)(t.point.x + dx), (int)(y2 - deltaY), (int)(twinCenter), (int)(midY - deltaY));
                                        }
                                    }

                                    g.DrawLine(pen, (int)(parents.father.point.x + parents.mother.point.x) / 2, midY, (int)twinCenter, midY);
                                }
                            }
                        }
                        else if (children.Count == 1)
                        {
                            if (parents.mother != null && parents.father != null)
                            {
                                double motherX = parents.mother.point.x;
                                double motherY = parents.mother.point.y;

                                double fatherX = parents.father.point.x;
                                double fatherY = parents.father.point.y;

                                double childX = children[0].point.x;
                                double childY = children[0].point.y;


                                double parentsSpan = motherX - fatherX;
                                double p = parentsSpan == 0 ? 0.5 : (childX - fatherX) / parentsSpan;

                                p = p > 1 ? 1 : p < 0 ? 0 : p;

                                int topX = (int)((fatherX * (1 - p) + motherX * p));
                                int topY = (int)((fatherY * (1 - p) + motherY * p));

                                g.DrawLine(pen, topX, topY, (int)childX, (int)childY);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteToLog(e.ToString());
                    }
                }
            };
        }
    }
}

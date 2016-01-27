using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Model.PatientRecord;


namespace RiskApps3.View.PatientRecord.Pedigree.drawingSteps
{
    /// <summary>
    /// Draws layout steps.
    /// </summary>
    class DrawLayoutSteps
    {

        public readonly DrawingStep step;

        public DrawLayoutSteps(PedigreeModel model)
        {
            step = delegate(Graphics g)
            {
                
                if (model.parameters.drawVisualDebugging && model.couplesGraphIsPlanar)
                {
                    //draw the center line
                    //double idealCenterX = (model.displayXMin + model.displayYMax) / 2;



                    int avgVerticalOffset = (int)(
                        (model.parameters.verticalSpacing - model.parameters.individualSize) / 4);
                    foreach (PedigreeCouple couple in model.couples)
                    {
                        //draw lines showing the averages between couples
                        int motherX = (int)couple.mother.point.x;
                        int motherY = (int)couple.mother.point.y + model.parameters.individualSize / 2;
                        int fatherX = (int)couple.father.point.x;
                        int fatherY = (int)couple.father.point.y + model.parameters.individualSize / 2;
                        int coupleCenterX = (motherX + fatherX) / 2;
                        int coupleCenterY = (motherY + fatherY) / 2 + avgVerticalOffset;

                        Pen pen = Pens.Green;
                        g.DrawLine(pen, motherX, motherY, motherX, coupleCenterY);
                        g.DrawLine(pen, motherX, coupleCenterY, coupleCenterX, coupleCenterY);
                        g.DrawLine(pen, fatherX, fatherY, fatherX, coupleCenterY);
                        g.DrawLine(pen, fatherX, coupleCenterY, coupleCenterX, coupleCenterY);

                        //draw lines showing the averages between sibships
                        if (couple.children.Count > 0)
                        {
                            int sibshipMinX = int.MaxValue;
                            int sibshipMaxX = -int.MaxValue;
                            foreach (PedigreeIndividual child in couple.children)
                            {
                                if (child.point.x < sibshipMinX)
                                    sibshipMinX = (int)child.point.x;
                                if (child.point.x > sibshipMaxX)
                                    sibshipMaxX = (int)child.point.x;
                            }

                            int sibshipY = (int)couple.point.y +
                                model.parameters.verticalSpacing - model.parameters.individualSize / 2;
                            int sibshipCenterX = (sibshipMinX + sibshipMaxX) / 2;
                            int sibshipCenterY = sibshipY - avgVerticalOffset;


                            g.DrawLine(pen, sibshipMinX, sibshipY, sibshipMinX, sibshipCenterY);
                            g.DrawLine(pen, sibshipMinX, sibshipCenterY, sibshipCenterX, sibshipCenterY);

                            g.DrawLine(pen, sibshipMaxX, sibshipY, sibshipMaxX, sibshipCenterY);
                            g.DrawLine(pen, sibshipMaxX, sibshipCenterY, sibshipCenterX, sibshipCenterY);


                            //draw the line showing the average of the two centers
                            g.DrawLine(pen, sibshipCenterX, sibshipCenterY, coupleCenterX, coupleCenterY);

                            int middleX = (sibshipCenterX + coupleCenterX) / 2;
                            int middleY = (sibshipCenterY + coupleCenterY) / 2;
                            int midRadius = 5;
                            int side = midRadius * 2;
                            g.FillEllipse(Brushes.Green, middleX - midRadius, middleY - midRadius, side, side);

                            //draw the ideal positions of the couple
                            bool fatherToTheLeft = fatherX < motherX;
                            int f = fatherToTheLeft ? 1 : -1;
                            int idealFatherX = middleX - f * model.parameters.horizontalSpacing / 2;
                            int idealMotherX = middleX + f * model.parameters.horizontalSpacing / 2;

                            pen = Pens.Red;
                            g.DrawLine(pen, idealFatherX, fatherY, middleX, middleY);
                            g.DrawLine(pen, idealMotherX, motherY, middleX, middleY);

                            DrawIndividual(idealFatherX, couple.father.point.y, couple.father.HraPerson.gender, model, g, pen);
                            DrawIndividual(idealMotherX, couple.mother.point.y, couple.mother.HraPerson.gender, model, g, pen);

                            //draw the ideal sibship positions
                            //Step 2 of 2: Pull sibship members toward their ideal positions,
                            //spaced ideally and centered around thir parent couple, leaving
                            //open spaces for spouses (as they will be positioned by the 
                            //layout step addressing couples).
                            {
                                double spacing = model.parameters.horizontalSpacing;

                                //Compute the offset from the couple center for positioning individuals
                                double offset = 0;
                                {
                                    int numIndividualsInSibshipSpan = 0;
                                    // foreach (PedigreeIndividual child in couple.children)
                                    for (int j = 0; j < couple.children.Count; j++)
                                    {
                                        PedigreeIndividual child = couple.children[j];

                                        //count the child
                                        numIndividualsInSibshipSpan++;

                                        //count the spouse ... sometimes
                                        //if (child.spouseCouples.Count == 1)
                                        //{
                                        //    bool thisSpouseCounts = true;
                                        //    bool spouseIsToRight = child.Spouse.point.x > child.point.x;

                                        //    //don't count the spouse to the right of
                                        //    //the sibship's rightmost child
                                        //    if (j == couple.children.Count - 1 && spouseIsToRight)
                                        //        thisSpouseCounts = false;

                                        //    //don't count the spouse to the left of
                                        //    //the sibship's leftmost child
                                        //    if (j == 0 && !spouseIsToRight)
                                        //        thisSpouseCounts = false;

                                        //    if (thisSpouseCounts)
                                        //        numIndividualsInSibshipSpan++;
                                        //}
                                        //else if (couple.children[j].spouseCouples.Count == 2)
                                        //    throw new Exception("Half siblings not yet supported");
                                    }
                                    double sibshipSpanSize = (numIndividualsInSibshipSpan - 1) * spacing;
                                    offset = middleX - sibshipSpanSize / 2;
                                }

                                //position individuals ideally by applying a force to them
                                int i = 0;
                                foreach (PedigreeIndividual child in couple.children)
                                {   //position the individuals horizontally based on 
                                    //computation of their ideal horizontal positions
                                    {
                                        double goalX = offset;

                                        //if the child has no spouse
                                        if (child.spouseCouples.Count == 0)
                                        {
                                            //compute the position directly
                                            //without incrementing i
                                            goalX += i * spacing;
                                        }
                                        //if the child has a spouse, insert appropriate spacing
                                        else if (child.spouseCouples.Count == 1)
                                        {
                                            //where the space goes depends on whether the spouse is 
                                            //to the left or right of the child, so find out
                                            //if the spouse is to the right or left of the child:
                                            //    bool spouseIsToRight = child.Spouse.point.x > child.point.x;

                                            //    //if the spouse is to the right...
                                            //    if (spouseIsToRight)
                                            //        //...but not for the rightmost child
                                            //        if (i != couple.children.Count - 1)
                                            //            //leave a space to the right.
                                            //            goalX = offset + (i++) * spacing;
                                            //        else
                                            //            goalX = offset + i * spacing;
                                            //    else//if the spouse is to the left...
                                            //        //...but not for the leftmost child
                                            //        if (i != 0)
                                            //            //leave a space to the left.
                                            //            goalX = offset + (++i) * spacing;
                                            //}
                                            //else if (child.spouseCouples.Count == 2)
                                            //    throw new Exception("Half siblings not yet supported");
                                        }
                                        g.DrawLine(pen, (float)middleX, (float)middleY, (float)goalX, (float)sibshipY);
                                        DrawIndividual(goalX, child.point.y, child.HraPerson.gender, model, g, pen);
                                        //increment i to account for enumeration this child
                                        i++;
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private void DrawIndividual(double x, double y, string gender, PedigreeModel model, Graphics g, Pen pen)
        {
            x -= model.parameters.individualSize / 2;
            y -= model.parameters.individualSize / 2;
            int size = (int)model.parameters.individualSize;
            if (gender == PedigreeIndividual.GENDER_MALE)
                g.DrawRectangle(pen, (int)x, (int)y, size, size);
            else
                g.DrawEllipse(pen, (int)x, (int)y, size, size);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// This class generates a pedigree layout step which enables the positions 
    /// of individual participants in a couple to influence the position of their
    /// common couple node.
    /// </summary>
    class SnapToGrid
    {
        static PedigreeCoupleComparer pcc = new PedigreeCoupleComparer();
        public readonly LayoutStep step;
        public SnapToGrid(PedigreeModel model, PedigreeController layoutEngine)
        {
            step = delegate()
            {
                if (model.io.mousePressed == false)
                {
                    foreach (PedigreeIndividual pi in model.individuals)
                    {
                        if (model.pointsBeingDragged.Contains(pi.point)==false)
                        {
                            double deltaX = pi.point.x % model.parameters.gridWidth;
                            if (deltaX > 0.0)
                            {
                                if (deltaX >= (model.parameters.gridWidth / 2.0))
                                    pi.point.x += (model.parameters.gridWidth - deltaX);
                                else
                                    pi.point.x -= (deltaX);
                            }

                            double deltaY = pi.point.y % model.parameters.gridHeight;
                            if (deltaY > 0.0)
                            {
                                if (deltaY >= (model.parameters.gridHeight / 2.0))
                                    pi.point.y += (model.parameters.gridHeight - deltaY);
                                else
                                    pi.point.y -= (deltaY);
                            }

                        }
                    }
                }
            };
        }

        class PedigreeCoupleComparer : IComparer<PedigreeCouple>
        {
            public int Compare(PedigreeCouple x, PedigreeCouple y)
            {
                if (x == null)
                {
                    if (y == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    if (y == null)
                    {
                        return 1;
                    }
                    else
                    {
                        int retval = (x.mother.numberOfAncestors + x.father.numberOfAncestors).CompareTo((y.mother.numberOfAncestors + y.father.numberOfAncestors));

                        if (retval != 0)
                        {
                            return retval;
                        }
                        else
                        {
                            return x.point.x.CompareTo(y.point.x);
                        }
                    }
                }
            }
        }
    }
}







/*
 * 
 * 
 *                 if (model.parameters.saveAndClose && model.parameters.allRelativesAccountedFor)
                    layoutEngine.SetMode(PedigreeController.SAVE_AND_CLOSE);
 * 
 * 
 * 
 * 
bool multDrag = false;
foreach (PedigreeIndividual pi in model.individuals)
{
    //pi.point.x = PedigreeUtils.pullToward(pi.point.x, model.gridWidth * ((int)pi.point.x / (int)model.gridWidth), attractionStrength);
    //pi.point.y = PedigreeUtils.pullToward(pi.point.y, model.gridHeight * ((int)pi.point.y / (int)model.gridHeight), attractionStrength);
                    
    if (model.Selected.Count > 0)
    {
        multDrag = true;
        if (model.firstSnapFrame == true || model.Selected.Contains(pi) == false)
        {
            if (pi.point.x % model.gridWidth > model.gridWidth / 2)
                pi.point.x = (model.gridWidth) * (1 + ((int)pi.point.x / (int)model.gridWidth));
            else
                pi.point.x = model.gridWidth * ((int)pi.point.x / (int)model.gridWidth);

            if (pi.point.y % model.gridHeight > model.gridHeight / 2)
                pi.point.y = (model.gridHeight) * (1 + ((int)pi.point.y / (int)model.gridHeight));
            else
                pi.point.y = model.gridHeight * ((int)pi.point.y / (int)model.gridHeight);

        }
    }
    else
    {
        if (model.firstSnapFrame == true || model.pointsBeingDragged.Contains(pi.point))
        {
            if (pi.point.x % model.gridWidth > model.gridWidth / 2)
                pi.point.x = (model.gridWidth) * (1 + ((int)pi.point.x / (int)model.gridWidth));
            else
                pi.point.x = model.gridWidth * ((int)pi.point.x / (int)model.gridWidth);

            if (pi.point.y % model.gridHeight > model.gridHeight / 2)
                pi.point.y = (model.gridHeight) * (1 + ((int)pi.point.y / (int)model.gridHeight));
            else
                pi.point.y = model.gridHeight * ((int)pi.point.y / (int)model.gridHeight);

        }
    }
}



List<PedigreeCouple> byAscentry = new List<PedigreeCouple>(model.couples);
byAscentry.Sort(pcc);
foreach (PedigreeCouple parents in byAscentry)
{
    //if (model.HideInlaws)
    //{
    //    if (parents.mother.bloodRelative == false || parents.father.bloodRelative == false)
    //    {
    //        int xTarget = ((int)(parents.mother.point.x + parents.father.point.x) / 2);
    //        parents.mother.point.x = xTarget;
    //        parents.father.point.x = xTarget;
    //    }
    //}


    if (parents.children.Count == 1)
    {
        {
            if (model.pointsBeingDragged.Contains(parents.children[0].point) == false)
            {
                int x = (int)((parents.mother.point.x + parents.father.point.x) / 2) - (int)parents.children[0].point.x;
                parents.children[0].point.x += x;
            }
        }
    }

}


if (multDrag == false)
    model.firstSnapFrame = true;
else
    model.firstSnapFrame = false;
*/
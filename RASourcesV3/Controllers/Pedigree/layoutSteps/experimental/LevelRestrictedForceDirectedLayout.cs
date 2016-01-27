using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;
using RiskApps3.Utilities;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// This class executes a level-restricted force directed layout on the couples graph.
    /// </summary>
    class LevelRestrictedForceDirectedLayout
    {
        public readonly LayoutStep step;

        /// <summary>
        /// A value close to zero.
        /// </summary>
        public static double epsilon = 1;

        /// <summary>
        /// During the repulsion layout step, force computations on objects
        /// closer together than repulsionEpsilon are considered instead to be
        /// exactly repulsionEpsilon units apart, for the sake of avoiding
        /// occasional astronomical velocity values which throw the system out of whack.
        /// </summary>
        public static double repulsionEpsilon = 10;

        /// <summary>
        /// A constant used as a scaling factor in the attraction layout step.
        /// </summary>
        public static double attractionStrength = 0.1;

        /// <summary>
        /// A constant used as a scaling factor in the repulsion layout step.
        /// </summary>
        public static double repulsionStrength = 10;

        public LevelRestrictedForceDirectedLayout(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                if (!model.couplesGraphIsPlanar)
                {
                    attractAdjacentCouplePairs(model);
                    repelAllCouplePairs(model);


                    //int n = model.coupleEdges.Count;
                    //for (int i = 0; i < n; i++)
                    //    for (int j = i + 1; j < n; j++)
                    //        if (model.edgesCross(model.coupleEdges[i], model.coupleEdges[j]))
                    //        {
                    //            ShrinkEdge(model.coupleEdges[i]);
                    //            ShrinkEdge(model.coupleEdges[j]);
                    //        }


                }
            };
        }




        private static void repelAllCouplePairs(PedigreeModel model)
        {
            try
            {
                int n = model.couples.Count;

                //consider the upper triangle (all pairs)
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        PedigreeCouple coupleA = model.couples[i];
                        PedigreeCouple coupleB = model.couples[j];

                        if (coupleA.GenerationalLevel == coupleB.GenerationalLevel)
                        {
                            PointWithVelocity a = coupleA.point;
                            PointWithVelocity b = coupleB.point;

                            //separate exactly overlapping points
                            if (b.x - a.x == 0)
                                a.x += epsilon;

                            double distance = b.x - a.x;
                            double spread = 200;
                            double forceX = 10 * repulsionStrength * spread / (distance * distance + spread);

                            forceX *= b.x > a.x ? 1 : -1;

                            a.dx -= forceX;
                            b.dx += forceX;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        private static void attractAdjacentCouplePairs(PedigreeModel model)
        {

            foreach (PedigreeCoupleEdge edge in model.coupleEdges)
                ShrinkEdge(edge, attractionStrength);

            int n = model.coupleEdges.Count;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                {
                    PedigreeCoupleEdge edgeA = model.coupleEdges[i];
                    PedigreeCoupleEdge edgeB = model.coupleEdges[j];

                    if (DetectPlanarity.EdgesCross(edgeA, edgeB, model.parameters.hideNonBloodRelatives))
                    {
                        CollapseEdge(edgeA,model);
                        CollapseEdge(edgeB,model);
                    }
                }
        }

        private static void CollapseEdge(PedigreeCoupleEdge edge,PedigreeModel model)
        {
            double avgX = (edge.u.point.x + edge.v.point.x) / 2;
            if (!model.pointsBeingDragged.Contains(edge.u.point))
                edge.u.point.x = avgX;
            if (!model.pointsBeingDragged.Contains(edge.v.point))
                edge.v.point.x = avgX;

            double avgY = (edge.u.point.y + edge.v.point.y) / 2;
            if (!model.pointsBeingDragged.Contains(edge.u.point))
                edge.u.point.y = avgY;
            if (!model.pointsBeingDragged.Contains(edge.v.point))
                edge.v.point.y = avgY;

        }
        private static void ShrinkEdge(PedigreeCoupleEdge edge, double strength)
        {
            PointWithVelocity a = edge.u.point;
            PointWithVelocity b = edge.v.point;

            double distance = b.x - a.x;

            double forceX = strength * distance;
            a.dx += forceX;
            b.dx -= forceX;
        }
    }
}

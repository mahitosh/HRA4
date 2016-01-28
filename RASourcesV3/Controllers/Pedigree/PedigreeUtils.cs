using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree
{
    class PedigreeUtils
    {
        public static void PullTowardX(double goalX, PointWithVelocity point, double strength, PedigreeModel model)
        {
            //double d = goalX - point.x;
            //double force = d * strength;
            //point.dx += force;
            if(!model.pointsBeingDragged.Contains(point))
                point.x = point.x * (1 - strength) + goalX * strength;
        }

        public static void PullTowardY(double goalY, PointWithVelocity point, double strength, PedigreeModel model)
        {
            //double distanceFromGoalY = goalY - point.y;
            //double force = distanceFromGoalY * strength;
            //point.dy += force;
            if (!model.pointsBeingDragged.Contains(point))
                point.y = point.y * (1 - strength) + goalY * strength;
        }

        /// <summary>
        /// Returns the individul under the given point, or null if none.
        /// </summary>
        public static PedigreeIndividual GetIndividualUnderPoint(PedigreeModel model, double x, double y)
        {

            x /= model.parameters.scale;
            y /= model.parameters.scale;


            x -= model.parameters.hOffset;
            y -= model.parameters.vOffset;

            //x /= model.parameters.scale;
            //y /= model.parameters.scale;



            foreach (PedigreeIndividual individual in model.individuals)
            {
                double dx = Math.Abs(x - individual.point.x);
                double dy = Math.Abs(y - individual.point.y);
                if (dx < model.parameters.individualSize/2 &&
                    dy < model.parameters.individualSize/2)
                    return individual;
            }
            return null;
        }

        internal static void PullTowardXWithForce(double goalX, PointWithVelocity point, double strength, PedigreeModel model)
        {
            double d = goalX - point.x;
            double force = d * strength;
            point.dx += force;
        }
        public static bool LinesIntersect(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            //formula from http://en.wikipedia.org/wiki/Line-line_intersection
            double pxNumerator = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            double pyNumerator = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
            double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (denominator == 0)
                return false;

            double px = pxNumerator / denominator;
            double py = pyNumerator / denominator;

            double aXMin = x1 < x2 ? x1 : x2;
            double aXMax = x1 > x2 ? x1 : x2;

            bool pFallsInAX = px > aXMin && px < aXMax;

            double aYMin = y1 < y2 ? y1 : y2;
            double aYMax = y1 > y2 ? y1 : y2;

            bool pFallsInAY = py > aYMin && py < aYMax;
            bool pFallsInA = pFallsInAX && pFallsInAY;

            double bXMin = x3 < x4 ? x3 : x4;
            double bXMax = x3 > x4 ? x3 : x4;

            bool pFallsInBX = px > bXMin && px < bXMax;

            double bYMin = y1 < y2 ? y1 : y2;
            double bYMax = y1 > y2 ? y1 : y2;

            bool pFallsInBY = py > bYMin && py < bYMax;
            bool pFallsInB = pFallsInBX && pFallsInBY;

            return pFallsInA && pFallsInB;
        }
        /// <summary>
        /// Derives a list of individuals in the given generational level, sorted by their X positions.
        /// The results get written to the "list" argument (after clearing it first).
        /// </summary>
        public static void GetIndividualsInGeneration(PedigreeModel model, int generation, List<PedigreeIndividual> list)
        {
            list.Clear();

            foreach (PedigreeCouple couple in model.couples)
            {
                if (couple.GenerationalLevel == generation)
                {
                    if (!list.Contains(couple.mother))
                    {
                        if (!model.parameters.hideNonBloodRelatives || couple.mother.bloodRelative)
                            list.Add(couple.mother);
                    }
                    if (!list.Contains(couple.father))
                    {
                        if (!model.parameters.hideNonBloodRelatives || couple.father.bloodRelative)
                            list.Add(couple.father);
                    }
                }
                else if (couple.GenerationalLevel == generation - 1)
                    foreach (PedigreeIndividual child in couple.children)
                        if (!list.Contains(child))
                        {
                            if (!model.parameters.hideNonBloodRelatives || child.bloodRelative)
                                list.Add(child);
                        }
            }

            //sort the list by x position
            list.Sort(delegate(PedigreeIndividual a, PedigreeIndividual b)
            {
                if (a == null)
                    return 1;

                if (b == null)
                    return -1;

                return a.point.x.CompareTo(b.point.x);
            });
        }

        public static List<PedigreeIndividual> GetIndividualsInGeneration(PedigreeModel model, int generation)
        {
            List<PedigreeIndividual> individuals = new List<PedigreeIndividual>();
            GetIndividualsInGeneration(model, generation, individuals);
            return individuals;
        }
    }
}

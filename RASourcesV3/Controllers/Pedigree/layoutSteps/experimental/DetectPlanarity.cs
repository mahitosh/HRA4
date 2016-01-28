using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    /// <summary>
    /// Responsible for setting the value of model.couplesGraphIsPlanar.
    /// </summary>
    class DetectPlanarity
    {
        public readonly LayoutStep step;
        public DetectPlanarity(PedigreeModel model)
        {
            step = delegate()
            {
                if (model.individuals.Count < 2)
                    return;

                model.couplesGraphIsPlanar = CouplesGraphIsPlanar(model);
            };
        }
        private static bool CouplesGraphIsPlanar(PedigreeModel model)
        {
            //TODO use http://en.wikipedia.org/wiki/Bentley%E2%80%93Ottmann_algorithm
            int n = model.coupleEdges.Count;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (EdgesCross(model.coupleEdges[i], model.coupleEdges[j], model.parameters.hideNonBloodRelatives))
                        return false;

            return true;
        }

        public static bool EdgesCross(PedigreeCoupleEdge a, PedigreeCoupleEdge b, bool hiding)
        {
            if (a == null || b == null)
                return false;

            if (hiding)
            {
                if (!a.u.mother.bloodRelative || !a.u.father.bloodRelative || !a.v.mother.bloodRelative || !a.v.father.bloodRelative)
                    return false;
                if (!b.u.mother.bloodRelative || !b.u.father.bloodRelative || !b.v.mother.bloodRelative || !b.v.father.bloodRelative)
                    return false;
            }
            bool aAndBShareAVertex = false;
            aAndBShareAVertex = aAndBShareAVertex || a.u == b.u;
            aAndBShareAVertex = aAndBShareAVertex || a.v == b.u;
            aAndBShareAVertex = aAndBShareAVertex || a.u == b.v;
            aAndBShareAVertex = aAndBShareAVertex || a.v == b.v;

            if (aAndBShareAVertex)
                return false;
            
            double x1 = a.u.point.x;
            double y1 = a.u.point.y;
            double x2 = a.v.point.x;
            double y2 = a.v.point.y;
            double x3 = b.u.point.x;
            double y3 = b.u.point.y;
            double x4 = b.v.point.x;
            double y4 = b.v.point.y;

            return PedigreeUtils.LinesIntersect(x1, y1, x2, y2, x3, y3, x4, y4);

        }

    }
}

using System;
using System.Collections.Generic;

using System.Text;

namespace RiskApps3.Model.PatientRecord.Pedigree
{
    /// <summary>
    /// An (x,y) point with (dx,dy) velocity.
    /// </summary>
    public class PointWithVelocity
    {
        public double x = 0;
        public double y = 0;
        public double dx = 0;
        public double dy = 0;

        internal void Set(PointWithVelocity point)
        {
            x = point.x;
            dx = point.dx;
            y = point.y;
            dy = point.dy;
        }
        internal void Set(double p_x, double p_y, double p_dx, double p_dy)
        {
            x = p_x;
            dx = p_dx;
            y = p_y;
            dy = p_dy;
        }
    }
}

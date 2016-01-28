using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RiskApps3.Utilities
{

    public class PolygonF
    {
        private PointF[] _pts;
        private float _minx = 0;
        private float _miny = 0;
        private float _maxx = 0;
        private float _maxy = 0;
        private float _xlength = 0;
        private float _ylength = 0;

        private PolygonF()
        {

        }

        /// <summary>
        /// Creates a new instance of a PolygonF based on the PointF[].
        /// </summary>
        /// <param name="pts">The array of PointF used to create the PolygonF.</param>
        public PolygonF(PointF[] pts)
        {
            Points = pts;
        }

        private PointF[] Points
        {
            get { return _pts; }
            set
            {
                _pts = value;
                _minx = _pts[0].X;
                _maxx = _pts[0].X;
                _miny = _pts[0].Y;
                _maxy = _pts[0].Y;

                foreach (PointF pt in _pts)
                {
                    if (pt.X < _minx)
                    {
                        _minx = pt.X;
                    }

                    if (pt.X > _maxx)
                    {
                        _maxx = pt.X;
                    }

                    if (pt.Y < _miny)
                    {
                        _miny = pt.Y;
                    }

                    if (pt.Y > _maxy)
                    {
                        _maxy = pt.Y;
                    }
                }

                _xlength = Math.Abs(_maxx - _minx);
                _ylength = Math.Abs(_maxy - _miny);
            }
        }

        /// <summary>
        /// The Rectangular Bounds of the Polygon.
        /// </summary>
        public RectangleF Bounds
        {
            get
            {
                return new RectangleF(_minx, _miny, _maxx - _minx, _maxy - _miny);
            }
        }

        /// <summary>
        /// The Minimum X coordinate value in the PointF collection.
        /// </summary>
        public float MinimumX
        {
            get { return _minx; }
        }

        /// <summary>
        /// The Maximum X coordinate value in the PointF collection.
        /// </summary>
        public float MaximumX
        {
            get { return _maxx; }
        }

        /// <summary>
        /// The Minimum Y coordinate value in the PointF collection.
        /// </summary>
        public float MinimumY
        {
            get { return _miny; }
        }

        /// <summary>
        /// The Maximum Y coordinate value in the PointF collection.
        /// </summary>
        public float MaximumY
        {
            get { return _maxy; }
        }

        /// <summary>
        /// The number of Points in the Polygon.
        /// </summary>
        public int NumberOfPoints
        {
            get { return _pts.Length; }
        }


        public bool IsInBounds(PointF pt)
        {
            return Bounds.Contains(pt);
        }


        public bool Contains(PointF pt)
        {
            bool isIn = false;

            if (IsInBounds(pt))
            {
                int i, j = 0;


                for (i = 0, j = NumberOfPoints - 1; i < NumberOfPoints; j = i++)
                {
                    if (
                        (
                         ((_pts[i].Y <= pt.Y) && (pt.Y < _pts[j].Y)) || ((_pts[j].Y <= pt.Y) && (pt.Y < _pts[i].Y))
                        ) &&
                        (pt.X < (_pts[j].X - _pts[i].X) * (pt.Y - _pts[i].Y) / (_pts[j].Y - _pts[i].Y) + _pts[i].X)
                       )
                    {
                        isIn = !isIn;
                    }
                }
            }

            return isIn;
        }

        /// <summary>
        /// Returns the PointF that represents the center of the Rectangular Bounds of the Polygon.
        /// </summary>
        public PointF CenterPointOfBounds
        {
            get
            {
                float x = _minx + (_xlength / 2);
                float y = _miny + (_ylength / 2);
                return new PointF(x, y);
            }
        }


        public PointF CenterPoint
        {
            get
            {
                PointF pt = CenterPointOfBounds;


                return pt;
            }
        }

        /// <summary>
        /// Calculates the Area of the Polygon.
        /// </summary>
        public decimal Area
        {
            get
            {
                decimal xy = 0M;
                for (int i = 0; i < _pts.Length; i++)
                {
                    PointF pt1;
                    PointF pt2;
                    if (i == _pts.Length - 1)
                    {
                        pt1 = _pts[i];
                        pt2 = _pts[0];
                    }
                    else
                    {
                        pt1 = _pts[i];
                        pt2 = _pts[i + 1];
                    }
                    xy += Convert.ToDecimal(pt1.X * pt2.Y);
                    xy -= Convert.ToDecimal(pt1.Y * pt2.X);
                }

                decimal area = Convert.ToDecimal(Math.Abs(xy)) * .5M;

                return area;
            }
        }
    }

    /// <summary>
    /// A Class representing a Polygon formed by an array of Point objects.
    /// Polygon objects are immutable.
    /// </summary>
    public class Polygon
    {
        private Point[] _pts;
        private int _minx = 0;
        private int _miny = 0;
        private int _maxx = 0;
        private int _maxy = 0;
        private int _xlength = 0;
        private int _ylength = 0;

        private Polygon()
        {

        }

        /// <summary>
        /// Creates a new instance of a Polygon based on the Point[].
        /// </summary>
        /// <param name="pts">The array of Point used to create the Polygon.</param>
        public Polygon(Point[] pts)
        {
            Points = pts;
        }

        private Point[] Points
        {
            get { return _pts; }
            set
            {
                _pts = value;
                _minx = _pts[0].X;
                _maxx = _pts[0].X;
                _miny = _pts[0].Y;
                _maxy = _pts[0].Y;

                foreach (Point pt in _pts)
                {
                    if (pt.X < _minx)
                    {
                        _minx = pt.X;
                    }

                    if (pt.X > _maxx)
                    {
                        _maxx = pt.X;
                    }

                    if (pt.Y < _miny)
                    {
                        _miny = pt.Y;
                    }

                    if (pt.Y > _maxy)
                    {
                        _maxy = pt.Y;
                    }
                }

                _xlength = Math.Abs(_maxx - _minx);
                _ylength = Math.Abs(_maxy - _miny);
            }
        }

        /// <summary>
        /// The Rectangular Bounds of the Polygon.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(_minx, _miny, _maxx - _minx, _maxy - _miny);
            }
        }

        /// <summary>
        /// The Minimum X coordinate value in the PointF collection.
        /// </summary>
        public int MinimumX
        {
            get { return _minx; }
        }

        /// <summary>
        /// The Maximum X coordinate value in the PointF collection.
        /// </summary>
        public int MaximumX
        {
            get { return _maxx; }
        }

        /// <summary>
        /// The Minimum Y coordinate value in the PointF collection.
        /// </summary>
        public int MinimumY
        {
            get { return _miny; }
        }

        /// <summary>
        /// The Maximum Y coordinate value in the PointF collection.
        /// </summary>
        public int MaximumY
        {
            get { return _maxy; }
        }

        /// <summary>
        /// The number of Points in the Polygon.
        /// </summary>
        public int NumberOfPoints
        {
            get { return _pts.Length; }
        }

        /// <summary>
        /// Compares the supplied point and determines whether or not it is inside the Rectangular Bounds
        /// of the Polygon.
        /// </summary>
        /// <param name="pt">The PointF to compare.</param>
        /// <returns>True if the PointF is within the Rectangular Bounds, False if it is not.</returns>
        public bool IsInBounds(Point pt)
        {
            return Bounds.Contains(pt);
        }


        public bool Contains(Point pt)
        {
            bool isIn = false;

            if (IsInBounds(pt))
            {
                int i, j = 0;


                for (i = 0, j = NumberOfPoints - 1; i < NumberOfPoints; j = i++)
                {
                    if (
                        (
                         ((_pts[i].Y <= pt.Y) && (pt.Y < _pts[j].Y)) || ((_pts[j].Y <= pt.Y) && (pt.Y < _pts[i].Y))
                        ) &&
                        (pt.X < (_pts[j].X - _pts[i].X) * (pt.Y - _pts[i].Y) / (_pts[j].Y - _pts[i].Y) + _pts[i].X)
                       )
                    {
                        isIn = !isIn;
                    }
                }
            }

            return isIn;
        }

        /// <summary>
        /// Returns the PointF that represents the center of the Rectangular Bounds of the Polygon.
        /// </summary>
        public Point CenterPointOfBounds
        {
            get
            {
                int x = _minx + (_xlength / 2);
                int y = _miny + (_ylength / 2);
                return new Point(x, y);
            }
        }

        /// <summary>
        /// NOT YET IMPLEMENTED.  Currently returns the same as CenterPointOfBounds.
        /// This is intended to be the Visual Center of the Polygon, and will be implemented
        /// once I can figure out how to calculate that Point.
        /// </summary>
        public Point CenterPoint
        {
            get
            {
                Point pt = CenterPointOfBounds;


                return pt;
            }
        }

        /// <summary>
        /// Calculates the Area of the Polygon.
        /// </summary>
        public decimal Area
        {
            get
            {
                decimal xy = 0M;
                for (int i = 0; i < _pts.Length; i++)
                {
                    Point pt1;
                    Point pt2;
                    if (i == _pts.Length - 1)
                    {
                        pt1 = _pts[i];
                        pt2 = _pts[0];
                    }
                    else
                    {
                        pt1 = _pts[i];
                        pt2 = _pts[i + 1];
                    }
                    xy += Convert.ToDecimal(pt1.X * pt2.Y);
                    xy -= Convert.ToDecimal(pt1.Y * pt2.X);
                }

                decimal area = Convert.ToDecimal(Math.Abs(xy)) * .5M;

                return area;
            }
        }
    }

}

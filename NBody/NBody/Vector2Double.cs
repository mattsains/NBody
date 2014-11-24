using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBody
{
    /// <summary>
    /// Partially rewritten Vector2 class for doubles instead of floats
    /// </summary>
    class Vector2Double
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2Double(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector2Double operator * (double a, Vector2Double b)
        {
            return new Vector2Double(b.X*a, b.Y*a);
        }
        public static Vector2Double operator *(Vector2Double b, double a)
        {
            return new Vector2Double(b.X * a, b.Y * a);
        }
        public static Vector2Double operator /(Vector2Double a, double b)
        {
            return new Vector2Double(a.X / b, a.Y / b);
        }
        public static Vector2Double operator + (Vector2Double a, Vector2Double b)
        {
            return new Vector2Double(a.X+b.X, a.Y+b.Y);
        }
        public static Vector2Double operator -(Vector2Double a, Vector2Double b)
        {
            return new Vector2Double(a.X - b.X, a.Y - b.Y);
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
    }
}

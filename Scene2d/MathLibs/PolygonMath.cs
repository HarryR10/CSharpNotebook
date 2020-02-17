using System.Collections.Generic;
using Scene2d.Exceptions;

namespace Scene2d.MathLibs
{
    public class PolygonMath
    {
        internal static void CheckIntersection(List<ScenePoint> Points, ScenePoint NewPoint)
        {
            var lastElement = Points[Points.Count - 1];

            double aX = NewPoint.X - lastElement.X;
            double aY = NewPoint.Y - lastElement.Y;

            for (int i = 1; i < Points.Count; i++)
            {
                double bX = lastElement.X - Points[i - 1].X;
                double bY = lastElement.Y - Points[i - 1].Y;

                double cX = lastElement.X - Points[i].X;
                double cY = lastElement.Y - Points[i].Y;

                //ax*by-bx*ay
                var firstMultiply = aX * bY - bX * aY;
                var secondMultiply = aX * cY - cX * aY;

                if ((firstMultiply < 0 & secondMultiply > 0) |
                    (firstMultiply > 0 & secondMultiply < 0))
                {
                    throw new BadPolygonPointException("intersection detected");
                }
            }

            //return true;
        }
    }
}

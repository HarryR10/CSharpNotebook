using System;
namespace Scene2d.MathLibs
{
    public class RotateMath
    {
        internal static ScenePoint PointAfterRotate(ScenePoint Center,
            ScenePoint Point, double Angle)
        {
            //X = (x — x0) *cos(alpha) — (y — y0) *sin(alpha) + x0;
            //Y = (x — x0) *sin(alpha) + (y — y0) *cos(alpha) + y0;

            double resultX;
            double resultY;
            Angle = Angle * Math.PI / 180;

            resultX = (Point.X - Center.X) * Math.Cos(Angle) -
                (Point.Y - Center.Y) * Math.Sin(Angle) + Center.X;


            resultY = (Point.X - Center.X) * Math.Sin(Angle) +
                (Point.Y - Center.Y) * Math.Cos(Angle) + Center.Y;

            return new ScenePoint(resultX, resultY);
        }
    }
}

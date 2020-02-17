using System.Linq;
using System.Collections.Generic;

namespace Scene2d.MathLibs
{
    public class RectangleMath
    {
        internal static ScenePoint GetCenterPoint(ScenePoint P1, ScenePoint P2)
        {
            var x = GetCenterDouble(P1.X, P2.X);
            var y = GetCenterDouble(P1.Y, P2.Y);
            return new ScenePoint(x, y);
        }

        // C1 & C2 = координаты
        private static double GetCenterDouble(double C1, double C2)
        {
            List<double> grades = new List<double> { C1, C2 };
            return grades.Average();
        }

        internal static SceneRectangle CommonCalcCircumscribingRectangle(
            ScenePoint[] Points)
        {
            var result = new SceneRectangle();

            ///////////////////////high X      //low X      //high Y     //low Y
            double[] commonXY = { Points[0].X, Points[0].X, Points[0].Y, Points[0].Y };

            foreach (var el in Points)
            {
                if (commonXY[0] > el.X)
                    commonXY[0] = el.X;
                if (commonXY[1] < el.X)
                    commonXY[1] = el.X;
                if (commonXY[2] > el.Y)
                    commonXY[2] = el.Y;
                if (commonXY[3] < el.Y)
                    commonXY[3] = el.Y;
            }

            result.Vertex1 = new ScenePoint(commonXY[1], commonXY[3]);
            result.Vertex2 = new ScenePoint(commonXY[0], commonXY[2]);
            return result;
        }
    }
}

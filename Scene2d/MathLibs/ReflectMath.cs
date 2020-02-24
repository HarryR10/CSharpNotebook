using System;

namespace Scene2d.MathLibs
{
    public class ReflectMath
    {
        public static void MakeReflection(ScenePoint[] Points,
            ScenePoint Center, ReflectOrientation Orientation)
        {
            if (Orientation == ReflectOrientation.Vertical)
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (Points[i].X != Center.X)
                    {
                        var distance = Math.Sqrt(Math.Pow(Points[i].X - Center.X, 2));
                        if (Points[i].X > Center.X)
                            Points[i].X = Points[i].X - 2 * distance;
                        else
                            Points[i].X = Points[i].X + 2 * distance;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Points.Length; i++)
                {
                    if (Points[i].Y != Center.Y)
                    {
                        var distance = Math.Sqrt(Math.Pow(Points[i].Y - Center.Y, 2));
                        if (Points[i].Y > Center.Y)
                            Points[i].Y = Points[i].Y - 2 * distance;
                        else
                            Points[i].Y = Points[i].Y + 2 * distance;
                    }
                }
            }
        }
    }
}
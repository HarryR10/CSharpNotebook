namespace Scene2d.Figures
{
    using System;
    using System.Drawing;
    using System.Linq;

    public class PolygonFigure : IFigure
    {
        private ScenePoint[] _points;

        public PolygonFigure(ScenePoint[] points)
        {
            _points = points;
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */

            using (var pen = new Pen(Color.DarkOrchid))
            {
                for (var i = 0; i < _points.Length; i++)
                {
                    ScenePoint firstPoint = _points[i];
                    ScenePoint secondPoint = i >= _points.Length - 1 ? _points.First() : _points[i + 1];

                    drawing.DrawLine(
                        pen,
                        (float)(firstPoint.X - origin.X),
                        (float)(firstPoint.Y - origin.Y),
                        (float)(secondPoint.X - origin.X),
                        (float)(secondPoint.Y - origin.Y));
                }
            }
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps current figure and has edges parallel to X and Y */

            var result = new SceneRectangle();

            ///////////////////////high X        //low X        //high Y     //low Y
            double[] commonXY = { _points[0].X, _points[0].X, _points[0].Y, _points[0].Y };

            foreach (var el in _points)
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

        public void Move(ScenePoint vector)
        {
            for(int i= 0; i < _points.Length; i++)
            {
                _points[i].X += vector.X;
                _points[i].Y += vector.Y;
            }
        }

        public void Rotate(double angle)
        {
            /* Should rotate current rectangle. Rotation origin point is the rectangle center.*/

            throw new NotImplementedException();
        }

        public void Reflect(ReflectOrientation orientation)
        {
            /* Should reflect the figure. Reflection edge is the rectangle axis of symmetry (horizontal or vertical). */

            throw new NotImplementedException();
        }

        public object Clone()
        {
            /* Should return new Rectangle with the same points as the current one. */

            return new PolygonFigure(_points);
            //throw new NotImplementedException();
        }
    }
}
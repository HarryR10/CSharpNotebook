namespace Scene2d.Figures
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Scene2d.MathLibs;

    public class PolygonFigure : IFigure
    {
        private ScenePoint[] _points;

        public PolygonFigure(ScenePoint[] points)
        {
            _points = points;
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
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
            return RectangleMath.CommonCalcCircumscribingRectangle(_points);
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
            var sceneRect = CalculateCircumscribingRectangle();
            var center = RectangleMath.GetCenterPoint(sceneRect.Vertex1, sceneRect.Vertex2);

            for(int i = 0; i < _points.Length; i++)
            {
                _points[i] = RotateMath.PointAfterRotate(center, _points[i], angle);
            }
        }

        public void Reflect(ReflectOrientation orientation)
        {
            var center = RectangleMath.GetCenterPoint(
                CalculateCircumscribingRectangle().Vertex1,
                CalculateCircumscribingRectangle().Vertex2);
            ReflectMath.MakeReflection(_points, center, orientation);
        }

        public object Clone()
        {
            return new PolygonFigure(_points);
        }
    }
}
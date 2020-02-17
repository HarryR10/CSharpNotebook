namespace Scene2d.Figures
{
    using System.Drawing;

    public class CircleFigure : IFigure
    {
        private ScenePoint _center;
        private double _radius;

        public CircleFigure(ScenePoint center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            /* Already implemented */
            using (var pen = new Pen(Color.Green))
            {
                drawing.DrawEllipse(
                    pen, 
                    (int) (_center.X - _radius - origin.X), 
                    (int) (_center.Y - _radius - origin.Y),
                    (int) (_radius * 2),
                    (int) (_radius * 2));
            }
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            var result = new SceneRectangle();
            result.Vertex1 = new ScenePoint(_center.X - _radius, _center.Y - _radius);
            result.Vertex2 = new ScenePoint(_center.X + _radius, _center.Y + _radius);
            return result;
        }

        public void Move(ScenePoint vector)
        {
            // is it?
            _center.X += vector.X;
            _center.Y += vector.Y;
        }

        public void Rotate(double angle)
        {
            // ???
        }

        public void Reflect(ReflectOrientation orientation)
        {
            // ???
        }

        public object Clone()
        {
            return new CircleFigure(_center, _radius);
        }

    }
}
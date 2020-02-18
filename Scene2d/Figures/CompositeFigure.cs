using System;
using System.Collections.Generic;
using System.Drawing;
using Scene2d.MathLibs;

namespace Scene2d.Figures
{
    public class CompositeFigure : ICompositeFigure
    {
        public IList<IFigure> ChildFigures 
        {
            get;
        }

        public CompositeFigure()
        {
            ChildFigures = new List<IFigure>();
        }

        public CompositeFigure(IList<IFigure> data)
        {
            ChildFigures = data;
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            var points = new List<ScenePoint>();
            foreach(var el in ChildFigures)
            {
                points.Add(el.CalculateCircumscribingRectangle().Vertex1);
                points.Add(el.CalculateCircumscribingRectangle().Vertex2);
            }
            var array = points.ToArray();
            return RectangleMath.CommonCalcCircumscribingRectangle(array);
        }

        public object Clone()
        {
            var toCopy = new List<IFigure>();
            foreach(var el in ChildFigures)
            {
                toCopy.Add((IFigure)el.Clone());
            }

            return new CompositeFigure(toCopy);
        }

        public void Move(ScenePoint vector)
        {
            foreach(var el in ChildFigures)
            {
                el.Move(vector);
            }
        }

        public void Reflect(ReflectOrientation orientation)
        {
            foreach (var el in ChildFigures)
            {
                el.Reflect(orientation);
            }
        }

        public void Rotate(double angle)
        {
            foreach (var el in ChildFigures)
            {
                el.Rotate(angle);
            }
        }

        // is unused
        void IFigure.Draw(ScenePoint origin, Graphics drawing)
        {
            throw new NotImplementedException();
        }
    }
}

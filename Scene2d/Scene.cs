namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scene2d.Figures;
    using Scene2d.Exceptions;

    //отладчик заходит в private readonly поля
    //в статические не заходит - они не являются полями конкретного экземпляра
    //не заходит и в поля с объявленной переменной, к-й не присвоили значение
    public class Scene
    {
        // possible implementation of figures storage
        // feel free to use your own!
        private readonly Dictionary<string, IFigure> _figures = new Dictionary<string, IFigure>();

        private readonly Dictionary<string, ICompositeFigure> _compositeFigures = new Dictionary<string, ICompositeFigure>();

        public void AddFigure(string name, IFigure figure)
        {
            foreach(var el in _figures)
            {
                if(el.Key == name)
                {
                    throw new NameAlreadyUsedException(name);
                }
            }

            _figures[name] = figure;
        }

        public SceneRectangle CalculateSceneCircumscribingRectangle()
        {
            /* Should calculate the rectangle that wraps the entire scene. */

            /* Already implemented but feel free to change according to figures storage strategy. */

            var allFigures = ListDrawableFigures()
                .Select(f => f.CalculateCircumscribingRectangle())
                .SelectMany(a => new[] { a.Vertex1, a.Vertex2 })
                .ToList();

            if (allFigures.Count == 0)
            {
                return new SceneRectangle();
            }

            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(allFigures.Min(p => p.X), allFigures.Min(p => p.Y)),
                Vertex2 = new ScenePoint(allFigures.Max(p => p.X), allFigures.Max(p => p.Y)),
            };
        }

        public void CreateCompositeFigure(string name, IEnumerable<string> childFigures)
        {
            foreach (var el in _compositeFigures)
            {
                if (el.Key == name)
                {
                    throw new NameAlreadyUsedException(name);
                }
            }
            /* todo: implement this */
        }

        public SceneRectangle CalculateCircumscribingRectangle(string name)
        {
            // todo: CalculateCircumscribingRectangle()
            throw new NotImplementedException();
        }

        public void MoveScene(ScenePoint vector)
        {
            // todo: MoveScene() 
        }

        public void Move(string name, ScenePoint vector)
        {
            if(_figures.TryGetValue(name, out IFigure finded))
            {
                finded.Move(vector);
                return;
            }

            if(_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            {
                findedComposite.Move(vector);
                return;
            }
            throw new BadNameException(name);
        }

        public void RotateScene(double angle)
        {
            // todo: RotateScene() 
        }

        public void Rotate(string name, double angle)
        {
            if (_figures.TryGetValue(name, out IFigure finded))
            {
                finded.Rotate(angle);
                return;
            }

            if (_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            {
                findedComposite.Rotate(angle);
                return;
            }
            throw new BadNameException(name);
        }

        public IEnumerable<IFigure> ListDrawableFigures()
        {
            /* Already implemented */

            return _figures
                .Values
                .Concat(_compositeFigures.SelectMany(x => x.Value.ChildFigures))
                .Distinct();
        }

        public void CopyScene(string copyName)
        {
            //todo: CopyScene
        }

        public void Copy(string originalName, string copyName)
        {
            if (_figures.TryGetValue(originalName, out IFigure finded))
            {
                var copy = finded.Clone();
                AddFigure(copyName, (IFigure)copy);
                return;
            }

            if (_compositeFigures.TryGetValue(originalName, out ICompositeFigure findedComposite))
            {
                //todo: test it
                var copy = findedComposite.Clone();
                CreateCompositeFigure(copyName, (IEnumerable<string>)copy);
                return;
            }
            throw new BadNameException(originalName);
        }

        public void DeleteScene()
        {
            //todo: DeleteScene
        }

        public void Delete(string name)
        {
            if (_figures.TryGetValue(name, out IFigure finded))
            {
                _figures.Remove(name);
                return;
            }

            if (_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            {
                _compositeFigures.Remove(name);
                return;
            }
            throw new BadNameException(name);
        }

        public void ReflectScene(ReflectOrientation reflectOrientation)
        {
            // todo: ReflectScene()
        }

        public void Reflect(string name, ReflectOrientation reflectOrientation)
        {
            if (_figures.TryGetValue(name, out IFigure finded))
            {
                finded.Reflect(reflectOrientation);
                return;
            }

            if (_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            {
                findedComposite.Reflect(reflectOrientation);
                return;
            }
            throw new BadNameException(name);
        }

        public string PrintCircumscribingRectangle(string name)
        {
            if (_figures.TryGetValue(name, out IFigure finded))
            {
                return finded.CalculateCircumscribingRectangle().Vertex1.ToString() + " " +
                    finded.CalculateCircumscribingRectangle().Vertex2.ToString();
            }

            if (_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            {
                return findedComposite.CalculateCircumscribingRectangle().Vertex1.ToString() + " " +
                    findedComposite.CalculateCircumscribingRectangle().Vertex2.ToString();
            }

            throw new BadNameException(name);
        }
    }
}

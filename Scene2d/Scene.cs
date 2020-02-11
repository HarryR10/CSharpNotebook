namespace Scene2d
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Scene2d.Figures;

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
            /* todo: check if the name is unique and throw if it's not */

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
            /* Should create a group figure. */
        }

        public SceneRectangle CalculateCircumscribingRectangle(string name)
        {
            /* Should calculate the rectangle that wraps figure or group 'name' */

            throw new NotImplementedException();
        }

        public void MoveScene(ScenePoint vector)
        {
            /* Should move all the figures and groups in the scene by 'vector' */
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

            /* todo: Should implementate some exceptions
        }

        public void RotateScene(double angle)
        {
            /* Should rotate all figures and groups in the scene by 'angle' */
        }

        public void Rotate(string name, double angle)
        {
            /* Should rotate figure or group 'name' by 'angle' */
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
            /* Should copy the entire scene to a group named 'copyName' */

            throw new NotImplementedException();
        }

        public void Copy(string originalName, string copyName)
        {
            /* Should copy figure or group 'originalName' to 'copyName' */

            throw new NotImplementedException();
        }

        public void DeleteScene()
        {
            //
        }

        public void Delete(string name)
        {
            //if (_figures.TryGetValue(name, out IFigure finded))
            //{
            //    finded.Move(vector);
            //    return;
            //}

            //if (_compositeFigures.TryGetValue(name, out ICompositeFigure findedComposite))
            //{
            //    findedComposite.Move(vector);
            //    return;
            //}

            /* todo: Should implementate some exceptions
        }

        public void ReflectScene(ReflectOrientation reflectOrientation)
        {
            /* Should reflect all the figures and groups in the scene */
        }

        public void Reflect(string name, ReflectOrientation reflectOrientation)
        {
            /* Should reflect figure or group 'name' */
        }
    }
}

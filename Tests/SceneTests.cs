using System.Collections.Generic;
using NUnit.Framework;
using Scene2d.Figures;
using Scene2d;
using Scene2d.Exceptions;

namespace Tests
{
    [TestFixture]
    public class SceneTests
    {
        // Для Reflect и Rotate уже написаны тесты в MathLibsTests
        [Test]
        public void Check_AddFigure_NameAlreadyUsedException()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 42;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            Assert.Throws<NameAlreadyUsedException>(() => testedScene.AddFigure("one", one));
        }

        [Test]
        public void Check_CreateCompositeFigure_NameAlreadyUsedException()
        {
            var testedScene = new Scene();
            var list = new List<string>();

            var center = new ScenePoint(0, 0);
            double radius = 42;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);
            list.Add("one");

            testedScene.CreateCompositeFigure("one", list);

            Assert.Throws<NameAlreadyUsedException>(() =>
            testedScene.CreateCompositeFigure("one", list));

        }

        [Test]
        public void Check_CreateCompositeFigure_BadNameException()
        {
            var testedScene = new Scene();
            var list = new List<string>();

            var center = new ScenePoint(0, 0);
            double radius = 42;
            IFigure one = new CircleFigure(center, radius);
            list.Add("one");

            Assert.Throws<BadNameException>(() =>
            testedScene.CreateCompositeFigure("one", list));
        }

        [Test]
        public void Check_MoveScene()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            var vertex1 = testedScene.CalculateSceneCircumscribingRectangle().Vertex1;
            var vertex2 = testedScene.CalculateSceneCircumscribingRectangle().Vertex2;

            testedScene.MoveScene(new ScenePoint(3, 0));

            Assert.That(testedScene.CalculateSceneCircumscribingRectangle().Vertex1.X, Is.EqualTo(
                vertex1.X + 3).Within(0.0001));
            Assert.That(testedScene.CalculateSceneCircumscribingRectangle().Vertex2.X, Is.EqualTo(
                vertex2.X + 3).Within(0.0001));

        }

        [Test]
        public void Check_Move()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            var vertex1 = one.CalculateCircumscribingRectangle().Vertex1;
            var vertex2 = one.CalculateCircumscribingRectangle().Vertex2;

            one.Move(new ScenePoint(3, 0));

            Assert.That(one.CalculateCircumscribingRectangle().Vertex1.X, Is.EqualTo(
                vertex1.X + 3).Within(0.0001));
            Assert.That(one.CalculateCircumscribingRectangle().Vertex2.X, Is.EqualTo(
                vertex2.X + 3).Within(0.0001));

        }

        [Test]
        public void Check_Copy_Сomposite()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            var list = new List<string>();
            list.Add("one");
            testedScene.CreateCompositeFigure("composite", list);
            
            testedScene.Copy("composite", "composite2");
            Assert.Throws<NameAlreadyUsedException>(() => testedScene.AddFigure("composite2_0", one));
        }

        [Test]
        public void Check_CopyScene_Сomposite()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            testedScene.CopyScene("composite");

            Assert.Throws<NameAlreadyUsedException>(() => testedScene.CopyScene("composite"));

        }

        [Test]
        public void Check_Delete_Figure()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            testedScene.Delete("one");
            Assert.Throws<BadNameException>(() => testedScene.Delete("one"));

        }

        [Test]
        public void Check_Delete_Сomposite()
        {
            var testedScene = new Scene();

            var center = new ScenePoint(0, 0);
            double radius = 88;
            IFigure one = new CircleFigure(center, radius);
            testedScene.AddFigure("one", one);

            var list = new List<string>();
            list.Add("one");
            testedScene.CreateCompositeFigure("composite", list);

            testedScene.Delete("composite");
            Assert.Throws<BadNameException>(() => testedScene.Delete("composite"));

        }

        [Test]
        public void Check_PrintCircumscribingRectangle_BadNameException()
        {
            var testedScene = new Scene();
            Assert.Throws<BadNameException>(() => testedScene.PrintCircumscribingRectangle("one"));

        }
    }
}
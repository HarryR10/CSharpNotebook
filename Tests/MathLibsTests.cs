using System.Collections.Generic;
using NUnit.Framework;
using Scene2d;
using Scene2d.MathLibs;
using Scene2d.Exceptions;

namespace Tests
{
    [TestFixture]
    public class MathLibsTests
    {
        [Test]
        public void Throw_Exeption_For_CheckIntersection()
        {
            List<ScenePoint> testedPoints = new List<ScenePoint>();

            var newPoint = new ScenePoint(-8, -6);

            testedPoints.Add(new ScenePoint(0, -10));
            testedPoints.Add(new ScenePoint(-10, 10));
            testedPoints.Add(new ScenePoint(10, 10));

            Assert.Throws<BadPolygonPointException>(() =>
            PolygonMath.CheckIntersection(testedPoints, newPoint));
        }

        [Test]
        public void Check_Point_Reflection_For_MakeReflection()
        {
            double[] xPoints = { 98, 87, 34, 21, 4.009, 1, 11111111111, 45897552 };
            double[] yPoints = { 349843, 344, 5.79000033333, 44343.33333333333333333333 };

            foreach(var x in xPoints)
            {
                foreach(var y in yPoints)
                {
                    var center = new ScenePoint(0, 0);
                    var point = new ScenePoint(x, y);
                    var points = new ScenePoint[1] { point };

                    ReflectMath.MakeReflection(points, center, ReflectOrientation.Vertical);
                    ReflectMath.MakeReflection(points, center, ReflectOrientation.Horizontal);
                    ReflectMath.MakeReflection(points, center, ReflectOrientation.Vertical);
                    ReflectMath.MakeReflection(points, center, ReflectOrientation.Horizontal);

                    //Assert.That(points[0], Is.EqualTo(point));
                    Assert.That(points[0].X, Is.EqualTo(x).Within(0.0001));
                    Assert.That(points[0].Y, Is.EqualTo(y).Within(0.0001));
                }
            }
        }

        [Test]
        public void Check_Point_Rotate_For_PointAfterRotate()
        {

            double[] xPoints = { 98, 87, 34, 21, 4.009, 1, 11111111111, 45897552 };
            double[] yPoints = { 349843, 344, 5.79000033333, 44343.33333333333333333333 };

            foreach (var x in xPoints)
            {
                foreach (var y in yPoints)
                {
                    var center = new ScenePoint(0, 0);
                    var point = new ScenePoint(x, y);

                    RotateMath.PointAfterRotate(center, point, 180);
                    RotateMath.PointAfterRotate(center, point, 90);
                    RotateMath.PointAfterRotate(center, point, 45);
                    RotateMath.PointAfterRotate(center, point, 22.5);
                    RotateMath.PointAfterRotate(center, point, 11.25);
                    RotateMath.PointAfterRotate(center, point, 5.625);
                    RotateMath.PointAfterRotate(center, point, 2.8125);
                    RotateMath.PointAfterRotate(center, point, 2.8125);


                    //Assert.That(points[0], Is.EqualTo(point));
                    Assert.That(point.X, Is.EqualTo(x).Within(0.0001));
                    Assert.That(point.Y, Is.EqualTo(y).Within(0.0001));
                }
            }
        }
    }
}
using System;

using ChipmunkX;
using ChipmunkX.Shapes;

namespace CruPhysicsUnitTest.UnitTests.ChipmunkX
{
    [Test]
    public class ShapeUnitTest
    {
        [Test]
        public void PolygonValidateTest()
        {
            Vector2D[] vertices1 = {
                new Vector2D(1, 1),
                new Vector2D(-1, 1),
                new Vector2D(-1, -1),
                new Vector2D(1, -1),
            };

            PolygonHelper.Validate(vertices1);

            Vector2D[] vertices2 = {
                new Vector2D(1, 1),
                new Vector2D(-1, -1),
                new Vector2D(-1, 1),
                new Vector2D(1, -1),
            };

            Assert.ExpectException<ArgumentException>(() => PolygonHelper.Validate(vertices2));
        }
    }
}
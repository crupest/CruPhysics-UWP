using System;
using ChipmunkX.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChipmunkX.Test.UnitTests
{
    [TestClass]
    public class ShapeUnitTest
    {
        [TestMethod]
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

            Assert.ThrowsException<ArgumentException>(() => PolygonHelper.Validate(vertices2));
        }
    }
}
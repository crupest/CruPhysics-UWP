using ChipmunkX.Shapes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChipmunkX.Test.UnitTests
{
    [TestClass]
    public class ChipmunkObjectUnitTest
    {
        [TestMethod]
        public void CreateAndDisposeTest()
        {
            Space space = new Space();
            Body body = new Body();
            Shape shape = new Circle(10.0);

            space.AddBody(body);
            body.AddShape(shape);

            space.Dispose();
            body.Dispose();
            shape.Dispose();
        }
    }
}

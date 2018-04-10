using ChipmunkX;
using ChipmunkX.Shapes;

namespace CruPhysicsUnitTest.UnitTests.ChipmunkX
{
    [Test]
    public class ChipmunkObjectUnitTest
    {
        [Test]
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

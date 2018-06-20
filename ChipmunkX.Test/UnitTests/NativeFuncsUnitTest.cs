using System;

using ChipmunkX.Native;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChipmunkX.Test.UnitTests
{
    [TestClass]
    public class BodyFuncsUnitTest
    {
        private const double delta = 0.0001;

        [TestMethod]
        public void BodyTest()
        {
            //New and free.
            var ptr = BodyFuncs.cpBodyNew(20.0, 10.0);
            BodyFuncs.cpBodyFree(BodyFuncs.cpBodyNewKinematic());
            BodyFuncs.cpBodyFree(BodyFuncs.cpBodyNewStatic());

            //Type
            BodyFuncs.cpBodySetType(ptr, cpBodyType.Dynamic);
            Assert.AreEqual(cpBodyType.Dynamic, BodyFuncs.cpBodyGetType(ptr));

            //Mass
            BodyFuncs.cpBodySetMass(ptr, 20.0);
            Assert.AreEqual(20.0, BodyFuncs.cpBodyGetMass(ptr), delta);

            //Moment
            BodyFuncs.cpBodySetMoment(ptr, 10.0);
            Assert.AreEqual(10.0, BodyFuncs.cpBodyGetMoment(ptr), delta);

            //Position
            BodyFuncs.cpBodySetPosition(ptr, new cpVect(20.0, 20.0));
            Assert.AreEqual(new cpVect(20.0, 20.0), BodyFuncs.cpBodyGetPosition(ptr));

            //Center of gravity.
            BodyFuncs.cpBodySetCenterOfGravity(ptr, new cpVect(20.0, 20.0));
            Assert.AreEqual(new cpVect(20.0, 20.0), BodyFuncs.cpBodyGetCenterOfGravity(ptr));

            //Velocity
            BodyFuncs.cpBodySetVelocity(ptr, new cpVect(20.0, 20.0));
            Assert.AreEqual(new cpVect(20.0, 20.0), BodyFuncs.cpBodyGetVelocity(ptr));

            //Force
            BodyFuncs.cpBodySetForce(ptr, new cpVect(20.0, 20.0));
            Assert.AreEqual(new cpVect(20.0, 20.0), BodyFuncs.cpBodyGetForce(ptr));

            //Angle
            BodyFuncs.cpBodySetAngle(ptr, 20.0);
            Assert.AreEqual(20.0, BodyFuncs.cpBodyGetAngle(ptr), delta);

            //Angular velocity
            BodyFuncs.cpBodySetAngularVelocity(ptr, 20.0);
            Assert.AreEqual(20.0, BodyFuncs.cpBodyGetAngularVelocity(ptr), delta);

            //Torque
            BodyFuncs.cpBodySetTorque(ptr, 20.0);
            Assert.AreEqual(20.0, BodyFuncs.cpBodyGetTorque(ptr), delta);

            //Coordinate conversion
            BodyFuncs.cpBodyLocalToWorld(ptr, new cpVect(0, 0));
            BodyFuncs.cpBodyWorldToLocal(ptr, new cpVect(0, 0));

            //Velocity conversion
            var vl = BodyFuncs.cpBodyGetVelocityAtLocalPoint(ptr, new cpVect(0, 0));
            var vw = BodyFuncs.cpBodyGetVelocityAtWorldPoint(ptr, new cpVect(0, 0));

            //Apply force and torque
            BodyFuncs.cpBodyApplyForceAtLocalPoint(ptr, new cpVect(10, 0), new cpVect(0, 0));
            BodyFuncs.cpBodyApplyForceAtWorldPoint(ptr, new cpVect(10, 0), new cpVect(0, 0));
            BodyFuncs.cpBodyApplyImpulseAtLocalPoint(ptr, new cpVect(10, 0), new cpVect(0, 0));
            BodyFuncs.cpBodyApplyImpulseAtWorldPoint(ptr, new cpVect(10, 0), new cpVect(0, 0));

            //Iterators
            BodyFuncs.cpBodyEachShape(ptr, (body, shape, data) => { }, IntPtr.Zero);
            BodyFuncs.cpBodyEachConstraint(ptr, (body, constraint, data) => { }, IntPtr.Zero);
            BodyFuncs.cpBodyEachArbiter(ptr, (body, arbiter, data) => { }, IntPtr.Zero);

            BodyFuncs.cpBodyFree(ptr);
        }
    }

    [TestClass]
    public class ShapeFuncsUnitTest
    {
        private IntPtr body;

        [TestInitialize]
        public void Initialize()
        {
            body = BodyFuncs.cpBodyNew(0, 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
            BodyFuncs.cpBodyFree(body);
        }

        [TestMethod]
        public void CircleShapeTest()
        {
            var circle = ShapeFuncs.cpCircleShapeNew(body, 10, new cpVect(10, 10));

            Assert.AreEqual(new cpVect(10, 10), ShapeFuncs.cpCircleShapeGetOffset(circle));
            Assert.AreEqual(10, ShapeFuncs.cpCircleShapeGetRadius(circle), 8);

            ShapeFuncs.cpShapeFree(circle);
        }

        [TestMethod]
        public void SegmentShapeTest()
        {
            var a = new cpVect(10, 10);
            var b = new cpVect(20, 20);
            var segment = ShapeFuncs.cpSegmentShapeNew(body, a, b, 1);

            Assert.AreEqual(a, ShapeFuncs.cpSegmentShapeGetA(segment));
            Assert.AreEqual(b, ShapeFuncs.cpSegmentShapeGetB(segment));
            ShapeFuncs.cpSegmentShapeGetNormal(segment);
            Assert.AreEqual(1, ShapeFuncs.cpSegmentShapeGetRadius(segment));

            ShapeFuncs.cpSegmentShapeSetNeighbors(segment, new cpVect(0, 0), new cpVect(30, 30));

            ShapeFuncs.cpShapeFree(segment);
        }

        [TestMethod]
        public void PolygonShapeTest()
        {
            var vertices = new cpVect[3] {
                    new cpVect(0,0),
                    new cpVect(10, 10),
                    new cpVect(10, 0)
                };

            var polygon = ShapeFuncs.cpPolyShapeNew(body, vertices.Length, vertices, cpTransform.Indentity, 0.0);
            var box1 = ShapeFuncs.cpBoxShapeNew(body, 10, 10, 0);
            var box2 = ShapeFuncs.cpBoxShapeNew2(body, new cpBB(0, 0, 10, 10), 0);

            Assert.AreEqual(3, ShapeFuncs.cpPolyShapeGetCount(polygon));

            //Don't compare with the vectors in vertices beacause the actual order of vertices may be changed.
            for (int i = 0; i < vertices.Length; i++)
                ShapeFuncs.cpPolyShapeGetVert(polygon, i);

            Assert.AreEqual(0.0, ShapeFuncs.cpPolyShapeGetRadius(polygon), 8);

            ShapeFuncs.cpShapeFree(polygon);
            ShapeFuncs.cpShapeFree(box1);
            ShapeFuncs.cpShapeFree(box2);
        }

        [TestMethod]
        public void ShapeTest()
        {
            var shape = ShapeFuncs.cpCircleShapeNew(body, 10, new cpVect(10, 10));

            //Elasticity
            ShapeFuncs.cpShapeSetElasticity(shape, 1);
            Assert.AreEqual(1, ShapeFuncs.cpShapeGetElasticity(shape));

            //Friction
            ShapeFuncs.cpShapeSetFriction(shape, 1);
            Assert.AreEqual(1, ShapeFuncs.cpShapeGetFriction(shape));

            //Surface velocity
            ShapeFuncs.cpShapeSetSurfaceVelocity(shape, new cpVect(1, 0));
            Assert.AreEqual(new cpVect(1, 0), ShapeFuncs.cpShapeGetSurfaceVelocity(shape));

            //Mass
            ShapeFuncs.cpShapeSetMass(shape, 10);
            Assert.AreEqual(10, ShapeFuncs.cpShapeGetMass(shape), 8);

            //Density
            ShapeFuncs.cpShapeSetDensity(shape, 1);
            Assert.AreEqual(1, ShapeFuncs.cpShapeGetDensity(shape), 8);

            ShapeFuncs.cpShapeFree(shape);
        }
    }

    [TestClass]
    public class SpaceFuncsUnitTest
    {
        [TestMethod]
        public void SpaceTest()
        {
            var ptr = SpaceFuncs.cpSpaceNew();

            //Gravity
            SpaceFuncs.cpSpaceSetGravity(ptr, new cpVect(0.0, -9.8));
            Assert.AreEqual(new cpVect(0.0, -9.8), SpaceFuncs.cpSpaceGetGravity(ptr));

            //Damping
            SpaceFuncs.cpSpaceSetDamping(ptr, 0.95);
            Assert.AreEqual(0.95, SpaceFuncs.cpSpaceGetDamping(ptr), 8);

            var body = BodyFuncs.cpBodyNewStatic();
            SpaceFuncs.cpSpaceAddBody(ptr, body);
            var shape = ShapeFuncs.cpBoxShapeNew(body, 10, 10, 0);
            SpaceFuncs.cpSpaceAddShape(ptr, shape);
            SpaceFuncs.cpSpaceRemoveShape(ptr, shape);
            SpaceFuncs.cpSpaceRemoveBody(ptr, body);

            //Step
            SpaceFuncs.cpSpaceStep(ptr, 1.0 / 60.0);

            SpaceFuncs.cpSpaceFree(ptr);
        }
    }
}

using System;

using Chipmunk.Native;

namespace CruPhysicsUnitTest
{
    namespace UnitTest
    {
        [Test]
        public class BodyFuncsUnitTest
        {
            private const int precision = 8;

            [Test]
            public void BodyTest()
            {
                //New and free.
                var ptr = BodyFuncs.cpBodyNew(20.0, 10.0);
                BodyFuncs.cpBodyFree(BodyFuncs.cpBodyNewStatic());

                //Type
                BodyFuncs.cpBodySetType(ptr, BodyType.Dynamic);
                Assert.Equal(BodyType.Dynamic, BodyFuncs.cpBodyGetType(ptr));

                //Mass
                BodyFuncs.cpBodySetMass(ptr, 20.0);
                Assert.Equal(20.0, BodyFuncs.cpBodyGetMass(ptr), precision);

                //Moment
                BodyFuncs.cpBodySetMoment(ptr, 10.0);
                Assert.Equal(10.0, BodyFuncs.cpBodyGetMoment(ptr), precision);

                //Position
                BodyFuncs.cpBodySetPosition(ptr, new Vector2D(20.0, 20.0));
                Assert.Equal(new Vector2D(20.0, 20.0), BodyFuncs.cpBodyGetPosition(ptr));

                //Center of gravity.
                BodyFuncs.cpBodySetCenterOfGravity(ptr, new Vector2D(20.0, 20.0));
                Assert.Equal(new Vector2D(20.0, 20.0), BodyFuncs.cpBodyGetCenterOfGravity(ptr));

                //Velocity
                BodyFuncs.cpBodySetVelocity(ptr, new Vector2D(20.0, 20.0));
                Assert.Equal(new Vector2D(20.0, 20.0), BodyFuncs.cpBodyGetVelocity(ptr));

                //Force
                BodyFuncs.cpBodySetForce(ptr, new Vector2D(20.0, 20.0));
                Assert.Equal(new Vector2D(20.0, 20.0), BodyFuncs.cpBodyGetForce(ptr));

                //Angle
                BodyFuncs.cpBodySetAngle(ptr, 20.0);
                Assert.Equal(20.0, BodyFuncs.cpBodyGetAngle(ptr), precision);

                //Angular velocity
                BodyFuncs.cpBodySetAngularVelocity(ptr, 20.0);
                Assert.Equal(20.0, BodyFuncs.cpBodyGetAngularVelocity(ptr), precision);

                //Torque
                BodyFuncs.cpBodySetTorque(ptr, 20.0);
                Assert.Equal(20.0, BodyFuncs.cpBodyGetTorque(ptr), precision);

                //Coordinate conversion
                BodyFuncs.cpBodyLocalToWorld(ptr, new Vector2D(0, 0));
                BodyFuncs.cpBodyWorldToLocal(ptr, new Vector2D(0, 0));

                //Velocity conversion
                var vl = BodyFuncs.cpBodyGetVelocityAtLocalPoint(ptr, new Vector2D(0, 0));
                var vw = BodyFuncs.cpBodyGetVelocityAtWorldPoint(ptr, new Vector2D(0, 0));

                //Apply force and torque
                BodyFuncs.cpBodyApplyForceAtLocalPoint(ptr, new Vector2D(10, 0), new Vector2D(0, 0));
                BodyFuncs.cpBodyApplyForceAtWorldPoint(ptr, new Vector2D(10, 0), new Vector2D(0, 0));
                BodyFuncs.cpBodyApplyImpulseAtLocalPoint(ptr, new Vector2D(10, 0), new Vector2D(0, 0));
                BodyFuncs.cpBodyApplyImpulseAtWorldPoint(ptr, new Vector2D(10, 0), new Vector2D(0, 0));

                //Iterators
                BodyFuncs.cpBodyEachShape(ptr, (body, shape, data) => { }, IntPtr.Zero);
                BodyFuncs.cpBodyEachConstraint(ptr, (body, constraint, data) => { }, IntPtr.Zero);
                BodyFuncs.cpBodyEachArbiter(ptr, (body, arbiter, data) => { }, IntPtr.Zero);

                BodyFuncs.cpBodyFree(ptr);
            }
        }

        [Test]
        public class ShapeFuncsUnitTest : IDisposable
        {
            private readonly IntPtr body;

            public ShapeFuncsUnitTest()
            {
                body = BodyFuncs.cpBodyNew(0, 0);
            }

            [Test]
            public void CircleShapeTest()
            {
                var circle = ShapeFuncs.cpCircleShapeNew(body, 10, new Vector2D(10, 10));

                Assert.Equal(new Vector2D(10, 10), ShapeFuncs.cpCircleShapeGetOffset(circle));
                Assert.Equal(10, ShapeFuncs.cpCircleShapeGetRadius(circle), 8);

                ShapeFuncs.cpShapeFree(circle);
            }

            [Test]
            public void SegmentShapeTest()
            {
                var a = new Vector2D(10, 10);
                var b = new Vector2D(20, 20);
                var segment = ShapeFuncs.cpSegmentShapeNew(body, a, b, 1);

                Assert.Equal(a, ShapeFuncs.cpSegmentShapeGetA(segment));
                Assert.Equal(b, ShapeFuncs.cpSegmentShapeGetB(segment));
                ShapeFuncs.cpSegmentShapeGetNormal(segment);
                Assert.Equal(1, ShapeFuncs.cpSegmentShapeGetRadius(segment));

                ShapeFuncs.cpSegmentShapeSetNeighbors(segment, new Vector2D(0, 0), new Vector2D(30, 30));

                ShapeFuncs.cpShapeFree(segment);
            }

            [Test]
            public void PolygonShapeTest()
            {
                var vertices = new Vector2D[3] {
                    new Vector2D(0,0),
                    new Vector2D(10, 10),
                    new Vector2D(10, 0)
                };

                var polygon = ShapeFuncs.cpPolyShapeNew(body, vertices.Length, vertices, Transform.Indentity, 0.0);
                var box1 = ShapeFuncs.cpBoxShapeNew(body, 10, 10, 0);
                var box2 = ShapeFuncs.cpBoxShapeNew2(body, new AABB(0, 0, 10, 10), 0);

                Assert.Equal(3, ShapeFuncs.cpPolyShapeGetCount(polygon));

                //Don't compare with the vectors in vertices beacause the actual order of vertices may be changed.
                for (int i = 0; i < vertices.Length; i++)
                    ShapeFuncs.cpPolyShapeGetVert(polygon, i);

                Assert.Equal(0.0, ShapeFuncs.cpPolyShapeGetRadius(polygon), 8);

                ShapeFuncs.cpShapeFree(polygon);
                ShapeFuncs.cpShapeFree(box1);
                ShapeFuncs.cpShapeFree(box2);
            }

            [Test]
            public void ShapeTest()
            {
                var shape = ShapeFuncs.cpCircleShapeNew(body, 10, new Vector2D(10, 10));

                //Elasticity
                ShapeFuncs.cpShapeSetElasticity(shape, 1);
                Assert.Equal(1, ShapeFuncs.cpShapeGetElasticity(shape));

                //Friction
                ShapeFuncs.cpShapeSetFriction(shape, 1);
                Assert.Equal(1, ShapeFuncs.cpShapeGetFriction(shape));

                //Surface velocity
                ShapeFuncs.cpShapeSetSurfaceVelocity(shape, new Vector2D(1, 0));
                Assert.Equal(new Vector2D(1, 0), ShapeFuncs.cpShapeGetSurfaceVelocity(shape));

                //Mass
                ShapeFuncs.cpShapeSetMass(shape, 10);
                Assert.Equal(10, ShapeFuncs.cpShapeGetMass(shape), 8);

                //Density
                ShapeFuncs.cpShapeSetDensity(shape, 1);
                Assert.Equal(1, ShapeFuncs.cpShapeGetDensity(shape), 8);

                ShapeFuncs.cpShapeFree(shape);
            }

            public void Dispose()
            {
                BodyFuncs.cpBodyFree(body);
            }
        }

        [Test]
        public class SpaceFuncsUnitTest
        {
            [Test]
            public void SpaceTest()
            {
                var ptr = SpaceFuncs.cpSpaceNew();

                //Gravity
                SpaceFuncs.cpSpaceSetGravity(ptr, new Vector2D(0.0, -9.8));
                Assert.Equal(new Vector2D(0.0, -9.8), SpaceFuncs.cpSpaceGetGravity(ptr));

                //Damping
                SpaceFuncs.cpSpaceSetDamping(ptr, 0.95);
                Assert.Equal(0.95, SpaceFuncs.cpSpaceGetDamping(ptr), 8);

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
}

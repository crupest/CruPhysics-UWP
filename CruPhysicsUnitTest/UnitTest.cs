using Chipmunk;

namespace CruPhysicsUnitTest
{
    namespace UnitTest
    {
        [Test]
        public class BodyUnitTest
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

                BodyFuncs.cpBodyFree(ptr);
            }
        }
    }
}

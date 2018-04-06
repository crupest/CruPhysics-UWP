using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace ChipmunkX
{
    namespace Native
    {
        using cpFloat = Double;

        public static class BaseInfo
        {
            public const string DllName = "Chipmunk.dll";
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct cpVect
        {
            public cpVect(cpFloat x, cpFloat y)
            {
                this.x = x;
                this.y = y;
            }

            public cpFloat x;
            public cpFloat y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct cpBB
        {
            public cpBB(double left, double bottom, double right, double top)
            {
                this.left = left;
                this.bottom = bottom;
                this.right = right;
                this.top = top;
            }

            public cpFloat left;
            public cpFloat bottom;
            public cpFloat right;
            public cpFloat top;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct cpTransform
        {
            public static readonly cpTransform Indentity = new cpTransform(1, 0, 0, 1, 0, 0);

            public cpTransform(cpFloat a, cpFloat b, cpFloat c, cpFloat d, cpFloat tx, cpFloat ty)
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.d = d;
                this.tx = tx;
                this.ty = ty;
            }

            public cpFloat a;
            public cpFloat b;
            public cpFloat c;
            public cpFloat d;
            public cpFloat tx;
            public cpFloat ty;
        }

        public enum BodyType : int
        {
            Dynamic,
            Kinematic,
            Static
        }

        public static class BodyFuncs
        {
            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpBodyNew(cpFloat m, cpFloat i);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyFree(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpBodyNewStatic();

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern BodyType cpBodyGetType(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetType(IntPtr body, BodyType type);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpBodyGetSpace(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpBodyGetMass(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetMass(IntPtr body, cpFloat mass);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpBodyGetMoment(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetMoment(IntPtr body, cpFloat moment);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetPosition(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetPosition(IntPtr body, cpVect position);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetCenterOfGravity(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetCenterOfGravity(IntPtr body, cpVect cog);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetVelocity(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetVelocity(IntPtr body, cpVect velocity);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetForce(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetForce(IntPtr body, cpVect force);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpBodyGetAngle(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetAngle(IntPtr body, cpFloat angle);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpBodyGetAngularVelocity(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetAngularVelocity(IntPtr body, cpFloat moment);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpBodyGetTorque(IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodySetTorque(IntPtr body, cpFloat torque);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyLocalToWorld(IntPtr body, cpVect v);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyWorldToLocal(IntPtr body, cpVect v);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetVelocityAtWorldPoint(IntPtr body, cpVect point);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpBodyGetVelocityAtLocalPoint(IntPtr body, cpVect point);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyApplyForceAtWorldPoint(IntPtr body, cpVect force, cpVect point);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyApplyForceAtLocalPoint(IntPtr body, cpVect force, cpVect point);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyApplyImpulseAtWorldPoint(IntPtr body, cpVect impulse, cpVect point);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyApplyImpulseAtLocalPoint(IntPtr body, cpVect impulse, cpVect point);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void cpBodyShapeIteratorFunc(IntPtr body, IntPtr shape, IntPtr data);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyEachShape(IntPtr body, cpBodyShapeIteratorFunc func, IntPtr data);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void cpBodyConstraintIteratorFunc(IntPtr body, IntPtr shape, IntPtr data);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyEachConstraint(IntPtr body, cpBodyConstraintIteratorFunc func, IntPtr data);

            [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            public delegate void cpBodyArbiterIteratorFunc(IntPtr body, IntPtr shape, IntPtr data);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpBodyEachArbiter(IntPtr body, cpBodyArbiterIteratorFunc func, IntPtr data);
        }

        public static class MomentAreaFuncs
        {
            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpMomentForCircle(cpFloat m, cpFloat r1, cpFloat r2, cpVect offset);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpMomentForSegment(cpFloat m, cpVect a, cpVect b, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpMomentForPoly(cpFloat m, int count, cpVect[] verts, cpVect offset, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpMomentForBox(cpFloat m, cpFloat width, cpFloat height);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpAreaForCircle(cpFloat r1, cpFloat r2);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpAreaForSegment(cpVect a, cpVect b, cpFloat r);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpAreaForPoly(int count, cpVect[] verts, cpFloat radius);
        }

        public static class ShapeFuncs
        {
            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpShapeGetElasticity(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeSetElasticity(IntPtr shape, cpFloat value);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpShapeGetFriction(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeSetFriction(IntPtr shape, cpFloat value);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpShapeGetSurfaceVelocity(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeSetSurfaceVelocity(IntPtr shape, cpVect value);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeFree(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpCircleShapeNew(IntPtr body, cpFloat radius, cpVect offset);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpCircleShapeGetOffset(IntPtr circleShape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpCircleShapeGetRadius(IntPtr circleShape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpSegmentShapeNew(IntPtr body, cpVect a, cpVect b, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpSegmentShapeGetA(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpSegmentShapeGetB(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpSegmentShapeGetNormal(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpSegmentShapeGetRadius(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSegmentShapeSetNeighbors(IntPtr shape, cpVect prev, cpVect next);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpPolyShapeNew(IntPtr body, int numVerts, cpVect[] verts, cpTransform transform, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern int cpPolyShapeGetCount(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpPolyShapeGetVert(IntPtr shape, int index);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpPolyShapeGetRadius(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpBoxShapeNew(IntPtr body, cpFloat width, cpFloat height, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpBoxShapeNew2(IntPtr body, cpBB box, cpFloat radius);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpShapeGetMass(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeSetMass(IntPtr shape, cpFloat mass);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpShapeGetDensity(IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpShapeSetDensity(IntPtr shape, cpFloat density);
        }

        public static class SpaceFuncs
        {
            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpVect cpSpaceGetGravity(IntPtr space);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceSetGravity(IntPtr space, cpVect value);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern cpFloat cpSpaceGetDamping(IntPtr space);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceSetDamping(IntPtr space, cpFloat value);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpSpaceNew();

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceFree(IntPtr space);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpSpaceAddShape(IntPtr space, IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpSpaceAddBody(IntPtr space, IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern IntPtr cpSpaceAddConstraint(IntPtr space, IntPtr constraint);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceRemoveShape(IntPtr space, IntPtr shape);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceRemoveBody(IntPtr space, IntPtr body);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceRemoveConstraint(IntPtr space, IntPtr constraint);

            [DllImport(BaseInfo.DllName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void cpSpaceStep(IntPtr space, cpFloat dt);
        }
    }
}

#pragma warning restore IDE1006 // Naming Styles

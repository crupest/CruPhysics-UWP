using System;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006 // Naming Styles

namespace Chipmunk
{
    using cpFloat = Double;
    using cpVect = Vector2D;

    public static class BaseInfo
    {
        public const string DllName = "Chipmunk.dll";
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2D
    {
        public Vector2D(cpFloat x, cpFloat y)
        {
            this.x = x;
            this.y = y;
        }

        public cpFloat x;
        public cpFloat y;
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
}

#pragma warning restore IDE1006 // Naming Styles

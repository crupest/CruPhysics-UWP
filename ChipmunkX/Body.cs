using System;
using System.Collections.Generic;

using ChipmunkX.Native;
using ChipmunkX.Shapes;

namespace ChipmunkX
{
    public enum BodyType
    {
        Dynamic,
        Kinematic,
        Static
    }

    public static class BodyTypeNativeHelper
    {
        public static BodyType FromNative(cpBodyType type)
        {
            switch (type)
            {
                case cpBodyType.Dynamic:
                    return BodyType.Dynamic;
                case cpBodyType.Kinematic:
                    return BodyType.Kinematic;
                case cpBodyType.Static:
                    return BodyType.Static;
                default:
                    throw new ArgumentException("Unknown native body type.", nameof(type));
            }
        }

        public static cpBodyType ToNative(BodyType type)
        {
            switch (type)
            {
                case BodyType.Dynamic:
                    return cpBodyType.Dynamic;
                case BodyType.Kinematic:
                    return cpBodyType.Kinematic;
                case BodyType.Static:
                    return cpBodyType.Static;
                default:
                    throw new ArgumentException("Unknown body type.", nameof(type));
            }
        }
    }

    /// <summary>
    /// A body consisting of shapes.
    /// </summary>
    public class Body : ChipmunkObject
    {
        internal Space _space = null;
        private List<Shape> _shapes = new List<Shape>();


        /// <summary>
        /// Create a body with no shape and specified type.
        /// </summary>
        /// <param name="bodyType">Body type.</param>
        public Body(BodyType bodyType = BodyType.Dynamic)
        {
            switch (bodyType)
            {
                case BodyType.Dynamic:
                    _ptr = BodyFuncs.cpBodyNew(0.0, 0.0);
                    break;
                case BodyType.Kinematic:
                    _ptr = BodyFuncs.cpBodyNewKinematic();
                    break;
                case BodyType.Static:
                    _ptr = BodyFuncs.cpBodyNewStatic();
                    break;
                default:
                    throw new ArgumentException("Unknown body type.", nameof(bodyType));
            }
        }


        /// <summary>
        /// Get or set the body type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is not valid.
        /// </exception>
        public BodyType BodyType
        {
            get
            {
                CheckValidation();
                return BodyTypeNativeHelper.FromNative(BodyFuncs.cpBodyGetType(_ptr));
            }
            set
            {
                CheckValidation();
                BodyFuncs.cpBodySetType(_ptr, BodyTypeNativeHelper.ToNative(value));
            }
        }

        /// <summary>
        /// Get the space that the body is attached to.
        /// Return null if the body hasn't been attached to any space.
        /// </summary>
        public Space Space => _space;


        /// <summary>
        /// Get the list of shapes.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is not valid.
        /// </exception>
        public IReadOnlyList<Shape> Shapes
        {
            get
            {
                CheckValidation();
                return _shapes;
            }
        }


        public double Mass
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetMass(_ptr);
            }
        }


        public Vector2D CenterOfGravity
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetCenterOfGravity(_ptr);
            }
        }


        public double Moment
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetMoment(_ptr);
            }
        }


        public Vector2D Position
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetPosition(_ptr);
            }
            set
            {
                CheckValidation();
                BodyFuncs.cpBodySetPosition(_ptr, value);
            }
        }


        public double Angle
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetAngle(_ptr);
            }
            set
            {
                CheckValidation();
                BodyFuncs.cpBodySetAngle(_ptr, value);
            }
        }


        public Vector2D Force
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetForce(_ptr);
            }
        }


        public double Torque
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetTorque(_ptr);
            }
        }


        public Vector2D Velocity
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetVelocity(_ptr);
            }
        }


        public double AngularVelocity
        {
            get
            {
                CheckValidation();
                return BodyFuncs.cpBodyGetAngularVelocity(_ptr);
            }
        }


        public Vector2D WorldToLocal(Vector2D point)
        {
            CheckValidation();
            return BodyFuncs.cpBodyWorldToLocal(_ptr, point);
        }


        public Vector2D LocalToWorld(Vector2D point)
        {
            CheckValidation();
            return BodyFuncs.cpBodyLocalToWorld(_ptr, point);
        }

        /// <summary>
        /// Add a shape to the body.
        /// </summary>
        /// <param name="shape">The shape to add.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the shape is already attached to a body.
        /// </exception>
        public void AddShape(Shape shape)
        {
            if (shape.Body != null)
                throw new InvalidOperationException(
                    "Shape is already attached to a body.");

            _shapes.Add(shape);
            shape.OnAttachToBody(this);

            if (Space != null)
                SpaceFuncs.cpSpaceAddShape(Space._ptr, shape._ptr);
        }


        /// <summary>
        /// Remove a shape from the body.
        /// </summary>
        /// <param name="shape">The shape to remove.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the shape is not attached to this body.
        /// </exception>
        public void RemoveShape(Shape shape)
        {
            if (shape.Body != this)
                throw new InvalidOperationException(
                    "The shape is not attached to this body.");

            if (Space != null)
                SpaceFuncs.cpSpaceRemoveShape(Space._ptr, shape._ptr);

            shape.OnDetachFromBody(this);

            _shapes.Remove(shape);
        }


        /// <summary>
        /// Remove all shapes from the body.
        /// </summary>
        public void ClearShape()
        {
            foreach (var shape in _shapes)
            {
                if (Space != null)
                    SpaceFuncs.cpSpaceRemoveShape(Space._ptr, shape._ptr);

                shape.OnDetachFromBody(this);
            }

            _shapes.Clear();
        }

        protected override sealed void DoDispose()
        {
            ClearShape();

            if (Space != null)
                Space.RemoveBody(this);

            BodyFuncs.cpBodyFree(_ptr);
        }
    }
}

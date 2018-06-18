using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ChipmunkX.Native;

namespace ChipmunkX.Shapes
{
    public enum ShapeType
    {
        Circle,
        Segment,
        Polygon
    }


    /// <summary>
    /// Represents a shape that can be part of body. Shape is immutable.
    /// </summary>
    public abstract class Shape : ChipmunkObject
    {
        internal Body _body = null;
        private double _elasticity = 0.0;
        private double _friction = 0.0;
        private Vector2D _surfaceVelocity = new Vector2D();
        private double _mass = 0.0;

        protected Shape(ShapeType shapeType)
        {
            ShapeType = shapeType;
        }


        /// <summary>
        /// Get the type of the shape.
        /// </summary>
        public ShapeType ShapeType { get; }


        /// <summary>
        /// Get the area of the shape.
        /// </summary>
        public abstract double Area { get; }


        /// <summary>
        /// Get or set the elasticity.
        /// </summary>
        public double Elasticity
        {
            get => _elasticity;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Elasticity can't be less than 0.");
                _elasticity = value;
                if (IsValid)
                    ShapeFuncs.cpShapeSetElasticity(_ptr, value);
            }
        }

        /// <summary>
        /// Get or set the friciton.
        /// </summary>
        public double Friction
        {
            get => _friction;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Friction can't be less than 0.");
                _friction = value;
                if (IsValid)
                    ShapeFuncs.cpShapeSetFriction(_ptr, value);
            }
        }


        /// <summary>
        /// Get or set the surface velocity.
        /// </summary>
        public Vector2D SurfaceVelocity
        {
            get => _surfaceVelocity;
            set
            {
                _surfaceVelocity = value;
                if (IsValid)
                    ShapeFuncs.cpShapeSetSurfaceVelocity(_ptr, value);
            }
        }

        /// <summary>
        /// Get or set the mass.
        /// </summary>
        public double Mass
        {
            get => _mass;
            set
            {
                if (value < 0.0)
                    throw new ArgumentOutOfRangeException(
                        nameof(value), value, "Mass can't be less than 0.");
                _mass = value;
                if (IsValid)
                {
                    Debug.WriteLineIf(Body.BodyType != BodyType.Dynamic,
                        "The body that the shape is attached to is" +
                        " not of type dynamic. You can't set the mass" +
                        " or intensity of the shape.");
                    ShapeFuncs.cpShapeSetMass(_ptr, value);
                }
            }
        }

        /// <summary>
        /// Get or set the intensity.
        /// </summary>
        public double Intensity
        {
            get => Mass / Area;
            set => Mass = Area * Intensity;
        }


        /// <summary>
        /// Get body that the shape is attached to.
        /// Return null if the shape hasn't been attached to any body.
        /// </summary>
        public Body Body => _body;


        /// <summary>
        /// Map the properties of the shape to underlying object.
        /// This can be called when a real shape is created (when
        /// <see cref="ChipmunkObject._ptr"/> is set).
        /// </summary>
        private void MapProperties()
        {
            Debug.Assert(IsValid, "Can't set properties when shape is not valid.");
            ShapeFuncs.cpShapeSetElasticity(_ptr, _elasticity);
            ShapeFuncs.cpShapeSetFriction(_ptr, _friction);
            ShapeFuncs.cpShapeSetSurfaceVelocity(_ptr, _surfaceVelocity);
            ShapeFuncs.cpShapeSetMass(_ptr, _mass);
        }

        /// <summary>
        /// Create unmanaged resources. Called when the shape
        /// is attached to a body.
        /// </summary>
        /// <param name="body"></param>
        protected abstract void Create(Body body);


        /// <summary>
        /// Called when attached to a body. It will create
        /// underlying unmanaged resources and map saved properties
        /// to them.
        /// </summary>
        /// <param name="body">The body that the shape will be attached to.</param>
        internal void OnAttachToBody(Body body)
        {
            Debug.Assert(!IsValid);

            if (body.BodyType != BodyType.Dynamic && Mass != 0.0)
                throw new InvalidOperationException("Can't add a shape with" +
                    " non-zero mass to non-static body.");

            _body = body;
            Create(body);
            MapProperties();
        }


        /// <summary>
        /// Called when detached to a body. It will free underlying
        /// unmanaged resources.
        /// </summary>
        /// <param name="body">
        /// The body that the shape will be detached from.
        /// </param>
        internal void OnDetachFromBody(Body body)
        {
            Debug.Assert(IsValid);

            ShapeFuncs.cpShapeFree(_ptr);
            _ptr = IntPtr.Zero;
            _body = null;
        }


        protected override sealed void DoDispose()
        {
            // If the _ptr is not zero, then the shape must be attached
            // to a body. Just detach from the body and the unmanaged
            // resources will be freed.
            Body.RemoveShape(this);
        }
    }


    /// <summary>
    /// Represents a circle shape.
    /// </summary>
    public class Circle : Shape
    {
        private readonly double _area;

        /// <summary>
        /// Create a circle.
        /// </summary>
        /// <param name="center">The center of circle.</param>
        /// <param name="radius">The radius of circle.</param>
        public Circle(Vector2D center, double radius)
            : base(ShapeType.Circle)
        {
            if (radius <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Radius must be greater than 0.");
            Center = center;
            Radius = radius;
            _area = MomentAreaFuncs.cpAreaForCircle(0.0, radius);
        }

        /// <summary>
        /// Create a circle with the center at (0, 0).
        /// </summary>
        /// <param name="radius">The radius of circle.</param>
        public Circle(double radius)
            : this(Vector2D.Zero, radius)
        {

        }


        /// <summary>
        /// Get the center.
        /// </summary>
        public Vector2D Center { get; }


        /// <summary>
        /// Get the radius.
        /// </summary>
        public double Radius { get; }


        /// <summary>
        /// Get the area.
        /// </summary>
        public override double Area => _area;


        protected override sealed void Create(Body body)
        {
            _ptr = ShapeFuncs.cpCircleShapeNew(body._ptr, Radius, Center);
        }
    }

    public static class PolygonHelper
    {
        /// <summary>
        /// Check whether the polygon of vertices is convex and CCW winding.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when vertices is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when the vertices don't satisfy the requirements.
        /// Get more message from exception.
        /// </exception>
        /// <remarks>
        /// Copied and modified from dyn4j.
        /// </remarks>
        public static void Validate(Vector2D[] vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException(nameof(vertices));

            int size = vertices.Length;
            if (size < 3)
                throw new ArgumentException("The number of vertices is less than 3.");

            // check for convex
            double area = 0.0;
            int sign = 0;
            for (int i = 0; i < size; i++)
            {
                Vector2D p0 = (i - 1 < 0) ? vertices[size - 1] : vertices[i - 1];
                Vector2D p1 = vertices[i];
                Vector2D p2 = (i + 1 == size) ? vertices[0] : vertices[i + 1];
                // check for coincident vertices
                if (p1 == p2)
                    throw new ArgumentException("Coincident vertices.");
                // check the cross product for CCW winding
                double cross = Vector2D.Cross(p1 - p0, p2 - p1);
                int tsign = Math.Sign(cross);
                area += cross;
                // check for convexity
                if (sign != 0 && tsign != sign)
                    throw new ArgumentException("Non-convex.");
                sign = tsign;
            }
            // check for CCW
            if (area < 0.0)
                throw new ArgumentException("Invalid winding.");
        }
    }

    public class Polygon : Shape
    {
        private readonly double _area;

        public Polygon(params Vector2D[] vertices)
            : this(0.0, vertices)
        {

        }

        public Polygon(double radius, params Vector2D[] vertices)
            : base(ShapeType.Polygon)
        {
            PolygonHelper.Validate(vertices);
            Vertices = vertices.ToList();
            Radius = radius;
            _area = MomentAreaFuncs.cpAreaForPoly(Vertices.Count,
                (from vertex in Vertices select (cpVect)vertex).ToArray(), Radius);
        }

        public IReadOnlyList<Vector2D> Vertices { get; }

        public double Radius { get; }

        public override double Area => _area;

        protected override void Create(Body body)
        {
            _ptr = ShapeFuncs.cpPolyShapeNewRaw(body._ptr, Vertices.Count,
                (from vertex in Vertices select (cpVect)vertex).ToArray(), Radius);
        }
    }

    class Segment : Shape
    {
        private readonly double _area;

        public Segment(Vector2D vertex1, Vector2D vertex2, double radius)
            : base(ShapeType.Segment)
        {
            if (vertex1 == vertex2)
            if (radius <= 0.0)
                throw new ArgumentOutOfRangeException(nameof(radius), radius, "Radius must be greater than 0.");

            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Length = Vector2D.Distance(vertex1, vertex2);
            Radius = radius;
            _area = MomentAreaFuncs.cpAreaForSegment(vertex1, vertex2, radius);
        }

        public Vector2D Vertex1 { get; }
        public Vector2D Vertex2 { get; }
        public double Length { get; }
        public double Radius { get; }
        public override double Area => _area;

        protected override void Create(Body body)
        {
            _ptr = ShapeFuncs.cpSegmentShapeNew(body._ptr, Vertex1, Vertex2, Radius);
        }
    }
}

using System;
using System.Collections.Generic;

using ChipmunkX.Native;

namespace ChipmunkX
{
    /// <summary>
    /// A world hold bodies, shapes and constraints.
    /// </summary>
    public class Space : ChipmunkObject
    {
        private List<Body> _bodies = new List<Body>();

        /// <summary>
        /// Create a empty space with gravity of (0, 0) and damping of 1.0.
        /// </summary>
        public Space()
        {
            _ptr = SpaceFuncs.cpSpaceNew();
        }


        /// <summary>
        /// Get or set the gravity of the space.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the space has been disposed of.
        /// </exception>
        public Vector2D Gravity
        {
            get
            {
                CheckValidation();
                return SpaceFuncs.cpSpaceGetGravity(_ptr);
            }
            set
            {
                CheckValidation();
                SpaceFuncs.cpSpaceSetGravity(_ptr, value);
            }
        }


        /// <summary>
        /// Get or set the damping of the space.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the space has been disposed of.
        /// </exception>
        public double Damping
        {
            get
            {
                CheckValidation();
                return SpaceFuncs.cpSpaceGetDamping(_ptr);
            }
            set
            {
                CheckValidation();
                SpaceFuncs.cpSpaceSetDamping(_ptr, value);
            }
        }


        /// <summary>
        /// Get a readonly bodies list.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the space has been disposed of.
        /// </exception>
        public IReadOnlyList<Body> Bodies
        {
            get
            {
                CheckValidation();
                return _bodies;
            }
        }


        /// <summary>
        /// Add a body to the space.
        /// </summary>
        /// <param name="body">The body to add.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is already attached to a space.
        /// </exception>
        public void AddBody(Body body)
        {
            if (body.Space != null)
                throw new InvalidOperationException(
                    "The body is already attached to a space.");

            _bodies.Add(body);

            SpaceFuncs.cpSpaceAddBody(_ptr, body._ptr);
            foreach (var shape in body.Shapes)
                SpaceFuncs.cpSpaceAddShape(_ptr, shape._ptr);

            body.OnAttachFromSpace(this);
        }


        /// <summary>
        /// Remove a body from the space.
        /// </summary>
        /// <param name="body">The body to remove.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the body is not attached to this space.
        /// </exception>
        public void RemoveBody(Body body)
        {
            if (body.Space != this)
                throw new InvalidOperationException(
                    "The body is not attached to this space.");

            foreach (var shape in body.Shapes)
                SpaceFuncs.cpSpaceRemoveShape(_ptr, shape._ptr);
            SpaceFuncs.cpSpaceRemoveBody(_ptr, body._ptr);

            body.OnDetachFromSpace(this);

            _bodies.Remove(body);
        }

        /// <summary>
        /// Remove all bodies from the space.
        /// </summary>
        public void ClearBody()
        {
            foreach (var body in _bodies)
            {
                foreach (var shape in body.Shapes)
                    SpaceFuncs.cpSpaceRemoveShape(_ptr, shape._ptr);
                SpaceFuncs.cpSpaceRemoveBody(_ptr, body._ptr);

                body.OnDetachFromSpace(this);
            }

            _bodies.Clear();
        }

        protected override void DoDispose()
        {
            ClearBody();
            SpaceFuncs.cpSpaceFree(_ptr);
        }
    }
}

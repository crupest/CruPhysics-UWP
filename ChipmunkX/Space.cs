﻿using System;
using System.Collections.Generic;

using ChipmunkX.Native;

namespace ChipmunkX
{
    /// <summary>
    /// A world hold bodies, shapes and constraints.
    /// </summary>
    /// <remarks>
    /// It is disposable and the finalizer will automatically call <c>dispose</c>.
    /// </remarks>
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
        /// <exception cref="ObjectDisposedException">
        /// Throw if the space has been disposed of.
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
        /// <exception cref="ObjectDisposedException">
        /// Throw if the space has been disposed of.
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
        /// <exception cref="ObjectDisposedException">
        /// Throw if the space has been disposed of.
        /// </exception>
        public IReadOnlyList<Body> Bodies
        {
            get
            {
                CheckValidation();
                return _bodies;
            }
        }

        protected override void DoDispose()
        {
            throw new NotImplementedException();
        }
    }
}
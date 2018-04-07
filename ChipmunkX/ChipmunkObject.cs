using System;

namespace ChipmunkX
{
    /// <summary>
    /// Represents an object holding a native pointer in Chipmunk.
    /// </summary>
    /// <remarks>
    /// Hold an <see cref="IntPtr"/> of unmanaged resource. This
    /// class implements <see cref="IDisposable"/>. So call
    /// <see cref="Dispose"/> to dispose of it. Subclass must set
    /// the <see cref="_ptr"/> in constructor and override the
    /// <see cref="DoDispose"/> to do the real disposing work.
    /// </remarks>
    public abstract class ChipmunkObject : IDisposable
    {
        internal protected IntPtr _ptr;


        ~ChipmunkObject()
        {
            _DoDispose();
        }

        /// <summary>
        /// Check whether the object is valid and throw when it is not valid.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the object is not valid.
        /// </exception>
        protected void CheckValidation()
        {
            if (!IsValid)
                throw new InvalidOperationException("The chipmunk object is not valid,"
                  +  " which means it might have been disposed of or even haven't been created.");
        }


        /// <summary>
        /// Called when being disposed and the <see cref="_ptr"/> is not zero.
        /// </summary>
        /// <remarks>
        /// The caller will set the <see cref="_ptr"/> to zero automatically so
        /// you don't have to reset it explicitly in this function.
        /// </remarks>
        protected abstract void DoDispose();


        /// <summary>
        /// Call <see cref="DoDispose"/> and
        /// set <see cref="_ptr"/> to <see cref="IntPtr.Zero"/>
        /// when <see cref="_ptr"/> is not zero.
        /// </summary>
        private void _DoDispose()
        {
            if (_ptr != IntPtr.Zero)
            {
                DoDispose();
                _ptr = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Dipose of the object and free the unmanaged resources if there exist any.
        /// </summary>
        /// <remarks>
        /// It is safe to call <see cref="Dispose"/> more than one times.
        /// </remarks>
        public void Dispose()
        {
            _DoDispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// True if the object is valid. You can perform operation on it safely.
        /// </summary>
        public bool IsValid => _ptr != IntPtr.Zero;

        /// <summary>
        /// Get the raw pointer of the unmanaged resource.
        /// Return zero if the object is invalid.
        /// </summary>
        public IntPtr RawHandle => _ptr;
    }
}

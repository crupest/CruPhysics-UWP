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
        protected IntPtr _ptr;


        ~ChipmunkObject()
        {
            DoDispose();
        }

        /// <summary>
        /// Check whether the object has been disposed of.
        /// If it is, then an exception will be thrown.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Thrown when the object has been disposed.
        /// </exception>
        protected void CheckValidation()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(Space));
        }


        /// <summary>
        /// Called when being disposed and the <see cref="_ptr"/> is not zero.
        /// </summary>
        /// <remarks>
        /// The caller will set the <see cref="_ptr"/> to zero automatically so
        /// you don't have to reset it in this function.
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
        /// Dipose of the object and free the unmanaged resources.
        /// </summary>
        /// <remarks>
        /// It is safe to call <c>Dispose</c> more than one times.
        /// </remarks>
        public void Dispose()
        {
            DoDispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// True if the space has been diposed of and it is no longer valid. 
        /// </summary>
        public bool IsDisposed => _ptr == IntPtr.Zero;

        /// <summary>
        /// Get the raw pointer of the unmanaged resource.
        /// Return zero if the object has been disposed of.
        /// </summary>
        public IntPtr RawHandle => _ptr;
    }
}

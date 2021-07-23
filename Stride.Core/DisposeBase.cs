using Stride.Core.ReferenceCounting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Stride.Core
{
    public abstract class DisposeBase : IDisposable, IReferencable
    {
        private int counter = 1;

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (!IsDisposed)
            {
                this.ReleaseInternal();
            }
        }

        protected virtual void Destroy()
        {

        }

        int IReferencable.ReferenceCount => counter;

        int IReferencable.AddReference()
        {
            OnAddReference();

            var newCounter = Interlocked.Increment(ref counter);
            if (newCounter <= 1)
                throw new InvalidOperationException("AddReferenceError!");
            return newCounter;
        }

        int IReferencable.Release()
        {
            OnReleaseReference();

            var newCounter = Interlocked.Decrement(ref counter);
            if (newCounter == 0)
            {
                Destroy();
                IsDisposed = true;
            }else if (newCounter <0)
            {
                throw new InvalidOperationException("ReleaseReferenceError!");
            }

            return newCounter;
        }

        protected void OnAddReference()
        {
            
        }

        protected virtual void OnReleaseReference()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class DisposableWrapper<T> : IDisposable where T : IDisposable, new()
    {
        T disposable;
        int referenceCount;

        public DisposableWrapper(T disposable)
        {
            this.disposable = disposable;
        }

        public T Acquire()
        {
            this.referenceCount++;
            return this.disposable;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.referenceCount--;

            if (referenceCount <= 0)
            {
                this.disposable.Dispose();
            }
        }

        #endregion
    }
}

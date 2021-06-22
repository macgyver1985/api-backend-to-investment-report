using System;

namespace InvestimentReport.Application.Helper
{

    public abstract class AdapterHelper : IDisposable
    {

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;

        ~AdapterHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

    }

}
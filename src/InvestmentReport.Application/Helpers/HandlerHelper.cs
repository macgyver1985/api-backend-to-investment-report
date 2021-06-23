using System;

namespace InvestmentReport.Application.Helper
{

    public abstract class HandlerHelper : IDisposable
    {

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;

        ~HandlerHelper()
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
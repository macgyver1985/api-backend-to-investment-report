using System;

namespace InvestmentReport.Presentation.Helpers
{

    /// <summary>
    /// Classe que auxilia a implementação concreta das controllers.
    /// </summary>
    public abstract class ControllerHelper : IDisposable
    {

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;

        ~ControllerHelper()
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
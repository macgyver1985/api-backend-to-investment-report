using System;
using System.Collections;
using System.Threading;
using InvestimentReport.CrossCutting.Trace.DTOs;
using Newtonsoft.Json;

namespace InvestimentReport.CrossCutting.Trace.Helpers
{

    public abstract class LoggerHelper : IDisposable
    {

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;
        private readonly Queue queue;

        public LoggerHelper()
        {
            this.queue = new Queue();
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.LoggerWatch), (object)this.queue);
        }

        ~LoggerHelper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        protected abstract void LoggerWatch(object state);

        protected void PushQueue<TPayload>(LoggerDTO<TPayload> data)
        {
            try
            {
                lock (this.synchronizeTasks)
                {
                    string json = JsonConvert.SerializeObject(data, Formatting.None);

                    this.queue.Enqueue(json);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
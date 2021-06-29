using System;
using System.Collections;
using System.Threading;
using InvestmentReport.CrossCutting.Trace.DTOs;
using Newtonsoft.Json;

namespace InvestmentReport.CrossCutting.Trace.Helpers
{

    /// <summary>
    /// Classe que de suporte para construção de outras implementações do ILogger.
    /// </summary>
    public abstract class LoggerHelper : IDisposable
    {

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;
        private readonly Queue queue;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
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

        /// <summary>
        /// Método que recebe a fila contendo as mensagens que devem ser persistidas. Esse método é iniciado com ThreadPool.QueueUserWorkItem.
        /// </summary>
        /// <param name="state">Instância de um System.Collections.Queue.</param>
        /// <see cref="System.Threading.ThreadPool.QueueUserWorkItem(WaitCallback, object?)"/>
        protected abstract void LoggerWatch(object state);

        /// <summary>
        /// Método que coloca a mensagem na fila para ser persisitda.
        /// </summary>
        /// <param name="data">Instância do InvestmentReport.CrossCutting.Trace.DTOs.LoggerDTO que contém os dados que serão persistidos.</param>
        /// <see cref="InvestmentReport.CrossCutting.Trace.DTOs.LoggerDTO"/>
        protected void PushQueue(LoggerDTO data)
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
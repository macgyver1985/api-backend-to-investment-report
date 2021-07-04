using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using InvestmentReport.CrossCutting.Trace.DTOs;
using InvestmentReport.CrossCutting.Trace.Exceptions;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace InvestmentReport.CrossCutting.Trace.Helpers
{

    /// <summary>
    /// Classe que de suporte para construção de outras implementações do ILogger.
    /// </summary>
    /// <remarks>
    /// Deve ser incluída a seção de configuração na raiz do appsettings.json ou appsettings.{ASPNETCORE_ENVIRONMENT}.json.
    /// Veja o exemplo da configuração que deve ser feita:
    /// <code>
    /// {
    ///     "Logger": {
    ///       "IsLoggerEnable": true,
    ///       "IsError": true,
    ///       "IsDebug": true
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public abstract class LoggerHelper : ILogger
    {

        #region Variables

        protected volatile object synchronizeTasks = new object();
        protected bool disposed = false;
        private readonly Queue queue;

        #endregion

        #region Properties

        public bool IsLoggerEnable { get; private set; }

        public bool IsError { get; private set; }

        public bool IsDebug { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor que recebe uma instância de IConfiguration.
        /// </summary>
        /// <param name="configuration">Instância da classe concreta que implementa um IConfiguration.</param>
        /// <exception cref="ArgumentException">
        /// Exceção retornada caso uma instância de IConfiguration não seja informada.
        /// </exception>
        /// <exception cref="ConfigurationException">
        /// Exceção retornada caso hajam problemas nas configurações necessárias para o Logger.
        /// </exception>
        /// <see cref="IConfiguration"/>
        public LoggerHelper(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.", "configuration");

            if (!configuration.GetSection("Logger").Exists())
                throw new ConfigurationException("Logger");

            if (string.IsNullOrEmpty(configuration["Logger:Path"]))
                throw new ConfigurationException("Logger", "Path");

            this.IsLoggerEnable = bool.TryParse(configuration["Logger:IsLoggerEnable"], out bool enable) ? enable : true;
            this.IsError = bool.TryParse(configuration["Logger:IsError"], out bool enableError) ? enableError : true;
            this.IsDebug = bool.TryParse(configuration["Logger:IsDebug"], out bool enableDebug) ? enableDebug : true;

            this.queue = new Queue();
            ThreadPool.QueueUserWorkItem(new WaitCallback(this.LoggerWatch), (object)this.queue);
        }

        ~LoggerHelper()
        {
            Dispose(false);
        }

        #endregion

        #region Methods

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
        /// <see cref="ThreadPool.QueueUserWorkItem(WaitCallback, object?)"/>
        /// <see cref="Queue"/>
        protected abstract void LoggerWatch(object state);

        /// <summary>
        /// Método que coloca a mensagem na fila para ser persisitda.
        /// </summary>
        /// <param name="data">Instância do LoggerDTO que contém os dados que serão persistidos.</param>
        /// <see cref="LoggerDTO"/>
        private void PushQueue(LoggerDTO data)
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

        private ILogger ErrorAux(Guid processId, string message, Exception ex, object payload)
        {
            if (this.IsLoggerEnable && this.IsError)
            {
                var context = new StackFrame(2);

                this.PushQueue(new LoggerDTO()
                {
                    CreatedDate = DateTime.UtcNow,
                    Type = ELoggerType.ERROR,
                    ProcessId = processId,
                    Context = context.GetMethod().DeclaringType.DeclaringType.FullName,
                    Method = context.GetMethod().DeclaringType.Name,
                    Message = message,
                    Payload = payload,
                    Exception = new ExceptionDTO()
                    {
                        Message = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
                        Type = $"{ex.GetType().Namespace}.{ex.GetType().Name}"
                    }
                });
            }

            return this;
        }

        private ILogger DebugAux(Guid processId, string message, object payload)
        {
            if (this.IsLoggerEnable && this.IsDebug)
            {
                var context = new StackFrame(2);

                this.PushQueue(new LoggerDTO()
                {
                    CreatedDate = DateTime.UtcNow,
                    Type = ELoggerType.DEBUG,
                    ProcessId = processId,
                    Context = context.GetMethod().DeclaringType.DeclaringType.FullName,
                    Method = context.GetMethod().DeclaringType.Name,
                    Message = message,
                    Payload = payload
                });
            }

            return this;
        }

        public ILogger Error(Guid processId, string message, Exception ex, object payload)
        {
            return this.ErrorAux(processId, message, ex, null);
        }

        public ILogger Error(Guid processId, string message, Exception ex)
        {
            return this.ErrorAux(processId, message, ex, null);
        }

        public ILogger Debug(Guid processId, string message, object payload)
        {
            return this.DebugAux(processId, message, null);
        }

        public ILogger Debug(Guid processId, string message)
        {
            return this.DebugAux(processId, message, null);
        }

        public async Task<ILogger> ErrorAsync(Guid processId, string message, Exception ex, object payload)
        {
            return await Task.Factory.StartNew(() => this.ErrorAux(processId, message, ex, payload));
        }

        public async Task<ILogger> ErrorAsync(Guid processId, string message, Exception ex)
        {
            return await Task.Factory.StartNew(() => this.ErrorAux(processId, message, ex, null));
        }

        public async Task<ILogger> DebugAsync(Guid processId, string message, object payload)
        {
            return await Task.Factory.StartNew(() => this.DebugAux(processId, message, payload));
        }

        public async Task<ILogger> DebugAsync(Guid processId, string message)
        {
            return await Task.Factory.StartNew(() => this.DebugAux(processId, message, null));
        }

        #endregion

    }

}
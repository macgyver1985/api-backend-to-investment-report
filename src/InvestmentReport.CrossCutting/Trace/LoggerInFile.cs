using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InvestmentReport.CrossCutting.Trace.DTOs;
using InvestmentReport.CrossCutting.Trace.Exceptions;
using InvestmentReport.CrossCutting.Trace.Helpers;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InvestmentReport.CrossCutting.Trace
{

    /// <summary>
    /// Classe que faz a gravação dos logs em arquivo.
    /// </summary>
    /// <remarks>
    /// Deve ser incluída a seção de configuração na raiz do appsettings.json ou appsettings.{ASPNETCORE_ENVIRONMENT}.json.
    /// Veja o exemplo da configuração que deve ser feita:
    /// <code>
    /// {
    ///     "LoggerInFile": {
    ///       "Path": "./../../logs/",
    ///       "IsLoggerEnable": true,
    ///       "IsError": true,
    ///       "IsDebug": true
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public sealed class LoggerInFile : LoggerHelper, ILogger
    {

        #region Variables

        private readonly string path;

        #endregion

        #region Properties

        public bool IsLoggerEnable { get; private set; }

        public bool IsError { get; private set; }

        public bool IsDebug { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor que recebe uma instância de Microsoft.Extensions.Configuration.IConfiguration.
        /// </summary>
        /// <param name="configuration">Instância da classe concreta que implementa um Microsoft.Extensions.Configuration.IConfiguration.</param>
        /// <exception cref="System.ArgumentException">
        /// Exceção retornada caso uma instância de Microsoft.Extensions.Configuration.IConfiguration não seja informada.
        /// </exception>
        /// <exception cref="InvestmentReport.CrossCutting.Trace.Exceptions.ConfigurationException">
        /// Exceção retornada caso hajam problemas nas configurações necessárias para o LoggerInFile.
        /// </exception>
        public LoggerInFile(IConfiguration configuration) : base()
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.", "configuration");

            if (!configuration.GetSection("LoggerInFile").Exists())
                throw new ConfigurationException("LoggerInFile");

            if (string.IsNullOrEmpty(configuration["LoggerInFile:Path"]))
                throw new ConfigurationException("LoggerInFile", "Path");

            if (Directory.Exists(configuration["LoggerInFile:Path"]))
                Directory.CreateDirectory(configuration["LoggerInFile:Path"]);

            this.path = configuration["LoggerInFile:Path"];
            this.IsLoggerEnable = bool.TryParse(configuration["LoggerInFile:IsLoggerEnable"], out bool enable) ? enable : true;
            this.IsError = bool.TryParse(configuration["LoggerInFile:IsError"], out bool enableError) ? enableError : true;
            this.IsDebug = bool.TryParse(configuration["LoggerInFile:IsDebug"], out bool enableDebug) ? enableDebug : true;
        }

        #endregion

        #region Methods

        protected override void Dispose(bool disposing) { }

        protected override void LoggerWatch(object state)
        {
            Queue queue = (Queue)state;

            while (true)
            {
                lock (this.synchronizeTasks)
                {
                    StringBuilder stringBuilder = new StringBuilder();

                    while (queue.Count > 0)
                        stringBuilder.AppendLine((string)queue.Dequeue());

                    if (stringBuilder.Length > 0)
                    {
                        using (StreamWriter streamWriter = new StreamWriter($"{this.path}logger_{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt", true, Encoding.UTF8))
                        {
                            streamWriter.Write(stringBuilder.ToString());
                            streamWriter.Close();
                        }
                    }
                }

                Thread.Sleep(500);
            }
        }

        public ILogger Error<Context>(Guid processId, string message, Exception ex, object payload)
        {
            if (this.IsLoggerEnable && this.IsError)
                this.PushQueue(new LoggerDTO()
                {
                    CreatedDate = DateTime.UtcNow,
                    Type = "ERROR",
                    ProcessId = processId,
                    Context = typeof(Context).Name,
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

            return this;
        }

        public ILogger Error<Context>(Guid processId, string message, Exception ex)
        {
            return this.Error<Context>(processId, message, ex, null);
        }

        public ILogger Debug<Context>(Guid processId, string message, object payload)
        {
            if (this.IsLoggerEnable && this.IsDebug)
                this.PushQueue(new LoggerDTO()
                {
                    CreatedDate = DateTime.UtcNow,
                    Type = "DEBUG",
                    ProcessId = processId,
                    Context = typeof(Context).Name,
                    Message = message,
                    Payload = payload
                });

            return this;
        }

        public ILogger Debug<Context>(Guid processId, string message)
        {
            return this.Debug<Context>(processId, message, null);
        }

        public async Task<ILogger> ErrorAsync<Context>(Guid processId, string message, Exception ex, object payload)
        {
            return await Task.Factory.StartNew(() => this.Error<Context>(processId, message, ex, payload));
        }

        public async Task<ILogger> ErrorAsync<Context>(Guid processId, string message, Exception ex)
        {
            return await Task.Factory.StartNew(() => this.Error<Context>(processId, message, ex, null));
        }

        public async Task<ILogger> DebugAsync<Context>(Guid processId, string message, object payload)
        {
            return await Task.Factory.StartNew(() => this.Debug<Context>(processId, message, payload));
        }

        public async Task<ILogger> DebugAsync<Context>(Guid processId, string message)
        {
            return await Task.Factory.StartNew(() => this.Debug<Context>(processId, message, null));
        }

        #endregion

    }

}
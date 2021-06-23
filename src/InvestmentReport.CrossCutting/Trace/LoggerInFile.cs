using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InvestmentReport.CrossCutting.Trace.DTOs;
using InvestmentReport.CrossCutting.Trace.Helpers;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InvestmentReport.CrossCutting.Trace
{

    public sealed class LoggerInFile : LoggerHelper, ILogger
    {

        private readonly string path;

        public bool IsLoggerEnable { get; private set; }

        public bool IsError { get; private set; }

        public bool IsDebug { get; private set; }

        public LoggerInFile(IConfiguration configuration) : base()
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.");

            if (string.IsNullOrEmpty(configuration["LoggerInFile:Path"]))
                throw new ArgumentException("The logs path is null.");

            if (Directory.Exists(configuration["LoggerInFile:Path"]))
                Directory.CreateDirectory(configuration["LoggerInFile:Path"]);

            this.path = configuration["LoggerInFile:Path"];
            this.IsLoggerEnable = bool.TryParse(configuration["LoggerInFile:IsLoggerEnable"], out bool enable) ? enable : true;
            this.IsError = bool.TryParse(configuration["LoggerInFile:IsError"], out bool enableError) ? enableError : true;
            this.IsDebug = bool.TryParse(configuration["LoggerInFile:IsDebug"], out bool enableDebug) ? enableDebug : true;
        }

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

        public async Task<ILogger> Error<Context, TPayload>(Guid processId, string message, Exception ex, TPayload payload)
        {
            if (this.IsLoggerEnable && this.IsError)
            {
                await Task.Factory.StartNew(() =>
                {
                    this.PushQueue(new LoggerDTO<TPayload>()
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
                            StackTrace = ex.StackTrace
                        }
                    });
                });
            }

            return this;
        }

        public async Task<ILogger> Error<Context>(Guid processId, string message, Exception ex)
        {
            if (this.IsLoggerEnable && this.IsError)
            {
                await Task.Factory.StartNew(() =>
                {
                    this.PushQueue(new LoggerDTO<string>()
                    {
                        CreatedDate = DateTime.UtcNow,
                        Type = "ERROR",
                        ProcessId = processId,
                        Context = typeof(Context).Name,
                        Message = message,
                        Exception = new ExceptionDTO()
                        {
                            Message = ex.Message,
                            Source = ex.Source,
                            StackTrace = ex.StackTrace
                        }
                    });
                });
            }

            return this;
        }

        public async Task<ILogger> Debug<Context, TPayload>(Guid processId, string message, TPayload payload)
        {
            if (this.IsLoggerEnable && this.IsDebug)
            {
                await Task.Factory.StartNew(() =>
                {
                    this.PushQueue(new LoggerDTO<TPayload>()
                    {
                        CreatedDate = DateTime.UtcNow,
                        Type = "DEBUG",
                        ProcessId = processId,
                        Context = typeof(Context).Name,
                        Message = message,
                        Payload = payload
                    });
                });
            }

            return this;
        }
    }

}
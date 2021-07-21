using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using InvestmentReport.CrossCutting.Trace.Helpers;
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
    ///     "Logger": {
    ///       "Path": "./../../logs/",
    ///       "IsLoggerEnable": true,
    ///       "IsError": true,
    ///       "IsDebug": true
    ///     }
    /// }
    /// </code>
    /// </remarks>
    /// <see cref="LoggerHelper"/>
    public sealed class LoggerInFile : LoggerHelper
    {

        #region Variables

        private readonly string path;

        #endregion

        #region Constructors

        /// <summary>
        /// Construtor que recebe uma instância de Microsoft.Extensions.Configuration.IConfiguration.
        /// Caso o diretório onde serão gravadas as mensagens não existe o mesmo será criado através do construtor da classe.
        /// </summary>
        /// <param name="configuration">Instância da classe concreta que implementa um Microsoft.Extensions.Configuration.IConfiguration.</param>
        /// <see cref="LoggerHelper.LoggerHelper(IConfiguration)"/>
        public LoggerInFile(IConfiguration configuration) : base(configuration)
        {
            if (Directory.Exists(configuration["Logger:Path"]))
                Directory.CreateDirectory(configuration["Logger:Path"]);

            this.path = configuration["Logger:Path"];
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

        #endregion

    }

}
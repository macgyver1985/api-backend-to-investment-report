using System;
using System.Threading.Tasks;

namespace InvestmentReport.CrossCutting.Trace.Interfaces
{

    /// <summary>
    /// Adapter para recurso de log.
    /// </summary>
    public interface ILogger : IDisposable
    {

        /// <summary>
        /// Indica se a escrita de logs está habilitada.
        /// </summary>
        bool IsLoggerEnable { get; }

        /// <summary>
        /// Indica se a escrita de erros está habilitada.
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Indica se a escrita de debug está habilitada.
        /// </summary>
        bool IsDebug { get; }

        /// <summary>
        /// Envia mensagem de erro para serer persistida.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsError devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="ex">Instância de uma System.Exception.</param>
        /// <param name="payload">Estrutura de dados relacionada com a mensagem de log.</param>
        /// <returns>Retorna ILogger.</returns>
        ILogger Error(Guid processId, string message, Exception ex, object payload);

        /// <summary>
        /// Envia mensagem de erro para serer persistida.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsError devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="ex">Instância de uma System.Exception.</param>
        /// <returns>Retorna ILogger.</returns>
        ILogger Error(Guid processId, string message, Exception ex);

        /// <summary>
        /// Envia mensagem de debug para serer persistida.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsDebug devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="payload">Estrutura de dados relacionada com a mensagem de log.</param>
        /// <returns>Retorna ILogger.</returns>
        ILogger Debug(Guid processId, string message, object payload);

        /// <summary>
        /// Envia mensagem de debug para serer persistida.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsDebug devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <returns>Retorna ILogger.</returns>
        ILogger Debug(Guid processId, string message);

        /// <summary>
        /// Envia mensagem de erro para serer persistida de forma assíncrona.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsError devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="ex">Instância de uma System.Exception.</param>
        /// <param name="payload">Estrutura de dados relacionada com a mensagem de log.</param>
        /// <returns>Retorna ILogger.</returns>
        Task<ILogger> ErrorAsync(Guid processId, string message, Exception ex, object payload);

        /// <summary>
        /// Envia mensagem de erro para serer persistida de forma assíncrona.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsError devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="ex">Instância de uma System.Exception.</param>
        /// <returns>Retorna ILogger.</returns>
        Task<ILogger> ErrorAsync(Guid processId, string message, Exception ex);

        /// <summary>
        /// Envia mensagem de debug para serer persistida de forma assíncrona.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsDebug devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <param name="payload">Estrutura de dados relacionada com a mensagem de log.</param>
        /// <returns>Retorna ILogger.</returns>
        Task<ILogger> DebugAsync(Guid processId, string message, object payload);

        /// <summary>
        /// Envia mensagem de debug para serer persistida de forma assíncrona.
        /// </summary>
        /// <remarks>
        /// IsLoggerEnable e IsDebug devem estar habilitados, ou seja, retornando true.
        /// </remarks>
        /// <param name="processId">Identificação do processo que gerou o log.</param>
        /// <param name="message">Mensagem customizada do log.</param>
        /// <returns>Retorna ILogger.</returns>
        Task<ILogger> DebugAsync(Guid processId, string message);

    }

}
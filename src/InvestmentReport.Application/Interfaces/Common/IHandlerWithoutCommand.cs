using System;
using System.Threading.Tasks;

namespace InvestmentReport.Application.Interfaces.Common
{

    /// <summary>
    /// Contrato base para implamentação de handlers que não recebem commmands.
    /// </summary>
    /// <typeparam name="T">Tipo de entidade de dominio que será retornada.</typeparam>
    public interface IHandlerWithoutCommand<T> : IDisposable
    {

        /// <summary>
        /// Método onde é implementado um serviço de dominio.
        /// </summary>
        /// <param name="processId">Indetificação do processo.</param>
        /// <returns>Retorna a entidade informada em T.</returns>
        Task<T> Execute(Guid processId);

    }

}
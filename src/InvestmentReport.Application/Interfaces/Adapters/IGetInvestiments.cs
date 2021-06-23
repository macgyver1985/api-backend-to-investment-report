using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentReport.Application.Interfaces.Adapters
{

    /// <summary>
    /// Adapter para implementação do serviços que busca os dados investimento que são externos.
    /// </summary>
    public interface IGetInvestments : IDisposable
    {

        /// <summary>
        /// Método que obtem o DTO do investimento desejado.
        /// </summary>
        /// <typeparam name="T">Typo do DTO que será consultado.</typeparam>
        /// <param name="processId">Identificação do processamento.</param>
        /// <returns>Retorna lista do DTO informado em T.</returns>
        Task<IList<T>> GetOf<T>(Guid processId);

    }

}
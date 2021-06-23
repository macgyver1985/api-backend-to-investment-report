using System;
using System.Threading.Tasks;

namespace InvestmentReport.Application.Interfaces.Adapters
{

    /// <summary>
    /// Adapter para implementação de serviços de cache.
    /// </summary>
    public interface ICache : IDisposable
    {

        /// <summary>
        /// Registra um cache.
        /// </summary>
        /// <param name="key">Identificação do cache.</param>
        /// <param name="value">Valor do cache.</param>
        /// <param name="expire">Tempo de validade do cache.</param>
        /// <returns>Retorna true quando registrado com sucesso.</returns>
        Task<bool> Register(string key, string value, TimeSpan expire);

        /// <summary>
        /// Obtem o valor de uma cache.
        /// </summary>
        /// <param name="key">Identificação do cache.</param>
        /// <returns>Retorna o cache em string.</returns>
        Task<string> Obtain(string key);

        /// <summary>
        /// Apaga um regitro de cache.
        /// </summary>
        /// <param name="key">Identificação do cache.</param>
        /// <returns>Retorno void.</returns>
        Task Remove(string key);

    }

}
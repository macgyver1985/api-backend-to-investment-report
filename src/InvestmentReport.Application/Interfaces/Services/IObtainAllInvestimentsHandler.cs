using System.Collections.Generic;
using InvestmentReport.Application.Interfaces.Common;
using InvestmentReport.Domain.Investments;

namespace InvestmentReport.Application.Interfaces.Services
{

    /// <summary>
    /// Contrato do serviço de dominio que retorna a lista dos investimentos de uma cliente.
    /// </summary>
    public interface IObtainAllInvestmentsHandler : IHandlerWithoutCommand<IList<Investment>> { }

}
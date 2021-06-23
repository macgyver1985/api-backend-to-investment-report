using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentReport.WebApi.ViewModels
{

    public class ListInvestmentsModel
    {

        public double ValorTotal
        {
            get
            {
                if (!this.Investmentos.Any())
                    return 0D;

                return this.Investmentos.Sum(t => t.ValorTotal);
            }
        }

        public Guid ProcessId { get; set; }

        public IList<InvestmentModel> Investmentos { get; private set; }

        public ListInvestmentsModel()
        {
            this.Investmentos = new List<InvestmentModel>();
        }

    }

}
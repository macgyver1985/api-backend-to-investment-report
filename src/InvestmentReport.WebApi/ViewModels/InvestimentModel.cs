using System;

namespace InvestmentReport.WebApi.ViewModels
{

    public class InvestmentModel
    {

        public string Nome { get; set; }

        public double ValorInvestido { get; set; }

        public double ValorTotal { get; set; }

        public DateTime Vencimento { get; set; }

        public double Ir { get; set; }

        public double ValorResgate { get; set; }

    }

}
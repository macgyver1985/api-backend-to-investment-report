using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InvestmentReport.Presentation.ViewModels
{

    public class ListInvestmentsModel
    {

        [JsonProperty("valorTotal")]
        public double TotalValue
        {
            get
            {
                if (this.Investments == null || !this.Investments.Any())
                    return 0D;

                return this.Investments.Sum(t => t.CurrentValue);
            }
        }

        [JsonProperty("investimentos")]
        public IList<InvestmentModel> Investments { get; internal set; }

    }

}
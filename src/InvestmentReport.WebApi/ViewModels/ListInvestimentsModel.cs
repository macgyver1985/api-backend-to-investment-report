using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.ViewModels
{

    public class ListInvestmentsModel
    {

        [JsonProperty("valorTotal")]
        public double TotalValue
        {
            get
            {
                if (!this.Investments.Any())
                    return 0D;

                return this.Investments.Sum(t => t.CurrentValue);
            }
        }

        [JsonProperty("investmentos")]
        public IList<InvestmentModel> Investments { get; private set; }

        public ListInvestmentsModel()
        {
            this.Investments = new List<InvestmentModel>();
        }

    }

}
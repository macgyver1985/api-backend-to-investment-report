using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace InvestimentReport.WebApi.ViewModels
{

    public class ListInvestimentsModel
    {

        [JsonProperty("valorTotal")]
        public double TotalValue
        {
            get
            {
                if (!this.Investiments.Any())
                    return 0D;

                return this.Investiments.Sum(t => t.CurrentValue);
            }
        }

        [JsonProperty("investimentos")]
        public IList<InvestimentModel> Investiments { get; private set; }

        public ListInvestimentsModel()
        {
            this.Investiments = new List<InvestimentModel>();
        }

    }

}
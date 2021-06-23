using System.Collections.Generic;
using InvestmentReport.Application.DTOs;
using InvestmentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestmentReport.Infrastructure.Services
{

    internal class FixedIncomeResponseFactory : ITypeServiceResponseFactory
    {

        public object ConvertList(JObject json)
        {
            List<FixedIncomeDTO> result = JsonConvert
                .DeserializeObject<List<FixedIncomeDTO>>(json.GetValue("lcis").ToString());

            return result;
        }

    }

}
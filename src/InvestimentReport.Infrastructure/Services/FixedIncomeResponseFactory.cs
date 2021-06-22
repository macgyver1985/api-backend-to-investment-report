using System.Collections.Generic;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestimentReport.Infrastructure.Services
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
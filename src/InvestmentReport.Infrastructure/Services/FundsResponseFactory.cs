using System.Collections.Generic;
using InvestmentReport.Application.DTOs;
using InvestmentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestmentReport.Infrastructure.Services
{

    internal class FundsResponseFactory : ITypeServiceResponseFactory
    {

        public object ConvertList(JObject json)
        {
            List<FundsDTO> result = JsonConvert
                .DeserializeObject<List<FundsDTO>>(json.GetValue("fundos").ToString());

            return result;
        }

    }

}
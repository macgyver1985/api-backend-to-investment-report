using System.Collections.Generic;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestimentReport.Infrastructure.Services
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
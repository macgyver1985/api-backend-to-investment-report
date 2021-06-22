using System.Collections.Generic;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestimentReport.Infrastructure.Services
{

    internal class DirectTreasureResponseFactory : ITypeServiceResponseFactory
    {

        public object ConvertList(JObject json)
        {
            List<DirectTreasureDTO> result = JsonConvert
                .DeserializeObject<List<DirectTreasureDTO>>(json.GetValue("tds").ToString());

            return result;
        }

    }

}
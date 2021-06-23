using System.Collections.Generic;
using InvestmentReport.Application.DTOs;
using InvestmentReport.Infrastructure.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvestmentReport.Infrastructure.Services
{

    /// <summary>
    /// teste
    /// </summary>
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
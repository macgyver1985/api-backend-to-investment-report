using Newtonsoft.Json.Linq;

namespace InvestmentReport.Infrastructure.Interfaces
{

    internal interface ITypeServiceResponseFactory
    {

        object ConvertList(JObject json);

    }

}
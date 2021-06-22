using Newtonsoft.Json.Linq;

namespace InvestimentReport.Infrastructure.Interfaces
{

    internal interface ITypeServiceResponseFactory
    {

        object ConvertList(JObject json);

    }

}
using System;
using System.Net;
using Newtonsoft.Json;

namespace InvestmentReport.Presentation.Helpers
{

    public sealed class HttpResponse<TData>
    {

        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; internal set; }

        [JsonProperty("processId")]
        public Guid ProcessId { get; internal set; }

        [JsonProperty("success")]
        public TData Success { get; internal set; }

        [JsonProperty("fault")]
        public string Fault { get; internal set; }

    }

}
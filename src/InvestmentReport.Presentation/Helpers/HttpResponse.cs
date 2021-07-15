using System;
using System.Net;
using Newtonsoft.Json;

namespace InvestmentReport.Presentation.Helpers
{

    public sealed class HttpResponse<TData>
    {

        [JsonProperty("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("processId")]
        public Guid ProcessId { get; set; }

        [JsonProperty("success")]
        public TData Success { get; set; }

        [JsonProperty("fault")]
        public string Fault { get; set; }

    }

}
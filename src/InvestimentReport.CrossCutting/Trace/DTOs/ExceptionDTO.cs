using Newtonsoft.Json;

namespace InvestimentReport.CrossCutting.Trace.DTOs
{

    public class ExceptionDTO
    {

        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

    }

}
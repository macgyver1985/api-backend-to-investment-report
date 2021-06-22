using System;
using Newtonsoft.Json;

namespace InvestimentReport.CrossCutting.Trace.DTOs
{

    public class LoggerDTO<TPayload>
    {

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("processId")]
        public Guid ProcessId { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("exception")]
        public ExceptionDTO Exception { get; set; }

        [JsonProperty("payload")]
        public TPayload Payload { get; set; }

    }

}
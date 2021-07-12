using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace InvestmentReport.Presentation.Helpers
{

    public sealed class HttpRequest<TData>
        where TData : class
    {

        public Dictionary<string, StringValues> Headers { get; private set; }

        public TData Body { get; private set; }

        public HttpRequest(
            Dictionary<string, StringValues> headers,
            TData body
        )
        {
            this.Headers = new Dictionary<string, StringValues>();
            this.Body = body;
        }

        public HttpRequest(Dictionary<string, StringValues> headers)
                    : this(headers, null)
        { }

        public HttpRequest(TData body)
            : this(null, body)
        { }

    }

}
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace InvestmentReport.Presentation.Helpers
{

    public sealed class HttpRequest<TData>
        where TData : class
    {

        private Dictionary<string, StringValues> headers;

        public IReadOnlyDictionary<string, StringValues> Headers { get => this.headers; }

        public TData Body { get; private set; }

        public HttpRequest(KeyValuePair<string, StringValues>[] headers, TData body)
        {
            this.headers = new Dictionary<string, StringValues>();
            this.Body = body;

            headers?.All(t => this.headers.TryAdd(t.Key, t.Value));
        }

        public HttpRequest(KeyValuePair<string, StringValues>[] headers)
            : this(headers, null)
        { }

        public HttpRequest(TData body)
            : this(null, body)
        { }

    }

}
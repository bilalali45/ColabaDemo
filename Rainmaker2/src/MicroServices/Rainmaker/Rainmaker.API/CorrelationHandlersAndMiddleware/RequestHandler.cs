using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RainMaker.API.CorrelationHandlersAndMiddleware
{
    public class RequestHandler : DelegatingHandler
    {
        private readonly ICorrelationIdAccessor _correlationIdAccessor;

        public RequestHandler(ICorrelationIdAccessor correlationIdAccessor)
        {
            this._correlationIdAccessor = correlationIdAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("CorrelationId", _correlationIdAccessor.GetCorrelationId());

            return base.SendAsync(request, cancellationToken);
        }
    }
}

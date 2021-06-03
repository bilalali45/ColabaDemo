using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TenantConfig.API.CorrelationHandlersAndMiddleware
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
            var tenant = _correlationIdAccessor.GetTenantModel();
            if(!string.IsNullOrEmpty(tenant))
            {
                request.Headers.Add(TenantConfig.Common.DistributedCache.Constants.COLABA_TENANT, tenant);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}

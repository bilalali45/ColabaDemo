using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using TenantConfig.Common.DistributedCache;

namespace Identity.CorrelationHandlersAndMiddleware
{

    public class CorrelationIdAccessor : ICorrelationIdAccessor
    {
        private readonly ILogger<CorrelationIdAccessor> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdAccessor(ILogger<CorrelationIdAccessor> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCorrelationId()
        {



            try
            {
                var context = _httpContextAccessor.HttpContext;
                var result = context?.Items["CorrelationId"] as string;

                return result;
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Unable to get correlation id in header");
            }

            return string.Empty;
        }
        public string GetTenantModel()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                var result = context?.Items[Constants.COLABA_TENANT] as TenantModel;

                return Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
            catch (Exception exception)
            {
                _logger.LogWarning(exception, "Unable to get tenant in header");
            }

            return string.Empty;
        }
    }
}

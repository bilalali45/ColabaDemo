namespace TenantConfig.API.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
        string GetTenantModel();
    }


}

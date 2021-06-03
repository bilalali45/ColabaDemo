namespace Identity.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
        string GetTenantModel();
    }


}

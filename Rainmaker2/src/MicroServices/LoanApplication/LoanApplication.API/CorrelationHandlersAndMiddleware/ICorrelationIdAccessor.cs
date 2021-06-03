namespace LoanApplication.API.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
        string GetTenantModel();
    }


}

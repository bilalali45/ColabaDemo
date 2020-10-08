namespace LosIntegration.API.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
    }


}

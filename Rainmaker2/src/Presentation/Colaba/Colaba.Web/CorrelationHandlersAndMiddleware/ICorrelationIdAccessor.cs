namespace Colaba.Web.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
    }


}

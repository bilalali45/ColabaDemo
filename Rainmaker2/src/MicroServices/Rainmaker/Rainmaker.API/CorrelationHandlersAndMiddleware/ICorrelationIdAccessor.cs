namespace RainMaker.API.CorrelationHandlersAndMiddleware
{

    public interface ICorrelationIdAccessor
    {
        string GetCorrelationId();
    }


}

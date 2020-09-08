using System;

namespace LosIntegration.API
{
    public class LosIntegrationException:Exception
    {
        public LosIntegrationException(string message) : base(message)
        { }
    }
}

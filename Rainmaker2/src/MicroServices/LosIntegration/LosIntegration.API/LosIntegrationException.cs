using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API
{
    public class LosIntegrationException:Exception
    {
        public LosIntegrationException(string message) : base(message)
        { }
    }
}

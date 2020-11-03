using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LosIntegration.API.Controllers
{
    public abstract class BaseApiController<T> : ControllerBase
    {
        protected int TenantId
        {
            get
            {
                return 1;
            }
        }
        protected short LosId
        {
            get
            {
                return 1;
            }
        }
        protected short RainmakerLosId
        {
            get
            {
                return 2;
            }
        }

        protected readonly ILogger<T> _logger;
        public BaseApiController(ILogger<T> logger)
        {
            this._logger = logger;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainGateway.Services
{
    public class AllowedApisOptions
    {
        public const string AllowedApis = "AllowedApis";

        public string[] Apis { get; set; }
    }
}

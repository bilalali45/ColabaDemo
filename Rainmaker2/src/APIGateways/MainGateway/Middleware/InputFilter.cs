using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MainGateway.Middleware
{
    public class InputFilter : RainsoftGateway.Core.Middleware.InputFilter
    {
        public InputFilter(RequestDelegate next) : base(next)
        {
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainGateway.Middleware
{
    public class ParameterPollutionDetection : RainsoftGateway.Core.Middleware.ParameterPollutionDetection
    {
        public ParameterPollutionDetection(RequestDelegate next) : base(next)
        {
        }
    }
}
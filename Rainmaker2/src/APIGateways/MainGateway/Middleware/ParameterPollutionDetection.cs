using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MainGateway.Middleware
{
    public class ParameterPollutionDetection
    {
        private readonly RequestDelegate _next;


        public ParameterPollutionDetection(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            var pollutionDetected = false;
            var queryStringString = context.Request.QueryString.ToString();

            var query = QueryHelpers.ParseQuery(queryString: queryStringString);

            var duplicateKeysExists = query.Any(predicate: keyValuePair => keyValuePair.Value.Count > 1);

            if (duplicateKeysExists) pollutionDetected = true;

            if (queryStringString.Contains(value: "%26") || queryStringString.Contains(value: "%3"))
                pollutionDetected = true;

            if (pollutionDetected)
            {
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                await context.Response.WriteAsync(text: "Potentially unsafe request detected",
                                                  encoding: Encoding.UTF8);
                return;
            }

            await _next.Invoke(context: context);
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainsoftGateway.Core.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string[] _pathToSkipInRequest;
        private readonly string[] _pathToSkipInResponse;
        public RequestResponseLoggingMiddleware(RequestDelegate next, string[] pathToSkipInRequest, string[] pathToSkipInResponse)
        {
            _next = next;
            _pathToSkipInRequest = pathToSkipInRequest;
            _pathToSkipInResponse = pathToSkipInResponse;
        }

        public virtual async Task InvokeAsync(HttpContext context, ILogger<RequestResponseLoggingMiddleware> _logger, IConfiguration configuration)
        {
            context.Request.EnableBuffering();

            var builder = new StringBuilder();

            var request = await FormatRequest(context.Request, configuration);

            builder.Append("Request: ").AppendLine(request);
            builder.AppendLine("Request headers:");
            foreach (var header in context.Request.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using var responseBody = new MemoryStream();
            //...and use that for the temporary response body
            context.Response.Body = responseBody;
            _logger.LogInformation(builder.ToString());
            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            //Format the response from the server
            builder = new StringBuilder();
            var response = await FormatResponse(context.Response, configuration, context.Request);
            builder.Append("Response: ").AppendLine(response);
            builder.AppendLine("Response headers: ");
            foreach (var header in context.Response.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            //Save log to chosen datastore
            _logger.LogInformation(builder.ToString());

            //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
            if (responseBody.Length > 0)
                await responseBody.CopyToAsync(originalBodyStream);
        }

        protected virtual async Task<string> FormatRequest(HttpRequest request, IConfiguration configuration)
        {
            // Leave the body open so the next middleware can read it.
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            body = Left(body, int.Parse(configuration["LoggingMiddleware:RequestSize"]) * 1024);
            // Do some processing with body…
            if (_pathToSkipInRequest?.Contains(request.Path.ToString(), StringComparer.OrdinalIgnoreCase)==true)
                body = "";
            var formattedRequest = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{request.QueryString} {body}";

            // Reset the request body stream position so the next middleware can read it
            request.Body.Position = 0;

            return formattedRequest;
        }

        protected virtual async Task<string> FormatResponse(HttpResponse response, IConfiguration configuration, HttpRequest request)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);
            text = Left(text, int.Parse(configuration["LoggingMiddleware:ResponseSize"]) * 1024);
            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            if (_pathToSkipInResponse?.Contains(request.Path.ToString(), StringComparer.OrdinalIgnoreCase)==true)
                text = "";
            return $"HTTP Status Code {response.StatusCode}: {text}";
        }

        protected static string Left(string s, int length)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            length = Math.Max(length, 0);

            if (s.Length > length)
            {
                return s.Substring(0, length);
            }
            else
            {
                return s;
            }
        }
    }
}

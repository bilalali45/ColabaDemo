using System.Net;
using Microsoft.AspNetCore.Http;

namespace Extensions.ExtensionClasses
{
    public static class HttpRequestExtensions
    {
        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                if (connection.LocalIpAddress != null)
                    return connection.RemoteIpAddress.Equals(comparand: connection.LocalIpAddress);
                return IPAddress.IsLoopback(address: connection.RemoteIpAddress);
            }

            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null) return true;

            return false;
        }
    }
}
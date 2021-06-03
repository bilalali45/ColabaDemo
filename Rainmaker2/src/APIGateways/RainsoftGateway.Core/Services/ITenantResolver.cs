using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RainsoftGateway.Core.Services
{
    public interface ITenantResolver
    {
        Task ResolveTenant(HttpContext context);
    }
}

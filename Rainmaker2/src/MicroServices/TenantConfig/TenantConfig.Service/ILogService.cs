using ColabaLog.Entity.Models;
using System.Threading.Tasks;
using TenantConfig.Model;

namespace TenantConfig.Service
{
    public interface ILogService : IServiceBase<ContactLog>
    {
        Task InsertLogContactEmail(string FirstName, string LastName, string Email, int tenantId);
        Task InsertLogContactEmailPhone(string FirstName, string LastName, string Email, string phone, int tenantId);
    }
}

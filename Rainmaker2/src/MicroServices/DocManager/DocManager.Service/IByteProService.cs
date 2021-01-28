using DocManager.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IByteProService
    {
        Task<Tenant> GetTenantSetting(int tenantId);
    }
}

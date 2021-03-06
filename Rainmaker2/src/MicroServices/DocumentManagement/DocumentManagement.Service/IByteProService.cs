using DocumentManagement.Entity;
using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IByteProService
    {
        Task<FileViewDto> View(AdminFileViewModel model, int tenantId);
        Task<Tenant> GetTenantSetting(int tenantId);
        Task SetTenantSetting(int tenantId, TenantSetting setting);
        Task UploadFiles(string id, string requestId, string docId, List<string> auth);
    }
}

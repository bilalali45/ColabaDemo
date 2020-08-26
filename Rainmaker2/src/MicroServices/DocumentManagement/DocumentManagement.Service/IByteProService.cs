using DocumentManagement.Entity;
using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IByteProService
    {
        Task<FileViewDTO> View(AdminFileViewModel model, int tenantId);
        Task<Tenant> GetTenantSetting(int tenantId);
        //Task<bool> UpdateByteProStatus(string id, string requestId, string docId, string fileId);
    }
}

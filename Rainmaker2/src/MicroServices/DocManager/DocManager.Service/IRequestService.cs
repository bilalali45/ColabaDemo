using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
   public interface IRequestService
    {
        Task<bool> Save(SaveModel loanApplication, IEnumerable<string> authHeader);
        Task<string> Submit(string contentType, string id, string requestId, string docId, string mcuName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userId, string userName, IEnumerable<string> authHeader);
        Task<List<FileViewDto>> GetFileByDocId(FileViewModel model, string ipAddress, int tenantId);
     }
}

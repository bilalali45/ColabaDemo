using DocManager.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocManager.Service
{
    public interface IThumbnailService
    {
        Task<SaveWorkbenchDocument> SaveWorkbenchDocument(string id, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                            int userProfileId, string userName, string encryptionAlgo, string encryptionKey);
        Task<SaveWorkbenchDocument> SaveTrashDocument(string id, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                           int userProfileId, string userName, string encryptionAlgo, string encryptionKey);
        Task<SaveWorkbenchDocument> SaveCategoryDocument(string id, string requestId, string docId, string fileId, int tenantId, string serverName, string mcuName, int size, string contentType,
                     int userProfileId, string userName, string encryptionAlgo, string encryptionKey);
    }
}

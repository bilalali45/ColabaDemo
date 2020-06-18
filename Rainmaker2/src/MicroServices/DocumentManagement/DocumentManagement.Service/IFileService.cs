using DocumentManagement.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IFileService
    {
     
        Task<bool> Done(DoneModel model,int userProfileId);
        Task<bool> Rename(FileRenameModel model, int userProfileId);

        Task Order(FileOrderModel model, int userProfileId);

        Task<bool> Submit(string contentType,string id,string requestId,string docId , string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userProfileId);
        Task<FileViewDTO> View(FileViewModel model, int userProfileId);
    }
}

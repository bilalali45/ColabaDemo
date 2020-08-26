﻿using DocumentManagement.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagement.Service
{
    public interface IFileService
    {
     
        Task<bool> Done(DoneModel model,int userProfileId, int tenantId, IEnumerable<string> authHeader);
        Task<bool> Rename(FileRenameModel model, int userProfileId, int tenantId);

        Task Order(FileOrderModel model, int userProfileId, int tenantId);

        Task<string> Submit(string contentType,string id,string requestId,string docId , string clientName, string serverName, int size, string encryptionKey, string encryptionAlgorithm, int tenantId, int userProfileId, IEnumerable<string> authHeader);
        Task<FileViewDTO> View(FileViewModel model, int userProfileId,string ipAddress, int tenantId);
        Task<List<FileViewDTO>> GetFileByDocId(FileViewModel model, int userProfileId, string ipAddress, int tenantId);
    }
}

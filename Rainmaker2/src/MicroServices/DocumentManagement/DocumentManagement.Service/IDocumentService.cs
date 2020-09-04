﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Model;

namespace DocumentManagement.Service
{
    public  interface IDocumentService
    {
        Task<List<DocumendDTO>> GetFiles(string id, string requestId, string docId);
        Task<List<ActivityLogDTO>> GetActivityLog(string id, string requestId, string docId);
        Task<List<GetTemplateModel>> GetDocumentsByTemplateIds(List<string> id, int tenantId);
        Task<List<EmailLogDTO>> GetEmailLog(string id, string requestId, string docId);

        Task<bool> McuRename(string id, string requestId, string docId, string fileId, string newName, string userName);
        Task<bool> AcceptDocument(string id, string requestId, string docId, string userName, IEnumerable<string> authHeader);
        Task<bool> RejectDocument(string id, string requestId, string docId,string message,int userId, string userName, IEnumerable<string> authHeader);
        Task<FileViewDTO> View(AdminFileViewModel model, int userProfileId, string ipAddress, int tenantId);

        Task<bool> UpdateByteProStatus(string id, string requestId, string docId, string fileId, bool isUploaded, int userId, int tenantId);

        Task<bool> DeleteFile(int loanApplicationId, string fileId);
    }
}
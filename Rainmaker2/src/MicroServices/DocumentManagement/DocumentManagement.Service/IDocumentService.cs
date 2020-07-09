using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Model;

namespace DocumentManagement.Service
{
    public  interface IDocumentService
    {
        Task<List<DocumendDTO>> GetFiles(string id, string requestId, string docId);
        Task<List<ActivityLogDTO>> GetActivityLog(string id, string typeId, string docId);
        Task<List<DocumentModel>> GetDocumemntsByTemplateIds(TemplateIdModel templateIdsModel);
        Task<List<EmailLogDTO>> GetEmailLog(string id);

        Task<bool> mcuRename(string id, string requestId, string docId, string fileId, string newName);
        Task<bool> AcceptDocument(string id, string requestId, string docId);
        Task<bool> RejectDocument(string id, string requestId, string docId,string message);
    }
}

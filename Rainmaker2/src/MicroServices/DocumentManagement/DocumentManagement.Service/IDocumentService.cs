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
        Task<List<ActivityLogDTO>> GetActivityLog(string id, string requestId, string docId);
    }
}

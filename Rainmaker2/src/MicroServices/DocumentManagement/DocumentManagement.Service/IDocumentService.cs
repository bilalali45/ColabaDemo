using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Model;

namespace DocumentManagement.Service
{
    public  interface IDocumentService
    {
        Task<List<Document>> GetDocumemntsByTemplateIds(TemplateIdModel templateIdsModel);
    }
}

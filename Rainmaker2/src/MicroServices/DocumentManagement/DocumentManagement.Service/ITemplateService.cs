using DocumentManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Service
{
    public interface ITemplateService
    {
        Task<List<TemplateModel>> GetTemplates(int tenantId, int userProfileId);
        Task<List<TemplateDTO>> GetDocument(string id);
        Task<bool> RenameTemplate(string  id, int tenantid, string newname,int userProfileId);
        Task<bool> DeleteDocument(string id, int tenantid, string documentid, int userProfileId);
        Task<bool> DeleteTemplate(string templateId,int tenantId, int userProfileId);
        Task<string> InsertTemplate(int tenantId,int userProfileId,string name);
        Task<List<CategoryDocumentTypeModel>> GetCategoryDocument();
        Task<bool> AddDocument(string templateId,int tenantId, int userProfileId, string typeId, string docName);
    }
}

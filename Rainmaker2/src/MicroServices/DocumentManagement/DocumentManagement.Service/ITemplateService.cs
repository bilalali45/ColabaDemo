using DocumentManagement.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Service
{
    public interface ITemplateService
    {
        Task<List<TemplateModel>> GetTemplates(int tenantId, int userProfileId);
        Task<List<TemplateDto>> GetDocument(string id);
        Task<bool> RenameTemplate(string  id, int tenantid, string newname,int userProfileId);
        Task<bool> RenameTenantTemplate(string id,
                                        int tenantid,
                                        string newname);
        Task<bool> DeleteDocument(string id, int tenantid, string documentid, int userProfileId);


        Task<bool> DeleteTenantDocument(string id,
                                        int tenantid,
                                        string documentid);
        Task<bool> DeleteTemplate(string templateId,int tenantId, int userProfileId);
        Task<bool> DeleteTenantTemplate(string templateId,
                                        int tenantId);
        Task<string> InsertTemplate(int tenantId,int userProfileId,string name);
        Task<string> InsertTenantTemplate(int tenantId,
                                          string name);
        Task<List<CategoryDocumentTypeModel>> GetCategoryDocument(int tenantId);
        Task<bool> AddDocument(string templateId,int tenantId, int userProfileId, string typeId, string docName);


        Task<bool> AddTenantDocument(string templateId,
                                     int tenantId,
                                     string typeId,
                                     string docName);
        Task<string> SaveTemplate(AddTemplateModel model, int userProfileId, int tenantId);
    }
}

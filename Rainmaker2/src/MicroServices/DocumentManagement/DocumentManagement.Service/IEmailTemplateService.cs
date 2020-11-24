using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DocumentManagement.Model;

namespace DocumentManagement.Service
{
    public interface IEmailTemplateService
    {
        Task<List<Model.EmailTemplate>> GetEmailTemplates(int tenantId);
        Task<Model.EmailTemplate> GetEmailTemplateById(string id);
        Task<Model.EmailTemplate> GetRenderEmailTemplateById(string id, int loanApplicationId, IEnumerable<string> authHeader);
        Task<Model.EmailTemplate> GetDraftEmailTemplateById(string id, int loanApplicationId, int tenantId, IEnumerable<string> authHeader);
        Task<List<TokenModel>> GetTokens();
        Task<string> InsertEmailTemplate(int tenantId, string templateName, string templateDescription, string fromAddress, string toAddress, string CCAddress, string subject, string emailBody, int userProfileId);
        Task<bool> DeleteEmailTemplate(string id, int userProfileId);
        Task<bool> UpdateEmailTemplate(string id,
                                       string templateName,
                                       string templateDescription,
                                       string fromAddress,
                                       string CCAddress,
                                       string subject,
                                       string emailBody,
                                       int userProfileId);
        Task UpdateSortOrder(List<TemplateSortModel> lstEmailTemplates);
    }
}

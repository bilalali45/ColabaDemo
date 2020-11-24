using Setting.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setting.Service
{
    public interface IEmailTemplateService
    {
        Task<List<EmailTemplate>> GetEmailTemplates(int templateTypeId,int tenantId,int userProfileId);
        Task<long> InsertEmailTemplate(int templateTypeId,int tenantId,string templateName, string templateDescription,string fromAddress,string toAddress,string CCAddress,string subject,string emailBody, int userProfileId);
        Task DeleteEmailTemplate(int id,int userProfileId);
        Task UpdateSortOrder(List<Model.EmailTemplate> lstEmailTemplates,
                             int userProfileId);
        Task<List<TokenModel>> GetTokens();
        Task<Model.EmailTemplate> GetEmailTemplateById(int id);
        Task UpdateEmailTemplate(int id, string templateName, string templateDescription, string fromAddress, string toAddress, string CCAddress, string subject, string emailBody, int userProfileId);
        Task<Model.EmailTemplate> GetRenderEmailTemplateById(int id, int loanApplicationId, IEnumerable<string> authHeader);
    }
}

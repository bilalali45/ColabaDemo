using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static DocumentManagement.Model.Template;

namespace DocumentManagement.Service
{
    public interface ITemplateService
    {
        Task<List<TemplateModel>> GetTemplates(int? tenantId, int userProfileId);
    }
}

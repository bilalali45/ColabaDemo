using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface ISitemapService
    {
        Task<List<Model.Sitemap>> GetMenu(int userProfileId);
        Task<List<Model.Sitemap>> GetSystemAdminMenu();
    }
}

using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rainmaker.Service
{
    public interface ISitemapService
    {
        Task<List<Sitemap>> GetMenu(int userProfileId);
        Task<List<Sitemap>> GeSystemAdmintMenu();
    }
}

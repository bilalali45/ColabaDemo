using Microsoft.EntityFrameworkCore;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class SitemapService : ServiceBase<RainMakerContext, Sitemap>, ISitemapService
    {
        public SitemapService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }

        public async Task<List<Sitemap>> GetMenu(int userProfileId)
        {
            var permission = await Uow.Repository<UserProfile>().Query(x => x.Id == userProfileId && x.IsActive == true)
                            .Include(x => x.UserInRoles).ThenInclude(x => x.UserRole).ThenInclude(x => x.UserPermissionRoleBinders).ThenInclude(x => x.UserPermission)
                            .SelectMany(x => x.UserInRoles.Where(y => y.UserRole.IsActive == true).Select(y => y.UserRole))
                            .SelectMany(x => x.UserPermissionRoleBinders.Where(z => z.UserPermission.IsActive == true && z.UserPermission.IsDeleted == false).Select(d => d.UserPermission))
                            .Select(x => x.Id)
                            .ToListAsync();


            var menu = from sitemap in Uow.Repository<Sitemap>().Query(s => s.IsParent == true || s.IsPermissive == true || (s.UserPermissionId != null && permission.Contains((int)s.UserPermissionId))).OrderBy(c => c.DisplayOrder)
                       select sitemap;

            return await menu.Where(x => x.IsActive == true && x.IsDeleted == false).Distinct().ToListAsync();
        }

        public async Task<List<Sitemap>> GeSystemAdmintMenu()
        {
            return await Uow.Repository<Sitemap>().Query(ss => ss.IsActive != false
                      && ss.IsDeleted != true
                      && (ss.UserPermission.IsActive != false && ss.UserPermission.IsDeleted != true)).Include(ss => ss.UserPermission).OrderBy(c => c.DisplayOrder).ToListAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class SitemapService : ServiceBase<RainMakerContext, Sitemap>, ISitemapService
    {
        public SitemapService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow, services)
        {
        }

        public async Task<List<Model.Sitemap>> GetMenu(int userProfileId)
        {
            var permission = await Uow.Repository<UserProfile>().Query(x => x.Id == userProfileId && x.IsActive)
                            .Include(x => x.UserInRoles).ThenInclude(x => x.UserRole).ThenInclude(x => x.UserPermissionRoleBinders).ThenInclude(x => x.UserPermission)
                            .SelectMany(x => x.UserInRoles.Where(y => y.UserRole.IsActive).Select(y => y.UserRole))
                            .SelectMany(x => x.UserPermissionRoleBinders.Where(z => z.UserPermission.IsActive  && !z.UserPermission.IsDeleted).Select(d => d.UserPermission))
                            .Select(x => x.Id)
                            .ToListAsync();

            var menu = from sitemap in Uow.Repository<Sitemap>().Query(s => s.IsParent || s.IsPermissive || (s.UserPermissionId != null && permission.Contains((int)s.UserPermissionId))).OrderBy(c => c.DisplayOrder)
                       select sitemap;

            return (await menu.Where(x => x.IsActive && !x.IsDeleted).Distinct().ToListAsync()).Select(s => new Model.Sitemap
            {
                Id = s.Id,
                Title = s.Title,
                Url = s.Url,
                IsParent = s.IsParent,
                IsExecutable = s.IsExecutable,
                ParentId = s.ParentId,
                IconClass = s.IconClass,
                DisplayOrder = s.DisplayOrder,
                IsPermissive = s.IsPermissive
            }).ToList();
        }

        public async Task<List<Model.Sitemap>> GetSystemAdminMenu()
        {
            return (await Uow.Repository<Sitemap>().Query(ss => ss.IsActive
                      && !ss.IsDeleted
                      && (ss.UserPermission.IsActive && !ss.UserPermission.IsDeleted)).Include(ss => ss.UserPermission).OrderBy(c => c.DisplayOrder).ToListAsync()).Select(s => new Model.Sitemap
                      {
                          Id = s.Id,
                          Title = s.Title,
                          Url = s.Url,
                          IsParent = s.IsParent,
                          IsExecutable = s.IsExecutable,
                          ParentId = s.ParentId,
                          IconClass = s.IconClass,
                          DisplayOrder = s.DisplayOrder,
                          IsPermissive = s.IsPermissive
                      }).ToList();
        }
    }
}

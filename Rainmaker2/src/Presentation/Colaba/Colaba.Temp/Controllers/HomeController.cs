using Colaba.Temp.Models;
using Identity.Data;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using TrackableEntities.Common.Core;

namespace Colaba.Temp.Controllers
{
    public class SettingModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
    public class MappingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
    public class HomeController : Controller
    {
        private Dictionary<string, dynamic> setupTables = new Dictionary<string, dynamic>
        {
            {"IncomeType",new {udf="udfIncomeType",type=typeof(IncomeType)} },
            {"AssetType",new {udf="udfAssetType",type=typeof(AssetType)} }
        };
        private readonly ILogger<HomeController> _logger;
        private readonly TenantConfigContext tenantConfigContext;
        private readonly IdentityContext identityContext;
        private readonly LoanApplicationContext loanApplicationContext;
        public HomeController(ILogger<HomeController> logger, TenantConfigContext tenantConfigContext,
            IdentityContext identityContext, LoanApplicationContext loanApplicationContext)
        {
            _logger = logger;
            this.tenantConfigContext = tenantConfigContext;
            this.identityContext = identityContext;
            this.loanApplicationContext = loanApplicationContext;
        }

        public async Task<IActionResult> Index([FromRoute]int? id)
        {
            var tenants = await tenantConfigContext.Set<Tenant>().Include(x=>x.TwoFaConfigs).Where(x=>x.IsActive).OrderBy(x=>x.Id).ToListAsync();
            ViewBag.SelectedTenant = id.HasValue ? tenants.First(x => x.Id == id).Id : tenants.First().Id;
            ViewBag.Tenants = tenants;
            var tenant = tenants.First(x=>x.Id== ViewBag.SelectedTenant);
            var twoFaSettings = await tenantConfigContext.Set<TwoFaMode>().OrderBy(x=>x.DisplayOrder).ToListAsync();
            ViewBag.Selected2fa = tenant.TwoFaConfigs.First().BorrowerTwoFaModeId;
            ViewBag.TwoFaSettings = twoFaSettings;
            var branches = await tenantConfigContext.Set<Branch>().Where(x => x.TenantId == tenant.Id).ToListAsync();
            ViewBag.Branches = branches;
            var terms = await tenantConfigContext.Set<TermsCondition>().Include(x=>x.Branch).Where(x => x.TenantId == tenant.Id).OrderBy(x=>x.BranchId).ThenBy(x=>x.TermTypeId).ToListAsync();
            ViewBag.Terms = terms;
            var settings = await loanApplicationContext.Set<Config>()
                                   .Include(navigationPropertyPath: x => x.ConfigSelections).OrderBy(keySelector: x => x.DisplayOrder).ThenBy(x=>x.Id).Select(selector: x => new SettingModel
                                   {
                                       Name = x.Name,
                                       Value = x.ConfigSelections.Any(y => y.TenantId == tenant.Id) ? (int)x.ConfigSelections.First(y => y.TenantId == tenant.Id).SelectionId : 1
                                   }).ToListAsync();
            ViewBag.Configs = settings;
            ViewBag.SetupTables = setupTables;


            var consents = await loanApplicationContext.Set<ConsentType>().Where(a => a.TenantId == tenant.Id).OrderBy(a=>a.OwnTypeId).ThenBy(a=>a.DisplayOrder).ToListAsync();
            if (consents.Count() <=0)
            {
                consents = await loanApplicationContext.Set<ConsentType>().Where(a => a.TenantId == null).OrderBy(a => a.OwnTypeId).ThenBy(a => a.DisplayOrder).ToListAsync();
            }

            ViewBag.Consents = consents;

            ViewBag.Questions = await loanApplicationContext.UdfQuestion(tenant.Id).ToListAsync();
            return View();
        }
        public IActionResult TenantChanged()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> TwoFaChanged()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            var setting = short.Parse(Request.Form["twofaSetting"][0]);
            var tenantConfig = await tenantConfigContext.Set<TwoFaConfig>().FirstAsync(x=>x.TenantId==id);
            tenantConfig.BorrowerTwoFaModeId = setting;
            tenantConfigContext.Update(tenantConfig);
            await tenantConfigContext.SaveChangesAsync();
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> TermsChanged()
        {
            var id = int.Parse(Request.Form["id"][0]);
            var terms = Request.Form[$"terms{id}"][0];
            var tenantId = int.Parse(Request.Form["tenantId"][0]);
            var term = await tenantConfigContext.Set<TermsCondition>().FirstAsync(x => x.Id == id);
            term.TermsContent = terms;
            tenantConfigContext.Update(term);
            await tenantConfigContext.SaveChangesAsync();
            return RedirectToAction("Index", new { id = tenantId });
        }
        public async Task<IActionResult> SettingChanged()
        {
            var tenantId = int.Parse(Request.Form["tenantId"][0]);
            var settings = await loanApplicationContext.Set<Config>().Select(x=>new { Name=x.Name, Id=x.Id }).ToListAsync();
            var tenantSettings = await loanApplicationContext.Set<ConfigSelection>().Where(x=>x.TenantId==tenantId).ToListAsync();
            foreach(var setting in settings)
            {
                var ts = tenantSettings.SingleOrDefault(x=>x.ConfigId==setting.Id);
                if(ts!=null)
                {
                    ts.SelectionId = byte.Parse(Request.Form[setting.Name][0]);
                    loanApplicationContext.Update(ts);
                }
                else
                {
                    ts = new ConfigSelection
                    {
                        ConfigId=setting.Id,
                        SelectionId= byte.Parse(Request.Form[setting.Name][0]),
                        TenantId=tenantId
                    };
                    loanApplicationContext.Add(ts);
                }
            }
            await loanApplicationContext.SaveChangesAsync();
            return RedirectToAction("Index", new { id = tenantId });
        }
        public async Task<IActionResult> ColorChanged()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            var branchId = int.Parse(Request.Form["branchId"][0]);
            var primaryColor = Request.Form["primaryColor"][0];
            var branch = await tenantConfigContext.Set<Branch>().FirstAsync(x => x.TenantId == id && x.Id==branchId);
            branch.PrimaryColor = primaryColor;
            tenantConfigContext.Update(branch);
            await tenantConfigContext.SaveChangesAsync();
            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> DeleteUser()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            var email = Request.Form["email"][0];
            var user = await identityContext.Set<Identity.Entity.Models.User>().FirstOrDefaultAsync(x => x.TenantId == id && x.UserName.ToLower()==email.ToLower() && x.UserTypeId==1);
            if (user != null)
            {
                user.UserName = DateTime.Now.ToString("yyyyMMddHHmmss") + user.UserName;
                identityContext.Update(user);
                await identityContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = id });
        }
        
        [HttpPost]
        public async Task<IActionResult> PopulateTableItems([FromForm] int tenantId,[FromForm] string tableName)
        {
            return Ok((await GetList(tenantId,tableName)).Item1);
        }
        private async Task<(List<MappingModel>,dynamic,dynamic)> GetList(int tenantId, string tableName)
        {
            List<MappingModel> mapping = new List<MappingModel>();
            MethodInfo genericMethod = typeof(LoanApplicationContext).GetMethod("Set");
            MethodInfo specificMethod = genericMethod.MakeGenericMethod(setupTables[tableName].type);
            dynamic set = specificMethod.Invoke(loanApplicationContext, new object[] { });
            dynamic all = await EntityFrameworkQueryableExtensions.ToListAsync(RelationalQueryableExtensions.FromSqlRaw(set, $"select *,'' as DisplayName,'' as TenantAlternateName from {tableName} where isactive=1"));
            dynamic list = await EntityFrameworkQueryableExtensions.ToListAsync(RelationalQueryableExtensions.FromSqlRaw(set, $"select * from {setupTables[tableName].udf}({tenantId},1)"));
            foreach(dynamic item in all)
            {
                var Checked = true;
                if (list.Find(new Predicate<dynamic>(x=>x.Id==item.Id)) == null)
                    Checked = false;
                mapping.Add(new MappingModel { Id=item.Id, Name=item.Name,Checked = Checked });
            }
            return (mapping,list,all);
        }
        
        public async Task<IActionResult> SaveTenantSetup()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            var tableName = Request.Form["tableName"].Count>0? Request.Form["tableName"][0]:null;
            if (!string.IsNullOrEmpty(tableName))
            {
                (var mapping, var list, var all) = await GetList(id, tableName);
                foreach(var item in all)
                {
                    var selection = Request.Form[$"item{item.Id}"].Count>0 ? Request.Form[$"item{item.Id}"][0] : null;
                    var itemId = (int)item.Id;
                    if (string.IsNullOrEmpty(selection))
                    {
                        if(!await loanApplicationContext.Set<InActiveSetupItem>().AnyAsync(x=>x.TenantId==id && x.EntityName==tableName && x.EntityRefId==itemId))
                        {
                            InActiveSetupItem inActiveSetupItem = new InActiveSetupItem
                            {
                                EntityName=tableName,
                                EntityRefId=item.Id,
                                TenantId=id,
                                TrackingState=TrackingState.Added
                            };
                            loanApplicationContext.Add(inActiveSetupItem);
                        }
                    }
                    else
                    {
                        var s = await loanApplicationContext.Set<InActiveSetupItem>().SingleOrDefaultAsync(x => x.TenantId == id && x.EntityName == tableName && x.EntityRefId == itemId);
                        if(s!=null)
                        {
                            s.TrackingState = TrackingState.Deleted;
                            loanApplicationContext.Remove(s);
                        }
                    }
                }
                await loanApplicationContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = id });
        }
        
        public async Task<IActionResult> SaveConsentType()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            int count = Request.Form["OwnTypeId"].Count;


            for (int i = 0; i < count; i++)
            {
                var owntypeid = int.Parse(Request.Form["OwnTypeId"][i]);
                var consentname = Request.Form["consentname"][i];
                var consenttext = Request.Form[$"ConsentText{i}"];
                var consent = await loanApplicationContext.Set<ConsentType>().SingleOrDefaultAsync(a => a.TenantId == id && a.Name == consentname && a.OwnTypeId == owntypeid);


                if (consent != null)
                {
                    consent.Description = consenttext;
                    loanApplicationContext.Set<ConsentType>().Update(consent);


                }
                else
                {

                    consent = new ConsentType()
                    {
                        Name = consentname,
                        Description = consenttext,
                        OwnTypeId = owntypeid,
                        IsActive = true,
                        IsDefault = false,
                        DisplayOrder = i + 1,
                        EntityTypeId = 221,
                        IsSystem = false,
                        IsDeleted = false,
                        TenantId = id,
                        CreatedOnUtc = DateTime.UtcNow
                        
                    };

                   
                    loanApplicationContext.Set<ConsentType>().Add(consent);


                }


            }
            await loanApplicationContext.SaveChangesAsync();

            return RedirectToAction("Index", new { id = id });
        }
        public async Task<IActionResult> QuestionChanged()
        {
            var id = int.Parse(Request.Form["tenantId"][0]);
            var questions = await loanApplicationContext.UdfQuestion(id).ToListAsync();
            foreach(var question in questions)
            {
                var option = int.Parse(Request.Form[$"option{question.Id}"][0]);
                var active = Request.Form[$"check{question.Id}"].Count > 0 ? true : false;
                var tq = await loanApplicationContext.Set<TenantQuestionOverride>().SingleOrDefaultAsync(x => x.QuestionId == question.Id && x.TenantId==id);
                if(tq!=null)
                {
                    tq.IsActive = active;
                    tq.BorrowerDisplayOptionId = option;
                    loanApplicationContext.Update(tq);
                }
                else
                {
                    tq = new TenantQuestionOverride
                    {
                        BorrowerDisplayOptionId=option,
                        IsActive=active,
                        QuestionId=question.Id,
                        TenantId=id
                    };
                    loanApplicationContext.Add(tq);
                }
            }
            await loanApplicationContext.SaveChangesAsync();
            return RedirectToAction("Index", new { id = id });
        }
    }
}

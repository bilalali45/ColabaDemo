using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rainmaker.Model;
using RainMaker.Common;
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
    public class NotificationService : ServiceBase<RainMakerContext,LoanApplication>, INotificationService
    {
        public NotificationService(IUnitOfWork<RainMakerContext> previousUow,
            IServiceProvider services) : base(previousUow: previousUow,
            services: services)
        {
        }
        public async Task<List<int>> GetAssignedUsers(int loanApplicationId)
        {
            List<int> list = new List<int>();
            LoanApplication application = await Repository.Query(x =>
                    x.Id == loanApplicationId).Include(x => x.Opportunity).ThenInclude(x => x.OpportunityLeadBinders)
                .ThenInclude(x => x.Customer)
                .Include(x => x.Opportunity).ThenInclude(x => x.LoanOfficer)
                .Include(x => x.Opportunity).ThenInclude(x => x.LoanCoordinator)
                .Include(x => x.Opportunity).ThenInclude(x => x.LoanProcessor)
                .Include(x => x.Opportunity).ThenInclude(x => x.PreProcessor).FirstAsync();
            if (application.Opportunity.LoanOfficer != null)
                list.Add(application.Opportunity.LoanOfficer.UserId.Value);
            if (application.Opportunity.LoanCoordinator != null)
                list.Add(application.Opportunity.LoanCoordinator.UserId.Value);
            if (application.Opportunity.LoanProcessor != null)
                list.Add(application.Opportunity.LoanProcessor.UserId.Value);
            if (application.Opportunity.PreProcessor != null)
                list.Add(application.Opportunity.PreProcessor.UserId.Value);
            return list.Distinct().ToList();
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId)
        {
            ICommonService commonService = services.GetRequiredService<ICommonService>();
            string url = await commonService.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.AdminDomainUrl);
            return await Repository
                         .Query(query: x =>x.Id == loanApplicationId)
                         .Include(navigationPropertyPath: x => x.PropertyInfo)
                         .ThenInclude(navigationPropertyPath: x => x.PropertyType)
                         .Include(navigationPropertyPath: x => x.PropertyInfo)
                         .ThenInclude(navigationPropertyPath: x => x.AddressInfo)
                         .ThenInclude(navigationPropertyPath: x => x.State)
                         .Include(navigationPropertyPath: x => x.LoanPurpose)
                         .Include(navigationPropertyPath: x => x.StatusList)
                         .Include(navigationPropertyPath: x => x.Opportunity)
                         .ThenInclude(navigationPropertyPath: x => x.OpportunityLeadBinders)
                         .ThenInclude(navigationPropertyPath: x => x.Customer)
                         .Include(navigationPropertyPath: x => x.Borrowers)
                         .ThenInclude(navigationPropertyPath: x => x.LoanContact)
                         .Select(selector: x => new LoanSummary
                         {
                             Url = url,
                             Name = x
                                                        .Borrowers.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact)
                                                        .Select(y =>
                                                            (string
                                                                .IsNullOrEmpty(y.LoanContact
                                                                    .FirstName)
                                                                ? ""
                                                                : y.LoanContact.FirstName) +
                                                            " " +
                                                            (string
                                                                .IsNullOrEmpty(y.LoanContact
                                                                    .LastName)
                                                                ? ""
                                                                : y.LoanContact.LastName))
                                                        .FirstOrDefault(),
                             CityName = x.PropertyInfo.AddressInfo.CityName,
                             CountyName = x.PropertyInfo.AddressInfo.CountyName,
                             LoanAmount = x.LoanAmount,
                             LoanPurpose = x.LoanPurpose.Description,
                             PropertyType = x.PropertyInfo.PropertyType.Description,
                             StateName =
                                                        x.PropertyInfo.AddressInfo.StateId == null ||
                                                        x.PropertyInfo.AddressInfo.StateId == 0
                                                            ? x.PropertyInfo.AddressInfo.StateName
                                                            : x.PropertyInfo.AddressInfo.State.Abbreviation,
                             StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                             ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                             CountryName = x.PropertyInfo.AddressInfo.CountryName,
                             UnitNumber = x.PropertyInfo.AddressInfo.UnitNo
                         }).FirstOrDefaultAsync();
        }
    }
}

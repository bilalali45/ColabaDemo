using Microsoft.EntityFrameworkCore;
using Rainmaker.Model;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using System.Linq;
using RainMaker.Common;

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext,LoanApplication>, ILoanApplicationService
    {
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow,services)
        {
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId, int userProfileId)
        {
            return await Repository.Query(x => x.Opportunity.OpportunityLeadBinders.Where(y=>y.OwnTypeId==(int)OwnTypeEnum.PrimaryContact).First().Customer.UserId==userProfileId && x.Id == loanApplicationId).Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyType)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo)
                .Include(x => x.LoanPurpose)
                .Include(x=>x.Opportunity).ThenInclude(x=>x.OpportunityLeadBinders).ThenInclude(x=>x.Customer)
                .Select(x => new LoanSummary{
                    CityName = x.PropertyInfo.AddressInfo.CityName,
                    CountyName = x.PropertyInfo.AddressInfo.CountyName,
                    LoanAmount = x.LoanAmount,
                    LoanPurpose = x.LoanPurpose.Description,
                    PropertyType = x.PropertyInfo.PropertyType.Description,
                    StateName = x.PropertyInfo.AddressInfo.StateName,
                    StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                    CountryName = x.PropertyInfo.AddressInfo.CountyName
                }).FirstOrDefaultAsync();
        }





        public async Task<LoanOfficer> GetLOInfo(int loanApplicationId, int businessUnitId, int userProfileId)
        {
            return await Repository.Query(x => x.Opportunity.OpportunityLeadBinders.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact).First().Customer.UserId == userProfileId && x.Id == loanApplicationId && x.BusinessUnit.Id == businessUnitId)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee).ThenInclude(x=>x.Contact)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeeBusinessUnitEmails).ThenInclude(x => x.EmailAccount)
                .Include(x => x.Opportunity).ThenInclude(x => x.OpportunityLeadBinders).ThenInclude(x => x.Customer)
                .Include(x => x.BusinessUnit)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeePhoneBinders).ThenInclude(x => x.CompanyPhoneInfo)
                .Select(x =>
                    new LoanOfficer()
                    {
                        Email = x.Opportunity.Employee.EmployeeBusinessUnitEmails.Where(y=>y.BusinessUnitId==businessUnitId).First().EmailAccount.Email,
                        FirstName = x.Opportunity.Employee.Contact.FirstName,
                        LastName = x.Opportunity.Employee.Contact.LastName,
                        NMLS = x.Opportunity.Employee.NmlsNo,
                        Phone = x.Opportunity.Employee.EmployeePhoneBinders.Where(y=>y.TypeId==3).First().CompanyPhoneInfo.Phone,
                        Photo = x.Opportunity.Employee.Photo,
                        WebUrl = x.BusinessUnit.WebUrl+"/lo/"+x.Opportunity.Employee.CmsName
                    }
                ).FirstOrDefaultAsync();
        }

        public async Task<LoanOfficer> GetDbaInfo(int businessUnitId)
        {
            var businessUnit = await Uow.Repository<BusinessUnit>().Query(x => x.Id == businessUnitId && x.BusinessUnitPhones.Where(y => y.TypeId == 3).Count() > 0)
                .Include(x => x.EmailAccount)
                .Include(x => x.BusinessUnitPhones).ThenInclude(x => x.CompanyPhoneInfo)
                .Select(x => new { 
                x.Name,
                x.BusinessUnitPhones.FirstOrDefault().CompanyPhoneInfo.Phone,
                x.EmailAccount.Email,
                x.WebUrl,
                x.Logo
                }).FirstOrDefaultAsync();
            var nmls = (await Uow.Repository<Branch>().Query(x => x.Id == 1).FirstOrDefaultAsync()).NmlsNo;
            return new LoanOfficer()
            {
                Email=businessUnit.Email,
                FirstName=businessUnit.Name,
                LastName=string.Empty,
                NMLS=nmls,
                Phone=businessUnit.Phone,
                Photo=businessUnit.Logo,
                WebUrl=businessUnit.WebUrl
            };
        }
    }
}

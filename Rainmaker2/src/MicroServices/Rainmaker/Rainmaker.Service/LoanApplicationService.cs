﻿using Microsoft.EntityFrameworkCore;
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

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext,LoanApplication>, ILoanApplicationService
    {
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow,services)
        {
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId)
        {
            return await Repository.Query(x => x.Id == loanApplicationId).Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyType)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo)
                .Include(x => x.LoanPurpose)
                .Select(x => new LoanSummary{
                    CityName = x.PropertyInfo.AddressInfo.CityName,
                    CountyName = x.PropertyInfo.AddressInfo.CountyName,
                    LoanAmount = x.LoanAmount,
                    LoanPurpose = x.LoanPurpose.Description,
                    PropertyType = x.PropertyInfo.PropertyType.Description,
                    StateName = x.PropertyInfo.AddressInfo.StateName,
                    StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode
                }).FirstOrDefaultAsync();
        }

        public async Task<LoanOfficer> GetLOInfo(int loanApplicationId, int businessUnitId)
        {
            return await Repository.Query(x => x.Id == loanApplicationId && x.BusinessUnit.Id == businessUnitId)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee).ThenInclude(x=>x.Contact)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeeBusinessUnitEmails).ThenInclude(x => x.EmailAccount)
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
    }
}
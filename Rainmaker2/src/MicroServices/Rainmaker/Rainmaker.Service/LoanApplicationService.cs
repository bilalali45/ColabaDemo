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
    }
}

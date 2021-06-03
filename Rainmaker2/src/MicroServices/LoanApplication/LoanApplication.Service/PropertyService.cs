using Extensions.ExtensionClasses;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class PropertyService : ServiceBase<LoanApplicationContext, PropertyInfo>, IPropertyService
    {
        private readonly IDbFunctionService _dbFunctionService;
        public PropertyService(IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services, IDbFunctionService dbFunctionService) : base(previousUow, services)
        {
            _dbFunctionService = dbFunctionService;
        }
        public async Task<bool> AddOrUpdatePropertyUsage(TenantModel tenant, AddPropertyUsageModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .SingleOrDefaultAsync();

            

            if (loanApplication.PropertyInfo == null)
            {
                loanApplication.PropertyInfo = new PropertyInfo { TrackingState = TrackableEntities.Common.Core.TrackingState.Added,TenantId=tenant.Id };
                loanApplication.TrackingState = TrackingState.Modified;
            }
            else
                loanApplication.PropertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;

            var propertyInfo = loanApplication.PropertyInfo;
            propertyInfo.PropertyUsageId = model.PropertyUsageId;
            propertyInfo.TenantId = tenant.Id;
            var primaryBorrower = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
                    .Include(x => x.LoanApplication).SingleAsync();

            var borrowers = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                    .Include(x => x.LoanApplication).ToListAsync();

            int count = 0;
            if (model.PropertyUsageId == 1)// primary
            {
                borrowers.ForEach(x => { x.WillLiveInSubjectProperty = model.Borrowers?.FirstOrDefault(y => y.Id == x.Id)?.WillLiveIn ?? false; x.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified; });
                count = borrowers.Count(x => x.WillLiveInSubjectProperty == true);
                primaryBorrower.WillLiveInSubjectProperty = true;
                primaryBorrower.TrackingState = TrackingState.Modified;
                count++;// one for primary borrower
            }
            else
            {
                borrowers.ForEach(x => { x.WillLiveInSubjectProperty = false; x.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified; });
                primaryBorrower.WillLiveInSubjectProperty = false;
                primaryBorrower.TrackingState = TrackingState.Modified;
            }
            //var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            loanApplication.State = model.State;
            loanApplication.NoOfPeopleLiveIn = count;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }
        public async Task<GetPropertyUsageModel> GetPropertyUsage(TenantModel tenant, int loanApplicationId, int userId)
        {
            var propertyInfo = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .Select(x => x.PropertyInfo)
                .SingleOrDefaultAsync();

            if (propertyInfo?.PropertyUsageId == null)
            {
                return null;
            }
            else
            {
                var borrowers = await Uow.Repository<Borrower>().Query(x => x.LoanApplication.Id == loanApplicationId && x.TenantId == tenant.Id && x.LoanApplication.UserId == userId && x.OwnTypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Secondary)
                    .Include(x => x.LoanApplication).Include(x => x.LoanContact_LoanContactId).ToListAsync();
                return new GetPropertyUsageModel() { PropertyUsageId = propertyInfo.PropertyUsageId.Value, Borrowers = borrowers.Select(x => new PropertyUsageBorrowerModel { FirstName = x.LoanContact_LoanContactId.FirstName, Id = x.Id, WillLiveIn = propertyInfo.PropertyUsageId != 1 ? (bool?)null : (x.WillLiveInSubjectProperty ?? false) }).ToList() };
            }
        }
        public async Task<PropertyTypeModel> GetPropertyType(TenantModel tenant, int loanApplicationId, int userId)
        {
            var propertyType = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyType)
                .Select(x => new PropertyTypeModel
                {
                    Id = x.PropertyInfo.PropertyType.Id,
                    Name = x.PropertyInfo.PropertyType.Name,
                    Image = null
                }).SingleOrDefaultAsync();
            if (propertyType?.Id == 0)
                propertyType = null;
            return propertyType;
        }

        public async Task<bool> AddOrUpdatePropertyType(TenantModel tenant, AddPropertyTypeModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .SingleAsync();
            if (loanApplication.PropertyInfo == null)
            {
                loanApplication.PropertyInfo = new PropertyInfo
                {
                    TenantId = tenant.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                };
            }
            else
            {
                loanApplication.PropertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            }
            loanApplication.PropertyInfo.PropertyTypeId = model.PropertyTypeId;
            var customPropertyTypes = _dbFunctionService.UdfPropertyType(tenant.Id);
            //loanApplication.PropertyInfo.NoOfUnits = (await Uow.Repository<PropertyType>().Query(x => x.Id == model.PropertyTypeId).SingleAsync()).NoOfUnits;
            loanApplication.PropertyInfo.NoOfUnits = customPropertyTypes.FirstOrDefault(ut => ut.Id == model.PropertyTypeId)?.NoOfUnits;
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<GetPropertyAddressModel> GetPropertyAddress(TenantModel tenant, int userId, int loanApplicationId)
        {
            //var address = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
            //    .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo)
            //    .Select(x => x.PropertyInfo.AddressInfo)
            //    .Select(x => new GetPropertyAddressModel
            //    {
            //        Id = x.Id,
            //        City = x.CityName,
            //        StateId = x.StateId.Value,
            //        Street = x.StreetAddress,
            //        Unit = x.UnitNo,
            //        ZipCode = x.ZipCode,
            //        StateName = x.StateName,
            //        CountryId = x.CountryId,
            //        CountryName = x.CountryName,
            //        EstimatedClosingDate = x.PropertyInfoes.First().LoanApplications.First().ExpectedClosingDate.SpecifyKind(DateTimeKind.Utc)
            //    }
            //    ).SingleAsync();

            var address = new GetPropertyAddressModel();

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(la => la.PropertyInfo).ThenInclude(pi => pi.AddressInfo)
                .SingleAsync();
            if (loanApplication.PropertyInfo != null && loanApplication.PropertyInfo.AddressInfo != null)
            {
                address = new GetPropertyAddressModel()
                {
                    Id = loanApplication.PropertyInfo.AddressInfo.Id,
                    City = loanApplication.PropertyInfo.AddressInfo.CityName,
                    StateId = loanApplication.PropertyInfo.AddressInfo.StateId.Value,
                    Street = loanApplication.PropertyInfo.AddressInfo.StreetAddress,
                    Unit = loanApplication.PropertyInfo.AddressInfo.UnitNo,
                    CountryId = loanApplication.PropertyInfo.AddressInfo.CountryId,
                    CountryName = loanApplication.PropertyInfo.AddressInfo.CountryName,
                    EstimatedClosingDate = loanApplication.ExpectedClosingDate.SpecifyKind(DateTimeKind.Utc)
                };
            }

            if (address?.Id == 0)
                address = null;
            return address;
        }
        public async Task<bool> AddOrUpdatePropertyAddress(TenantModel tenant, AddPropertyAddressModel model, int userId)
        {
            var propertyInfo = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo).ThenInclude(x=>x.AddressInfo)
                .Select(x => x.PropertyInfo)
                .SingleAsync();
            if(propertyInfo.AddressInfo==null)
            {
                propertyInfo.AddressInfo = new AddressInfo
                {
                    TenantId=tenant.Id,
                    TrackingState=TrackableEntities.Common.Core.TrackingState.Added
                };
            }
            else
            {
                propertyInfo.AddressInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            }
            propertyInfo.AddressInfo.CityName = model.City;
            propertyInfo.AddressInfo.CountryId = 1;
            propertyInfo.AddressInfo.CountryName = await Uow.Repository<Country>().Query(x => x.Id == 1).Select(x=>x.Name).SingleAsync();
            propertyInfo.AddressInfo.StateId = model.StateId;
            propertyInfo.AddressInfo.StreetAddress = model.Street;
            propertyInfo.AddressInfo.UnitNo = model.Unit;
            propertyInfo.AddressInfo.ZipCode = model.ZipCode;
            propertyInfo.AddressInfo.StateName = model.StateName;
            propertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            if (application.LoanGoalId == ((int)LoanGoals.PropertyUnderContract))
            {
                application.ExpectedClosingDate = model.EstimatedClosingDate;
            }
            else
            {
                application.ExpectedClosingDate = null;
            }
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            await Uow.SaveChangesAsync();
            return true;
        }
    }
}

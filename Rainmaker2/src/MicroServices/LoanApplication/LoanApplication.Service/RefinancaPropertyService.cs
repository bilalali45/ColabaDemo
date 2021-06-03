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
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using static LoanApplication.Model.RefinancePropertyModel;

namespace LoanApplication.Service
{
    public class RefinancePropertyService : ServiceBase<LoanApplicationContext, PropertyInfo>, IRefinancePropertyService
    {
        private readonly IDbFunctionService _dbFunctionService;
        public RefinancePropertyService(IUnitOfWork<LoanApplicationContext> previousUow, IServiceProvider services, IDbFunctionService dbFunctionService) : base(previousUow, services)
        {
            _dbFunctionService = dbFunctionService;
        }

        public async Task<BorrowerResidenceStatusModel> GetPrimaryBorrowerResidenceHousingStatus(TenantModel tenant, int userId, int loanApplicationId, int borrowerId)
        {
            BorrowerResidence borrowerResidence = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerResidence>().Query(x => x.BorrowerId == borrowerId && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id && x.TypeId == (int)TenantConfig.Common.DistributedCache.OwnType.Primary)
            .SingleAsync();

            BorrowerResidenceStatusModel model = new BorrowerResidenceStatusModel();
            model.borrowerId = borrowerId;
            model.LoanApplicationId = loanApplicationId;
            model.OwnershipTypeId = borrowerResidence.OwnershipTypeId;
            model.borrowerResidenceId = borrowerResidence.Id;

            model.IsSameAsPropertyAddress = borrowerResidence.IsSameAsPropertyAddress;

            return model;
        }

        public async Task<PropertyTypeModel> GetPropertyType(TenantModel tenant, int userId, int loanApplicationId)
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
        public async Task<bool> AddOrUpdatePropertyType(TenantModel tenant, int userId, AddPropertyTypeModel model)
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
            loanApplication.PropertyInfo.NoOfUnits = customPropertyTypes.FirstOrDefault(ut => ut.Id == model.PropertyTypeId)?.NoOfUnits;
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }
        public async Task<GetRefinancePropertyUsageModel> GetPropertyUsageRent(TenantModel tenant,  int userId, int loanApplicationId)
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
                return new GetRefinancePropertyUsageModel() { LoanApplicationId = loanApplicationId, PropertyUsageId = propertyInfo.PropertyUsageId, RentalIncome = propertyInfo.RentalIncome, IsMixedUseProperty = propertyInfo.IsMixedUseProperty, MixedUsePropertyExplanation = propertyInfo.MixedUsePropertyExplanation };
            }
        }
        public async Task<bool> AddOrUpdatePropertyUsageRent(TenantModel tenant, int userId, AddPropertyUsageRefinanceModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
               .Include(x => x.PropertyInfo)
               .SingleOrDefaultAsync();



            if (loanApplication.PropertyInfo == null)
            {
                loanApplication.PropertyInfo = new PropertyInfo { TrackingState = TrackableEntities.Common.Core.TrackingState.Added, TenantId = tenant.Id };
            }
            else
                loanApplication.PropertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;

            var propertyInfo = loanApplication.PropertyInfo;
            propertyInfo.PropertyUsageId = model.PropertyUsageId;
            propertyInfo.TenantId = tenant.Id;

            if (propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.ThisIsAnInvestmentProperty)
            {
                propertyInfo.RentalIncome = model.RentalIncome;
                propertyInfo.IsMixedUseProperty = null;
                propertyInfo.MixedUsePropertyExplanation = null;
            }
            else if (propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.ThisWillBeASecondHome)
            {
                propertyInfo.IsMixedUseProperty = model.IsMixedUseProperty;
                propertyInfo.RentalIncome = null;
                if (propertyInfo.IsMixedUseProperty == true)
                {
                    propertyInfo.MixedUsePropertyExplanation = model.IsMixedUsePropertyExplanation;
                }
                else
                {
                    propertyInfo.MixedUsePropertyExplanation = null;
                }
            }
            else
            {
                throw new Exception("Invalid property usage id");
            }

            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }
        public async Task<PropertyAddressModel> GetBorrowerResidenceAddress(TenantModel tenant, int userId, int borrowerResidenceId, int borrowerId, int loanApplicationId)
        {
            PropertyAddressModel propertyAddressModel = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == borrowerResidenceId && x.BorrowerId == borrowerId && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                     .Select(x => new PropertyAddressModel
                     {
                         IsSameAsPropertyAddress = x.IsSameAsPropertyAddress,
                         Address = new GenericAddressModel
                         {
                             City = x.LoanAddress.CityName,
                             CountryId = x.LoanAddress.CountryId,
                             StateId = x.LoanAddress.StateId,
                             Street = x.LoanAddress.StreetAddress,
                             Unit = x.LoanAddress.UnitNo,
                             ZipCode = x.LoanAddress.ZipCode,
                             CountryName = x.LoanAddress.CountryName,
                             StateName = x.LoanAddress.StateName
                         }
                     })
                    .SingleAsync();

            return propertyAddressModel;
        }

        public async Task<int> AddOrUpdateIsSameAsPropertyAddress(TenantModel tenant, int userId, SameAsPropertyAddress model)
        {
            var borrowerResidence = await Uow.Repository<BorrowerResidence>().Query(x => x.Id == model.BorrowerResidenceId && x.BorrowerId == model.BorrowerId && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.Borrower).ThenInclude(y => y.LoanApplication)
                .SingleAsync();

            borrowerResidence.IsSameAsPropertyAddress = model.IsSameAsPropertyAddress;
            borrowerResidence.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .SingleAsync();
            if (borrowerResidence.IsSameAsPropertyAddress == true)
            {
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
                loanApplication.PropertyInfo.AddressInfoId = borrowerResidence.LoanAddressId;
            }
            else
            {
                if (loanApplication.PropertyInfo != null && loanApplication.PropertyInfo.AddressInfoId == borrowerResidence.LoanAddressId)
                {
                    loanApplication.PropertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    loanApplication.PropertyInfo.AddressInfoId = null;
                }
            }
            loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);

            await SaveChangesAsync();
            return borrowerResidence.Id;
        }

        public async Task<bool> AddOrUpdatePropertyUsageOwn(TenantModel tenant, AddPropertyUsagerequestModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .SingleOrDefaultAsync();

            if (loanApplication.PropertyInfo == null)
            {
                loanApplication.PropertyInfo = new PropertyInfo { TrackingState = TrackableEntities.Common.Core.TrackingState.Added, TenantId = tenant.Id };
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
            if (propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.ThisWillBeASecondHome || propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence)
            {
                propertyInfo.IsMixedUseProperty = model.IsMixedUseProperty;
                if (propertyInfo.IsMixedUseProperty == true)
                {
                    propertyInfo.MixedUsePropertyExplanation = model.IsMixedUsePropertyExplanation;
                }
                else
                {
                    propertyInfo.MixedUsePropertyExplanation = null;
                }
            }
            if ((int)PropertyTypes.Duplex2Unit == propertyInfo.PropertyTypeId || (int)PropertyTypes.Quadplex4Unit == propertyInfo.PropertyTypeId || (int)PropertyTypes.Triplex3Unit == propertyInfo.PropertyTypeId)
            {
                if (propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.ThisIsAnInvestmentProperty || propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.IWillLiveHerePrimaryResidence)
                {
                    loanApplication.PropertyInfo.RentalIncome = model.RentalIncome;
                }
                else
                {
                    loanApplication.PropertyInfo.RentalIncome = null;
                }
            }
            else
            {
                if (propertyInfo.PropertyUsageId.Value == (int)PropertyUsageEnum.ThisIsAnInvestmentProperty)
                {
                    loanApplication.PropertyInfo.RentalIncome = model.RentalIncome;
                }
                else
                {
                    loanApplication.PropertyInfo.RentalIncome = null;
                }
            }

            loanApplication.State = model.State;
            loanApplication.NoOfPeopleLiveIn = count;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<AddPropertyUsagerequestModel> GetPropertyUsageOwn(TenantModel tenant, int loanApplicationId, int userId)
        {
            AddPropertyUsagerequestModel propertyUsagerequestModel = new AddPropertyUsagerequestModel();
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo)
                .Include(x => x.Borrowers).ThenInclude(y => y.LoanContact_LoanContactId)
                .SingleAsync();

            if (loanApplication.PropertyInfo?.PropertyUsageId == null)
            {
                return null;
            }

            propertyUsagerequestModel.IsMixedUseProperty = loanApplication.PropertyInfo.IsMixedUseProperty;
            propertyUsagerequestModel.IsMixedUsePropertyExplanation = loanApplication.PropertyInfo.MixedUsePropertyExplanation;
            propertyUsagerequestModel.RentalIncome = loanApplication.PropertyInfo.RentalIncome;
            propertyUsagerequestModel.LoanApplicationId = loanApplication.Id;
            propertyUsagerequestModel.PropertyUsageId = loanApplication.PropertyInfo.PropertyUsageId.Value;

            propertyUsagerequestModel.Borrowers = loanApplication.Borrowers.Select(x => new RefinancePropertyModel.PropertyUsageModel
            {
                FirstName = x.LoanContact_LoanContactId.FirstName,
                Id = x.Id,
                WillLiveIn = x.WillLiveInSubjectProperty
            }).ToList();

            return propertyUsagerequestModel;
        }


        public async Task<GenericAddressModel> GetPropertyAddress(TenantModel tenant, int userId, int loanApplicationId)
        {
            var address = default(GenericAddressModel);

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(la => la.PropertyInfo).ThenInclude(pi => pi.AddressInfo)
                .SingleAsync();
            if (loanApplication.PropertyInfo != null && loanApplication.PropertyInfo.AddressInfo != null)
            {
                address = new GenericAddressModel()
                {
                    City = loanApplication.PropertyInfo.AddressInfo.CityName,
                    StateId = loanApplication.PropertyInfo.AddressInfo.StateId.Value,
                    Street = loanApplication.PropertyInfo.AddressInfo.StreetAddress,
                    Unit = loanApplication.PropertyInfo.AddressInfo.UnitNo,
                    CountryId = loanApplication.PropertyInfo.AddressInfo.CountryId,
                    CountryName = loanApplication.PropertyInfo.AddressInfo.CountryName,
                    StateName = loanApplication.PropertyInfo.AddressInfo.StateName,
                    ZipCode = loanApplication.PropertyInfo.AddressInfo.ZipCode
                };
            }

            return address;
        }

        public async Task<bool> AddOrUpdatePropertyAddress(TenantModel tenant, AddPropertyAddressModel model, int userId)
        {
            var propertyInfo = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo)
                .Select(x => x.PropertyInfo)
                .SingleAsync();
            if (propertyInfo.AddressInfo == null)
            {
                propertyInfo.AddressInfo = new AddressInfo
                {
                    TenantId = tenant.Id,
                    TrackingState = TrackableEntities.Common.Core.TrackingState.Added
                };
            }
            else
            {
                propertyInfo.AddressInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            }
            propertyInfo.AddressInfo.CityName = model.City;
            propertyInfo.AddressInfo.CountryId = 1;
            propertyInfo.AddressInfo.CountryName = await Uow.Repository<Country>().Query(x => x.Id == 1).Select(x => x.Name).SingleAsync();
            propertyInfo.AddressInfo.StateId = model.StateId;
            propertyInfo.AddressInfo.StreetAddress = model.Street;
            propertyInfo.AddressInfo.UnitNo = model.Unit;
            propertyInfo.AddressInfo.ZipCode = model.ZipCode;
            propertyInfo.AddressInfo.StateName = model.StateName;
            propertyInfo.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(application);
            await Uow.SaveChangesAsync();
            return true;
        }

        public async Task<SubjectPropertyDetails> GetSubjectPropertyDetails(TenantModel tenant, int userId, int loanApplicationId)
        {
            var subjectPropertyDetails = default(SubjectPropertyDetails);

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(la => la.PropertyInfo).ThenInclude(pi => pi.AddressInfo)
                .SingleAsync();
            if (loanApplication.PropertyInfo != null)
            {
                subjectPropertyDetails = new SubjectPropertyDetails()
                {
                    DateAcquired = loanApplication.PropertyInfo.DateAcquired,
                    HoaDues = loanApplication.PropertyInfo.HoaDues,
                    PropertyValue = loanApplication.PropertyInfo.PropertyValue
                };
            }

            return subjectPropertyDetails;
        }

        public async Task<bool> AddOrUpdateSubjectPropertyDetails(TenantModel tenant, SubjectPropertyDetailsRequestModel model, int userId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(la => la.PropertyInfo)
                .SingleAsync();
            if (model != null)
            {
                loanApplication.PropertyInfo.PropertyValue = model.PropertyValue;
                loanApplication.PropertyInfo.HoaDues = model.HoaDues;
                loanApplication.PropertyInfo.DateAcquired = model.DateAcquired;
                loanApplication.IsPropertyIdentified = true;

                loanApplication.PropertyInfo.TrackingState = TrackingState.Modified;


                loanApplication.State = model.State;

                Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
                await Uow.SaveChangesAsync();
                return true;
            }

            return false;
        }


        public async Task<int> AddOrUpdateFirstMortgageValue(TenantModel tenant, int userId, FirstMortgageModel model)
        {

            LoanApplicationDb.Entity.Models.BorrowerProperty borrowerProperty = await Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>()
                .Query(query: x => x.TenantId == tenant.Id && x.Id == model.Id && x.Borrower.LoanApplication.UserId == userId
                        && x.Borrower.LoanApplicationId == model.LoanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyTaxEscrows)
                 .Include(x => x.PropertyInfo).ThenInclude(x => x.MortgageOnProperties)
                .SingleAsync();



            PropertyTaxEscrow AddEscrow(EscrowEntityTypeEnum escrow)
            {
                var tax = borrowerProperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)escrow);
                if (tax == null)
                {
                    tax = new PropertyTaxEscrow();
                    tax.TrackingState = TrackingState.Added;
                    tax.TenantId = tenant.Id;
                    tax.EscrowEntityTypeId = (int)escrow;
                    borrowerProperty.PropertyInfo.PropertyTaxEscrows.Add(tax);
                }
                else
                {
                    tax.TrackingState = TrackingState.Modified;
                }
                return tax;
            }


            MortgageOnProperty AddMortgage(MortgageTypeEnum mortgageType)
            {
                var mortgageOnProperty = borrowerProperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)mortgageType);
                if (mortgageOnProperty == null)
                {
                    mortgageOnProperty = new MortgageOnProperty();
                    mortgageOnProperty.TrackingState = TrackingState.Added;
                    mortgageOnProperty.TenantId = tenant.Id;

                    mortgageOnProperty.MortgageTypeId = (int)mortgageType;

                    borrowerProperty.PropertyInfo.MortgageOnProperties.Add(mortgageOnProperty);
                }
                else
                {
                    mortgageOnProperty.TrackingState = TrackingState.Modified;
                }
                return mortgageOnProperty;
            }



            borrowerProperty.PropertyInfo.TrackingState = TrackingState.Modified;
            borrowerProperty.PropertyInfo.HasFirstMortgage = true;

            if (model.PropertyTax.HasValue)
            {
                var propertyTax = AddEscrow(EscrowEntityTypeEnum.PropertyTax);
                propertyTax.AnnuallyPayment = model.PropertyTax.Value;
                propertyTax.IncludedInMortgagePayment = model.PropertyTaxesIncludeinPayment;
            }

            if (model.HomeOwnerInsurance.HasValue)
            {
                var hoi = AddEscrow(EscrowEntityTypeEnum.HomeOwnerInsurance);
                hoi.AnnuallyPayment = model.HomeOwnerInsurance.Value;
                hoi.IncludedInMortgagePayment = model.HomeOwnerInsuranceIncludeinPayment;
            }

            if (model.FloodInsurance.HasValue)
            {
                var flood = AddEscrow(EscrowEntityTypeEnum.FloodInsurance);
                flood.AnnuallyPayment = model.FloodInsurance.Value;
                flood.IncludedInMortgagePayment = model.FloodInsuranceIncludeinPayment;
            }

            var firstmortgage = AddMortgage(MortgageTypeEnum.FirstMortgage);
            firstmortgage.MonthlyPayment = model.FirstMortgagePayment;
            firstmortgage.MortgageBalance = model.UnpaidFirstMortgagePayment;
            firstmortgage.PaidAtClosing = model.PaidAtClosing.Value;

            firstmortgage.IsHeloc = model.IsHeloc;
            if (model.IsHeloc == true)
            {
                firstmortgage.MortgageLimit = model.HelocCreditLimit;
            }
            else
            {
                firstmortgage.MortgageLimit = null;

            }


            Uow.Repository<LoanApplicationDb.Entity.Models.BorrowerProperty>().Update(borrowerProperty);

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();
            return borrowerProperty.Id;



        }

        public async Task<FirstMortgageModel> GetFirstMortgageValue(TenantModel tenant, int userId, int loanApplicationId)
        {
            //var borrowerproperty = await Uow.Repository<BorrowerProperty>().Query(x => x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == loanApplicationId && x.Borrower.LoanApplication.UserId == userId)
            //    .Include(x => x.PropertyInfo)
            //    .ThenInclude(x => x.PropertyTaxEscrows)
            //    .Include(x => x.PropertyInfo)
            //    .ThenInclude(x => x.MortgageOnProperties)
            //    .SingleOrDefaultAsync();


            var borrowerproperty = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.TenantId == tenant.Id && x.Id == loanApplicationId && x.UserId == userId)
              .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.PropertyTaxEscrows)
                .Include(x => x.PropertyInfo)
                .ThenInclude(x => x.MortgageOnProperties)
              .SingleOrDefaultAsync();




            if (borrowerproperty?.PropertyInfo?.MortgageOnProperties.Any(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage) == true)
            {
                FirstMortgageModel model = new FirstMortgageModel();

                model.Id = borrowerproperty.Id;
                model.LoanApplicationId = loanApplicationId;
                model.FirstMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MonthlyPayment;
                model.UnpaidFirstMortgagePayment = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MortgageBalance;

                model.HomeOwnerInsurance = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.AnnuallyPayment;
                model.HomeOwnerInsuranceIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.IncludedInMortgagePayment;

                model.FloodInsurance = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.AnnuallyPayment;
                model.FloodInsuranceIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.IncludedInMortgagePayment;

                model.PropertyTax = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.AnnuallyPayment;
                model.PropertyTaxesIncludeinPayment = borrowerproperty.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.IncludedInMortgagePayment;

                model.IsHeloc = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.IsHeloc;
                model.HelocCreditLimit = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.MortgageLimit;

                model.PaidAtClosing = borrowerproperty.PropertyInfo.MortgageOnProperties.FirstOrDefault(x => x.MortgageTypeId == (int)MortgageTypeEnum.FirstMortgage)?.PaidAtClosing;

                return model;
            }
            else
                return null;
        }



        public async Task<HasMortgageModel> DoYouHaveFirstMortgage(TenantModel tenant, int userId, int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                                .Include(x => x.PropertyInfo).ThenInclude(y => y.PropertyTaxEscrows)
                                .SingleAsync();

            var model = new HasMortgageModel();
            model.HasFirstMortgage = loanApplication.PropertyInfo.HasFirstMortgage;
            model.LoanApplicationId = loanApplicationId;

            if (model.HasFirstMortgage == false)
            {
                model.PropertyTax = loanApplication.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTax)?.AnnuallyPayment;
                model.HomeOwnerInsurance = loanApplication.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnerInsurance)?.AnnuallyPayment;
                model.FloodInsurance = loanApplication.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.FloodInsurance)?.AnnuallyPayment;
            }
            return model;

        }

        public async Task<int> AddOrUpdateHasFirstMortgage(TenantModel tenant, int userId, HasMortgageModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.PropertyInfo).ThenInclude(y => y.PropertyTaxEscrows)
                .SingleAsync();

            PropertyTaxEscrow AddEscrow(EscrowEntityTypeEnum escrow)
            {
                var tax = loanApplication.PropertyInfo.PropertyTaxEscrows.FirstOrDefault(x => x.EscrowEntityTypeId == (int)escrow);
                if (tax == null)
                {
                    tax = new PropertyTaxEscrow();
                    tax.TrackingState = TrackingState.Added;
                    tax.TenantId = tenant.Id;
                    tax.EscrowEntityTypeId = (int)escrow;
                    loanApplication.PropertyInfo.PropertyTaxEscrows.Add(tax);
                }
                else
                {
                    tax.TrackingState = TrackingState.Modified;
                }
                return tax;
            }

            loanApplication.PropertyInfo.HasFirstMortgage = model.HasFirstMortgage;
            loanApplication.PropertyInfo.TrackingState = TrackingState.Modified;

            if (model.HasFirstMortgage == false)
            {
                if (model.PropertyTax.HasValue)
                {
                    var propertyTax = AddEscrow(EscrowEntityTypeEnum.PropertyTax);
                    propertyTax.AnnuallyPayment = model.PropertyTax.Value;
                }

                if (model.HomeOwnerInsurance.HasValue)
                {
                    var hoi = AddEscrow(EscrowEntityTypeEnum.HomeOwnerInsurance);
                    hoi.AnnuallyPayment = model.HomeOwnerInsurance.Value;
                }

                if (model.FloodInsurance.HasValue)
                {
                    var flood = AddEscrow(EscrowEntityTypeEnum.FloodInsurance);
                    flood.AnnuallyPayment = model.FloodInsurance.Value;
                }
            }
            else
            {
                //foreach (var tax in borrowerProperty.PropertyInfo.PropertyTaxEscrows)
                //{
                //    tax.TrackingState = TrackingState.Deleted;
                //}
            }

            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);

            await SaveChangesAsync();
            return loanApplication.Id;
        }
    }
}
